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
    
    public partial class INV_CLTransfer
    {
        public INV_CLTransfer()
        {
            this.INV_CLTransferFrom = new HashSet<INV_CLTransferFrom>();
            this.INV_CLTransferTo = new HashSet<INV_CLTransferTo>();
        }
    
        public long CLTransferID { get; set; }
        public string CLTransferNo { get; set; }
        public Nullable<System.DateTime> CLTransferDate { get; set; }
        public string CLTransferCategory { get; set; }
        public string TranrsferType { get; set; }
        public Nullable<byte> TransactionStore { get; set; }
        public Nullable<byte> IssueStore { get; set; }
        public Nullable<long> ReferenceJobOrdNo { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> IssuedBy { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string IssueNote { get; set; }
        public Nullable<int> CheckedBy { get; set; }
        public Nullable<System.DateTime> CheckDate { get; set; }
        public string CheckNote { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public string ApproveNote { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual SYS_Store SYS_Store { get; set; }
        public virtual User User3 { get; set; }
        public virtual User User4 { get; set; }
        public virtual SYS_Store SYS_Store1 { get; set; }
        public virtual ICollection<INV_CLTransferFrom> INV_CLTransferFrom { get; set; }
        public virtual ICollection<INV_CLTransferTo> INV_CLTransferTo { get; set; }
    }
}