using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class OrganizationSelect : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                //Fill dropdown
                FillDropdown();
                //BindData
                BindData();
            }
        }

        private void BindData()
        {
            List<OrganizationBO> orgs = this.MemberService.GetOrganizationsByMemberId(this.MemeberId);
            if (orgs.Count > 0)
            {
                grvUserOrganiztion.Visible = true;
                grvUserOrganiztion.DataSource = orgs;
                grvUserOrganiztion.DataBind();
                lbUserNotInAnyOrganization.Visible = false;
            }
            else
            {
                grvUserOrganiztion.Visible = false;
            }
        }

        private void FillDropdown()
        {
            List<OrganizationBO> organizations = this.MemberService.GetAllOrganization();
            ddlOrgainzation.Items.Clear();
            foreach (OrganizationBO o in organizations)
            { 
                ddlOrgainzation.Items.Add(new ListItem(o.Name, o.OrganizationId.ToString()));
            }

           
        }

        public int MemeberId
        {
            get
            {
                return CommonHelper.QueryStringInt("MemberId");
            }
        }

        protected void btAddNewOrganization_Click(object sender, EventArgs e)
        {
            int organizationId = int.Parse(ddlOrgainzation.SelectedValue);
            try
            {
                this.MemberService.InsertUserOrganizationMapping(this.MemeberId, organizationId);
                BindData();
            }
            catch(Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void grvUserOrganiztion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUserOrganizationMap")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grvUserOrganiztion.Rows[index];
                int organizationId= int.Parse(row.Cells[0].Text);

                this.MemberService.DeleteUserOrganizationMapping(this.MemeberId, organizationId);
                BindData();
            }
        }
    }
}