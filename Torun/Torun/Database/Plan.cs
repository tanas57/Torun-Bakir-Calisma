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
    
    public partial class Plan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plan()
        {
            this.WorkDones = new HashSet<WorkDone>();
        }
    
        public int id { get; set; }
        public int work_id { get; set; }
        public System.DateTime add_time { get; set; }
        public System.DateTime work_plan_time { get; set; }
        public byte status { get; set; }
    
        public virtual TodoList TodoList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkDone> WorkDones { get; set; }
    }
}
