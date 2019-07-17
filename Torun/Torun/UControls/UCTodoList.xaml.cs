using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Torun.Database;
using Torun.Windows;
using Torun.Windows.Request;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCTodoList.xaml
    /// </summary>
    public partial class UCTodoList : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private DB db; private users currentUser;
        public UCTodoList()
        {
            InitializeComponent();
            db = mainWindow.db;
            currentUser = mainWindow.currentUser;
            Grid_todoList.ItemsSource = db.GetTodoLists(currentUser);
        }
        private void btn_adddRequest_Click(object sender, RoutedEventArgs e)
        {
            RequestAdd requestAdd = new RequestAdd();
            requestAdd.Owner = mainWindow;
            mainWindow.Opacity = 0.5;
            requestAdd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            requestAdd.ShowDialog();
        }

        private void Btn_requestDelete_Click(object sender, RoutedEventArgs e)
        {
            todoList todoList = Grid_todoList.SelectedItem as todoList;
            var result = MessageBox.Show(todoList.id + " id numaralı talep silinecek, onaylıyor musunuz ?", "Uyarı", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                db.DeleteTodoList(todoList);
                mainWindow.UpdateScreens();
            }
        }

        private void Grid_todoList_Loaded(object sender, RoutedEventArgs e)
        {
            // priority ve status int to string işlemi

        }

        private void Grid_todoList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // get detail of selected row
        }

        private void Btn_requestEdit_Click(object sender, RoutedEventArgs e)
        {
            RequestDetail requestEdit = new RequestDetail();
            requestEdit.Owner = mainWindow;
            requestEdit.todolist = Grid_todoList.SelectedItem as todoList;
            mainWindow.Opacity = 0.5;
            requestEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            requestEdit.ShowDialog();
        }

        private void Btn_requestSchedule_Click(object sender, RoutedEventArgs e)
        {
            RequestSchedule requestSchedule = new RequestSchedule();
            requestSchedule.Owner = mainWindow;
            requestSchedule.todolist = Grid_todoList.SelectedItem as todoList;
            mainWindow.Opacity = 0.5;
            requestSchedule.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            requestSchedule.ShowDialog();
        }
    }
}
