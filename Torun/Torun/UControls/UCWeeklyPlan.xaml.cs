using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private DateTime planStartDate;
        public UCWeeklyPlan()
        {
            InitializeComponent();
            db = mainWindow.db;
            currentUser = mainWindow.currentUser;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_dayMonday.Content = mainWindow.language.UCWeeklyPlanDaysMonday;
            lbl_dayTuesday.Content = mainWindow.language.UCWeeklyPlanDaysTuesday;
            lbl_dayWednesday.Content = mainWindow.language.UCWeeklyPlanDaysWednesday;
            lbl_dayThursday.Content = mainWindow.language.UCWeeklyPlanDaysThursday;
            lbl_dayFriday.Content = mainWindow.language.UCWeeklyPlanDaysFriday;
            date_picker.Text = mainWindow.language.UCWeeklyPlanCurrentTime;
            
            LabelDateUpdate(DateTime.Now.Date);
        }

        private void Date_picker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (date_picker.SelectedDate.HasValue)
            {
                DateTime value = date_picker.SelectedDate.Value;
                LabelDateUpdate(value);
            }
        }

        private void LabelDateUpdate(DateTime datetime)
        {
            planStartDate = datetime.AddDays(-(int)datetime.DayOfWeek + (int)DayOfWeek.Monday);
            lbl_dates.Text = planStartDate.ToShortDateString() + " - " + planStartDate.AddDays(4).ToShortDateString();
            Grid_todoList0.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate); txt_MondayCount.Text = Grid_todoList0.Items.Count.ToString() + " adet kayıt";
            Grid_todoList1.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate.AddDays(1));
            Grid_todoList2.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate.AddDays(2));
            Grid_todoList3.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate.AddDays(3));
            Grid_todoList4.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate.AddDays(4));
        }

        private void Date_picker_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Date_picker_CalendarClosed(sender, e);
        }

        private void Date_picker_MouseEnter(object sender, MouseEventArgs e)
        {
            date_picker.IsDropDownOpen = true;
        }
    }
}
