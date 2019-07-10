using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Torun.Database;
using Torun.Windows;
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
            requestAdd.ShowDialog();
        }
    }
}
