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
            
            this.Title = mainWindow.language.RequestScheduleTitle;
            req_RequestSchedule.Content = mainWindow.language.RequestScheduleTitle;
            schedule_ReqNumber.Content = mainWindow.language.RequestScheduleReqNumber;
            schedule_ReqDate.Content = mainWindow.language.RequestScheduleReqDate;
            schedule_ReqADay.Content = mainWindow.language.RequestScheduleADay;
            schedule_ReqManyDays.Content = mainWindow.language.RequestScheduleManyDays;
            if (todolist == null) this.Close();
            else
            {
                schedule_DBReqDate.Content = todolist.add_time;
                schedule_DBReqNumber.Content = todolist.request_number;
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }
    }
}
