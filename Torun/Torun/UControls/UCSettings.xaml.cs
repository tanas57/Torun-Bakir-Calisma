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
        public Setting UserSettings { get; set; }
        public UCSettings()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            UserSettings = mainWindow.UserSettings;
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

            switch ((CountType)UserSettings.set_countType)
            {
                case CountType.Daily: radioDaily.IsChecked = true;break;
                case CountType.Weekly: radioWeekly.IsChecked = true; break;
                case CountType.Montly: radioMonthly.IsChecked = true; break;
                case CountType.Yearly: radioYearly.IsChecked = true; break;
                case CountType.FromTheBeginning : radioBeforeStart.IsChecked = true; break;
            }
            switch ((CountType)UserSettings.set_defaultReportInterval)
            {
                case CountType.Daily: radioDailyReport.IsChecked = true; break;
                case CountType.Weekly: radioWeeklyReport.IsChecked = true; break;
                case CountType.Montly: radioMonthlyReport.IsChecked = true; break;
                case CountType.Yearly: radioYearlyReport.IsChecked = true; break;
                case CountType.FromTheBeginning: radioBeforeStartReport.IsChecked = true; break;
            }
            switch ((ReportType)UserSettings.set_defaultReportType)
            {
                case ReportType.OnlyPlan: reportOnlyPlan.IsChecked = true; break;
                case ReportType.OnlyWorkDone: reportOnlyWorkdone.IsChecked = true; break;
                case ReportType.Both: reportBoth.IsChecked = true; break;
            }
        }

        private void RadioDaily_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Daily;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
        }

        private void RadioWeekly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Weekly;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
        }

        private void RadioMonthly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Montly;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
        }

        private void RadioBeforeStart_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.FromTheBeginning;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
        }

        private void RadioYearly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Yearly;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
        }

        private void RadioDailyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Daily;
            mainWindow.UpdateSettings();
        }

        private void RadioWeeklyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Weekly;
            mainWindow.UpdateSettings();
        }

        private void RadioMonthlyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Montly;
            mainWindow.UpdateSettings();
        }

        private void RadioYearlyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Yearly;
            mainWindow.UpdateSettings();
        }

        private void RadioBeforeStartReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.FromTheBeginning;
            mainWindow.UpdateSettings();
        }

        private void ReportOnlyPlan_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportListType = ReportType.OnlyPlan;
            mainWindow.UpdateSettings();
        }

        private void ReportOnlyWorkdone_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportListType = ReportType.OnlyWorkDone;
            mainWindow.UpdateSettings();
        }

        private void ReportBoth_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportListType = ReportType.Both;
            mainWindow.UpdateSettings();
        }
    }
}
