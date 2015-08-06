using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework
{
    // Data access object factory
    // ** Factory Pattern

    public class DaoFactory : IDaoFactory
    {
        public IMemberDao MemberDao { get { return new MemberDao(); } }
        public IRoleTypeDao RoleTypeDao { get { return new RoleTypeDao(); } }
        public IProductDao ProductDao { get { return new ProductDao(); } }
        public IOrderDao OrderDao { get { return new OrderDao(); } }
    }
}
