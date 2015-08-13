using AutoMapper;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework
{
    public class RoleTypeDao:IRoleTypeDao
    {
        static RoleTypeDao()
        {
            Mapper.CreateMap<LibRoleType, RoleTypeBO>();
            Mapper.CreateMap<RoleTypeBO, LibRoleType>();
        }

        public List<RoleTypeBO> GetRoleTypes()
        {
            using (var context = new InThuDoEntities())
            {
                var roles = context.LibRoleTypes.ToList();
                return Mapper.Map<List<LibRoleType>, List<RoleTypeBO>>(roles);
            }
        }
    }
}
