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
    
    public partial class SLS_BuyerOrderDelivery
    {
        public long BuyerOrderDeliveryID { get; set; }
        public Nullable<long> BuyerOrderID { get; set; }
        public Nullable<int> OrdDeliverySL { get; set; }
        public Nullable<System.DateTime> OrdDeliveryDate { get; set; }
        public Nullable<decimal> OrdDateFootQty { get; set; }
        public Nullable<decimal> OrdDateMeterQty { get; set; }
        public Nullable<int> PIDeliverySL { get; set; }
        public Nullable<System.DateTime> PIDeliveryDate { get; set; }
        public Nullable<decimal> PIDateFootQty { get; set; }
        public Nullable<decimal> PIDateMeterQty { get; set; }
        public Nullable<int> ArticleID { get; set; }
        public Nullable<decimal> ArticleFootQty { get; set; }
        public Nullable<decimal> ArticleMeterQty { get; set; }
        public Nullable<int> ColorID { get; set; }
        public Nullable<decimal> ColorFootQty { get; set; }
        public Nullable<decimal> ColorMeterQty { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual SLS_BuyerOrder SLS_BuyerOrder { get; set; }
        public virtual Sys_Article Sys_Article { get; set; }
        public virtual Sys_Color Sys_Color { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
