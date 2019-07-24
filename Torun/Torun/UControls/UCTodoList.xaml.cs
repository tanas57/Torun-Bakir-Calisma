using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Torun.Database;
using Torun.Windows;
using Torun.Windows.Request;
using System;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCTodoList.xaml
    /// </summary>
    public partial class UCTodoList : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private DB db; private users currentUser;
        public bool buttonDetail = false;
        public UCTodoList()
        {
            InitializeComponent();
            db = mainWindow.DB;
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
            ucTodolist_ListDetail.Content = mainWindow.Lang.UCTodoListAddedTime;
            ucTodolist_addRequest.Content = mainWindow.Lang.UCTodoListAddRequest;
            ucTodolist_Description.Header = mainWindow.Lang.UCTodoListDescription;
            ucTodolist_requestNumber.Header = mainWindow.Lang.UCTodoListRequestNumber;
            ucTodolist_Priority.Header = mainWindow.Lang.UCTodoListPriority;
            ucTodolist_Processes.Header = mainWindow.Lang.UCTodoListProcesses;
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

        private void Grid_detail_Click(object sender, RoutedEventArgs e)
        {
            // detay butonuna tıklandı ilk kez.
            if (!buttonDetail)
            {
                DataGridTextColumn add_time = new DataGridTextColumn();
                add_time.Header = mainWindow.Lang.UCTodoListAddedTime;
                add_time.MinWidth = 100;
                add_time.IsReadOnly = true;
                add_time.Binding = new System.Windows.Data.Binding("add_time");
                Grid_todoList.Columns.Add(add_time);

                DataGridTextColumn status = new DataGridTextColumn();
                status.Header = mainWindow.Lang.UCTodoListStatus;
                status.MinWidth = 30;
                status.IsReadOnly = true;
                status.Binding = new System.Windows.Data.Binding("StatusString");
                Grid_todoList.Columns.Add(status);

                Grid_todoList.Columns[4].Width = Grid_todoList.Columns[4].Width.Value - 205;
                buttonDetail = true;
            }
            else
            { // ikinci basışta eklenenleri gizle
                Grid_todoList.Columns.RemoveAt(Grid_todoList.Columns.Count - 1); // add_time column
                Grid_todoList.Columns.RemoveAt(Grid_todoList.Columns.Count - 1); // status column
                Grid_todoList.Columns[4].Width = Grid_todoList.Columns[4].Width.Value + 190;
                buttonDetail = false;
            }
        }

        private void DataGridRequestNumber_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            todoList todolist = Grid_todoList.SelectedItem as todoList;
            int result = 0;
            if (int.TryParse(todolist.request_number,out result))
            {
                try
                {
                    System.Diagnostics.Process.Start("chrome.exe","http://erpdestek/WorkOrder.do?woMode=viewWO&woID=" + result + "&fromListView=true");
                }catch (Exception)
                {
                    System.Diagnostics.Process.Start("http://erpdestek/WorkOrder.do?woMode=viewWO&woID=" + result + "&fromListView=true");
                }
                
            }
            
            
        }

        private void DataGridCell_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            todoList todolist = Grid_todoList.SelectedItem as todoList;
            MessageBox.Show(todolist.description, mainWindow.Lang.UCTodoListInfoMessage, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
