//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataObjects.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderStatusMapping
    {
        public int OrderStatusMappingId { get; set; }
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public System.DateTime StatusDate { get; set; }
        public Nullable<bool> IsFailed { get; set; }
    
        public virtual LibOrderStatu LibOrderStatu { get; set; }
        public virtual Order Order { get; set; }
    }
}