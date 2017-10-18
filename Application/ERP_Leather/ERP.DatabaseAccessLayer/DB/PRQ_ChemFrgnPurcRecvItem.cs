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
    
    public partial class PRQ_ChemFrgnPurcRecvItem
    {
        public long ReceiveItemID { get; set; }
        public Nullable<int> PLReceiveID { get; set; }
        public Nullable<int> ReceiveID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<decimal> ChallanQty { get; set; }
        public Nullable<decimal> ReceiveQty { get; set; }
        public Nullable<byte> UnitID { get; set; }
        public Nullable<byte> PackSize { get; set; }
        public Nullable<byte> SizeUnit { get; set; }
        public Nullable<decimal> PackQty { get; set; }
        public string BatchNo { get; set; }
        public string LotNo { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> ManufacturerID { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual PRQ_ChemFrgnPurcRecv PRQ_ChemFrgnPurcRecv { get; set; }
        public virtual Sys_ChemicalItem Sys_ChemicalItem { get; set; }
        public virtual Sys_Supplier Sys_Supplier { get; set; }
        public virtual User User { get; set; }
        public virtual Sys_Size Sys_Size { get; set; }
        public virtual PRQ_ChemFrgnPurcRecvPL PRQ_ChemFrgnPurcRecvPL { get; set; }
        public virtual User User1 { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_Supplier Sys_Supplier1 { get; set; }
        public virtual Sys_Unit Sys_Unit1 { get; set; }
    }
}
