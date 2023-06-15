using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace TicketingSystem.Framework
{
    public class Server
    {
        internal const string SOURCE_TICKET = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Tickets.mdf;Integrated Security=True";
        internal const string SOURCE_USERS  = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Users.mdf;Integrated Security=True";

        internal static string HashString(string nonHashString)
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

        internal static SqlConnection GetConnection(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection;
            }
        }

        internal static void CloseConnection(SqlDataReader sdr, SqlCommand sc, SqlConnection connection)
        {
            sdr.Close();  //  CLOSES THE READER
            sc.Dispose();  //  NULLS THE COMMAND
            connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE
        }

        internal static void CloseConnection(SqlConnection connection)
        {
            connection.Close(); //  CLOSES OPEN CONNECTION TO SQL DATABASE
        }
    }
}
