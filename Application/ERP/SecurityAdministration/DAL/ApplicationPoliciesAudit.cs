//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecurityAdministration.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApplicationPoliciesAudit
    {
        public int ID { get; set; }
        public byte PolicyID { get; set; }
        public byte ApplicationID { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string SetBy { get; set; }
        public System.DateTime SetOn { get; set; }
        public string DataStatusFlag { get; set; }
    }
}
