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
    
    public partial class EXP_CnFBill
    {
        public long CnfBillID { get; set; }
        public string CnfBillNo { get; set; }
        public string RefCnfBillNo { get; set; }
        public Nullable<System.DateTime> CnfBillDate { get; set; }
        public Nullable<long> CIID { get; set; }
        public Nullable<long> PLID { get; set; }
        public string BillOfEntryNo { get; set; }
        public Nullable<System.DateTime> BillOfEntryDate { get; set; }
        public Nullable<decimal> AssesmentValue { get; set; }
        public string CnfBillNote { get; set; }
        public Nullable<int> CnfAgentID { get; set; }
        public Nullable<decimal> DutyAccountCharge { get; set; }
        public Nullable<decimal> PortCharge { get; set; }
        public Nullable<decimal> ShippingCharge { get; set; }
        public Nullable<decimal> BerthOperatorCharge { get; set; }
        public Nullable<decimal> NOCCharge { get; set; }
        public Nullable<decimal> ExamineCharge { get; set; }
        public Nullable<decimal> SpecialDeliveryCharge { get; set; }
        public Nullable<decimal> AmendmentCharge { get; set; }
        public Nullable<decimal> ChemicalTestCharge { get; set; }
        public Nullable<decimal> AgencyCommission { get; set; }
        public Nullable<decimal> StampCharge { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<byte> CnfBillCurrency { get; set; }
        public Nullable<byte> ExchangeCurrency { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<decimal> ExchangeValue { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual EXP_CI EXP_CI { get; set; }
        public virtual Sys_Buyer Sys_Buyer { get; set; }
        public virtual Sys_Currency Sys_Currency { get; set; }
        public virtual Sys_Currency Sys_Currency1 { get; set; }
        public virtual User User { get; set; }
        public virtual EXP_PackingList EXP_PackingList { get; set; }
        public virtual User User1 { get; set; }
    }
}
