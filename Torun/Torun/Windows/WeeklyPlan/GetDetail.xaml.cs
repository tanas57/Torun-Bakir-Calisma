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
            this.Title = mainWindow.language.WeeklyDetailTitle + " " + Plan.RequestNumber;
            weeklyPlan_title.Content = mainWindow.language.WeeklyDetailTitle + " " + Plan.RequestNumber;

            plans plan = mainWindow.db.GetPlanByID(Plan.PlanID); // selected plan
            todoList todolist = mainWindow.db.GetTodoByID(Plan.WorkID); // plan's work
            dbDescription.Text = todolist.description;
            getDetailDescription.Text = mainWindow.language.WeeklyDetailDescription;
            getDetailCalendar.Text = mainWindow.language.WeeklyDetailCalendar;
            var work_plans = mainWindow.db.PlanToCalendar(Plan.WorkID);
            for (int i = 0; i < work_plans.Count; i++)
            {
                dbCalendar.SelectedDates.Add(work_plans[i].work_plan_time.Value);
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

        private void DbDescription_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
