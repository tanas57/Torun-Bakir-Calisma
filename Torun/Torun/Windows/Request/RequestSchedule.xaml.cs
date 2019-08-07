using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Torun.Classes;
using Torun.Database;

namespace Torun.Windows.Request
{
    /// <summary>
    /// Interaction logic for RequestSchedule.xaml
    /// </summary>
    public partial class RequestSchedule : Window
    {
        private readonly MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public TodoList todolist;
        public RequestSchedule()
        {
            InitializeComponent();
        }

        private void Req_btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Opacity = 1;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            schedule_chooseDate.Content = mainWindow.Lang.RequestScheduleChooseDate;
            this.Title = mainWindow.Lang.RequestScheduleTitle;
            req_RequestSchedule.Content = mainWindow.Lang.RequestScheduleTitle;
            schedule_ReqNumber.Content = mainWindow.Lang.RequestScheduleReqNumber;
            Schedule_Save.Content = mainWindow.Lang.RequestScheduleSave;
            if (todolist == null) this.Close();
            else
            {
                schedule_DBReqNumber.Content = todolist.request_number;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Schedule_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (schedule_ReqDatePicker.SelectedDate.HasValue)
                {
                    Plan plans;
                    lbl_scheduleResult.Background = Brushes.Blue;
                    lbl_scheduleResult.Content = "Planlama kaydediliyor...";
                    int count = schedule_ReqDatePicker.SelectedDates.Count, count2 = 0;
                    foreach (var item in schedule_ReqDatePicker.SelectedDates)
                    {
                        plans = new Plan
                        {
                            add_time = DateTime.Now,
                            work_id = todolist.id,
                            status = 0,
                            work_plan_time = item
                        };
                        mainWindow.DB.AddPlanDates(plans);
                        count2++;
                    }
                    if (count == count2)
                    {
                        todolist.status = (int)StatusType.Planned;
                        mainWindow.DB.EditTodoList(todolist);
                        mainWindow.UpdateScreens();
                        this.Close();
                    }
                    else
                    {
                        lbl_scheduleResult.Background = Brushes.Red;
                        lbl_scheduleResult.Content = mainWindow.Lang.RequestScheduleSaveFailed;
                    }
                }
                else
                {
                    lbl_scheduleResult.Background = Brushes.Red;
                    lbl_scheduleResult.Content = mainWindow.Lang.RequestScheduleSaveChooseDateError;
                }
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "requestschedule_Schedule_Save_Click", error_text = ex.Message, log_user = mainWindow.User.id });
            }
        }

        private void Schedule_ReqDatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Schedule_Save_Click(sender, e);
        }
    }
}
