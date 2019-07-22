using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torun.Lang
{
    public interface ILanguage
    {
        /* WELCOME PAGE STARTS*/

        // LOGIN //
        string WelcomeLoginTitle { get; }
        string WelcomeLoginRemember { get; }
        string WelcomeLoginButton { get; }
        string WelcomeLoginFailedNotEnoughUserOrPassword { get; }
        string WelcomeLoginFailedUserNotFind { get; }
        string WelcomeLoginFailedWrongPassword { get; }
        string WelcomeLoginSuccess { get; }
        // REGISTER //
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

        /* WELCOME PAGE ENDS*/

        /* COMBOBOX ITEMS */

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

        /* REQUEST ADD WINDOWS STARTS */
        string RequestAddRequestNumber { get; }
        string RequestAddRequestPriority { get; }
        string RequestAddRequestDescription { get; }
        string RequestAddRequestButton { get; }
        string RequestAddRequestResultOk { get; }
        string RequestAddRequestResultNo { get; }
        string RequestAddRequestResultNotSelected { get; }
        string RequestAddRequestResultNoDescription { get; }
        string RequestAddRequestTitle { get; }
        /* REQUEST ADD WINDOWS ENDS */

        /* REQUEST EDIT WINDOWS ENDS */
        string RequestEditTitle { get; }
        string RequestEditButton { get; }
        string RequestEditLabelSaveOK { get; }
        string RequestEditLabelSaveNO { get; }
        /* REQUEST EDIT WINDOWS ENDS */
        /* MAIN PAGE STARTS */
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
        /* MAIN PAGE ENDS */

        /* REQUEST SCHEDULE PAGE STARTS*/
        string RequestScheduleTitle { get; }
        string RequestScheduleReqNumber { get; }
        string RequestScheduleChooseDate { get; }
        string RequestScheduleSave { get; }
        string RequestScheduleSaveChooseDateError { get; }
        string RequestScheduleSaveFailed { get; }

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
        // Todolist ends
        #endregion

        // Weekly plan page
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

        string WeeklyDetailTitle { get; }
        string WeeklyDetailDescription { get; }
        string WeeklyDetailCalendar { get; }
    }
}
