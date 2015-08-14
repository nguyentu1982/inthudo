using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Order business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign Key Mapping.
    
    public class OrderBO : BusinessObject
    {
        // ** Enterprise Design Pattern: Identity field pattern

        public int OrderId { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<int> DepositTypeId { get; set; }
        public Nullable<int> ShippingMethodId { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedDate { get; set; }
        public Nullable<bool> Deleted { get; set; }

        public virtual CustomerBO Customer { get; set; }
        public virtual ICollection<DesignBO> Designs { get; set; }
        public virtual Member BusinessMan { get; set; }
        public virtual Member User1 { get; set; }
        public virtual Member User2 { get; set; }
        public virtual DepositMethodBO DepositType { get; set; }
        public virtual ShippingMethodBO ShippingMethod { get; set; }
        public virtual IList<OrderDetailBO> OrderItems { get; set; }

        public string CustomerName
        {
            get;
            set;
        }
        
        public string BusinessManName
        {
            get;
            set;
        }

        public string DepositTypeName
        {
            get;
            set;
        }

        public string ShippingMethodName
        {
            get;
            set;
        }

        public decimal Total
        {
            get;
            set;
        }
    }
}
