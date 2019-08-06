using AutoUpdaterDotNET;
using System;

namespace Torun.Classes
{
    public static class Update
    {
        public static void Check()
        {
            AutoUpdater.Start("http://dosya.muslu.net/Torun/update.xml");
            AutoUpdater.OpenDownloadPage = true;
            AutoUpdater.UpdateMode = Mode.Forced;
            AutoUpdater.DownloadPath = Environment.CurrentDirectory;
            AutoUpdater.UpdateFormSize = new System.Drawing.Size(800, 600);
        }
    }
}
