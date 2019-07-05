using System.Windows.Controls;

namespace Torun.Classes
{
    class UserControllCall
    {
        /// <summary>
        /// Get user control design from UControls to add main content area
        /// </summary>
        /// <param name="grd">Using grid must be entered</param>
        /// <param name="uc">Which one user control</param>
        public static void Add(Grid grd, UserControl uc)
        {
            if(grd.Children.Count > 0) grd.Children.Clear();
            grd.Children.Add(uc);
        }
    }
}
