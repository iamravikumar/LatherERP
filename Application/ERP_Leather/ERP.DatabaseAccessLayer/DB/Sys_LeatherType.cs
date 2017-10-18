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
    
    public partial class Sys_LeatherType
    {
        public Sys_LeatherType()
        {
            this.Inv_LeatherIssueItem = new HashSet<Inv_LeatherIssueItem>();
            this.Inv_LeatherStockAdjustItem = new HashSet<Inv_LeatherStockAdjustItem>();
            this.Inv_LeatherStockAdjustRequest = new HashSet<Inv_LeatherStockAdjustRequest>();
            this.Inv_LeatherTransferReceiveItem = new HashSet<Inv_LeatherTransferReceiveItem>();
            this.Inv_StockDaily = new HashSet<Inv_StockDaily>();
            this.Inv_StockItem = new HashSet<Inv_StockItem>();
            this.Inv_StockSupplier = new HashSet<Inv_StockSupplier>();
            this.INV_WetBlueIssueItem = new HashSet<INV_WetBlueIssueItem>();
            this.INV_WetBlueSelectionStock = new HashSet<INV_WetBlueSelectionStock>();
            this.INV_WetBlueStockDaily = new HashSet<INV_WetBlueStockDaily>();
            this.INV_WetBlueStockItem = new HashSet<INV_WetBlueStockItem>();
            this.INV_WetBlueStockSupplier = new HashSet<INV_WetBlueStockSupplier>();
            this.PRD_WBProductionPurchase = new HashSet<PRD_WBProductionPurchase>();
            this.PRD_WBSellectionIssueItem = new HashSet<PRD_WBSellectionIssueItem>();
            this.PRD_WetBlueProductionStock = new HashSet<PRD_WetBlueProductionStock>();
            this.PRD_YearMonthSchedulePurchase = new HashSet<PRD_YearMonthSchedulePurchase>();
            this.INV_CLTransferFrom = new HashSet<INV_CLTransferFrom>();
            this.INV_CLTransferTo = new HashSet<INV_CLTransferTo>();
            this.INV_CrustBuyerStock = new HashSet<INV_CrustBuyerStock>();
            this.INV_CrustQCStock = new HashSet<INV_CrustQCStock>();
            this.INV_CrustStockDaily = new HashSet<INV_CrustStockDaily>();
            this.INV_CrustStockItem = new HashSet<INV_CrustStockItem>();
            this.PRD_WBSelectionItem = new HashSet<PRD_WBSelectionItem>();
            this.PRD_CrustLeatherProductionStock = new HashSet<PRD_CrustLeatherProductionStock>();
            this.INV_CrustLeatherReceiveItem = new HashSet<INV_CrustLeatherReceiveItem>();
            this.INV_FinishBuyerStock = new HashSet<INV_FinishBuyerStock>();
            this.INV_FinishLeatherIssueItem = new HashSet<INV_FinishLeatherIssueItem>();
            this.INV_FinishLeatherReceiveItem = new HashSet<INV_FinishLeatherReceiveItem>();
            this.INV_FinishStockDaily = new HashSet<INV_FinishStockDaily>();
            this.INV_FinishStockItem = new HashSet<INV_FinishStockItem>();
            this.INV_FNTransferFrom = new HashSet<INV_FNTransferFrom>();
            this.INV_FNTransferTo = new HashSet<INV_FNTransferTo>();
            this.PRD_FinishLeatherProductionStock = new HashSet<PRD_FinishLeatherProductionStock>();
            this.PRD_FinishLeatherQCItem = new HashSet<PRD_FinishLeatherQCItem>();
            this.INV_FinishBuyerQCStock = new HashSet<INV_FinishBuyerQCStock>();
            this.INV_FinishOwnQCStock = new HashSet<INV_FinishOwnQCStock>();
            this.PRD_CrustLeatherQCItem = new HashSet<PRD_CrustLeatherQCItem>();
            this.INV_CrustLeatherIssueItem = new HashSet<INV_CrustLeatherIssueItem>();
            this.EXP_CIPIItem = new HashSet<EXP_CIPIItem>();
            this.EXP_PLPIItemColor = new HashSet<EXP_PLPIItemColor>();
            this.PRD_CLProductionItem = new HashSet<PRD_CLProductionItem>();
            this.PRD_FNProductionItem = new HashSet<PRD_FNProductionItem>();
            this.PRD_YearMonthCrustReqItem = new HashSet<PRD_YearMonthCrustReqItem>();
            this.PRD_YearMonthCrustScheduleItem = new HashSet<PRD_YearMonthCrustScheduleItem>();
            this.PRD_YearMonthFinishReqItem = new HashSet<PRD_YearMonthFinishReqItem>();
            this.PRD_YearMonthFinishScheduleItem = new HashSet<PRD_YearMonthFinishScheduleItem>();
            this.INV_FinishPackingStock = new HashSet<INV_FinishPackingStock>();
            this.EXP_PIItem = new HashSet<EXP_PIItem>();
            this.SLS_BuyerOrderItem = new HashSet<SLS_BuyerOrderItem>();
        }
    
        public byte LeatherTypeID { get; set; }
        public string LeatherTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public System.DateTime SetOn { get; set; }
        public int SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual ICollection<Inv_LeatherIssueItem> Inv_LeatherIssueItem { get; set; }
        public virtual ICollection<Inv_LeatherStockAdjustItem> Inv_LeatherStockAdjustItem { get; set; }
        public virtual ICollection<Inv_LeatherStockAdjustRequest> Inv_LeatherStockAdjustRequest { get; set; }
        public virtual ICollection<Inv_LeatherTransferReceiveItem> Inv_LeatherTransferReceiveItem { get; set; }
        public virtual ICollection<Inv_StockDaily> Inv_StockDaily { get; set; }
        public virtual ICollection<Inv_StockItem> Inv_StockItem { get; set; }
        public virtual ICollection<Inv_StockSupplier> Inv_StockSupplier { get; set; }
        public virtual ICollection<INV_WetBlueIssueItem> INV_WetBlueIssueItem { get; set; }
        public virtual ICollection<INV_WetBlueSelectionStock> INV_WetBlueSelectionStock { get; set; }
        public virtual ICollection<INV_WetBlueStockDaily> INV_WetBlueStockDaily { get; set; }
        public virtual ICollection<INV_WetBlueStockItem> INV_WetBlueStockItem { get; set; }
        public virtual ICollection<INV_WetBlueStockSupplier> INV_WetBlueStockSupplier { get; set; }
        public virtual ICollection<PRD_WBProductionPurchase> PRD_WBProductionPurchase { get; set; }
        public virtual ICollection<PRD_WBSellectionIssueItem> PRD_WBSellectionIssueItem { get; set; }
        public virtual ICollection<PRD_WetBlueProductionStock> PRD_WetBlueProductionStock { get; set; }
        public virtual ICollection<PRD_YearMonthSchedulePurchase> PRD_YearMonthSchedulePurchase { get; set; }
        public virtual ICollection<INV_CLTransferFrom> INV_CLTransferFrom { get; set; }
        public virtual ICollection<INV_CLTransferTo> INV_CLTransferTo { get; set; }
        public virtual ICollection<INV_CrustBuyerStock> INV_CrustBuyerStock { get; set; }
        public virtual ICollection<INV_CrustQCStock> INV_CrustQCStock { get; set; }
        public virtual ICollection<INV_CrustStockDaily> INV_CrustStockDaily { get; set; }
        public virtual ICollection<INV_CrustStockItem> INV_CrustStockItem { get; set; }
        public virtual ICollection<PRD_WBSelectionItem> PRD_WBSelectionItem { get; set; }
        public virtual ICollection<PRD_CrustLeatherProductionStock> PRD_CrustLeatherProductionStock { get; set; }
        public virtual ICollection<INV_CrustLeatherReceiveItem> INV_CrustLeatherReceiveItem { get; set; }
        public virtual ICollection<INV_FinishBuyerStock> INV_FinishBuyerStock { get; set; }
        public virtual ICollection<INV_FinishLeatherIssueItem> INV_FinishLeatherIssueItem { get; set; }
        public virtual ICollection<INV_FinishLeatherReceiveItem> INV_FinishLeatherReceiveItem { get; set; }
        public virtual ICollection<INV_FinishStockDaily> INV_FinishStockDaily { get; set; }
        public virtual ICollection<INV_FinishStockItem> INV_FinishStockItem { get; set; }
        public virtual ICollection<INV_FNTransferFrom> INV_FNTransferFrom { get; set; }
        public virtual ICollection<INV_FNTransferTo> INV_FNTransferTo { get; set; }
        public virtual ICollection<PRD_FinishLeatherProductionStock> PRD_FinishLeatherProductionStock { get; set; }
        public virtual ICollection<PRD_FinishLeatherQCItem> PRD_FinishLeatherQCItem { get; set; }
        public virtual ICollection<INV_FinishBuyerQCStock> INV_FinishBuyerQCStock { get; set; }
        public virtual ICollection<INV_FinishOwnQCStock> INV_FinishOwnQCStock { get; set; }
        public virtual ICollection<PRD_CrustLeatherQCItem> PRD_CrustLeatherQCItem { get; set; }
        public virtual ICollection<INV_CrustLeatherIssueItem> INV_CrustLeatherIssueItem { get; set; }
        public virtual ICollection<EXP_CIPIItem> EXP_CIPIItem { get; set; }
        public virtual ICollection<EXP_PLPIItemColor> EXP_PLPIItemColor { get; set; }
        public virtual ICollection<PRD_CLProductionItem> PRD_CLProductionItem { get; set; }
        public virtual ICollection<PRD_FNProductionItem> PRD_FNProductionItem { get; set; }
        public virtual ICollection<PRD_YearMonthCrustReqItem> PRD_YearMonthCrustReqItem { get; set; }
        public virtual ICollection<PRD_YearMonthCrustScheduleItem> PRD_YearMonthCrustScheduleItem { get; set; }
        public virtual ICollection<PRD_YearMonthFinishReqItem> PRD_YearMonthFinishReqItem { get; set; }
        public virtual ICollection<PRD_YearMonthFinishScheduleItem> PRD_YearMonthFinishScheduleItem { get; set; }
        public virtual ICollection<INV_FinishPackingStock> INV_FinishPackingStock { get; set; }
        public virtual ICollection<EXP_PIItem> EXP_PIItem { get; set; }
        public virtual ICollection<SLS_BuyerOrderItem> SLS_BuyerOrderItem { get; set; }
    }
}