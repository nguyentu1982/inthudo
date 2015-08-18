using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace InthudoService
{
    public interface ICustomerService
    {
        List<CustomerBO> GetCustomers(string customerName, string telephone, string email, string companyName);

        CustomerBO GetCustomerById(int custId);

        CustomerBO GetCustomerByOrder(int orderId);
    }
}
