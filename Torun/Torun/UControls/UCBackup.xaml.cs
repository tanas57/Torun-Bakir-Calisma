using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Torun.Classes.FileOperations;
using Torun.Database;
using Torun.Lang;

namespace Torun.UControls
{
    /// <summary>
    /// Interaction logic for UCBackup.xaml
    /// </summary>
    public partial class UCBackup : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        public ILanguage Lang { get; set; }
        public DB DB { get; set; }
        public User User { get; set; }
        private SQLDBBackup SqlBackup { get; set; }
        private List<Backup> Backups { get; set; }
        public UCBackup()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
            SqlBackup = new SQLDBBackup();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            backupPaths.Content = Lang.BackupChangePathLabel;
            backupListLbl.Content = Lang.BackupListLblTitle;
            btnBackup.Content = Lang.BackupDoit;
            btnRestore.Content = Lang.BackupDoRestore;

            FillListBox();
        }
        private void FillListBox()
        {
            Backups = DB.GetBackups(User, true);

            backupList.Items.Clear();
            foreach (var item in Backups)
            {
                backupList.Items.Add(item.filename + " / " + item.id);
            }
        }
        private void BtnBackup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();
                string currentBackupFileName = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-" + FileNames.FILENAME_BACKUP + "-" + random.Next(1000, 9999) + ".bak";

                string path = FileNames.BACKUP_FILE_PATH + @"\" + currentBackupFileName;

                SqlBackup.Backup(path);
                Backup backup = new Backup()
                {
                    filename = currentBackupFileName,
                    filepath = path,
                    user_id = User.id
                };
                int id = DB.AddBackup(backup);
                if (id > 0)
                {
                    backupList.Items.Add(currentBackupFileName + " / " + id.ToString());

                    result.Background = Brushes.Green;
                    result.Content = Lang.BackupSuccessfully;
                    FillListBox();
                }
                else
                {
                    result.Background = Brushes.Red;
                    result.Content = Lang.BackupFailed;
                }
            }
            catch(Exception ex)
            {
                DB.AddLog(new Log() { error_page = "backup_buttonbackup", error_text = ex.Message,
                    log_date = DateTime.Now, log_user = User.id });
            }
        }

        private void BtnRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (backupList.SelectedIndex == -1)
                {
                    result.Background = Brushes.Red;
                    result.Content = Lang.BackupSelectListBox;
                }
                else
                {
                    string filename = backupList.SelectedItem.ToString();
                    string[] arr = filename.Split('/');
                    int id = int.Parse(arr[1]);
                    Backup selectedBackup = DB.GetBackup(User, id);

                    result.Background = Brushes.Green;
                    result.Content = Lang.BackupRestoreSuccessfully;

                    Backups = DB.GetBackups(User);

                    SqlBackup.Restore(selectedBackup.filepath); // restore current selected backup
                    DB.AddBackupRange(Backups); // the backup table must not be deleted
                    mainWindow.Menu_userLogout_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                DB.AddLog(new Log()
                {
                    error_page = "backup_buttonbackup",
                    error_text = ex.Message,
                    log_date = DateTime.Now,
                    log_user = User.id
                });
            }
}
    }
}
