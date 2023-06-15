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
    public partial class ViewTickets : Page
    {
        public static ViewTickets current;
        public static int viewType;
        // 1 = Current Open Tickets
        // 2 = Current Closed Tickets
        // 3 = Current All Tickets
        // 4 = All Tickets

        public ViewTickets()
        {
            current = this;
            InitializeComponent();
            // All Tickets
            HolderMain.Children.Clear();
            showCorrectTickets(viewType);
        }

        public void Refresh()
        {
            HolderMain.Children.Clear();
            showCorrectTickets(viewType);
        }

        private void showCorrectTickets(int vt)
        {
            switch (vt)
            {
                case 1:
                    {
                        List<int> allTicketIds = Ticket.GetAllTicketIds(1);
                        if(allTicketIds == null)
                        {
                            break;
                        }
                        foreach (int t in allTicketIds)
                        {
                            bool validTicket;
                            Ticket ticket = new Ticket(t, out validTicket);
                            if (validTicket)
                            {
                                AddTicketToList(ticket);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        List<int> allTicketIds = Ticket.GetAllTicketIds(2);
                        if (allTicketIds == null)
                        {
                            break;
                        }
                        foreach (int t in allTicketIds)
                        {
                            bool validTicket;
                            Ticket ticket = new Ticket(t, out validTicket);
                            if (validTicket)
                            {
                                AddTicketToList(ticket);
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        List<int> allTicketIds = Ticket.GetAllTicketIds(3);
                        if (allTicketIds == null)
                        {
                            break;
                        }
                        foreach (int t in allTicketIds)
                        {
                            bool validTicket;
                            Ticket ticket = new Ticket(t, out validTicket);
                            if (validTicket)
                            {
                                AddTicketToList(ticket);
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        List<int> allTicketIds = Ticket.GetAllTicketIds();
                        if (allTicketIds == null)
                        {
                            break;
                        }
                        foreach (int t in allTicketIds)
                        {
                            bool validTicket;
                            Ticket ticket = new Ticket(t, out validTicket);
                            if (validTicket)
                            {
                                AddTicketToList(ticket);
                            }
                        }
                        break;
                    }
                default:
                    {
                        Debug.LogError("Tickets List Not Valid");
                        break;
                    }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            window.ChangeWindow("SpecificTicket.xaml");
        }

        private void AddTicketToList(Ticket t)
        {
            


            Button button = new Button
            {
                Height = 125,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };

            button.Click += (sender, e) =>
            {
                SpecificTicket.target = t;
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("SpecificTicket.xaml");
            };

            Border border = new Border
            {
                Height = 125,
                CornerRadius = new CornerRadius(4),
                Background = new SolidColorBrush(Color.FromRgb(249, 249, 249))
            };

            Grid grid = new Grid()
            {
                Height = 125
            };

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Label label1 = new Label
            {
                Content = t.GetTitle(),
                FontSize = 24,
                FontWeight = FontWeights.Regular,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0),
                FontFamily = new FontFamily("Inter")
            };
            Grid.SetColumn(label1, 0);
            Grid.SetRow(label1, 0);

            Label label2 = new Label
            {
                Content = "Ticket state - stage",
                FontSize = 24,
                FontWeight = FontWeights.Regular,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0),
                FontFamily = new FontFamily("Inter")
            };
            if (t.GetStatus())
            {
                label2.Content = "Open";
            }
            else
            {
                label2.Content = "False";
            }

            Grid.SetColumn(label2, 1);
            Grid.SetRow(label2, 0);

            Label label3 = new Label
            {
                Content = "Created - when made",
                FontSize = 24,
                FontWeight = FontWeights.Regular,
                HorizontalAlignment = HorizontalAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 20, 0),
                FontFamily = new FontFamily("Inter")
            };
            Grid.SetColumn(label3, 2);
            Grid.SetRow(label3, 0);

            Label label4 = new Label
            {
                Content = "INC" + t.GetID().ToString(),
                FontSize = 24,
                FontWeight = FontWeights.Regular,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0),
                FontFamily = new FontFamily("Inter")
            };
            Grid.SetColumn(label4, 0);
            Grid.SetRow(label4, 1);

            Label label5 = new Label
            {
                Content = "urgency - urgenchy lvl",
                FontSize = 24,
                FontWeight = FontWeights.Regular,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0),
                FontFamily = new FontFamily("Inter")
            };

            switch (t.GetUrgency())
            {
                case 1:
                    label5.Content = "1 - High";
                    break;
                case 2:
                    label5.Content = "2 - Medium";
                    break;
                case 3:
                    label5.Content = "3 - Low";
                    break;
            }

            Grid.SetColumn(label5, 1);
            Grid.SetRow(label5, 1);

            Label label6 = new Label
            {
                Content = "updated - when updated",
                FontSize = 24,
                FontWeight = FontWeights.Regular,
                HorizontalAlignment = HorizontalAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 20, 0),
                FontFamily = new FontFamily("Inter")
            };

            TimeSpan CreatedDW = (((DateTime)(t.GetCreatedTime())) - ((DateTime)(DateTime.Now))).Duration(); ;
            if (CreatedDW.Days != 0)
            {
                label3.Content = CreatedDW.Days + "d ago";
            }
            else if (CreatedDW.Hours != 0)
            {
                label3.Content = CreatedDW.Hours + "h ago";
            }
            else
            {
                label3.Content = CreatedDW.Minutes + "m ago";
            }

            TimeSpan UpdatedDW = (((DateTime)(t.GetUpdatedTime())) - ((DateTime)(DateTime.Now))).Duration(); ;
            if (UpdatedDW.Days != 0)
            {
                label6.Content = UpdatedDW.Days + "d ago";
            }
            else if (UpdatedDW.Hours != 0)
            {
                label6.Content = UpdatedDW.Hours + "h ago";
            }
            else
            {
                label6.Content = UpdatedDW.Minutes + "m ago";
            }

            Grid.SetColumn(label6, 2);
            Grid.SetRow(label6, 1);

            


            Style GridStyle = new Style(typeof(Grid));
            GridStyle.Setters.Add(new Setter(Grid.BackgroundProperty, new SolidColorBrush(Color.FromRgb(249, 249, 249))));

            Trigger gridTrigger = new Trigger();
            gridTrigger.Property = Grid.IsMouseOverProperty;
            gridTrigger.Value = true;
            gridTrigger.Setters.Add(new Setter(Grid.BackgroundProperty, new SolidColorBrush(Color.FromRgb(204, 238, 255))));
            GridStyle.Triggers.Add(gridTrigger);

            Style buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(249, 249, 249))));

            ControlTemplate buttonTemplate = new ControlTemplate(typeof(Button));
            FrameworkElementFactory buttonBorder = new FrameworkElementFactory(typeof(Border));
            Binding backgroundBinding = new Binding("Background") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) };
            buttonBorder.SetBinding(Border.BackgroundProperty, backgroundBinding);
            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            buttonBorder.AppendChild(contentPresenter);
            buttonTemplate.VisualTree = buttonBorder;
            buttonStyle.Setters.Add(new Setter(Button.TemplateProperty, buttonTemplate));

            Trigger buttonTrigger = new Trigger();
            buttonTrigger.Property = Button.IsMouseOverProperty;
            buttonTrigger.Value = true;
            buttonTrigger.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(204, 238, 255))));
            buttonStyle.Triggers.Add(buttonTrigger);


            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
            button.Resources.Add(typeof(Border), borderStyle);

            Style borderStyle1 = new Style(typeof(Grid));
            borderStyle1.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
            button.Resources.Add(typeof(Grid), borderStyle1);

            Style borderStyle2 = new Style(typeof(Button));
            borderStyle2.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
            button.Resources.Add(typeof(Button), borderStyle2);

            grid.Resources.Add(typeof(Grid), GridStyle);


            grid.Children.Add(label1);
            grid.Children.Add(label2);
            grid.Children.Add(label3);
            grid.Children.Add(label4);
            grid.Children.Add(label5);
            grid.Children.Add(label6);

            border.Child = button;
            button.Content = grid;
            border.Margin = new Thickness(10);
            

            HolderMain.Children.Add(border);
        }
    }
}
