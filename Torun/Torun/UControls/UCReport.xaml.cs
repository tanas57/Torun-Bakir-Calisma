using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Torun.Database;
using Torun.Classes;
using Torun.Lang;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCReport.xaml
    /// </summary>
    public partial class UCReport : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private ILanguage Lang { get; set; }
        private DB DB { get; set; }
        private CountType CountType { get; set; }
        private User User { get; set; }
        public UCReport()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planWorkdoneSelect.Items.Clear();
            timeIntervalSelect.Items.Clear();

            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeOnlyPlan);
            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeOnlyWorkDone);
            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeBothofThem);

            timeIntervalSelect.Items.Add(Lang.SettingsRadioDaily);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioWeekly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioMonthly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioYearly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioBeforeStart);

            timeIntervalSelect.SelectedIndex = (int)Settings.DefaultReportInterval;
            planWorkdoneSelect.SelectedIndex = (int)Settings.DefaultReportInterval;

            lblType.Content = Lang.ReportLabelPlanWorkdone;
            lblTime.Content = Lang.ReportLabelTimeInterval;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            CountType = (CountType)planWorkdoneSelect.SelectedIndex;
            if (planWorkdoneSelect.SelectedIndex == (int)ReportType.OnlyPlan)
            {
                ReportGridPlan.Visibility = Visibility.Visible;
                grid_onlyPlan.ItemsSource = DB.GetPlansForReport(User, CountType);
            }
            else if (planWorkdoneSelect.SelectedIndex == (int)ReportType.OnlyWorkDone)
            {

            }
            else if (planWorkdoneSelect.SelectedIndex == (int)ReportType.Both)
            {

            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (mainWindow.WindowState == WindowState.Normal)
            {
                planbig1.Width = 285;
                planbig2.Width = 285;
            }
            else
            {
                planbig1.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
                planbig2.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
            }
        }
    }
}
