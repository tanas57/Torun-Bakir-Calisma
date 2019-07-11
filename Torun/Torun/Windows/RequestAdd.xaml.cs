using System;
using System.Linq;
using System.Text;
using System.Windows;
using Torun.Database;
using Torun.Classes;
namespace Torun.Windows
{
    /// <summary>
    /// Interaction logic for RequestAdd.xaml
    /// </summary>
    public partial class RequestAdd : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public RequestAdd()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            req_Priority.Items.Add(mainWindow.language.ComboboxPriorityLow);
            req_Priority.Items.Add(mainWindow.language.ComboboxPriorityNormal);
            req_Priority.Items.Add(mainWindow.language.ComboboxPriorityHigh);

            lbl_reqNumber.Content = mainWindow.language.RequestAddRequestNumber;
            lbl_reqPriority.Content = mainWindow.language.RequestAddRequestPriority;
            lbl_reqDescription.Content = mainWindow.language.RequestAddRequestDescription;
            
        }

        private void Req_Save_Click(object sender, RoutedEventArgs e)
        {
            DB db = mainWindow.db;
            todoList todoList = new todoList();
            todoList.user_id = mainWindow.currentUser.id;
            todoList.status = (byte)StatusType.Added;
            todoList.priority = (byte)req_Priority.SelectedIndex;
            todoList.description = req_Description.Text;
            todoList.request_number = req_Number.Text;
            if(db.AddTodoList(todoList) == 0) MessageBox.Show("Eklenemedi");
            else MessageBox.Show("Eklendii");
        }
    }
}
