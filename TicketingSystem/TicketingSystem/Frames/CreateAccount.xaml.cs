using System.Windows;
using System.Windows.Controls;
using TicketingSystem.Framework;

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Page
    {
        /// <summary>
        /// constructor for create account page
        /// </summary>
        public CreateAccount()
        {
            InitializeComponent();
        }

        /// <summary>
        /// returns to previous page in navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        /// <summary>
        /// creates the account if the data is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Password.Text == PasswordConfirm.Text)
            {
                User u = User.CreateNew(FirstName.Text, LastName.Text, EmailAddress.Text, (User.Type)AccountType.SelectedIndex, Password.Text);
                SpecifcAccount.target = u;
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("SpecifcAccount.xaml");
            }
            
        }

        // TODO: ghost text HERE

    }
}
