using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class OrderSearch
    {
        public OrderSearch()
        { 
            
        }

        public int OrderId { get; set; }
        public int CustId { get; set; }
        public int ProductId { get; set; }
        public int ShipMethodId { get; set; }
        public int DepositMethodId { get; set; }
        public OrderDetailStatusEnum OrderDetailStatus { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public int BusManId { get; set; }
        public int DesignerManId { get; set; }
    }
}
