﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Torun.Database;
using Torun.Windows;
using Torun.Windows.Request;
using Torun.Classes;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCTodoList.xaml
    /// </summary>
    public partial class UCTodoList : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        
        public bool buttonDetail = false;
        private DB DB { get; set; }
        private User User;
        public UCTodoList()
        {
            InitializeComponent();
            DB = mainWindow.DB;
            User = mainWindow.User;
            Grid_todoList.ItemsSource = DB.GetTodoLists(User);
        }
        private void btn_adddRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RequestAdd requestAdd = new RequestAdd();
                requestAdd.Owner = mainWindow;
                mainWindow.Opacity = 0.5;
                requestAdd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                requestAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "uctodolist", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Btn_requestDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.TodoListGrid temp = Grid_todoList.SelectedItem as DB.TodoListGrid;
                TodoList todoList = mainWindow.DB.GetTodoByID(temp.WorkID);
                if (todoList.status == (int)StatusType.InProcess)
                {
                    var result = MessageBox.Show("Bu talebin tamamlanan kısımları var, yinede silinsin mi (tamamlananlarda silinecek) ?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        // delete all workdone
                        var workDone = mainWindow.DB.GetWorkdoneByID(todoList.id);
                        foreach (var item in workDone) mainWindow.DB.RemoveWorkdone(item);
                        // delete all plans
                        var plans = mainWindow.DB.PlanToCalendar(todoList.id);
                        foreach (var item in plans) mainWindow.DB.RemovePlan(item);
                        // delete work
                        DB.DeleteTodoList(todoList);
                        mainWindow.UpdateScreens();
                    }
                }
                else
                {
                    var result = MessageBox.Show(todoList.id + " id numaralı talep silinecek, onaylıyor musunuz ?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        DB.DeleteTodoList(todoList);
                        mainWindow.UpdateScreens();
                    }
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "uctodolist", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Grid_todoList_Loaded(object sender, RoutedEventArgs e)
        {
            // priority ve status int to string işlemi
            ucTodolist_ListDetail.Content = mainWindow.Lang.UCTodoListAddedTime;
            ucTodolist_addRequest.Content = mainWindow.Lang.UCTodoListAddRequest;
            ucTodolist_Description.Header = mainWindow.Lang.UCTodoListDescription;
            ucTodolist_requestNumber.Header = mainWindow.Lang.UCTodoListRequestNumber;
            ucTodolist_Priority.Header = mainWindow.Lang.UCTodoListPriority;
            ucTodolist_Processes.Header = mainWindow.Lang.UCTodoListProcesses;
            if(mainWindow.WindowState == WindowState.Maximized) ucTodolist_Description.Width = SystemParameters.PrimaryScreenWidth - 560;
        }

        private void Btn_requestEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RequestDetail requestEdit = new RequestDetail();
                requestEdit.Owner = mainWindow;
                DB.TodoListGrid temp = Grid_todoList.SelectedItem as DB.TodoListGrid;
                TodoList todoList = mainWindow.DB.GetTodoByID(temp.WorkID);
                requestEdit.todolist = todoList;
                mainWindow.Opacity = 0.5;
                requestEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                requestEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "uctodolist", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Btn_requestSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RequestSchedule requestSchedule = new RequestSchedule();
                DB.TodoListGrid temp = Grid_todoList.SelectedItem as DB.TodoListGrid;
                TodoList todoList = mainWindow.DB.GetTodoByID(temp.WorkID);
                requestSchedule.Owner = mainWindow;
                requestSchedule.todolist = todoList;
                mainWindow.Opacity = 0.5;
                requestSchedule.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                requestSchedule.ShowDialog();
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "uctodolist", error_text = ex.Message, log_user = User.id });
            }
        }

        private void Grid_detail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // detail button clicked twice
                if (!buttonDetail)
                {
                    DataGridTextColumn add_time = new DataGridTextColumn();
                    add_time.Header = mainWindow.Lang.UCTodoListAddedTime;
                    add_time.Width = 120;
                    add_time.IsReadOnly = true;
                    add_time.Binding = new System.Windows.Data.Binding("AddTime");
                    Grid_todoList.Columns.Add(add_time);

                    DataGridTextColumn status = new DataGridTextColumn();
                    status.Header = mainWindow.Lang.UCTodoListStatus;
                    status.Width = 90;
                    status.IsReadOnly = true;
                    status.Binding = new System.Windows.Data.Binding("StatusString");
                    Grid_todoList.Columns.Add(status);

                    Grid_todoList.Columns[4].Width = Grid_todoList.Columns[4].Width.Value - 210;
                    buttonDetail = true;
                }
                else
                { // ikinci basışta eklenenleri gizle
                    Grid_todoList.Columns.RemoveAt(Grid_todoList.Columns.Count - 1); // add_time column
                    Grid_todoList.Columns.RemoveAt(Grid_todoList.Columns.Count - 1); // status column
                    Grid_todoList.Columns[4].Width = Grid_todoList.Columns[4].Width.Value + 210;
                    buttonDetail = false;
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "uctodolist", error_text = ex.Message, log_user = User.id });
            }
        }

        private void DataGridRequestNumber_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DB.TodoListGrid temp = Grid_todoList.SelectedItem as DB.TodoListGrid;
                TodoList todolist = mainWindow.DB.GetTodoByID(temp.WorkID);
                int result = 0;
                if (int.TryParse(todolist.request_number, out result))
                {
                    try
                    {
                        System.Diagnostics.Process.Start("chrome.exe", "http://erpdestek/WorkOrder.do?woMode=viewWO&woID=" + result + "&fromListView=true");
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Process.Start("http://erpdestek/WorkOrder.do?woMode=viewWO&woID=" + result + "&fromListView=true");
                    }

                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "uctodolist", error_text = ex.Message, log_user = User.id });
            }
        }

        private void DataGridCell_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DB.TodoListGrid temp = Grid_todoList.SelectedItem as DB.TodoListGrid;
            TodoList todoList = mainWindow.DB.GetTodoByID(temp.WorkID);
            MessageBox.Show(todoList.description, mainWindow.Lang.UCTodoListInfoMessage, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
