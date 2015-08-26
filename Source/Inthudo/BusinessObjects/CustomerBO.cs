using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class CustomerBO : BusinessObject
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string TaxCode { get; set; }
        public string Note { get; set; }

        public virtual MemberBO User { get; set; }
        public virtual MemberBO User1 { get; set; }
        public virtual ICollection<OrderBO> Orders { get; set; }
    }
}
