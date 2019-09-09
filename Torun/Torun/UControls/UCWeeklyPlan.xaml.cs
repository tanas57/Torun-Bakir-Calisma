using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Torun.Database;
using Torun.Windows.WeeklyPlan;
using Torun.Classes;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCWeeklyPlan.xaml
    /// </summary>
    public partial class UCWeeklyPlan : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
       
        private DateTime planStartDate;
        private DataGrid SelectedGrid { get; set; }
        private OrderBy Order { get; set; }
        private User User { get; set; }
        private DB DB { get; set; }
        public UCWeeklyPlan()
        {
            InitializeComponent();
            Order = OrderBy.AddedTimeDesc;
            User = mainWindow.User;
            DB = mainWindow.DB;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lbl_dayMonday.Content = mainWindow.Lang.UCWeeklyPlanDaysMonday;
                lbl_dayTuesday.Content = mainWindow.Lang.UCWeeklyPlanDaysTuesday;
                lbl_dayWednesday.Content = mainWindow.Lang.UCWeeklyPlanDaysWednesday;
                lbl_dayThursday.Content = mainWindow.Lang.UCWeeklyPlanDaysThursday;
                lbl_dayFriday.Content = mainWindow.Lang.UCWeeklyPlanDaysFriday;
                date_picker.Text = mainWindow.Lang.UCWeeklyPlanCurrentTime;

                txt_GetDetail.Text = mainWindow.Lang.UCWeeklyPlanButtonGetDetail;
                txt_MarkCompleted.Text = mainWindow.Lang.UCWeeklyPlanButtonDoCompleted;
                txt_RemovePlan.Text = mainWindow.Lang.UCWeeklyPlanButtonRemove;
                txt_Edit.Text = mainWindow.Lang.UCWeeklyPlanButtonEdit;

                UserControl_SizeChanged(sender, null);
                LabelandGridUpdate(DateTime.Now.Date);

                sort_lbl.Content = mainWindow.Lang.WeeklyPlanSortLbl;
                sort_AddTime.Content = mainWindow.Lang.WeeklyPlanSortAddTime;
                sort_AddTimeDesc.Content = mainWindow.Lang.WeeklyPlanSortAddTimeDesc;
                sort_Priority.Content = mainWindow.Lang.WeeklyPlanSortPriorityAsc;
                sort_PriorityDesc.Content = mainWindow.Lang.WeeklyPlanSortPriorityDesc;
                sort_NameDesc.Content = mainWindow.Lang.WeeklyPlanSortNameDesc;
                sort_NameAsc.Content = mainWindow.Lang.WeeklyPlanSortNameAsc;
                txt_closeDetail.Text = mainWindow.Lang.UCWeeklyPlanOpenDetail;
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplan", error_text = ex.Message, log_user = User.id });
            }
        }

        public void Date_picker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (date_picker.SelectedDate.HasValue)
                {
                    DateTime value = date_picker.SelectedDate.Value;
                    LabelandGridUpdate(value);
                }
                else LabelandGridUpdate(DateTime.Now.Date);
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplan", error_text = ex.Message, log_user = User.id });
            }
        }

        private void LabelandGridUpdate(DateTime datetime)
        {
            try
            {
                planStartDate = datetime.AddDays(-(int)datetime.DayOfWeek + (int)DayOfWeek.Monday);
                lbl_dates.Text = planStartDate.ToShortDateString() + " - " + planStartDate.AddDays(4).ToShortDateString();
                int maxLine = 0;
                Grid_todoList0.ItemsSource = DB.ListWeeklyPlanDay(User, planStartDate, Order); txt_Count0.Text = Grid_todoList0.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;
                Grid_todoList1.ItemsSource = DB.ListWeeklyPlanDay(User, planStartDate.AddDays(1), Order); txt_Count1.Text = Grid_todoList1.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;
                Grid_todoList2.ItemsSource = DB.ListWeeklyPlanDay(User, planStartDate.AddDays(2), Order); txt_Count2.Text = Grid_todoList2.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;
                Grid_todoList3.ItemsSource = DB.ListWeeklyPlanDay(User, planStartDate.AddDays(3), Order); txt_Count3.Text = Grid_todoList3.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;
                Grid_todoList4.ItemsSource = DB.ListWeeklyPlanDay(User, planStartDate.AddDays(4), Order); txt_Count4.Text = Grid_todoList4.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;

                if (Grid_todoList0.Items.Count >= maxLine) maxLine = Grid_todoList0.Items.Count;
                if (Grid_todoList1.Items.Count >= maxLine) maxLine = Grid_todoList1.Items.Count;
                if (Grid_todoList2.Items.Count >= maxLine) maxLine = Grid_todoList2.Items.Count;
                if (Grid_todoList3.Items.Count >= maxLine) maxLine = Grid_todoList3.Items.Count;
                if (Grid_todoList4.Items.Count >= maxLine) maxLine = Grid_todoList4.Items.Count;
                order.Header = "Sıra";
                List<int> numbers = new List<int>();
                for (int i = 1; i <= maxLine; i++) numbers.Add(i);
                numbersGrid.ItemsSource = numbers;
                SelectedGrid = null; // null error fix
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplan", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Date_picker_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Date_picker_CalendarClosed(sender, e);
        }

        private void Date_picker_MouseEnter(object sender, MouseEventArgs e)
        {
            date_picker.IsDropDownOpen = true;
        }

        private void Btn_GetDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedGrid != null)
                {
                    GetDetail getDetail = new GetDetail();
                    getDetail.Owner = mainWindow;
                    getDetail.Plan = SelectedGrid.SelectedItem as DB.WeeklyPlan;
                    mainWindow.Opacity = 0.5;
                    getDetail.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    getDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplan", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Btn_doComplated_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedGrid != null)
                {
                    MarkCompleted markCompleted = new MarkCompleted();
                    markCompleted.Owner = mainWindow;
                    markCompleted.Plan = SelectedGrid.SelectedItem as DB.WeeklyPlan;
                    mainWindow.Opacity = 0.5;
                    markCompleted.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    if (markCompleted.ShowDialog() == false)
                    {
                        Date_picker_CalendarClosed(sender, e);
                        mainWindow.UpdateScreens();
                    }
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplan", error_text = ex.Message, log_user = User.id });
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double gridWidth = 159; // if form size changed, size must be increase.
            if (mainWindow.WindowState == WindowState.Normal) gridWidth = 159;
            else gridWidth += ((int)SystemParameters.WorkArea.Width - 997) / 5;
            gridCell0.Width = gridWidth; gridCell1.Width = gridWidth; gridCell2.Width = gridWidth;
            gridCell3.Width = gridWidth; gridCell4.Width = gridWidth;
        }

        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedGrid != null)
                {
                    RemovePlan removePlan = new RemovePlan();
                    removePlan.Owner = mainWindow;
                    removePlan.Plan = SelectedGrid.SelectedItem as DB.WeeklyPlan;
                    mainWindow.Opacity = 0.5;
                    removePlan.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    if (removePlan.ShowDialog() == false)
                    {
                        Date_picker_CalendarClosed(sender, e);
                        mainWindow.UpdateScreens();
                    }
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplan", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedGrid != null)
                {
                    EditPlan editPlan = new EditPlan();
                    editPlan.Owner = mainWindow;
                    editPlan.Plan = SelectedGrid.SelectedItem as DB.WeeklyPlan;
                    mainWindow.Opacity = 0.5;
                    editPlan.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    if (editPlan.ShowDialog() == false)
                    {
                        Date_picker_CalendarClosed(sender, e);
                        mainWindow.UpdateScreens();
                    }
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplan", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Sort_AddTime_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.AddedTimeAsc;
            Date_picker_CalendarClosed(sender, e); // update weekly plan data grids according to datapicker's date
        }

        private void Sort_Priority_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.PriorityAsc;
            Date_picker_CalendarClosed(sender, e); // update weekly plan data grids according to datapicker's date
        }

        private void Sort_NameDesc_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.NameDesc;
            Date_picker_CalendarClosed(sender, e); // update weekly plan data grids according to datapicker's date
        }

        private void Sort_NameAsc_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.NameAsc;
            Date_picker_CalendarClosed(sender, e); // update weekly plan data grids according to datapicker's date
        }

        private void Sort_PriorityDesc_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.PriorityDesc;
            Date_picker_CalendarClosed(sender, e); // update weekly plan data grids according to datapicker's date
        }

        private void Grid_todoList0_CurrentCellChanged(object sender, EventArgs e)
        {
            SelectedGrid = Grid_todoList0;
        }

        private void Grid_todoList1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SelectedGrid = Grid_todoList1;
        }

        private void Grid_todoList2_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SelectedGrid = Grid_todoList2;
        }

        private void Grid_todoList3_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SelectedGrid = Grid_todoList3;
        }

        private void Grid_todoList4_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SelectedGrid = Grid_todoList4;
        }

        private void Sort_AddTimeDesc_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.AddedTimeDesc;
            Date_picker_CalendarClosed(sender, e); // update weekly plan data grids according to datapicker's date
        }

        private void Btn_changeView_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangeViewWeeklyPlan();
        }

    }
}
