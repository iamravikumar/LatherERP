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
    
    public partial class LCM_PackingList
    {
        public LCM_PackingList()
        {
            this.LCM_PackingListItem = new HashSet<LCM_PackingListItem>();
            this.LCM_PackingListPacks = new HashSet<LCM_PackingListPacks>();
            this.PRQ_ChemFrgnPurcRecvPL = new HashSet<PRQ_ChemFrgnPurcRecvPL>();
            this.LCM_LCFileRecord = new HashSet<LCM_LCFileRecord>();
        }
    
        public int PLID { get; set; }
        public string PLNo { get; set; }
        public Nullable<System.DateTime> PLDate { get; set; }
        public Nullable<int> LCID { get; set; }
        public string LCNo { get; set; }
        public Nullable<int> CIID { get; set; }
        public string CINo { get; set; }
        public Nullable<decimal> PLNetWeight { get; set; }
        public Nullable<byte> NetWeightUnit { get; set; }
        public Nullable<decimal> PLGrossWeight { get; set; }
        public Nullable<byte> GrossWeightUnit { get; set; }
        public string PLNote { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> CheckedBy { get; set; }
        public Nullable<System.DateTime> CheckDate { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual LCM_CommercialInvoice LCM_CommercialInvoice { get; set; }
        public virtual LCM_CommercialInvoice LCM_CommercialInvoice1 { get; set; }
        public virtual LCM_LCOpening LCM_LCOpening { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual ICollection<LCM_PackingListItem> LCM_PackingListItem { get; set; }
        public virtual ICollection<LCM_PackingListPacks> LCM_PackingListPacks { get; set; }
        public virtual ICollection<PRQ_ChemFrgnPurcRecvPL> PRQ_ChemFrgnPurcRecvPL { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_Unit Sys_Unit1 { get; set; }
        public virtual ICollection<LCM_LCFileRecord> LCM_LCFileRecord { get; set; }
    }
}
