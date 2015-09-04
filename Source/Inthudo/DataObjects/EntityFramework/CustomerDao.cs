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
                            (string.IsNullOrEmpty(companyName) || c.Company.Contains(companyName)) &&
                            (c.Deleted == false || c.Deleted == null)
                            select c;

                return MapCustomerList(query);                
            }
        }

        public CustomerBO GetCustomerById(int custId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from c in context.Customers
                            where c.CustomerId == custId
                            select c;
                return MapCustomer(query.FirstOrDefault());
                
            }
        }

        public CustomerBO GetCustomerByOrder(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from c in context.Customers
                            join o in context.Orders on c.CustomerId equals o.CustomerId
                            where o.OrderId == orderId
                            select c;
                return MapCustomer(query.FirstOrDefault());
            }
        }


        public void UpdateCustomer(CustomerBO customer)
        {
            using (var context = new InThuDoEntities())
            {
                Customer cust = context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);
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
                cust.CustomerTypeId = customer.CustomerTypeId;

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
                    Note = customer.Note,
                    CustomerTypeId = customer.CustomerTypeId
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

        private CustomerBO MapCustomer(Customer c)
        {
            if (c == null) return null;

            CustomerBO cust = new CustomerBO()
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
                CustomerTypeId = c.CustomerTypeId,
                CustomerType = new CustomerTypeBO() { 
                    Id = c.LibCustomerType.Id,
                    Code = c.LibCustomerType.Code,
                    Name = c.LibCustomerType.Name,
                    Description = c.LibCustomerType.Description
                }
            };

            return cust;
        }

        private List<CustomerBO> MapCustomerList(IQueryable<Customer> custs)
        {
            List<CustomerBO> result = new List<CustomerBO>();
            foreach (Customer c in custs)
            { 
                result.Add(this.MapCustomer(c));
            }
            return result;
        }

        private CustomerTypeBO MapCustomerType(LibCustomerType libCustType)
        {
            if (libCustType == null) return null;

            return new CustomerTypeBO()
            {
                Id = libCustType.Id,
                Code = libCustType.Code,
                Name = libCustType.Name,
                Description = libCustType.Description                
            };     
        }

        public int GetCustomerTypeId(string code)
        {
            using (var context = new InThuDoEntities())
            { 
                var query = from c in context.LibCustomerTypes
                            where c.Code == code
                                select c;
                if (query.Count() == 0) return 0;
                return query.FirstOrDefault().Id;
            }
        }


        public CustomerTypeBO GetCustomerTypeById(int id)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from c in context.LibCustomerTypes
                            where c.Id== id
                            select c;
                if (query.Count() == 0) return null;
                return this.MapCustomerType(query.FirstOrDefault());
            }
        }
    }
}
