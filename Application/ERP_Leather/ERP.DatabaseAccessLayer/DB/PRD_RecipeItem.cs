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
    
    public partial class PRD_RecipeItem
    {
        public long RecipeItemID { get; set; }
        public Nullable<int> RecipeID { get; set; }
        public int ItemID { get; set; }
        public decimal RequiredQty { get; set; }
        public byte UnitID { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    
        public virtual PRD_Recipe PRD_Recipe { get; set; }
        public virtual Sys_ChemicalItem Sys_ChemicalItem { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Sys_Unit Sys_Unit { get; set; }
    }
}
