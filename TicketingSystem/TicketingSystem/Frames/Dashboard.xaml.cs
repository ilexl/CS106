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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
            Holder.Children.Clear();
            User.Type buttonsType = (User.Type)(MainWindow.user.userType);
            switch (buttonsType)
            {
                default:
                case User.Type.User:
                    {
                        AddButton("Create Ticket", "CreateTicket.xaml");
                        AddButton("Open Tickets", "ViewTickets.xaml");
                        AddButton("Closed Tickets", "ViewTickets.xaml");
                        break;
                    }
                case User.Type.Tech:
                    {
                        AddButton("Create Ticket", "CreateTicket.xaml");
                        AddButton("Open Tickets", "ViewTickets.xaml");
                        AddButton("All Tickets", "ViewTickets.xaml");
                        break;
                    }
                case User.Type.Admin:
                    {
                        AddButton("All Tickets", "ViewTickets.xaml");
                        AddButton("All Users", "ViewAccounts.xaml");
                        AddButton("Create Ticket", "CreateTicket.xaml");
                        break;
                    }
            }
        }

        private void AddButton(string title, string navStr)
        {
            int ViewTicketInt = 0;
            if(title == "All Tickets")
            {
                ViewTicketInt = 4;
            }
            else if (title == "Closed Tickets")
            {
                ViewTicketInt = 2;
            }
            else
            {
                ViewTicketInt = 1;
            }            

            Button button = new Button();
            button.Margin = new Thickness(24.5, 0, 0, 0);
            button.Width = 350;

            TextBlock textBlock = new TextBlock();
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Width = 200;
            textBlock.Background = Brushes.Transparent;
            textBlock.Foreground = Brushes.White;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.FontSize = 40;
            textBlock.FontFamily = new FontFamily("Epilogue");
            textBlock.Text = title;

            button.Content = textBlock;
            Style buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, (SolidColorBrush)(new BrushConverter().ConvertFrom("#0066FF"))));
            buttonStyle.Setters.Add(new Setter(Button.TemplateProperty, CreateControlTemplate()));

            Trigger mouseOverTrigger = new Trigger { Property = UIElement.IsMouseOverProperty, Value = true };
            mouseOverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, (SolidColorBrush)(new BrushConverter().ConvertFrom("#0044BB"))));
            buttonStyle.Triggers.Add(mouseOverTrigger);

            button.Resources.Add(typeof(Button), buttonStyle);
            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
            button.Resources.Add(typeof(Border), borderStyle);

            ControlTemplate CreateControlTemplate()
            {
                ControlTemplate template = new ControlTemplate(typeof(Button));

                FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
                border.SetBinding(Border.BackgroundProperty, new Binding
                {
                    Path = new PropertyPath("Background"),
                    RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
                });
                template.VisualTree = border;

                FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
                contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
                border.AppendChild(contentPresenter);

                return template;
            }

            button.Click += (e, sender) =>
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                ViewTickets.viewType = ViewTicketInt;
                mw.ChangeWindow(navStr);
                if (ViewTickets.current != null)
                {
                    ViewTickets.current.Refresh();
                }
            };

            Holder.Children.Add(button);
        }
    }
}
