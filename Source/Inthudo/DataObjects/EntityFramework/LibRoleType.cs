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
    
    public partial class LibRoleType
    {
        public LibRoleType()
        {
            this.Users = new HashSet<User>();
        }
    
        public int RoleTypeId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
    }
}