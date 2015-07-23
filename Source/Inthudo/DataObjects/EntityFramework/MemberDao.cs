using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    // Data access object for Member
    // ** DAO Pattern

    public class MemberDao : IMemberDao
    {
        static MemberDao()
        {
            Mapper.CreateMap<User, Member>();
            Mapper.CreateMap<Member, User>();
        }
        public List<Member> GetMembers(string sortExpression = "MemberId ASC")
        {
            using (var context = new InthudoEntities())
            {
                var members = context.Users.AsQueryable().OrderBy(sortExpression).ToList();
                return Mapper.Map<List<User>, List<Member>>(members);
            }
        }

        public Member GetMember(int memberId)
        {
            using (var context = new InthudoEntities())
            {
                var member = context.Users.FirstOrDefault(c => c.UserId == memberId) as User;
                return Mapper.Map<User, Member>(member);
            }
        }

        public Member GetMemberByEmail(string email)
        {
            using (var context = new InthudoEntities())
            {
                var member = context.Users.FirstOrDefault(c => c.Email == email) as User;
                return Mapper.Map<User, Member>(member);
            }
        }

        

        

        public void InsertMember(Member member)
        {
            using (var context = new InthudoEntities())
            {
                var entity = Mapper.Map<Member, User>(member);

                context.Users.Add(entity);
                context.SaveChanges();

                // update business object with new id
                member.MemberId = entity.UserId;
            }
            
        }

        public void UpdateMember(Member member)
        {
            using (var context = new InthudoEntities())
            {
                var entity = context.Users.SingleOrDefault(m => m.UserId == member.MemberId);
                entity.Email = member.Email;
                

                //context.Members.Attach(entity); 
                context.SaveChanges();
             
            }
        }

        public void DeleteMember(Member member)
        {
            using (var context = new InthudoEntities())
            {
                var entity = context.Users.SingleOrDefault(m => m.UserId == member.MemberId);
                context.Users.Remove(entity);
                context.SaveChanges();
            }
        }


        public Member GetMemberByOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetMembersWithOrderStatistics(string sortExpression)
        {
            throw new NotImplementedException();
        }
    }
}
