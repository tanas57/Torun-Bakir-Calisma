using System;
using System.Windows;
using System.Windows.Media;
using Torun.Database;
using Torun.Classes;
using Torun.Classes.FileOperations;
using Torun.Lang;
using System.Windows.Input;

namespace Torun.windows
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private users currentUser;
        private readonly DB db;
        private bool passwordMD5 = false;
        private readonly ILanguage language;
        public Welcome()
        {
            InitializeComponent();
            db = new DB();
            language = new TR();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            currentUser = new users();
            if(register_firstname.Text != String.Empty 
                && register_lastname.Text != String.Empty 
                && register_username.Text != String.Empty 
                && register_password.Password != String.Empty 
                && register_password2.Password != String.Empty)
            {
                currentUser.firstname = register_firstname.Text;
                currentUser.lastname = register_lastname.Text;
                if(register_username.Text.Length >= 3)
                {
                    if (String.Equals(register_password.Password, register_password2.Password) == true)
                        if (register_password.Password.Length >= 3)
                        {
                            currentUser.password = MD5Crypt.MD5Hash(register_password.Password);
                            currentUser.user_name = register_username.Text.ToLower();
                            currentUser.pc_name = System.Environment.MachineName;
                            currentUser.last_login = null;
                            currentUser.login_status = 0;
                            currentUser.user_status = 1;
                            currentUser.register_date = DateTime.Now;
                            switch (db.Register(currentUser))
                            {
                                case 1:
                                    lbl_RegResult.Content = language.WelcomeSignSuccess;
                                    lbl_RegResult.Background = Brushes.Green;
                                    break;
                                case 0:
                                    lbl_RegResult.Content = language.WelcomeSignUserAlreadyExists;
                                    lbl_RegResult.Background = Brushes.Red;
                                    break;
                            }
                        }
                        else
                        {
                            lbl_RegResult.Content = language.WelcomeSignPasswordNotEnough;
                            lbl_RegResult.Background = Brushes.Red;
                        }
                    else
                    {
                        lbl_RegResult.Content = language.WelcomeSignPasswordsNotMatch;
                        lbl_RegResult.Background = Brushes.Red;
                    }
                }
                else
                {
                    lbl_RegResult.Content = language.WelcomeSignUsernameLenghtMustBeGreaterThanThree;
                    lbl_RegResult.Background = Brushes.Red;
                }
            }
            else
            {
                lbl_RegResult.Content = language.WelcomeSignFillAllFields;
                lbl_RegResult.Background = Brushes.Red;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            currentUser = new users();
            if (chk_loginSave.IsChecked == true && passwordMD5 == true) currentUser.password = login_password.Password;
            else
                if (passwordMD5 == false)
                currentUser.password = MD5Crypt.MD5Hash(login_password.Password);
            else
                currentUser.password = login_password.Password;

            currentUser.user_name = login_username.Text.ToLower();
            if (currentUser.user_name.Length < 3 || currentUser.password.Length < 3)
            {
                lbl_LoginResult.Visibility = Visibility.Visible;
                lbl_LoginResult.Content = language.WelcomeLoginFailedNotEnoughUserOrPassword;
                lbl_LoginResult.Background = Brushes.Red;
            }
            else
            {
                switch (db.Login(currentUser))
                {
                    case 1:
                        lbl_LoginResult.Visibility = Visibility.Hidden;
                        lbl_LoginResult.Content = language.WelcomeLoginSuccess;
                        lbl_LoginResult.Background = Brushes.Green;
                        /*
                        * when an user login, a file which has a name that is current username
                        */
                        FileOperation.UserFilename = currentUser.user_name;
                        
                        FileOperation.Write(currentUser.user_name, FileNames.IS_LOGGED, true);
                        FileOperation.ControlUserFile();
                        if (chk_loginSave.IsChecked == true)
                        {
                            FileOperation.Write(currentUser.user_name, FileNames.FILENAME_USERNAME);
                            FileOperation.Write(currentUser.password, FileNames.FILENAME_PASSWORD);
                        }
                        MainWindow mainWindow = new MainWindow();
                        this.Hide();
                        mainWindow.welcome = this;
                        mainWindow.currentUser = this.currentUser;
                        mainWindow.language = language;
                        mainWindow.Show();
                        break;

                    case 2:
                        lbl_LoginResult.Visibility = Visibility.Visible;
                        lbl_LoginResult.Content = language.WelcomeLoginFailedUserNotFind;
                        lbl_LoginResult.Background = Brushes.Red;
                        break;

                    case 3:
                        lbl_LoginResult.Visibility = Visibility.Visible;
                        lbl_LoginResult.Content = language.WelcomeLoginFailedWrongPassword;
                        lbl_LoginResult.Background = Brushes.Red;
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // form items names from language
            lbl_loginTitle.Content = language.WelcomeLoginTitle;
            lbl_registerTitle.Content = language.WelcomeRegisterTitle;
            btnSignUp.Content = language.WelcomeRegisterButton;
            lbl_info_username.Content = language.WelcomeRegisterTitleUsername;
            lbl_info_password.Content = language.WelcomeRegisterTitlePassword;
            lbl_info_password2.Content = language.WelcomeRegisterTitlePasswordAgain;
            lbl_info_firstname.Content = language.WelcomeRegisterTitleFirstName;
            lbl_info_lastname.Content = language.WelcomeRegisterTitleLastName;
            chk_loginSave.Content = language.WelcomeLoginRemember;
            btnLogin.Content = language.WelcomeLoginButton;

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
    }
}
