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
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Page
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Password.Text == PasswordConfirm.Text && User.ValidateEmail(EmailAddress.Text))
            {
                User u = User.CreateNew(FirstName.Text, LastName.Text, EmailAddress.Text, (User.Type)AccountType.SelectedIndex, Password.Text);
                SpecifcAccount.target = u;
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("SpecifcAccount.xaml");
            }
            else if (!User.ValidateEmail(EmailAddress.Text))
            {
                MessageBoxResult invalidEmail = MessageBox.Show("Email address already in use!");
            }
        }

        private void ResetText()
        {
            // TODO: needs implementing
        }
    }
}
