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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        private int WorkFriendCount { get; set; }
        private List<bool> CheckBoxes { get; set; }
        private List<RoutineWork> RoutineWorks { get; set; }
        private int WorkCount { get; set; }
        public UCCheckList()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
            CheckBoxes = new List<bool>();
            WorkFriendCount = 1;
            for (int i = 0; i < WorkCount; i++) CheckBoxes.Add(true);
        }
        private class CheckListObject
        {
            public string WorkDescription { get; set; }
            public bool Daily1 { get; set; }
            public bool Daily2 { get; set; }
            public bool Daily3 { get; set; }
            public bool Weekly1 { get; set; }
            public bool Weekly2 { get; set; }
            public bool Weekly3 { get; set; }
        }
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

            WorkFriendCount = listBoxUser.Items.Count + 1;

            for (int i = 0; i < WorkCount * WorkFriendCount; i++) CheckBoxes.Add(true);
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

            ReloadCheckList();

            RefreshMainPage();
        }
        private void RefreshMainPage()
        {
            ChecklistDockPanel.Children.Clear();
            listBoxUser.Items.Add(User.firstname + " " + User.lastname + " - " + User.id);

            // locate names and work relationship, and create datagrid columns
            for (int i = listBoxUser.Items.Count-1; -1 < i; i--)
            {
                string[] temp = listBoxUser.Items[i].ToString().Split('-');
                Label tempLabel;

                if(i != listBoxUser.Items.Count - 1) ChecklistDockPanel.Children.Add(new Label() { Content = " | "});

                ChecklistDockPanel.Children.Add(tempLabel = new Label() { Content = temp[0]});

                tempLabel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                DataGridCheckBoxColumn daily = new DataGridCheckBoxColumn();
                daily.Header = Lang.SettingsRadioDaily;
                daily.Width = tempLabel.DesiredSize.Width / 2;
                daily.MinWidth = 60;
                daily.IsReadOnly = true;
                daily.Binding = new Binding("Daily" + listBoxUser.Items.Count+1);
                daily.IsReadOnly = false;
                
                Grid_CheckList.Columns.Add(daily);

                DataGridCheckBoxColumn weekly = new DataGridCheckBoxColumn();
                weekly.Header = Lang.SettingsRadioWeekly;
                weekly.Width = (tempLabel.DesiredSize.Width / 2) + 25;
                weekly.MinWidth = 70;
                weekly.IsReadOnly = true;
                weekly.Binding = new Binding("Weekly" + listBoxUser.Items.Count + 1);
                weekly.IsReadOnly = false;
                
                Grid_CheckList.Columns.Add(weekly);
            }

            List<CheckListObject> listSource = new List<CheckListObject>();
            CheckListObject addObject = null;
            int counter = 0;

            for (int i = 0; i < WorkCount; i++)
            {
                addObject = new CheckListObject();
                // description
                addObject.WorkDescription = RoutineWorks[i].work_description;
                // for checkboxes
                for (int j = 1; j <= WorkFriendCount; j++)
                {
                    switch (j)
                    {
                        default:
                        case 1: // only us
                            addObject.Daily1 = CheckBoxes[++counter];
                            addObject.Weekly1 = CheckBoxes[counter];
                            break;

                        case 2: // me and one user
                            addObject.Daily2 = CheckBoxes[++counter];
                            addObject.Weekly2 = CheckBoxes[counter];
                            break;

                        case 3:// me and two user
                            addObject.Daily3 = CheckBoxes[++counter];
                            addObject.Weekly3 = CheckBoxes[counter];
                            break;
                    }
                }
                
                counter++;
                //for (int j = 0; j < WorkCount; j++)
                //{
                //    switch (j)
                //    {
                //        case 0:
                //            addObject.Daily1 = CheckBoxes[];
                //            break;
                //    }
                //}
                listSource.Add(addObject);
            }
            

            
            Grid_CheckList.ItemsSource = listSource;
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
                tabControl.Width += SystemParameters.PrimaryScreenWidth - 1000;
                Grid_Checklist.Width += SystemParameters.PrimaryScreenWidth - 1000;
                Grid_Checklist.Height += SystemParameters.PrimaryScreenHeight - 650;
                gridDescriptionColumn.MinWidth +=(double) SystemParameters.PrimaryScreenWidth - 1000;
            }
            else
            {
                tabControl.Width = 780;
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
    }
}
