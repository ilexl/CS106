using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data.SqlClient;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for MyAccount.xaml
    /// </summary>
    public partial class MyAccount : Page
    {
        public const string connectionStringUsers = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Users.mdf;Integrated Security=True";

        public MyAccount()
        {
            InitializeComponent();
        }

        private void ButtonClick_ApplyPassword(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;

            //  CHECKS IF THE NEW PASSWORDS MATCHES, AND IF THE OLD PASSWORD MATCHES THEIR CURRENT PASSWORD
            if (NewPassword.Text == ConfirmNewPassword.Text && OldPassword.Text == window.user.password)
            {
                //  DISPOSES CONNECTION WHEN FINISHED
                using (SqlConnection connection = new SqlConnection(connectionStringUsers))
                {
                    connection.Open();

                    //  FILESTREAM / WRITER, ALLOWS INSERTING / UPDATING ROWS IN SQL
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    string commandText = "UPDATE Users SET Password='" + NewPassword.Text + "' WHERE ID='" + window.user.ID + "';";

                    adapter.InsertCommand = new SqlCommand(commandText, connection);

                    adapter.InsertCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
