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

namespace Torun.Windows.WorkCompleted
{
    /// <summary>
    /// Interaction logic for EditWorkDoneCalendar.xaml
    /// </summary>
    public partial class EditWorkDoneCalendar : Window
    {
        public MainWindow mainWindow; EditWorkDone editWorkDone;
        public List<DateTime> SelectedDates { get; set; }
        public EditWorkDoneCalendar()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            editWorkDone = (EditWorkDone)this.Owner;
            Title = mainWindow.Lang.WeeklyEditPlanCalendarAddTitle;
            calendarTitle.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddTitle;
            dateChoosetxt.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddLabel;
            save.Content = mainWindow.Lang.ButtonSave;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            save.IsEnabled = true;
            Mouse.Capture(null);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            editWorkDone.SelectedDates = calendar.SelectedDates.ToList();
            this.Close();
        }
    }
}
