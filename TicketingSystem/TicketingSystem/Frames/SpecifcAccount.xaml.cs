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
using TicketingSystem.Framework;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for SpecifcAccount.xaml
    /// </summary>
    public partial class SpecifcAccount : Page
    {
        public static User target;
        private bool accountTypeFocused = false;
        private bool emailFocused = false;
        public SpecifcAccount()
        {
            InitializeComponent();
            if(target == null)
            {
                Debug.LogError("No target user found for Specific Account");
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("ViewAccounts.xaml");
            }
            else
            {
                ShowAccountDetails(target);
            }
        }

        private void ShowAccountDetails(User user)
        {
            AccountIDTextBlock.Text = user.ID.ToString();
            NameTextBlock.Text = user.firstName + " " + user.lastName;
            Email.Text = user.email;
            AccountType.Text = User.TypeToString(user);
        }

        private void ButtonClick_ResetPassword(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonClick_Discard(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {

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
            else if (textBox.Text == window.user.email)
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

        private void AccountTypeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "No e-mail address found.")
            {
                accountTypeFocused = true;
                textBox.Text = string.Empty;
            }
            else if (textBox.Text == window.user.email)
            {
                accountTypeFocused = true;
                textBox.Text = string.Empty;
            }
        }

        private void AccountTypeTextBox_LostFocus(object sender, RoutedEventArgs e)
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

    }
}
