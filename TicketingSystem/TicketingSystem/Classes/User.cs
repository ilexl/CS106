using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TicketingSystem.Classes
{
    class User
    {
        int ID;
        int userType;
        string password;
        string email;
        string firstName;
        string lastName;
        
        void ConnectToDatabase(string _ID, string _Password, string _Email)
        {
            string connectionString = Properties.Settings.Default.UserDatabaseConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Users WHERE (ID=" + _ID + " || Email=" + _Email + ") && Password=" + _Password;
                var correctUser = command.ExecuteScalar();
                connection.Close();
            }
        }
    }
}
