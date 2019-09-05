using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


namespace TorunBTProgramUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program güncellemesi indiriliyor...");
            string upPath = @"http://dosya.muslu.net/Torun/Debug.rar";
            string copyPath = AppDomain.CurrentDomain.BaseDirectory;

            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri(upPath),
                    // Param2 = Path to save
                    copyPath
                );
            }
            Console.ReadKey();
        }
        static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < 20; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("");
            }

            Console.SetCursorPosition(1, 0);
            Console.Write("İndirilme durumu %" + e.ProgressPercentage);
        }
    }
}
