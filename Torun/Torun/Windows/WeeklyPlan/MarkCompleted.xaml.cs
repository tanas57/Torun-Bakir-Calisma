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

namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for MarkCompleted.xaml
    /// </summary>
    public partial class MarkCompleted : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WeeklyPlan Plan { get; set; }
        public MarkCompleted()
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

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            completed_title.Content = mainWindow.language.WeeklyCompletedTitle;
            completed_aDay.Content = mainWindow.language.WeeklyCompletedCurrentDay;
            completed_allDays.Content = mainWindow.language.WeeklyCompletedAllWork;
            completed_save.Content = mainWindow.language.ButtonSave;
        }

        private void Completed_save_Click(object sender, RoutedEventArgs e)
        {
            if (completed_aDay.IsChecked == true) // a day
            {
                // move this plan to workdone
                //mainWindow.db.MoveWorkToWorkDone(mainWindow.db.GetPlanByID(Plan.PlanID));

            }
            else // all work
            {
                //move plans that have current work id to workdone
            }
        }
    }
}
