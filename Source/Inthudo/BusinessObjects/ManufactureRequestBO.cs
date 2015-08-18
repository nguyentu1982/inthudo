using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ManufactureRequestBO
    {
        public int ManufactureRequestId { get; set; }
        public int DesignRequestId { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public int Quantity { get; set; }

        public virtual DesignRequestBO DesignRequest { get; set; }
        public virtual MemberBO Member { get; set; }
        public virtual MemberBO Member1 { get; set; }
    }
}
