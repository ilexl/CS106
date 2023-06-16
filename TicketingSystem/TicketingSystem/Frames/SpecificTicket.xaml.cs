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
                string comment = comments[i];
                Grid newCommentGrid = new Grid();
                newCommentGrid.Margin = new Thickness(0, 0, 0, 20);
                newCommentGrid.Height = 100;
                newCommentGrid.ColumnDefinitions.Add(new ColumnDefinition());

                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(8, GridUnitType.Star);
                newCommentGrid.ColumnDefinitions.Add(columnDefinition);

                

                TextBlock commentInitials = new TextBlock();
                commentInitials.Text = comment.Substring(0, 2);
                commentInitials.FontSize = 50;
                commentInitials.Width = 100;
                //commentInitials.VerticalContentAlignment = VerticalAlignment.Center;
                //commentInitials.HorizontalContentAlignment = HorizontalAlignment.Center;
                Border roundedCorner = new Border();
                roundedCorner.Background = Brushes.White;
                roundedCorner.Width = 100;
                roundedCorner.Padding = new Thickness(17, 12, 0, 0);
                roundedCorner.CornerRadius = new CornerRadius(60);
                roundedCorner.Child = commentInitials;

                newCommentGrid.Children.Add(roundedCorner);

                Line line = new Line();
                line.VerticalAlignment = VerticalAlignment.Center;
                line.Margin = new Thickness(15, 15, 0, 0);
                line.StrokeEndLineCap = PenLineCap.Triangle;
                line.StrokeStartLineCap = PenLineCap.Round;
                line.StrokeThickness = 80;
                line.Stroke = Brushes.White;
                line.X2 = 20;
                line.Y2 = 20;
                line.X1 = 1020;
                line.Y1 = 20;

                newCommentGrid.Children.Add(line);
                line.SetValue(Grid.ColumnProperty, 1);

                StackPanel stackPanel = new StackPanel();
                stackPanel.SetValue(Grid.ColumnProperty, 1);

                TextBlock nameBlock = new TextBlock();
                nameBlock.Text = comment.Substring(0, 2);
                nameBlock.Margin = new Thickness(40, 10, 0, 0);
                nameBlock.FontSize = 40;
                nameBlock.FontWeight = FontWeights.SemiBold;
                nameBlock.FontFamily = new FontFamily("{DynamicResource Epilogue}");

                stackPanel.Children.Add(nameBlock);

                TextBlock commentBlock = new TextBlock();
                commentBlock.Text = comment.Substring(2);
                commentBlock.Margin = new Thickness(40, 0, 0, 0);
                commentBlock.FontSize = 25;
                commentBlock.FontWeight = FontWeights.Regular;
                commentBlock.FontFamily = new FontFamily("{DynamicResource Epilogue}");

                stackPanel.Children.Add(commentBlock);

                stackPanel.Height = commentBlock.Height + nameBlock.Height + 10;

                newCommentGrid.Children.Add(stackPanel);

                CommentField.Children.Add(newCommentGrid);
            }

            Grid startCommentGrid = new Grid();
            startCommentGrid.Margin = new Thickness(0, 0, 0, 20);
            startCommentGrid.Height = 100;
            startCommentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(8, GridUnitType.Star);
            startCommentGrid.ColumnDefinitions.Add(cd);
            TextBlock StartInitials = new TextBlock();
            StartInitials.Text = "START";
            StartInitials.FontSize = 30;
            StartInitials.Width = 100;
            Border Start = new Border();
            Start.Background = Brushes.White;
            Start.Width = 100;
            Start.Padding = new Thickness(10, 28, 0, 20);
            Start.CornerRadius = new CornerRadius(60);
            Start.Child = StartInitials;

            startCommentGrid.Children.Add(Start);
            CommentField.Children.Add(startCommentGrid);
        }

        private void ResetText()
        {
            CommentInputField.Text = "Add Comment Here";
            CommentInputField.Foreground = Brushes.Gray;
            CommentField.Children.Clear();
            CommentButton.Focus();
            ViewTicketDetails(target);
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            target.AddComment(CommentInputField.Text);
            ResetText();
        }

        private void CommentInputField_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Add Comment Here")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Set the desired text color
            }
        }

        private void CommentInputField_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Add Comment Here";
                textBox.Foreground = Brushes.Gray; // Set the desired ghost text color
            }
        }

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

        private void Urgency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            target.ChangeUrgency(Urgency.SelectedIndex + 1);
        }
    }
}
