using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Server;

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

        public static void TestCreate()
        {
            Ticket t = CreateNew("testing123", "testing321", "testing111", 2, DateTime.Now);
            AddNewTicket(t);
        }

        public static Ticket CreateNew(string callerID, string creatorID, string title, int urgency, DateTime created)
        {
            Ticket ticket = new Ticket();
            ticket.id = 2;
            ticket.status = true; // open by default
            ticket.callerID = callerID;
            ticket.creatorID = creatorID;
            ticket.title = title;
            ticket.urgency = urgency;
            ticket.created = created;
            ticket.updated = created;
            ticket.resolveReason = RESOLVEREASON.None;
            ticket.comments = new List<string>();

            return ticket;
        }

        private static int NewID()
        {

            

            return 0; // TODO : Create new id based off database
        }

        private static void AddNewTicket(Ticket t)
        {
            using (SqlConnection connection = new SqlConnection(SOURCE))  //  DISPOSES CONNECTION WHEN FINISHED
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string reason = "";
                string commentsAll = "";
                foreach(string s in t.comments)
                {
                    commentsAll += s;
                }
                if(commentsAll == "")
                {
                    commentsAll = "NULL";
                }
                if(t.resolveReason == RESOLVEREASON.None)
                {
                    reason = "NULL";
                }
                else
                {
                    reason = ((int)t.resolveReason).ToString();
                }

                string commandText = "INSERT INTO AllTickets VALUES(2, 'True', 'Test', 'Test', 'Test', '2', NULL, '6/8/2023 3:55:00 PM', '6/8/2023 3:55:00 PM', NULL)";
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

        private static void DeleteTicket(int _id)
        {

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
                            comments = (sqlReader.GetString(9)).Split(' ').ToList();
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














        enum RESOLVEREASON : int
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
