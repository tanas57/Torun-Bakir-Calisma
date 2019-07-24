using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Torun.Classes;
using Torun.Database;
using Torun.UControls;
namespace Torun.Windows.Request
{
    /// <summary>
    /// Interaction logic for RequestDetail.xaml
    /// </summary>
    public partial class RequestDetail : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public todoList todolist;
        public RequestDetail()
        {
            InitializeComponent();
        }
        private void RequestDetail_Load(object sender, RoutedEventArgs e)
        {
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityLow);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityNormal);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityHigh);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityUrgent);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityProject);

            req_Number.Text = todolist.request_number;
            req_Description.Text = todolist.description;
            req_Priority.SelectedIndex = (byte)todolist.priority;

            lbl_reqNumber.Content = mainWindow.Lang.RequestAddRequestNumber;
            lbl_reqPriority.Content = mainWindow.Lang.RequestAddRequestPriority;
            lbl_reqDescription.Content = mainWindow.Lang.RequestAddRequestDescription;
            lbl_req_Button.Content = mainWindow.Lang.RequestEditButton;
            req_RequestAdd.Content = mainWindow.Lang.RequestEditTitle;
            this.Title = mainWindow.Lang.RequestEditTitle;
        }

        private void ReqBtnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Opacity = 1;
            this.Close();
        }

        private void Req_Save_Click(object sender, RoutedEventArgs e)
        {
            DB db = mainWindow.DB;
            todolist.status = (byte)StatusType.Edited;
            todolist.priority = (byte)req_Priority.SelectedIndex;
            todolist.description = req_Description.Text;

            todolist.request_number = req_Number.Text.ToUpper();
            req_Result.Visibility = Visibility.Visible;
            if (req_Priority.SelectedIndex == -1)
            {
                req_Result.Content = mainWindow.Lang.RequestAddRequestResultNotSelected;
                req_Result.Background = Brushes.Red;
            }
            else if (req_Description.Text == String.Empty)
            {
                req_Result.Content = mainWindow.Lang.RequestAddRequestResultNoDescription;
                req_Result.Background = Brushes.Red;
            }
            else
            {
                if (db.EditTodoList(todolist) == 0)
                {
                    req_Result.Content = mainWindow.Lang.RequestEditLabelSaveNO;
                    req_Result.Background = Brushes.Red;
                }
                else
                {
                    req_Result.Content = mainWindow.Lang.RequestEditLabelSaveOK;
                    req_Result.Background = Brushes.Green;
                    mainWindow.UpdateScreens();
                    this.Close();
                }
            }
        }

        private void Req_DetailTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Req_DetailTitle_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }
    }
}
