using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Torun.Lang;
using Torun.Classes;
using Torun.Database;

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
        public UCWeeklyPlanDetail()
        {
            InitializeComponent();
            DB = mainWindow.DB;
            Lang = mainWindow.Lang;
            User = mainWindow.User;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btn_changeView.Content = Lang.UCWeeklyPlanCloseDetail;
            txt_GetDetail.Text = mainWindow.Lang.UCWeeklyPlanButtonGetDetail;
            txt_MarkCompleted.Text = mainWindow.Lang.UCWeeklyPlanButtonDoCompleted;
            txt_RemovePlan.Text = mainWindow.Lang.UCWeeklyPlanButtonRemove;
            txt_Edit.Text = mainWindow.Lang.UCWeeklyPlanButtonEdit;

            UserControl_SizeChanged(sender, null);

            sort_lbl.Content = mainWindow.Lang.WeeklyPlanSortLbl;
            sort_AddTime.Content = mainWindow.Lang.WeeklyPlanSortAddTime;
            sort_AddTimeDesc.Content = mainWindow.Lang.WeeklyPlanSortAddTimeDesc;
            sort_Priority.Content = mainWindow.Lang.WeeklyPlanSortPriorityAsc;
            sort_PriorityDesc.Content = mainWindow.Lang.WeeklyPlanSortPriorityDesc;
            sort_NameDesc.Content = mainWindow.Lang.WeeklyPlanSortNameDesc;
            sort_NameAsc.Content = mainWindow.Lang.WeeklyPlanSortNameAsc;
            btn_changeView.Content = mainWindow.Lang.UCWeeklyPlanOpenDetail;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Btn_GetDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_doComplated_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_changeView_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangeViewWeeklyPlan();
        }

        private void Sort_AddTime_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sort_AddTimeDesc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sort_Priority_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sort_PriorityDesc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sort_NameDesc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sort_NameAsc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Plan_list_Loaded(object sender, RoutedEventArgs e)
        {
            plan_list.ItemsSource = DB.GetWeeklyPlanDetail(User);
        }
    }
}
