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
    /// Interaction logic for EditRoutineWork.xaml
    /// </summary>
    public partial class EditRoutineWork : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public User User { get; set; }
        public RoutineWork RoutineWork { get; set; }
        public EditRoutineWork()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(!(dbDescription.Text.Length > 3))
            {
                result.Content = Lang.UCChecklistAddError;
                result.Background = Brushes.Red;
            }
            else
            {
                RoutineWork.work_description = dbDescription.Text;
                DB.EditRoutineWork(RoutineWork);
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            routinWorkTitle.Content = Lang.UCChecklistTitle;
            workDescription.Content = Lang.UCChecklistAddLbl;
            saveLbl.Content = Lang.ButtonSave;
            dbDescription.Text = RoutineWork.work_description;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse
                .LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                Save_Click(sender, e);
        }

        private void Req_btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
