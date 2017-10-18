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
    
    public partial class INV_ChemStockDaily
    {
        public long TransectionID { get; set; }
        public byte StoreID { get; set; }
        public System.DateTime StockDate { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<byte> UnitID { get; set; }
        public Nullable<byte> PackSize { get; set; }
        public Nullable<byte> SizeUnit { get; set; }
        public Nullable<decimal> OpeningQty { get; set; }
        public Nullable<decimal> ReceiveQty { get; set; }
        public Nullable<decimal> IssueQty { get; set; }
        public Nullable<decimal> ClosingQty { get; set; }
        public Nullable<decimal> PackOpeningQty { get; set; }
        public Nullable<decimal> PackReceiveQty { get; set; }
        public Nullable<decimal> PackIssueQty { get; set; }
        public Nullable<decimal> PackClosingQty { get; set; }
        public Nullable<int> ManufacturerID { get; set; }
        public Nullable<int> RefTransactionID { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual Sys_ChemicalItem Sys_ChemicalItem { get; set; }
        public virtual Sys_Supplier Sys_Supplier { get; set; }
        public virtual User User { get; set; }
        public virtual Sys_Size Sys_Size { get; set; }
        public virtual User User1 { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual SYS_Store SYS_Store { get; set; }
        public virtual Sys_Unit Sys_Unit1 { get; set; }
    }
}
