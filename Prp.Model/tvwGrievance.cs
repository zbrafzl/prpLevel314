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
    
    public partial class tvwGrievance
    {
        public int grievanceId { get; set; }
        public int grievanceTypeId { get; set; }
        public int applicantId { get; set; }
        public string subject { get; set; }
        public string application { get; set; }
        public System.DateTime dated { get; set; }
        public string grievanceType { get; set; }
        public int appearanceId { get; set; }
        public int attendanceNo { get; set; }
        public int appearanceTypeId { get; set; }
        public string appearanceType { get; set; }
        public int guardianId { get; set; }
        public string guardian { get; set; }
        public string guardianName { get; set; }
        public string guardianContactNumber { get; set; }
        public string actionComments { get; set; }
        public System.DateTime datedAppearance { get; set; }
        public int actionTypeId { get; set; }
        public string actionType { get; set; }
        public int adminIdAppearance { get; set; }
        public string adminAppearance { get; set; }
        public int statusId { get; set; }
        public string status { get; set; }
        public System.DateTime datedStatus { get; set; }
        public int adminIdStatus { get; set; }
        public string adminName { get; set; }
    }
}
