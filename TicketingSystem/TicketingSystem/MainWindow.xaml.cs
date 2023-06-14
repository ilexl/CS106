using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicketingSystem.Framework;
using TicketingSystem.Frames;

namespace TicketingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool LoggedIn = false; // temp
        public static User user = new User();
        //public const string connectionStringUsers = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Server\Users.mdf;Integrated Security=True";
       
        
        public MainWindow()
        {
            InitializeComponent();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Debug.SetLogger(new Debug.LogConsole());
            }
            else
            {
                Debug.SetLogger(new Debug.LogTxt());
                // or another that follows the correct interface
            }

            Debug.Log(DateTime.Now.ToString());
            Debug.Log("application started");

            if (LoggedIn)
            {
                MainWindowVisability(true);
                LoginWindowVisability(false);
            }
            else
            {
                MainWindowVisability(false);
                LoginWindowVisability(true);
            }

        }

        public void ChangeWindow(string windowName)
        {
            mainFrame.Navigate(new Uri("./Frames/" + windowName, UriKind.Relative));
            Debug.Log(windowName + " opened");

            if(ViewAccounts.current != null)
            {
                ViewAccounts.current.Refresh();
            }
            if(ViewTickets.current != null)
            {
                ViewTickets.current.Refresh();
            }
        }

        public static SolidColorBrush HexColor(string hex)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
        }


#region LOGIN_STUFF
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            user = new User();
            LoggedIn = false;
            MainWindowVisability(LoggedIn);
            LoginWindowVisability(true);
            ((TicketingSystem.Frames.Login)Login.Content).ResetText();
        }

        /// <summary>
        /// Hides or Shows the main window and nav bar
        /// </summary>
        /// <param name="shown"></param>
        private void MainWindowVisability(bool shown)
        {
            if (shown)
            {
                ContentHolder.Visibility = Visibility.Visible;
                SideBarHolder.Visibility = Visibility.Visible;
            }
            else
            {
                ContentHolder.Visibility = Visibility.Hidden;
                SideBarHolder.Visibility = Visibility.Hidden;
            }
        }

        private void LoginWindowVisability(bool shown)
        {
            if (shown)
            {
                Login.Navigate(new Uri("./Frames/Login.xaml", UriKind.Relative));
                Login.Visibility = Visibility.Visible;
            }
            else
            {
                Login.Visibility = Visibility.Hidden;
            }
        }

        public bool LoginActivation(string username, string password)
        {
            if (user.Login(username, password))
            {
                MainWindowVisability(true);
                LoginWindowVisability(false);
                ChangeWindow("Dashboard.xaml");

                // Temp
                GenerateSideNavButtons((User.Type)user.userType);

                return true;
            }
            else
            {
                return false;
            }
        }
