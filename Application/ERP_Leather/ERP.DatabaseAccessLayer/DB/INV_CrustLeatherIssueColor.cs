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
    
    public partial class INV_CrustLeatherIssueColor
    {
        public INV_CrustLeatherIssueColor()
        {
            this.INV_CrustLeatherReceiveColor = new HashSet<INV_CrustLeatherReceiveColor>();
        }
    
        public long CrustLeatherIssueColorID { get; set; }
        public Nullable<long> CrustLeatherIssueItemID { get; set; }
        public Nullable<long> CrustLeatherIssueID { get; set; }
        public Nullable<int> ColorID { get; set; }
        public Nullable<short> GradeID { get; set; }
        public Nullable<decimal> IssuePcs { get; set; }
        public Nullable<decimal> IssueSide { get; set; }
        public Nullable<decimal> IssueArea { get; set; }
        public Nullable<byte> AreaUnit { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public string CrustQCLabel { get; set; }
        public Nullable<decimal> SideArea { get; set; }
        public string GradeRange { get; set; }
        public Nullable<int> ArticleColorNo { get; set; }
    
        public virtual INV_CrustLeatherIssue INV_CrustLeatherIssue { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_Color Sys_Color { get; set; }
        public virtual Sys_Grade Sys_Grade { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<INV_CrustLeatherReceiveColor> INV_CrustLeatherReceiveColor { get; set; }
        public virtual INV_CrustLeatherIssueItem INV_CrustLeatherIssueItem { get; set; }
    }
}
