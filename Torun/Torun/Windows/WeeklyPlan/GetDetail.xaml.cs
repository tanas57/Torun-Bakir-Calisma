using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Torun.Database;

namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for GetDetail.xaml
    /// </summary>
    public partial class GetDetail : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WeeklyPlan Plan { get; set; }
        public GetDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Title = mainWindow.Lang.WeeklyDetailTitle + " " + Plan.RequestNumber;
                weeklyPlan_title.Content = mainWindow.Lang.WeeklyDetailTitle + " " + Plan.RequestNumber;

                Plan plan = mainWindow.DB.GetPlanByID(Plan.PlanID); // selected plan
                TodoList todolist = mainWindow.DB.GetTodoByID(Plan.WorkID); // plan's work
                dbDescription.Text = todolist.description;
                getDetailDescription.Text = mainWindow.Lang.WeeklyDetailDescription;
                getDetailCalendar.Text = mainWindow.Lang.WeeklyDetailCalendar;
                getDetailCalendarOK.Text = mainWindow.Lang.WeeklyDetailCalendarOK;
                var work_plans = mainWindow.DB.PlanToCalendar(Plan.WorkID);
                for (int i = 0; i < work_plans.Count; i++)
                {
                    dbCalendar.SelectedDates.Add(work_plans[i].work_plan_time);
                }
                var workDone = mainWindow.DB.GetWorkdoneByID(Plan.WorkID);
                for (int i = 0; i < workDone.Count; i++)
                {
                    dbCalendarOK.SelectedDates.Add(workDone[i].workDoneTime.Value.Date);
                }
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "getdetail_Window_Loaded", error_text = ex.Message, log_user = mainWindow.User.id });
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }
    }
}
