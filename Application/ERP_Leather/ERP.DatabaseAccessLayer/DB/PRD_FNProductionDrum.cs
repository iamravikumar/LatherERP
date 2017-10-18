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
    
    public partial class PRD_FNProductionDrum
    {
        public PRD_FNProductionDrum()
        {
            this.PRD_FinishLeatherProductionStock = new HashSet<PRD_FinishLeatherProductionStock>();
            this.PRD_FNProductionChemical = new HashSet<PRD_FNProductionChemical>();
        }
    
        public long FNProductionDrumID { get; set; }
        public Nullable<long> FNProductionColorID { get; set; }
        public Nullable<int> MachineID { get; set; }
        public string MachineNo { get; set; }
        public Nullable<int> ColorID { get; set; }
        public Nullable<short> GradeID { get; set; }
        public Nullable<decimal> DrumArea { get; set; }
        public Nullable<byte> AreaUnit { get; set; }
        public Nullable<decimal> DrumPcs { get; set; }
        public Nullable<decimal> DrumSide { get; set; }
        public Nullable<decimal> DrumWeight { get; set; }
        public Nullable<byte> WeightUnit { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public Nullable<int> ArticleColorNo { get; set; }
        public string GradeRange { get; set; }
    
        public virtual ICollection<PRD_FinishLeatherProductionStock> PRD_FinishLeatherProductionStock { get; set; }
        public virtual ICollection<PRD_FNProductionChemical> PRD_FNProductionChemical { get; set; }
        public virtual PRD_FNProductionColor PRD_FNProductionColor { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
        public virtual Sys_Color Sys_Color { get; set; }
        public virtual Sys_Grade Sys_Grade { get; set; }
        public virtual Sys_Machine Sys_Machine { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Sys_Unit Sys_Unit1 { get; set; }
    }
}