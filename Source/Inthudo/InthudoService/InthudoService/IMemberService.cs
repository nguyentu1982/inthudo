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

        Member GetMember(int memberId);
        Member GetMemberByEmail(string email);
        List<Member> GetMembers(string sortExpression);
        Member GetMemberByOrder(int orderId);
        List<Member> GetMembersWithOrderStatistics(string sortExpression);
        void InsertMember(Member member);
        void InsertMember(Member member, out MemberStatus status);
        void UpdateMember(Member member);
        void DeleteMember(Member member);
        bool ValidateUser(string user, string pass);
        void ChangePass(int userId, string pass);
        Member GetMemberByUserName(string user);
        #endregion Member Repository

        List<Member> GetMembers(string username, string email, string fullName, string address, int roletypeId);
    }
}
