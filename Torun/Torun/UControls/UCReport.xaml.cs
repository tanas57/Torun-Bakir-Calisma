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
using System.Threading;
using System.Windows.Media;
using Torun.Windows.Report;
using Torun.Windows.WeeklyPlan;
using Torun.Windows.WorkCompleted;

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
        private User SearchUser { get; set; }
        public List<DateTime> SearchDateTimes { get; set; }
        public UCReport()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
            SearchUser = User;
            if (User.user_permission == 2) // Admin
            {
                adminRow.Visibility = Visibility.Visible;
            }
            else
            {
                adminRow.Visibility = Visibility.Hidden;
                reportBorder.Height += 50;
                reportBorder.Margin = new Thickness(0, -50, 0, 0);
            }
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
            timeIntervalSelect.Items.Add(Lang.CountTypeSelectDate);

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

            userSelectLBL.Content = Lang.UCChecklistRelationshipUserList;
            reportSearch.ToolTip = Lang.ReportSearchTitle;

            var users = DB.GetUsers(User, true);
            UserList.Items.Clear();

            foreach (var item in users)
            {
                UserList.Items.Add(item.FullName + " - " + item.UserID);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (User.user_permission == 2)
            {
                if (UserList.SelectedItem != null)
                {
                    string[] arr = UserList.SelectedItem.ToString().Split('-');
                    User user = DB.GetUserByID(int.Parse(arr[1]));
                    SearchUser = user;
                }
                //}
                //if(SearchDateTimes[0] != null && SearchDateTimes[1] != null && CountType == CountType.SelectDate)
                //{

                //}

                if (planWorkdoneSelect.SelectedIndex == (int)ReportType.OnlyPlan)
                {
                    grid_onlyPlan.Visibility = Visibility.Visible;
                    grid_onlyWorkdone.Visibility = Visibility.Hidden;
                    grid_both.Visibility = Visibility.Hidden;
                    if (SearchDateTimes != null && SearchDateTimes[0] != null && SearchDateTimes[1] != null && CountType == CountType.SelectDate) grid_onlyPlan.ItemsSource = DB.GetPlansForReport(SearchUser, CountType, SearchDateTimes);
                    else grid_onlyPlan.ItemsSource = DB.GetPlansForReport(SearchUser, CountType);
                }
                else if (planWorkdoneSelect.SelectedIndex == (int)ReportType.OnlyWorkDone)
                {
                    grid_onlyPlan.Visibility = Visibility.Hidden;
                    grid_onlyWorkdone.Visibility = Visibility.Visible;
                    grid_both.Visibility = Visibility.Hidden;
                    if (SearchDateTimes != null && SearchDateTimes[0] != null && SearchDateTimes[1] != null && CountType == CountType.SelectDate) grid_onlyWorkdone.ItemsSource = DB.GetWorkDoneForReport(SearchUser, CountType, SearchDateTimes);
                    else grid_onlyWorkdone.ItemsSource = DB.GetWorkDoneForReport(SearchUser, CountType);
                }
                else if (planWorkdoneSelect.SelectedIndex == (int)ReportType.Both)
                {
                    grid_onlyPlan.Visibility = Visibility.Hidden;
                    grid_onlyWorkdone.Visibility = Visibility.Hidden;
                    grid_both.Visibility = Visibility.Visible;
                    if (SearchDateTimes != null && SearchDateTimes[0] != null && SearchDateTimes[1] != null && CountType == CountType.SelectDate) grid_both.ItemsSource = DB.GetWorkDoneAndPlansForReport(SearchUser, CountType, SearchDateTimes);
                    else grid_both.ItemsSource = DB.GetWorkDoneAndPlansForReport(SearchUser, CountType);
                }
                btn_excel.Visibility = Visibility.Visible;
                btn_pdf.Visibility = Visibility.Visible;
                TorunLogo.Visibility = Visibility.Hidden;
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (mainWindow.WindowState == WindowState.Normal)
            {
                grid1PlanDate.Width = 285;
                grid1AddDate.Width = 285;
                grid2PlanDate.Width = 285;
                grid2DoneDate.Width = 285;
                grid2workDoneWorkDoneTime.Width = 650;

                ReportGridPlan.MaxWidth = 830;
                ReportGridPlan.MaxHeight = 470;

                ReportGridPlan.Width = 825;
                ReportGridPlan.Height = 465;

                grid_onlyWorkdone.Width = 825;
                grid_onlyWorkdone.Height = 465;

                grid_both.Width = 825;
                grid_both.Height = 465;
            }
            else
            {
                grid1PlanDate.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
                grid1AddDate.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
                grid2PlanDate.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
                grid2DoneDate.Width = 285 + (int)(SystemParameters.WorkArea.Width - 1000) / 2;
                ReportGridPlan.MaxWidth = 830 + ((int)(SystemParameters.WorkArea.Width - 1000));
                ReportGridPlan.MaxHeight = 465 + ((int)(SystemParameters.WorkArea.Height - 610));
                ReportGridPlan.Width = 825 + ((int)(SystemParameters.WorkArea.Width - 1000));
                ReportGridPlan.Height = 460 + ((int)(SystemParameters.WorkArea.Height - 610));

                grid_onlyWorkdone.Width = 825 + ((int)(SystemParameters.WorkArea.Width - 1000));
                grid_onlyWorkdone.Height = 460 + ((int)(SystemParameters.WorkArea.Height - 610));

                grid_both.Width = 825 + ((int)(SystemParameters.WorkArea.Width - 1000)); ;
                grid_both.Height = 460 + ((int)(SystemParameters.WorkArea.Height - 610));

                grid2workDoneWorkDoneTime.Width = 650 + ((int)(SystemParameters.WorkArea.Width - 1093));
            }
        }

        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportProcess.Background = Brushes.Blue;
                reportProcess.Content = Lang.ReportProcessStart;

                Thread thread = new Thread(new ThreadStart(ExportPDF));
                thread.Start();
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucreport_pdfbutton", error_text = ex.Message, log_user = User.id });
            }
        }
        /// <summary>
        /// Reports exports as PDF or EXCEL type
        /// </summary>
        /// <param name="type">True : EXCEL, False : PDF</param>
        private void ExportPDF()
        {
            //type = true;
            //if(type) FileOperation.ExportAsEXCEL(User, CountType, (ReportType)planWorkdoneSelect.SelectedIndex, DB);
            //else FileOperation.ExportAsPDF(User, CountType, (ReportType)planWorkdoneSelect.SelectedIndex, DB);
            //FileOperation.ExportAsPDF(User, CountType, (ReportType)planWorkdoneSelect.SelectedIndex, DB);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (SearchDateTimes != null && SearchDateTimes[0] != null && SearchDateTimes[1] != null && CountType == CountType.SelectDate) FileOperation.ExportAsPDF(SearchUser, CountType, (ReportType)planWorkdoneSelect.SelectedIndex, DB, SearchDateTimes);
                else FileOperation.ExportAsPDF(SearchUser, CountType, (ReportType)planWorkdoneSelect.SelectedIndex, DB);

                reportProcess.Background = Brushes.Green;
                reportProcess.Content = Lang.ReportProcessEnd;
            }));

        }
        private void ExportEXCEL()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (SearchDateTimes != null && SearchDateTimes[0] != null && SearchDateTimes[1] != null && CountType == CountType.SelectDate) FileOperation.ExportAsEXCEL(SearchUser, CountType, (ReportType)planWorkdoneSelect.SelectedIndex, DB, SearchDateTimes);
                else FileOperation.ExportAsEXCEL(SearchUser, CountType, (ReportType)planWorkdoneSelect.SelectedIndex, DB);
                reportProcess.Background = Brushes.Green;
                reportProcess.Content = Lang.ReportProcessEnd;
            }));
        }
        private void Btn_excel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportProcess.Background = Brushes.Blue;
                reportProcess.Content = Lang.ReportProcessStart;

                Thread thread = new Thread(new ThreadStart(ExportEXCEL));
                thread.Start();
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucreport_excelbutton", error_text = ex.Message, log_user = User.id });
            }
        }

        private void ReportSearch_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ReportSearch reportSearch = new ReportSearch();
            reportSearch.Owner = mainWindow;
            reportSearch.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            reportSearch.Top = 200;
            reportSearch.ShowDialog();

            grid_onlyPlan.Visibility = Visibility.Visible;
            grid_onlyWorkdone.Visibility = Visibility.Hidden;
            grid_both.Visibility = Visibility.Hidden;

        }

        private void Grid_onlyPlan_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (grid_onlyPlan != null)
                {
                    GetDetail getDetail = new GetDetail();
                    getDetail.Owner = mainWindow;
                    getDetail.Plan = grid_onlyPlan.SelectedItem as DB.WeeklyPlan;
                    mainWindow.Opacity = 0.5;
                    getDetail.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    getDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucreport_gridonlyplan_doubleclick", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Grid_onlyWorkdone_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (grid_onlyWorkdone != null)
                {
                    DetailWorkDone detailWorkDone = new DetailWorkDone();
                    detailWorkDone.Owner = mainWindow;
                    detailWorkDone.Work = grid_onlyWorkdone.SelectedItem as DB.WorkDoneList;
                    mainWindow.Opacity = 0.5;
                    detailWorkDone.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    detailWorkDone.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucreport_gridonlyworkdone_doubleclick", error_text = ex.Message, log_user = User.id });
            }
        }

        private void TimeIntervalSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountType = (CountType)timeIntervalSelect.SelectedIndex;
            if (CountType == CountType.SelectDate)
            {
                ReportDateSelector dateSelector = new ReportDateSelector();
                dateSelector.Owner = mainWindow;
                dateSelector.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                mainWindow.Opacity = 0.5;
                dateSelector.ShowDialog();
            }
        }
    }
}
