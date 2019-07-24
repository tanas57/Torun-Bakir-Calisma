using System;
using System.Windows;
using System.Windows.Media;
using Torun.Database;
using Torun.Classes;
using Torun.Classes.FileOperations;
using Torun.Classes.Keyboard;
using Torun.Lang;
using System.Windows.Input;

namespace Torun.windows
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private bool passwordMD5 = false;
        private ILanguage Lang { get; set; }
        private users User { get; set; }
        public DB DB { get; set; }
        public Welcome()
        {
            InitializeComponent();
            DB = new DB();
            Lang = CurrentLanguage.Language;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            User = new users();
            if(register_firstname.Text != String.Empty 
                && register_lastname.Text != String.Empty 
                && register_username.Text != String.Empty 
                && register_password.Password != String.Empty 
                && register_password2.Password != String.Empty)
            {
                User.firstname = register_firstname.Text.ToUpper();
                User.lastname = register_lastname.Text.ToUpper();
                if(register_username.Text.Length >= 3)
                {
                    if (String.Equals(register_password.Password, register_password2.Password) == true)
                        if (register_password.Password.Length >= 3)
                        {
                            User.password = MD5Crypt.MD5Hash(register_password.Password);
                            User.user_name = register_username.Text.ToLower();
                            User.pc_name = System.Environment.MachineName;
                            User.last_login = null;
                            User.login_status = 0;
                            User.user_status = 1;
                            User.register_date = DateTime.Now;
                            switch (DB.Register(User))
                            {
                                case 1:
                                    lbl_RegResult.Content = Lang.WelcomeSignSuccess;
                                    lbl_RegResult.Background = Brushes.Green;
                                    register_firstname.Text = ""; register_lastname.Text = "";
                                    register_username.Text = ""; register_password.Password = ""; register_password2.Password = "";
                                    break;
                                case 0:
                                    lbl_RegResult.Content = Lang.WelcomeSignUserAlreadyExists;
                                    lbl_RegResult.Background = Brushes.Red;
                                    break;
                            }
                        }
                        else
                        {
                            lbl_RegResult.Content = Lang.WelcomeSignPasswordNotEnough;
                            lbl_RegResult.Background = Brushes.Red;
                        }
                    else
                    {
                        lbl_RegResult.Content = Lang.WelcomeSignPasswordsNotMatch;
                        lbl_RegResult.Background = Brushes.Red;
                    }
                }
                else
                {
                    lbl_RegResult.Content = Lang.WelcomeSignUsernameLenghtMustBeGreaterThanThree;
                    lbl_RegResult.Background = Brushes.Red;
                }
            }
            else
            {
                lbl_RegResult.Content = Lang.WelcomeSignFillAllFields;
                lbl_RegResult.Background = Brushes.Red;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            User = new users();
            if (chk_loginSave.IsChecked == true && passwordMD5 == true) User.password = login_password.Password;
            else
                if (passwordMD5 == false)
                User.password = MD5Crypt.MD5Hash(login_password.Password);
            else
                User.password = login_password.Password;

            User.user_name = login_username.Text.ToLower();
            if (User.user_name.Length < 3 || User.password.Length < 3)
            {
                lbl_LoginResult.Visibility = Visibility.Visible;
                lbl_LoginResult.Content = Lang.WelcomeLoginFailedNotEnoughUserOrPassword;
                lbl_LoginResult.Background = Brushes.Red;
            }
            else
            {
                switch (DB.Login(User))
                {
                    case 1:
                        lbl_LoginResult.Visibility = Visibility.Hidden;
                        lbl_LoginResult.Content = Lang.WelcomeLoginSuccess;
                        lbl_LoginResult.Background = Brushes.Green;
                        /*
                        * when an user login, a file which has a name that is current username
                        */
                        FileOperation.UserFilename = User.user_name;
                        
                        FileOperation.Write(User.user_name, FileNames.IS_LOGGED, true);
                        FileOperation.ControlUserFile();
                        if (chk_loginSave.IsChecked == true)
                        {
                            FileOperation.Write(User.user_name, FileNames.FILENAME_USERNAME);
                            FileOperation.Write(User.password, FileNames.FILENAME_PASSWORD);
                        }
                        MainWindow mainWindow = new MainWindow();
                        this.Hide();
                        mainWindow.welcome = this;

                        mainWindow.User = this.User;
                        mainWindow.Lang = Lang;
                        mainWindow.DB = this.DB;

                        mainWindow.Show();
                        break;

                    case 2:
                        lbl_LoginResult.Visibility = Visibility.Visible;
                        lbl_LoginResult.Content = Lang.WelcomeLoginFailedUserNotFind;
                        lbl_LoginResult.Background = Brushes.Red;
                        break;

                    case 3:
                        lbl_LoginResult.Visibility = Visibility.Visible;
                        lbl_LoginResult.Content = Lang.WelcomeLoginFailedWrongPassword;
                        lbl_LoginResult.Background = Brushes.Red;
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CapsLockControl();
            capslockControl.Content = Lang.WelcomeCapsLock;
            // form items names from Lang
            lbl_loginTitle.Content = Lang.WelcomeLoginTitle;
            lbl_registerTitle.Content = Lang.WelcomeRegisterTitle;
            btnSignUp.Content = Lang.WelcomeRegisterButton;
            lbl_info_username.Content = Lang.WelcomeRegisterTitleUsername;
            lbl_info_password.Content = Lang.WelcomeRegisterTitlePassword;
            lbl_info_password2.Content = Lang.WelcomeRegisterTitlePasswordAgain;
            lbl_info_firstname.Content = Lang.WelcomeRegisterTitleFirstName;
            lbl_info_lastname.Content = Lang.WelcomeRegisterTitleLastName;
            chk_loginSave.Content = Lang.WelcomeLoginRemember;
            btnLogin.Content = Lang.WelcomeLoginButton;

            if (FileOperation.FileExists(FileNames.IS_LOGGED))
            {
                FileOperation.UserFilename = FileOperation.Read(FileNames.IS_LOGGED);
                if (FileOperation.FileExists(FileNames.FILENAME_USERNAME) && FileOperation.FileExists(FileNames.FILENAME_PASSWORD))
                {
                    chk_loginSave.IsChecked = true;
                    login_username.Text = FileOperation.Read(FileNames.FILENAME_USERNAME);
                    login_password.Password = FileOperation.Read(FileNames.FILENAME_PASSWORD);
                    passwordMD5 = true;
                }
            }
        }

        private void Login_password_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //passwordMD5 = false;
        }

        private void Login_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passwordMD5 = false;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CapsLockControl();
        }

        private void CapsLockControl()
        {
            if (KeyControl.CapsLock()) capslockControl.Visibility = Visibility.Visible;
            else capslockControl.Visibility = Visibility.Hidden;
        }
        private void StackPanel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) BtnLogin_Click(sender, e);
        }

        private void Login_username_KeyUp(object sender, KeyEventArgs e)
        {
            if (login_username.Text == String.Empty) login_password.Password = String.Empty;
        }
    }
}
