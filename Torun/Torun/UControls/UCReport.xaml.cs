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
    /// Interaction logic for UCReport.xaml
    /// </summary>
    public partial class UCReport : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public UCReport()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeOnlyPlan);
            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeOnlyWorkDone);
            planWorkdoneSelect.Items.Add(Lang.ReportComboTypeBothofThem);

            timeIntervalSelect.Items.Add(Lang.SettingsRadioDaily);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioWeekly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioMonthly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioYearly);
            timeIntervalSelect.Items.Add(Lang.SettingsRadioBeforeStart);


        }
    }
}
