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
    
    public partial class tvwResearchJournal
    {
        public int researchJournalId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string url { get; set; }
        public int typeId { get; set; }
        public string typeName { get; set; }
        public string batch { get; set; }
        public int batchId { get; set; }
        public int regionId { get; set; }
        public string regionName { get; set; }
        public string displine { get; set; }
        public int displineId { get; set; }
        public bool isActive { get; set; }
        public int adminId { get; set; }
        public string adminName { get; set; }
        public System.DateTime dated { get; set; }
    }
}
