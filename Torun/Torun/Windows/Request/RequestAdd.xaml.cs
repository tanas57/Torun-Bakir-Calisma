using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Torun.Database;
using Torun.Classes;
using System.Drawing;
using System.Windows.Input;

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
            req_Priority.Items.Add(mainWindow.language.ComboboxPriorityUrgent);
            req_Priority.Items.Add(mainWindow.language.ComboboxPriorityProject);

            lbl_reqNumber.Content = mainWindow.language.RequestAddRequestNumber;
            lbl_reqPriority.Content = mainWindow.language.RequestAddRequestPriority;
            lbl_reqDescription.Content = mainWindow.language.RequestAddRequestDescription;
            lbl_req_Button.Content = mainWindow.language.RequestAddRequestButton;
            req_RequestAdd.Content = mainWindow.language.RequestAddRequestTitle;
            this.Title = mainWindow.language.RequestAddRequestTitle;
        }

        private void Req_Save_Click(object sender, RoutedEventArgs e)
        {
            DB db = mainWindow.db;
            todoList todoList = new todoList();
            todoList.user_id = mainWindow.currentUser.id;
            todoList.status = (byte)StatusType.Added;
            todoList.priority = (byte)req_Priority.SelectedIndex;
            todoList.description = req_Description.Text;

            todoList.request_number = req_Number.Text.ToUpper();
            req_Result.Visibility = Visibility.Visible;
            if (req_Priority.SelectedIndex == -1)
            {
                req_Result.Content = mainWindow.language.RequestAddRequestResultNotSelected;
                req_Result.Background = System.Windows.Media.Brushes.Red;
            }
            else if(req_Description.Text == String.Empty)
            {
                req_Result.Content = mainWindow.language.RequestAddRequestResultNoDescription;
                req_Result.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                if (db.AddTodoList(todoList) == 0)
                {
                    req_Result.Content = mainWindow.language.RequestAddRequestResultNo;
                    req_Result.Background = System.Windows.Media.Brushes.Red;
                }
                else {
                    req_Result.Content = mainWindow.language.RequestAddRequestResultOk;
                    req_Result.Background = System.Windows.Media.Brushes.Green;
                    mainWindow.UpdateScreens();
                    this.Close();
                }
            }
        }

        private void ReqBtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }
    }
}
