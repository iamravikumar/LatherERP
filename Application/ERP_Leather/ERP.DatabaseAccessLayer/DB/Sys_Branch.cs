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
    
    public partial class Sys_Branch
    {
        public Sys_Branch()
        {
            this.LCM_BankDebitVoucher = new HashSet<LCM_BankDebitVoucher>();
            this.LCM_Insurence = new HashSet<LCM_Insurence>();
            this.LCM_LCOpening = new HashSet<LCM_LCOpening>();
            this.LCM_LCOpening1 = new HashSet<LCM_LCOpening>();
            this.LCM_LimInfo = new HashSet<LCM_LimInfo>();
            this.PRQ_ChemicalPI = new HashSet<PRQ_ChemicalPI>();
            this.EXP_LCOpening = new HashSet<EXP_LCOpening>();
            this.EXP_LCOpening1 = new HashSet<EXP_LCOpening>();
            this.EXP_LeatherPI = new HashSet<EXP_LeatherPI>();
            this.EXP_BankDebitVoucher = new HashSet<EXP_BankDebitVoucher>();
            this.LCM_LCOpening11 = new HashSet<LCM_LCOpening>();
            this.PRQ_ChemicalPI1 = new HashSet<PRQ_ChemicalPI>();
            this.EXP_ExportForm = new HashSet<EXP_ExportForm>();
            this.EXP_LeatherPI1 = new HashSet<EXP_LeatherPI>();
            this.EXP_BankLoan = new HashSet<EXP_BankLoan>();
            this.EXP_BankVoucher = new HashSet<EXP_BankVoucher>();
        }
    
        public int BranchID { get; set; }
        public Nullable<int> BankID { get; set; }
        public string BanchCode { get; set; }
        public string BranchName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public Nullable<decimal> LCLimit { get; set; }
        public Nullable<decimal> LCOpened { get; set; }
        public Nullable<decimal> LCBalance { get; set; }
        public Nullable<decimal> LCMargin { get; set; }
        public Nullable<decimal> LCMarginUsed { get; set; }
        public Nullable<decimal> LCMarginBalance { get; set; }
        public Nullable<byte> LCCurrency { get; set; }
        public string BranchSwiftCode { get; set; }
        public Nullable<decimal> LIMLimit { get; set; }
        public Nullable<decimal> LIMTaken { get; set; }
        public Nullable<decimal> LIMBalance { get; set; }
        public Nullable<byte> LIMCurrency { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual ICollection<LCM_BankDebitVoucher> LCM_BankDebitVoucher { get; set; }
        public virtual ICollection<LCM_Insurence> LCM_Insurence { get; set; }
        public virtual ICollection<LCM_LCOpening> LCM_LCOpening { get; set; }
        public virtual ICollection<LCM_LCOpening> LCM_LCOpening1 { get; set; }
        public virtual ICollection<LCM_LimInfo> LCM_LimInfo { get; set; }
        public virtual ICollection<PRQ_ChemicalPI> PRQ_ChemicalPI { get; set; }
        public virtual Sys_Bank Sys_Bank { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Sys_Currency Sys_Currency { get; set; }
        public virtual Sys_Currency Sys_Currency1 { get; set; }
        public virtual ICollection<EXP_LCOpening> EXP_LCOpening { get; set; }
        public virtual ICollection<EXP_LCOpening> EXP_LCOpening1 { get; set; }
        public virtual ICollection<EXP_LeatherPI> EXP_LeatherPI { get; set; }
        public virtual ICollection<EXP_BankDebitVoucher> EXP_BankDebitVoucher { get; set; }
        public virtual ICollection<LCM_LCOpening> LCM_LCOpening11 { get; set; }
        public virtual ICollection<PRQ_ChemicalPI> PRQ_ChemicalPI1 { get; set; }
        public virtual ICollection<EXP_ExportForm> EXP_ExportForm { get; set; }
        public virtual ICollection<EXP_LeatherPI> EXP_LeatherPI1 { get; set; }
        public virtual ICollection<EXP_BankLoan> EXP_BankLoan { get; set; }
        public virtual ICollection<EXP_BankVoucher> EXP_BankVoucher { get; set; }
    }
}
