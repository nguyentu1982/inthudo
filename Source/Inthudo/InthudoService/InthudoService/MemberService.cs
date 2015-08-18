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

        public MemberBO GetMember(int memberId)
        {
            return memberDao.GetMember(memberId);
        }

        public MemberBO GetMemberByEmail(string email)
        {
            return memberDao.GetMemberByEmail(email);
        }

        public List<MemberBO> GetMembers(string sortExpression)
        {
            return memberDao.GetMembers(sortExpression);
        }

        public MemberBO GetMemberByOrder(int orderId)
        {
            return memberDao.GetMemberByOrder(orderId);
        }

        public List<MemberBO> GetMembersWithOrderStatistics(string sortExpression)
        {
            throw new NotImplementedException();
        }

        public void InsertMember(MemberBO member)
        {
            memberDao.InsertMember(member);
        }        

        public void UpdateMember(MemberBO member)
        {
            memberDao.UpdateMember(member);
        }

        public void DeleteMember(MemberBO member)
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


        public List<MemberBO> GetMembers(string username, string email, string fullName, string telephone, int roletypeId)
        {
            return memberDao.GetMembers(username, email, fullName, telephone, roletypeId);
        }


        public void InsertMember(MemberBO member, out MemberStatus status)
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

        public MemberBO GetMemberByTelephone(string telephone)
        {
           return memberDao.GetMemberByTelephone(telephone);
        }

        public MemberBO GetMemberByUserName(string userName)
        {
            return memberDao.GetMemberByUserName(userName);
        }


        public MemberBO GetMember(string user)
        {
            return memberDao.GetMemberByUserName(user);
        }


       
    }
}
