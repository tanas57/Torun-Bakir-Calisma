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
        string WelcomeLoginTitle { get; set; }
        string WelcomeLoginRemember { get; set; }
        string WelcomeLoginButton { get; set; }
        string WelcomeLoginFailedNotEnoughUserOrPassword { get; set; }
        string WelcomeLoginFailedUserNotFind { get; set; }
        string WelcomeLoginFailedWrongPassword { get; set; }
        string WelcomeLoginSuccess { get; set; }
        // REGISTER //
        string WelcomeRegisterTitle { get; set; }
        string WelcomeRegisterButton { get; set; }
        string WelcomeSignSuccess { get; set; }
        string WelcomeSignUserAlreadyExists { get; set; }
        string WelcomeSignPasswordNotEnough { get; set; }
        string WelcomeSignPasswordsNotMatch { get; set; }
        string WelcomeSignUsernameLenghtMustBeGreaterThanThree { get; set; }
        string WelcomeSignFillAllFields { get; set; }
        string WelcomeRegisterTitleUsername { get; set; }
        string WelcomeRegisterTitlePassword { get; set; }
        string WelcomeRegisterTitlePasswordAgain { get; set; }
        string WelcomeRegisterTitleFirstName { get; set; }
        string WelcomeRegisterTitleLastName { get; set; }

        /* WELCOME PAGE ENDS*/
    }
}
