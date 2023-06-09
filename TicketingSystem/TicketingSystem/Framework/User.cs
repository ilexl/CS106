using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Security.Cryptography;
using System.Text;

namespace TicketingSystem.Framework
{
    public class User
    {
        public const string connectionStringUsers = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Users.mdf;Integrated Security=True";

        public int ID;
        public int userType;
        public string password;
        public string email;
        public string firstName;
        public string lastName;
        
        public bool ConnectToDatabase(string _ID, string _NonHashedPassword)
        {
            var window = (MainWindow)Application.Current.MainWindow;

            //  DISPOSES CONNECTION WHEN FINISHED
            using (SqlConnection connection = new SqlConnection(connectionStringUsers))
            {
                connection.Open();

                SqlDataReader sqlReader;                    //  FILESTREAM / READER, MAKES THE DATA INDEXABLE
                SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

                string _Password = HashString(_NonHashedPassword);

                command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
                command.CommandText = "SELECT * FROM Users WHERE (ID='" + _ID + "' OR Email='" + _ID + "') AND Password='" + _Password + "';";
                
                sqlReader = command.ExecuteReader();        //  TAKES THE OUTPUT INTO THE READER

                if (sqlReader.HasRows)                      //  USER FOUND WITH MATCHING CREDENTIALS
                {
                    while (sqlReader.Read())
    {
                        ID = sqlReader.GetInt32(0);         //  Sets this instance's ID to the the corresponding cell in the matching row
                        password = sqlReader.GetString(1);  //  Sets this instance's password to the the corresponding cell in the matching row
                        userType = sqlReader.GetInt32(2);   //  Sets this instance's usertype to the the corresponding cell in the matching row
                        email = sqlReader.GetString(3);     //  Sets this instance's email to the the corresponding cell in the matching row
                        firstName = sqlReader.GetString(4); //  Sets this instance's first name to the the corresponding cell in the matching row
                        lastName = sqlReader.GetString(5);  //  Sets this instance's last name to the the corresponding cell in the matching row

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
        public enum Type : int
        {
            User = 1,
            Tech = 2,
            Admin = 3,
            Test = 4
        }

        public static string HashString(string nonHashString)
        {
            byte[] tmpSource;
            byte[] tmpHash;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(nonHashString);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            StringBuilder sOutput = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                sOutput.Append(tmpHash[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            oldPassword = HashString(oldPassword);
            newPassword = HashString(newPassword);
            //  CHECKS IF THE NEW PASSWORDS MATCHES, AND IF THE OLD PASSWORD MATCHES THEIR CURRENT PASSWORD
            if (oldPassword == password)
            {
                //  DISPOSES CONNECTION WHEN FINISHED
                using (SqlConnection connection = new SqlConnection(connectionStringUsers))
                {
                    connection.Open();

                    //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string commandText = "UPDATE Users SET Password='" + newPassword + "' WHERE ID='" + ID + "';";
                    adapter.InsertCommand = new SqlCommand(commandText, connection);
                    adapter.InsertCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
