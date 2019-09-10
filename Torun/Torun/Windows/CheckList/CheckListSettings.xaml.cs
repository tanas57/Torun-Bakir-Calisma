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
    /// Interaction logic for CheckListSettings.xaml
    /// </summary>
    public partial class CheckListSettings : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private ILanguage Lang { get; set; }
        private DB DB { get; set; }
        public User User { get; set; }
        private Setting setting;
        public CheckListSettings()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = Lang.UCCheckListSettingTitle;
            workDescription.Content = Lang.UCCheckListLabel;
            save.Content = Lang.ButtonSave;
            setting = DB.GetUserSettings(User);
            title1.Text = setting.routineWorkTitle1;
            title2.Text = setting.routineWorkTitle2;
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

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Save_Click(sender, e);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            setting.routineWorkTitle1 = title1.Text;
            setting.routineWorkTitle2 = title2.Text;
            DB.UpdateUserSettings(User, setting);
        }
    }
}
