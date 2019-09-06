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
using Torun.Lang;

namespace Torun.Windows.CheckList
{
    /// <summary>
    /// Interaction logic for AddNewRoutineWork.xaml
    /// </summary>
    public partial class AddNewRoutineWork : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public User User { get; set; }
        public AddNewRoutineWork()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Save_Click(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            routinWorkTitle.Content = Lang.UCChecklistAddNewPage;
            workDescription.Content = Lang.UCChecklistAddLbl;
            saveLbl.Content = Lang.ButtonAdd;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse
                .LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void Req_btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!(dbDescription.Text.Length > 3))
            {
                result.Content = Lang.UCChecklistAddError;
                result.Background = Brushes.Red;
            }
            else
            {
                if (DB.AddRoutineWork(new RoutineWork() { user_id = User.id, work_description = dbDescription.Text }) > 0)
                {
                    this.Close();
                    mainWindow.UCCheckList.ReloadCheckList();
                    mainWindow.UCCheckList.IsAdded = true;
                }
            }
        }
    }
}
