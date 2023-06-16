using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for SpecificTicket.xaml
    /// </summary>
    public partial class SpecificTicket : Page
    {
        public static Ticket target;
        public SpecificTicket()
        {
            InitializeComponent();
            ViewTicketDetails(target);
        }
        private void ViewTicketDetails(Ticket t)
        {
            bool validTicket = false;
            _ = new Ticket(t.GetID(), out validTicket);
            if (!validTicket)
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.ChangeWindow("Dashboard.xaml");
            }

            TicketNumber.Content = "INC" + t.GetID().ToString();
            TitleT.Content = t.GetTitle();
            if (t.GetStatus())
            {
                Status.Content = "Open";
            }
            else
            {
                Status.Content = "False";
            }
            Caller.Content = t.GetCallerID();
            CreatedBy.Content = t.GetCreatorID();
            Urgency.SelectedIndex = t.GetUrgency() - 1;

            TimeSpan CreatedDW = (((DateTime)(t.GetCreatedTime())) - ((DateTime)(DateTime.Now))).Duration(); ;
            if(CreatedDW.Days != 0)
            {
                Created.Content = CreatedDW.Days + "d ago";
            }
            else if (CreatedDW.Hours != 0)
            {
                Created.Content = CreatedDW.Hours + "h ago";
            }
            else
            {
                Created.Content = CreatedDW.Minutes + "m ago";
            }

            TimeSpan UpdatedDW = (((DateTime)(t.GetUpdatedTime())) - ((DateTime)(DateTime.Now))).Duration(); ;
            if (UpdatedDW.Days != 0)
            {
                Updated.Content = UpdatedDW.Days + "d ago";
            }
            else if (UpdatedDW.Hours != 0)
            {
                Updated.Content = UpdatedDW.Hours + "h ago";
            }
            else
            {
                Updated.Content = UpdatedDW.Minutes + "m ago";
            }

            List<String> comments = t.GetComments();



            for(int i = comments.Count() - 1; i >= 0; i--)
            {
                //  START OF NEW COMMENT GRID CREATION
                Grid newCommentGrid = new Grid();

                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(140, GridUnitType.Pixel);
                
                string comment = comments[i];

                newCommentGrid.RowDefinitions.Add(rowDefinition);



                newCommentGrid.ColumnDefinitions.Add(new ColumnDefinition());

                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(8, GridUnitType.Star);
                newCommentGrid.ColumnDefinitions.Add(columnDefinition);
                ColumnDefinition cd3 = new ColumnDefinition();
                cd3.Width = new GridLength(0, GridUnitType.Pixel);                //  SETS COLUMN WIDTH TO 8 STARS
                newCommentGrid.ColumnDefinitions.Add(cd3);
                //  END OF NEW COMMENT GRID CREATION



                //  START OF INITIALS CREATION
                TextBlock commentInitials = new TextBlock();
                commentInitials.Text = comment.Substring(0, 2);
                commentInitials.FontSize = 50;
                commentInitials.Width = 100;
                Border roundedCorner = new Border();
                roundedCorner.Background = Brushes.White;
                roundedCorner.Width = 100;
                roundedCorner.Height = 100;
                roundedCorner.VerticalAlignment = VerticalAlignment.Center;
                roundedCorner.Padding = new Thickness(17, 12, 0, 0);
                roundedCorner.CornerRadius = new CornerRadius(60);
                roundedCorner.Child = commentInitials;
                roundedCorner.Margin = new Thickness(30, 0,0,0);
                roundedCorner.HorizontalAlignment = HorizontalAlignment.Left;

                StackPanel lineHH = new StackPanel();
                lineHH.Background = MainWindow.HexColor("#F9F9F9");
                lineHH.SetValue(Grid.RowProperty, 0);
                lineHH.SetValue(Grid.RowSpanProperty, 2);
                lineHH.Width = 6;
                lineHH.Margin = new Thickness(77, 0, 0, 0);
                lineHH.HorizontalAlignment = HorizontalAlignment.Left;

                if (i == comments.Count() - 1)
                {
                    lineHH.Margin = new Thickness(77, 30, 0, 0);

                }


                newCommentGrid.Children.Add(lineHH);

                newCommentGrid.Children.Add(roundedCorner);
                //  END OF INITIALS CREATION

                //StackPanel stackPanelL = new StackPanel();                               //  CREATES NEW STACKPANEL
                //stackPanelL.VerticalAlignment = VerticalAlignment.Center;                //  SETS VERTICAL ALIGNMENT OF STACKPANEL TO CENTER
                //stackPanelL.SetValue(Grid.ColumnProperty, 0);
                //stackPanelL.Background = Brushes.White;
                //stackPanelL.Width = 20;

                //  START OF COMMENT STACKPANEL CREATION
                StackPanel stackPanel = new StackPanel();                               //  CREATES NEW STACKPANEL
                stackPanel.VerticalAlignment = VerticalAlignment.Center;                //  SETS VERTICAL ALIGNMENT OF STACKPANEL TO CENTER
                stackPanel.SetValue(Grid.ColumnProperty, 1);                            //  SETS COLUMN OF STACKPANEL TO 1
                


                    //  START OF NAME BLOCK CREATION
                TextBlock nameBlock = new TextBlock();                                  //  CREATES NEW TEXTBLOCK FOR NAME
                nameBlock.Text = comment.Substring(0, 2);                               //  SETS NAME BLOCK TEXT TO INITIALS (FIRST TO LETTERS OF COMMENT STRING)
                nameBlock.Margin = new Thickness(100, 10, 0, 0);                        //  SETS MARGIN OF NAME BLOCK
                nameBlock.FontSize = 40;                                                //  SETS FONT SIZE OF NAME BLOCK TO 40
                nameBlock.FontWeight = FontWeights.SemiBold;                            //  SETS FONT WEIGHT OF NAME BLOCK TO SEMI BOLD
                nameBlock.FontFamily = new FontFamily("{DynamicResource Epilogue}");    //  SETS FONT OF NAME BLOCK
                    //  END OF NAME BLOCK CREATION



                    //  START OF COMMENT BLOCK CREATION
                TextBlock commentBlock = new TextBlock();                               //  CREATES NEW TEXTBLOCK FOR COMMENT
                commentBlock.Text = comment.Substring(2);                               //  SETS COMMENT BLOCK TEXT TO COMMENT FROM INDEX 2 AND AFTER (EVERYTHING AFTER INITIALS)
                commentBlock.TextWrapping = TextWrapping.Wrap;                          //  SETS TEXT WRAPPING TO WRAP
                commentBlock.Margin = new Thickness(100, 0, 100, 10);                   //  SETS MARGIN OF COMMENT BLOCK
                commentBlock.FontSize = 25;                                             //  SETS FONT SIZE OF COMMENT BLOCK TO 25
                commentBlock.FontWeight = FontWeights.Regular;                          //  SETS FONT WEIGHT OF COMMENT BLOCK TO REGULAR
                commentBlock.FontFamily = new FontFamily("{DynamicResource Epilogue}"); //  SETS FONT OF COMMENT BLOCK
                commentBlock.VerticalAlignment = VerticalAlignment.Stretch;             //  SETS VERTICAL ALIGNMENT TO STRETCH
                    //  END OF COMMENT BLOCK CREATION

                stackPanel.Children.Add(nameBlock);                                     //  ADDS NAME TO STACKPANEL
                stackPanel.Children.Add(commentBlock);                                  //  ADDS COMMENT TO STACKPANEL
                //  END OF COMMENT STACKPANEL CREATION



                //  START OF LINE CREATION 
                Line line = new Line();                             
                line.VerticalAlignment = VerticalAlignment.Center;  //  SETS LINE VERTICAL ALIGNMENT TO CENTER
                line.Margin = new Thickness(15, 15, 0, 15);          //  SETS MARGIN OF LINE
                line.StrokeEndLineCap = PenLineCap.Triangle;        //  SETS LINE CAPS TO TRIANGLE
                line.StrokeStartLineCap = PenLineCap.Round;         //  SETS LINE CAPS TO ROUND
                line.Stroke = Brushes.White;                        //  SETS LINE COLOUR TO WHITE


                newCommentGrid.Children.Add(line);                  //  ADDS LINE TO NEW COMMENT GRID
                line.SetValue(Grid.ColumnProperty, 1);              //  SETS LINE TO COLUMN 1
                //  END OF LINE CREATION



                stackPanel.SizeChanged += StackPanel_SizeChanged;   //  ADDS EVENT HANDLER FOR WHEN STACKPANEL SIZE CHANGES

                newCommentGrid.Children.Add(stackPanel);            //  ADDS STACKPANEL TO NEW COMMENT GRID
                
                CommentField.Children.Add(newCommentGrid);          //  ADDS COMMENT TO GRID

                

            }

            Grid startCommentGrid = new Grid();                             //  CREATES NEW GRID FOR START COMMENT
            startCommentGrid.Margin = new Thickness(0, 0, 0, 0);           //  SETS MARGIN OF START COMMENT GRID
            startCommentGrid.Height = 100;                                  //  SETS HEIGHT OF START COMMENT GRID TO 100

            startCommentGrid.ColumnDefinitions.Add(new ColumnDefinition()); //  ADDS NEW COLUMN TO START COMMENT GRID
            ColumnDefinition cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(0, GridUnitType.Pixel);                //  SETS COLUMN WIDTH TO 8 STARS
            startCommentGrid.ColumnDefinitions.Add(cd2);
            ColumnDefinition cd = new ColumnDefinition();                   //  CREATES NEW COLUMN DEFINITION
            cd.Width = new GridLength(8, GridUnitType.Star);                //  SETS COLUMN WIDTH TO 8 STARS
            startCommentGrid.ColumnDefinitions.Add(cd);                     //  ADDS COLUMN DEFINITION TO START COMMENT GRID
                              //  ADDS COLUMN DEFINITION TO START COMMENT GRID

            TextBlock StartInitials = new TextBlock();                      //  CREATES NEW TEXTBLOCK FOR START COMMENT
            StartInitials.Text = "START";                                   //  SETS TEXT OF START COMMENT
            StartInitials.FontSize = 30;                                    //  SETS FONT SIZE OF START COMMENT
            StartInitials.Width = 100;                                      //  SETS WIDTH OF START COMMENT
            Border Start = new Border();                                    //  CREATES NEW BORDER FOR START COMMENT
            Start.Background = Brushes.White;                               //  SETS BACKGROUND OF START COMMENT TO WHITE
            Start.Width = 100;                                              //  SETS WIDTH OF START COMMENT
            Start.Padding = new Thickness(10, 28, 0, 20);                   //  SETS PADDING OF START COMMENT
            Start.CornerRadius = new CornerRadius(60);                      //  SETS CORNER RADIUS OF START COMMENT
            Start.Child = StartInitials;
            Start.Margin = new Thickness(30, 0, 0, 0);
            Start.HorizontalAlignment = HorizontalAlignment.Left;//  ADDS START COMMENT TO START COMMENT BORDER
            Start.SetValue(Grid.RowProperty, 1);

            StackPanel lineH = new StackPanel();
            lineH.Background = MainWindow.HexColor("#F9F9F9");
            lineH.SetValue(Grid.RowProperty, 0);
            lineH.SetValue(Grid.RowSpanProperty, 2);
            lineH.Width = 6;
            lineH.Margin = new Thickness(77, 0, 0, 0);
            lineH.HorizontalAlignment = HorizontalAlignment.Left;



            startCommentGrid.Children.Add(lineH);                           //  ADDS START COMMENT TO START COMMENT GRID
            startCommentGrid.Children.Add(Start);                           //  ADDS START COMMENT TO START COMMENT GRID


            CommentField.Children.Add(startCommentGrid);                    //  ADDS START COMMENT GRID TO COMMENT FIELD
        }

        /// <summary>
        /// Resets UI elements and empties text box
        /// </summary>
        private void ResetText()
        {
            CommentInputField.Text = "Add Comment Here";
            CommentInputField.Foreground = Brushes.Gray;
            CommentField.Children.Clear();
            CommentButton.Focus();
            ViewTicketDetails(target);
        }

        /// <summary>
        /// Event handler for when the comment button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommentInputField.Text == string.Empty || CommentInputField.Text == "Add Comment Here")
            {
                ResetText();
                MessageBoxResult emptyComment = MessageBox.Show("Fill out comment before submitting!");
            }
            else
            {
                target.AddComment(CommentInputField.Text);
                ResetText();
            }
        }

        /// <summary>
        /// Event handler for when the comment input field is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentInputField_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Add Comment Here")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        /// <summary>
        /// Event handler for when the comment input field loses focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentInputField_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Add Comment Here";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

        /// <summary>
        /// Event handler for when the enter key is pressed while the comment input field is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                if (CommentInputField.Text == string.Empty || CommentInputField.Text == "Add Comment Here")
                {
                    ResetText();
                    MessageBoxResult emptyComment = MessageBox.Show("Fill out comment before submitting!");
                }
                else
                {
                    target.AddComment(CommentInputField.Text);
                    ResetText();
                }
            }
        }

        /// <summary>
        /// Event handler for when the urgency combo box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Urgency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            target.ChangeUrgency(Urgency.SelectedIndex + 1);
        }

        /// <summary>
        /// Event handler that handles comment resizing when the stackpanel has shifted size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StackPanel panel = (StackPanel)sender;
            Grid parentGrid = (Grid)panel.Parent;

            Line line = parentGrid.Children.OfType<Line>().FirstOrDefault();

            if (line != null)
            {
                line.StrokeThickness = e.NewSize.Height;

                line.Y1 = e.NewSize.Height / 2;
                line.Y2 = e.NewSize.Height / 2;

                line.X1 = e.NewSize.Width - 150;
                line.X2 = line.X1 - e.NewSize.Width + 220;
            }
        }
    }
}
