using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Torun.Database;
using Torun.Windows.WeeklyPlan;
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

            txt_GetDetail.Text = mainWindow.language.UCWeeklyPlanButtonGetDetail;
            txt_MarkCompleted.Text = mainWindow.language.UCWeeklyPlanButtonDoCompleted;
            txt_RemovePlan.Text = mainWindow.language.UCWeeklyPlanButtonRemove;
            txt_Edit.Text = mainWindow.language.UCWeeklyPlanButtonEdit;
            txt_ChangeDate.Text = mainWindow.language.UCWeeklyPlanButtonChangeDate;
            LabelandGridUpdate(DateTime.Now.Date);

            UserControl_SizeChanged(sender, null);
            LabelandGridUpdate(DateTime.Now.Date);
        }

        public void Date_picker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (date_picker.SelectedDate.HasValue)
            {
                DateTime value = date_picker.SelectedDate.Value;
                LabelandGridUpdate(value);
            }
            else LabelandGridUpdate(DateTime.Now.Date);
        }

        private void LabelandGridUpdate(DateTime datetime)
        {
            planStartDate = datetime.AddDays(-(int)datetime.DayOfWeek + (int)DayOfWeek.Monday);
            lbl_dates.Text = planStartDate.ToShortDateString() + " - " + planStartDate.AddDays(4).ToShortDateString();
            Grid_todoList0.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate); txt_Count0.Text = Grid_todoList0.Items.Count.ToString() + " " + mainWindow.language.UCWeeklyPlanNumOfPlans;
            Grid_todoList1.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate.AddDays(1)); txt_Count1.Text = Grid_todoList1.Items.Count.ToString() + " " + mainWindow.language.UCWeeklyPlanNumOfPlans;
            Grid_todoList2.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate.AddDays(2)); txt_Count2.Text = Grid_todoList2.Items.Count.ToString() + " " + mainWindow.language.UCWeeklyPlanNumOfPlans;
            Grid_todoList3.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate.AddDays(3)); txt_Count3.Text = Grid_todoList3.Items.Count.ToString() + " " + mainWindow.language.UCWeeklyPlanNumOfPlans;
            Grid_todoList4.ItemsSource = db.ListWeeklyPlanDay(currentUser, planStartDate.AddDays(4)); txt_Count4.Text = Grid_todoList4.Items.Count.ToString() + " " + mainWindow.language.UCWeeklyPlanNumOfPlans;
        }

        private void Date_picker_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Date_picker_CalendarClosed(sender, e);
        }

        private void Date_picker_MouseEnter(object sender, MouseEventArgs e)
        {
            date_picker.IsDropDownOpen = true;
        }
        private DataGrid GridControl()
        {
            if (Grid_todoList0.SelectedItem != null) return Grid_todoList0;
            else if (Grid_todoList1.SelectedItem != null) return Grid_todoList1;
            else if (Grid_todoList2.SelectedItem != null) return Grid_todoList2;
            else if (Grid_todoList3.SelectedItem != null) return Grid_todoList3;
            else if (Grid_todoList4.SelectedItem != null) return Grid_todoList4;
            else return null;
        }

        private void Btn_GetDetail_Click(object sender, RoutedEventArgs e)
        {
            DataGrid selectedGrid = GridControl();
            if (selectedGrid != null)
            {
                GetDetail getDetail = new GetDetail();
                getDetail.Owner = mainWindow;
                getDetail.Plan = selectedGrid.SelectedItem as DB.WeeklyPlan;
                mainWindow.Opacity = 0.5;
                getDetail.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                getDetail.ShowDialog();
            }
        }

        private void Btn_doComplated_Click(object sender, RoutedEventArgs e)
        {
            DataGrid selectedGrid = GridControl();
            if (selectedGrid != null)
            {
                MarkCompleted markCompleted = new MarkCompleted();
                markCompleted.Owner = mainWindow;
                markCompleted.Plan = selectedGrid.SelectedItem as DB.WeeklyPlan;
                mainWindow.Opacity = 0.5;
                markCompleted.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (markCompleted.ShowDialog() == false)
                {
                    Date_picker_CalendarClosed(sender, e);
                    mainWindow.UpdateScreens();
                }
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int gridWidth = 155; // if form size changed, size must be increase.
            if (mainWindow.WindowState == WindowState.Normal) gridWidth = 155;
            else gridWidth += ((int)SystemParameters.WorkArea.Width - 1000) / 5;
            gridCell0.Width = gridWidth; gridCell1.Width = gridWidth; gridCell2.Width = gridWidth;
            gridCell3.Width = gridWidth; gridCell4.Width = gridWidth;
        }
    }
}
