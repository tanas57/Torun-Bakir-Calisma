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
using System.Windows.Shapes;
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
        public todoList todolist;
        public RequestSchedule()
        {
            InitializeComponent();
        }

        private void Req_btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Opacity = 1;
            this.Close();
        }

        private void Req_Save_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            schedule_chooseDate.Content = mainWindow.language.RequestScheduleChooseDate;
            this.Title = mainWindow.language.RequestScheduleTitle;
            req_RequestSchedule.Content = mainWindow.language.RequestScheduleTitle;
            schedule_ReqNumber.Content = mainWindow.language.RequestScheduleReqNumber;
            schedule_ReqDate.Content = mainWindow.language.RequestScheduleReqDate;
            Schedule_Save.Content = mainWindow.language.RequestScheduleSave;
            if (todolist == null) this.Close();
            else
            {
                schedule_DBReqDate.Content = todolist.add_time.Value.ToString("dd.MM.yyyy");
                schedule_DBReqNumber.Content = todolist.request_number;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Schedule_Save_Click(object sender, RoutedEventArgs e)
        {
            if (schedule_ReqDatePicker.SelectedDate.HasValue)
            {
                plans plans;
                lbl_scheduleResult.Background = Brushes.Blue;
                lbl_scheduleResult.Content = "Planlama kaydediliyor...";
                int count = schedule_ReqDatePicker.SelectedDates.Count, count2 = 0;
                foreach (var item in schedule_ReqDatePicker.SelectedDates)
                {
                    plans = new plans
                    {
                        add_time = DateTime.Now,
                        work_id = todolist.id,
                        work_plan_time = item
                    };
                    mainWindow.db.AddPlanDates(plans);
                    count2++;
                }
                if(count == count2)
                {
                    todolist.status = (int)StatusType.Planned;
                    mainWindow.db.EditTodoList(todolist);
                    mainWindow.UpdateScreens();
                    this.Close();
                }
                else
                {
                    lbl_scheduleResult.Background = Brushes.Red;
                    lbl_scheduleResult.Content = mainWindow.language.RequestScheduleSaveFailed;
                }
            }
            else
            {
                lbl_scheduleResult.Background = Brushes.Red;
                lbl_scheduleResult.Content = mainWindow.language.RequestScheduleSaveChooseDateError;
            }
            
            
        }
    }
}
