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
    
    public partial class AdditionalOperationPermission
    {
        public int UserID { get; set; }
        public byte OperationID { get; set; }
        public bool HaveAccess { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DateTime SetOn { get; set; }
        public int SetBy { get; set; }
    
        public virtual ScreenOperation ScreenOperation { get; set; }
        public virtual User User { get; set; }
    }
}
