using System;

namespace Torun.Classes
{
    public static class Settings
    {
        public static CountType MainRequestCountType { get; set; }
        public static bool AutoOpen { get; set; }
        public static bool AutoBackup { get; set; }
        public static CountType BackupTimeInterval { get; set; }
        public static CountType DefaultReportInterval { get; set; }
    }
}
