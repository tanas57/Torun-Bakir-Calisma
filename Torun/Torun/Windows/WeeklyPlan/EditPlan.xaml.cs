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

namespace Torun.Windows.WeeklyPlan
{
    /// <summary>
    /// Interaction logic for EditPlan.xaml
    /// </summary>
    public partial class EditPlan : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public DB.WeeklyPlan Plan { get; set; }
        public EditPlan()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            editRequestPriority.Items.Add(mainWindow.language.ComboboxPriorityLow);
            editRequestPriority.Items.Add(mainWindow.language.ComboboxPriorityNormal);
            editRequestPriority.Items.Add(mainWindow.language.ComboboxPriorityHigh);
            editRequestPriority.Items.Add(mainWindow.language.ComboboxPriorityUrgent);
            editRequestPriority.Items.Add(mainWindow.language.ComboboxPriorityProject);

            lbl_reqNum.Text = mainWindow.language.RequestAddRequestNumber;
            lbl_reqPriority.Text = mainWindow.language.RequestAddRequestPriority;
            lbl_description.Text = mainWindow.language.RequestAddRequestDescription;

            plan_add.Content = mainWindow.language.ButtonAdd;
            plan_remove.Content = mainWindow.language.ButtonRemove;
            plan_transfer.Content = mainWindow.language.ButtonTransfer;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
