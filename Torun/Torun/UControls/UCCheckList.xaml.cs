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
        public UCCheckList()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
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
            Grid_Checklist.ItemsSource = DB.GetRoutineWorks(User);
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
    }
}
