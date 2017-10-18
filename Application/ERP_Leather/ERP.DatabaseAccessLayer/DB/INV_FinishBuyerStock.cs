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
    
    public partial class INV_FinishBuyerStock
    {
        public long TransectionID { get; set; }
        public Nullable<byte> StoreID { get; set; }
        public string FinishQCLabel { get; set; }
        public Nullable<int> BuyerID { get; set; }
        public Nullable<long> BuyerOrderID { get; set; }
        public Nullable<int> ArticleID { get; set; }
        public string ArticleNo { get; set; }
        public string ArticleChallanNo { get; set; }
        public Nullable<int> ColorID { get; set; }
        public Nullable<byte> ItemTypeID { get; set; }
        public Nullable<byte> LeatherTypeID { get; set; }
        public Nullable<byte> LeatherStatusID { get; set; }
        public Nullable<short> GradeID { get; set; }
        public Nullable<decimal> OpeningStockPcs { get; set; }
        public Nullable<decimal> OpeningStockSide { get; set; }
        public Nullable<decimal> OpeningStockArea { get; set; }
        public Nullable<decimal> ReceiveStockPcs { get; set; }
        public Nullable<decimal> ReceiveStockSide { get; set; }
        public Nullable<decimal> ReceiveStockArea { get; set; }
        public Nullable<decimal> IssueStockPcs { get; set; }
        public Nullable<decimal> IssueStockSide { get; set; }
        public Nullable<decimal> IssueStockArea { get; set; }
        public Nullable<decimal> ClosingStockPcs { get; set; }
        public Nullable<decimal> ClosingStockSide { get; set; }
        public Nullable<decimal> ClosingStockArea { get; set; }
        public Nullable<byte> ClosingStockAreaUnit { get; set; }
        public Nullable<int> ArticleColorNo { get; set; }
        public string GradeRange { get; set; }
        public Nullable<long> ArticleChallanID { get; set; }
    
        public virtual Sys_Article Sys_Article { get; set; }
        public virtual Sys_Buyer Sys_Buyer { get; set; }
        public virtual SLS_BuyerOrder SLS_BuyerOrder { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_Color Sys_Color { get; set; }
        public virtual Sys_Grade Sys_Grade { get; set; }
        public virtual Sys_ItemType Sys_ItemType { get; set; }
        public virtual Sys_LeatherStatus Sys_LeatherStatus { get; set; }
        public virtual Sys_LeatherType Sys_LeatherType { get; set; }
        public virtual SYS_Store SYS_Store { get; set; }
        public virtual Sys_ArticleChallan Sys_ArticleChallan { get; set; }
    }
}
