using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class DesignRequests : BaseUserControl
    {
        const string Not_Allow_Designer_View_Others_Design_Request = "NotAllowDesignerViewOthersDesignRequest";
        const string Not_Allow_User_Of_Other_Department_View_Design_Request = "NotAllowUserOfOtherDepartmentViewDesignRequest";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDefaultData();
            }
        }

        private void LoadDefaultData()
        {
            //Products
            ddlProducts.Items.Clear();
            ddlProducts.Items.Add(new ListItem(string.Empty, "0"));
            List<ProductBO> products = this.ProductService.GetAllProucts();
            foreach (ProductBO p in products)
            {
                ddlProducts.Items.Add(new ListItem( p.Name, p.ProductId.ToString()));
            }
            //Status
            ddlDesignRequestStatus.Items.Clear();
            ddlDesignRequestStatus.Items.Add(new ListItem(string.Empty, "0"));
            List<OrderStatusBO> status = this.OrderService.GetAllOrderStatus();
            foreach (OrderStatusBO s in status)
            {
                ddlDesignRequestStatus.Items.Add(new ListItem(s.Name, s.OrderStatusId.ToString()));
            }
            //Designer
            ddlDesigner.Items.Clear();
            ddlDesigner.Items.Add(new ListItem(string.Empty, "0"));
            List<MemberBO> designers = this.MemberService.GetDesigners();
            foreach (MemberBO m in designers)
            {
                ddlDesigner.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }

            //Check Roles and Department to bindata
            MemberBO mem = this.MemberService.GetMember(this.LoggedInUserId);

            if (this.SettingService.GetBoolSetting(Not_Allow_User_Of_Other_Department_View_Design_Request))
            {
                if (mem.RoleName == "User")
                {
                    if (mem.Department.Code != "PTK") Response.Redirect("Login.aspx");
                }
            }

            if ( this.SettingService.GetBoolSetting(Not_Allow_Designer_View_Others_Design_Request))
            {
                if (mem.RoleName == "User")
                {                   
                    ddlDesigner.SelectedValue = mem.UserId.ToString();
                    ddlDesigner.Enabled = false;
                }
            }
        }

        protected void btFind_Click(object sender, EventArgs e)
        {
            DateTime? requestFrom = ctrlDatePickerFrom.SelectedDate;

            DateTime? requestTo = ctrlDatePickerTo.SelectedDate;
            
            int custId = 0;
            int.TryParse(ctrlCustomerSelect.CustomerCode, out custId);
            
            int productId = 0;
            int.TryParse(ddlProducts.SelectedValue, out productId);
            
            int designRequestStatus = 0;
            int.TryParse(ddlDesignRequestStatus.SelectedValue, out designRequestStatus);

            int designerId = 0;
            int.TryParse(ddlDesigner.SelectedValue, out designerId);

            DesignRequestSearch searchObj = new DesignRequestSearch()
            {
                RequestFrom = requestFrom,
                RequestTo = requestTo,
                CustomerId = custId,
                ProductId = productId,
                OrderDetailStatus = (OrderDetailStatusEnum)designRequestStatus,
                DesignerId = designerId
            };

            List<DesignRequestBO> designRequests = this.OrderService.GetDesignRequests(searchObj);
            grvDesignRequest.DataSource = designRequests;
            grvDesignRequest.DataBind();
        }

        protected void grvDesignRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int designRequestId = int.Parse(e.Row.Cells[0].Text);
                DesignRequestBO ds = this.OrderService.GetDesignRequestById(designRequestId);

                if (ds != null)
                {
                    int orderId = ds.OrderItem.OrderId;
                    int orderDetailId = ds.OrderItem.OrderItemId;
                    HyperLink designRequestHyperLink = e.Row.Cells[7].FindControl("hlDesignRequest") as HyperLink;
                    string url = string.Format("/DesignRequestEdit.aspx?OrderId={0}&OrderDetailId={1}&DesignRequestId={2}", orderId, orderDetailId, designRequestId);
                    designRequestHyperLink.Attributes.Add("onclick", "OpenWindow('" + url + "')");
                    designRequestHyperLink.Attributes.Add("class", "a-popup");
                    designRequestHyperLink.Text = "Xem";
                }
            }
        }
    }
}