#endregion
#region SIDE_NAV
        private void GenerateSideNavButtons(User.Type loginType)
        {
            var sidenav = SideNavButtonsHolder;
            sidenav.Children.Clear(); // Removes current buttons
            switch (loginType)
            {
                case User.Type.User:
                {
                    CreateSideNavButton("Dashboard", "Dashboard.xaml", "./Resources/Icons/Home.png");
                    CreateSideNavButtonT("View Tickets", "ViewTickets.xaml", "./Resources/Icons/File_dock_search.png", 1);
                    CreateSideNavButtonT("Closed Tickets", "ViewTickets.xaml", "./Resources/Icons/Arhives_group_docks.png", 2);
                    CreateSideNavButton("Create Ticket", "CreateTicket.xaml", "./Resources/Icons/File_dock_add.png");
                    CreateSideNavButton("My Account", "MyAccount.xaml", "./Resources/Icons/Lock.png");
                    CreateSideNavButton("Settings", "Settings.xaml", "./Resources/Icons/Setting_line.png");
                    // Code for User
                    break;
                }
                case User.Type.Tech:
                {
                    CreateSideNavButton("Dashboard", "Dashboard.xaml", "./Resources/Icons/Home.png");
                    CreateSideNavButtonT("View Tickets", "ViewTickets.xaml", "./Resources/Icons/File_dock_search.png", 1);
                    CreateSideNavButtonT("Closed Tickets", "ViewTickets.xaml", "./Resources/Icons/Arhives_group_docks.png", 2);
                    CreateSideNavButtonT("All Tickets", "ViewTickets.xaml", "./Resources/Icons/Arhives_group_docks.png", 3);
                    CreateSideNavButton("Create Ticket", "CreateTicket.xaml", "./Resources/Icons/File_dock_add.png");
                    CreateSideNavButton("My Account", "MyAccount.xaml", "./Resources/Icons/Lock.png");
                    CreateSideNavButton("Settings", "Settings.xaml", "./Resources/Icons/Setting_line.png");
                    // Code for Tech
                    break;
                }    
                case User.Type.Admin:
                {
                    CreateSideNavButton("Dashboard", "Dashboard.xaml", "./Resources/Icons/Home.png");
                    CreateSideNavButtonT("All Tickets", "ViewTickets.xaml", "./Resources/Icons/File_dock_search.png", 4);
                    CreateSideNavButton("All Accounts", "ViewAccounts.xaml", "./Resources/Icons/People.png");
                    CreateSideNavButton("Console", "Console.xaml", "./Resources/Icons/terminal.png");
                    CreateSideNavButton("My Account", "MyAccount.xaml", "./Resources/Icons/Lock.png");
                    CreateSideNavButton("Settings", "Settings.xaml", "./Resources/Icons/Setting_line.png");
                    // Code for Admin
                    break;
                }
                case User.Type.Test:
                default:
                {
                    CreateSideNavButton("Dashboard", "Dashboard.xaml", "./Resources/Icons/Home.png");
                    CreateSideNavButton("View Tickets", "ViewTickets.xaml", "./Resources/Icons/File_dock_search.png");
                    CreateSideNavButton("Closed Tickets", "ClosedTickets.xaml", "./Resources/Icons/Arhives_group_docks.png");
                    CreateSideNavButton("Create Ticket", "CreateTicket.xaml", "./Resources/Icons/File_dock_add.png");
                    CreateSideNavButton("All Tickets", "ViewTickets.xaml", "./Resources/Icons/File_dock_search.png");
                    CreateSideNavButton("All Accounts", "ViewAccounts.xaml", "./Resources/Icons/People.png");
                    CreateSideNavButton("Console", "Console.xaml", "./Resources/Icons/terminal.png");
                    CreateSideNavButton("My Account", "MyAccount.xaml", "./Resources/Icons/Lock.png");
                    CreateSideNavButton("Settings", "Settings.xaml", "./Resources/Icons/Setting_line.png");
                    // Code for Test or No Valid Value - For Testing
                    break;
                }
            }
        }

        /// <summary>
        /// Creates a sidebar button for navigation
        /// </summary>
        /// <param name="nameDisplay">Name of the function as displayes</param>
        /// <param name="nameFrame">Frame to show location</param>
        /// <param name="iconLocation">Icon location to display</param>
        void CreateSideNavButton(string nameDisplay, string nameFrame, string iconLocation)
        {
            var sidenav = SideNavButtonsHolder;
            Button main = new Button();
            StackPanel stackPanel = new StackPanel();
            Image icon = new Image();
            TextBlock textBlock = new TextBlock();

            // button properties
            main.Margin = new Thickness(10);
            main.Background = HexColor("#00000000");
            main.BorderBrush = HexColor("#00000000");
            main.Foreground = HexColor("#FFF9F9F9");

            main.Click += (sender, e) => ChangeWindow(nameFrame);

            // stack panel properties
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Width = 305;

            // icon properties
            icon.HorizontalAlignment = HorizontalAlignment.Left;
            icon.Height = 33;
            icon.Width = 33;
            icon.Source = new BitmapImage(new Uri(iconLocation, UriKind.Relative));
            icon.Margin = new Thickness(10, 0, 3, 7);

            // text block properties
            textBlock.Text = nameDisplay;
            textBlock.FontFamily = (FontFamily)FindResource("Epilogue");
            textBlock.FontWeight = FontWeights.SemiBold;
            textBlock.Height = 38;
            textBlock.Foreground = HexColor("#FFF9F9F9");
            textBlock.FontSize = 32;
            textBlock.TextWrapping = TextWrapping.NoWrap;

            // add each element to correct parents
            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(textBlock);
            main.Content = stackPanel;
            sidenav.Children.Add(main);
        }

        /// <summary>
        /// Creates a sidebar button for navigation
        /// </summary>
        /// <param name="nameDisplay">Name of the function as displayes</param>
        /// <param name="nameFrame">Frame to show location</param>
        /// <param name="iconLocation">Icon location to display</param>
        void CreateSideNavButtonT(string nameDisplay, string nameFrame, string iconLocation, int selection)
        {
            var sidenav = SideNavButtonsHolder;
            Button main = new Button();
            StackPanel stackPanel = new StackPanel();
            Image icon = new Image();
            TextBlock textBlock = new TextBlock();

            // button properties
            main.Margin = new Thickness(10);
            main.Background = HexColor("#00000000");
            main.BorderBrush = HexColor("#00000000");
            main.Foreground = HexColor("#FFF9F9F9");

            main.Click += (sender, e) =>
            {
                ViewTickets.viewType = selection;
                ChangeWindow(nameFrame);
            };
            

            // stack panel properties
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Width = 305;

            // icon properties
            icon.HorizontalAlignment = HorizontalAlignment.Left;
            icon.Height = 33;
            icon.Width = 33;
            icon.Source = new BitmapImage(new Uri(iconLocation, UriKind.Relative));
            icon.Margin = new Thickness(10, 0, 3, 7);

            // text block properties
            textBlock.Text = nameDisplay;
            textBlock.FontFamily = (FontFamily)FindResource("Epilogue");
            textBlock.FontWeight = FontWeights.SemiBold;
            textBlock.Height = 38;
            textBlock.Foreground = HexColor("#FFF9F9F9");
            textBlock.FontSize = 32;
            textBlock.TextWrapping = TextWrapping.NoWrap;

            // add each element to correct parents
            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(textBlock);
            main.Content = stackPanel;
            sidenav.Children.Add(main);
        }
        #endregion


    }
}