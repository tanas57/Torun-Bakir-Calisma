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
namespace Torun.windows
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private users currentUser;
        private DB db;
        private string str_saveLogin = "username.txt";
        private string str_savePass = "password.txt";
        private bool passwordMD5 = false;
        public Welcome()
        {
            InitializeComponent();
            currentUser = new users();
            db = new DB();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
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
                            currentUser.user_name = register_username.Text;
                            currentUser.pc_name = System.Environment.MachineName;
                            currentUser.last_login = null;
                            currentUser.login_status = 0;
                            currentUser.user_status = 1;
                            currentUser.register_date = DateTime.Now;
                            switch (db.Register(currentUser))
                            {
                                case 1:
                                    break;
                                case 0:
                                    break;
                            }
                        }
                        else
                        {
                            lbl_RegResult.Content = "Şifre yetersiz";
                            lbl_RegResult.Background = Brushes.Red;
                        }
                    else
                    {
                        lbl_RegResult.Content = "Girilen şifreler uyuşmuyor";
                        lbl_RegResult.Background = Brushes.Red;
                    }
                }
                else
                {
                    lbl_RegResult.Content = "Kullanıcı adı en az 3 karakter olmalı";
                    lbl_RegResult.Background = Brushes.Red;
                }
            }
            else
            {
                lbl_RegResult.Content = "Lütfen boş alanları doldurun";
                lbl_RegResult.Background = Brushes.Red;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(chk_loginSave.IsChecked == true && passwordMD5 == true) currentUser.password = login_password.Password;
            else
                if(passwordMD5 == false)
                    currentUser.password = MD5Crypt.MD5Hash(login_password.Password);

            currentUser.user_name = login_username.Text;
            currentUser.login_status = 1;
            currentUser.last_login = DateTime.Now;
            if (currentUser.user_name.Length < 3 || currentUser.password.Length < 3)
            {
                lbl_LoginResult.Visibility = Visibility.Visible;
                lbl_LoginResult.Content = "Kullanıcı adı veya şifre yetersiz";
                lbl_LoginResult.Background = Brushes.Red;
            }
            else
            {
                switch (db.Login(currentUser))
                {
                    case 1:
                        lbl_LoginResult.Visibility = Visibility.Hidden;
                        lbl_LoginResult.Content = "Giriş başarılı";
                        lbl_LoginResult.Background = Brushes.Green;
                        if (chk_loginSave.IsChecked == true)
                        {
                            FileOperation.Write(currentUser.user_name, str_saveLogin);
                            FileOperation.Write(currentUser.password, str_savePass);
                        }
                        MainWindow mainWindow = new MainWindow();
                        this.Hide();
                        mainWindow.welcome = this;
                        mainWindow.currentUser = this.currentUser;
                        mainWindow.Show();
                        break;

                    case 2:
                        lbl_LoginResult.Visibility = Visibility.Visible;
                        lbl_LoginResult.Content = "Kullanıcı bulunamadı";
                        lbl_LoginResult.Background = Brushes.Red;
                        break;

                    case 3:
                        lbl_LoginResult.Visibility = Visibility.Visible;
                        lbl_LoginResult.Content = "Şifre yanlış";
                        lbl_LoginResult.Background = Brushes.Red;
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(FileOperation.FileExists(str_saveLogin) && FileOperation.FileExists(str_savePass))
            {
                chk_loginSave.IsChecked = true;
                login_username.Text = FileOperation.Read(str_saveLogin);
                login_password.Password = FileOperation.Read(str_savePass);
                passwordMD5 = true;
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
    }
}
