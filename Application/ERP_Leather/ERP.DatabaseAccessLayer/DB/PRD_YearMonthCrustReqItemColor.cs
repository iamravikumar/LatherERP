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
    
    public partial class PRD_YearMonthCrustReqItemColor
    {
        public long ReqItemColorID { get; set; }
        public Nullable<long> RequisitionItemID { get; set; }
        public Nullable<int> ColorID { get; set; }
        public Nullable<decimal> ColorPcs { get; set; }
        public Nullable<decimal> ColorSide { get; set; }
        public Nullable<decimal> ColorArea { get; set; }
        public Nullable<byte> AreaUnit { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public Nullable<int> ArticleColorNo { get; set; }
    
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_Color Sys_Color { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual PRD_YearMonthCrustReqItem PRD_YearMonthCrustReqItem { get; set; }
    }
}
