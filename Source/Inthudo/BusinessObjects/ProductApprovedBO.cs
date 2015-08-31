using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class ProductApprovedBO:BusinessObject
    {
        public int ProductId { get; set; }
        public int OrderDetailId { get; set; }
        public int DesignRequestId { get; set; }
        public int ManufactureRequestId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string ProductName { get; set; }
        public decimal Total
        {
            get
            {
                int quantity = 0;
                decimal price = 0;
                int.TryParse(Quantity.ToString(), out quantity);
                decimal.TryParse(Price.ToString(), out price);
                return quantity * price;
            }
        }
    }
}
