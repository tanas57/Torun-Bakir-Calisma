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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Torun.Classes;
using Torun.Database;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCWorkDone.xaml
    /// </summary>
    public partial class UCWorkDone : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private DB db; private users currentUser;
        private DateTime planStartDate;
        private DataGrid SelectedGrid { get; set; }
        private OrderBy Order { get; set; }
        public UCWorkDone()
        {
            InitializeComponent();
            db = mainWindow.DB;
            currentUser = mainWindow.User;
            Order = OrderBy.AddedTime;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int gridWidth = 155; // if form size changed, size must be increase.
            if (mainWindow.WindowState == WindowState.Normal) gridWidth = 150;
            else gridWidth += ((int)SystemParameters.WorkArea.Width - 1030) / 5;
            gridCell0.Width = gridWidth; gridCell1.Width = gridWidth; gridCell2.Width = gridWidth;
            gridCell3.Width = gridWidth; gridCell4.Width = gridWidth;
        }

        private void Btn_GetDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Date_picker_MouseEnter(object sender, MouseEventArgs e)
        {
            date_picker.IsDropDownOpen = true;
        }

        private void Date_picker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (date_picker.SelectedDate.HasValue)
            {
                DateTime value = date_picker.SelectedDate.Value;
                LabelandGridUpdate(value);
            }
            else LabelandGridUpdate(DateTime.Now.Date);
        }

        private void Date_picker_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Date_picker_CalendarClosed(sender, e);
        }

        private void Grid_todoList0_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
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

        private void Sort_AddTime_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.AddedTime;
            Date_picker_CalendarClosed(sender, e); // update weekly plan data grids according to datapicker's date
            sort_AddTime.Background = System.Windows.Media.Brushes.Green;
        }

        private void Sort_Priority_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sort_PriorityDesc_Click(object sender, RoutedEventArgs e)
        {

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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
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
            LabelandGridUpdate(DateTime.Now.Date);

            UserControl_SizeChanged(sender, null);
            LabelandGridUpdate(DateTime.Now.Date);

            sort_lbl.Content = mainWindow.Lang.WeeklyPlanSortLbl;
            sort_AddTime.Content = mainWindow.Lang.WeeklyPlanSortAddTime;
            sort_Priority.Content = mainWindow.Lang.WeeklyPlanSortPriorityAsc;
            sort_PriorityDesc.Content = mainWindow.Lang.WeeklyPlanSortPriorityDesc;
            sort_NameDesc.Content = mainWindow.Lang.WeeklyPlanSortNameDesc;
            sort_NameAsc.Content = mainWindow.Lang.WeeklyPlanSortNameAsc;
        }
        private void LabelandGridUpdate(DateTime datetime)
        {
            planStartDate = datetime.AddDays(-(int)datetime.DayOfWeek + (int)DayOfWeek.Monday);
            lbl_dates.Text = planStartDate.ToShortDateString() + " - " + planStartDate.AddDays(4).ToShortDateString();
            int maxLine = 0;
            Grid_todoList0.ItemsSource = db.ListWorkDone(currentUser, planStartDate, Order); txt_Count0.Text = Grid_todoList0.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;
            Grid_todoList1.ItemsSource = db.ListWorkDone(currentUser, planStartDate.AddDays(1), Order); txt_Count1.Text = Grid_todoList1.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;
            Grid_todoList2.ItemsSource = db.ListWorkDone(currentUser, planStartDate.AddDays(2), Order); txt_Count2.Text = Grid_todoList2.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;
            Grid_todoList3.ItemsSource = db.ListWorkDone(currentUser, planStartDate.AddDays(3), Order); txt_Count3.Text = Grid_todoList3.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;
            Grid_todoList4.ItemsSource = db.ListWorkDone(currentUser, planStartDate.AddDays(4), Order); txt_Count4.Text = Grid_todoList4.Items.Count.ToString() + " " + mainWindow.Lang.UCWeeklyPlanNumOfPlans;

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

        private void Btn_doComplated_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
