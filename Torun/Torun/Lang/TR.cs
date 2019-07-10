using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torun.Lang
{
    class TR : ILanguage
    {
        /* 
         * WELCOME PAGE STARTS
         */

        /* LOGIN */
        public string WelcomeLoginTitle { get => "Giriş"; set => throw new NotImplementedException(); }
        public string WelcomeLoginFailedNotEnoughUserOrPassword { get => "Kullanıcı adı veya şifre yanlış"; set => throw new NotImplementedException(); }
        public string WelcomeLoginFailedUserNotFind { get => "Kullanıcı bulunamadı"; set => throw new NotImplementedException(); }
        public string WelcomeLoginFailedWrongPassword { get => "Şifre yanlış"; set => throw new NotImplementedException(); }
        public string WelcomeLoginSuccess { get => "Giriş başarılı"; set => throw new NotImplementedException(); }
        public string WelcomeLoginRemember { get => "Girişi hatırla"; set => throw new NotImplementedException(); }
        public string WelcomeLoginButton { get => "Giriş yap"; set => throw new NotImplementedException(); }
        /* REGISTER */
        public string WelcomeRegisterTitle { get => "Yeni üyelik"; set => throw new NotImplementedException(); }
        public string WelcomeSignSuccess { get => "Kayıt başarılı, giriş yapabilirsiniz"; set => throw new NotImplementedException(); }
        public string WelcomeSignUserAlreadyExists { get => "Kullanıcı adı sistemde mevcut"; set => throw new NotImplementedException(); }
        public string WelcomeSignPasswordNotEnough { get => "Şifre yetersiz"; set => throw new NotImplementedException(); }
        public string WelcomeSignPasswordsNotMatch { get => "Şifreler eşleşmiyor"; set => throw new NotImplementedException(); }
        public string WelcomeSignUsernameLenghtMustBeGreaterThanThree { get => "Kullanıcı adı en az 3 karakter olmalı"; set => throw new NotImplementedException(); }
        public string WelcomeSignFillAllFields { get => "Boş alanları doldurun"; set => throw new NotImplementedException(); }
        public string WelcomeRegisterButton { get => "Üye ol"; set => throw new NotImplementedException(); }
        public string WelcomeRegisterTitleUsername { get => "kullanıcı adı"; set => throw new NotImplementedException(); }
        public string WelcomeRegisterTitlePassword { get => "şifre"; set => throw new NotImplementedException(); }
        public string WelcomeRegisterTitlePasswordAgain { get => "şifre tekrarı"; set => throw new NotImplementedException(); }
        public string WelcomeRegisterTitleFirstName { get => "kullanıcı ismi"; set => throw new NotImplementedException(); }
        public string WelcomeRegisterTitleLastName { get => "kullanıcı soyismi"; set => throw new NotImplementedException(); }
        /* REGISTER */
        /* 
         * WELCOME PAGE ENDS
         */
    }
}
