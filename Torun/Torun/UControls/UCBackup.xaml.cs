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
using Torun.Classes;
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
        private int BackupCount { get; set; }
        private SQLDBBackup SqlBackup { get; set; }
        public UCBackup()
        {
            InitializeComponent();
            Lang = mainWindow.Lang;
            DB = mainWindow.DB;
            User = mainWindow.User;
            BackupCount = 1;
            SqlBackup = new SQLDBBackup();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            backupPaths.Content = Lang.BackupChangePathLabel;
            backupListLbl.Content = Lang.BackupListLblTitle;
            backupPath.Content = Lang.BackupChangePath;
            backupPath2.Content = Lang.BackupChangePath2;
            changePath.Content = Lang.ButtonEdit;
            changePath2.Content = Lang.ButtonEdit;
            btnBackup.Content = Lang.BackupDoit;
            btnRestore.Content = Lang.BackupDoRestore;
        }

        private void BtnBackup_Click(object sender, RoutedEventArgs e)
        {
            string currentBackupFileName = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-" + FileNames.FILENAME_BACKUP + "-" + BackupCount + ".bak";
            //FileOperation.BackupFolderProcess() + 
            string path = FileNames.BACKUP_FILE_PATH + @"\" + currentBackupFileName;
            MessageBox.Show(path);
            SqlBackup.Backup(path);
            backupList.Items.Add(currentBackupFileName);
            BackupCount++;
        }
    }
}
