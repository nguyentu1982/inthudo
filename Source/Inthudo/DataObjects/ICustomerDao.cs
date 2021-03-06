﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects
{
    public interface ICustomerDao
    {
        List<CustomerBO> GetCustomers(string customerName, string telephone, string email, string companyName,int customerTypeId);

        CustomerBO GetCustomerById(int custId);

        CustomerBO GetCustomerByOrder(int orderId);

        void UpdateCustomer(CustomerBO customer);

        int InsertCustomer(CustomerBO customer);

        void MarkCustomerAsDeleted(int custId);

        int GetCustomerTypeId(string code);

        CustomerTypeBO GetCustomerTypeById(int id);
    }
}
