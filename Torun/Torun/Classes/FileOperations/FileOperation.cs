using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Torun.Classes.FileOperations;
using Torun.Database;
using Torun.Lang;

namespace Torun.Classes
{
    public static class FileOperation
    {
        public static ILanguage Lang => CurrentLanguage.Language;
        public static void ExportAsPDF(User user, CountType countType, List<DB.WorkDoneList> workDoneList)
        {
            List<DateTime> dateTimes = Functions.GetDateInterval(countType);
            var Renderer = new IronPdf.HtmlToPdf();
            string html = @"<html>
	<head>
	<style type='text/css'>
		.dayTitle{
			background:green; color:white; padding:5px; text-align:center; font-family:tahoma; font-size:17px;
			margin-top: 10px;
			}
		table tr td{
			border: 1px solid #ccc;
			padding:5px;
		}
	</style>
	</head>
	<body>
	<div style='border:1px solid #ccc;'>
		<div>" + DateTime.Now.ToShortDateString() + " " + Lang.ReportHasDate + " " + user.firstname + " " + user.lastname + " " + Lang.ReportCreatedBy + " " + Functions.ConvertCountTypeToString(countType) + " " + Lang.MainPageMenuReport + @"</div>
		<div></div>
		<div>";
            foreach (var item in workDoneList)
            {
                html += @"<div class='dayTitle'>Pazartesi (29.07.2019)</div>
				<table width='100%'>
					<tr style='width:98%' >
						<td style='width:25%'>" + item.RequestNumber +@"</td>
						<td style='width:75%'>"+ item.Description +@"</td>
					</tr>
				</table>
			</div>";
            }
            html += @"
		</div>
	</div>
	</body>
</html>";
            MessageBox.Show(html);
            var PDF = Renderer.RenderHtmlAsPdf(html);
            var OutputPath = getFilePath(DateTime.Now.ToShortDateString() + "-" + FileNames.FILENAME_REPORT);
            PDF.SaveAs(OutputPath);
            // This neat trick opens our PDF file so we can see the result in our default PDF viewer
            System.Diagnostics.Process.Start(OutputPath);

        }
        public static string ProfilePhotoPath()
        {
            return getFilePath(FileNames.FILENAME_PROFILE);
        }
        public static bool isProfileExists()
        {
            return FileExists(FileNames.FILENAME_PROFILE);
        }
        public static void ChangeUserPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openFileDialog.Filter = "Image files | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openFileDialog.Multiselect = false;
                File.Delete(ProfilePhotoPath());
                File.Copy(openFileDialog.FileName, ProfilePhotoPath());
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
