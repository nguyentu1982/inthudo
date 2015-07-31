using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace InthudoService
{
    public class RoleTypeService:ServiceBase, IRoleTypeService
    {
        static readonly IRoleTypeDao roleTypeDao = factory.RoleTypeDao;
        public List<BusinessObjects.RoleType> GetRoleTypes()
        {
            return roleTypeDao.GetRoleTypes();
        }
    }
}
