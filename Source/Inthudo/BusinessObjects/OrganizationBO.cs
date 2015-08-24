using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class OrganizationBO
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserOrganizationMapppingBO> UserOrganizationMapppings { get; set; }
    }
}
