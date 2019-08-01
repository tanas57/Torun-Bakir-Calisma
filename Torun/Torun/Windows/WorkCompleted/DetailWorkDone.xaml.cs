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
using Torun.Windows.WeeklyPlan;
using Torun.Classes;
using Torun.Lang;
namespace Torun.Windows.WorkCompleted
{
    /// <summary>
    /// Interaction logic for DetailWorkDone.xaml
    /// </summary>
    public partial class DetailWorkDone : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WorkDoneList Work { get; set; }
        private ILanguage Lang { get; set; }
        private DB DB { get; set; }
        private TodoList todoList;
        public DetailWorkDone()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = Lang.WorkDoneDetailTitle;
            workDoneDetail_title.Content = Lang.WorkDoneDetailTitle;
            lbl_reqDescription.Content = Lang.RequestAddRequestDescription;
            lbl_reqPriority.Content = Lang.RequestAddRequestPriority;
            lbl_reqNumber.Content = Lang.RequestAddRequestNumber;

            groupPlan.Header = Lang.WorkDoneDetailGroupPlanAndWorkDone;
            groupRequest.Header = Lang.RequestAddRequestNumber;
            getDetailCalendar.Text = Lang.WeeklyDetailCalendar;
            getDetailCalendarOK.Text = Lang.WeeklyDetailCalendarOK;
            getDetailDescription.Text = Lang.WorkDoneDetailDescription;

            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityLow);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityNormal);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityHigh);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityUrgent);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityProject);

            todoList = DB.GetTodoByID(Work.WorkID);
            req_Number.Text = todoList.request_number;
            req_Priority.SelectedIndex = todoList.priority;
            req_Description.Text = todoList.description;

            var work_plans = mainWindow.DB.PlanToCalendar(Work.WorkID);
            int order = 1;
            for (int i = 0; i < work_plans.Count; i++)
            {
                dbCalendar.SelectedDates.Add(work_plans[i].work_plan_time);
            }
            var workDone = mainWindow.DB.GetWorkdoneByID(Work.WorkID);
            for (int i = 0; i < workDone.Count; i++)
            {
                dbCalendarOK.SelectedDates.Add(workDone[i].workDoneTime.Value.Date);
                if (!workDone[i].description.Equals(String.Empty))
                {
                    dbDescription.Text += (order) + " - " + workDone[i].description + "\n";
                    order++;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Lbl_reqDescription_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(todoList.description, mainWindow.Lang.UCTodoListInfoMessage, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
