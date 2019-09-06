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
        public string WelcomeLoginFailedWrongPassword { get => "Şifre yanlış girildi"; }
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
        public string RequestAddReqNumEmpty => "Talep numarası boş olamaz"; public string RequestAddDoTimed => "Talep zamanlayarak kaydedilsin mi ?";
        public string RequestAddWorkDone => "Talebi tamamlandı olarak kaydet ?";
        public string RequestAddCompletedTimeSelect => "İş Tamamlanma Zamanı Seçin";
        public string RequestAddDoScheduled => "İş Zamanını Seçiniz";
        public string RequestAddFinishDate => "Bugün bitirildi işaretleyin ya da tarih seçin";
        public string RequestAddScheduleDate => "Talep zamanlanıcak ise takvimden tarih seçin";
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
        public string MainPageMenuSettings => "Ayarlar";
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
        public string UCWeeklyPlanOpenDetail => "Detaylı Görünüme Geç";
        public string UCWeeklyPlanCloseDetail => "Haftalık Görünüme Geç";
        public string WeeklyPlanDetailPlanDate => "Plandaki Tarih";
        #endregion
        // Wekkly detail
        #region
        public string WeeklyDetailTitle => "Talep Detay Görüntüleme : ";

        public string WeeklyDetailDescription => "Açıklama";

        public string WeeklyDetailCalendar => "Planlanan";
        public string WeeklyDetailCalendarOK => "Gerçekleşen";
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
        public string ButtonGet => "Getir";

        #endregion
        // wekkly remove
        #region
        public string WeeklyRemoveTitle => "Plandan Çıkarma";
        public string WeeklyRemoveAday => "Sadece seçili iş";
        public string WeeklyRemoveAllDays => "Tüm talep işlerini çıkar(talep yapılcaklar listesine gider)";
        public string WeeklyRemoveButtonRemove => "Plandan çıkar";
        #endregion
        // weekly plan edit page
        #region
        public string WeeklyEditPlanTitle => "Plan Düzenleme Sayfası";
        public string WeeklyEditPlanRequestInfo => "Talep Bilgileri";
        public string WeeklyEditPlanInfo => "Plan Bilgileri";
        public string WeeklyEditPlanNotSelectPlan => "Herhangi bir plan seçilmedi";
        public string WeeklyEditPlanRemoved => "Seçilen plan kaldırıldı";

        public string WeeklyEditPlanRemovePlanTransferTodoList => "Seçilen plan silindi, talep yeniden yapılacaklar listesine taşındı";

        public string WeeklyEditPlanCalendarAddTitle => "Planı Güne Ekleme";

        public string WeeklyEditPlanCalendarAddLabel => "Gün(leri) seçin";

        public string WeeklyEditPlanCalendarAddDates => "Seçilen tarihler talebe eklendi";

        public string WeeklyEditPlanRemoveWorkdoneError => "Seçili plan iş tamamlandığından dolayı silinemiyor." + MainPageWorkDone + " bölümünden silinebilir";

        public string WeeklyEditPlanTransferTitle => "Talep Tarih Tranfer";

        public string WeeklyEditPlanCalendarAddADates => "Yeni tarihi seçiniz";

        public string WeeklyEditPlanTransferError => "Seçili plan iş tamamlandığından dolayı transfer edilemiyor." + MainPageWorkDone + " bölümünden düzenlenebilir";

        public string WeeklyEditPlanTransfered => "Seçili planın tarihi güncellenmiştir";
        #endregion
        // weekly plan sort buttons
        #region
        public string RequestAddToWorkDone => "Talebi bugün tamamlandı olarak ekle";

        public string WeeklyPlanSortLbl => "Sıralama Yöntemi : ";

        public string WeeklyPlanSortAddTime => "Eklenme Zamanı(Artan)";

        public string WeeklyPlanSortAddTimeDesc => "Eklenme Tarihi(Azalan)";

        public string WeeklyPlanSortNameDesc => "Talep Adı(Azalan)";

        public string WeeklyPlanSortNameAsc => "Talep Adı(Artan)";

        public string WeeklyPlanSortPriorityAsc => "Öncelik(Artan)";

        public string WeeklyPlanSortPriorityDesc => "Öncelik(Azalan)";
        #endregion
        // work done remove page
        #region
        public string WorkDoneRemoveTitle => "İşi Tamamlanmadı İşaretle";

        public string WorkDoneRemoveSelectADay => "Seçilen işi yapılmadı olarak işaretle";

        public string WorkDoneRemoveSelectAllDays => "Tüm talebi yapılmadı olarak işaretle";

        public string UCWorkDoneRemoveWorkDone => "Tamamlanmadı İşaretle";

        public string WorkDoneDetailTitle => "Tamamlanan İş Detayı";
        public string WorkDoneTime => "Tamamlanma Zamanı";

        public string WorkDoneDetailGroupPlanAndWorkDone => "Planlanan ve Tamamlanan Bilgisi";

        public string WorkDoneDetailDescription => "Planlanan ve Tamamlanan Kayıtların Açıklamaları";

        public string WorkDoneEditTitle => "Tamamlanan İş Düzenleme";

        public string WorkDoneEditCompletedWorks => "Tamamlanan işler";

        public string WorkDoneEditChoosenWorkDescription => "Tüm İş Açıklamaları(Listeden tek tek seçebilir ve düzenlenebilir)";

        public string WorkDoneEditWorkLabel => "Bir adet iş seçip, ardından işlemi seçin";

        public string WorkDoneEditRequestSaveButton => "Talep Bilgisini Kaydet";

        public string WorkDoneEditWorkNotSelected => "İşlem için herhangi bir iş seçilmeli";

        public string WorkDoneEditFor => "için";

        public string WorkDoneEditWorkDescriptionUpdate => "Seçilen iş açıklaması güncellendi";

        #endregion
        // Settings page
        #region
        public string SettingsGroupMainCount => "Ana Ekran Talep Sayaç Ayarı";
        public string SettingsRadioDaily => "Günlük";
        public string SettingsRadioWeekly => "Haftalık";
        public string SettingsRadioMonthly => "Aylık";
        public string SettingsRadioYearly => "Yıllık";
        public string SettingsRadioBeforeStart => "En baştan beri";
        public string SettingsTitleProfile => "Profil Ayarları";
        public string SettingsTitleReport => "Varsayılan Raporlama Zaman Aralığı";
        public string SettingsTitleTimeInterval => "Varsayılan Raporlama Tipi";
        public string SettingsTitleAutoBackup => "Otomatik Yedekleme Ayarı";
        public string SettingsAutoBackupActive => "Otomatik yedekleme aktif et";
        public string SettignsAutoBackup => "Otomatik yedekleme";
        public string SettingsApplied => "Olarak Ayarlandı";
        public string SettingsActive => "Aktif";
        public string SettingsPasive => "Pasif";
        public string SettingsOpenSetting => "Otomatik Açılma";
        public string SettingsCurrentPassword => "Şuanki şifreniz";

        public string SettingsNameUpdate => "İsmi Güncelle";
        public string SettingsPasswordUpdate => "Şifre Güncelle";
        public string SettingsResultNameUp => "İsim bilgileri güncellenmiştir";
        public string SettingsResultPasswordUp => "Şifre bilgisi güncellenmiştir";
        public string SettingsNameMustGreaterThanThreeChars => "İsim bilgileri en az 3 karakter olabilir";
        public string SettingsPasswordMustGreaterThanThreeChars => "Şifreniz en az 3 karakter olabilir";
        public string SettingsCheckUpdate => "Güncelleştirmeleri Denetle";
        public string SettingsUpdateChecked => "Güncelleştirmeler kontrol edildi.";
        
        #endregion
        // Report page
        #region
        public string ReportComboTypeOnlyPlan => "Haftalık Plan";
        public string ReportComboTypeOnlyWorkDone => "Yapılmış İşler";
        public string ReportComboTypeBothofThem => "İkisi Birlikte";
        public string ReportLabelPlanWorkdone => "Listeleme Tipini Seçin : ";
        public string ReportLabelTimeInterval => "Zaman Aralığını Seçin : ";
        public string ReportToPDF => "PDF'e Aktar";
        public string ReportToExcel => "Excel'e Aktar";
        public string ReportHasDate => "Tarihli";
        public string ReportCreatedBy => "Tarafından oluşturuldu";
        public string ReportSelectDay => "Günü";
        public string ReportSelectWeek => "Hafta";
        public string ReportSelectMonth => "Ay";
        public string ReportSelectYear => "Yıl";

        #endregion
        // Backup page
        #region
        public string BackupListLblTitle => "Sistemde Bulunan Yedekler";
        public string BackupChangePathLabel => "Yedekleme İşlemleri";
        public string BackupDoit => "Yedek Oluştur";
        public string BackupDoRestore => "Geri Yükle";
        public string BackupSuccessfully => "Yedekleme başarıyla alındı";
        public string BackupFailed => "Yedekleme başarısız oldu";
        public string BackupRestoreSuccessfully => "Geri yükleme tamamlandı";
        public string BackupRestoreFailed => "Geri yükleme başarısız";
        public string BackupAutoBackupCompleted => "Otomatik yedekleme başarılı";
        public string BackupSelectListBox => "Lütfen listeden bir yedek seçiniz.";
        public string ReportProcessStart => "Rapor hazırlanıyor lütfen bekleyin...";

        public string ReportProcessEnd => "Rapor başarıyla oluşturuldu";

        #endregion
        // UCCheckList
        #region
        public string UCChecklistTitle => "Rutin İşler Takip";

        public string UCChecklistAddNewPage => "Yeni Rutin İş Ekle";

        public string UCChecklistInSystemPage => "Sistemdeki Rutin İşler";

        public string UCChecklistUpdatePage => "Güncelleme Ekranı";

        public string UCChecklistProcessDescription => "Yapılacak İşin Tanımı";
        public string UCChecklistProcessFrequency => "İş yapılma aralığı";

        public string UCChecklistAddSuccess => "Rutin iş kaydı başarıyla eklendi";

        public string UCChecklistAddError => "İş açıklaması uygun değil, yada boş";
        public string UCChecklistAddLbl => "Rutin İş Açıklamasını Girin";
        public string UCChecklistRoutineWork => "İş Açıklaması";

        public string UCChecklistRelationshipTitle => "Rutin İşlerde Beraber Çalışılan Kullanıcıları Seçme";

        public string UCChecklistRelationshipUserList => "Kullanıcı Listesi";

        public string UCChecklistRelationshipWorkWith => "Beraber Çalışılan Kullanıcılar";

        public string UCChecklistRelationshipAddUser => "Kullanıcıyı Ekle";

        public string UCChecklistRelationshipRemoveUser => "Kullanıcıyı Kaldır";

        public string UCChecklistRelationshipUserAddSuccess => "Kullanıcı ekleme işlemi başarılı, bundan sonraki kayıtlarda onun ismide çıkacak";

        public string UCChecklistRelationshipUserAddFailed => "Kullanıcı ekleme işlemi başarısız oldu, zaten kullanıcı var";

        public string UCChecklistRelationshipUserRemoveSuccess => "Kullanıcı kaldırma başarılı, beraber çalışma kayıtlarınız silindi";

        public string UCChecklistRelationshipUserRemoveFailed => "Kullanıcı kaldırma işlemi başarısız";
        public string UCCheckListNote => "Not: Bir kullanıcı en fazla 3 kişi ile çalışabilir.\nBaşkası ile çalışan kullanıcı yeni çalışma arkaşı ekleyemez";
        public string UCCheckListOtherRelation => "Başkasıyla çalışıyorsun. Çalışma ayarlarını o yönetiyor.";
        public string UCCheckListRelationFull => "Sistem uyarısı : En fazla 3 kişi çalışabilmeye izin veriliyor.";
        public string UCCheckListReport => "Raporlama";
        #endregion
        public string ReportSearchTitle => "Raporlarda Arama";
    }
}
