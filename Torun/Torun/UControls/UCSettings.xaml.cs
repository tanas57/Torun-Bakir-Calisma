
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Torun.Database;
using Torun.Classes;
using Torun.Lang;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCSettings.xaml
    /// </summary>
    public partial class UCSettings : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public UCSettings()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            titleMainCount.Content = Lang.SettingsGroupMainCount;
            titleProfile.Content = Lang.SettingsTitleProfile;
            titleReport.Content = Lang.SettingsTitleReport;

            groupMainCount.Header = Lang.SettingsGroupMainCount;
            radioDaily.Content = Lang.SettingsRadioDaily;
            radioWeekly.Content = Lang.SettingsRadioWeekly;
            radioMonthly.Content = Lang.SettingsRadioMonthly;
            radioYearly.Content = Lang.SettingsRadioYearly;
            radioBeforeStart.Content = Lang.SettingsRadioBeforeStart;

            groupReport.Header = Lang.SettingsGroupMainCount;
            radioDailyReport.Content = Lang.SettingsRadioDaily;
            radioWeeklyReport.Content = Lang.SettingsRadioWeekly;
            radioMonthlyReport.Content = Lang.SettingsRadioMonthly;
            radioYearlyReport.Content = Lang.SettingsRadioYearly;
            radioBeforeStartReport.Content = Lang.SettingsRadioBeforeStart;

            reportOnlyPlan.Content = Lang.ReportComboTypeOnlyPlan;
            reportOnlyWorkdone.Content = Lang.ReportComboTypeOnlyWorkDone;
            reportBoth.Content = Lang.ReportComboTypeBothofThem;

            titleReportType.Content = Lang.SettingsTitleTimeInterval;
            groupReportType.Header = Lang.SettingsTitleTimeInterval;
        }

        private void RadioDaily_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Daily;
            mainWindow.UpdateScreens();
        }

        private void RadioWeekly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Weekly;
            mainWindow.UpdateScreens();
        }

        private void RadioMonthly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Montly;
            mainWindow.UpdateScreens();
        }

        private void RadioBeforeStart_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.FromTheBeginning;
            mainWindow.UpdateScreens();
        }

        private void RadioYearly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Yearly;
            mainWindow.UpdateScreens();
        }

        private void RadioDailyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Daily;
            mainWindow.UpdateScreens();
        }

        private void RadioWeeklyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Weekly;
            mainWindow.UpdateScreens();
        }

        private void RadioMonthlyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Montly;
            mainWindow.UpdateScreens();
        }

        private void RadioYearlyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Yearly;
            mainWindow.UpdateScreens();
        }

        private void RadioBeforeStartReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.FromTheBeginning;
            mainWindow.UpdateScreens();
        }
    }
}
