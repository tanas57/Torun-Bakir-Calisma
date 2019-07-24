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
        public byte UpdateMode { get; set; }
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WeeklyPlan Plan { get; set; }
        private todoList todoList; // current request
        private List<plans> plans; // current plans
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
            plans = mainWindow.DB.PlanToCalendar(Plan.WorkID);

            editRequestNumber.Text = todoList.request_number;
            editRequestDescription.Text = todoList.description;
            editRequestPriority.SelectedIndex = (int)todoList.priority;

            for (int i = 0; i < plans.Count; i++)
            {
                plans temp = plans[i];
                list_plan.Items.Add(temp.work_plan_time.Value.ToShortDateString() + " - " + temp.id);
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
            bool error = false; result.Visibility = Visibility.Visible;
            if (editRequestNumber.Text == String.Empty)
            {
                error = true;
                result.Content = mainWindow.Lang.RequestAddReqNumEmpty;
                result.Background = System.Windows.Media.Brushes.Red;
            }
            else if(lbl_description.Text == String.Empty)
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
        }

        private void Plan_add_Click(object sender, RoutedEventArgs e)
        {
            EditPlanChooseDate editPlanChooseDate = new EditPlanChooseDate();
            editPlanChooseDate.Owner = this;
            editPlanChooseDate.mainWindow = mainWindow;
            this.Opacity = 0.5;
            if(editPlanChooseDate.ShowDialog() == false)
            {
                if(SelectedDates.Count > 0)
                {
                    foreach (var item in SelectedDates)
                    {
                        plans plan = new plans();
                        plan.add_time = DateTime.Now; plan.work_id = todoList.id;
                        plan.work_plan_time = item.Date;
                        mainWindow.DB.AddPlanDates(plan);
                    }
                    result.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddDates;
                    result.Background = System.Windows.Media.Brushes.Green;

                    list_plan.Items.Clear();
                    plans = mainWindow.DB.PlanToCalendar(Plan.WorkID);
                    for (int i = 0; i < plans.Count; i++)
                    {
                        plans temp = plans[i];
                        list_plan.Items.Add(temp.work_plan_time.Value.ToShortDateString() + " - " + temp.id);
                    }
                }
            }
        }

        private void Plan_remove_Click(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Visible;
            if(list_plan.SelectedIndex == -1)
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
                    plans plan = mainWindow.DB.GetPlanByID(plan_id);
                    mainWindow.DB.RemovePlan(plan);
                    result.Content = mainWindow.Lang.WeeklyEditPlanRemoved;
                    result.Background = System.Windows.Media.Brushes.Green;
                    list_plan.Items.RemoveAt(list_plan.SelectedIndex);
                    // related request do not have any plan, so we must transfer back in todolist
                    if(list_plan.Items.Count < 1)
                    {
                        result.Content = mainWindow.Lang.WeeklyEditPlanRemovePlanTransferTodoList;
                        todoList.status = (int)StatusType.Edited;
                        mainWindow.DB.EditTodoList(todoList);
                    }
                }
                catch (Exception) { }
            }
        }

        private void Plan_transfer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
