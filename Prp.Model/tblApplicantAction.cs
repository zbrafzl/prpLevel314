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
    
    public partial class tblApplicantAction
    {
        public int actionId { get; set; }
        public int applicantId { get; set; }
        public int specialityJobId { get; set; }
        public string image { get; set; }
        public int typeId { get; set; }
        public int categoryId { get; set; }
        public bool isDocsCollected { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int statusId { get; set; }
        public string remarks { get; set; }
        public System.DateTime dated { get; set; }
        public int adminId { get; set; }
    }
}
