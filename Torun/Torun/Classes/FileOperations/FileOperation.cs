using IronPdf;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static void ExportAsPDF(User user, CountType countType, ReportType reportType, DB db)
        {
            List<DateTime> dateTimes = Functions.GetDateInterval(countType);
            DateTime start = dateTimes[0];
            DateTime end = dateTimes[1];

            string fullname = user.firstname + " " + user.lastname;

            // use to print selected day, week, month, or year on the screen and PDF
            string strReportType = String.Empty;
            switch (reportType)
            {
                case ReportType.OnlyPlan: strReportType = Lang.ReportComboTypeOnlyPlan; break;
                case ReportType.OnlyWorkDone: strReportType = Lang.ReportComboTypeOnlyWorkDone; break;
                case ReportType.Both: strReportType = Lang.ReportComboTypeBothofThem; break;
            }
            // get the week number or day number according to selected address interval
            string timeString = String.Empty;
            CultureInfo myCI = new CultureInfo("tr-TR");
            Calendar myCal = myCI.Calendar;

            switch (countType)
            {
                case CountType.Daily: timeString = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)DateTime.Now.DayOfWeek] + " " + Lang.ReportSelectDay + " " + strReportType;  break;
                case CountType.Yearly: timeString = DateTime.Now.Year.ToString() + "." + Lang.ReportSelectYear + " " + strReportType; break;
                case CountType.Montly: timeString = DateTime.Now.Month.ToString() + "." + Lang.ReportSelectMonth + " " + strReportType; break;
                case CountType.Weekly: timeString = myCal.GetWeekOfYear(DateTime.Now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek) + "." + Lang.ReportSelectWeek + " " + strReportType; break;
            }
            var Renderer = new IronPdf.HtmlToPdf();

            string torunLogoPath = Application.StartupPath.ToString() + "//torun-logo-2.png";

            string html = @"<html>
	<head>
	<style type='text/css'>
		.dayTitle{
			background:#B0151E; color:white; padding:5px; font-family:tahoma; font-size:17px;
			margin-top: 10px; width:95%;  height:auto; margin-left:auto; margin-right:auto;
			}
		table tr td{
			padding:5px; margin-left:auto; margin-right:auto;
		}
		.bolder{
			font-weight:bold; font-size:17px;
			}
			#main{
			text-align:center;
			border:1px solid #ccc; margin-left:auto; margin-right:auto;
			}
			.clas{
				margin-left:auto; margin-right:auto;text-align:center;
				
			}
			.clas table{
				margin-left:25px;}
	</style>
	</head>
	<body>
	<div id='main'>
		<table border='0' width='100%'>
			<tr>
				<td width='33%'><img src='" + torunLogoPath + @"' height='70' /></td>
				<td width='33%'>
					<table border='0'>
						<tr><td class='bolder'>" + fullname + @"</td></tr>
						<tr><td class='bolder'>" + timeString + @"</td></tr>
					</table>
				</td>
				<td width='11%'>
					<table border='0'>
						<tr><td class='bolder'>" + DateTime.Now.ToShortDateString() + @"</td></tr>
						<tr><td class='bolder'>" + DateTime.Now.ToShortTimeString() + @"</td></tr>
					</table>
				</td>
			</tr>
		</table>";
            if(reportType == ReportType.OnlyWorkDone)
            {
                while (end >= start)
                {
                    List<DB.WorkDoneList> workDone = db.ListWorkDone(user, start.Date, OrderBy.AddedTimeAsc);
                    if (workDone.Count >= 1)
                    {
                        html += @"<div class='clas'>
				<div class='dayTitle'>" + CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)start.DayOfWeek] + " (" + start.ToShortDateString() + @")</div>
					<table width='100%'>";
                        foreach (var item in workDone)
                        {
                            html += @"<tr>
							<td style='width:30%'>" + item.RequestNumber + @"</td>
							<td style='width:70%'>" + item.Description + @"</td>
						</tr>";
                        }
                        html += @"
					</table>";
                    }
                    start = start.AddDays(1);
                }
            }
            else if(reportType == ReportType.OnlyPlan)
            {
                while (end >= start)
                {
                    List<DB.WeeklyPlan> weeklyPlans = db.ListWeeklyPlanbyDate(user, start.Date);
                    if (weeklyPlans.Count >= 1)
                    {
                        html += @"<div class='clas'>
				<div class='dayTitle'>" + CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)start.DayOfWeek] + " (" + start.ToShortDateString() + @")</div>
					<table width='100%'>";
                        foreach (var item in weeklyPlans)
                        {
                            html += @"<tr>
							<td style='width:30%'>" + item.RequestNumber + @"</td>
							<td style='width:70%'>" + item.WorkDescription + @"</td>
						</tr>";
                        }
                        html += @"
					</table>";
                    }
                    start = start.AddDays(1);
                }
            }
            else if (reportType == ReportType.Both)
            {
                //start = new DateTime(2019, 07, 31);
                while (end >= start)
                {
                    List<DB.WorkDoneandPlans> workPlans = db.GetWorkDoneAndPlansbyDate(user, start.Date);
                    if (workPlans.Count >= 1)
                    {
                        html += @"<div class='clas'>
				<div class='dayTitle'>" + CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)start.DayOfWeek] + " (" + start.ToShortDateString() + @")</div>
					<table width='100%'>";
                        foreach (var item in workPlans)
                        {
                            html += @"<tr>
							<td style='width:30%'>" + item.RequestNumber + @"</td>
                            <td style='width:18%'>Planlanan : " + item.PlanDate.ToShortDateString() + " Gerçekleşen: "+ (item.WorkDoneDate.ToShortDateString() == "1.01.0001"
? " - ": item.WorkDoneDate.ToShortDateString()) + @"</td>
							<td style='width:52%'>" + item.WorkDescription + @"</td>
						</tr>";
                        }
                        html += @"
					</table>";
                    }
                    start = start.AddDays(1);
                }
            }
            html += @"
			</div>

		</div>
	</div>
	</body>
</html>";
            Renderer.PrintOptions.Footer = new HtmlHeaderFooter()
            {
                Height = 15,
                HtmlFragment = "<center><i>{page} of {total-pages}<i></center>",
                DrawDividerLine = true
            };
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
