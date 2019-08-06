namespace Torun.Classes
{
    public enum StatusType
    {
        Deleted = 0,
        Added = 1,
        InProcess = 2,
        Closed = 3,
        Edited = 4,
        Planned = 5
    }
    public enum PriorityType
    {
        Low = 0,
        Normal = 1,
        High = 2,
        Urgent = 3,
        Project = 4
    }
    public enum OrderBy
    {
        AddedTimeAsc = 0,
        AddedTimeDesc = 1,
        PriorityAsc = 2,
        PriorityDesc = 3,
        NameDesc = 4,
        NameAsc = 5,
    }
    public enum CountType
    {
        Daily = 0,
        Weekly = 1,
        Montly = 2,
        Yearly = 3,
        FromTheBeginning = 4
    }
    public enum ReportType
    {
        OnlyPlan = 0,
        OnlyWorkDone = 1,
        Both = 2
    }
    public enum TorunLanguage
    {
        TR = 0,
        EN = 1
    }
}
