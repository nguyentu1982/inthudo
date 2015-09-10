using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    
    // defines methods to access Members.
    // this is a database-independent interface. Implementations are database specific
    // ** DAO Pattern

    
    public interface IMemberDao
    {
        // gets a specific Member

        MemberBO GetMember(int memberId);

        // gets a specific Member by email

        MemberBO GetMemberByEmail(string email);

        // gets a sorted list of all Members

        List<MemberBO> GetMembers(string sortExpression = "MemberId ASC");

        // gets Member given an order

        MemberBO GetMemberByOrder(int orderId);

        // gets Members with order statistics in given sort order.

        List<MemberBO> GetMembersWithOrderStatistics(string sortExpression);

        // inserts a new Member
        // following insert, Member object will contain the new identifier
        
        int InsertMember(MemberBO member);

        // updates a Member

        void UpdateMember(MemberBO member);

        // deletes a Member

        void DeleteMember(MemberBO member);

        bool Login(string user, string pass);

        void ChangePass(int userId, string pass);

        List<MemberBO> GetMembers(string username, string email, string fullName, string telephone, int roletypeId, int departId, int organizationId);

        MemberBO GetMemberByTelephone(string telephone);

        MemberBO GetMemberByUserName(string userName);

        MemberBO GetMember(string user, string pass);

        List<DepartmentBO> GetAllDepartment();

        List<OrganizationBO> GetAllOrganization();

        List<OrganizationBO> GetOrganizationsByMemberId(int memberId);

        void InsertUserOrganizationMapping(int memberId, int organizationId);

        void DeleteUserOrganizationMapping(int memberId, int organizationId);

        List<MemberBO> GetDesigners(List<int> orgsId);

        List<MemberBO> GetBusinessMen(List<int> orgsId);
    }
}
