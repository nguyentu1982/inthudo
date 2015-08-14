using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Order Detail business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign key mapping


    public class OrderDetailBO : BusinessObject
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string Specification { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public int OrderId { get; set; }
        public int? DesignerId { get; set; }

        public virtual Member User { get; set; }
        public virtual Member User1 { get; set; }
        public virtual ProductBO Product { get; set; }
        public virtual OrderBO Order { get; set; }

        public string ProductName
        {
            get;
            set;
        }
    }
}
