namespace Torun.Classes
{
    public enum StatusType
    {
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
        AddedTime = 0,
        PriorityAsc = 1,
        PriorityDesc = 2,
        NameDesc = 3,
        NameAsc = 4,
    }
    public enum CountType
    {
        Daily = 0,
        Weekly = 1,
        Montly = 2,
        Yearly = 3,
        FromTheBeginning = 4
    }
}
