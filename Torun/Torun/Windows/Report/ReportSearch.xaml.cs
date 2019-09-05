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

namespace Torun.Windows.Report
{
    /// <summary>
    /// Interaction logic for ReportSearch.xaml
    /// </summary>
    public partial class ReportSearch : Window
    {
        public ReportSearch()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = Lang.ReportSearchTitle;
            searchTitle.Content = Lang.ReportSearchTitle;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
