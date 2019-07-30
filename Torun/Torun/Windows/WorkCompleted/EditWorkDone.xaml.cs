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
using Torun.Classes;
using Torun.Lang;

namespace Torun.Windows.WorkCompleted
{
    /// <summary>
    /// Interaction logic for EditWorkDone.xaml
    /// </summary>
    public partial class EditWorkDone : Window
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private TodoList todoList; // current request
        private List<WorkDone> works; // current plans
        public List<DateTime> SelectedDates { get; set; }
        public DB.WorkDoneList Work { get; set; }
        private ILanguage Lang { get; set; }
        private DB DB { get; set; }
        public EditWorkDone()
        {
            InitializeComponent();
            DB = mainWindow.DB;
            Lang = mainWindow.Lang;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = Lang.WorkDoneEditTitle;
            workDoneDetail_title.Content = Lang.WorkDoneEditTitle;
            lbl_reqDescription.Content = Lang.RequestAddRequestDescription;
            lbl_reqPriority.Content = Lang.RequestAddRequestPriority;
            lbl_reqNumber.Content = Lang.RequestAddRequestNumber;

            groupPlan.Header = Lang.WorkDoneDetailGroupPlanAndWorkDone;
            groupRequest.Header = Lang.RequestAddRequestNumber;
            getDetailDescription.Text = Lang.WorkDoneEditChoosenWorkDescription;

            workDone_remove.Content = Lang.ButtonRemove;
            workDone_transfer.Content = Lang.ButtonTransfer;
            request_save.Content = Lang.WorkDoneEditRequestSaveButton;

            getDetailCalendarOK.Text = Lang.WorkDoneEditCompletedWorks;
            listWorkdoneLbl.Text = Lang.WorkDoneEditWorkLabel;
            workDoneDescriptionSave.Content = Lang.WorkDoneEditChoosenWorkDescription;

            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityLow);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityNormal);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityHigh);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityUrgent);
            req_Priority.Items.Add(mainWindow.Lang.ComboboxPriorityProject);

            todoList = DB.GetTodoByID(Work.WorkID);
            req_Number.Text = todoList.request_number;
            req_Priority.SelectedIndex = todoList.priority;
            req_Description.Text = todoList.description;

            WorkDoneListUpdate();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Opacity = 1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WorkDone_remove_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void WorkDoneListUpdate()
        {
            list_workdone.Items.Clear();
            works = mainWindow.DB.GetWorkdoneByID(Work.WorkID);
            for (int i = 0; i < works.Count; i++)
            {
                WorkDone temp = works[i];
                list_workdone.Items.Add(temp.workDoneTime.ToShortDateString() + " - " + temp.id);
            }
        }
        private void WorkDone_transfer_Click(object sender, RoutedEventArgs e)
        {
            EditWorkDone editWorkDone = new EditWorkDone();
            editWorkDone.Owner = this;
            editWorkDone.mainWindow = mainWindow;
            this.Opacity = 0.5;
            if (editWorkDone.ShowDialog() == false)
            {
                if (SelectedDates != null && SelectedDates.Count > 0)
                {
                    foreach (var item in SelectedDates)
                    {
                        if (!mainWindow.DB.IsPlanExists(item.Date, todoList.id)) // for the choosen date is found in database, must not add again
                        {
                            Plan plan = new Plan();
                            plan.add_time = DateTime.Now; plan.work_id = todoList.id;
                            plan.work_plan_time = item.Date; plan.status = 0;
                            mainWindow.DB.AddPlanDates(plan);
                        }
                    }
                    //result.Content = mainWindow.Lang.WeeklyEditPlanCalendarAddDates;
                    //result.Background = System.Windows.Media.Brushes.Green;

                    WorkDoneListUpdate();
                }
            }
        }

        private void WorkDoneDescriptionSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Lbl_reqDescription_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(todoList.description, mainWindow.Lang.UCTodoListInfoMessage, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
