using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Torun.Database;
using Torun.Classes;
namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for EditPlan.xaml
    /// </summary>
    public partial class EditPlan : Window
    {
        public List<DateTime> SelectedDates { get; set; }
        public bool DateControl { get; set; }
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WeeklyPlan Plan { get; set; }
        private TodoList todoList; // current request
        private List<Plan> plans; // current plans
        public EditPlan()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                editRequestPriority.Items.Add(mainWindow.Lang.ComboboxPriorityLow);
                editRequestPriority.Items.Add(mainWindow.Lang.ComboboxPriorityNormal);
                editRequestPriority.Items.Add(mainWindow.Lang.ComboboxPriorityHigh);
                editRequestPriority.Items.Add(mainWindow.Lang.ComboboxPriorityUrgent);
                editRequestPriority.Items.Add(mainWindow.Lang.ComboboxPriorityProject);

                lbl_reqNum.Text = mainWindow.Lang.RequestAddRequestNumber;
                lbl_reqPriority.Text = mainWindow.Lang.RequestAddRequestPriority;
                lbl_description.Text = mainWindow.Lang.RequestAddRequestDescription;
                ReqInfo.Text = mainWindow.Lang.WeeklyEditPlanRequestInfo;
                planInfo.Text = mainWindow.Lang.WeeklyEditPlanInfo;
                plan_add.Content = mainWindow.Lang.ButtonAdd;
                plan_remove.Content = mainWindow.Lang.ButtonRemove;
                plan_transfer.Content = mainWindow.Lang.ButtonTransfer;
                this.Title = mainWindow.Lang.WeeklyEditPlanTitle;
                editPlanTitle.Content = mainWindow.Lang.WeeklyEditPlanTitle;
                savechanges.Content = mainWindow.Lang.ButtonSave;

                todoList = mainWindow.DB.GetTodoByID(Plan.WorkID);

                editRequestNumber.Text = todoList.request_number;
                editRequestDescription.Text = todoList.description;
                editRequestPriority.SelectedIndex = (int)todoList.priority;

                PlanListUpdate();
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "editplan_Window_Loaded", error_text = ex.Message, log_user = mainWindow.User.id });
                }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Savechanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool error = false; result.Visibility = Visibility.Visible;
                if (editRequestNumber.Text == String.Empty)
                {
                    error = true;
                    result.Content = mainWindow.Lang.RequestAddReqNumEmpty;
                    result.Background = System.Windows.Media.Brushes.Red;
                }
                else if (lbl_description.Text == String.Empty)
                {
                    error = true;
                    result.Content = mainWindow.Lang.RequestAddRequestResultNoDescription;
                    result.Background = System.Windows.Media.Brushes.Red;
                }

                if (!error)
                {
                    todoList.request_number = editRequestNumber.Text.ToUpper();
                    todoList.description = editRequestDescription.Text;
                    todoList.priority = (byte)editRequestPriority.SelectedIndex;
                    mainWindow.DB.EditTodoList(todoList);
                    // succsess info
                    result.Content = mainWindow.Lang.RequestEditLabelSaveOK;
                    result.Background = System.Windows.Media.Brushes.Green;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "editplan_Savechanges_Click", error_text = ex.Message, log_user = mainWindow.User.id });
                }
        }

        private void Plan_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditPlanChooseDate editPlanChooseDate = new EditPlanChooseDate();
                editPlanChooseDate.Owner = this;
                editPlanChooseDate.mainWindow = mainWindow;
                this.Opacity = 0.5;
                if (editPlanChooseDate.ShowDialog() == false)
                {
                    if (SelectedDates != null && SelectedDates.Count > 0)
                    {
                        foreach (var item in SelectedDates)
                        {
                            if (!mainWindow.DB.IsPlanExists(item.Date, todoList.id)) // for the choosen date is found in database, must not add again
                            {
                                Plan plan = new Plan();
                                plan.add_time = DateTime.Now; plan.work_id = todoList.id;
                                plan.work_plan_time = item.Date; plan.status = 0;
                                mainWindow.DB.AddPlanDates(plan);
                            }
                        }
                        result.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddDates;
                        result.Background = System.Windows.Media.Brushes.Green;

                        PlanListUpdate();
                    }
                }
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "editplan_Plan_add_Click", error_text = ex.Message, log_user = mainWindow.User.id });
            }
        }

        private void Plan_remove_Click(object sender, RoutedEventArgs e)
        {
            var result2 = MessageBox.Show("Seçili tarihteki iş yapılmadı olarak işaretlenecek, onaylıyor musunuz ?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result2 == MessageBoxResult.Yes)
            {
                result.Visibility = Visibility.Visible;
                if (list_plan.SelectedIndex == -1)
                {
                    result.Content = mainWindow.Lang.WeeklyEditPlanNotSelectPlan;
                    result.Background = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    try
                    {
                        string[] arr = list_plan.SelectedValue.ToString().Split('-');
                        int plan_id = int.Parse(arr[1].Trim());
                        if (arr.Length > 2)
                        {
                            result.Content = mainWindow.Lang.WeeklyEditPlanRemoveWorkdoneError;
                            result.Background = System.Windows.Media.Brushes.Red;
                        }
                        else
                        {
                            Plan plan = mainWindow.DB.GetPlanByID(plan_id);
                            mainWindow.DB.RemovePlan(plan);
                            result.Content = mainWindow.Lang.WeeklyEditPlanRemoved;
                            result.Background = System.Windows.Media.Brushes.Green;
                            list_plan.Items.RemoveAt(list_plan.SelectedIndex);
                            // related request do not have any plan, so we must transfer back in todolist
                            if (list_plan.Items.Count < 1)
                            {
                                result.Content = mainWindow.Lang.WeeklyEditPlanRemovePlanTransferTodoList;
                                todoList.status = (int)StatusType.Edited;
                                mainWindow.DB.EditTodoList(todoList);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mainWindow.DB.AddLog(new Log { error_page = "editplan_Plan_remove_Click", error_text = ex.Message, log_user = mainWindow.User.id });
                    }
                }
            }
        }

        private void Plan_transfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                result.Visibility = Visibility.Visible;
                if (list_plan.SelectedIndex == -1)
                {
                    result.Content = mainWindow.Lang.WeeklyEditPlanNotSelectPlan;
                    result.Background = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    EditPlanTransferBtn editPlanTransferBtn = new EditPlanTransferBtn();
                    editPlanTransferBtn.Owner = this;
                    editPlanTransferBtn.mainWindow = mainWindow;
                    this.Opacity = 0.5;
                    if (editPlanTransferBtn.ShowDialog() == false)
                    {
                        if (SelectedDates != null && SelectedDates.Count > 0)
                        {
                            foreach (var item in SelectedDates)
                            {
                                if (!mainWindow.DB.IsPlanExists(item.Date, todoList.id)) // for the choosen date is found in database, must not add again
                                {
                                    string[] arr = list_plan.SelectedValue.ToString().Split('-');
                                    int plan_id = int.Parse(arr[1].Trim());
                                    if (arr.Length > 2)
                                    {
                                        result.Content = mainWindow.Lang.WeeklyEditPlanTransferError;
                                        result.Background = System.Windows.Media.Brushes.Red;
                                    }
                                    else
                                    {
                                        Plan plan = mainWindow.DB.GetPlanByID(plan_id);
                                        plan.work_plan_time = item.Date;
                                        mainWindow.DB.EditPlan(plan);
                                        result.Content = mainWindow.Lang.WeeklyEditPlanTransfered;
                                        result.Background = System.Windows.Media.Brushes.Green;
                                        list_plan.Items.RemoveAt(list_plan.SelectedIndex);
                                    }
                                }
                            }
                            result.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddDates;
                            result.Background = System.Windows.Media.Brushes.Green;
                            PlanListUpdate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "editplan_Plan_transfer_Click", error_text = ex.Message, log_user = mainWindow.User.id });
            }
        }
        private void PlanListUpdate()
        {
            try
            {
                list_plan.Items.Clear();
                plans = mainWindow.DB.PlanToCalendar(Plan.WorkID);
                for (int i = 0; i < plans.Count; i++)
                {
                    Plan temp = plans[i];
                    if (temp.status == 0) list_plan.Items.Add(temp.work_plan_time.ToShortDateString() + " - " + temp.id);
                    else if (temp.status == 1) list_plan.Items.Add(temp.work_plan_time.ToShortDateString() + " - " + temp.id + " - OK");
                }
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "editplan_PlanListUpdate", error_text = ex.Message, log_user = mainWindow.User.id });
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Savechanges_Click(sender, e);
        }
    }
}
