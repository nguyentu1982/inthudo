using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class DesignRequestBO :BusinessObject
    {
        public int DesignRequestId { get; set; }
        public string Description { get; set; }
        public Nullable<int> DesignerId { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public int OrderId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public int OrderItemId { get; set; }
        public Nullable<bool> ApprovedByCustomer { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }

        public virtual OrderDetailBO OrderItem { get; set; }
        public virtual MemberBO User { get; set; }
        public virtual MemberBO User1 { get; set; }
        public virtual MemberBO User2 { get; set; }

        public DateTime? OrderDate { get; set; }
        public string ProductName { get; set; }
        public string OrderDetailStatusString {
            get {
                return OrderItem.OrderDetailStatusString;
            }
        }
    }
}
