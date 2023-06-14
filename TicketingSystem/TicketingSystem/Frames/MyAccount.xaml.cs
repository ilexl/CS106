﻿using System;
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
            if (oldPasswordFocused && newPasswordFocused && confirmPasswordFocused)
            {
                if (NewPassword.Password == ConfPassword.Password)
                {
                    if (!MainWindow.user.ChangePassword(OldPassword.Password, NewPassword.Password))
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
                if (Email.Text != MainWindow.user.email)
                {
                    MainWindow.user.ChangeEmail(Email.Text);
                }
            }

            ResetText();
        }

        public void ResetText()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            OldPassword.Password = string.Empty;
            NewPassword.Password = string.Empty;
            ConfPassword.Password = string.Empty;

            PasswordGhostText.Visibility = Visibility.Visible;
            NewPasswordGhostText.Visibility = Visibility.Visible;
            ConfPasswordGhostText.Visibility = Visibility.Visible;

            if (MainWindow.user.email == null)
            {
                Email.Text = "No e-mail address found.";
            }
            else
            {
                Email.Text = MainWindow.user.email;
            }

            AccountIDTextBlock.Text = "#" + MainWindow.user.ID;
            NameTextBlock.Text = MainWindow.user.firstName + " " + MainWindow.user.lastName;

            ApplyButton.Focus();
        }

        private void OldPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            oldPasswordFocused = true;
            PasswordGhostText.Visibility = Visibility.Hidden;
        }

        private void OldPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                oldPasswordFocused = false;
                PasswordGhostText.Visibility = Visibility.Visible;
            }
        }

        private void NewPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            newPasswordFocused = true;
            NewPasswordGhostText.Visibility = Visibility.Hidden;
        }

        private void NewPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                newPasswordFocused = false;
                NewPasswordGhostText.Visibility = Visibility.Visible;
            }
        }

        private void ConfPassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            confirmPasswordFocused = true;
            ConfPasswordGhostText.Visibility = Visibility.Hidden;
        }

        private void ConfPassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                confirmPasswordFocused = false;
                ConfPasswordGhostText.Visibility = Visibility.Visible;
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
            else if(textBox.Text == MainWindow.user.email)
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
                if (MainWindow.user.email != "")
                {
                    if (MainWindow.user.email == null)
                    {
                        textBox.Text = "No e-mail address found.";
                    }
                    else
                    {
                        textBox.Text = MainWindow.user.email;
                    }
                }
                else
                {
                    textBox.Text = MainWindow.user.email;
                }
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                
                if (oldPasswordFocused && newPasswordFocused && confirmPasswordFocused)
                {
                    if (NewPassword.Password == ConfPassword.Password)
                    {
                        if (!MainWindow.user.ChangePassword(OldPassword.Password, NewPassword.Password))
                        {
                            ResetText();
                            MessageBoxResult wrongOldPass = MessageBox.Show("Old password is incorrect!");
                        }
                        else
                        {
                            MessageBoxResult successfullyChangedPassword = MessageBox.Show("Successfully updated password!");
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
                    if (Email.Text != MainWindow.user.email)
                    {
                        MainWindow.user.ChangeEmail(Email.Text);
                        MessageBoxResult successfullyChangedPassword = MessageBox.Show("Successfully updated e-mail address!");
                    }
                }

                ResetText();
            }
        }

        private void NewPasswordGhostText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewPassword.Focus();
        }

        private void PasswordGhostText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OldPassword.Focus();
        }

        private void ConfPasswordGhostText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ConfPassword.Focus();
        }
    }
}
