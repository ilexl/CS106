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
                if(!window.user.ChangePassword(OldPassword.Text, NewPassword.Text))
                {
                    ResetText();
                    MessageBoxResult wrongOldPass = MessageBox.Show("Old password is incorrect!");
                }
            }
        }

        public void ResetText()
        {
            OldPassword.Text = "Old password";
            NewPassword.Text = "New password";
            ConfirmNewPassword.Text = "Confirm new password";

            OldPassword.Foreground = Brushes.Gray;
            NewPassword.Foreground = Brushes.Gray;
            ConfirmNewPassword.Foreground = Brushes.Gray;
        }

        private void OldPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Old password")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        private void OldPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Old password";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        private void NewPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "New password")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        private void NewPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "New password";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        private void ConfPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Confirm new password")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        private void ConfPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Confirm new password";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        private void OldPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
