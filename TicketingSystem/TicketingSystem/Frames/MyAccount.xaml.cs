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

        private bool oldPasswordFocused = false;
        private bool newPasswordFocused = false;
        private bool confirmPasswordFocused = false;
        private bool emailFocused = false;

        public MyAccount()
        {
            InitializeComponent();
            ResetText();
        }

        private void ButtonClick_Apply(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;

            if (oldPasswordFocused && newPasswordFocused && confirmPasswordFocused)
            {
                if (NewPassword.Text == ConfirmNewPassword.Text)
                {
                    if (!window.user.ChangePassword(OldPassword.Text, NewPassword.Text))
                    {
                        ResetText();
                        MessageBoxResult wrongOldPass = MessageBox.Show("Old password is incorrect!");
                    }
                }
                else
                {
                    ResetText();
                    MessageBoxResult nonMatchNewPass = MessageBox.Show("New password does not match in both fields!");
                }
            }

            if (emailFocused)
            {
                if (Email.Text != window.user.email)
                {
                    window.user.ChangeEmail(Email.Text);
                }
            }

            ResetText();
        }

        public void ResetText()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            OldPassword.Text = "Old password";
            NewPassword.Text = "New password";
            ConfirmNewPassword.Text = "Confirm new password";
            if (window.user.email == null)
            {
                Email.Text = "No e-mail address found.";
            }
            else
            {
                Email.Text = window.user.email;
            }

            AccountIDTextBlock.Text = "#" + window.user.ID;
            NameTextBlock.Text = window.user.firstName + " " + window.user.lastName;

            OldPassword.Foreground = Brushes.Gray;
            NewPassword.Foreground = Brushes.Gray;
            ConfirmNewPassword.Foreground = Brushes.Gray;

            ApplyButton.Focus();
        }

        private void OldPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Old password")
            {
                oldPasswordFocused = true;
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        private void OldPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                oldPasswordFocused = false;
                textBox.Text = "Old password";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        private void NewPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "New password")
            {
                newPasswordFocused = true;
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        private void NewPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                newPasswordFocused = false;
                textBox.Text = "New password";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        private void ConfPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Confirm new password")
            {
                confirmPasswordFocused = true;
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        private void ConfPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                confirmPasswordFocused = false;
                textBox.Text = "Confirm new password";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "No e-mail address found.")
            {
                emailFocused = true;
                textBox.Text = string.Empty;
            }
            else if(textBox.Text == window.user.email)
            {
                emailFocused = true;
                textBox.Text = string.Empty;
            }
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                emailFocused = false;
                if (window.user.email != "")
                {
                    if (window.user.email == null)
                    {
                        textBox.Text = "No e-mail address found.";
                    }
                    else
                    {
                        textBox.Text = window.user.email;
                    }
                }
                else
                {
                    textBox.Text = window.user.email;
                }
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                var window = (MainWindow)Application.Current.MainWindow;
                
                if (oldPasswordFocused && newPasswordFocused && confirmPasswordFocused)
                {
                    if (NewPassword.Text == ConfirmNewPassword.Text)
                    {
                        if (!window.user.ChangePassword(OldPassword.Text, NewPassword.Text))
                        {
                            ResetText();
                            MessageBoxResult wrongOldPass = MessageBox.Show("Old password is incorrect!");
                        }
                    }
                    else
                    {
                        ResetText();
                        MessageBoxResult nonMatchNewPass = MessageBox.Show("New password does not match in both fields!");
                    }
                }

                if (emailFocused)
                {
                    if (Email.Text != window.user.email)
                    {
                        window.user.ChangeEmail(Email.Text);
                    }
                }

                ResetText();
            }
        }
    }
}
