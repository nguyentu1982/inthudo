using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class RoleType:BusinessObject
    {
        public RoleType()
        { 
        
        }

        public int RoleTypeId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        
    }
}
