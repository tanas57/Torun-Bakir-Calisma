using System;
using System.Collections.Generic;
using Torun.Database;
using Torun.Lang;
using Microsoft.Office.Interop.Excel;
namespace Torun.Classes
{
    public static class Functions
    {
        public static ILanguage Lang => CurrentLanguage.Language;
        public static List<DateTime> GetDateInterval(CountType countType, User user = null)
        {
            DateTime timeStart, timeEnd;
            switch (countType)
            {
                case CountType.Daily:
                    timeStart = DateTime.Now.Date.AddSeconds(0);
                    timeEnd = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                    break;

                default:
                case CountType.Weekly:
                    timeStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday).Date;
                    timeEnd = timeStart.AddDays(5).Date.AddSeconds(-1);
                    break;

                case CountType.Montly:
                    timeStart = DateTime.Now.AddDays(-DateTime.Now.Day).Date.AddSeconds(1).AddDays(1);
                    timeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddSeconds(-1);
                    break;

                case CountType.Yearly:
                    timeStart = new DateTime(DateTime.Now.Year, 1, 1).Date;
                    timeEnd = new DateTime(DateTime.Now.Year, 12, 1).AddMonths(1).AddSeconds(-1);
                    break;

                case CountType.FromTheBeginning:
                    timeStart = user.register_date;
                    timeEnd = new DateTime(DateTime.Now.Year,12, 31).AddDays(1).AddSeconds(-1);
                    break;
            }
            List<DateTime> dateTimes = new List<DateTime>();
            dateTimes.Add(timeStart);
            dateTimes.Add(timeEnd);
            return dateTimes;
        }
        public static string ConvertCountTypeToString(CountType countType)
        {
            switch (countType)
            {
                case CountType.Daily: return Lang.SettingsRadioDaily;
                case CountType.Weekly: return Lang.SettingsRadioWeekly;
                case CountType.Montly: return Lang.SettingsRadioMonthly;
                case CountType.Yearly: return Lang.SettingsRadioYearly;
                default:
                case CountType.FromTheBeginning: return Lang.SettingsRadioBeforeStart;
            }
        }
        public static string PriorityString(PriorityType priorityType)
        {
            switch (priorityType)
            {
                case PriorityType.Low: return Lang.ComboboxPriorityLow;
                default:
                case PriorityType.Normal: return Lang.ComboboxPriorityNormal;
                case PriorityType.High: return Lang.ComboboxPriorityHigh;
                case PriorityType.Urgent: return Lang.ComboboxPriorityUrgent;
                case PriorityType.Project: return Lang.ComboboxPriorityProject;
            }
        }
        public static string StatusString(StatusType statusType)
        {
            switch (statusType)
            {
                default:
                case StatusType.Added: return Lang.ComboboxStatusNew;
                case StatusType.InProcess: return Lang.ComboboxStatusInProcess;
                case StatusType.Closed: return Lang.ComboboxStatusClosed;
                case StatusType.Edited: return Lang.ComboboxStatusEdited;
                case StatusType.Planned: return Lang.ComboboxStatusPlanned;
            }
        }
        /// <summary>
        /// Creates a new cell with settings
        /// </summary>
        /// <param name="cell">Current selected cell</param>
        /// <param name="bgColor">Cell background color</param>
        /// <param name="fontColor">Cell font color</param>
        /// <param name="columnWidth">Column width</param>
        /// <param name="rowHeight">Cell row height</param>
        /// <param name="borderColor">Cell border color if is exists</param>
        /// <param name="value2">Cell value</param>
        /// <param name="font">Cell font family</param>
        /// <param name="bold">is font bold</param>
        /// <param name="fontsize">font size</param>
        /// <param name="textcenter">is text in cell center</param>
        public static void ExcelCellProcess(Range cell, int bgColor, int fontColor, int borderColor, int columnWidth, int rowHeight, string value2, string font, bool bold, int fontsize, bool textcenter = false)
        {
            cell.Interior.Color = bgColor;
            cell.ColumnWidth = columnWidth;
            cell.RowHeight = rowHeight;
            cell.Font.Color = fontColor;
            cell.Value2 = value2;
            cell.Font.Name = font;
            cell.Font.Bold = bold;
            cell.Font.Size = fontsize;
            if(textcenter) cell.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            if (borderColor != -1)
            {
                Microsoft.Office.Interop.Excel.Borders border = cell.Borders;
                cell.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 1;
                border.Color = borderColor;
            }
            if (textcenter)
            {
                cell.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                cell.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            }
        }
    }
}
