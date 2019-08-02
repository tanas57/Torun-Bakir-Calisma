using System;
using System.Collections.Generic;
namespace Torun.Classes
{
    public static class Functions
    {
        public static List<DateTime> GetDateInterval(CountType countType)
        {
            DateTime timeStart, timeEnd;
            switch (countType)
            {
                case CountType.Daily:
                    timeStart = DateTime.Now.Date.AddSeconds(0);
                    timeEnd = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                    break;

                case CountType.Weekly:
                    timeStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday).Date;
                    timeEnd = timeStart.AddDays(5).Date;
                    break;

                case CountType.Montly:
                    timeStart = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1).Date;
                    timeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31).AddDays(1).AddSeconds(-1);
                    break;

                case CountType.Yearly:
                    timeStart = new DateTime(DateTime.Now.Year, 1, 1).Date;
                    timeEnd = new DateTime(DateTime.Now.Year, 12, 31).AddDays(1).AddSeconds(-1);
                    break;

                default:
                case CountType.FromTheBeginning:
                    timeStart = new DateTime(DateTime.Now.AddYears(-100).Year, 1, 1).Date;
                    timeEnd = new DateTime(DateTime.Now.Year,12, 31).AddDays(1).AddSeconds(-1);
                    break;
            }
            List<DateTime> dateTimes = new List<DateTime>();
            dateTimes.Add(timeStart);
            dateTimes.Add(timeEnd);
            return dateTimes;
        }
    }
}
