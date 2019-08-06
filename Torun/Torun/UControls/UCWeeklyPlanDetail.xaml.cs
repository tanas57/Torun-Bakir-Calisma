using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Torun.Windows.WeeklyPlan;
using Torun.Lang;
using Torun.Classes;
using Torun.Database;
using System.Windows.Data;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCWeeklyPlanDetail.xaml
    /// </summary>
    public partial class UCWeeklyPlanDetail : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private DB DB { get; set; }
        private ILanguage Lang { get; set; }
        private User User { get; set; }
        private OrderBy Order { get; set; }
        public UCWeeklyPlanDetail()
        {
            InitializeComponent();
            DB = mainWindow.DB;
            Lang = mainWindow.Lang;
            User = mainWindow.User;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lbl_changeView.Text = Lang.UCWeeklyPlanCloseDetail;
                txt_GetDetail.Text = Lang.UCWeeklyPlanButtonGetDetail;
                txt_MarkCompleted.Text = Lang.UCWeeklyPlanButtonDoCompleted;
                txt_RemovePlan.Text = Lang.UCWeeklyPlanButtonRemove;
                txt_Edit.Text = Lang.UCWeeklyPlanButtonEdit;

                UserControl_SizeChanged(sender, null);

                sort_lbl.Content = Lang.WeeklyPlanSortLbl;
                sort_AddTime.Content = Lang.WeeklyPlanSortAddTime;
                sort_AddTimeDesc.Content = Lang.WeeklyPlanSortAddTimeDesc;
                sort_Priority.Content = Lang.WeeklyPlanSortPriorityAsc;
                sort_PriorityDesc.Content = Lang.WeeklyPlanSortPriorityDesc;
                sort_NameDesc.Content = Lang.WeeklyPlanSortNameDesc;
                sort_NameAsc.Content = Lang.WeeklyPlanSortNameAsc;

                list_requestNumber.Header = Lang.RequestAddRequestNumber;
                list_priority.Header = Lang.RequestAddRequestPriority;
                list_planDate.Header = Lang.WeeklyPlanDetailPlanDate;
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplandetail", error_text = ex.Message, log_user = User.id });
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (mainWindow.WindowState == WindowState.Normal) list_planDate.Width = 570;
            else list_planDate.Width = (int)SystemParameters.WorkArea.Width - 430;
        }

        private void Btn_GetDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (plan_list.SelectedItem != null)
                {
                    GetDetail getDetail = new GetDetail();
                    getDetail.Owner = mainWindow;
                    getDetail.Plan = plan_list.SelectedItem as DB.WeeklyPlan;
                    mainWindow.Opacity = 0.5;
                    getDetail.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    getDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucweeklyplandetail", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Btn_doComplated_Click(object sender, RoutedEventArgs e)
        {
            if (plan_list.SelectedItem != null)
            {
                MarkCompleted markCompleted = new MarkCompleted();
                markCompleted.Owner = mainWindow;
                markCompleted.Plan = plan_list.SelectedItem as DB.WeeklyPlan;
                mainWindow.Opacity = 0.5;
                markCompleted.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (markCompleted.ShowDialog() == false)
                {
                    mainWindow.UpdateScreens();
                    plan_list.ItemsSource = DB.GetWeeklyPlanDetail(User, Order);
                }
            }
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (plan_list.SelectedItem != null)
            {
                EditPlan editPlan = new EditPlan();
                editPlan.Owner = mainWindow;
                editPlan.Plan = plan_list.SelectedItem as DB.WeeklyPlan;
                mainWindow.Opacity = 0.5;
                editPlan.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (editPlan.ShowDialog() == false)
                {
                    mainWindow.UpdateScreens();
                    plan_list.ItemsSource = DB.GetWeeklyPlanDetail(User, Order);
                }
            }
        }

        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (plan_list.SelectedItem != null)
            {
                RemovePlan removePlan = new RemovePlan();
                removePlan.Owner = mainWindow;
                removePlan.Plan = plan_list.SelectedItem as DB.WeeklyPlan;
                mainWindow.Opacity = 0.5;
                removePlan.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (removePlan.ShowDialog() == false)
                {
                    mainWindow.UpdateScreens();
                    plan_list.ItemsSource = DB.GetWeeklyPlanDetail(User, Order);
                }
            }
        }

        private void Btn_changeView_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangeViewWeeklyPlan();
        }

        private void Sort_AddTime_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.AddedTimeAsc;
        }

        private void Sort_AddTimeDesc_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.AddedTimeDesc;
        }

        private void Sort_Priority_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.PriorityAsc;
        }

        private void Sort_PriorityDesc_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.PriorityDesc;
        }

        private void Sort_NameDesc_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.NameDesc;
        }

        private void Sort_NameAsc_Click(object sender, RoutedEventArgs e)
        {
            Order = OrderBy.NameAsc;
        }

        private void Plan_list_Loaded(object sender, RoutedEventArgs e)
        {
            plan_list.ItemsSource = DB.GetWeeklyPlanDetail(User, Order);
        }

    }
}