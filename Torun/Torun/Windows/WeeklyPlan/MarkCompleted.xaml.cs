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
            mainWindow.UpdateScreens();
            mainWindow.ucWeeklyPlan.Date_picker_CalendarClosed(sender, null);
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
            completed_title.Content = mainWindow.language.WeeklyCompletedTitle;
            completed_aDay.Content = mainWindow.language.WeeklyCompletedCurrentDay;
            completed_allDays.Content = mainWindow.language.WeeklyCompletedAllWork;
            completed_save.Content = mainWindow.language.ButtonSave;
            completedNote.Content = mainWindow.language.WeeklyCompletedNote;
            int workDayCount = mainWindow.db.PlanToCalendar(Plan.WorkID).Count;
            if (workDayCount == 1) completed_allDays.IsEnabled = false;
        }

        private void Completed_save_Click(object sender, RoutedEventArgs e)
        {
            if (completed_allDays.IsEnabled == false) // the work only one day
            {
                // move this plan to workdone
                plans plan = mainWindow.db.GetPlanByID(Plan.PlanID);
                WorkDone workDone = new WorkDone();
                workDone.add_time = DateTime.Now; workDone.workDoneTime = DateTime.Now;
                workDone.plan_id = Plan.PlanID; workDone.description = DbcompletedNote.Text;
                workDone.status = 2; // end of the work

                todoList todolist = mainWindow.db.GetTodoByID(Plan.WorkID);
                todolist.status = 3; // todolist item status have to mark 3: closed
                mainWindow.db.MoveWorkToWorkDone(workDone);
                mainWindow.db.EditTodoList(todolist);
                plan.status = 1;
                mainWindow.db.EditPlan(plan);
            }
            else // work has many days
            {
                if (completed_aDay.IsChecked == true) // only one day completed
                {
                    plans plan = mainWindow.db.GetPlanByID(Plan.PlanID);
                    WorkDone workDone = new WorkDone();
                    workDone.add_time = DateTime.Now; workDone.workDoneTime = DateTime.Now;
                    workDone.plan_id = Plan.PlanID; workDone.description = DbcompletedNote.Text;
                    workDone.status = 1; // end of the work
                    mainWindow.db.MoveWorkToWorkDone(workDone);
                    plan.status = 1;
                    mainWindow.db.EditPlan(plan);
                }
                else // all work completed
                {
                    var plans = mainWindow.db.PlanToCalendar(Plan.PlanID);
                    for (int i = 0; i < plans.Count; i++)
                    {
                        plans plan = plans[i];
                        WorkDone workDone = new WorkDone();
                        workDone.add_time = DateTime.Now; workDone.workDoneTime = DateTime.Now;
                        workDone.plan_id = Plan.PlanID; workDone.description = DbcompletedNote.Text;
                        workDone.status = 1; // end of the work
                        mainWindow.db.MoveWorkToWorkDone(workDone);
                        plan.status = 1;
                        mainWindow.db.EditPlan(plan);
                    }
                    todoList todolist = mainWindow.db.GetTodoByID((int)plans[0].work_id);
                    todolist.status = 3; // closed
                    mainWindow.db.EditTodoList(todolist);
                }
            }
            this.Close();
        }

    }
}
