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
        private const string SOURCE = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Tickets.mdf";

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

        /* Ticket class needs to
         * Create a ticket
         * Add comments to ticket
         * Change properties of a ticket
         */

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
                using (SqlConnection connection = new SqlConnection(SOURCE))
                {
                    connection.Open();
                    string tableName = "AllTickets"; 
                    string countQuery = $"SELECT COUNT(*) FROM {tableName}";
                    using (SqlCommand command = new SqlCommand(countQuery, connection))
                    {
                        int rowCount = (int)command.ExecuteScalar();
                        return rowCount + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred: {ex.Message}");
            }

                return 0; // TODO : Create new id based off database
        }

        private static void AddNewTicket(Ticket t)
        {
            const string SPACE = ", ";
            using (SqlConnection connection = new SqlConnection(SOURCE))  //  DISPOSES CONNECTION WHEN FINISHED
            {
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
                

                string commandText = "INSERT INTO AllTickets VALUES(";
                commandText += t.id.ToString();
                commandText += SPACE;
                commandText += "'" + t.status.ToString() + "'";
                commandText += SPACE;
                commandText += "'" + t.callerID + "'";
                commandText += SPACE;
                commandText += "'" + t.creatorID + "'";
                commandText += SPACE;
                commandText += "'" + t.title + "'";
                commandText += SPACE;
                commandText += "'" + t.urgency.ToString() + "'";
                commandText += SPACE;
                commandText += reason;
                commandText += SPACE;
                commandText += "'" + t.created.ToString() + "'";
                commandText += SPACE;
                commandText += "'" + t.updated.ToString() + "'"; ;
                commandText += SPACE;
                commandText += commentsAll;
                commandText += ");";

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                Debug.Log(commandText);

                //commandText = "INSERT INTO AllTickets VALUES(2, 'True', 'Test', 'Test', 'Test', '2', NULL, '6/8/2023 3:55:00 PM', '6/8/2023 3:55:00 PM', NULL)";



                /*
                string commandText = "INSERT INTO AllTickets (Id, STATUS, CALLER, CREATOR, TITLE, URGENCY, RESOLVEREASON, CREATED, UPDATED, COMMENTS)";
                commandText += "\nVALUES (" + t.id + ", " + t.status.ToString() + ", " + t.callerID + ", " + t.callerID + ", " + t.title + ", " + t.urgency + ", " + reason + ", " + t.created + ", " + t.updated + ", " + commentsAll + ");";
                Debug.Log(commandText);
                */

                adapter.InsertCommand = new SqlCommand(commandText, connection);


                adapter.InsertCommand.ExecuteNonQuery();

                //command.Dispose();
                connection.Close();
            }
        }

        private bool GetTicketInfo(int _id)
        {
            using (SqlConnection connection = new SqlConnection(SOURCE))  //  DISPOSES CONNECTION WHEN FINISHED
            {
                connection.Open();

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

                        sqlReader.Close();  //  CLOSES THE READER
                        command.Dispose();  //  NULLS THE COMMAND
                        connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE

                        return true;
                    }
                }
                else                    //  INCORRECT / INVALID CREDENTIALS
                {
                    sqlReader.Close();  //  CLOSES THE READER
                    command.Dispose();  //  NULLS THE COMMAND
                    connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE

                    return false;
                }
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

            using (SqlConnection connection = new SqlConnection(SOURCE))
            {
                connection.Open();

                //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                SqlDataAdapter adapter = new SqlDataAdapter();
                string commandText = "UPDATE AllTickets SET COMMENTS='" + comment + "' WHERE ID='" + this.id + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.ExecuteNonQuery();
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(SOURCE))
            {
                connection.Open();

                //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                SqlDataAdapter adapter = new SqlDataAdapter();
                string commandText = "UPDATE AllTickets SET UPDATED='" + DateTime.Now.ToString() + "' WHERE ID='" + this.id + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<int> GetAllTicketIds()
        {

            //  DISPOSES CONNECTION WHEN FINISHED
            using (SqlConnection connection = new SqlConnection(SOURCE))
            {
                connection.Open();

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
                    sqlReader.Close();  //  CLOSES THE READER
                    command.Dispose();  //  NULLS THE COMMAND
                    connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE
                    return ids;
                }
                else                    //  INCORRECT / INVALID CREDENTIALS
                {
                    sqlReader.Close();  //  CLOSES THE READER
                    command.Dispose();  //  NULLS THE COMMAND
                    connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE

                    return null;
                }
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
            //  DISPOSES CONNECTION WHEN FINISHED
            using (SqlConnection connection = new SqlConnection(SOURCE))
            {
                connection.Open();

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
                    sqlReader.Close();  //  CLOSES THE READER
                    command.Dispose();  //  NULLS THE COMMAND
                    connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE
                    return ids;
                }
                else                    //  INCORRECT / INVALID CREDENTIALS
                {
                    sqlReader.Close();  //  CLOSES THE READER
                    command.Dispose();  //  NULLS THE COMMAND
                    connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE

                    return null;
                }
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
