using System;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Torun.Classes;
using Torun.UControls;
using Torun.Database;
using Torun.Lang;

namespace Torun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Window welcome;
        public UCTodoList uCTodoList;
        public UCWeeklyPlan ucWeeklyPlan;
        public UCWorkDone uCWorkDone;
        public UCWeeklyPlanDetail weeklyPlanDetail;
        public UCSettings uCSettings;
        public UCReport uCReport;
        public UCBackup uCBackup;
        public Setting UserSettings { get; set; }
        private bool formLogoutControl = false; // for form closing control, and logout button action
        private int changeCount = 0; // weekly plan change view changes count
        //public users User { get; set; } daha sonra bu şekil değiştir
        public User User { get; set; }
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void GetSettings()
        {
            try
            {
                if(User != null)
                {
                    UserSettings = DB.GetUserSettings(User);
                    Settings.MainRequestCountType = (CountType)UserSettings.set_countType;
                    Settings.DefaultReportInterval = (CountType)UserSettings.set_defaultReportInterval;
                    Settings.DefaultReportListType = (ReportType)UserSettings.set_defaultReportType;
                    Settings.AutoOpen = UserSettings.set_autoOpen;
                    Settings.AutoBackup = UserSettings.set_autoBackup;
                    Settings.BackupTimeInterval = (CountType)UserSettings.set_backupTimeInterval;
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "mainwindow_GetSettings", error_text = ex.Message, log_user = User.id });
                }
        }
        public void UpdateSettings()
        {
            try
            {
                UserSettings.set_countType = (byte)Settings.MainRequestCountType;
                UserSettings.set_defaultReportInterval = (byte)Settings.DefaultReportInterval;
                UserSettings.set_defaultReportType = (byte)Settings.DefaultReportListType;
                UserSettings.set_autoOpen = Settings.AutoOpen;
                UserSettings.set_autoBackup = Settings.AutoBackup;
                UserSettings.set_backupTimeInterval = (byte)Settings.BackupTimeInterval;
                DB.UpdateUserSettings(User, UserSettings);
            }catch(Exception ex)
            {
                DB.AddLog(new Log { error_page = "mainwindow_UpdateSettings", error_text = ex.Message, log_user = User.id});
                }
        }
        public void ChangeViewWeeklyPlan()
        {
            changeCount++;
            if(changeCount % 2 == 1)
            {
                if (weeklyPlanDetail == null) UserControllCall.Add(Grd_Content, weeklyPlanDetail = new UCWeeklyPlanDetail());
                else UserControllCall.Add(Grd_Content, weeklyPlanDetail);
            }
            else
            {
                if (ucWeeklyPlan == null) UserControllCall.Add(Grd_Content, ucWeeklyPlan = new UCWeeklyPlan());
                else UserControllCall.Add(Grd_Content, ucWeeklyPlan);
            }
        }
        public void UpdateScreens()
        {
            try
            {
                requestCount.Content = DB.GetRequestCount(1, User, Settings.MainRequestCountType); // load count of all request
                requestOpen.Content = DB.GetRequestCount(2, User, Settings.MainRequestCountType);  // load count of currently open requests
                requestClosed.Content = DB.GetRequestCount(3, User, Settings.MainRequestCountType);// load count of closed request until today
                if (uCTodoList != null) uCTodoList.Grid_todoList.ItemsSource = DB.GetTodoLists(User);
                if (ucWeeklyPlan != null)
                {
                    ucWeeklyPlan.Date_picker_CalendarClosed(null, null);
                }
                if (uCWorkDone != null)
                {
                    uCWorkDone.Date_picker_CalendarClosed(null, null);
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "mainwindow_UpdateScreens", error_text = ex.Message, log_user = User.id });
                }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            DB.LogOut(User);
            this.Close();
            welcome.Close();
        }

        private void Brd_top_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Brd_topLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void BtnFormDown_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Btn_toDo_Click(object sender, RoutedEventArgs e)
        {
            if (uCTodoList == null) UserControllCall.Add(Grd_Content, uCTodoList = new UCTodoList());
            else UserControllCall.Add(Grd_Content, uCTodoList);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // set maximum screen size
                this.MaxWidth = SystemParameters.PrimaryScreenWidth;
                this.MaxHeight = SystemParameters.PrimaryScreenHeight;
                MenuVersion.Content = "Ver : " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " @Muslu.NET" ;
                User = DB.GetUserDetail(User);
                GetSettings();

                requestCount.Content = DB.GetRequestCount(1, User, Settings.MainRequestCountType); // load count of all request
                requestOpen.Content = DB.GetRequestCount(2, User, Settings.MainRequestCountType);  // load count of currently open requests
                requestClosed.Content = DB.GetRequestCount(3, User, Settings.MainRequestCountType);// load count of closed request until today
                menuUsername.Content = User.firstname + " " + User.lastname;

                if (FileOperation.isProfileExists())
                {
                    ellipsePhoto.ImageSource = GetImage(FileOperation.ProfilePhotoPath());
                }
                mainPage_title.Content = Lang.MainPageTitle;
                mainPage_totalRequest.Content = Lang.MainPageTotalRequest;
                mainPage_openRequest.Content = Lang.MainPageOpenRequest;
                mainPage_closedRequest.Content = Lang.MainPageClosedRequest;
                mainPage_logOut.Content = Lang.MainPageLogOut;
                mainPage_menuTodo.Content = Lang.MainPageMenuToDo;
                mainPage_menuWeeklyPlan.Content = Lang.MainPageMenuWeeklyPlan;
                mainPage_menuWordDone.Content = Lang.MainPageWorkDone;
                mainPage_menuReport.Content = Lang.MainPageMenuReport;
                mainPage_menuBackup.Content = Lang.MainPageMenuBackup;
                mainPage_menuSettings.Content = Lang.MainPageMenuSettings;

            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "mainwindow_Window_Loaded", error_text = ex.Message, log_user = User.id });
            }
        }
        private void BtnFormUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                if (uCTodoList != null)
                {
                    if (!uCTodoList.buttonDetail) uCTodoList.Grid_todoList.Columns[4].Width = SystemParameters.PrimaryScreenWidth - 600;
                    else uCTodoList.Grid_todoList.Columns[4].Width = SystemParameters.PrimaryScreenWidth - 810;
                }
            }
            else
            {
                this.WindowState = WindowState.Normal;
                if (uCTodoList != null)
                {
                    if (!uCTodoList.buttonDetail) uCTodoList.Grid_todoList.Columns[4].Width = 420;
                    else uCTodoList.Grid_todoList.Columns[4].Width = 230;
                }
            }
        }
        private void Menu_userLogout_Click(object sender, RoutedEventArgs e)
        {
            DB.LogOut(User);
            formLogoutControl = true;
            this.Close();
            this.User = null;
            welcome.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!formLogoutControl) welcome.Close(); // when user logout, do not close the main form
        }
        private void Btn_WeeklyPlan_Click(object sender, RoutedEventArgs e)
        {
            if (ucWeeklyPlan == null) UserControllCall.Add(Grd_Content, ucWeeklyPlan = new UCWeeklyPlan());
            else UserControllCall.Add(Grd_Content, ucWeeklyPlan);
        }
        private void PhotoChange_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                FileOperation.ChangeUserPhoto();
                ellipsePhoto.ImageSource = GetImage(FileOperation.ProfilePhotoPath());
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "mainwindow_PhotoChange_MouseDown", error_text = ex.Message, log_user = User.id });
                }
        }
        private BitmapImage GetImage(string imageUri)
        {
            try
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(imageUri, UriKind.RelativeOrAbsolute);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bitmapImage.EndInit();
                return bitmapImage;
            }
            catch(Exception ex)
            {
                DB.AddLog(new Log { error_page = "mainwindow_GetImage", error_text = ex.Message, log_user = User.id });
                return null;
            }
        }

        private void BtnWorkDone_Click(object sender, RoutedEventArgs e)
        {
            if (uCWorkDone == null) UserControllCall.Add(Grd_Content, uCWorkDone = new UCWorkDone());
            else UserControllCall.Add(Grd_Content, uCWorkDone);
        }

        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            if (uCSettings == null) UserControllCall.Add(Grd_Content, uCSettings = new UCSettings());
            else UserControllCall.Add(Grd_Content, uCSettings);
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            if (uCReport == null) UserControllCall.Add(Grd_Content, uCReport = new UCReport());
            else UserControllCall.Add(Grd_Content, uCReport);
        }

        private void BtnBackup_Click(object sender, RoutedEventArgs e)
        {
            if (uCBackup == null) UserControllCall.Add(Grd_Content, uCBackup = new UCBackup());
            else UserControllCall.Add(Grd_Content, uCBackup);
        }

        private void MenuVersion_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://muslu.net");
        }
    }
}
