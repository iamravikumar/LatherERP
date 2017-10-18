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
    
    public partial class PRD_ChemProdReq
    {
        public PRD_ChemProdReq()
        {
            this.INV_StoreTransRequest = new HashSet<INV_StoreTransRequest>();
            this.PRD_ChemProdReqItem = new HashSet<PRD_ChemProdReqItem>();
            this.PRD_CLProductionChemical = new HashSet<PRD_CLProductionChemical>();
            this.PRD_WBProductionChemical = new HashSet<PRD_WBProductionChemical>();
            this.PRD_FNProductionChemical = new HashSet<PRD_FNProductionChemical>();
        }
    
        public int RequisitionID { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionCategory { get; set; }
        public string RequisitionType { get; set; }
        public byte RequisitionFrom { get; set; }
        public byte RequisitionTo { get; set; }
        public Nullable<byte> RequiredByTime { get; set; }
        public Nullable<int> ReqRaisedBy { get; set; }
        public Nullable<System.DateTime> ReqRaisedOn { get; set; }
        public string RequisitionNote { get; set; }
        public Nullable<int> RecipeFor { get; set; }
        public Nullable<int> RecipeID { get; set; }
        public string ArticleNo { get; set; }
        public string LeatherSize { get; set; }
        public Nullable<byte> SizeUnit { get; set; }
        public string Selection { get; set; }
        public string Thickness { get; set; }
        public Nullable<byte> ThicknessUnit { get; set; }
        public Nullable<int> BuyerID { get; set; }
        public Nullable<int> BuyerOrderID { get; set; }
        public Nullable<int> JobOrderID { get; set; }
        public string JobOrderNo { get; set; }
        public Nullable<decimal> ProductionQty { get; set; }
        public string RequisitionStatus { get; set; }
        public string RequisitionState { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public string ApprovalAdvice { get; set; }
        public string ApprovalAdviceNote { get; set; }
        public Nullable<int> CheckedBy { get; set; }
        public Nullable<System.DateTime> CheckDate { get; set; }
        public Nullable<int> PreparedBy { get; set; }
        public Nullable<System.DateTime> PreparedOn { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual ICollection<INV_StoreTransRequest> INV_StoreTransRequest { get; set; }
        public virtual User User { get; set; }
        public virtual Sys_Buyer Sys_Buyer { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual User User3 { get; set; }
        public virtual Sys_ProductionProces Sys_ProductionProces { get; set; }
        public virtual PRD_Recipe PRD_Recipe { get; set; }
        public virtual User User4 { get; set; }
        public virtual SYS_Store SYS_Store { get; set; }
        public virtual SYS_Store SYS_Store1 { get; set; }
        public virtual User User5 { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_Unit Sys_Unit1 { get; set; }
        public virtual ICollection<PRD_ChemProdReqItem> PRD_ChemProdReqItem { get; set; }
        public virtual ICollection<PRD_CLProductionChemical> PRD_CLProductionChemical { get; set; }
        public virtual ICollection<PRD_WBProductionChemical> PRD_WBProductionChemical { get; set; }
        public virtual ICollection<PRD_FNProductionChemical> PRD_FNProductionChemical { get; set; }
    }
}