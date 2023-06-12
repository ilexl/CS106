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

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            ResetText();
        }
        public void ResetText()
        {
            LoginUserName.Text = "Username";
            LoginPassword.Password = string.Empty;
            PasswordGhostText.Visibility = Visibility.Visible;
            LoginUserName.Foreground = Brushes.Gray;
            //LoginPassword.Foreground = Brushes.Gray;

            LoginButton.Focus();
        }
        private void ButtonClick_Login(object sender, RoutedEventArgs e)
        {
            if(!((MainWindow)Application.Current.MainWindow).LoginActivation(LoginUserName.Text, LoginPassword.Password))
            {
                ResetText();
                MessageBoxResult wrongCredentials = MessageBox.Show("Incorrect credentials!");
            }
        }

        private void UTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Username")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        private void UTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Username";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        private void PTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                PasswordGhostText.Visibility = Visibility.Hidden;
            }
        }

        private void PTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(textBox.Password))
            {
                PasswordGhostText.Visibility = Visibility.Visible;
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                if (!((MainWindow)Application.Current.MainWindow).LoginActivation(LoginUserName.Text, LoginPassword.Password))
                {
                    ResetText();
                    MessageBoxResult wrongCredentials = MessageBox.Show("Incorrect credentials!");
                }
            }
        }

        private void PasswordGhostText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginPassword.Focus();
        }
    }
}
