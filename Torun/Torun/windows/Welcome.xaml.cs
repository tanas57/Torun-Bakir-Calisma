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
        users currentUser;
        private DB db;
        private string str_saveLogin = "username.txt";
        private string str_savePass = "password.txt";
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
            currentUser.firstname = register_firstname.Text;
            currentUser.lastname = register_lastname.Text;
            currentUser.user_name = register_username.Text;
            if (register_password.Password == register_password2.Password)
                if(register_password.Password.Length >= 3)
                    currentUser.password = MD5Crypt.MD5Hash(register_password.Password);
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
            if(currentUser.firstname != String.Empty && currentUser.lastname != String.Empty && currentUser.user_name.Length >= 3)
            {
                currentUser.pc_name = System.Environment.MachineName;
                currentUser.last_login = null;
                currentUser.login_status = 0;
                currentUser.user_status = 1;
                currentUser.register_date = DateTime.Now;
                db.Register(currentUser);
            }
            else
            {
                lbl_RegResult.Content = "Lütfen boş alanları doldurun";
                lbl_RegResult.Background = Brushes.Red;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            currentUser.password = MD5Crypt.MD5Hash(login_password.Password);
            currentUser.user_name = login_username.Text;
            if(currentUser.user_name.Length < 3 || currentUser.password.Length < 3)
            {
                lbl_LoginResult.Content = "Kullanıcı adı veya şifre yetersiz";
                lbl_LoginResult.Background = Brushes.Red;
            }
            else
            {
                switch (db.Login(currentUser))
                {
                    case 1:
                        lbl_LoginResult.Content = "Giriş başarılı";
                        lbl_LoginResult.Background = Brushes.Green;
                        if (chk_loginSave.IsChecked == true)
                        {
                            
                        }
                        break;

                    case 2:
                        lbl_LoginResult.Content = "Kullanıcı bulunamadı";
                        lbl_LoginResult.Background = Brushes.Red;
                        break;

                    case 3:
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
            }
        }
    }
}
