using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Torun.Database;
using Torun.Classes;
using Torun.Lang;
using System.Windows.Media;

namespace Torun.Windows.WorkCompleted
{
    /// <summary>
    /// Interaction logic for EditWorkDone.xaml
    /// </summary>
    public partial class EditWorkDone : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private TodoList todoList; // current request
        private List<WorkDone> works; // current plans
        public DateTime SelectedDate { get; set; }
        public DB.WorkDoneList Work { get; set; }
        private ILanguage Lang { get; set; }
        private DB DB { get; set; }
        public EditWorkDone()
        {
            InitializeComponent();
            DB = mainWindow.DB;
            Lang = mainWindow.Lang;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = Lang.WorkDoneEditTitle;
            workDoneDetail_title.Content = Lang.WorkDoneEditTitle;
            lbl_reqDescription.Content = Lang.RequestAddRequestDescription;
            lbl_reqPriority.Content = Lang.RequestAddRequestPriority;
            lbl_reqNumber.Content = Lang.RequestAddRequestNumber;

            groupPlan.Header = Lang.WorkDoneDetailGroupPlanAndWorkDone;
            groupRequest.Header = Lang.RequestAddRequestNumber;
            getDetailDescription.Text = Lang.WorkDoneEditChoosenWorkDescription;

            workDone_remove.Content = Lang.ButtonRemove;
            workDone_transfer.Content = Lang.ButtonTransfer;
            request_save.Content = Lang.WorkDoneEditRequestSaveButton;

            getDetailCalendarOK.Text = Lang.WorkDoneEditCompletedWorks;
            listWorkdoneLbl.Text = Lang.WorkDoneEditWorkLabel;
            workDoneDescriptionSave.Content = Lang.WorkDoneEditChoosenWorkDescription;

            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityLow);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityNormal);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityHigh);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityUrgent);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityProject);

            todoList = DB.GetTodoByID(Work.WorkID);
            req_Number.Text = todoList.request_number;
            req_Priority.SelectedIndex = todoList.priority;
            req_Description.Text = todoList.description;

            WorkDoneListUpdate();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.UpdateScreens();
            Close();
        }

        private void WorkDone_remove_Click(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Visible;
            if (list_workdone.SelectedIndex >= 0)
            {
                try
                {
                    string[] arr = list_workdone.SelectedValue.ToString().Split('-');
                    int work_id = int.Parse(arr[1].Trim());
                    if (arr.Length > 2)
                    {
                        result.Content = mainWindow.Lang.WeeklyEditPlanRemoveWorkdoneError;
                        result.Background = Brushes.Red;
                    }
                    else
                    {
                        WorkDone work = DB.GetWorkDoneByID(work_id);
                        Plan plan = work.Plan;
                        TodoList todoList = plan.TodoList;
                        if (DB.GetWorkdoneByID(work_id).Count > 1)
                        {
                            todoList.status = (int)StatusType.InProcess;
                        }
                        else
                        {
                            todoList.status = (int)StatusType.Planned;
                        }
                        plan.status = (int)StatusType.Deleted;
                        mainWindow.DB.RemoveWorkdone(work);
                        mainWindow.DB.EditPlan(plan);
                        result.Content = mainWindow.Lang.WeeklyEditPlanRemoved;
                        result.Background = Brushes.Green;
                    }
                }
                catch (Exception) { }
                WorkDoneListUpdate();
            }
        }
        private void WorkDoneListUpdate()
        {
            list_workdone.Items.Clear();
            works = mainWindow.DB.GetWorkdoneByID(Work.WorkID);
            for (int i = 0; i < works.Count; i++)
            {
                WorkDone temp = works[i];
                list_workdone.Items.Add(temp.workDoneTime.ToShortDateString() + " - " + temp.id);
            }
        }
        private void WorkDone_transfer_Click(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Visible;
            if (list_workdone.SelectedIndex >= 0)
            {
                try
                {
                    string[] arr = list_workdone.SelectedValue.ToString().Split('-');
                    int plan_id = int.Parse(arr[1].Trim());
                    if (arr.Length > 2)
                    {
                        result.Content = mainWindow.Lang.WeeklyEditPlanRemoveWorkdoneError;
                        result.Background = Brushes.Red;
                    }
                    else
                    {
                        EditWorkDoneCalendar editWorkDone = new EditWorkDoneCalendar();
                        editWorkDone.Owner = this;
                        this.Opacity = 0.5;
                        if (editWorkDone.ShowDialog() == false)
                        {
                            if (SelectedDate != null)
                            {
                                if (!DB.IsWorkDoneExists(SelectedDate.Date, todoList.id)) // if there is no selected date in database
                                {
                                    WorkDone work = DB.GetWorkDoneByID(plan_id);
                                    work.workDoneTime = SelectedDate.Date;
                                    DB.EditWorkDone(work);
                                }

                                result.Content = mainWindow.Lang.WeeklyEditPlanTransfered;
                                result.Background = Brushes.Green;

                                WorkDoneListUpdate();
                                mainWindow.UpdateScreens();
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
            else
            {
                result.Background = Brushes.Red;
                result.Content = Lang.WorkDoneEditWorkNotSelected;
            }
        }

        private void WorkDoneDescriptionSave_Click(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Visible;
            if (list_workdone.SelectedIndex >= 0)
            {
                try
                {
                    string[] arr = list_workdone.SelectedValue.ToString().Split('-');
                    int work_id = int.Parse(arr[1].Trim());
                    if (arr.Length > 2)
                    {
                        result.Content = mainWindow.Lang.WeeklyEditPlanRemoveWorkdoneError;
                        result.Background = Brushes.Red;
                    }
                    else
                    {
                        WorkDone work = DB.GetWorkDoneByID(work_id);
                        work.description = dbDescription.Text;
                        DB.EditWorkDone(work);
                        result.Content = mainWindow.Lang.WorkDoneEditWorkDescriptionUpdate;
                        result.Background = Brushes.Green;
                    }
                }
                catch (Exception) { }
            }
        }
        private void Lbl_reqDescription_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(todoList.description, mainWindow.Lang.UCTodoListInfoMessage, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Request_save_Click(object sender, RoutedEventArgs e)
        {
            todoList.priority = (byte)req_Priority.SelectedIndex;
            todoList.description = req_Description.Text;

            todoList.request_number = req_Number.Text.ToUpper();
            result.Visibility = Visibility.Visible;
            if (req_Priority.SelectedIndex == -1)
            {
                result.Content = mainWindow.Lang.RequestAddRequestResultNotSelected;
                result.Background = Brushes.Red;
            }
            else if (req_Description.Text == String.Empty)
            {
                result.Content = mainWindow.Lang.RequestAddRequestResultNoDescription;
                result.Background = Brushes.Red;
            }
            else if (req_Number.Text.Length < 1)
            {
                result.Content = mainWindow.Lang.RequestAddReqNumEmpty;
                result.Background = Brushes.Red;
            }
            else
            {
                if (DB.EditTodoList(todoList) == 0)
                {
                    result.Content = mainWindow.Lang.RequestEditLabelSaveNO;
                    result.Background = Brushes.Red;
                }
                else
                {
                    result.Content = mainWindow.Lang.RequestEditLabelSaveOK;
                    result.Background = Brushes.Green;
                    mainWindow.UpdateScreens();
                }
            }
        }

        private void List_workdone_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (list_workdone.SelectedIndex >= 0)
            {
                try
                {
                    string[] arr = list_workdone.SelectedValue.ToString().Split('-');
                    string work_date = arr[0].Trim();

                    getDetailDescription.Text = work_date + " " + Lang.WorkDoneEditFor + " " + Lang.WorkDoneEditChoosenWorkDescription;
                }
                catch (Exception) { }
            }
        }
    }
}