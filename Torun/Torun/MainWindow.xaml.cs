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

        private bool formLogoutControl = false; // for form closing control, and logout button action
        private int changeCount = 0; // weekly plan change view changes count
        //public users User { get; set; } daha sonra bu şekil değiştir
        public User User { get; set; }
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            GetSettings();
        }
        private void GetSettings()
        {
            Settings.MainRequestCountType = CountType.Daily;
            Settings.DefaultReportInterval = CountType.Daily;
            Settings.DefaultReportInterval = CountType.Weekly;
            Settings.DefaultReportListType = ReportType.OnlyPlan;
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
            requestCount.Content = DB.GetRequestCount(1, User, Settings.MainRequestCountType); // load count of all request
            requestOpen.Content = DB.GetRequestCount(2, User, Settings.MainRequestCountType);  // load count of currently open requests
            requestClosed.Content = DB.GetRequestCount(3, User, Settings.MainRequestCountType);// load count of closed request until today
            if(uCTodoList != null) uCTodoList.Grid_todoList.ItemsSource = DB.GetTodoLists(User);
            if (ucWeeklyPlan != null)
            {
                ucWeeklyPlan.Date_picker_CalendarClosed(null, null);
            }
            if (uCWorkDone != null)
            {
                uCWorkDone.Date_picker_CalendarClosed(null, null);
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
            // set maximum screen size
            this.MaxWidth = SystemParameters.PrimaryScreenWidth;
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;
            MenuVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            User = DB.GetUserDetail(User);
            requestCount.Content = DB.GetRequestCount(1, User, Settings.MainRequestCountType); // load count of all request
            requestOpen.Content = DB.GetRequestCount(2, User, Settings.MainRequestCountType);  // load count of currently open requests
            requestClosed.Content = DB.GetRequestCount(3, User, Settings.MainRequestCountType);// load count of closed request until today
            menuUsername.Content = User.firstname + " " + User.lastname;

            if (FileOperation.isProfileExists())
            {
                photoChange.Source = GetImage(FileOperation.ProfilePhotoPath());
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
        private void BtnFormUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                if (uCTodoList != null)
                {
                    if (!uCTodoList.buttonDetail) uCTodoList.Grid_todoList.Columns[4].Width = SystemParameters.PrimaryScreenWidth - 560;
                    else uCTodoList.Grid_todoList.Columns[4].Width = SystemParameters.PrimaryScreenWidth - 780;
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
            // hatalar var.
            FileOperation.ChangeUserPhoto();
            photoChange.Source = GetImage(FileOperation.ProfilePhotoPath());
        }
        private BitmapImage GetImage(string imageUri)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri( imageUri, UriKind.RelativeOrAbsolute);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmapImage.EndInit();
            return bitmapImage;
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
    }
}
