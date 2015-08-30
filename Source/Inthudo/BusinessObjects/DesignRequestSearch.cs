using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class DesignRequestSearch
    {
        public DesignRequestSearch()
        { 
        
        }

        public DateTime? RequestFrom { get; set; }
        public DateTime? RequestTo { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DesignRequestStatusEnum DesignRequestStatus { get; set; }
        public int DesignerId { get; set; }
    }
}
