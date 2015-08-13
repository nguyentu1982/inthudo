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
            Mapper.CreateMap<LibRoleType, RoleTypeBO>();
            Mapper.CreateMap<RoleTypeBO, LibRoleType>();
        }
        public List<Member> GetMembers(string sortExpression = "UserId ASC")
        {
            using (context)
            {
                var members = context.Users.Where(m=> m.Deteted ==false || m.Deteted == null).AsQueryable().OrderBy(sortExpression).ToList();

                var roles = context.LibRoleTypes.ToList();
                return members.AsQueryable().Select(m =>
                    new Member
                    {
                        UserId = m.UserId,
                        Email = m.Email,
                        
                        RoleType = Mapper.Map<LibRoleType, RoleTypeBO>(roles.Where(r=> r.RoleTypeId == m.RoleTypeId).FirstOrDefault()),
                        FullName = m.FullName,
                        Address = m.Address,
                        Telephone = m.Telephone,
                        UserName = m.UserName
                    }
               )
               .ToList();

                //return Mapper.Map<List<User>, List<Member>>(members);
            }
        }

        public Member GetMember(int memberId)
        {
            using (context)
            {
                var member = context.Users.FirstOrDefault(c => c.UserId == memberId && (c.Deteted == false || c.Deteted ==null)) as User;
                return Mapper.Map<User, Member>(member);
            }
        }

        public Member GetMemberByEmail(string email)
        {
            using (context)
            {
                var member = context.Users.FirstOrDefault(c => c.Email == email) as User;
                return Mapper.Map<User, Member>(member);
            }
        }

        

        

        public void InsertMember(Member member)
        {
            using (context)
            {
                var entity = Mapper.Map<Member, User>(member);

                context.Users.Add(entity);
                context.SaveChanges();

                // update business object with new id
                member.UserId = entity.UserId;
            }
            
        }

        public void UpdateMember(Member member)
        {
            using (context)
            {
                var entity = context.Users.SingleOrDefault(m => m.UserId == member.UserId);
                entity.Email = member.Email;
                entity.UserName = member.UserName;
                entity.Address = member.Address;
                entity.Telephone = member.Telephone;
                entity.RoleTypeId = member.RoleTypeId;
                entity.LastEditedOn = member.LastEditedOn;
                entity.FullName = member.FullName;

                //context.Members.Attach(entity); 
                context.SaveChanges();
             
            }
        }

        public void DeleteMember(Member member)
        {
            using (context )
            {
                var entity = context.Users.SingleOrDefault(m => m.UserId == member.UserId);
                entity.Deteted = true;
                
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


        public bool Login(string user, string pass)
        {
            using(context)
            {
                var member = context.Users.Where(m=> m.UserName == user && m.Password == pass &&(m.Deteted == null || m.Deteted ==false));
                if(member.Count() == 1)
                {
                    return true;    
                }
                return false;
                
            }
        }


        public void ChangePass(int userId, string pass)
        {
            using (context )
            {
                var mem = context.Users.SingleOrDefault(m => m.UserId == userId);
                if (mem == null) return;
                mem.Password = pass;
                context.SaveChanges();
            }
        }


        public List<Member> GetMembers(string username, string email, string fullName, string address, int roletypeId)
        {
            using (context)
            {
                var query = from mem in context.Users
                            where
                            (String.IsNullOrEmpty(username) || mem.UserName.Contains(username))&&
                            (String.IsNullOrEmpty(email) || mem.Email.Contains(email))&&
                            (String.IsNullOrEmpty(fullName)|| mem.FullName.Contains(fullName))&&
                            (String.IsNullOrEmpty(address)|| mem.FullName.Contains(address))&&
                            (roletypeId ==0 || mem.RoleTypeId == roletypeId)&&
                            (mem.Deteted == null || mem.Deteted == false)
                            select mem;

                var roles = context.LibRoleTypes;
                return query.ToList().AsQueryable().Select(m =>
                    new Member
                    { 
                        UserId = m.UserId,
                        Email = m.Email,

                        RoleType = Mapper.Map<LibRoleType, RoleTypeBO>(roles.Where(r => r.RoleTypeId == m.RoleTypeId).FirstOrDefault()),
                        FullName = m.FullName,
                        Address = m.Address,
                        Telephone = m.Telephone,
                        UserName = m.UserName
                    }
               )
               .ToList();

            }
        }


        public Member GetMemberByTelephone(string telephone)
        {
            using (context)
            {
                var member = context.Users.FirstOrDefault(c => c.Telephone== telephone) as User;
                return Mapper.Map<User, Member>(member);
            }
        }

        public Member GetMemberByUserName(string userName)
        {
            using (context)
            {
                var member = context.Users.FirstOrDefault(c => c.UserName == userName) as User;
                return Mapper.Map<User, Member>(member);
            }
        }


        public Member GetMember(string user, string pass)
        {
            throw new NotImplementedException();
        }

        public InThuDoEntities context
        {
            get
            {
                return new InThuDoEntities();
            }
        }
    }
}
