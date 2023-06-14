﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for CreateTicket.xaml
    /// </summary>
    public partial class CreateTicket : Page
    {
        private const string SOURCE = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Tickets.mdf";

        public CreateTicket()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;

            //  DISPOSES CONNECTION WHEN FINISHED
            using (SqlConnection connection = new SqlConnection(SOURCE))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();      //  USED TO SPECIFY THE SQL QUERY

                command.Connection = connection;            //  SPECIFIES THE CONNECTION THAT THE COMMAND WILL BE USED IN
                command.CommandText = "SELECT COUNT(*) FROM AllTickets;";

                int amountOfRows = (int)command.ExecuteScalar(); ;        //  TAKES THE OUTPUT INTO THE READER

                DateTime currentDateTime = DateTime.Now;
                string sqlFormattedDate = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                command.CommandText = "INSERT INTO AllTickets (Id,STATUS,CALLER,CREATOR,TITLE,URGENCY,CREATED,UPDATED) VALUES (@val1, @val2, @val3, @val4, @val5, @val6, @val8, @val9)";
                using (command)
                {
                    command.Parameters.AddWithValue("@val1", amountOfRows + 1);
                    command.Parameters.AddWithValue("@val2", 1);
                    command.Parameters.AddWithValue("@val3", window.user.ID);
                    command.Parameters.AddWithValue("@val4", window.user.ID);
                    command.Parameters.AddWithValue("@val5", TitleInput.Text);
                    command.Parameters.AddWithValue("@val6", 1);    //  NEED TO ADD DATABINDING TO COMBOBOX TO READ URGENCY
                    //command.Parameters.AddWithValue("@val7", null);
                    command.Parameters.AddWithValue("@val8", sqlFormattedDate);
                    command.Parameters.AddWithValue("@val9", sqlFormattedDate);
                    //command.Parameters.AddWithValue("@val10", null);

                    command.ExecuteNonQuery();

                }
            }
        }
    }
}
