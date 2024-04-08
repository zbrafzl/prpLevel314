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
    
    public partial class tblApplicantLeaveCorrespondence
    {
        public int leaveCorrespondenceId { get; set; }
        public Nullable<int> applicantLeaveId { get; set; }
        public Nullable<int> correspondenceFrom { get; set; }
        public Nullable<int> correspondenceTo { get; set; }
        public string remarks { get; set; }
        public string imageLeaveForm { get; set; }
        public string imageAffidavit { get; set; }
        public string imageMedical { get; set; }
        public string imageMaternity { get; set; }
        public string imagePGAC { get; set; }
        public string imageForwarding { get; set; }
        public string imageSurety { get; set; }
        public string imageAttorney { get; set; }
        public string imageVisa { get; set; }
        public string imagePurpose { get; set; }
        public Nullable<bool> applicationValidity { get; set; }
        public Nullable<bool> affidavitValidity { get; set; }
        public Nullable<bool> medicalValidity { get; set; }
        public Nullable<bool> matenrityValidity { get; set; }
        public Nullable<bool> forwardingValidity { get; set; }
        public Nullable<bool> ipgacValidity { get; set; }
        public Nullable<bool> suretyValidity { get; set; }
        public Nullable<bool> attorneyValidity { get; set; }
        public Nullable<bool> purposeValidity { get; set; }
        public string applicationRemarks { get; set; }
        public string affidavitRemarks { get; set; }
        public string medicalRemarks { get; set; }
        public string matenrityRemarks { get; set; }
        public string forwardingRemarks { get; set; }
        public string ipgacRemarks { get; set; }
        public string suretyRemarks { get; set; }
        public string attorneyRemarks { get; set; }
        public string purposeRemarks { get; set; }
        public string imageAttachment { get; set; }
        public Nullable<int> actionPerformed { get; set; }
        public Nullable<System.DateTime> startDated { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public Nullable<System.DateTime> dated { get; set; }
        public Nullable<int> adminId { get; set; }
        public Nullable<int> applicantId { get; set; }
        public Nullable<int> leaveTypeId { get; set; }
        public string imageRTMC { get; set; }
        public Nullable<bool> RTMCValidity { get; set; }
        public string RTMCRemarks { get; set; }
        public string imagePreviousLeaveReport { get; set; }
        public Nullable<bool> imagePreviousLeaveReportValidy { get; set; }
        public string imagePreviousLeaveReportRemarks { get; set; }
    }
}