using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem;

namespace TicketingSystem.Framework
{
    public class Ticket
    {
        private int id;
        private bool status; // false or 0 means closed - true or 1 means open
        private string callerID;
        private string creatorID;
        private string title;
        private int urgency;
        private RESOLVEREASON resolveReason;
        private DateTime created;
        private DateTime updated;
        private List<string> comments;

        #region Instance-Get
        public int GetID()
        {
            return id;
        }
        public bool GetStatus()
        {
            return status;
        }
        public string GetCallerID()
        {
            return callerID;
        }
        public string GetCreatorID()
        {
            return creatorID;
        }
        public string GetTitle()
        {
            return title;
        }
        public int GetUrgency()
        {
            return urgency;
        }
        public RESOLVEREASON GetResolveReason()
        {
            return resolveReason;
        }
        public DateTime GetCreatedTime()
        {
            return created;
        }
        public DateTime GetUpdatedTime()
        {
            return updated;
        }
        public List<string> GetComments()
        {
            return comments;
        }
        #endregion

        /// <summary>
        /// Creates an instance of ticket with a known ID
        /// </summary>
        /// <param name="_id">Known id</param>
        public Ticket(int _id, out bool worked)
        {
            this.id = _id;
            worked = GetTicketInfo(id);
        }

        private Ticket()
        {
            // Does nothing - not to be used except by static functions
        }

        public static Ticket CreateNew(string callerID, string creatorID, string title, int urgency, DateTime created)
        {
            Ticket ticket = new Ticket();
            ticket.id = NewID();
            ticket.status = true; // open by default
            ticket.callerID = callerID;
            ticket.creatorID = creatorID;
            ticket.title = title;
            ticket.urgency = urgency;
            ticket.created = created;
            ticket.updated = created;
            ticket.resolveReason = RESOLVEREASON.None;
            ticket.comments = new List<string>();

            AddNewTicket(ticket);
            return ticket;
        }

