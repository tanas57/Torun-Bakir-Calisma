//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Torun.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Setting
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public byte set_countType { get; set; }
        public bool set_autoOpen { get; set; }
        public bool set_autoBackup { get; set; }
        public byte set_backupTimeInterval { get; set; }
        public byte set_defaultReportInterval { get; set; }
        public byte set_defaultReportType { get; set; }
        public string routineWorkTitle1 { get; set; }
        public string routineWorkTitle2 { get; set; }
    
        public virtual User User { get; set; }
    }
}
