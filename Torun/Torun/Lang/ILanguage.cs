namespace Torun.Lang
{
    public interface ILanguage
    {
        /* WELCOME PAGE STARTS*/

        // LOGIN //
        #region
        string WelcomeLoginTitle { get; }
        string WelcomeLoginRemember { get; }
        string WelcomeLoginButton { get; }
        string WelcomeLoginFailedNotEnoughUserOrPassword { get; }
        string WelcomeLoginFailedUserNotFind { get; }
        string WelcomeLoginFailedWrongPassword { get; }
        string WelcomeLoginSuccess { get; }
        #endregion
        // REGISTER //
        #region
        string WelcomeRegisterTitle { get; }
        string WelcomeRegisterButton { get; }
        string WelcomeSignSuccess { get; }
        string WelcomeSignUserAlreadyExists { get; }
        string WelcomeSignPasswordNotEnough { get; }
        string WelcomeSignPasswordsNotMatch { get; }
        string WelcomeSignUsernameLenghtMustBeGreaterThanThree { get; }
        string WelcomeSignFillAllFields { get; }
        string WelcomeRegisterTitleUsername { get; }
        string WelcomeRegisterTitlePassword { get; }
        string WelcomeRegisterTitlePasswordAgain { get; }
        string WelcomeRegisterTitleFirstName { get; }
        string WelcomeRegisterTitleLastName { get; }
        string WelcomeCapsLock { get; }
        #endregion

        /* WELCOME PAGE ENDS*/

        /* COMBOBOX ITEMS */
        #region

        // PRIORITY
        string ComboboxPriorityLow { get; }
        string ComboboxPriorityNormal { get; }
        string ComboboxPriorityHigh { get; }
        string ComboboxPriorityUrgent { get; }
        string ComboboxPriorityProject { get; }
        // STATUS
        string ComboboxStatusNew { get; }
        string ComboboxStatusInProcess { get; }
        string ComboboxStatusClosed { get; }
        string ComboboxStatusEdited { get; }
        string ComboboxStatusPlanned { get; }
        /* COMBOBOX ITEMS */
        #endregion

        /* REQUEST ADD WINDOWS STARTS */
        #region
        string RequestAddRequestNumber { get; }
        string RequestAddRequestPriority { get; }
        string RequestAddRequestDescription { get; }
        string RequestAddRequestButton { get; }
        string RequestAddRequestResultOk { get; }
        string RequestAddRequestResultNo { get; }
        string RequestAddRequestResultNotSelected { get; }
        string RequestAddRequestResultNoDescription { get; }
        string RequestAddRequestTitle { get; }
        string RequestAddReqNumEmpty { get; }
        string RequestAddToWorkDone { get; }
        /* REQUEST ADD WINDOWS ENDS */
        #endregion

        /* REQUEST EDIT WINDOWS ENDS */
        #region
        string RequestEditTitle { get; }
        string RequestEditButton { get; }
        string RequestEditLabelSaveOK { get; }
        string RequestEditLabelSaveNO { get; }
        /* REQUEST EDIT WINDOWS ENDS */
        #endregion
        /* MAIN PAGE STARTS */
        #region
        string MainPageTitle { get; }
        string MainPageTotalRequest { get; }
        string MainPageOpenRequest { get; }
        string MainPageClosedRequest { get; }
        string MainPageLogOut { get; }
        string MainPageMenuToDo { get; }
        string MainPageMenuWeeklyPlan { get; }
        string MainPageWorkDone { get; }
        string MainPageMenuReport { get; }
        string MainPageMenuBackup { get; }
        #endregion
        /* MAIN PAGE ENDS */

        /* REQUEST SCHEDULE PAGE STARTS*/
        #region
        string RequestScheduleTitle { get; }
        string RequestScheduleReqNumber { get; }
        string RequestScheduleChooseDate { get; }
        string RequestScheduleSave { get; }
        string RequestScheduleSaveChooseDateError { get; }
        string RequestScheduleSaveFailed { get; }
        #endregion

        /* REQUEST SCHEDULE PAGE ENDS */
        // Todolist page
        #region
        string UCTodoListAddRequest { get; }
        string UCTodoListDetailList { get; }
        string UCTodoListProcesses { get; }
        string UCTodoListRequestNumber { get; }
        string UCTodoListPriority { get; }
        string UCTodoListDescription { get; }
        string UCTodoListAddedTime { get; }
        string UCTodoListStatus { get; }
        string UCTodoListScheduleButton { get; }
        string UCTodoListEditButton { get; }
        string UCTodoListDeleteButton { get; }
        string UCTodoListInfoMessage { get; }
        // Todolist ends
        #endregion
        // Weekly plan page
        #region
        string UCWeeklyPlanButtonGetDetail { get; }
        string UCWeeklyPlanButtonDoCompleted { get; }
        string UCWeeklyPlanButtonEdit { get; }
        string UCWeeklyPlanButtonRemove { get; }
        string UCWeeklyPlanButtonChangeDate { get; }
        string UCWeeklyPlanDaysMonday { get; }
        string UCWeeklyPlanDaysTuesday { get; }
        string UCWeeklyPlanDaysWednesday { get; }
        string UCWeeklyPlanDaysThursday { get; }
        string UCWeeklyPlanDaysFriday { get; }
        string UCWeeklyPlanCurrentTime { get; }
        string UCWeeklyPlanNumOfPlans { get; }
        #endregion
        // weekly detail
        #region
        string WeeklyDetailTitle { get; }
        string WeeklyDetailDescription { get; }
        string WeeklyDetailCalendar { get; }
        string WeeklyDetailCalendarOK { get; }
        #endregion
        // weekly completed
        #region
        string WeeklyCompletedTitle { get; }
        string WeeklyCompletedCurrentDay { get; }
        string WeeklyCompletedAllWork { get; }
        string WeeklyCompletedNote { get; }
        #endregion
        // buttons
        #region
        string ButtonSave { get; }
        string ButtonDelete { get; }
        string ButtonEdit { get; }
        string ButtonRemove { get; }
        string ButtonTransfer { get; }
        string ButtonAdd { get; }
        #endregion
        // weekly remove
        #region
        string WeeklyRemoveTitle { get; }
        string WeeklyRemoveAday { get; }
        string WeeklyRemoveAllDays { get; }
        string WeeklyRemoveButtonRemove { get; }
        #endregion
        // weekly edit plan
        #region
        string WeeklyEditPlanTitle { get; }
        string WeeklyEditPlanRequestInfo { get; }
        string WeeklyEditPlanInfo { get; }
        string WeeklyEditPlanNotSelectPlan { get; }
        string WeeklyEditPlanRemoved { get; }
        string WeeklyEditPlanRemoveWorkdoneError { get; }
        string WeeklyEditPlanRemovePlanTransferTodoList { get; }

        string WeeklyEditPlanCalendarAddTitle { get; }
        string WeeklyEditPlanCalendarAddLabel { get; }
        string WeeklyEditPlanCalendarAddDates { get; }

        string WeeklyEditPlanTransferTitle { get; }
        string WeeklyEditPlanCalendarAddADates { get; }
        string WeeklyEditPlanTransferError { get; }
        string WeeklyEditPlanTransfered { get; }
        #endregion
        // weekly plan sort buttons
        #region
        string WeeklyPlanSortLbl { get; }
        string WeeklyPlanSortAddTime { get; }
        string WeeklyPlanSortAddTimeDesc { get; }
        string WeeklyPlanSortPriorityAsc { get; }
        string WeeklyPlanSortPriorityDesc { get; }
        string WeeklyPlanSortNameDesc { get; }
        string WeeklyPlanSortNameAsc { get; }
        #endregion
        // UC Work done page
        string UCWorkDoneRemoveWorkDone { get; }
        // workdone remove page
        #region
        string WorkDoneRemoveTitle { get; }
        string WorkDoneRemoveSelectADay { get; }
        string WorkDoneRemoveSelectAllDays { get; }
        #endregion
        // workdone get detail page
        string WorkDoneDetailTitle { get; }
        string WorkDoneDetailGroupPlanAndWorkDone { get; }
        string WorkDoneDetailDescription{ get; }
        string WorkDoneEditTitle { get; }
        string WorkDoneEditCompletedWorks { get; }
        string WorkDoneEditChoosenWorkDescription { get; }
        string WorkDoneEditWorkLabel { get; }
        string WorkDoneEditRequestSaveButton { get; }
    }
}
