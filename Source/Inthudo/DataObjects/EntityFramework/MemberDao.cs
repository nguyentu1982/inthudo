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
            using (var context = new InThuDoEntities())
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
                        UserName = m.UserName,
                        Department = new DepartmentBO()
                        {
                            DepartmentId = m.LibDepartment.DepartmentId,
                            Code = m.LibDepartment.Code,
                            Name = m.LibDepartment.Name,
                            Description = m.LibDepartment.Description
                        }
                    }
               )
               .ToList();

                //return Mapper.Map<List<User>, List<Member>>(members);
            }
        }

        public MemberBO GetMember(int memberId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from u in context.Users
                            join d in context.LibDepartments on u.DepartmentId equals d.DepartmentId into departGroup
                            from d2 in departGroup.DefaultIfEmpty()
                            where
                            (u.UserId == memberId)
                            select new MemberBO()
                            {
                                UserId = u.UserId,
                                Email = u.Email,
                                RoleTypeId = u.RoleTypeId,
                                DepartmentId = u.DepartmentId,
                                RoleType = new RoleTypeBO()
                                {
                                    RoleTypeId = u.RoleTypeId,
                                    RoleName = u.LibRoleType.RoleName,
                                    RoleDescription = u.LibRoleType.RoleDescription
                                },
                                FullName = u.FullName,
                                Address = u.Address,
                                Telephone = u.Telephone,
                                UserName = u.UserName,
                                Department = new DepartmentBO() { 
                                    DepartmentId = d2.DepartmentId,
                                    Code = d2.Code,
                                    Name = d2.Name,
                                    Description = d2.Description
                                }
                            };

                return query.FirstOrDefault();
            }
        }

        public MemberBO GetMemberByEmail(string email)
        {
            using (var context = new InThuDoEntities())
            {
                var member = context.Users.FirstOrDefault(c => c.Email == email) as User;
                return Mapper.Map<User, MemberBO>(member);
            }
        }

        public int InsertMember(MemberBO member)
        {
            using (var context = new InThuDoEntities())
            {
                var entity = Mapper.Map<MemberBO, User>(member);

                context.Users.Add(entity);
                context.SaveChanges();

                // update business object with new id
                return entity.UserId;
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
                entity.DepartmentId = member.DepartmentId;
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
            using(var context = new InThuDoEntities())
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

        public List<MemberBO> GetMembers(string username, string email, string fullName, string telephone, int roletypeId, int departId, int organizationId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from mem in context.Users
                            join uom in context.UserOrganizationMapppings on mem.UserId equals uom.UserId into uomGroup
                            from uom2 in uomGroup.DefaultIfEmpty()
                            where
                            (String.IsNullOrEmpty(username) || mem.UserName.Contains(username))&&
                            (String.IsNullOrEmpty(email) || mem.Email.Contains(email))&&
                            (String.IsNullOrEmpty(fullName)|| mem.FullName.Contains(fullName))&&
                            (String.IsNullOrEmpty(telephone)|| mem.FullName.Contains(telephone))&&
                            (roletypeId ==0 || mem.RoleTypeId == roletypeId)&&
                            (departId == 0 || mem.DepartmentId == departId)&&
                            (organizationId ==0 || uom2.OrganizationId == organizationId)&&
                            (mem.Deteted == null || mem.Deteted == false)
                            select mem;

                var roles = context.LibRoleTypes;
                return query.ToList().AsQueryable().Select(m =>
                    new MemberBO
                    {
                        UserId = m.UserId,
                        Email = m.Email,
                        DepartmentId = m.DepartmentId,
                        RoleType = Mapper.Map<LibRoleType, RoleTypeBO>(roles.Where(r => r.RoleTypeId == m.RoleTypeId).FirstOrDefault()),
                        FullName = m.FullName,
                        Address = m.Address,
                        Telephone = m.Telephone,
                        UserName = m.UserName,
                        Department = new DepartmentBO()
                        {
                            DepartmentId = m.LibDepartment.DepartmentId,
                            Code = m.LibDepartment.Code,
                            Name = m.LibDepartment.Name,
                            Description = m.LibDepartment.Description
                        }
                    }
               )
               .ToList();

            }
        }


        public MemberBO GetMemberByTelephone(string telephone)
        {
            using (var context = new InThuDoEntities())
            {
                var member = context.Users.FirstOrDefault(c => c.Telephone== telephone) as User;
                return Mapper.Map<User, MemberBO>(member);
            }
        }

        public MemberBO GetMemberByUserName(string userName)
        {
            using (var context = new InThuDoEntities())
            {
                var member = context.Users.FirstOrDefault(c => c.UserName == userName) as User;
                var result = Mapper.Map<User, MemberBO>(member);
                result.Department = new DepartmentBO() { 
                    DepartmentId = member.LibDepartment.DepartmentId,
                    Code = member.LibDepartment.Code,
                    Name = member.LibDepartment.Name,
                    Description = member.LibDepartment.Description
                };
                return result;
            }
        }

        public MemberBO GetMember(string user, string pass)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentBO> GetAllDepartment()
        {
            using (var context = new InThuDoEntities())
            {
                var query = from d in context.LibDepartments
                            select new DepartmentBO() { 
                                DepartmentId = d.DepartmentId,
                                Name = d.Name,
                                Description = d.Description
                            };
                return query.ToList();
            }
        }

        public List<OrganizationBO> GetAllOrganization()
        {
            using (var context = new InThuDoEntities())
            {
                var query = from or in context.Organizations
                            select new OrganizationBO() { 
                                OrganizationId = or.OrganizationId,
                                Name = or.Name,
                                Address = or.Address,
                                Description = or.Description,
                                FaxNumber = or.FaxNumber,
                                PhoneNumber = or.PhoneNumber,
                                TaxCode = or.TaxCode
                            };
                if (query == null) return new List<OrganizationBO>();

                return query.ToList();
            }
        }

        public List<OrganizationBO> GetOrganizationsByMemberId(int memberId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from om in context.UserOrganizationMapppings
                            where om.UserId == memberId
                            select new OrganizationBO() { 
                                OrganizationId = om.OrganizationId,
                                Name = om.Organization.Name,
                                Address = om.Organization.Address,
                                Description = om.Organization.Description,
                                FaxNumber = om.Organization.FaxNumber,
                                PhoneNumber = om.Organization.PhoneNumber,
                                TaxCode = om.Organization.TaxCode
                            };
                if (query == null) return new List<OrganizationBO>();

                return query.ToList();
            }
        }


        public void InsertUserOrganizationMapping(int memberId, int organizationId)
        {
            using (var context = new InThuDoEntities())
            {
                UserOrganizationMappping userOrganization = new UserOrganizationMappping()
                {
                    OrganizationId = organizationId,
                    UserId = memberId,
                };

                context.UserOrganizationMapppings.Add(userOrganization);
                context.SaveChanges();
            }
        }

        public void DeleteUserOrganizationMapping(int memberId, int organizationId)
        {
            using (var context = new InThuDoEntities())
            {
                UserOrganizationMappping userOrganizationMap = context.UserOrganizationMapppings.Where(uo => uo.UserId == memberId && uo.OrganizationId == organizationId).FirstOrDefault();
                if (userOrganizationMap == null)
                    return;

                context.UserOrganizationMapppings.Remove(userOrganizationMap);
                context.SaveChanges();
            }
        }

        public List<MemberBO> GetDesigners(int organizationId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from m in context.Users
                            join d in context.LibDepartments on m.DepartmentId equals d.DepartmentId
                            join o in context.UserOrganizationMapppings on m.UserId equals o.UserId
                            where
                            (m.Deteted == false || m.Deteted == null) &&
                            (d.Code == "PTK")&&
                            (organizationId ==0 || o.OrganizationId == organizationId)
                            select new MemberBO()
                            {
                                UserId = m.UserId,
                                UserName = m.UserName,
                                FullName = m.FullName,
                            };
                return query.Distinct().ToList();
            }
        }

        public List<MemberBO> GetBusinessMen(int organizationId)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from m in context.Users
                            join d in context.LibDepartments on m.DepartmentId equals d.DepartmentId
                            join o in context.UserOrganizationMapppings on m.UserId equals o.UserId
                            where
                            (m.Deteted == false || m.Deteted == null) &&
                            (d.Code == "PKD")&&
                             (organizationId == 0 || o.OrganizationId == organizationId)
                            select new MemberBO()
                            {
                                UserId = m.UserId,
                                UserName = m.UserName,
                                FullName = m.FullName,
                            };
                return query.Distinct().ToList();
            }
        }
    }
}
