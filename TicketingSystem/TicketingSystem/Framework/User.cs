using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Security.Cryptography;

namespace TicketingSystem.Framework
{
    public class User
    {
        public int ID;
        public int userType;
        public string password;
        public string email;
        public string firstName;
        public string lastName;

        /// <summary>
        /// Gets a user based on their ID
        /// </summary>
        /// <param name="ID">The users ID</param>
        /// <returns></returns>
        public static User GetUserFromID(int ID)
        {
            User user = new User();

            SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
            SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
            SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

            command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
            command.CommandText = "SELECT * FROM Users WHERE (ID='" + ID.ToString() + "');";
            sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

            if (sqlReader.HasRows) //  USER FOUND WITH MATCHING CREDENTIALS
            {
                while (sqlReader.Read())
                {
                    user.ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                    user.password = sqlReader.GetString(1);  //  Sets this instance's password to the the corresponding cell in the matching row
                    user.userType = sqlReader.GetInt32(2);   //  Sets this instance's usertype to the the corresponding cell in the matching row
                    user.email = sqlReader.GetString(3);     //  Sets this instance's email to the the corresponding cell in the matching row
                    user.firstName = sqlReader.GetString(4); //  Sets this instance's first name to the the corresponding cell in the matching row
                    user.lastName = sqlReader.GetString(5);  //  Sets this instance's last name to the the corresponding cell in the matching row

                    Server.CloseConnection(sqlReader, command, connection);
                    return user;
                }
            }
            else //  INCORRECT / INVALID CREDENTIALS
            {
                Server.CloseConnection(sqlReader, command, connection);
                return null;
            }
            return null;
        }

        /// <summary>
        /// Logs a user in and gets their details from the server
        /// </summary>
        /// <param name="_ID">user id</param>
        /// <param name="_NonHashedPassword">users raw password</param>
        /// <returns></returns>
        public bool Login(string _ID, string _NonHashedPassword)
        {
            string _Password = Server.HashString(_NonHashedPassword);
            
            SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
            SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
            SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

            command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
            command.CommandText = "SELECT * FROM Users WHERE (ID='" + _ID + "' OR Email='" + _ID + "') AND Password='" + _Password + "';";
            sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

            if (sqlReader.HasRows) //  USER FOUND WITH MATCHING CREDENTIALS
            {
                while (sqlReader.Read())
                {
                    ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                    password = sqlReader.GetString(1);  //  Sets this instance's password to the the corresponding cell in the matching row
                    userType = sqlReader.GetInt32(2);   //  Sets this instance's usertype to the the corresponding cell in the matching row
                    email = sqlReader.GetString(3);     //  Sets this instance's email to the the corresponding cell in the matching row
                    firstName = sqlReader.GetString(4); //  Sets this instance's first name to the the corresponding cell in the matching row
                    lastName = sqlReader.GetString(5);  //  Sets this instance's last name to the the corresponding cell in the matching row

                    Server.CloseConnection(sqlReader, command, connection);
                    return true;
                }
            }
            else //  INCORRECT / INVALID CREDENTIALS
            {
                Server.CloseConnection(sqlReader, command, connection);
                return false;
            }
            return false;
        }

        /// <summary>
        /// Changes the password of the current instance of the user if the old password matches the current password
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            oldPassword = Server.HashString(oldPassword);
            newPassword = Server.HashString(newPassword);
            //  CHECKS IF THE NEW PASSWORDS MATCHES, AND IF THE OLD PASSWORD MATCHES THEIR CURRENT PASSWORD
            if (oldPassword == password)
            {
                SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
                SqlDataAdapter adapter = new SqlDataAdapter();

                string commandText = "UPDATE Users SET Password='" + newPassword + "' WHERE ID='" + ID + "';";
                adapter.InsertCommand = new SqlCommand(commandText, connection);
                adapter.InsertCommand.ExecuteNonQuery();
                
                Server.CloseConnection(connection);
                password = newPassword;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the email of the current instance of the user
        /// </summary>
        /// <param name="newEmail"></param>
        public void ChangeEmail(string newEmail)
        {
            //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
            SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
            SqlDataAdapter adapter = new SqlDataAdapter();
            string commandText = "UPDATE Users SET Email='" + newEmail + "' WHERE ID='" + ID + "';";
            adapter.InsertCommand = new SqlCommand(commandText, connection);
            adapter.InsertCommand.ExecuteNonQuery();
            Server.CloseConnection(connection);
            email = newEmail;
        }

        /// <summary>
        /// Gets a list of all the account ids in the system
        /// </summary>
        /// <returns></returns>
        public static List<int> GetAllAccountIds()
        {
            SqlConnection connection = Server.GetConnection(Server.SOURCE_USERS);
            SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
            SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

            command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
            command.CommandText = "SELECT * FROM Users;";
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
            else //  INCORRECT / INVALID CREDENTIALS
            {
                Server.CloseConnection(sqlReader, command, connection);
                return null;
            }
        }

        /// <summary>
        /// TODO: NEEDS IMPLEMENTING
        /// </summary>
        /// <returns>The acitve tickets assigned to a user</returns>
        public int GetActiveTicketsAmount()
        {
            return 0;
        }

        /// <summary>
        /// The user type which defines a users privileges
        /// </summary>
        public enum Type : int
        {
            User = 1,
            Tech = 2,
            Admin = 3,
            Test = 4
        }

        /// <summary>
        /// Gets usertype as a string
        /// </summary>
        /// <param name="user">User to check</param>
        /// <returns></returns>
        public static string TypeToString(User user)
        {
            Type type = (Type)user.userType;
            switch (type)
            {
                case Type.User:
                    {
                        return "User";
                    }
                case Type.Tech:
                    {
                        return "Tech";
                    }
                case Type.Admin:
                    {
                        return "Admin";
                    }
                case Type.Test:
                    {
                        return "Test";
                    }
                default:
                    {
                        Debug.LogError("User type invalid for user " + user.ID.ToString());
                        return "Error - Unknown User Type";
                    }
            }
        }

    }
}