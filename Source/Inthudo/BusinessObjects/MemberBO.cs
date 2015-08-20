using BusinessObjects.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    // Member business object

    // ** Enterprise Design Pattern: Domain Model, Identity Field.
    // this is also the place where business rules are established.

    public class MemberBO : BusinessObject
    {
        public MemberBO()
        {
            // establish business rules

            AddRule(new ValidateId("UserId"));

            AddRule(new ValidateRequired("Email"));
            AddRule(new ValidateLength("Email", 1, 100));
            AddRule(new ValidateEmail("Email"));
        }

        // ** Enterprise Design Pattern: Identity field pattern

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public Nullable<bool> Deteted { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int RoleTypeId { get; set; }
        public Nullable<int> DepartmentId { get; set; }

        public virtual DepartmentBO Department { get; set; }

        public RoleTypeBO RoleType { get; set; }
        public string RoleName
        {
            get { return RoleType.RoleName; }
            set { RoleName = value; }
        }

        public string DepartmentName
        {
            get;
            set;
        }
    
    }
}
