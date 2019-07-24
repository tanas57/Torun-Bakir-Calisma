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

namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for EditPlan.xaml
    /// </summary>
    public partial class EditPlan : Window
    {
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
    }
}
