//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblLeaveDoc
    {
        public int leaveDocId { get; set; }
        public int leaveId { get; set; }
        public int fileId { get; set; }
        public string fileName { get; set; }
        public int statusId { get; set; }
        public string remarks { get; set; }
        public System.DateTime dated { get; set; }
        public int adminId { get; set; }
    }
}