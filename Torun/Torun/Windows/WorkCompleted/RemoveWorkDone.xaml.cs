﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Torun.Database;
using Torun.Classes;

namespace Torun.Windows.WorkCompleted
{
    /// <summary>
    /// Interaction logic for RemoveWorkDone.xaml
    /// </summary>
    public partial class RemoveWorkDone : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WorkDoneList Work { get; set; }
        public RemoveWorkDone()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = mainWindow.Lang.WorkDoneRemoveTitle;
            remove_title.Content = mainWindow.Lang.WorkDoneRemoveTitle;
            remove_aDay.Content = mainWindow.Lang.WorkDoneRemoveSelectADay;
            remove_allDays.Content = mainWindow.Lang.WorkDoneRemoveSelectAllDays;
            removeSave.Content = mainWindow.Lang.WeeklyRemoveButtonRemove;
            int workDayCount = mainWindow.DB.GetWorkdoneByID(Work.WorkID).Count;
            if (workDayCount == 1) remove_allDays.IsEnabled = false;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("İş yapılmadı olarak işaretlenecek, onaylıyor musunuz ?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    WorkDone work = mainWindow.DB.GetWorkDoneByID(Work.WorkDoneID);
                    int workDoneCount = mainWindow.DB.GetWorkdoneByID(Work.WorkID).Count;
                    int plansCount = mainWindow.DB.PlanToCalendar(Work.WorkID, true).Count; // status continue of works

                    if (remove_aDay.IsChecked == true) // only selected work done
                    {
                        Plan plan = mainWindow.DB.GetPlanByID(work.plan_id);
                        TodoList todoList = mainWindow.DB.GetTodoByID(plan.work_id);
                        if (remove_allDays.IsEnabled == false)
                        {
                            if (workDoneCount > 1) // there is any completed works so the work is being process
                            {
                                todoList.status = (int)StatusType.InProcess;
                            }
                            else todoList.status = (int)StatusType.Planned;
                        }
                        else
                        {
                            todoList.status = (int)StatusType.InProcess;
                        }
                        plan.status = (int)StatusType.Deleted;
                        mainWindow.DB.RemoveWorkdone(work);
                        mainWindow.DB.EditPlan(plan);
                    }
                    else if (remove_allDays.IsChecked == true)
                    {
                        var works = mainWindow.DB.GetWorkdoneByID(Work.WorkID);
                        if (works.Count > 0)
                        {
                            Plan plan = mainWindow.DB.GetPlanByID(works[0].plan_id);
                            TodoList todoList = mainWindow.DB.GetTodoByID(plan.work_id);
                            todoList.status = (int)StatusType.Planned;
                        }
                        foreach (var item in works)
                        {
                            // workdone will be deleted
                            // plan will be planned
                            Plan plan = mainWindow.DB.GetPlanByID(item.plan_id);
                            plan.status = (int)StatusType.Planned;
                            mainWindow.DB.EditPlan(item.Plan);
                            mainWindow.DB.RemoveWorkdone(item);
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                mainWindow.DB.AddLog(new Log { error_page = "removeworkdone_RemoveSave_Click", error_text = ex.Message, log_user = mainWindow.User.id });
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) RemoveSave_Click(sender, e);
        }
    }
}
