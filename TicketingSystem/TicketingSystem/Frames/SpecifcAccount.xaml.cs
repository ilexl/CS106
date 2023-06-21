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
                MessageBox.Show("Operation was not successful!\nPlease try again...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            AccountType.SelectedIndex = user.userType - 1;
        }

        private void ButtonClick_ResetPassword(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirm = MessageBox.Show("Are you sure you want to reset this accounts password?","Warning!", MessageBoxButton.YesNo , MessageBoxImage.Warning);
            if(confirm == MessageBoxResult.Yes)
            {
                string password = User.GenerateRandomPassword();
                target.AdminChangePassword(password);
                MessageBox.Show("The new TEMPORARY password is " + password, "New Password!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ButtonClick_Discard(object sender, RoutedEventArgs e)
        {
            ShowAccountDetails(target);
        }

        private void ButtonClick_Save(object sender, RoutedEventArgs e)
        {
            // TODO: make name editable

            if (Email.Text != target.email)
            {
                if (User.ValidateEmail(Email.Text))
                {
                    target.ChangeEmail(Email.Text);
                }
                else
                {
                    MessageBox.Show("Email address already in use by other account!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            target.ChangeAccountType(AccountType.SelectedIndex + 1);
            MessageBox.Show("Changes saved!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "No e-mail address found.")
            {
                emailFocused = true;
                textBox.Text = string.Empty;
            }
            else if (textBox.Text == MainWindow.user.email)
            {
                emailFocused = true;
                textBox.Text = string.Empty;
            }
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
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

        private void AccountTypeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "No e-mail address found.")
            {
                accountTypeFocused = true;
                textBox.Text = string.Empty;
            }
            else if (textBox.Text == MainWindow.user.email)
            {
                accountTypeFocused = true;
                textBox.Text = string.Empty;
            }
        }

        private void AccountTypeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
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

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirm = MessageBox.Show("Are you sure you want to DELETE this account?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (confirm == MessageBoxResult.Yes)
            {
                User.DeleteAccount(target);
                MessageBox.Show("Account DELETED!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("ViewAccounts.xaml");
            }
        }
    }
}
