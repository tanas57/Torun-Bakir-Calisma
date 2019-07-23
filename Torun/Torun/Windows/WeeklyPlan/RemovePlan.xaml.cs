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
            todoList todoList = mainWindow.db.GetTodoByID(Plan.WorkID);
            plans plan = mainWindow.db.GetPlanByID(Plan.PlanID);
            if(remove_aDay.IsChecked == true)
            {
                // only one day
                if(remove_allDays.IsEnabled == false)
                {
                    // todolist has only one plan so todolist can be removed
                    mainWindow.db.RemovePlan(plan);
                    if(!(mainWindow.db.GetWorkdoneByID(todoList.id).Count > 0)) mainWindow.db.DeleteTodoList(todoList);
                }
                else
                {
                    mainWindow.db.RemovePlan(plan);
                }
            }
            else if(remove_allDaysExceptDoit.IsChecked == true)
            {
                // all days except workdone
            }
            else if(remove_allDays.IsChecked == true)
            {
                // all days && work done
            }
            this.Close();
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
            this.Title = mainWindow.language.WeeklyRemoveTitle;
            remove_title.Content = mainWindow.language.WeeklyRemoveTitle;
            remove_aDay.Content = mainWindow.language.WeeklyRemoveAday;
            remove_allDaysExceptDoit.Content = mainWindow.language.WeeklyRemoveAllDays;
            remove_allDays.Content = mainWindow.language.WeeklyRemoveAllDaysExceptDoit;
            removeSave.Content = mainWindow.language.WeeklyRemoveButtonRemove;
            int workDayCount = mainWindow.db.PlanToCalendar(Plan.WorkID).Count;
            if (workDayCount == 1)
            {
                remove_allDaysExceptDoit.IsEnabled = false;
                remove_allDays.IsEnabled = false;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }
    }
}
