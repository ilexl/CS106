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
            if(NewPassword.Text == ConfirmNewPassword.Text)
            {
                window.user.ChangePassword(OldPassword.Text, NewPassword.Text);
            }
        }
    }
}
