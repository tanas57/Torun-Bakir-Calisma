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
            
        }
    }
}
