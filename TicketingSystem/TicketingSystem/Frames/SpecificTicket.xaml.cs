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
        }
    }
}
