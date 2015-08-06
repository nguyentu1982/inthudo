using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace InthudoService
{
    public interface IRoleTypeService
    {
        List<RoleTypeBO> GetRoleTypes();
    }
}
