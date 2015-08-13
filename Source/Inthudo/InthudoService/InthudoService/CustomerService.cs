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
    }
}
