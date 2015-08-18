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
                            select new CustomerBO() {
                                CustomerId = c.CustomerId,
                                Name = c.Name,
                                Telephone = c.Telephone,
                                Address = c.Address,
                                Email = c.Email,
                                CreatedOn = c.CreatedOn,
                                CreatedBy = c.CreatedBy,
                                LastEditedOn = c.LastEditedOn,
                                LastEditedBy = c.LastEditedBy,
                                Company = c.Company,
                                PhoneNumber = c.PhoneNumber,
                                FaxNumber = c.FaxNumber,
                                TaxCode = c.TaxCode,
                            };
                return query.ToList();
                
            }
        }


        public CustomerBO GetCustomerById(int custId)
        {
            using (var context = new InThuDoEntities())
            {
                return ( from c in context.Customers
                       where c.CustomerId == custId
                       select new CustomerBO()
                       {
                           CustomerId = c.CustomerId,
                           Name = c.Name,
                           Telephone = c.Telephone,
                           Address = c.Address,
                           Email = c.Email,
                           CreatedOn = c.CreatedOn,
                           CreatedBy = c.CreatedBy,
                           LastEditedOn = c.LastEditedOn,
                           LastEditedBy = c.LastEditedBy,
                           Company = c.Company,
                           PhoneNumber = c.PhoneNumber,
                           FaxNumber = c.FaxNumber,
                           TaxCode = c.TaxCode,
                       }).FirstOrDefault();
                
            }
        }

        public InThuDoEntities context
        {
            get
            {
                return new InThuDoEntities();
            }
        }


        public CustomerBO GetCustomerByOrder(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from c in context.Customers
                            join o in context.Orders on c.CustomerId equals o.CustomerId
                            where o.OrderId == orderId
                            select new CustomerBO()
                            {
                                CustomerId = c.CustomerId,
                                Name = c.Name,
                                Telephone = c.Telephone,
                                Address = c.Address,
                                Email = c.Email,
                                CreatedOn = c.CreatedOn,
                                CreatedBy = c.CreatedBy,
                                LastEditedOn = c.LastEditedOn,
                                LastEditedBy = c.LastEditedBy,
                                Company = c.Company,
                                PhoneNumber = c.PhoneNumber,
                                FaxNumber = c.FaxNumber,
                                TaxCode = c.TaxCode,
                            };
                return query.FirstOrDefault();
            }
        }
    }
}
