using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Common;

namespace Web.Modules
{
    public partial class Dashboard : BaseUserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            //Check roles
            if (this.LoggedInMember.RoleName.ToLower() == Constant.USER_ROLE_NAME.ToLower())
            {
                Response.Redirect("Login.aspx");
            }

            ddlCompany.Items.Clear();
            ddlCompany.Items.Add(new ListItem(string.Empty, "0"));
            List<OrganizationBO> orgs = this.MemberService.GetOrganizationsByMemberId(this.LoggedInUserId);
            foreach (OrganizationBO o in orgs)
            { 
                ddlCompany.Items.Add(new ListItem(o.Name, o.OrganizationId.ToString()));
            }            
        }

        protected void btFind_Click(object sender, EventArgs e)
        {
            DateTime? from = ctrlDateFrom.SelectedDate;
            DateTime? to = ctrlDateTo.SelectedDate;

            List<MemberBO> businessMen = this.MemberService.GetBusinessMen(this.LoggedInOrganizationIds);
            foreach (MemberBO m in businessMen)
            {
                BussinessReportByUser report = new BussinessReportByUser();
                report = (BussinessReportByUser)Page.LoadControl("~/Modules/BussinessReportByUser.ascx");
                report.ID = m.UserId.ToString();
                report.From = from;
                report.To = to;
                report.MemberId = m.UserId;
                panelBusiness.Controls.Add(report);
            }
            List<int> orgsId = new List<int>();
            if (int.Parse(ddlCompany.SelectedValue) == 0)
            {
                foreach (ListItem i in ddlCompany.Items)
                {
                    if (int.Parse(i.Value) != 0)
                        orgsId.Add(int.Parse(i.Value));
                }
            }
            else
            {
                orgsId.Add(int.Parse(ddlCompany.SelectedValue));
            }
            
            List<MemberBO> designers = this.MemberService.GetDesigners(orgsId);
            foreach (MemberBO m in designers)
            {
                DesignRequestReportByDesigner designReport = new DesignRequestReportByDesigner();
                designReport = (DesignRequestReportByDesigner)Page.LoadControl("~/Modules/DesignRequestReportByDesigner.ascx");
                designReport.MemberId = m.UserId;
                designReport.From = from;
                designReport.To = to;
                panelDesignRequest.Controls.Add(designReport);
            }
        }
    }
}