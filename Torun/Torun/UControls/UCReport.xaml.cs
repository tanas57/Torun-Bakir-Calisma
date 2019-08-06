using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Torun.Database;
using Torun.Classes;
using Torun.Lang;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCReport.xaml
    /// </summary>
    public partial class UCReport : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private ILanguage Lang { get; set; }
        private DB DB { get; set; }
        private CountType CountType { get; set; }
        private User User { get; set; }
        private List<DB.WorkDoneList> workDoneLists;
        public UCReport()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planWorkdoneSelect.Items.Clear();
            timeIntervalSelect.Items.Clear();

            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeOnlyPlan);
            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeOnlyWorkDone);
            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeBothofThem);

            timeIntervalSelect.Items.Add(Lang.SettingsRadioDaily);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioWeekly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioMonthly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioYearly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioBeforeStart);

            timeIntervalSelect.SelectedIndex = (int)Settings.DefaultReportInterval;
            planWorkdoneSelect.SelectedIndex = (int)Settings.DefaultReportListType;

            lblType.Content = Lang.ReportLabelPlanWorkdone;
            lblTime.Content = Lang.ReportLabelTimeInterval;

            grid1ReqNum.Header = Lang.RequestAddRequestNumber;
            grid1AddDate.Header = Lang.UCTodoListAddedTime;
            grid1PlanDate.Header = Lang.WeeklyPlanDetailPlanDate;
            grid1Priority.Header = Lang.RequestAddRequestPriority;

            grid2ReqNum.Header = Lang.RequestAddRequestNumber;
            grid2DoneDate.Header = Lang.WorkDoneTime;
            grid2PlanDate.Header = Lang.WeeklyPlanDetailPlanDate;
            grid2Priority.Header = Lang.RequestAddRequestPriority;

            grid2workDoneReqNum.Header = Lang.RequestAddRequestNumber;
            grid2workDoneWorkDoneTime.Header = Lang.WorkDoneTime;
            search.Content = Lang.ButtonGet;

            btnPdfText.Text = Lang.ReportToPDF;
            btntxtexcel.Text = Lang.ReportToExcel;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            CountType = (CountType)timeIntervalSelect.SelectedIndex;
            if (planWorkdoneSelect.SelectedIndex == (int)ReportType.OnlyPlan)
            {
                grid_onlyPlan.Visibility = Visibility.Visible;
                grid_onlyWorkdone.Visibility = Visibility.Hidden;
                grid_both.Visibility = Visibility.Hidden;
                grid_onlyPlan.ItemsSource = DB.GetPlansForReport(User, CountType);
            }
            else if (planWorkdoneSelect.SelectedIndex == (int)ReportType.OnlyWorkDone)
            {
                grid_onlyPlan.Visibility = Visibility.Hidden;
                grid_onlyWorkdone.Visibility = Visibility.Visible;
                grid_both.Visibility = Visibility.Hidden;
                workDoneLists = DB.GetWorkDoneForReport(User, CountType);
                grid_onlyWorkdone.ItemsSource = workDoneLists;
            }
            else if (planWorkdoneSelect.SelectedIndex == (int)ReportType.Both)
            {
                grid_onlyPlan.Visibility = Visibility.Hidden;
                grid_onlyWorkdone.Visibility = Visibility.Hidden;
                grid_both.Visibility = Visibility.Visible;
                grid_both.ItemsSource = DB.GetWorkDoneAndPlansForReport(User, CountType);
            }
            btn_excel.Visibility = Visibility.Visible;
            btn_pdf.Visibility = Visibility.Visible;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (mainWindow.WindowState == WindowState.Normal)
            {
                grid1PlanDate.Width = 285;
                grid1AddDate.Width = 285;
                grid2PlanDate.Width = 285;
                grid2DoneDate.Width = 285;
            }
            else
            {
                grid1PlanDate.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
                grid1AddDate.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
                grid2PlanDate.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
                grid2DoneDate.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
            }
        }

        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileOperation.ExportAsPDF(User, CountType, (ReportType)planWorkdoneSelect.SelectedIndex, DB);
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucreport", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Btn_excel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
