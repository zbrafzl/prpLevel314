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
    
    public partial class tblFaculty
    {
        public int facultyId { get; set; }
        public int levelId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string detail { get; set; }
        public bool isActive { get; set; }
        public int sortOrder { get; set; }
        public System.DateTime dated { get; set; }
        public int adminId { get; set; }
    }
}
