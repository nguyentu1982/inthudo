//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataObjects.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Customers = new HashSet<Customer>();
            this.Customers1 = new HashSet<Customer>();
            this.EmployeeDepartmentMappings = new HashSet<EmployeeDepartmentMapping>();
            this.Orders = new HashSet<Order>();
            this.Orders1 = new HashSet<Order>();
            this.Orders2 = new HashSet<Order>();
            this.OrderItems = new HashSet<OrderItem>();
            this.OrderItems1 = new HashSet<OrderItem>();
            this.DesignRequests = new HashSet<DesignRequest>();
            this.DesignRequests1 = new HashSet<DesignRequest>();
            this.DesignRequests2 = new HashSet<DesignRequest>();
            this.ManufactureRequests = new HashSet<ManufactureRequest>();
            this.ManufactureRequests1 = new HashSet<ManufactureRequest>();
            this.UserOrganizationMapppings = new HashSet<UserOrganizationMappping>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleTypeId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public Nullable<bool> Deteted { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public Nullable<int> DepartmentId { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Customer> Customers1 { get; set; }
        public virtual ICollection<EmployeeDepartmentMapping> EmployeeDepartmentMappings { get; set; }
        public virtual LibRoleType LibRoleType { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Order> Orders1 { get; set; }
        public virtual ICollection<Order> Orders2 { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems1 { get; set; }
        public virtual ICollection<DesignRequest> DesignRequests { get; set; }
        public virtual ICollection<DesignRequest> DesignRequests1 { get; set; }
        public virtual ICollection<DesignRequest> DesignRequests2 { get; set; }
        public virtual ICollection<ManufactureRequest> ManufactureRequests { get; set; }
        public virtual ICollection<ManufactureRequest> ManufactureRequests1 { get; set; }
        public virtual LibDepartment LibDepartment { get; set; }
        public virtual ICollection<UserOrganizationMappping> UserOrganizationMapppings { get; set; }
    }
}
