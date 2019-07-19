using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Torun.Database;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCWeeklyPlan.xaml
    /// </summary>
    public partial class UCWeeklyPlan : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private DB db; private users currentUser;
        public UCWeeklyPlan()
        {
            InitializeComponent();
            db = mainWindow.db;
            currentUser = mainWindow.currentUser;
            Grid_todoList0.ItemsSource = db.ListWeeklyPlanDay(currentUser, new DateTime(2019,07,22)); txt_MondayCount.Text = Grid_todoList0.Items.Count.ToString() + " adet kayıt";
            Grid_todoList1.ItemsSource = db.ListWeeklyPlanDay(currentUser, new DateTime(2019, 07, 23));
            Grid_todoList2.ItemsSource = db.ListWeeklyPlanDay(currentUser, new DateTime(2019, 07, 24));
            Grid_todoList3.ItemsSource = db.ListWeeklyPlanDay(currentUser, new DateTime(2019, 07, 25));
            Grid_todoList4.ItemsSource = db.ListWeeklyPlanDay(currentUser, new DateTime(2019, 07, 26));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_dayMonday.Content = mainWindow.language.UCWeeklyPlanDaysMonday;
            lbl_dayTuesday.Content = mainWindow.language.UCWeeklyPlanDaysTuesday;
            lbl_dayWednesday.Content = mainWindow.language.UCWeeklyPlanDaysWednesday;
            lbl_dayThursday.Content = mainWindow.language.UCWeeklyPlanDaysThursday;
            lbl_dayFriday.Content = mainWindow.language.UCWeeklyPlanDaysFriday;
            lbl_currentTime.Content = mainWindow.language.UCWeeklyPlanCurrentTime;

            lbl_dates.Text = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).ToShortDateString() + " - " + DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday).ToShortDateString();
        }
    }
}
