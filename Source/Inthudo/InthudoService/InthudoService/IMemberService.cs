using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace InthudoService
{
    public interface IMemberService
    {
        #region Member Repository

        MemberBO GetMember(int memberId);
        MemberBO GetMemberByEmail(string email);
        List<MemberBO> GetMembers(string sortExpression);
        MemberBO GetMemberByOrder(int orderId);
        List<MemberBO> GetMembersWithOrderStatistics(string sortExpression);
        int InsertMember(MemberBO member);
        int InsertMember(MemberBO member, out MemberStatus status);
        void UpdateMember(MemberBO member);
        void DeleteMember(MemberBO member);
        bool ValidateUser(string user, string pass);
        void ChangePass(int userId, string pass);
        MemberBO GetMemberByUserName(string user);
        #endregion Member Repository

        List<MemberBO> GetMembers(string username, string email, string fullName, string telephone, int roletypeId, int departId);

        List<DepartmentBO> GetAllDepartment();
    }
}
