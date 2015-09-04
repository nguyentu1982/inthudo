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
    public partial class Orders : BaseUserControl
    {
        const string Not_Allow_Business_Man_View_Others_Order = "NotAllowBusinessManViewOthersOrder";
        const string Not_Allow_User_Of_Other_Department_View_Order = "NotAllowUserOfOtherDepartmentViewOrder";
        const string Not_Allow_User_View_Organization = "NotAllowUserViewOrganization";

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
            if(this.MemId !=0)
                ddlBusinessManId.SelectedValue = this.MemId.ToString();
            if(this.From != null)
                ctrlDatePickerFrom.SelectedDate = this.From;
            if(this.To != null)
                ctrlDatePickerTo.SelectedDate = this.To;
            if (this.OrderStatus != 0)
                cblOrderStatus.SelectedValue = ((int)this.OrderStatus).ToString();
            if(this.MemId != 0)
                btFind_Click(new object(), new EventArgs());
        }

        private void LoadDefaultData()
        {
            cblOrderStatus.Attributes.Add("onclick", "radioMe(event);");
            //Product drop down
            ddlProduct.Items.Clear();
            ddlProduct.Items.Add(new ListItem(string.Empty, "0"));
            List<ProductBO> products = this.ProductService.GetAllProucts();
            foreach (ProductBO p in products)
            {
                ddlProduct.Items.Add(new ListItem(p.Name, p.ProductId.ToString()));
            }
            //Shipping 
            ddlShipping.Items.Clear();
            ddlShipping.Items.Add(new ListItem(string.Empty, "0"));
            List<ShippingMethodBO> ships = this.OrderService.GetAllShippingMehod();
            foreach (ShippingMethodBO s in ships)
            {
                ddlShipping.Items.Add(new ListItem(s.Name, s.ShippingMethodId.ToString()));
            }
            //Deposit
            ddlDeposit.Items.Clear();
            ddlDeposit.Items.Add(new ListItem(string.Empty, "0"));
            List<DepositMethodBO> deposits = this.OrderService.GetAllDepositMethod();
            foreach (DepositMethodBO d in deposits)
            {
                ddlDeposit.Items.Add(new ListItem(d.Name, d.DepositTypeId.ToString()));
            }
            //Order Status
            ddlOrderDetailStatus.Items.Clear();
            ddlOrderDetailStatus.Items.Add(new ListItem(string.Empty, "0"));
            List<OrderStatusBO> status = this.OrderService.GetAllOrderStatus();
            foreach (OrderStatusBO s in status)
            {
                ddlOrderDetailStatus.Items.Add(new ListItem(s.Name, s.OrderStatusId.ToString()));
            }

            
            //Business man
            ddlBusinessManId.Items.Clear();
            ddlBusinessManId.Items.Add(new ListItem(string.Empty, "0"));
            List<MemberBO> businessMans = this.MemberService.GetBusinessMen(0);
            foreach (MemberBO m in businessMans)
            {
                ddlBusinessManId.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }
            //Designner man
            ddlDesingerId.Items.Clear();
            ddlDesingerId.Items.Add(new ListItem(string.Empty, "0"));
            List<MemberBO> designers = this.MemberService.GetDesigners(base.LoggedInOrganizationIds);
            foreach (MemberBO m in designers)
            {
                ddlDesingerId.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }

            //Company
            ddlCompany.Items.Clear();
            ddlCompany.Items.Add(new ListItem(string.Empty, "0"));
            List<OrganizationBO> organizations = this.OrderService.GetAllOrganizations();
            foreach (OrganizationBO og in organizations)
            {
                ddlCompany.Items.Add(new ListItem(og.Name, og.OrganizationId.ToString()));
            }

            //Check Roles and Department to bindata
            MemberBO mem = this.MemberService.GetMember(this.LoggedInUserId);
            if (this.SettingService.GetBoolSetting(Not_Allow_User_Of_Other_Department_View_Order))
            {
                if (mem.RoleName.ToLower() == Constant.USER_ROLE_NAME.ToLower())
                {
                    if (mem.Department.Code != Constant.PKD) Response.Redirect("Login.aspx");
                }
            }

            if (this.SettingService.GetBoolSetting(Not_Allow_Business_Man_View_Others_Order))
            {
                if (mem.RoleName.ToLower() == Constant.USER_ROLE_NAME.ToLower())
                {
                    ddlBusinessManId.SelectedValue = mem.UserId.ToString();
                    ddlBusinessManId.Enabled = false;
                }
            }

            if (this.SettingService.GetBoolSetting(Not_Allow_User_View_Organization))
            {
                if (mem.RoleName.ToLower() == Constant.USER_ROLE_NAME.ToLower())
                {
                    pnlCompany.Visible = false;
                }
            }


            // Suppervisor role
            if (mem.RoleName.ToLower() == Constant.SUPPERVISOR_ROLE_NAME.ToLower())
            {
                List<OrganizationBO> orgsOfMem = this.MemberService.GetOrganizationsByMemberId(mem.UserId);
                if (orgsOfMem.Count == 1)
                {
                    ddlCompany.SelectedValue = orgsOfMem[0].OrganizationId.ToString();
                    ddlCompany.Enabled = false;
                }
            }
        }

        protected void btFind_Click(object sender, EventArgs e)
        {
            DateTime? orderFrom = ctrlDatePickerFrom.SelectedDate;
            DateTime? orderTo = ctrlDatePickerTo.SelectedDate;

            int orderId = 0;
            int.TryParse(txtOrderCode.Text, out orderId);
            
            int custId = 0;
            int.TryParse(ctrlCustomerSelect.CustomerCode, out custId);
            
            int productId = 0;
            int.TryParse(ddlProduct.SelectedValue, out productId);
            
            int shipMethodId = 0;
            int.TryParse(ddlShipping.SelectedValue, out shipMethodId);
            
            int depositMethodId = 0;
            int.TryParse(ddlDeposit.SelectedValue, out depositMethodId);
            
            int orderDetailStatusId = 0;
            int.TryParse(ddlOrderDetailStatus.SelectedValue, out orderDetailStatusId);
            
            int busManId = 0;
            int.TryParse(ddlBusinessManId.SelectedValue,out busManId);
            
            int designerManId = 0;
            int.TryParse(ddlDesingerId.SelectedValue, out designerManId);

            int orderStatusValue = 0;
            int.TryParse(cblOrderStatus.SelectedValue, out orderStatusValue);

            OrderSearch orderSearchObj = new OrderSearch()
            {
                OrderFrom = orderFrom,
                OrderTo = orderTo,
                OrderId= orderId,
                CustId = custId,
                ProductId = productId,
                ShipMethodId = shipMethodId,
                DepositMethodId = depositMethodId,
                OrderDetailStatus = (OrderDetailStatusEnum)orderDetailStatusId,
                OrderStatus = (OrderStatusEnum)orderStatusValue,
                BusManId = busManId,
                DesignerManId = designerManId
            };

            List<OrderBO> orders = this.OrderService.GetOrders(orderSearchObj);
            grvOrders.DataSource = orders;
            grvOrders.DataBind();

            lbNumberOfOrders.Text = orders.Count.ToString();
            lbOrderTotal.Text = orders.Sum(o => o.OrderTotal).ToString("C0");

            lbNotCompletedNumberOfOrders.Text = orders.Where(o=>o.OrderStatus == OrderStatusEnum.NotCompleted).Count().ToString();
            lbNotCompletedOrderTotal.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.NotCompleted).Sum(o => o.OrderTotal).ToString("C0");

            lbCompletedNumberOfOrders.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.Completed).Count().ToString();
            lbCompletedOrderTotal.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.Completed).Sum(o => o.OrderTotal).ToString("C0");

            lbFailedNumberOfOrders.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.IsFailed).Count().ToString();
            lbFailedOrderTotal.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.IsFailed).Sum(o => o.OrderTotal).ToString("C0");

            List<ProductApprovedBO> failedProductsResult = new List<ProductApprovedBO>();
            foreach (OrderBO o in orders)
            {
                List<ProductApprovedBO> failedProducts = this.OrderService.GetFailedProductByOrderId(o.OrderId);
                foreach (ProductApprovedBO pa in failedProducts)
                {
                    failedProductsResult.Add(pa);       
                }
            }

            lbFailedOrderDetailTotal.Text = failedProductsResult.Sum(fd => fd.Total).ToString("C0");

            lbOverdueNumberOfOrders.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.Overdue).Count().ToString();
            lbOverdueOrderTotal.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.Overdue).Sum(o => o.OrderTotal).ToString("C0");
            var overdueOrders = orders.Where(o => o.OrderStatus == OrderStatusEnum.Overdue);
            decimal overdueOrderDetailTotal = 0;
            foreach (OrderBO o in overdueOrders)
            {
                foreach (OrderDetailBO od in o.OrderItems)
                {
                    if (od.OrderDetailStatus == OrderDetailStatusEnum.Overdue)
                    {
                        overdueOrderDetailTotal += od.Quantity * od.Price;
                    }
                }
            }
            lbOverdueOrderDetailTotal.Text = overdueOrderDetailTotal.ToString("C0");
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderAdd.aspx");
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvOrders.Rows)
            {
                CheckBox chk = row.Cells[0].Controls[1] as CheckBox;
                if (chk != null && chk.Checked)
                {
                    int orderId = int.Parse(row.Cells[1].Text);

                    OrderBO order = this.OrderService.GetOrderById(orderId);

                    if (order == null) return;
                    
                    try
                    {
                        this.OrderService.MarkOrderAsDeleted(orderId);

                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                    
                }
            }

            btFind_Click(new object(), new EventArgs());
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> orgsId = new List<int>();
            int companyId = 0;
            int.TryParse(ddlCompany.SelectedValue, out companyId);
            if (companyId == 0)
            {
                foreach (ListItem li in ddlCompany.Items)
                {
                    if (int.Parse(li.Value) != 0)
                        orgsId.Add(int.Parse(li.Value));
                }
            }
            else
            {
                orgsId.Add(int.Parse(ddlCompany.SelectedValue));
            }
            //Business man
            ddlBusinessManId.Items.Clear();
            ddlBusinessManId.Items.Add(new ListItem(string.Empty, "0"));
            List<MemberBO> businessMans = this.MemberService.GetBusinessMen(companyId);
            foreach (MemberBO m in businessMans)
            {
                ddlBusinessManId.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }
            //Designner man
            ddlDesingerId.Items.Clear();
            ddlDesingerId.Items.Add(new ListItem(string.Empty, "0"));
            List<MemberBO> designers = this.MemberService.GetDesigners(orgsId);
            foreach (MemberBO m in designers)
            {
                ddlDesingerId.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }
        }

        public int MemId
        {
            get
            {
                return Common.Utils.CommonHelper.QueryStringInt("MemId");
            }
        }

        public DateTime From
        {
            get
            {
                DateTime from;
                DateTime.TryParse(Common.Utils.CommonHelper.QueryString("From"), out from);
                return from;
            }
        }

        public DateTime To
        {
            get
            {
                DateTime to;
                DateTime.TryParse(Common.Utils.CommonHelper.QueryString("To"), out to);
                return to;
            }
        }

        public OrderStatusEnum OrderStatus
        {
            get
            {
                return (OrderStatusEnum)Common.Utils.CommonHelper.QueryStringInt("OrderStatus");
            }
        }
    }
}