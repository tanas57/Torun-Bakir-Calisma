
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Torun.Database;
using Torun.Lang;
using Torun.Windows.CheckList;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCCheckList.xaml
    /// </summary>
    public partial class UCCheckList : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public User User { get; set; }
        private User Relation { get; set; }
        private List<CheckListObject> GridSource { get; set; }
        private List<RoutineWork> RoutineWorks { get; set; }
        private int WorkCount { get; set; }
        private bool CheckFirstFill { get; set; }
        public bool IsAdded { get; set; }
        private DateTime CurrentDate { get; set; }
        public UCCheckList()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
            changeDate.SelectedDate = DateTime.Now.Date;
            try
            {
                ReloadCheckList();
                GridSource = new List<CheckListObject>();
                CheckFirstFill = true;
                for (int i = 0; i < WorkCount; i++) GridSource.Add(new CheckListObject());
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucchecklist_constructor", error_text = ex.Message, log_user = User.id });
            }
        }
        private class CheckListObject
        {
            public int WorkID { get; set; }
            public int Order { get; set; }
            public string WorkDescription { get; set; }
            public bool Daily1 { get; set; }
            public bool Daily2 { get; set; }
            public bool Daily3 { get; set; }
            public bool Weekly1 { get; set; }
            public bool Weekly2 { get; set; }
            public bool Weekly3 { get; set; }
        }
        /// <summary>
        /// save all checklist changes to the database
        /// </summary>
        private void SaveChanges()
        {
            // add new record
            string ticks = "";
            for (int i = 0; i < GridSource.Count; i++)
            {
                ticks += "*" + GridSource[i].WorkID + ":" + GridSource[i].Daily1 + "," + GridSource[i].Weekly1 + "," + GridSource[i].Daily2 + "," + GridSource[i].Weekly2 + "," + GridSource[i].Daily3 + "," + GridSource[i].Weekly3 + ",";
            }
            DB.UpdateCheckListRecord(Relation, ticks, CurrentDate);
        }
        /// <summary>
        /// get changes from database if is it exists, otherwise create new record.
        /// </summary>
        private void GetChanges()
        {
            try
            {
                RoutineWorkRecord user_record = DB.GetCheckListRecord(Relation, CurrentDate);
                if (user_record != null)
                {
                    string[] ids = user_record.work_Ticks.Split('*');

                    for (int i = 1; i < ids.Length; i++)
                    {
                        string[] workID_parse = ids[i].Split(':');

                        int work_id = int.Parse(workID_parse[0]);

                        for (int j = 0; j < GridSource.Count; j++)
                        {
                            if (GridSource[j].WorkID == work_id)
                            {
                                string[] parse = workID_parse[1].Split(',');

                                GridSource[j].Daily1 = bool.Parse(parse[0]);
                                GridSource[j].Weekly1 = bool.Parse(parse[1]);
                                GridSource[j].Daily2 = bool.Parse(parse[2]);
                                GridSource[j].Weekly2 = bool.Parse(parse[3]);
                                GridSource[j].Daily3 = bool.Parse(parse[4]);
                                GridSource[j].Weekly3 = bool.Parse(parse[5]);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // add new record
                    string ticks = "";
                    for (int i = 0; i < GridSource.Count; i++)
                    {
                        GridSource[i].Daily1 = false; GridSource[i].Daily2 = false; GridSource[i].Daily3 = false;
                        GridSource[i].Weekly1 = false; GridSource[i].Weekly2 = false; GridSource[i].Weekly3 = false;
                        ticks += "*" + GridSource[i].WorkID + ":" + GridSource[i].Daily1 + "," + GridSource[i].Weekly1 + "," + GridSource[i].Daily2 + "," + GridSource[i].Weekly2 + "," + GridSource[i].Daily3 + "," + GridSource[i].Weekly3 + ",";
                    }
                    DB.AddCheckListRecord(Relation, ticks, CurrentDate);
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucchecklist_getchangesfunction", error_text = ex.Message, log_user = User.id });
            }
        }
        /// <summary>
        /// Refresh checklist pages; 
        /// datagrids, listboxes, and comboboxes
        /// </summary>
        public void ReloadCheckList()
        {
            try
            {
                Relation = DB.GetUsersRelationShipWithOtherUser(User);

                // user has a relation list 
                if (Relation != User)
                {
                    relationStack.IsEnabled = false;
                    relWorkFriend.Visibility = Visibility.Visible;
                    relTitle.Foreground = Brushes.White;
                }
                RoutineWorks = DB.GetRoutineWorks(Relation);
                Grid_Checklist.ItemsSource = RoutineWorks;

                WorkCount = Grid_Checklist.Items.Count;

                var users = DB.GetUsers(Relation);
                userList.Items.Clear();

                foreach (var item in users)
                {
                    userList.Items.Add(item.FullName + " - " + item.UserID);
                }

                var workWithUser = DB.GetUsersRelationShip(Relation);
                listBoxUser.Items.Clear();
                foreach (var item in workWithUser)
                {
                    listBoxUser.Items.Add(item.FullName + " - " + item.UserID);
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log { error_page = "ucchecklist_reloadfunction", error_text = ex.Message, log_user = User.id });
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            checklistTabUpdate.Header = Lang.UCChecklistUpdatePage;
            checklistTabInSystem.Header = Lang.UCChecklistInSystemPage + " & " + Lang.UCCheckListReport;
            addNewWork.ToolTip = Lang.UCChecklistAddNewPage;
            gridProcessColumn.Header = Lang.UCTodoListProcesses;
            gridDescriptionColumn.Header = Lang.UCChecklistRoutineWork;

            relTitle.Content = Lang.UCChecklistRelationshipTitle;
            userListLBL.Content = Lang.UCChecklistRelationshipUserList;
            usersWithWorkLBL.Content = Lang.UCChecklistRelationshipWorkWith;
            delRel.Content = Lang.UCChecklistRelationshipRemoveUser;
            relationShipSave.Content = Lang.UCChecklistRelationshipAddUser;
            relationShip.Header = Lang.UCChecklistRelationshipWorkWith;
            checkListNote.Content = Lang.UCCheckListNote;
            relWorkFriend.Content = Lang.UCCheckListOtherRelation;
            createReport.ToolTip = Lang.UCCheckListButtonTitle;

            ReloadCheckList(); // mainpage checklist load
            RefreshMainPage(); // refresh other listboxes
            GetChanges(); // get data from db if is it exist
        }
        private void AddColumnToDataGrid(DataGrid dataGrid, DataGridBoundColumn column, string header, int width, string binding)
        {
            DataGridBoundColumn temp = column;
            temp.Header = header;
            temp.Width = width;
            temp.IsReadOnly = true;
            temp.Binding = new Binding(binding);
            temp.IsReadOnly = false;
            dataGrid.Columns.Add(temp);
        }
        public void RefreshMainPage()
        {
            ChecklistDockPanel.Children.Clear();
            listBoxUser.Items.Add(Relation.firstname + " " + Relation.lastname + " - " + Relation.id);

            Grid_CheckList.Columns.Clear();

            AddColumnToDataGrid(Grid_CheckList, new DataGridTextColumn(), "Sıra", 35, "Order");
            AddColumnToDataGrid(Grid_CheckList, new DataGridTextColumn(), "Rutin İş Açıklaması", 350, "WorkDescription");
            // locate names and work relationship, and create datagrid columns
            for (int i = listBoxUser.Items.Count-1; -1 < i; i--)
            {
                string[] temp = listBoxUser.Items[i].ToString().Split('-');
                Label tempLabel;

                if(i != listBoxUser.Items.Count - 1) ChecklistDockPanel.Children.Add(new Label() { Content = " | "});

                ChecklistDockPanel.Children.Add(tempLabel = new Label() { Content = temp[0]});

                tempLabel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                string bindingPath = "Daily" + Math.Abs(i - listBoxUser.Items.Count);
                AddColumnToDataGrid(Grid_CheckList, new DataGridCheckBoxColumn(), Lang.SettingsRadioDaily, (int)(tempLabel.DesiredSize.Width / 2), bindingPath);

                bindingPath = "Weekly" + Math.Abs(i - listBoxUser.Items.Count);
                AddColumnToDataGrid(Grid_CheckList, new DataGridCheckBoxColumn(), Lang.SettingsRadioWeekly, (int)(tempLabel.DesiredSize.Width / 2) + 25, bindingPath);

            }

            if (CheckFirstFill)
            {
                for (int i = 0; i < WorkCount; i++)
                {
                    // description
                    GridSource[i].Order = (i + 1);
                    GridSource[i].WorkID = RoutineWorks[i].id;
                    GridSource[i].WorkDescription = RoutineWorks[i].work_description;
                }
                CheckFirstFill = false;
            }

            if (IsAdded)
            {
                IsAdded = false;
                GridSource.Add(new CheckListObject());
                GridSource[GridSource.Count - 1].Order = GridSource.Count;
                GridSource[GridSource.Count - 1].WorkID = RoutineWorks[GridSource.Count - 1].id;
                GridSource[GridSource.Count - 1].WorkDescription = RoutineWorks[GridSource.Count - 1].work_description;
            }
            Grid_CheckList.ItemsSource = GridSource;
            ReloadCheckList();
        }
        private void Btn_workEdit_Click(object sender, RoutedEventArgs e)
        {
            if(Grid_Checklist.SelectedItem != null)
            {
                EditRoutineWork editRoutineWork = new EditRoutineWork();
                editRoutineWork.RoutineWork = Grid_Checklist.SelectedItem as RoutineWork;
                editRoutineWork.Owner = mainWindow;
                mainWindow.Opacity = 0.5;
                editRoutineWork.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (editRoutineWork.ShowDialog() == false)
                {
                    ReloadCheckList();
                }
            }
        }

        private void Btn_workDelete_Click(object sender, RoutedEventArgs e)
        {
            RoutineWork routineWork = Grid_Checklist.SelectedItem as RoutineWork;
            var result = MessageBox.Show(routineWork.id + " id numaralı iş silinecek, onaylıyor musunuz ?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var item in GridSource)
                {
                    if(item.WorkID == routineWork.id)
                    {
                        GridSource.Remove(item);
                        break;
                    }
                }
                DB.DeleteRoutineWork(routineWork);
                ReloadCheckList();
                RefreshMainPage();
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (mainWindow.WindowState == WindowState.Maximized)
            {
                tabControl.Height = (double)SystemParameters.FullPrimaryScreenHeight - 80;
                tabControl.Width = (double)SystemParameters.PrimaryScreenWidth - 180;
                Grid_Checklist.Width += tabControl.Width - 10;
                Grid_Checklist.Height += tabControl.Height - 10;
                Grid_CheckList.Width += tabControl.Width - 10;
                Grid_CheckList.Height += tabControl.Height - 10;
                gridDescriptionColumn.MinWidth += (double)SystemParameters.PrimaryScreenWidth - 1000;
            }
            else
            {
                tabControl.Width = 820;
                tabControl.Height = 560;
                Grid_CheckList.Width += tabControl.Width - 10;
                Grid_CheckList.Height += tabControl.Height - 15;
                Grid_Checklist.Width += tabControl.Width - 10;
                Grid_Checklist.Height += tabControl.Height - 35;
                gridDescriptionColumn.MinWidth = 495;
            }
        }

        private void RelationShipSave_Click(object sender, RoutedEventArgs e)
        {
            if (userList.SelectedItem != null)
            {
                string[] arr = userList.SelectedItem.ToString().Split('-');
                if (!DB.UserRelationShipControl(User, int.Parse(arr[1])))
                {
                    if(listBoxUser.Items.Count < 2)
                    {
                        if (DB.AddUserRelationShip(User, int.Parse(arr[1])) > 0)
                        {
                            relResult.Content = Lang.UCChecklistRelationshipUserAddSuccess;
                            relResult.Background = Brushes.Green;
                            ReloadCheckList(); // mainpage checklist load
                            RefreshMainPage(); // refresh other listboxes
                            GetChanges(); // get data from db if is it exist
                        }
                    }
                    else
                    {
                        relResult.Content = Lang.UCCheckListRelationFull;
                        relResult.Background = Brushes.Red;
                    }
                }
                else
                {
                    relResult.Content = Lang.UCChecklistRelationshipUserAddFailed;
                    relResult.Background = Brushes.Red;
                }
            }
        }

        private void DelRel_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxUser.SelectedItem != null)
            {
                string[] arr = listBoxUser.SelectedItem.ToString().Split('-');
                if (DB.RemoveRelationShip(User, int.Parse(arr[1])))
                {
                    relResult.Content = Lang.UCChecklistRelationshipUserRemoveSuccess;
                    relResult.Background = Brushes.Green;
                    ReloadCheckList(); // mainpage checklist load
                    RefreshMainPage(); // refresh other listboxes
                    GetChanges(); // get data from db if is it exist
                }
                else
                {
                    relResult.Content = Lang.UCChecklistRelationshipUserRemoveFailed;
                    relResult.Background = Brushes.Red;
                }
            }
        }

        private void Changed()
        {
            if (Grid_CheckList.SelectedItem != null)
            {
                var columnIndex = Grid_CheckList.CurrentColumn.DisplayIndex;

                if (columnIndex > 1) // order, and description are skipped
                {
                    // get selected row info
                    CheckListObject selected = Grid_CheckList.SelectedItem as CheckListObject;
                    switch (columnIndex)
                    {
                        case 2: // daily1
                            if (selected.Daily1) selected.Daily1 = false;
                            else selected.Daily1 = true;
                            break;
                        case 3: // weekly1
                            if (selected.Weekly1) selected.Weekly1 = false;
                            else selected.Weekly1 = true;
                            break;
                        case 4: // daily1
                            if (selected.Daily2) selected.Daily2 = false;
                            else selected.Daily2 = true;
                            break;
                        case 5: // weekly1
                            if (selected.Weekly2) selected.Weekly2 = false;
                            else selected.Weekly2 = true;
                            break;
                        case 6: // daily1
                            if (selected.Daily3) selected.Daily3 = false;
                            else selected.Daily3 = true;
                            break;
                        case 7: // weekly1
                            if (selected.Weekly3) selected.Weekly3 = false;
                            else selected.Weekly3 = true;
                            break;
                    }
                }

                RefreshMainPage(); // refresh user screen
                SaveChanges();  // checklist changes saves to database       
            }
        }

        private void Grid_CheckList_CurrentCellChanged(object sender, EventArgs e)
        {
            var currentRowIndex = Grid_CheckList.Items.IndexOf(Grid_CheckList.CurrentItem);
            Grid_CheckList.SelectedIndex = currentRowIndex;
            Changed();
        }

        private void AddNewWork_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AddNewRoutineWork add = new AddNewRoutineWork();
            add.Owner = mainWindow;
            mainWindow.Opacity = 0.5;
            add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            add.ShowDialog();
            if (IsAdded)
            {
                Grid_CheckList.ItemsSource = null; // bug fix
                RefreshMainPage();
            }
        }

        private void CreateReport_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CheckListReport checkListReport = new CheckListReport();
            checkListReport.Owner = mainWindow;
            checkListReport.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            mainWindow.Opacity = 0.5;
            checkListReport.ShowDialog();
        }

        private void ChangeDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (changeDate.SelectedDate.HasValue)
            {
                CurrentDate = changeDate.SelectedDate.Value;
                ReloadCheckList(); // mainpage checklist load
                RefreshMainPage(); // refresh other listboxes
                GetChanges(); // get data from db if is it exist
            }
        }
    }
}
