using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Torun.Database;
using Torun.Classes;
using Torun.Lang;
using System.Windows.Media;

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
        public User User { get; set; }
        public UCSettings()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            UserSettings = mainWindow.UserSettings;
            User = mainWindow.User;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            titleMainCount.Content = Lang.SettingsGroupMainCount;
            titleProfile.Content = Lang.SettingsTitleProfile;
            titleReport.Content = Lang.SettingsTitleReport;
            titleAutoBackup.Content = Lang.SettingsTitleAutoBackup;
            titleAutoStart.Content = Lang.SettingsOpenSetting;

            groupMainCount.Header = Lang.SettingsGroupMainCount;
            radioDaily.Content = Lang.SettingsRadioDaily;
            radioWeekly.Content = Lang.SettingsRadioWeekly;
            radioMonthly.Content = Lang.SettingsRadioMonthly;
            radioYearly.Content = Lang.SettingsRadioYearly;
            radioBeforeStart.Content = Lang.SettingsRadioBeforeStart;

            groupReport.Header = Lang.SettingsTitleReport;
            radioDailyReport.Content = Lang.SettingsRadioDaily;
            radioWeeklyReport.Content = Lang.SettingsRadioWeekly;
            radioMonthlyReport.Content = Lang.SettingsRadioMonthly;
            radioYearlyReport.Content = Lang.SettingsRadioYearly;
            radioBeforeStartReport.Content = Lang.SettingsRadioBeforeStart;

            groupAutoBackup.Header = Lang.SettingsTitleAutoBackup;
            radioDailyBackup.Content = Lang.SettingsRadioDaily;
            radioWeeklyBackup.Content = Lang.SettingsRadioWeekly;
            radioMonthlyBackup.Content = Lang.SettingsRadioMonthly;
            radioYearlyBackup.Content = Lang.SettingsRadioYearly;

            reportOnlyPlan.Content = Lang.ReportComboTypeOnlyPlan;
            reportOnlyWorkdone.Content = Lang.ReportComboTypeOnlyWorkDone;
            reportBoth.Content = Lang.ReportComboTypeBothofThem;

            titleReportType.Content = Lang.SettingsTitleTimeInterval;
            groupReportType.Header = Lang.SettingsTitleTimeInterval;
            autoBackupCheck.Content = Lang.SettingsAutoBackupActive;
            autoOpen.Content = Lang.SettingsOpenSetting;

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

            if(UserSettings.set_autoBackup == true) autoBackupCheck.IsChecked = true;
            else backupRadios.IsEnabled = false;

            if (UserSettings.set_autoOpen == true) autoOpen.IsChecked = true;

            switch ((CountType)UserSettings.set_backupTimeInterval)
            {
                case CountType.Daily: radioDailyBackup.IsChecked = true; break;
                case CountType.Weekly: radioWeeklyBackup.IsChecked = true; break;
                case CountType.Montly: radioMonthlyBackup.IsChecked = true; break;
                case CountType.Yearly: radioYearlyBackup.IsChecked = true; break;
            }
            result.Visibility = Visibility.Hidden;

            lblFirstName.Content = Lang.WelcomeRegisterTitleFirstName;
            lblLastName.Content = Lang.WelcomeRegisterTitleLastName;
            lblNewPassword.Content = Lang.WelcomeRegisterTitlePassword;
            lblNewPassword2.Content = Lang.WelcomeRegisterTitlePasswordAgain;
            lblCurrentPassword.Content = Lang.SettingsCurrentPassword;

            txtFirstName.Text = User.firstname;
            txtLastName.Text = User.lastname;
            saveName.Content = Lang.SettingsNameUpdate;
            savePassword.Content = Lang.SettingsPasswordUpdate;
        }

        private void RadioDaily_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Daily;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsGroupMainCount + " " + Lang.SettingsRadioDaily + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioWeekly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Weekly;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsGroupMainCount + " " + Lang.SettingsRadioWeekly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioMonthly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Montly;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsGroupMainCount + " " + Lang.SettingsRadioMonthly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioBeforeStart_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.FromTheBeginning;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsGroupMainCount + " " + Lang.SettingsRadioBeforeStart + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioYearly_Checked(object sender, RoutedEventArgs e)
        {
            Settings.MainRequestCountType = CountType.Yearly;
            mainWindow.UpdateScreens();
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsGroupMainCount + " " + Lang.SettingsRadioYearly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioDailyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Daily;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleReport + " " + Lang.SettingsRadioDaily + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioWeeklyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Weekly;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleReport + " " + Lang.SettingsRadioWeekly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioMonthlyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Montly;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleReport + " " + Lang.SettingsRadioMonthly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioYearlyReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.Yearly;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleReport + " " + Lang.SettingsRadioYearly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioBeforeStartReport_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportInterval = CountType.FromTheBeginning;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleReport + " " + Lang.SettingsRadioBeforeStart + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void ReportOnlyPlan_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportListType = ReportType.OnlyPlan;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleTimeInterval + " " + Lang.ReportComboTypeOnlyPlan + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void ReportOnlyWorkdone_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportListType = ReportType.OnlyWorkDone;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleTimeInterval + " " + Lang.ReportComboTypeOnlyWorkDone + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void ReportBoth_Checked(object sender, RoutedEventArgs e)
        {
            Settings.DefaultReportListType = ReportType.Both;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleTimeInterval + " " + Lang.ReportComboTypeBothofThem + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioDailyBackup_Checked(object sender, RoutedEventArgs e)
        {
            Settings.BackupTimeInterval = CountType.Daily;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleAutoBackup + " " + Lang.SettingsRadioDaily + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioWeeklyBackup_Checked(object sender, RoutedEventArgs e)
        {
            Settings.BackupTimeInterval = CountType.Weekly;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleAutoBackup + " " + Lang.SettingsRadioWeekly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioMonthlyBackup_Checked(object sender, RoutedEventArgs e)
        {
            Settings.BackupTimeInterval = CountType.Montly;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleAutoBackup + " " + Lang.SettingsRadioMonthly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void RadioYearlyBackup_Checked(object sender, RoutedEventArgs e)
        {
            Settings.BackupTimeInterval = CountType.Yearly;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsTitleAutoBackup + " " + Lang.SettingsRadioYearly + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }


        private void AutoBackupCheck_Checked(object sender, RoutedEventArgs e)
        {
            backupRadios.IsEnabled = true;
            Settings.AutoBackup = true;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettignsAutoBackup + " " + Lang.SettingsActive + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;

        }

        private void AutoOpen_Checked(object sender, RoutedEventArgs e)
        {
            Settings.AutoOpen = true;
            mainWindow.UpdateSettings(); 
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsOpenSetting + " " + Lang.SettingsActive + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void AutoOpen_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.AutoOpen = false;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettingsOpenSetting + " " + Lang.SettingsPasive + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void AutoBackupCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            backupRadios.IsEnabled = false;
            Settings.AutoBackup = false;
            mainWindow.UpdateSettings();
            result.Visibility = Visibility.Visible;
            result.Content = Lang.SettignsAutoBackup + " " + Lang.SettingsPasive + " " + Lang.SettingsApplied;
            result.Background = Brushes.Green;
        }

        private void SaveName_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text.Length >= 3 && txtLastName.Text.Length >= 3)
            {
                User.firstname = txtFirstName.Text;
                User.lastname = txtLastName.Text;
                DB.UpdateUser(User);
                result.Visibility = Visibility.Visible;
                result.Content = Lang.SettingsResultNameUp;
                result.Background = Brushes.Green;
                mainWindow.menuUsername.Content = User.firstname + " " + User.lastname;
            }
            else
            {
                result.Visibility = Visibility.Visible;
                result.Content = Lang.SettingsNameMustGreaterThanThreeChars;
                result.Background = Brushes.Red;
            }
        }

        private void SavePassword_Click(object sender, RoutedEventArgs e)
        {
            if (txtCurrentPassword.Password.Length >= 3 && txtNewPassword.Password.Length >= 3 && txtNewPassword2.Password.Length >= 3)
            {
                if (txtNewPassword.Password.Equals(txtNewPassword2.Password))
                {
                    if (User.password.Equals(MD5Crypt.MD5Hash(txtCurrentPassword.Password)))
                    {
                        User.password = MD5Crypt.MD5Hash(txtNewPassword.Password);
                        DB.UpdateUser(User);
                        result.Visibility = Visibility.Visible;
                        result.Content = Lang.SettingsResultPasswordUp;
                        result.Background = Brushes.Green;
                    }
                    else
                    {
                        result.Visibility = Visibility.Visible;
                        result.Content = Lang.WelcomeLoginFailedWrongPassword;
                        result.Background = Brushes.Red;
                    }
                }
                else
                {
                    result.Visibility = Visibility.Visible;
                    result.Content = Lang.WelcomeSignPasswordsNotMatch;
                    result.Background = Brushes.Red;
                }
            }
            else
            {
                result.Visibility = Visibility.Visible;
                result.Content = Lang.SettingsPasswordMustGreaterThanThreeChars;
                result.Background = Brushes.Red;
            }
        }
    }
}
