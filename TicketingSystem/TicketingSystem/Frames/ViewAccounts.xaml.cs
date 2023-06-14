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
    /// Interaction logic for ViewTickets.xaml
    /// </summary>
    public partial class ViewAccounts : Page
    {
        public ViewAccounts()
        {
            InitializeComponent();
            AllAccounts.Children.Clear();
            List<int> allIds = User.GetAllAccountIds();
            foreach(int id in allIds)
            {
                AddAccountToMenu(id);
            }
        }


        private void AddAccountToMenu(int id)
        {
            User data = User.GetUser(id);

            StackPanel main = AllAccounts;
            StackPanel holder = new StackPanel();
            Border border = new Border();
            Grid spacing = new Grid();

            Label accountIDLabel = new Label();
            Label accountNameLabel = new Label();
            Label accountTypeLabel = new Label();
            Label accountTicketsLabel = new Label();
            Button clickButton = new Button();

            accountIDLabel.Content = data.ID.ToString();
            accountNameLabel.Content = data.firstName + " " + data.lastName;
            accountTypeLabel.Content = User.TypeToString(data);
            accountTicketsLabel.Content = data.GetActiveTicketsAmount();

            border.CornerRadius = new CornerRadius(4);
            border.Background = (Brush)MainWindow.HexColor("#F9F9F9FF");

            holder.Height = 100;
            holder.Orientation = Orientation.Vertical;
            holder.Margin = new Thickness(20, 10, 20, 10);

            ColumnDefinition CD1 = new ColumnDefinition();
            CD1.Width = new GridLength(230);
            ColumnDefinition CD2 = new ColumnDefinition();
            ColumnDefinition CD3 = new ColumnDefinition();
            ColumnDefinition CD4 = new ColumnDefinition();
            CD4.Width = new GridLength(300);
            ColumnDefinition CD5 = new ColumnDefinition();
            ColumnDefinition CD6 = new ColumnDefinition();
            ColumnDefinition CD7 = new ColumnDefinition();
            spacing.ColumnDefinitions.Add(CD1);
            spacing.ColumnDefinitions.Add(CD2);
            spacing.ColumnDefinitions.Add(CD3);
            spacing.ColumnDefinitions.Add(CD4);
            spacing.ColumnDefinitions.Add(CD5);
            spacing.ColumnDefinitions.Add(CD6);
            spacing.ColumnDefinitions.Add(CD7);

            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(100);
            spacing.RowDefinitions.Add(rowDefinition);

            accountIDLabel.SetValue(Grid.ColumnProperty, 0);
            accountIDLabel.FontWeight = FontWeights.SemiBold;
            accountIDLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
            accountIDLabel.HorizontalAlignment = HorizontalAlignment.Left;
            accountIDLabel.Margin = new Thickness(10, 0, 0, 0);
            accountIDLabel.VerticalAlignment = VerticalAlignment.Center;
            accountIDLabel.Width = 200;
            accountIDLabel.Foreground = MainWindow.HexColor("#111122");
            accountIDLabel.FontSize = 32;
            accountIDLabel.FontFamily = (FontFamily)FindResource("Epilogue");

            accountNameLabel.SetValue(Grid.ColumnProperty, 1);
            accountNameLabel.SetValue(Grid.ColumnSpanProperty, 2);
            accountNameLabel.FontWeight = FontWeights.SemiBold;
            accountNameLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
            accountNameLabel.HorizontalAlignment = HorizontalAlignment.Left;
            accountNameLabel.Margin = new Thickness(0,0,0,5);
            accountNameLabel.VerticalAlignment = VerticalAlignment.Center;
            accountNameLabel.Width = 300;
            accountNameLabel.Foreground = MainWindow.HexColor("#111122");
            accountNameLabel.FontSize = 32;
            accountNameLabel.FontFamily = (FontFamily)FindResource("Epilogue");

            accountTypeLabel.SetValue(Grid.ColumnProperty, 3);
            accountTypeLabel.FontWeight = FontWeights.SemiBold;
            accountTypeLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            accountTypeLabel.HorizontalAlignment = HorizontalAlignment.Center;
            accountTypeLabel.Margin = new Thickness(0,0,0,5);
            accountTypeLabel.VerticalAlignment = VerticalAlignment.Center;
            accountTypeLabel.Width = 200;
            accountTypeLabel.Foreground = MainWindow.HexColor("#111122");
            accountTypeLabel.FontSize = 32;
            accountTypeLabel.FontFamily = (FontFamily)FindResource("Epilogue");

            accountTicketsLabel.SetValue(Grid.ColumnProperty, 6);
            accountTicketsLabel.FontWeight = FontWeights.SemiBold;
            accountTicketsLabel.HorizontalContentAlignment = HorizontalAlignment.Right;
            accountTicketsLabel.HorizontalAlignment = HorizontalAlignment.Right;
            accountTicketsLabel.Padding = new Thickness(0,0,20,0);
            accountTicketsLabel.VerticalAlignment = VerticalAlignment.Center;
            accountTicketsLabel.Width = 200;
            accountTicketsLabel.Foreground = MainWindow.HexColor("#111122");
            accountTicketsLabel.FontSize = 32;
            accountTicketsLabel.FontFamily = (FontFamily)FindResource("Epilogue");

            clickButton.SetValue(Grid.ColumnSpanProperty, 7);
            clickButton.Background = MainWindow.HexColor("#00000000");
            clickButton.BorderThickness = new Thickness(0);

            spacing.Children.Add(accountIDLabel);
            spacing.Children.Add(accountNameLabel);
            spacing.Children.Add(accountTypeLabel);
            spacing.Children.Add(accountTicketsLabel);
            spacing.Children.Add(clickButton);
            border.Child = spacing;
            holder.Children.Add(border);
            main.Children.Add(holder);
        }
    }
}
