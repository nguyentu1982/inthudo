using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Product business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign key mapping

    public class ProductBO : BusinessObject
    {
        // ** Enterprise Design Pattern: Identity field pattern

        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}

