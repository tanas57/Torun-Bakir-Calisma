using System.Linq;
using System.Windows;
using System.Windows.Input;
using Torun.Database;
using Torun.Classes;
using System;

namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for RemovePlan.xaml
    /// </summary>
    public partial class RemovePlan : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WeeklyPlan Plan { get; set; }
        public RemovePlan()
        {
            InitializeComponent();
        }

        private void RemoveSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Plan yapılacaklar listesine taşınacak, onaylıyor musunuz ?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    TodoList todoList = mainWindow.DB.GetTodoByID(Plan.WorkID);
                    if (remove_aDay.IsChecked == true) // selected plan will remove from plan
                    {
                        // tek gün seçili ise planda aktif halen iş var ise yapılacak listesine atılmayacak !!! // planned
                        // eğer tek iş o olupta remove edilirse yapılacaklara gönderelim // düzenlendi olsun
                        Plan plan = mainWindow.DB.GetPlanByID(Plan.PlanID);
                        if (remove_allDays.IsEnabled == false)
                        {
                            // todolist has only one plan so work can be transfer back todolist
                            mainWindow.DB.RemovePlan(plan);
                            if ((mainWindow.DB.GetWorkdoneByID(todoList.id).Count > 0))
                            {
                                // there are any completed plans, so the work status must be in progress
                                todoList.status = (int)StatusType.Closed;
                            }
                            else
                            {
                                todoList.status = (int)StatusType.Edited;
                            }
                        }
                        else
                        {
                            // only delete selected plan, do not change work
                            todoList.status = (int)StatusType.Planned;
                            mainWindow.DB.RemovePlan(plan);
                        }
                        mainWindow.DB.EditTodoList(todoList);
                    }
                    else if (remove_allDays.IsChecked == true)
                    {
                        // selected work transfers to todolist
                        if ((mainWindow.DB.GetWorkdoneByID(todoList.id).Count > 0))
                        {
                            // there are any completed plans, so the work status must be in progress
                            todoList.status = (int)StatusType.InProcess;
                        }
                        else
                        {
                            todoList.status = (int)StatusType.Edited;
                        }
                        mainWindow.DB.EditTodoList(todoList);
                        // remove all plans that are continued in plans
                        var plans = mainWindow.DB.PlanToCalendar(Plan.WorkID, true);
                        foreach (var item in plans)
                        {
                            mainWindow.DB.RemovePlan(item);
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "removeplan_RemoveSave_Click", error_text = ex.Message, log_user = mainWindow.User.id });
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = mainWindow.Lang.WeeklyRemoveTitle;
            remove_title.Content = mainWindow.Lang.WeeklyRemoveTitle;
            remove_aDay.Content = mainWindow.Lang.WeeklyRemoveAday;
            remove_allDays.Content = mainWindow.Lang.WeeklyRemoveAllDays;
            removeSave.Content = mainWindow.Lang.WeeklyRemoveButtonRemove;
            int workDayCount = mainWindow.DB.PlanToCalendar(Plan.WorkID, true).Count;
            if (workDayCount == 1) remove_allDays.IsEnabled = false;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) RemoveSave_Click(sender, e);
        }
    }
}
