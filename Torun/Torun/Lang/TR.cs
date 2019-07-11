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
        public string WelcomeLoginTitle { get => "Giriş"; }
        public string WelcomeLoginFailedNotEnoughUserOrPassword { get => "Kullanıcı adı veya şifre yanlış"; }
        public string WelcomeLoginFailedUserNotFind { get => "Kullanıcı bulunamadı"; }
        public string WelcomeLoginFailedWrongPassword { get => "Şifre yanlış"; }
        public string WelcomeLoginSuccess { get => "Giriş başarılı"; }
        public string WelcomeLoginRemember { get => "Beni hatırla"; }
        public string WelcomeLoginButton { get => "Giriş yap"; }
        /* REGISTER */
        public string WelcomeRegisterTitle { get => "Yeni üyelik"; }
        public string WelcomeSignSuccess { get => "Kayıt başarılı, giriş yapabilirsiniz"; }
        public string WelcomeSignUserAlreadyExists { get => "Kullanıcı adı sistemde mevcut"; }
        public string WelcomeSignPasswordNotEnough { get => "Şifre yetersiz"; }
        public string WelcomeSignPasswordsNotMatch { get => "Şifreler eşleşmiyor"; }
        public string WelcomeSignUsernameLenghtMustBeGreaterThanThree { get => "Kullanıcı adı en az 3 karakter olmalı"; }
        public string WelcomeSignFillAllFields { get => "Boş alanları doldurun"; }
        public string WelcomeRegisterButton { get => "Üye ol"; }
        public string WelcomeRegisterTitleUsername { get => "kullanıcı adı"; }
        public string WelcomeRegisterTitlePassword { get => "şifre"; }
        public string WelcomeRegisterTitlePasswordAgain { get => "şifre tekrarı"; }
        public string WelcomeRegisterTitleFirstName { get => "kullanıcı ismi"; }
        public string WelcomeRegisterTitleLastName { get => "kullanıcı soyismi"; }
        public string ComboboxPriorityLow { get => "Düşük"; }
        public string ComboboxPriorityNormal { get => "Normal"; }
        public string ComboboxPriorityHigh { get => "Yüksek"; }
        public string ComboboxStatusNew { get => "Yeni eklendi"; }
        public string ComboboxStatusInProcess { get => "İşlemde"; }
        public string ComboboxStatusClosed { get => "Kapatıldı"; }

        public string RequestAddRequestNumber => "Talep numarası";

        public string RequestAddRequestPriority => "Öncelik";

        public string RequestAddRequestDescription => "Açıklama";

        public string RequestAddRequestButton => "Talep Ekle";

        public string RequestAddRequestResultOk => "Talep başarıyla eklendi";

        public string RequestAddRequestResultNo => "Talep numarası sistemde kayıtlı";

        public string RequestAddRequestResultNotSelected => "Öncelik seçilmedi";

        public string RequestAddRequestResultNoDescription => "Açıklama girilmedi";

        public string RequestAddRequestTitle => "Yeni Talep Ekle";
        /* REGISTER */
        /* 
         * WELCOME PAGE ENDS
         */
    }
}
