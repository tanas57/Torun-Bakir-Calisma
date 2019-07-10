using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torun.Lang
{
    public interface ILanguage
    {
        string WelcomeLoginFailedNotEnoughUserOrPassword { get; set; }
        string WelcomeLoginFailedUserNotFind { get; set; }
        string WelcomeLoginFailedWrongPassword { get; set; }
        string WelcomeLoginSuccess { get; set; }

    }
}
