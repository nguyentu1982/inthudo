﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    
    // abstract factory interface. Creates data access objects.
    // ** GoF Design Pattern: Factory.
    
    public interface IDaoFactory
    {
        IMemberDao MemberDao { get; }
        IRoleTypeDao RoleTypeDao { get; }
        IProductDao ProductDao { get; }
        IOrderDao OrderDao { get; }
        ICustomerDao CustomerDao { get; }
        ISettingDao SettingDao { get; }
    }
}
