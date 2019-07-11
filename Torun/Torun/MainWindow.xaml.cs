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
        public DB db;
        public Window welcome;
        public users currentUser;
        public ILanguage language;
        private bool formLogoutControl = false; // for form closing control, and logout button action
        public MainWindow()
        {
            InitializeComponent();
            Personnel pr = new Personnel();
            db = new DB();
            pr.Name = "Tayyip";
            pr.Surname = "Muslu";
            
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
            UserControllCall.Add(Grd_Content, new UCTodoList());
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
        }

        private void DockPanel_ContextMenuClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {

        }

        private void BtnFormUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
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
    }
}
