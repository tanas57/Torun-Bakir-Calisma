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

        /* WELCOME PAGE ENDS*/

        /* COMBOBOX ITEMS */

        // PRIORITY
        string ComboboxPriorityLow { get; }
        string ComboboxPriorityNormal { get; }
        string ComboboxPriorityHigh { get; }
        // STATUS
        string ComboboxStatusNew { get; }
        string ComboboxStatusInProcess { get; }
        string ComboboxStatusClosed { get; }
        /* COMBOBOX ITEMS */

        /* REQUEST ADD WINDOWS STARTS */
        string RequestAddRequestNumber { get; }
        string RequestAddRequestPriority { get; }
        string RequestAddRequestDescription { get; }
        string RequestAddRequestButton { get; }
        /* REQUEST ADD WINDOWS ENDS */

    }
}
