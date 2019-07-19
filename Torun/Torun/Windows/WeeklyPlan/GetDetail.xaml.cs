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
using System.Windows.Shapes;
using Torun.Database;
namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for GetDetail.xaml
    /// </summary>
    public partial class GetDetail : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WeeklyPlan Plan { get; set; }
        public GetDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = Plan.RequestNumber + " numaralı talep detayı";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }
    }
}
