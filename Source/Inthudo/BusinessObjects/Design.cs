using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class DesignBO :BusinessObject
    {
        public int DesignId { get; set; }
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

        public virtual OrderBO Order { get; set; }
        public virtual Member User { get; set; }
        public virtual Member User1 { get; set; }
        public virtual Member User2 { get; set; }
    }
}
