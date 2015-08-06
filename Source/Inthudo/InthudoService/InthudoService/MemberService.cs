using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace InthudoService
{
    public partial class MemberService:ServiceBase,IMemberService 
    {
        static readonly IMemberDao memberDao = factory.MemberDao;

        public Member GetMember(int memberId)
        {
            return memberDao.GetMember(memberId);
        }

        public Member GetMemberByEmail(string email)
        {
            return memberDao.GetMemberByEmail(email);
        }

        public List<Member> GetMembers(string sortExpression)
        {
            return memberDao.GetMembers(sortExpression);
        }

        public Member GetMemberByOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetMembersWithOrderStatistics(string sortExpression)
        {
            throw new NotImplementedException();
        }

        public void InsertMember(Member member)
        {
            memberDao.InsertMember(member);
        }        

        public void UpdateMember(Member member)
        {
            memberDao.UpdateMember(member);
        }

        public void DeleteMember(Member member)
        {
            memberDao.DeleteMember(member);
        }

        public bool ValidateUser(string user, string pass)
        {
            return memberDao.Login(user, pass);
        }


        public void ChangePass(int userId, string pass)
        {
            memberDao.ChangePass(userId, pass);
        }


        public List<Member> GetMembers(string username, string email, string fullName, string address, int roletypeId)
        {
            return memberDao.GetMembers(username, email, fullName, address, roletypeId);
        }


        public void InsertMember(Member member, out MemberStatus status)
        {
            status = MemberStatus.Success;
            if (this.GetMemberByUserName(member.UserName) != null)
            {
                status = MemberStatus.DupplicateUser;            
            }

            if (this.GetMemberByEmail(member.Email) != null)
            {
                status = MemberStatus.DupplicateEmail;
            }

            if (this.GetMemberByTelephone(member.Telephone) != null)
            {
                status = MemberStatus.DupplicateTelephone;
            }

            if (status == MemberStatus.Success)
            {
                InsertMember(member);
            }
        }

        public Member GetMemberByTelephone(string telephone)
        {
           return memberDao.GetMemberByTelephone(telephone);
        }

        public Member GetMemberByUserName(string userName)
        {
            return memberDao.GetMemberByUserName(userName);
        }


        public Member GetMember(string user)
        {
            return memberDao.GetMemberByUserName(user);
        }


       
    }
}
