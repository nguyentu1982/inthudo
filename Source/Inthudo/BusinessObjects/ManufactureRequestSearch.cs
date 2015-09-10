using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ManufactureRequestSearch
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public ManufactureRequestStatusEnum ManufactureRequestStatus { get; set; }
        public int DesignerId { get; set; }
        public int BusinessManId { get; set; }
        public int ManufactureId { get; set; }
        public int OrderId { get; set; }
        public List<int> PrintTypeIds { get; set; }
    }
}
