using System;
using System.IO;
using System.Threading.Tasks;

namespace Torun.Classes
{
    public static class FileOperation
    {
        public static bool Write(string data, string filename)
        {
            if (File.Exists(filename)) File.Delete(filename);
            TextWriter txt = new StreamWriter(filename);
            txt.Write(data);
            txt.Close();
            return true;
        }

        public static string Read(string filename)
        {
            TextReader txt = new StreamReader(filename);
            string data = txt.ReadLine();
            if (data != String.Empty) return data;

            return String.Empty;
        }

        public static bool FileExists(string filename)
        {
            return File.Exists(filename);
        }
    }
}
