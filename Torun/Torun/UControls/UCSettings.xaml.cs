using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Torun.Database;
using Torun.Classes;
using Torun.Lang;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCSettings.xaml
    /// </summary>
    public partial class UCSettings : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public UCSettings()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            groupMainCount.Header = Lang.SettingsGroupMainCount;
            radioDaily.Content = Lang.SettingsRadioDaily;
            radioWeekly.Content = Lang.SettingsRadioWeekly;
            radioMonthly.Content = Lang.SettingsRadioMonthly;
            radioBeforeStart.Content = Lang.SettingsRadioBeforeStart;
        }
    }
}