        private static int NewID()
        {
            try
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);
                string tableName = "AllTickets"; 
                string countQuery = $"SELECT COUNT(*) FROM {tableName}";
                SqlCommand command = new SqlCommand(countQuery, connection);
                int rowCount = (int)command.ExecuteScalar();
                Server.CloseConnection(connection);
                return rowCount + 1;
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred: {ex.Message}");
            }
            return 0; 
        }

        private static void AddNewTicket(Ticket t)
        {
            const string SPACE = ", ";

            #region format
            // format reason and comments
            string reason = "'";
            string commentsAll = "'";
            foreach (string s in t.comments)
            {
                commentsAll += (s + "♦");
            }
            if (commentsAll.EndsWith("♦"))
            {
                commentsAll = commentsAll.Remove(commentsAll.Length - 1, 1); // remove last symbol
            }
            commentsAll += "'";
            if (commentsAll == "''")
            {
                commentsAll = "NULL";
            }
                
            if (t.resolveReason == RESOLVEREASON.None)
            {
                reason = "NULL";
            }
            else
            {
                reason = ((int)t.resolveReason).ToString();
                reason += "'";
            }
            #endregion
            #region createCommand
            string commandText = "INSERT INTO AllTickets VALUES(";
            commandText += t.id.ToString();
            commandText += SPACE;
            commandText += "'" + t.status.ToString() + "'";
            commandText += SPACE;
            commandText += "'" + t.callerID + "'";
            commandText += SPACE;
            commandText += "'" + t.creatorID + "'";
            commandText += SPACE;
            commandText += "@tTitle";
            commandText += SPACE;
            commandText += "'" + t.urgency.ToString() + "'";
            commandText += SPACE;
            commandText += reason;
            commandText += SPACE;
            commandText += "'" + t.created.ToString() + "'";
            commandText += SPACE;
            commandText += "'" + t.updated.ToString() + "'"; ;
            commandText += SPACE;
            commandText += "@commentsAll";
            commandText += ");";
            #endregion

            SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand(commandText, connection);
            adapter.InsertCommand.Parameters.AddWithValue("@tTitle", t.title);
            adapter.InsertCommand.Parameters.AddWithValue("@commentsAll", commentsAll);
            adapter.InsertCommand.ExecuteNonQuery();
            Server.CloseConnection(connection);
        }

        private bool GetTicketInfo(int _id)
        {
            SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);

            SqlDataReader sqlReader;                //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
            SqlCommand command = new SqlCommand();  //  USED TO SPECIFY THE SQL QUERY
            command.Connection = connection;        //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
            command.CommandText = "SELECT * FROM AllTickets WHERE ID='" + _id + "';";
            sqlReader = command.ExecuteReader();    //  TAKES THE OUTPUT INTO THE READER

            if (sqlReader.HasRows)  //  USER FOUND WITH MATCHING CREDENTIALS
            {
                while (sqlReader.Read())
                {
                    id = sqlReader.GetInt32(0);
                    status = sqlReader.GetBoolean(1);
                    callerID = sqlReader.GetString(2);
                    creatorID = sqlReader.GetString(3);
                    title = sqlReader.GetString(4);
                    urgency = sqlReader.GetInt32(5);
                    try
                    {
                        resolveReason = (RESOLVEREASON)sqlReader.GetInt32(6);
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        resolveReason = RESOLVEREASON.None;
                    }
                    created = sqlReader.GetDateTime(7);
                    updated = sqlReader.GetDateTime(8);
                    try
                    {
                        comments = (sqlReader.GetString(9)).Split('♦').ToList();
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        comments = new List<string>();
                    }

                    Server.CloseConnection(sqlReader, command, connection);
                    return true;
                }
            }
            else                    //  INCORRECT / INVALID CREDENTIALS
            {
                Server.CloseConnection(sqlReader, command, connection);
                return false;
            }
            return false;
        }

        public void AddComment(string comment)
        {
            comments.Add(comment);
            comment = "";
            foreach(string c in comments)
            {
                comment += c + '♦';
            }
            if (comment.EndsWith("♦"))
            {
                comment = comment.Remove(comment.Length - 1, 1); // remove last symbol
            }

            using (SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET))
            {
                //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                SqlDataAdapter adapter = new SqlDataAdapter();
                string commandText = "UPDATE AllTickets SET COMMENTS=@comment WHERE ID='" + this.id + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.Parameters.AddWithValue("@comment", comment);
                adapter.InsertCommand.ExecuteNonQuery();
                Server.CloseConnection(connection);
            }

            using (SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                string commandText = "UPDATE AllTickets SET UPDATED='" + DateTime.Now.ToString() + "' WHERE ID='" + this.id + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.ExecuteNonQuery();
                Server.CloseConnection(connection);
            }
        }

        public static List<int> GetAllTicketIds()
        {

            SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);
            SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
            SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

            command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
            command.CommandText = "SELECT * FROM AllTickets;";
            sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

            if (sqlReader.HasRows)                      //  USER FOUND WITH MATCHING CREDENTIALS
            {
                List<int> ids = new List<int>();
                while (sqlReader.Read())
                {
                    int ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                    ids.Add(ID);
                }
                Server.CloseConnection(sqlReader, command, connection);
                return ids;
            }
            else                    //  INCORRECT / INVALID CREDENTIALS
            {
                Server.CloseConnection(sqlReader, command, connection);
                return null;
            }
        }

        public static List<int> GetAllTicketIds(int opa)
        {
            User currentUser = MainWindow.user;
            string cmdAdd = "";
            if(opa == 1)
            {
                cmdAdd = "AND Status='True'";
            }
            if(opa == 2)
            {
                cmdAdd = "AND Status='False'";
            }

            SqlConnection connection = Server.GetConnection(Server.SOURCE_TICKET);
            SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
            SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

            command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
            command.CommandText = "SELECT * FROM AllTickets WHERE (CALLER='" + currentUser.ID.ToString() + "' OR CREATOR='" + currentUser.ID.ToString() + "')" +  cmdAdd + ";";
            sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

            if (sqlReader.HasRows)                      //  USER FOUND WITH MATCHING CREDENTIALS
            {
                List<int> ids = new List<int>();
                while (sqlReader.Read())
                {
                    int ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                    ids.Add(ID);
                }
                Server.CloseConnection(sqlReader, command, connection);
                return ids;
            }
            else                    //  INCORRECT / INVALID CREDENTIALS
            {
                Server.CloseConnection(sqlReader, command, connection);
                return null;
            }
            
        }

        public enum RESOLVEREASON : int
        {
            None = 0,
            FIXED = 1,
            NORESOLUTION = 2,
            ADVISEGIVEN = 3,
            NOLONGERREQUIRED = 4,
            NILRESPONSE = 5,
            OTHER = 6
        }
    }
}
