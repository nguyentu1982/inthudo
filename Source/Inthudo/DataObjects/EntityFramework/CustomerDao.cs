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
            using (var context = new InThuDoEntities())
            {
                var query = from c in context.Customers
                            where
                            (string.IsNullOrEmpty(customerName) || c.Name.Contains(customerName)) &&
                            (string.IsNullOrEmpty(telephone) || c.Telephone.Contains(telephone)) &&
                            (string.IsNullOrEmpty(email) || c.Email.Contains(email)) &&
                            (string.IsNullOrEmpty(companyName) || c.Company.Contains(companyName))&&
                            (c.Deleted == false || c.Deleted == null)
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
                                Note = c.Note,
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
                           Note = c.Note,
                       }).FirstOrDefault();
                
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
                                Note = c.Note
                            };
                return query.FirstOrDefault();
            }
        }


        public void UpdateCustomer(CustomerBO customer)
        {
            using (var context = new InThuDoEntities())
            {
                Customer cust = context.Customers.Where(c => c.CustomerId == customer.CustomerId) as Customer;
                cust.Name = customer.Name;
                cust.Telephone = customer.Telephone;
                cust.Address = customer.Address;
                cust.Email = customer.Email;
                cust.LastEditedOn = customer.LastEditedOn;
                cust.LastEditedBy = customer.LastEditedBy;
                cust.Company = customer.Company;
                cust.PhoneNumber = customer.PhoneNumber;
                cust.FaxNumber = customer.FaxNumber;
                cust.TaxCode = customer.TaxCode;
                cust.Note = customer.Note;

                context.SaveChanges();
            }
        }

        public int InsertCustomer(CustomerBO customer)
        {
            using (var context = new InThuDoEntities())
            {
                Customer cust = new Customer() { 
                    Name = customer.Name,
                    Telephone = customer.Telephone,
                    Address = customer.Address,
                    Email = customer.Email,
                    CreatedOn = customer.CreatedOn,
                    CreatedBy = customer.CreatedBy,
                    Company = customer.Company,
                    PhoneNumber = customer.PhoneNumber,
                    FaxNumber = customer.FaxNumber,
                    TaxCode = customer.TaxCode,
                    Note = customer.Note
                };

                context.Customers.Add(cust);
                context.SaveChanges();
                return cust.CustomerId;
            }
        }


        public void MarkCustomerAsDeleted(int custId)
        {
            using (var context = new InThuDoEntities())
            {
                Customer cust = (from c in context.Customers
                                where c.CustomerId == custId
                                select c).FirstOrDefault();
                cust.Deleted = true;
                context.SaveChanges();
            }
        }
    }
}
