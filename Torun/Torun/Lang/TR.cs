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
        #region
        public string WelcomeLoginTitle { get => "Giriş"; }
        public string WelcomeLoginFailedNotEnoughUserOrPassword { get => "Kullanıcı adı veya şifre yanlış"; }
        public string WelcomeLoginFailedUserNotFind { get => "Kullanıcı bulunamadı"; }
        public string WelcomeLoginFailedWrongPassword { get => "Şifre yanlış"; }
        public string WelcomeLoginSuccess { get => "Giriş başarılı"; }
        public string WelcomeLoginRemember { get => "Beni hatırla"; }
        public string WelcomeLoginButton { get => "Giriş yap"; }
        #endregion
        /* REGISTER */
        #region
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
        public string WelcomeCapsLock => "Caps Lock Açık";
        #endregion
        /* REGISTER */
        /*   WELCOME PAGE ENDS */
        // COMBOBOX VALUES
        #region
        public string ComboboxPriorityLow { get => "Düşük"; }
        public string ComboboxPriorityNormal { get => "Normal"; }
        public string ComboboxPriorityHigh { get => "Yüksek"; }
        public string ComboboxPriorityUrgent => "Acil";
        public string ComboboxPriorityProject => "Proje";
        public string ComboboxStatusNew { get => "Yeni eklendi"; }
        public string ComboboxStatusInProcess { get => "İşlemde"; }
        public string ComboboxStatusClosed { get => "Kapatıldı"; }
        public string ComboboxStatusEdited => "Düzenlendi";
        public string ComboboxStatusPlanned => "Planlandı";
        #endregion
        /* REQUEST PAGES */
        // REQUEST ADD PAGE 
        #region

        public string RequestAddRequestNumber => "Talep numarası";

        public string RequestAddRequestPriority => "Öncelik";

        public string RequestAddRequestDescription => "Açıklama";

        public string RequestAddRequestButton => "Talep Ekle";

        public string RequestAddRequestResultOk => "Talep başarıyla eklendi";

        public string RequestAddRequestResultNo => "Talep numarası sistemde kayıtlı";

        public string RequestAddRequestResultNotSelected => "Öncelik seçilmedi";

        public string RequestAddRequestResultNoDescription => "Açıklama girilmedi";

        public string RequestAddRequestTitle => "Yeni Talep Ekle";

        public string RequestAddReqNumEmpty => "Talep numarası boş olamaz";
        #endregion
        /* REQUEST EDIT WINDOWS ENDS */
        #region
        public string RequestEditTitle => "Talep Düzenleme";

        public string RequestEditButton => "Kaydet";

        public string RequestEditLabelSaveOK => "Talep düzenlemesi kaydedildi";

        public string RequestEditLabelSaveNO => "Talep düzenlemesi yapılamadı";
        #endregion
        // MAINPAGE STARTS
        #region
        public string MainPageTitle => "Torun - Plan Tracer";

        public string MainPageTotalRequest => "Toplam kayıtlı talep sayısı : ";

        public string MainPageOpenRequest => "Açık talep : ";

        public string MainPageClosedRequest => "Kapatılmış talep : ";

        public string MainPageLogOut => "Çıkış yap";

        public string MainPageMenuToDo => "Yapılacaklar";

        public string MainPageMenuWeeklyPlan => "Haftalık Plan";

        public string MainPageWorkDone => "Yapılmış İşler";

        public string MainPageMenuReport => "Rapor";

        public string MainPageMenuBackup => "Yedekleme";
        // MAIN PAGE ENDS
        #endregion
        // REQUEST SCHEDULE PAGE STARTS
        #region
        public string RequestScheduleTitle => "Talep Zamanlama";
        public string RequestScheduleReqNumber => "Talep Numarası";
        public string RequestScheduleChooseDate => "Tarih seçiniz";
        public string RequestScheduleSave => "Planla";
        public string RequestScheduleSaveChooseDateError => "Takvimden tarih seçiniz";
        public string RequestScheduleSaveFailed => "İşlem başarısız";

        #endregion
        /* REQUEST PAGES */
        // Todolist user control
        #region
        public string UCTodoListAddRequest => "Yeni talep ekle";
        public string UCTodoListDetailList => "Liste Detayını Aç/Kapa";
        public string UCTodoListProcesses => "İşlemler";
        public string UCTodoListRequestNumber => "Talep Numarası";
        public string UCTodoListPriority => "Öncelik";
        public string UCTodoListDescription => "Açıklama";
        public string UCTodoListAddedTime => "Eklenme Zamanı";
        public string UCTodoListStatus => "Durum";
        public string UCTodoListScheduleButton => "Zamanla";
        public string UCTodoListEditButton => "Düzenle";
        public string UCTodoListDeleteButton => "Sil";
        public string UCTodoListInfoMessage => "Açıklama detayı";

        #endregion
        // Weekly plan page
        #region
        public string UCWeeklyPlanButtonGetDetail => "Detay Getir";

        public string UCWeeklyPlanButtonDoCompleted => "Tamamlandı İşaretle";

        public string UCWeeklyPlanButtonEdit => "Düzenle";

        public string UCWeeklyPlanButtonRemove => "Plandan Çıkar";

        public string UCWeeklyPlanButtonChangeDate => "Zaman Dilimi Değiştir";

        public string UCWeeklyPlanDaysMonday => "Pazartesi";

        public string UCWeeklyPlanDaysTuesday => "Salı";

        public string UCWeeklyPlanDaysWednesday => "Çarşamba";

        public string UCWeeklyPlanDaysThursday => "Perşembe";

        public string UCWeeklyPlanDaysFriday => "Cuma";

        public string UCWeeklyPlanCurrentTime => "Geçerli Zaman Dilimi";
        public string UCWeeklyPlanNumOfPlans => "adet kayıt";
        #endregion
        // Wekkly detail
        #region
        public string WeeklyDetailTitle => "Talep Detay Görüntüleme : ";

        public string WeeklyDetailDescription => "Açıklama";

        public string WeeklyDetailCalendar => "Plan Tarihleri";
        #endregion
        // weekly completed
        #region
        public string WeeklyCompletedTitle => "Talep Tamamlama Sayfası";

        public string WeeklyCompletedCurrentDay => "Bugünkü iş bitti işaretle";

        public string WeeklyCompletedAllWork => "Tüm talebi tamamlandı işaretle";
        public string WeeklyCompletedNote => "Notunuz (varsa)";
        #endregion
        // buttons
        #region
        public string ButtonSave => "Kaydet";

        public string ButtonDelete => "Sil";

        public string ButtonEdit => "Düzenle";
        public string ButtonRemove => "Kaldır";
        public string ButtonTransfer => "Taşı";
        public string ButtonAdd => "Ekle";

        #endregion
        // wekkly remove
        #region
        public string WeeklyRemoveTitle => "Plandan Çıkarma";
        public string WeeklyRemoveAday => "Sadece seçili iş";
        public string WeeklyRemoveAllDays => "Tüm talep işlerini çıkar(talep tamamen silinir)";
        public string WeeklyRemoveAllDaysExceptDoit => "Tüm talep işleri çıkar (tamamlananlar silinmez)";
        public string WeeklyRemoveButtonRemove => "Plandan çıkar";
        public string WeeklyEditPlanTitle => "Plan Düzenleme Sayfası";
        public string WeeklyEditPlanRequestInfo => "Talep Bilgileri";
        public string WeeklyEditPlanInfo => "Plan Bilgileri";
        public string WeeklyEditPlanNotSelectPlan => "Herhangi bir plan seçilmedi";
        public string WeeklyEditPlanRemoved => "Seçilen plan kaldırıldı";

        public string WeeklyEditPlanRemovePlanTransferTodoList => "Seçilen plan silindi, talep yeniden yapılacaklar listesine taşındı";

        public string WeeklyEditPlanCalendarAddTitle => "Planı Güne Ekleme";

        public string WeeklyEditPlanCalendarAddLabel => "Bir adet gün seçin";

        public string WeeklyEditPlanCalendarAddDates => "Seçilen tarihler talebe eklendi";


        #endregion
    }
}
