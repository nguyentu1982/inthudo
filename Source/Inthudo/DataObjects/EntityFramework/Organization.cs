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
    
    public partial class Organization
    {
        public Organization()
        {
            this.UserOrganizationMapppings = new HashSet<UserOrganizationMappping>();
        }
    
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<UserOrganizationMappping> UserOrganizationMapppings { get; set; }
    }
}
