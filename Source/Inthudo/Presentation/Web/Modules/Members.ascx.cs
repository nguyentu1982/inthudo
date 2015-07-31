using InthudoService;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Modules
{
    public partial class Members : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefaultData();
            }
        }

        private void LoadDefaultData()
        {
            //Binding data Role dropdown list
            ddlRoles.Items.Clear();
            ddlRoles.Items.Add(new ListItem("", "0"));

            IRoleTypeService roleTypeService = new RoleTypeService();
            List<RoleType> roles = roleTypeService.GetRoleTypes();

            for (int i = 0; i < roles.Count; i++)
            {
                ddlRoles.Items.Add(new ListItem(roles[i].RoleName, roles[i].RoleTypeId.ToString()));
            }

            //Binding gridview
            IMemberService memberService = new MemberService();
            string orderBy = "UserId ASC";
            grvMembers.DataSource = memberService.GetMembers(orderBy);
            grvMembers.DataBind();
        }

        protected void btAddMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberAdd.aspx");
        }

        protected void btFindMember_Click(object sender, EventArgs e)
        {
            IMemberService memberService = new MemberService();
            grvMembers.DataSource = memberService.GetMembers(txtUserName.Text, txtEmail.Text, txtFullName.Text, txtTelephone.Text,int.Parse(ddlRoles.SelectedValue));
            grvMembers.DataBind();
        }

        protected void btDeleteUser_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvMembers.Rows)
            {
                CheckBox chk = row.Cells[0].Controls[1] as CheckBox;
                if (chk != null && chk.Checked)
                {
                    int userId = int.Parse(row.Cells[1].Text);
                    IMemberService memberService = new MemberService();
                    Member mem = memberService.GetMember(userId);
                    if(mem != null)
                    {
                        try
                        {
                            memberService.DeleteMember(mem);
                            
                        }
                        catch (Exception ex)
                        {
                            ProcessException(ex);
                        }
                    }                    
                }
            }

            btFindMember_Click(new object(), new EventArgs());
        }
    }
}