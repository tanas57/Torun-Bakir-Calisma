using System;
using System.IO;
using System.Threading.Tasks;
namespace Torun.Classes
{
    public static class FileOperation
    {
        private const string DIR_LOGIN = "login";
        private const string DIR_REPORT = "rapor";
        public static string UserFilename { get; set; }
        private static string getFilePath(string file, bool withoutUser = false)
        {
            if (file == String.Empty) return System.Environment.CurrentDirectory + "//" + UserFilename;
            if(withoutUser == true) return System.Environment.CurrentDirectory + "//" + file;
            return System.Environment.CurrentDirectory + "//" + UserFilename + "//" + file;
        }

        public static bool Write(string data, string filename, bool withoutUser = false)
        {
            if (File.Exists(getFilePath(filename))) File.Delete(getFilePath(filename));
            TextWriter txt = new StreamWriter(getFilePath(filename, withoutUser));
            txt.Write(data);
            txt.Close();
            return true;
        }

        public static string Read(string filename)
        {
            TextReader txt = new StreamReader(getFilePath(filename));
            string data = txt.ReadLine();
            if (data != String.Empty) return data;

            return String.Empty;
        }

        public static bool FileExists(string filename)
        {
            return File.Exists(getFilePath(filename));
        }
        /// <summary>
        /// initially creates a directory for user documents
        /// </summary>
        /// <param name="username"></param>
        public static void ControlUserFile()
        {
            if (File.Exists("logged"))
            {
                if (!Directory.Exists(getFilePath(String.Empty)) == true) Directory.CreateDirectory(getFilePath(String.Empty));
            }
        }
    }
}
