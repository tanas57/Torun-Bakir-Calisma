﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for EditPlanTransferBtn.xaml
    /// </summary>
    public partial class EditPlanTransferBtn : Window
    {
        public MainWindow mainWindow; EditPlan editPlan;
        public EditPlanTransferBtn()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            editPlan.Opacity = 1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            editPlan = (EditPlan)this.Owner;
            Title = mainWindow.Lang.WeeklyEditPlanTransferTitle;
            calendarTitle.Content = mainWindow.Lang.WeeklyEditPlanTransferTitle;
            dateChoosetxt.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddADates;
            save.Content = mainWindow.Lang.ButtonTransfer;
            editPlan.DateControl = false;
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
            editPlan.DateControl = false;
            this.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Save_Click(sender, e);
        }
    }
}
