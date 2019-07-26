using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityLow);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityNormal);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityHigh);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityUrgent);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityProject);

            lbl_reqNumber.Content = mainWindow.Lang.RequestAddRequestNumber;
            lbl_reqPriority.Content = mainWindow.Lang.RequestAddRequestPriority;
            lbl_reqDescription.Content = mainWindow.Lang.RequestAddRequestDescription;
            lbl_req_Button.Content = mainWindow.Lang.RequestAddRequestButton;
            req_RequestAdd.Content = mainWindow.Lang.RequestAddRequestTitle;
            this.Title = mainWindow.Lang.RequestAddRequestTitle;
            addWorkDone.Content = mainWindow.Lang.RequestAddToWorkDone;
        }

        private void Req_Save_Click(object sender, RoutedEventArgs e)
        {
            DB db = mainWindow.DB;
            todoList todoList = new todoList();
            todoList.user_id = mainWindow.User.id;
            if(addWorkDone.IsChecked == true) todoList.status = (byte)StatusType.Closed;
            else todoList.status = (byte)StatusType.Added;
            todoList.priority = (byte)req_Priority.SelectedIndex;
            todoList.description = req_Description.Text;

            todoList.request_number = req_Number.Text.ToUpper();
            req_Result.Visibility = Visibility.Visible;
            if(req_Number.Text.ToUpper() != String.Empty)
            {
                if (req_Priority.SelectedIndex == -1)
                {
                    req_Result.Content = mainWindow.Lang.RequestAddRequestResultNotSelected;
                    req_Result.Background = System.Windows.Media.Brushes.Red;
                }
                else if (req_Description.Text == String.Empty)
                {
                    req_Result.Content = mainWindow.Lang.RequestAddRequestResultNoDescription;
                    req_Result.Background = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    int work_id = db.AddTodoList(todoList);
                    if (work_id == 0)
                    {
                        req_Result.Content = mainWindow.Lang.RequestAddRequestResultNo;
                        req_Result.Background = System.Windows.Media.Brushes.Red;
                    }
                    else
                    {
                        req_Result.Content = mainWindow.Lang.RequestAddRequestResultOk;
                        req_Result.Background = System.Windows.Media.Brushes.Green;
                        if (addWorkDone.IsChecked == true)
                        {
                            plans plan = new plans();
                            plan.add_time = DateTime.Now; plan.work_plan_time = DateTime.Now.Date;
                            plan.status = 1; plan.work_id = work_id;
                            int plan_id = mainWindow.DB.AddPlanDates(plan);

                            WorkDone workDone = new WorkDone();
                            workDone.plan_id = plan_id;
                            workDone.workDoneTime = DateTime.Now.Date; workDone.add_time = DateTime.Now;
                            workDone.status = 2;
                            mainWindow.DB.MoveWorkToWorkDone(workDone);
                        }
                        mainWindow.UpdateScreens();
                        this.Close();
                    }
                }
            }
            else
            {
                req_Result.Content = mainWindow.Lang.RequestAddReqNumEmpty;
                req_Result.Background = System.Windows.Media.Brushes.Red;
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
