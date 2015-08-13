using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ShippingMethodBO
    {
        public int ShippingMethodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrderBO> Orders { get; set; }
    }
}
