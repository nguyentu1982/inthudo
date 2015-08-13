using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    public partial class CustomerDao:ICustomerDao
    {
        static CustomerDao()
        {
            Mapper.CreateMap<Customer, CustomerBO>();
            Mapper.CreateMap<CustomerBO, Customer>();
        }

        public List<CustomerBO> GetCustomers(string customerName, string telephone, string email, string companyName)
        {
            using ( context )
            {
                var query = from c in context.Customers
                            where
                            (string.IsNullOrEmpty(customerName) || c.Name.Contains(customerName)) &&
                            (string.IsNullOrEmpty(telephone) || c.Telephone.Contains(telephone)) &&
                            (string.IsNullOrEmpty(email) || c.Email.Contains(email)) &&
                            (string.IsNullOrEmpty(companyName) || c.Company.Contains(companyName))
                            select c;
                return Mapper.Map<List<Customer>, List<CustomerBO>>(query.ToList());
            }
        }


        public CustomerBO GetCustomerById(int custId)
        {
            using ( context )
            {
                var query = context.Customers.Where(c => c.CustomerId == custId).FirstOrDefault();
                return Mapper.Map<Customer, CustomerBO>(query);
            }
        }

        public InThuDoEntities context
        {
            get
            {
                return new InThuDoEntities();
            }
        }
    }
}
