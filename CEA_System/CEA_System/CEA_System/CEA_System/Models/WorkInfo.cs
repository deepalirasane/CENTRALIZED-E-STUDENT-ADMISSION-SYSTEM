//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CEA_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkInfo
    {
        public int WorkInfoId { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Description { get; set; }
        public string Achievement { get; set; }
        public Nullable<int> StudentId { get; set; }
    
        public virtual Student_Profile Student_Profile { get; set; }
    }
}
