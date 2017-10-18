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
    
    public partial class EXP_PackingList
    {
        public EXP_PackingList()
        {
            this.EXP_BillofLading = new HashSet<EXP_BillofLading>();
            this.EXP_CnFBill = new HashSet<EXP_CnFBill>();
            this.EXP_PLPI = new HashSet<EXP_PLPI>();
            this.EXP_DeliveryChallanCI = new HashSet<EXP_DeliveryChallanCI>();
            this.EXP_ExportForm = new HashSet<EXP_ExportForm>();
            this.EXP_TransportChallanCI = new HashSet<EXP_TransportChallanCI>();
        }
    
        public long PLID { get; set; }
        public string PLNo { get; set; }
        public Nullable<System.DateTime> PLDate { get; set; }
        public Nullable<long> CIID { get; set; }
        public string BalesNo { get; set; }
        public Nullable<decimal> BaleQty { get; set; }
        public Nullable<decimal> TotalPcs { get; set; }
        public Nullable<decimal> TotalSide { get; set; }
        public Nullable<decimal> MeterCIQty { get; set; }
        public Nullable<decimal> FootCIQty { get; set; }
        public Nullable<decimal> PLNetWeight { get; set; }
        public Nullable<byte> NetWeightUnit { get; set; }
        public Nullable<decimal> PLGrossWeight { get; set; }
        public Nullable<byte> GrossWeightUnit { get; set; }
        public string PLMarks { get; set; }
        public string PLNote { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> CheckedBy { get; set; }
        public Nullable<System.DateTime> CheckDate { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual ICollection<EXP_BillofLading> EXP_BillofLading { get; set; }
        public virtual EXP_CI EXP_CI { get; set; }
        public virtual ICollection<EXP_CnFBill> EXP_CnFBill { get; set; }
        public virtual User User { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual User User1 { get; set; }
        public virtual Sys_Unit Sys_Unit1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual ICollection<EXP_PLPI> EXP_PLPI { get; set; }
        public virtual ICollection<EXP_DeliveryChallanCI> EXP_DeliveryChallanCI { get; set; }
        public virtual ICollection<EXP_ExportForm> EXP_ExportForm { get; set; }
        public virtual ICollection<EXP_TransportChallanCI> EXP_TransportChallanCI { get; set; }
    }
}
