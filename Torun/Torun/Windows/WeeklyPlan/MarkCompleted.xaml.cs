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
    /// Interaction logic for MarkCompleted.xaml
    /// </summary>
    public partial class MarkCompleted : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WeeklyPlan Plan { get; set; }
        public MarkCompleted()
        {
            InitializeComponent();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = mainWindow.Lang.WeeklyCompletedTitle;
            completed_title.Content = mainWindow.Lang.WeeklyCompletedTitle;
            completed_aDay.Content = mainWindow.Lang.WeeklyCompletedCurrentDay;
            completed_allDays.Content = mainWindow.Lang.WeeklyCompletedAllWork;
            completed_save.Content = mainWindow.Lang.ButtonSave;
            completedNote.Content = mainWindow.Lang.WeeklyCompletedNote;
            int workDayCount = mainWindow.DB.PlanToCalendar(Plan.WorkID).Count;
            if (workDayCount == 1) completed_allDays.IsEnabled = false;
        }
        private void Completed_save_Click(object sender, RoutedEventArgs e)
        {
            if (completed_allDays.IsEnabled == false) // the work only one day
            {
                // move this plan to workdone
                plans plan = mainWindow.DB.GetPlanByID(Plan.PlanID);
                WorkDone workDone = new WorkDone();
                workDone.add_time = DateTime.Now; workDone.workDoneTime = DateTime.Now;
                workDone.plan_id = Plan.PlanID; workDone.description = DbcompletedNote.Text;
                workDone.status = 2; // end of the work

                todoList todolist = mainWindow.DB.GetTodoByID(Plan.WorkID);
                todolist.status = 3; // todolist item status have to mark 3: closed
                mainWindow.DB.MoveWorkToWorkDone(workDone);
                mainWindow.DB.EditTodoList(todolist);
                plan.status = 1;
                mainWindow.DB.EditPlan(plan);
            }
            else // work has many days
            {
                if (completed_aDay.IsChecked == true) // only one day completed
                {
                    plans plan = mainWindow.DB.GetPlanByID(Plan.PlanID);
                    WorkDone workDone = new WorkDone();
                    workDone.add_time = DateTime.Now; workDone.workDoneTime = DateTime.Now;
                    workDone.plan_id = Plan.PlanID; workDone.description = DbcompletedNote.Text;
                    workDone.status = 1; // end of the work
                    mainWindow.DB.MoveWorkToWorkDone(workDone);
                    plan.status = 1;
                    mainWindow.DB.EditPlan(plan);
                }
                else // all work completed
                {
                    // a work has a lot of plans
                    // the plans that have current work_id status must be 1
                    // at the same time the plan_id's transfers to workDone table
                    // and finally, todolist work_id status must be 3 : closed
                    var plans = mainWindow.DB.PlanToCalendar(Plan.WorkID);
                    WorkDone workDone;
                    for (int i = 0; i < plans.Count; i++)
                    {
                        plans plan = plans[i]; // for each plan
                        plan.status = 1;
                        mainWindow.DB.EditPlan(plan);

                        workDone = new WorkDone();
                        workDone.add_time = DateTime.Now; workDone.workDoneTime = DateTime.Now;
                        workDone.plan_id = plan.id; workDone.description = DbcompletedNote.Text;
                        workDone.status = 1; // end of the work
                        mainWindow.DB.MoveWorkToWorkDone(workDone);
                    }
                    if(plans.Count > 0)
                    {
                        todoList todolist = mainWindow.DB.GetTodoByID((int)plans[0].work_id); // in here exception error !
                        todolist.status = 3; // closed
                        mainWindow.DB.EditTodoList(todolist);
                    }
                }
            }
            this.Close();
        }

    }
}
