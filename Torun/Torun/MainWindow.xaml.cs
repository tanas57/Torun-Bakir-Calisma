using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Torun.Classes;
using Torun.UControls;
using Torun.Database;
namespace Torun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DB db;
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
            this.Close();
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
            this.MaxWidth = SystemParameters.PrimaryScreenWidth;
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;
            MenuVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            requestCount.Content = db.getRequestCount(1); // load count of all request
            requestOpen.Content = db.getRequestCount(2);  // load count of currently open requests
            requestClosed.Content = db.getRequestCount(3);// load count of closed request until today
        }

        private void DockPanel_ContextMenuClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {

        }

        private void BtnFormUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
    }
}
