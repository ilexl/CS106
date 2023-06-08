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
using TicketingSystem.Classes;

namespace TicketingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user = new User();
        public bool LoggedIn = false; // temp

        //  !!RELATIVE STRING, REFACTORING RECOMMENDED!!
        public const string connectionStringUsers = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Classes\Users.mdf;Integrated Security=True";


        //  UTILITY CLASS TO ASSURE ACCESSIBILITY TO THE USER DETAILS FROM ANY GIVEN CLASS
        internal User User { get => user; set => user = value; }

        public MainWindow()
        {
            InitializeComponent();

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

        private void ChangeWindow(string windowName)
        {
            mainFrame.Navigate(new Uri("./Frames/" + windowName, UriKind.Relative));
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
                Login.Visibility =  Visibility.Visible;
                Login.Navigate(new Uri("./Frames/Login.xaml", UriKind.Relative));
            }
            else
            {
                Login.Visibility = Visibility.Hidden;
            }
        }

        public void LoginActivation(string username, string password)
        {
            User = new User();
            
            if (User.ConnectToDatabase(username, password))
            {
                MainWindowVisability(true);
                LoginWindowVisability(false);
                ChangeWindow("Dashboard.xaml");
            }
        }

        public void Button_Dashboard(object sender, RoutedEventArgs e)
        {
            ChangeWindow("Dashboard.xaml");
        }
        public void Button_Settings(object sender, RoutedEventArgs e)
        {
            ChangeWindow("Settings.xaml");
        }

        public void Button_Account(object sender, RoutedEventArgs e)
        {
            ChangeWindow("MyAccount.xaml");
        }

        public string GetConnectionStringUsers()
        {
            return connectionStringUsers;
        }
    }
}
