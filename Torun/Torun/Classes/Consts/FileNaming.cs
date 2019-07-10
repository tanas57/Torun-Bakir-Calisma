
using System.ComponentModel;

namespace Torun.Classes.Consts
{
    enum FileNaming
    {
        [field: Description("username.txt")]
        UserName,
        [field: Description("password.txt")]
        Password
    }
}