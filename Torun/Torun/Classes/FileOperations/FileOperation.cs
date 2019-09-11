using IronPdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Torun.Classes.FileOperations;
using Torun.Database;
using Torun.Lang;
using Torun.UControls;

namespace Torun.Classes
{
    public static class FileOperation
    {
        private static List<UCCheckList.CheckListObject> GridSource { get; set; }
        public static ILanguage Lang => CurrentLanguage.Language;
        public static void CheckListExportEXCEL(User user, CountType countType, DB db, List<DateTime> selectedDates = null)
        {
            List<DateTime> dateTimes = null;

            Range cell;

            if (countType == CountType.SelectDate) dateTimes = selectedDates;
            else dateTimes = Functions.GetDateInterval(countType, user);

            DateTime start = dateTimes[0];
            DateTime end = dateTimes[1];

            string torunLogoPath = System.Windows.Forms.Application.StartupPath.ToString() + @"\torun_red_mini.png";
            string checkBoxTick = System.Windows.Forms.Application.StartupPath.ToString() + @"\check.png";
            string checkBoxNoTick = System.Windows.Forms.Application.StartupPath.ToString() + @"\nocheck.png";

            Microsoft.Office.Interop.Excel.Application excelPage = new Microsoft.Office.Interop.Excel.Application();

            var ColorGray = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            var ColorWhite = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            var ColorBlack = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);

            var workWithUser = db.GetUsersRelationShip(user);
            int userCount = workWithUser.Count + 1;

            string[] names = new string[3];
            names[0] = user.firstname + " " + user.lastname;

            for (int i = 1; i <= workWithUser.Count; i++)
            {
                names[i] = workWithUser[i-1].FullName;
            }

            Setting setting = db.GetUserSettings(user);

            Workbook workbook = excelPage.Workbooks.Add(Type.Missing);

            workbook.Worksheets[1].Delete();
            workbook.Worksheets[1].Delete();

            // user work records
            var works = db.GetRoutineWorks(user);
            while (end >= start)
            {

                RoutineWorkRecord user_record = db.GetCheckListRecord(user, end.Date);

                if (user_record == null) { end = end.AddDays(-1); continue; }

                Worksheet worksheet = workbook.Worksheets.Add();

                worksheet.Name = end.Day.ToString();

                end = end.AddDays(-1);

                int row = 3, column = 2;

                // excel header starts
                #region
                cell = worksheet.Cells[row, column];
                cell.Value2 = "TORUN BAKIR";
                cell.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkRed);
                cell.Font.Size = 20;
                cell.Font.Bold = true;
                cell.Font.Name = "Calibri";
                cell.ColumnWidth = 20;
                row++;

                cell = worksheet.Cells[row, column];
                cell.Value2 = setting.routineWorkTitle1.ToUpper();
                cell.Font.Color = ColorGray;
                cell.Font.Size = 9;
                cell.Font.Name = "Calibri";
                cell.ColumnWidth = 20;
                row++;

                cell = worksheet.Cells[row, column];
                cell.Value2 = setting.routineWorkTitle2.ToUpper();
                cell.Font.Color = ColorGray;
                cell.Font.Size = 9;
                cell.Font.Name = "Calibri";
                cell.ColumnWidth = 20;

                row += 2;
                #endregion
                // excel header ends

                cell = worksheet.Cells[3, 2];
                cell.ColumnWidth = 20;
                // table starts
                #region
                cell = worksheet.Range[worksheet.Cells[row, 2], worksheet.Cells[row, 3]];
                cell.Merge();
                Functions.ExcelCellProcess(cell, ColorGray, ColorBlack, ColorBlack, 70, 18, "", "Calibri", true, 8, false);

                if (userCount == 2)
                {
                    cell = worksheet.Range[worksheet.Cells[row, 4], worksheet.Cells[row, 5]];
                    cell.Merge();
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 18, 18, names[0].ToUpper(), "Calibri", true, 8, true);

                    cell = worksheet.Range[worksheet.Cells[row, 6], worksheet.Cells[row, 7]];
                    cell.Merge();

                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 18, 18, names[1].ToUpper(), "Calibri", true, 8, true);

