using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
namespace Torun.Classes.Keyboard
{
    public class KeyControl
    {
        public static bool CapsLock()
        {
            if (Control.IsKeyLocked(Keys.CapsLock)) return true;
            return false;
        }
    }
}
