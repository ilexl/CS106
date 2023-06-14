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

namespace TicketingSystem.Frames
{
    /// <summary>
    /// Interaction logic for ViewTickets.xaml
    /// </summary>
    public partial class ViewTickets : Page
    {
        public ViewTickets()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            window.ChangeWindow("SpecificTicket.xaml");
        }
    }
}
