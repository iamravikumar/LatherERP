//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.DatabaseAccessLayer.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApplicationPolicy
    {
        public byte PolicyID { get; set; }
        public byte ApplicationID { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int SetBy { get; set; }
        public System.DateTime SetOn { get; set; }
    
        public virtual Application Application { get; set; }
    }
}