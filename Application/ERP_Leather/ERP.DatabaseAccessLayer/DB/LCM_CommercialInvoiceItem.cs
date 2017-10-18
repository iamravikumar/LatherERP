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
    
    public partial class LCM_CommercialInvoiceItem
    {
        public long CIItemID { get; set; }
        public Nullable<int> CIID { get; set; }
        public int ItemID { get; set; }
        public Nullable<byte> PackSize { get; set; }
        public Nullable<byte> SizeUnit { get; set; }
        public Nullable<int> PackQty { get; set; }
        public decimal CIQty { get; set; }
        public byte CIUnit { get; set; }
        public decimal CIUnitPrice { get; set; }
        public Nullable<decimal> CITotalPrice { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> ManufacturerID { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual LCM_CommercialInvoice LCM_CommercialInvoice { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_ChemicalItem Sys_ChemicalItem { get; set; }
        public virtual Sys_Supplier Sys_Supplier { get; set; }
        public virtual Sys_Size Sys_Size { get; set; }
        public virtual Sys_Unit Sys_Unit1 { get; set; }
        public virtual Sys_Supplier Sys_Supplier1 { get; set; }
    }
}
