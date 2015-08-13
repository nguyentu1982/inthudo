using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class OrderStatusMappingBO
    {
        public int OrderStatusMappingId { get; set; }
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public System.DateTime StatusDate { get; set; }
        public Nullable<bool> IsFailed { get; set; }

        public virtual OrderStatusBO OrderStatus { get; set; }
        public virtual OrderBO Order { get; set; }
    }
}
