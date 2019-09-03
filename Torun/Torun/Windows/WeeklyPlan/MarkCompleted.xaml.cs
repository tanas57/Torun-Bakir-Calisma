using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
        private bool screenLoad = false; // to fix chooseDate was null error.
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
            int workDayCount = mainWindow.DB.PlanToCalendar(Plan.WorkID, true).Count;
            if (workDayCount == 1) completed_allDays.IsEnabled = false;
            screenLoad = true;
        }
        private void Completed_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // initial values
                DateTime dateTime = new DateTime(1, 1, 1);
                bool flag = false;
                Plan plan = mainWindow.DB.GetPlanByID(Plan.PlanID);
                TodoList todoList = mainWindow.DB.GetTodoByID(plan.work_id);
                string requestDescription = todoList.description;

                if (todayCompleted.IsChecked == true)
                {
                    dateTime = plan.work_plan_time;
                    flag = true;
                }
                else
                {
                    if (chooseDate.SelectedDate != null)
                    {
                        dateTime = chooseDate.SelectedDate.Value;
                        flag = true;
                    }
                    else
                    {
                        result.Visibility = Visibility.Visible;
                        result.Content = mainWindow.Lang.RequestScheduleChooseDate;
                    }
                }
                if (flag)
                {
                    if (completed_allDays.IsEnabled == false) // the work only one day
                    {
                        // move this plan to workdone
                        WorkDone workDone = new WorkDone();
                        workDone.add_time = DateTime.Now; workDone.workDoneTime = dateTime;
                        workDone.plan_id = Plan.PlanID;

                        if (DbcompletedNote.Text != String.Empty) workDone.description = DbcompletedNote.Text;
                        else workDone.description = requestDescription;

                        workDone.status = 2; // end of the work

                        TodoList todolist = mainWindow.DB.GetTodoByID(Plan.WorkID);
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
                            WorkDone workDone = new WorkDone();
                            workDone.add_time = DateTime.Now; workDone.workDoneTime = dateTime;
                            workDone.plan_id = Plan.PlanID;

                            if (DbcompletedNote.Text != String.Empty) workDone.description = DbcompletedNote.Text;
                            else workDone.description = requestDescription;

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
                            var plans = mainWindow.DB.PlanToCalendar(Plan.WorkID, true);
                            WorkDone workDone;
                            for (int i = 0; i < plans.Count; i++)
                            {
                                plan = plans[i]; // for each plan
                                plan.status = 1;
                                mainWindow.DB.EditPlan(plan);

                                workDone = new WorkDone();
                                workDone.add_time = DateTime.Now; workDone.workDoneTime = dateTime;
                                workDone.plan_id = plan.id;

                                if (DbcompletedNote.Text != String.Empty) workDone.description = DbcompletedNote.Text;
                                else workDone.description = requestDescription;

                                workDone.status = 1; // end of the work
                                mainWindow.DB.MoveWorkToWorkDone(workDone);
                            }
                            if (plans.Count > 0)
                            {
                                TodoList todolist = mainWindow.DB.GetTodoByID((int)plans[0].work_id); // in here exception error !
                                todolist.status = 3; // closed
                                mainWindow.DB.EditTodoList(todolist);
                            }
                        }
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "markcompleted_Completed_save_Click", error_text = ex.Message, log_user = mainWindow.User.id });
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Completed_save_Click(sender, e);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(screenLoad) chooseDate.IsEnabled = false;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (screenLoad) chooseDate.IsEnabled = true;
        }
    }
}
