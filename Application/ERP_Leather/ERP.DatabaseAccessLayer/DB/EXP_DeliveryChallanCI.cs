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
    
    public partial class EXP_DeliveryChallanCI
    {
        public long DeliverChallanID { get; set; }
        public long CIID { get; set; }
        public Nullable<long> PLID { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual EXP_CI EXP_CI { get; set; }
        public virtual EXP_DeliveryChallan EXP_DeliveryChallan { get; set; }
        public virtual User User { get; set; }
        public virtual EXP_PackingList EXP_PackingList { get; set; }
        public virtual User User1 { get; set; }
    }
}