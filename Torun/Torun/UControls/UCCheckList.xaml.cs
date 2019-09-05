
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
        private List<CheckListObject> GridSource { get; set; }
        private List<RoutineWork> RoutineWorks { get; set; }
        private int WorkCount { get; set; }
        private bool CheckFirstFill { get; set; }
        public UCCheckList()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
            ReloadCheckList();
            GridSource = new List<CheckListObject>();
            CheckFirstFill = true;
            for (int i = 0; i < WorkCount; i++) GridSource.Add(new CheckListObject());
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
            DB.UpdateCheckListRecord(User, ticks, DateTime.Now.Date);
        }
        /// <summary>
        /// get changes from database if is it exists, otherwise create new record.
        /// </summary>
        private void GetChanges()
        {
            DateTime today = DateTime.Now.Date;
            RoutineWorkRecord user_record = DB.GetCheckListRecord(User, today);
            
            if(user_record != null)
            {
                string deneme = user_record.work_Ticks;
                string[] ids = user_record.work_Ticks.Split('*');
                
                for (int i = 2; i < ids.Length; i++)
                {
                    string[] workID_parse = ids[i].Split(':');

                    int work_id = int.Parse(workID_parse[0]);

                    for (int j = 0; j < GridSource.Count; j++)
                    {
                        if(GridSource[j].WorkID == work_id)
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
                    ticks += "*" + GridSource[i].WorkID + ":" + GridSource[i].Daily1 + "," + GridSource[i].Weekly1 + "," + GridSource[i].Daily2 + "," + GridSource[i].Weekly2 + "," + GridSource[i].Daily3 + "," + GridSource[i].Weekly3 + ",";
                }
                DB.AddCheckListRecord(User, ticks, today);
            }
        }
        /// <summary>
        /// add new item to routine work list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWork_Click(object sender, RoutedEventArgs e)
        {
            if(!(workDescription.Text.Length > 3))
            {
                result.Content = Lang.UCChecklistAddError;
                result.Background = Brushes.Red;
            }
            else
            {
                if (DB.AddRoutineWork(new RoutineWork() { user_id = User.id, work_description = workDescription.Text }) > 0)
                {
                    result.Content = Lang.UCChecklistAddSuccess;
                    result.Background = Brushes.Green;
                    workDescription.Text = "";
                    ReloadCheckList();
                }
            }
        }
        /// <summary>
        /// Refresh checklist pages; 
        /// datagrids, listboxes, and comboboxes
        /// </summary>
        private void ReloadCheckList()
        {
            RoutineWorks = DB.GetRoutineWorks(User);
            Grid_Checklist.ItemsSource = RoutineWorks;

            WorkCount = Grid_Checklist.Items.Count;

            var users = DB.GetUsers(User);
            userList.Items.Clear();

            foreach (var item in users)
            {
                userList.Items.Add(item.FullName + " - " + item.UserID);
            }

            var workWithUser = DB.GetUsersRelationShip(User);
            listBoxUser.Items.Clear();
            foreach (var item in workWithUser)
            {
                listBoxUser.Items.Add(item.FullName + " - " + item.UserID);
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            addWork.Content = Lang.ButtonAdd;
            routineAddLbl.Content = Lang.UCChecklistAddLbl;
            checklistTabUpdate.Header = Lang.UCChecklistUpdatePage;
            checklistTabInSystem.Header = Lang.UCChecklistInSystemPage;
            checklistTabAddNew.Header = Lang.UCChecklistAddNewPage;
            gridProcessColumn.Header = Lang.UCTodoListProcesses;
            gridDescriptionColumn.Header = Lang.UCChecklistRoutineWork;
            ReloadCheckList();

            relTitle.Content = Lang.UCChecklistRelationshipTitle;
            userListLBL.Content = Lang.UCChecklistRelationshipUserList;
            usersWithWorkLBL.Content = Lang.UCChecklistRelationshipWorkWith;
            delRel.Content = Lang.UCChecklistRelationshipRemoveUser;
            relationShipSave.Content = Lang.UCChecklistRelationshipAddUser;
            relationShip.Header = Lang.UCChecklistRelationshipWorkWith;

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
        private void RefreshMainPage()
        {
            ChecklistDockPanel.Children.Clear();
            listBoxUser.Items.Add(User.firstname + " " + User.lastname + " - " + User.id);

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
                DB.DeleteRoutineWork(routineWork);
                ReloadCheckList();
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(mainWindow.WindowState == WindowState.Maximized)
            {
                tabControl.Width  = SystemParameters.PrimaryScreenWidth - 230;
                tabControl.Height = SystemParameters.PrimaryScreenHeight - 180;
                Grid_Checklist.Width += SystemParameters.PrimaryScreenWidth - 1000;
                Grid_Checklist.Height += SystemParameters.PrimaryScreenHeight - 650;
                Grid_CheckList.Width += SystemParameters.PrimaryScreenWidth - 1000;
                Grid_CheckList.Height += SystemParameters.PrimaryScreenHeight - 650;
                gridDescriptionColumn.MinWidth +=(double) SystemParameters.PrimaryScreenWidth - 1000;
            }
            else
            {
                tabControl.Width = 775;
                tabControl.Height = 480;
                Grid_Checklist.Width = 780;
                gridDescriptionColumn.MinWidth = 635;
            }
        }

        private void RelationShipSave_Click(object sender, RoutedEventArgs e)
        {
            if (userList.SelectedItem != null)
            {
                string[] arr = userList.SelectedItem.ToString().Split('-');
                if (!DB.UserRelationShipControl(User, int.Parse(arr[1])))
                {
                    if (DB.AddUserRelationShip(User, int.Parse(arr[1])) > 0)
                    {
                        relResult.Content = Lang.UCChecklistRelationshipUserAddSuccess;
                        relResult.Background = Brushes.Green;
                        ReloadCheckList();
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
                    ReloadCheckList();
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
    }
}
