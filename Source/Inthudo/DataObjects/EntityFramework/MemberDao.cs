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
            Mapper.CreateMap<User, MemberBO>();
            Mapper.CreateMap<MemberBO, User>();
            Mapper.CreateMap<LibRoleType, RoleTypeBO>();
            Mapper.CreateMap<RoleTypeBO, LibRoleType>();
        }
        public List<MemberBO> GetMembers(string sortExpression = "UserId ASC")
        {
            using (context)
            {
                var members = context.Users.Where(m=> m.Deteted ==false || m.Deteted == null).AsQueryable().OrderBy(sortExpression).ToList();

                var roles = context.LibRoleTypes.ToList();
                return members.AsQueryable().Select(m =>
                    new MemberBO
                    {
                        UserId = m.UserId,
                        Email = m.Email,
                        
                        RoleType = new RoleTypeBO()
                        {
                          RoleTypeId = m.RoleTypeId,
                          RoleName = m.LibRoleType.RoleName,
                          RoleDescription = m.LibRoleType.RoleDescription
                        },
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

        public MemberBO GetMember(int memberId)
        {
            using (context)
            {
                var member = context.Users.FirstOrDefault(c => c.UserId == memberId && (c.Deteted == false || c.Deteted ==null)) as User;
                return Mapper.Map<User, MemberBO>(member);
            }
        }

        public MemberBO GetMemberByEmail(string email)
        {
            using (context)
            {
                var member = context.Users.FirstOrDefault(c => c.Email == email) as User;
                return Mapper.Map<User, MemberBO>(member);
            }
        }

        

        

        public void InsertMember(MemberBO member)
        {
            using (var context = new InThuDoEntities())
            {
                var entity = Mapper.Map<MemberBO, User>(member);

                context.Users.Add(entity);
                context.SaveChanges();

                // update business object with new id
                member.UserId = entity.UserId;
            }
            
        }

        public void UpdateMember(MemberBO member)
        {
            using (var context = new InThuDoEntities())
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

        public void DeleteMember(MemberBO member)
        {
            using (var context = new InThuDoEntities())
            {
                var entity = context.Users.SingleOrDefault(m => m.UserId == member.UserId);
                entity.Deteted = true;
                
                context.SaveChanges();
            }
        }


        public MemberBO GetMemberByOrder(int orderId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from u in context.Users
                            join o in context.Orders on u.UserId equals o.UserId
                            where
                            (o.OrderId == orderId)
                            select new MemberBO()
                            {
                                UserId = u.UserId,
                                Email = u.Email,                               
                                FullName = u.FullName,
                                Address = u.Address,
                                Telephone = u.Telephone,
                                UserName = u.UserName
                            };
                return query.FirstOrDefault();
            }
        }

        public List<MemberBO> GetMembersWithOrderStatistics(string sortExpression)
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
            using (var context = new InThuDoEntities())
            {
                var mem = context.Users.SingleOrDefault(m => m.UserId == userId);
                if (mem == null) return;
                mem.Password = pass;
                context.SaveChanges();
            }
        }


        public List<MemberBO> GetMembers(string username, string email, string fullName, string telephone, int roletypeId)
        {
            using (context)
            {
                var query = from mem in context.Users
                            where
                            (String.IsNullOrEmpty(username) || mem.UserName.Contains(username))&&
                            (String.IsNullOrEmpty(email) || mem.Email.Contains(email))&&
                            (String.IsNullOrEmpty(fullName)|| mem.FullName.Contains(fullName))&&
                            (String.IsNullOrEmpty(telephone)|| mem.FullName.Contains(telephone))&&
                            (roletypeId ==0 || mem.RoleTypeId == roletypeId)&&
                            (mem.Deteted == null || mem.Deteted == false)
                            select mem;

                var roles = context.LibRoleTypes;
                return query.ToList().AsQueryable().Select(m =>
                    new MemberBO
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


        public MemberBO GetMemberByTelephone(string telephone)
        {
            using (context)
            {
                var member = context.Users.FirstOrDefault(c => c.Telephone== telephone) as User;
                return Mapper.Map<User, MemberBO>(member);
            }
        }

        public MemberBO GetMemberByUserName(string userName)
        {
            using (context)
            {
                var member = context.Users.FirstOrDefault(c => c.UserName == userName) as User;
                return Mapper.Map<User, MemberBO>(member);
            }
        }


        public MemberBO GetMember(string user, string pass)
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
