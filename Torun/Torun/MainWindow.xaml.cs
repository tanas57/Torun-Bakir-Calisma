using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Torun.Classes;
using Torun.UControls;
using Torun.Database;
using Torun.Lang;
using System.Windows.Media.Imaging;
using System;
using System.Drawing;

namespace Torun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DB db;
        public Window welcome;
        public users currentUser;
        public ILanguage language;
        public UCTodoList uCTodoList;
        public UCWeeklyPlan ucWeeklyPlan;
        private bool formLogoutControl = false; // for form closing control, and logout button action
        //public users CurrentUser { get; set; } daha sonra bu şekil değiştir
        public MainWindow()
        {
            InitializeComponent();
        }
        public void UpdateScreens()
        {
            requestCount.Content = db.GetRequestCount(1, currentUser); // load count of all request
            requestOpen.Content = db.GetRequestCount(2, currentUser);  // load count of currently open requests
            requestClosed.Content = db.GetRequestCount(3, currentUser);// load count of closed request until today
            if(uCTodoList != null) uCTodoList.Grid_todoList.ItemsSource = db.GetTodoLists(currentUser);
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            db.LogOut(currentUser);
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
            currentUser = db.GetUserDetail(currentUser);
            requestCount.Content = db.GetRequestCount(1, currentUser); // load count of all request
            requestOpen.Content = db.GetRequestCount(2, currentUser);  // load count of currently open requests
            requestClosed.Content = db.GetRequestCount(3, currentUser);// load count of closed request until today
            menuUsername.Content = currentUser.firstname + " " + currentUser.lastname;

            if (FileOperation.isProfileExists())
            {
                photoChange.Source = GetImage(FileOperation.ProfilePhotoPath());
            }
            mainPage_title.Content = language.MainPageTitle;
            mainPage_totalRequest.Content = language.MainPageTotalRequest;
            mainPage_openRequest.Content = language.MainPageOpenRequest;
            mainPage_closedRequest.Content = language.MainPageClosedRequest;
            mainPage_logOut.Content = language.MainPageLogOut;
            mainPage_menuTodo.Content = language.MainPageMenuToDo;
            mainPage_menuWeeklyPlan.Content = language.MainPageMenuWeeklyPlan;
            mainPage_menuWordDone.Content = language.MainPageWorkDone;
            mainPage_menuReport.Content = language.MainPageMenuReport;
            mainPage_menuBackup.Content = language.MainPageMenuBackup;
        }
        private void BtnFormUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                if (uCTodoList != null)
                {
                    if (!uCTodoList.buttonDetail) uCTodoList.Grid_todoList.Columns[4].Width = SystemParameters.PrimaryScreenWidth - 568;
                    else uCTodoList.Grid_todoList.Columns[4].Width = SystemParameters.PrimaryScreenWidth - 758;
                }
            }
            else
            {
                this.WindowState = WindowState.Normal;
                if (uCTodoList != null)
                {
                    if (!uCTodoList.buttonDetail) uCTodoList.Grid_todoList.Columns[4].Width = 430;
                    else uCTodoList.Grid_todoList.Columns[4].Width = 242;
                }
            }
        }
        private void Menu_userLogout_Click(object sender, RoutedEventArgs e)
        {
            db.LogOut(currentUser);
            formLogoutControl = true;
            this.Close();
            this.currentUser = null;
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
            FileOperation.ChangeUserPhoto();
            photoChange.Source = GetImage(FileOperation.ProfilePhotoPath());
        }
        private BitmapImage GetImage(string imageUri)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri( imageUri, UriKind.RelativeOrAbsolute);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}
