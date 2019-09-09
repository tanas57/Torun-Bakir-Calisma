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
using Torun.Classes;
using Torun.Database;
using Torun.Lang;

namespace Torun.Windows.Report
{
    /// <summary>
    /// Interaction logic for ReportDateSelector.xaml
    /// </summary>
    public partial class ReportDateSelector : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private ILanguage Lang { get; set; }
        public ReportDateSelector()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonOK_Click(sender, null);
                BtnClose_Click(sender, null);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = Lang.ReportDateSelectorTitle;
            searchTitle.Content = Lang.ReportDateSelectorTitle;
            dateStartLBL.Content = Lang.ReportDateStart;
            dateEndLBL.Content = Lang.ReportDateStop;
            buttonOK.Content = Lang.ButtonGet;
            dateStart.Focus();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.uCReport.timeIntervalSelect.SelectedIndex = 1;
            this.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
            {
                MessageBox.Show(Lang.ReportDateSelectorDateMustBeSelect);
            }
            else
            {
                if (dateStart.SelectedDate.Value > dateEnd.SelectedDate.Value)
                {
                    MessageBox.Show(Lang.ReportDateSelectorStartBiggerThanEnd);
                    mainWindow.uCReport.timeIntervalSelect.SelectedIndex = 1;
                }
                else
                {
                    List<DateTime> temp = new List<DateTime>();
                    temp.Add(dateStart.SelectedDate.Value);
                    temp.Add(dateEnd.SelectedDate.Value.AddDays(1).AddSeconds(-1));
                    mainWindow.uCReport.timeIntervalSelect.SelectedIndex = (int)CountType.SelectDate;
                    mainWindow.uCReport.SearchDateTimes = temp;
                    this.Close();
                }
            }
        }
    }
}