                    row++; column = 2;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 20, 18, Lang.UCCheckListOrder.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 50, 18, Lang.UCCheckListWorkDescription.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioDaily.ToUpper(), "Calibri", true, 8, true); ;
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioWeekly.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioDaily.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioWeekly.ToUpper(), "Calibri", true, 8, true);
                }
                else if (userCount == 1)
                {
                    cell = worksheet.Range[worksheet.Cells[row, 4], worksheet.Cells[row, 5]];
                    cell.Merge();
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 18, 18, names[0].ToUpper(), "Calibri", true, 8, true);

                    row++; column = 2;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 20, 18, Lang.UCCheckListOrder.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 50, 18, Lang.UCCheckListWorkDescription.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioDaily.ToUpper(), "Calibri", true, 8, true); ;
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioWeekly.ToUpper(), "Calibri", true, 8, true);
                    column++;
                }
                else if (userCount == 3)
                {
                    cell = worksheet.Range[worksheet.Cells[row, 4], worksheet.Cells[row, 5]];
                    cell.Merge();
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 18, 18, names[0].ToUpper(), "Calibri", true, 8, true);

                    cell = worksheet.Range[worksheet.Cells[row, 6], worksheet.Cells[row, 7]];
                    cell.Merge();

                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 18, 18, names[1].ToUpper(), "Calibri", true, 8, true);

                    cell = worksheet.Range[worksheet.Cells[row, 8], worksheet.Cells[row, 9]];
                    cell.Merge();

                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 18, 18, names[2].ToUpper(), "Calibri", true, 8, true);

                    row++; column = 2;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 20, 18, Lang.UCCheckListOrder.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 50, 18, Lang.UCCheckListWorkDescription.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioDaily.ToUpper(), "Calibri", true, 8, true); ;
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioWeekly.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioDaily.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioWeekly.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioDaily.ToUpper(), "Calibri", true, 8, true);
                    column++;

                    cell = worksheet.Cells[row, column];
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, Lang.SettingsRadioWeekly.ToUpper(), "Calibri", true, 8, true);

                }

                #endregion
                // table ends

                // order number and description columns width are setted.
                cell = worksheet.Cells[8, 2];
                cell.ColumnWidth = 6;
                cell = worksheet.Cells[8, 3];
                cell.ColumnWidth = 45;

                if (userCount == 2) worksheet.Shapes.AddPicture(torunLogoPath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 448, 20, 80, 70);
                else if (userCount == 1) worksheet.Shapes.AddPicture(torunLogoPath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 344, 20, 80, 70);
                else if (userCount == 3) worksheet.Shapes.AddPicture(torunLogoPath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 550, 20, 80, 70);


                GridSource = new List<UCCheckList.CheckListObject>();
                for (int i = 0; i < works.Count; i++) GridSource.Add(new UCCheckList.CheckListObject());
                // fill the table with data from database
                string user2 = user.id.ToString();
                if (user_record != null)
                {
                    string[] ids = user_record.work_Ticks.Split('*');

                    for (int i = 1; i < ids.Length; i++)
                    {
                        string[] workID_parse = ids[i].Split(':');

                        int work_id = int.Parse(workID_parse[0]);

                        string[] parse = workID_parse[1].Split(',');

                        if (GridSource == null) continue; // bug fix
                        if (GridSource[i - 1] == null) continue; // bug fix

                        GridSource[i - 1].WorkID = work_id;
                        GridSource[i - 1].Order = i;
                        GridSource[i - 1].WorkDescription = db.CheckListDescriptionByID(work_id);
                        GridSource[i - 1].Daily1 = bool.Parse(parse[0]);
                        GridSource[i - 1].Weekly1 = bool.Parse(parse[1]);
                        GridSource[i - 1].Daily2 = bool.Parse(parse[2]);
                        GridSource[i - 1].Weekly2 = bool.Parse(parse[3]);
                        GridSource[i - 1].Daily3 = bool.Parse(parse[4]);
                        GridSource[i - 1].Weekly3 = bool.Parse(parse[5]);
                    }
                }

                row++;
                Microsoft.Office.Interop.Excel.OLEObjects objs = worksheet.OLEObjects();
                int top = 139;

                for (int i = 0; i < GridSource.Count; i++)
                {
                    var item = GridSource[i];

                    column = 2;
                    if (userCount == 1)
                    {
                        cell = worksheet.Cells[row, column];
                        Functions.ExcelCellProcess(cell, ColorWhite, ColorBlack, ColorBlack, 6, 18, item.Order.ToString(), "Calibri", true, 8, true);
                        column++;

                        cell = worksheet.Cells[row, column];
                        Functions.ExcelCellProcess(cell, ColorWhite, ColorBlack, ColorBlack, 50, 18, item.WorkDescription, "Calibri", false, 8, true);
                        column++;

                        cell = worksheet.Cells[row, column];
                        Functions.ExcelCellProcess(cell, ColorWhite, ColorBlack, ColorBlack, 9, 18, "", "Calibri", false, 8, true);
                        column++;

                        cell = worksheet.Cells[row, column];
                        Functions.ExcelCellProcess(cell, ColorWhite, ColorBlack, ColorBlack, 9, 18, "", "Calibri", false, 8, true);

                        if (item.Daily1) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 369, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 369, top, 16, 16);

                        if (item.Weekly1) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 420, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 420, top, 16, 16);
                    }
                    else if (userCount == 2)
                    {
                        cell = worksheet.Cells[row, column];
                        Functions.ExcelCellProcess(cell, ColorWhite, ColorBlack, ColorBlack, 6, 18, item.Order.ToString(), "Calibri", true, 8, true);
                        column++;

                        cell = worksheet.Cells[row, column];
                        Functions.ExcelCellProcess(cell, ColorWhite, ColorBlack, -1, 50, 18, item.WorkDescription, "Calibri", false, 8, true);
                        column++;

                        if (item.Daily1) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 369, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 369, top, 16, 16);

                        if (item.Weekly1) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 420, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 420, top, 16, 16);

                        if (item.Daily2) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 470, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 470, top, 16, 16);

                        if (item.Weekly2) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 520, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 520, top, 16, 16);
                    }
                    else if (userCount == 3)
                    {
                        cell = worksheet.Cells[row, column];
                        Functions.ExcelCellProcess(cell, ColorWhite, ColorBlack, ColorBlack, 6, 18, item.Order.ToString(), "Calibri", true, 8, true);
                        column++;

                        cell = worksheet.Cells[row, column];
                        Functions.ExcelCellProcess(cell, ColorWhite, ColorBlack, -1, 50, 18, item.WorkDescription, "Calibri", false, 8, true);
                        column++;

                        if (item.Daily1) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 369, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 369, top, 16, 16);

                        if (item.Weekly1) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 420, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 420, top, 16, 16);

                        if (item.Daily2) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 470, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 470, top, 16, 16);

                        if (item.Weekly2) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 520, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 520, top, 16, 16);

                        if (item.Daily3) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 570, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 570, top, 16, 16);

                        if (item.Weekly3) worksheet.Shapes.AddPicture(checkBoxTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 620, top, 16, 16);
                        else worksheet.Shapes.AddPicture(checkBoxNoTick, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 620, top, 16, 16);
                    }

                    row++; top += 18;
                }

                if (userCount == 1)
                {
                    cell = worksheet.Range[worksheet.Cells[row, 4], worksheet.Cells[row, 5]];
                    cell.Merge();
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, end.AddDays(1).ToShortDateString(), "Calibri", true, 8, true);
                }
                else if (userCount == 2)
                {
                    cell = worksheet.Range[worksheet.Cells[row, 6], worksheet.Cells[row, 7]];
                    cell.Merge();
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, end.AddDays(1).ToShortDateString(), "Calibri", true, 8, true);
                }
                else if (userCount == 3)
                {
                    cell = worksheet.Range[worksheet.Cells[row, 8], worksheet.Cells[row, 9]];
                    cell.Merge();
                    Functions.ExcelCellProcess(cell, ColorGray, ColorWhite, ColorBlack, 9, 18, end.AddDays(1).ToShortDateString(), "Calibri", true, 8, true);
                }
            }
            // control are the year and month directories exists
            int order = 1;
            string outputFilePath = ReportFolderProcess() + @"\" + DateTime.Now.ToString("dd-MM-yyy") + "-" + FileNames.FILENAME_REPORCHECKLIST; // create outputfile path
            while (true)
            {
                if (File.Exists(outputFilePath))
                {
                    outputFilePath = ReportFolderProcess() + @"\" + DateTime.Now.ToString("dd-MM-yyy") + "-" + order + "-" + FileNames.FILENAME_REPORCHECKLIST; // create outputfile path
                    order++;
                }
                else break;
            }

            // save excel file to user folder
            workbook.SaveAs(outputFilePath, XlFileFormat.xlOpenXMLWorkbook, Type.Missing,
            Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange,
            XlSaveConflictResolution.xlUserResolution, true,
            Type.Missing, Type.Missing, Type.Missing);

            workbook.Close();
            excelPage.Quit();
            System.Diagnostics.Process.Start(outputFilePath);
            System.Diagnostics.Process.Start(ReportFolderProcess());
        }
        public static void ExportAsPDF(User user, CountType countType, ReportType reportType, DB db, List<DateTime> selectedDates = null)
        {
            List<DateTime> dateTimes;

            if (countType == CountType.SelectDate) dateTimes = selectedDates;
            else dateTimes = Functions.GetDateInterval(countType, user);

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
                case CountType.Daily: timeString = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)DateTime.Now.DayOfWeek] + " " + Lang.ReportSelectDay + " " + strReportType; break;
                case CountType.Yearly: timeString = DateTime.Now.Year.ToString() + "." + Lang.ReportSelectYear + " " + strReportType; break;
                case CountType.Montly: timeString = DateTime.Now.Month.ToString() + "." + Lang.ReportSelectMonth + " " + strReportType; break;
                case CountType.Weekly: timeString = myCal.GetWeekOfYear(DateTime.Now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek) + "." + Lang.ReportSelectWeek + " " + strReportType; break;
                // report date bug fix.
                case CountType.FromTheBeginning: start = user.register_date; break;
                case CountType.SelectDate:
                    timeString = start.Date.ToShortDateString() + " - " + end.Date.ToShortDateString() + " " + Lang.ReportDateIntervalReport;
                    break;
            }

            string torunLogoPath = System.Windows.Forms.Application.StartupPath.ToString() + "//torun-logo-2.png";

            var priorities = new List<PriorityType>() { PriorityType.Urgent, PriorityType.High, PriorityType.Normal, PriorityType.Low, PriorityType.Project };

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
            if (reportType == ReportType.OnlyWorkDone)
            {
                while (end >= start)
                {
                    List<DB.WorkDoneList> workDone = db.ListWorkDone(user, start.Date, OrderBy.AddedTimeAsc);
                    if (workDone.Count >= 1)
                    {
                        html += @"<div class='clas'>
				<div class='dayTitle'>" + CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)start.DayOfWeek] + " (" + start.ToShortDateString() + @")</div>
					<table width='97%'>";
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
            else if (reportType == ReportType.OnlyPlan)
            {
                while (end >= start)
                {
                    List<DB.WeeklyPlan> weeklyPlans = db.ListWeeklyPlanbyDate(user, start.Date);
                    if (weeklyPlans.Count >= 1)
                    {
                        html += @"<div class='clas'>
				<div class='dayTitle'>" + CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)start.DayOfWeek] + " (" + start.ToShortDateString() + @")</div>
					<table width='97%'>";
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
					<table width='97%'>";
                        foreach (var item in workPlans)
                        {
                            html += @"<tr>
							<td style='width:30%'>" + item.RequestNumber + @"</td>
                            <td style='width:18%'>Planlanan : " + item.PlanDate.ToShortDateString() + " Gerçekleşen: " + (item.WorkDoneDate.ToShortDateString() == "1.01.0001"
? " - " : item.WorkDoneDate.ToShortDateString()) + @"</td>
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
            <table>
                <tr>
<div class='dayTitle'>" + Lang.ReportRequestStatistic + @"</div>
			<tr width='60%'>
							<td style='width:30%'>" + Lang.MainPageTotalRequest + @"</td>
							<td style='width:70%'>" + db.GetRequestCount(1, user, countType, selectedDates).ToString() + @"</td>
			</tr>
            <tr width='60%'>
							<td style='width:30%'>" + Lang.MainPageClosedRequest + @"</td>
							<td style='width:70%'>" + db.GetRequestCount(3, user, countType, selectedDates).ToString() + @"</td>
			</tr>
            <tr width='60%'>
							<td style='width:30%'>" + Lang.MainPageOpenRequest + @"</td>
							<td style='width:70%'>" + db.GetRequestCount(2, user, countType, selectedDates).ToString() + @"</td>
			</tr>";

            foreach (var item in priorities)
            {
                html += @"<tr width='60%'>
							<td style='width:30%'>" + Functions.PriorityString(item) + " " + Lang.ReportRequestNumber + @"</td>
							<td style='width:70%'>" + db.GetRequestCount(item, user, countType, selectedDates) + @"</td>
			</tr>";
            }
            html += @"</table>
</tr>
            </table>
		</div>
	</div>
	</body>
</html>";
            // instantiate the html to pdf converter
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.RenderHtmlAsPdf(html);

            // control are the year and month directories exists
            var OutputPath = ReportFolderProcess() + @"\" + DateTime.Now.ToString("dd-MM-yyy") + "-" + FileNames.FILENAME_REPORTPDF; // create outputfile path
            int order = 1;
            while (true)
            {
                if (File.Exists(OutputPath))
                {
                    OutputPath = ReportFolderProcess() + @"\" + DateTime.Now.ToString("dd-MM-yyy") + "-" + order + "-" + FileNames.FILENAME_REPORTPDF; // create outputfile path
                    order++;
                }
                else break;
            }
            doc.SaveAs(OutputPath);
            // This neat trick opens our PDF file so we can see the result in our default PDF viewer
            System.Diagnostics.Process.Start(OutputPath);
            System.Diagnostics.Process.Start(ReportFolderProcess());
        }
        public static void ExportAsEXCEL(User user, CountType countType, ReportType reportType, DB db, List<DateTime> selectedDates = null)
        {
            List<DateTime> dateTimes = null;

            if (countType == CountType.SelectDate) dateTimes = selectedDates;
            else dateTimes = Functions.GetDateInterval(countType, user);

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
                case CountType.Daily: timeString = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)DateTime.Now.DayOfWeek] + " " + Lang.ReportSelectDay + " " + strReportType; break;
                case CountType.Yearly: timeString = DateTime.Now.Year.ToString() + "." + Lang.ReportSelectYear + " " + strReportType; break;
                case CountType.Montly: timeString = DateTime.Now.Month.ToString() + "." + Lang.ReportSelectMonth + " " + strReportType; break;
                case CountType.Weekly: timeString = myCal.GetWeekOfYear(DateTime.Now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek) + "." + Lang.ReportSelectWeek + " " + strReportType; break;
                // report date bug fix.
                case CountType.FromTheBeginning: start = user.register_date; break;
            }

            string torunLogoPath = System.Windows.Forms.Application.StartupPath.ToString() + "//torun-logo-2.png";

            Microsoft.Office.Interop.Excel.Application excelPage = new Microsoft.Office.Interop.Excel.Application();

            Workbook workbook = excelPage.Workbooks.Add(Type.Missing);
            Worksheet worksheet = workbook.Sheets[1];

            int startColumn = 1, startRow = 1;

            Range rang2 = worksheet.Cells[startRow, startColumn];
            worksheet.Shapes.AddPicture(torunLogoPath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 180, 60);
            rang2.RowHeight = 58;

            Range rang3;

            if (reportType == ReportType.Both)
            {
                rang3 = worksheet.Range[worksheet.Cells[startRow, 2], worksheet.Cells[startRow, 5]];
                worksheet.Cells[startRow, 5] = DateTime.Now.ToShortDateString() + " " + Lang.ReportHasDate + " " + timeString + " " + Lang.MainPageMenuReport + " " + fullname + " " + Lang.ReportCreatedBy;
            }
            else
            {
                rang3 = worksheet.Range[worksheet.Cells[startRow, 2], worksheet.Cells[startRow, 3]];
                worksheet.Cells[startRow, 3] = DateTime.Now.ToShortDateString() + " " + Lang.ReportHasDate + " " + timeString + " " + Lang.MainPageMenuReport + " " + fullname + " " + Lang.ReportCreatedBy;
            }
            rang3.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            rang3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            rang3.Merge();
            rang2.RowHeight = 55;
            startRow++; startColumn = 1;

            int[] columnWidths;

            if (reportType == ReportType.OnlyWorkDone) columnWidths = new int[] { 30, 165 };
            else if (reportType == ReportType.OnlyPlan) columnWidths = new int[] { 30, 165 };
            else columnWidths = new int[] { 30, 10, 19, 135 };

            for (int i = 0; i < columnWidths.Length; i++)
            {
                Range rangeRequest = worksheet.Cells[startRow, startColumn];
                rangeRequest.ColumnWidth = columnWidths[i];
                rangeRequest.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                rangeRequest.Font.Size = 13;
                rangeRequest.Font.Bold = true;
                rangeRequest.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                startColumn++;
            }
            startRow++;

            if (reportType == ReportType.OnlyWorkDone)
            {
                while (end >= start)
                {
                    List<DB.WorkDoneList> workDone = db.ListWorkDone(user, start.Date, OrderBy.AddedTimeAsc);
                    if (workDone.Count >= 1)
                    {
                        Range rang = worksheet.Range[worksheet.Cells[startRow, 1], worksheet.Cells[startRow, 2]];
                        worksheet.Cells[startRow, 2] = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)start.DayOfWeek] + " (" + start.ToShortDateString() + @")";
                        rang.Font.Size = 15;
                        rang.Font.Bold = true;
                        rang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        rang.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.IndianRed);
                        rang.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rang.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rang.Merge();
                        startRow++;

                        foreach (var item in workDone)
                        {
                            worksheet.Cells[startRow, 1] = item.RequestNumber;
                            worksheet.Cells[startRow, 2] = item.Description;
                            startRow++;
                        }
                    }
                    start = start.AddDays(1);
                }
            }
            else if (reportType == ReportType.OnlyPlan)
            {
                while (end >= start)
                {
                    List<DB.WeeklyPlan> weeklyPlans = db.ListWeeklyPlanbyDate(user, start.Date);
                    if (weeklyPlans.Count >= 1)
                    {
                        Range rang = worksheet.Range[worksheet.Cells[startRow, 1], worksheet.Cells[startRow, 2]];
                        worksheet.Cells[startRow, 2] = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)start.DayOfWeek] + " (" + start.ToShortDateString() + @")";
                        rang.Font.Size = 15;
                        rang.Font.Bold = true;
                        rang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        rang.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.IndianRed);
                        rang.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rang.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rang.Merge();
                        startRow++;

                        foreach (var item in weeklyPlans)
                        {
                            worksheet.Cells[startRow, 1] = item.RequestNumber;
                            worksheet.Cells[startRow, 2] = item.WorkDescription;
                            startRow++;
                        }
                    }
                    start = start.AddDays(1);
                }
            }
            else if (reportType == ReportType.Both)
            {
                while (end >= start)
                {
                    List<DB.WorkDoneandPlans> workPlans = db.GetWorkDoneAndPlansbyDate(user, start.Date);
                    if (workPlans.Count >= 1)
                    {
                        Range rang = worksheet.Range[worksheet.Cells[startRow, 1], worksheet.Cells[startRow, 4]];
                        worksheet.Cells[startRow, 4] = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)start.DayOfWeek] + " (" + start.ToShortDateString() + @")";
                        rang.Font.Size = 15;
                        rang.Font.Bold = true;
                        rang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        rang.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.IndianRed);
                        rang.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rang.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rang.Merge();
                        startRow++;

                        Range rng = worksheet.Cells[startRow, 1];
                        rng.RowHeight = 15;

                        foreach (var item in workPlans)
                        {
                            worksheet.Cells[startRow, 1] = item.RequestNumber;
                            worksheet.Cells[startRow, 2] = item.PlanDate;
                            worksheet.Cells[startRow, 3] = item.WorkDoneDate;
                            worksheet.Cells[startRow, 4] = item.WorkDescription;
                            startRow++;
                        }
                    }
                    start = start.AddDays(1);
                }
            }

            Range select = worksheet.Cells[7, 1];
            select.Select(); // select empty a cell 

            // report request statictics
            string statictic_str = Lang.MainPageTotalRequest + " " + db.GetRequestCount(1, user, countType, selectedDates).ToString() + "\n" +
                                   Lang.MainPageOpenRequest + " " + db.GetRequestCount(2, user, countType, selectedDates).ToString() + "\n" +
                                   Lang.MainPageClosedRequest + " " + db.GetRequestCount(3, user, countType, selectedDates).ToString() + "\n";

            var priorities = new List<PriorityType>() { PriorityType.Urgent, PriorityType.High, PriorityType.Normal, PriorityType.Low, PriorityType.Project };

            foreach (var item in priorities)
            {
                statictic_str += Functions.PriorityString(item) + " " + Lang.ReportRequestNumber + " : " + db.GetRequestCount(item, user, countType, selectedDates) + "\n";
            }

            startRow++;

            if (reportType == ReportType.Both)
            {
                Range rang = worksheet.Range[worksheet.Cells[startRow, 1], worksheet.Cells[startRow, 4]];
                worksheet.Cells[startRow, 4] = statictic_str;
                rang.Font.Size = 13;
                rang.Font.Bold = true;
                rang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                rang.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                rang.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                rang.Merge();
            }
            else
            {
                Range rang = worksheet.Range[worksheet.Cells[startRow, 1], worksheet.Cells[startRow, 2]];
                worksheet.Cells[startRow, 2] = statictic_str;
                rang.Font.Size = 13;
                rang.Font.Bold = true;
                rang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                rang.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                rang.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                rang.Merge();
            }

            // control are the year and month directories exists
            int order = 1;
            string outputFilePath = ReportFolderProcess() + @"\" + DateTime.Now.ToString("dd-MM-yyy") + "-" + FileNames.FILENAME_REPORTEXCEL; // create outputfile path
            while (true)
            {
                if (File.Exists(outputFilePath))
                {
                    outputFilePath = ReportFolderProcess() + @"\" + DateTime.Now.ToString("dd-MM-yyy") + "-" + order + "-" + FileNames.FILENAME_REPORTEXCEL; // create outputfile path
                    order++;
                }
                else break;
            }

            // save excel file to user folder
            workbook.SaveAs(outputFilePath, XlFileFormat.xlOpenXMLWorkbook, Type.Missing,
            Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange,
            XlSaveConflictResolution.xlUserResolution, true,
            Type.Missing, Type.Missing, Type.Missing);

            workbook.Close();
            excelPage.Quit();
            System.Diagnostics.Process.Start(outputFilePath);
            System.Diagnostics.Process.Start(ReportFolderProcess());
        }
        private static string ReportFolderProcess()
        {
            if (!Directory.Exists(getFilePath(FileNames.FILENAME_REPORT_FOLDER)))
                Directory.CreateDirectory(getFilePath(FileNames.FILENAME_REPORT_FOLDER));

            if (!Directory.Exists(getFilePath(FileNames.FILENAME_REPORT_FOLDER + @"\" + DateTime.Now.Year.ToString())))
                Directory.CreateDirectory(getFilePath(FileNames.FILENAME_REPORT_FOLDER + @"\" + DateTime.Now.Year.ToString()));

            if (!Directory.Exists(getFilePath(FileNames.FILENAME_REPORT_FOLDER + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString())))
                Directory.CreateDirectory(getFilePath(FileNames.FILENAME_REPORT_FOLDER + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString()));

            return getFilePath(FileNames.FILENAME_REPORT_FOLDER + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString());
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
                if(openFileDialog.FileName != String.Empty)
                {
                    File.Delete(ProfilePhotoPath());
                    File.Copy(openFileDialog.FileName, ProfilePhotoPath());
                }
            }
        }
        public static string UserFilename { get; set; }
        private static string getFilePath(string file, bool withoutUser = false)
        {
            if (file == String.Empty) return System.Environment.CurrentDirectory + @"\" + UserFilename;
            if (withoutUser) return System.Environment.CurrentDirectory + @"\" + file;
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

        public static bool FileExists(string filename, bool withoutUser = false)
        {
            return File.Exists(getFilePath(filename, withoutUser));
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
