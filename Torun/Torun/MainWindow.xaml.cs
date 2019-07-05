using System.Windows;
using System.Windows.Input;
using Torun.Classes;
using Torun.UControls;

namespace Torun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Personnel pr = new Personnel();
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
    }
}
