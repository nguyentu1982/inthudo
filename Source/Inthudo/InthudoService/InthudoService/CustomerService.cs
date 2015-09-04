using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace InthudoService
{
    public class CustomerService:ServiceBase, ICustomerService
    {
        static readonly ICustomerDao customerDao = factory.CustomerDao;

        public List<CustomerBO> GetCustomers(string customerName, string telephone, string email, string companyName)
        {
            return customerDao.GetCustomers(customerName, telephone, email, companyName);
        }


        public CustomerBO GetCustomerById(int custId)
        {
            return customerDao.GetCustomerById(custId);
        }


        public CustomerBO GetCustomerByOrder(int orderId)
        {
            return customerDao.GetCustomerByOrder(orderId);
        }


        public void UpdateCustomer(CustomerBO customer)
        {
            customerDao.UpdateCustomer(customer);
        }

        public int InsertCustomer(CustomerBO customer)
        {
            return customerDao.InsertCustomer(customer);
        }


        public void MarkCustomerAsDeleted(int custId)
        {
            customerDao.MarkCustomerAsDeleted(custId);
        }


        public int GetCustomerTypeId(string code)
        {
            return customerDao.GetCustomerTypeId(code);
        }


        public CustomerTypeBO GetCustomerTypeById(int id)
        {
            return customerDao.GetCustomerTypeById(id);
        }
    }
}
