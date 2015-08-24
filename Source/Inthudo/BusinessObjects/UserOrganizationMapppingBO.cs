using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class UserOrganizationMapppingBO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }

        public virtual OrganizationBO Organization { get; set; }
        public virtual MemberBO Member { get; set; }
    }
}
