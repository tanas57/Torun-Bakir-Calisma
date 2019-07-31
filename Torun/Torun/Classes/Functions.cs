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
                    timeStart = DateTime.Now.Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;

                case CountType.Weekly:
                    timeStart = DateTime.Now.AddDays(-7).Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;

                case CountType.Montly:
                    timeStart = DateTime.Now.AddMonths(-1).Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;

                case CountType.Yearly:
                    timeStart = DateTime.Now.AddYears(-1).Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;

                default:
                case CountType.FromTheBeginning:
                    timeStart = DateTime.Now.AddYears(-100).Date;
                    timeEnd = DateTime.Now.AddDays(1).Date;
                    break;
            }
            List<DateTime> dateTimes = new List<DateTime>();
            dateTimes.Add(timeStart);
            dateTimes.Add(timeEnd);
            return dateTimes;
        }
    }
}
