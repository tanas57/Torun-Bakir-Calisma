using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for EditPlanChooseDate.xaml
    /// </summary>
    public partial class EditPlanChooseDate : Window
    {
        public MainWindow mainWindow; EditPlan editPlan;
        public EditPlanChooseDate()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            editPlan = (EditPlan)this.Owner;
            Title = mainWindow.Lang.WeeklyEditPlanCalendarAddTitle;
            calendarTitle.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddTitle;
            dateChoosetxt.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddLabel;
            save.Content = mainWindow.Lang.ButtonSave;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            editPlan.Opacity = 1;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            save.IsEnabled = true;
            Mouse.Capture(null);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            editPlan.SelectedDates = calendar.SelectedDates.ToList();
            editPlan.UpdateMode = 1;
            this.Close();
        }
    }
}
