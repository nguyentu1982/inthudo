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
            Mapper.CreateMap<LibRoleType, RoleType>();
            Mapper.CreateMap<RoleType, LibRoleType>();
        }

        public List<RoleType> GetRoleTypes()
        {
            using (var context = new InthudoEntities())
            {
                var roles = context.LibRoleTypes.ToList();
                return Mapper.Map<List<LibRoleType>, List<RoleType>>(roles);
            }
        }
    }
}
