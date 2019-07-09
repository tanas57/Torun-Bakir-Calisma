using System.Windows.Controls;
using Torun.Database;
namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCTodoList.xaml
    /// </summary>
    public partial class UCTodoList : UserControl
    {
        public UCTodoList()
        {
            InitializeComponent();
            DB db = new DB();
            //Grid_todoList.ItemsSource = db.GetTodoLists(MainWindow );
        }
    }
}
