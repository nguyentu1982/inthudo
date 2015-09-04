using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Common.Utils;

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
                BindData();
            }
        }

        private void BindData()
        {
            if (this.MemberId != 0)
                ddlDesigner.SelectedValue = this.MemberId.ToString();
            if (this.From != null)
                ctrlDatePickerFrom.SelectedDate = this.From;
            if (this.To != null)
                ctrlDatePickerTo.SelectedDate = this.To;
            if (this.DesignRequestStatus != 0)
                ddlDesignRequestStatus.SelectedValue = ((int)this.DesignRequestStatus).ToString();
            if (this.MemberId != 0)
                btFind_Click(new object(), new EventArgs());
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
            //ddlDesignRequestStatus.Items.Clear();
            //ddlDesignRequestStatus.Items.Add(new ListItem(string.Empty, "0"));
            //List<OrderStatusBO> status = this.OrderService.GetAllOrderStatus();
            //foreach (OrderStatusBO s in status)
            //{
            //    ddlDesignRequestStatus.Items.Add(new ListItem(s.Name, s.OrderStatusId.ToString()));
            //}
            //Designer
            ddlDesigner.Items.Clear();
            ddlDesigner.Items.Add(new ListItem(string.Empty, "0"));

            List<MemberBO> designers = this.MemberService.GetDesigners(base.LoggedInOrganizationIds);
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
                DesignRequestStatus = (DesignRequestStatusEnum)designRequestStatus,
                DesignerId = designerId
            };

            List<DesignRequestBO> designRequests = this.OrderService.GetDesignRequests(searchObj);
            grvDesignRequest.DataSource = designRequests;
            grvDesignRequest.DataBind();

            List<OrderDetailBO> orderDetail = new List<OrderDetailBO>();
            foreach(DesignRequestBO dr in designRequests)
            {
                orderDetail.Add(this.OrderService.GetOrderDetailById(dr.OrderItemId));    
            }

            lbTotalRequest.Text = designRequests.Count.ToString();
            lbTotalDesignRequestCreated.Text = designRequests.Where(od => od.DesignRequestStatus == DesignRequestStatusEnum.DesignRequestCreated).Count().ToString();
            lbTotalDesignRequestDesigning.Text = designRequests.Where(od => od.DesignRequestStatus == DesignRequestStatusEnum.Designing).Count().ToString();
            lbTotalDesignRequestWaitForApproved.Text = designRequests.Where(od => od.DesignRequestStatus == DesignRequestStatusEnum.DesignCopmleted).Count().ToString();
            lbTotalDesignRequestApproved.Text = designRequests.Where(od => od.DesignRequestStatus == DesignRequestStatusEnum.DesignApprovedByCustomer).Count().ToString();
            lbTotalDesignRequestNOTApproved.Text = designRequests.Where(od => od.DesignRequestStatus == DesignRequestStatusEnum.DesignNotApproved).Count().ToString();
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

        public int MemberId
        {
            get
            {
                return CommonHelper.QueryStringInt("MemberId");
            }
        }

        public DateTime From
        {
            get
            {
                DateTime from;
                DateTime.TryParse(CommonHelper.QueryString("From"), out from);
                return from;
            }
        }

        public DateTime To
        {
            get
            {
                DateTime to;
                DateTime.TryParse(CommonHelper.QueryString("To"), out to);
                return to;
            }
        }

        public DesignRequestStatusEnum DesignRequestStatus
        {
            get
            {
                return (DesignRequestStatusEnum)CommonHelper.QueryStringInt("DesignRequestStatus");
            }
        }
    }
}