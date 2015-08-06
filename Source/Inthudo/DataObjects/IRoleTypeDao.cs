using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects
{
    public interface IRoleTypeDao
    {
        List<RoleTypeBO> GetRoleTypes();
    }
}
