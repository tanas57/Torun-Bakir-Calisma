using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Torun.Classes;
using Torun.Lang;
using Torun.Database;
using System.Windows.Media;
using System.Threading;

namespace Torun.Windows.CheckList
{
    /// <summary>
    /// Interaction logic for CheckListReport.xaml
    /// </summary>
    public partial class CheckListReport : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public User User { get; set; }
        public CheckListReport()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            User = mainWindow.UCCheckList.Relation;
            DB = mainWindow.DB;
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

            timeInterval.Items.Add(Lang.SettingsRadioDaily);
            timeInterval.Items.Add(Lang.SettingsRadioWeekly);
            timeInterval.Items.Add(Lang.SettingsRadioMonthly);
            timeInterval.Items.Add(Lang.SettingsRadioYearly);
            timeInterval.Items.Add(Lang.SettingsRadioBeforeStart);
            timeInterval.Items.Add(Lang.CountTypeSelectDate);

            timeInterval.SelectedIndex = 1;

            lblTimeInterval.Content = Lang.ReportLabelTimeInterval;
            Height = 150;
            hidden1.Visibility = Visibility.Hidden;
            hidden2.Visibility = Visibility.Hidden;
            buttonOK.Margin = new Thickness(0, -120, 0, 0);
            result.Margin = new Thickness(0, -40, 0, 2);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private List<DateTime> tempDate = new List<DateTime>();
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(Report));
            
            if (timeInterval.SelectedIndex == (int)CountType.SelectDate)
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
                    }
                    else
                    {
                        tempDate.Add(dateStart.SelectedDate.Value);
                        tempDate.Add(dateEnd.SelectedDate.Value.AddDays(1).AddSeconds(-1));
                        timeInterval.SelectedIndex = (int)CountType.SelectDate;

                        result.Content = Lang.ReportProcessStart;
                        result.Background = Brushes.Blue;
                        thread.Start();
                    }
                }
            }
            else
            {
                result.Content = Lang.ReportProcessStart;
                result.Background = Brushes.Blue;
                thread.Start();
            }
        }
        private void Report()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (tempDate.Count > 0)
                {
                    FileOperation.CheckListExportEXCEL(User, (CountType)timeInterval.SelectedIndex, DB, tempDate);
                    tempDate.Clear();
                }
                else FileOperation.CheckListExportEXCEL(User, (CountType)timeInterval.SelectedIndex, DB);
                result.Content = Lang.ReportProcessEnd;
                result.Background = Brushes.Green;
            }));
        }
        private void TimeInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(timeInterval.SelectedIndex == (int)CountType.SelectDate)
            {
                Height = 233;
                hidden1.Visibility = Visibility.Visible;
                hidden2.Visibility = Visibility.Visible;
                buttonOK.Margin = new Thickness(0, 5, 0, 0);
                result.Margin = new Thickness(0, 3, 0, -5);
            }
            else
            {
                Height = 150;
                hidden1.Visibility = Visibility.Hidden;
                hidden2.Visibility = Visibility.Hidden;
                buttonOK.Margin = new Thickness(0, -120, 0, 0);
                result.Margin = new Thickness(0, -40, 0, 2);
            }
        }
    }
}
