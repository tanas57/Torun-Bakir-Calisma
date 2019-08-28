using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Torun.Lang;

namespace Torun.Windows.WorkCompleted
{
    /// <summary>
    /// Interaction logic for EditWorkDoneCalendar.xaml
    /// </summary>
    public partial class EditWorkDoneCalendar : Window
    {
        private EditWorkDone editWorkDone;
        private ILanguage Lang { get; set; }
        public List<DateTime> SelectedDates { get; set; }
        public EditWorkDoneCalendar()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            editWorkDone.Opacity = 1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            editWorkDone = (EditWorkDone)this.Owner;
            Lang = CurrentLanguage.Language;

            Title = Lang.WeeklyEditPlanCalendarAddTitle;
            calendarTitle.Content = Lang.WeeklyEditPlanCalendarAddTitle;
            dateChoosetxt.Content = Lang.WeeklyEditPlanCalendarAddLabel;
            save.Content = Lang.ButtonTransfer;
            editWorkDone.DateControl = false;
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
            editWorkDone.SelectedDate = calendar.SelectedDate.Value;
            editWorkDone.DateControl = true;
            this.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Save_Click(sender, e);
        }
    }
}
