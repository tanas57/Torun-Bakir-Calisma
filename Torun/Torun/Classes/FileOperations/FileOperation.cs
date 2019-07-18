using System;
using System.IO;
using System.Windows.Forms;
using Torun.Classes.FileOperations;
namespace Torun.Classes
{
    public static class FileOperation
    {
        private const string DIR_LOGIN = "login";
        private const string DIR_REPORT = "rapor";

        public static string UserImage { get; set; }
        public static void ChangeUserPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                UserImage = getFilePath(UserFilename + openFileDialog.Filter);
                File.Copy(openFileDialog.FileName, UserImage);
            }
        }
        public static string UserFilename { get; set; }
        private static string getFilePath(string file, bool withoutUser = false)
        {
            if (file == String.Empty) return System.Environment.CurrentDirectory + @"\" + UserFilename;
            if(withoutUser) return System.Environment.CurrentDirectory + @"\" + file;
            else return System.Environment.CurrentDirectory + @"\" + UserFilename + @"\" + file;
        }
        public static bool Write(string data, string filename, bool withoutUser = false)
        {
            string filePath = getFilePath(filename, withoutUser);

            if (File.Exists(filePath)) File.Delete(filePath);
            TextWriter txt = new StreamWriter(filePath);
            txt.Write(data);
            txt.Close();

            return true;
        }

        public static string Read(string filename)
        {
            TextReader txt = new StreamReader(getFilePath(filename));
            string data = txt.ReadLine();
            txt.Close();
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
            if (File.Exists(FileNames.IS_LOGGED))
            {
                string filePath = getFilePath(String.Empty);
                if (!Directory.Exists(filePath) == true) Directory.CreateDirectory(filePath);
            }
        }
    }
}
