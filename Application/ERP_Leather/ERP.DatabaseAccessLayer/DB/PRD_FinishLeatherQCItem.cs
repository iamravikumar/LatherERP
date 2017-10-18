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
    
    public partial class PRD_FinishLeatherQCItem
    {
        public PRD_FinishLeatherQCItem()
        {
            this.PRD_FinishLeatherQCSelection = new HashSet<PRD_FinishLeatherQCSelection>();
        }
    
        public long FNQCItemID { get; set; }
        public Nullable<long> FinishLeatherQCRefID { get; set; }
        public Nullable<long> FinishLeatherQCID { get; set; }
        public Nullable<long> ScheduleItemID { get; set; }
        public string ScheduleProductionNo { get; set; }
        public Nullable<int> BuyerID { get; set; }
        public Nullable<long> BuyerOrderID { get; set; }
        public Nullable<int> ArticleID { get; set; }
        public string ArticleNo { get; set; }
        public string ArticleChallanNo { get; set; }
        public Nullable<int> ColorID { get; set; }
        public Nullable<byte> ItemTypeID { get; set; }
        public Nullable<byte> LeatherTypeID { get; set; }
        public Nullable<byte> LeatherStatusID { get; set; }
        public Nullable<decimal> ProductionPcs { get; set; }
        public Nullable<decimal> ProductionSide { get; set; }
        public Nullable<decimal> Productionrea { get; set; }
        public Nullable<decimal> FNQCPcs { get; set; }
        public Nullable<decimal> FNQCSide { get; set; }
        public Nullable<decimal> FNQCArea { get; set; }
        public Nullable<byte> FNQCAreaUnit { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public Nullable<int> ArticleColorNo { get; set; }
        public Nullable<long> ArticleChallanID { get; set; }
    
        public virtual PRD_FinishLeatherQC PRD_FinishLeatherQC { get; set; }
        public virtual Sys_Article Sys_Article { get; set; }
        public virtual Sys_Buyer Sys_Buyer { get; set; }
        public virtual SLS_BuyerOrder SLS_BuyerOrder { get; set; }
        public virtual Sys_Color Sys_Color { get; set; }
        public virtual PRD_FinishLeatherQCRef PRD_FinishLeatherQCRef { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_ItemType Sys_ItemType { get; set; }
        public virtual Sys_LeatherStatus Sys_LeatherStatus { get; set; }
        public virtual Sys_LeatherType Sys_LeatherType { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<PRD_FinishLeatherQCSelection> PRD_FinishLeatherQCSelection { get; set; }
        public virtual PRD_YearMonthFinishScheduleItem PRD_YearMonthFinishScheduleItem { get; set; }
        public virtual Sys_ArticleChallan Sys_ArticleChallan { get; set; }
    }
}
