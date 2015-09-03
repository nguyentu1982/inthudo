using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using InthudoService;
using Common.Utils;
using Common;

namespace Web.Modules
{
    public partial class MemberInfo : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {               
                this.FillDropDowns();
                this.BindData();
            }
        }

        private void BindData()
        {
            IMemberService memberService = new MemberService();
            MemberBO member = memberService.GetMember(this.MemeberId);
            if (member != null)
            {
                if (this.LoggedInMember.RoleName.ToLower() == Constant.USER_ROLE_NAME.ToLower())
                {
                    if (this.LoggedInUserId != this.MemeberId)
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                txtUserName.Text = member.UserName;
                ddlDepartment.SelectedValue = member.DepartmentId.ToString();
                txtFullName.Text = member.FullName;
                txtAdress.Text = member.Address;
                txtTelephone.Text = member.Telephone;
                txtEmail.Text = member.Email;
                lbUserId.Text = member.UserId.ToString();
                ddlRoleType.SelectedValue = member.RoleTypeId.ToString();
            }
            else
            {
                pnlUserId.Visible = false;
                btChangePass.Visible = false;
                ctrlOrganizationSelect.Visible = false;
            }
        }

        private void FillDropDowns()
        {
            ddlRoleType.Items.Clear();
           

            IRoleTypeService roleTypeService = new RoleTypeService();
            List<RoleTypeBO> roles = roleTypeService.GetRoleTypes();

            for (int i = 0; i < roles.Count; i++)
            {
                ddlRoleType.Items.Add(new ListItem(roles[i].RoleName, roles[i].RoleTypeId.ToString()));
            }

            //Department Dropdown list
            ddlDepartment.Items.Clear();            
            List<DepartmentBO> departments = this.MemberService.GetAllDepartment();
            foreach (DepartmentBO d in departments)
            {
                ddlDepartment.Items.Add(new ListItem(d.Name, d.DepartmentId.ToString()));
            }
        }

        public MemberBO SaveInfo()
        {
            IMemberService memberService = new MemberService();
            MemberBO member = memberService.GetMember(this.MemeberId);
            if (member != null)
            {
                member.UserName = txtUserName.Text;
                member.DepartmentId = int.Parse(ddlDepartment.SelectedValue);
                member.FullName = txtFullName.Text;
                member.Address = txtAdress.Text;
                member.Telephone = txtTelephone.Text;
                member.Email = txtEmail.Text;
                member.RoleTypeId = int.Parse(ddlRoleType.SelectedValue);
                member.LastEditedOn = DateTime.Now;
                memberService.UpdateMember(member);
            }
            else
            {
                member = new MemberBO()
                {
                    UserName = txtUserName.Text,
                    Password = txtPassword.Text,
                    FullName = txtFullName.Text,
                    Address = txtAdress.Text,
                    Telephone = txtTelephone.Text,
                    Email = txtEmail.Text,
                    RoleTypeId = int.Parse(ddlRoleType.SelectedValue),
                    CreatedOn = DateTime.Now,
                    DepartmentId = int.Parse(ddlDepartment.SelectedValue)
                };
                MemberStatus status = MemberStatus.Success;
                member.UserId = memberService.InsertMember(member, out status);

                if (status != MemberStatus.Success)
                {
                    throw new InthudoException(string.Format("Không thể tạo user: {0}",status.ToString()));
                }
            }
            return member;
        }        

        protected void btChangePass_Click(object sender, EventArgs e)
        {
            IMemberService memberService = new MemberService();
            MemberBO member = memberService.GetMember(this.MemeberId);
            if (member == null) return;
            memberService.ChangePass(this.MemeberId, txtPassword.Text);

        }

        public int MemeberId
        {
            get
            {
                return CommonHelper.QueryStringInt("MemberId");
            }
        }
    }
}