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
    
    public partial class tblEmployeeTrainee
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public int applicantId { get; set; }
        public int statusId { get; set; }
        public bool isActive { get; set; }
        public int adminId { get; set; }
        public System.DateTime dated { get; set; }
    }
}
