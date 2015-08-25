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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDefaultData();
            }
        }

        private void LoadDefaultData()
        {
            cblOrderStatus.Attributes.Add("onclick", "radioMe(event);");
            //Product drop down
            ddlProduct.Items.Clear();
            ddlProduct.Items.Add(new ListItem(string.Empty,"0"));
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

            string orderby = "UserId ASC";
            //Business man
            ddlBusinessManId.Items.Clear();
            ddlBusinessManId.Items.Add(new ListItem(string.Empty, "0"));
            List<MemberBO> businessMans = this.MemberService.GetBusinessMen();
            foreach (MemberBO m in businessMans)
            {
                ddlBusinessManId.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }
            //Designner man
            ddlDesingerId.Items.Clear();
            ddlDesingerId.Items.Add(new ListItem(string.Empty, "0"));
            List<MemberBO> designers = this.MemberService.GetDesigners();
            foreach (MemberBO m in designers)
            {
                ddlDesingerId.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }

            //Check Roles and Department to bindata
            MemberBO mem = this.MemberService.GetMember(this.LoggedInUserId);
            if (this.SettingService.GetBoolSetting(Not_Allow_User_Of_Other_Department_View_Order))
            {
                if (mem.RoleName == "User")
                {
                    if (mem.Department.Code != Constant.PKD) Response.Redirect("Login.aspx");
                }
            }

            if (this.SettingService.GetBoolSetting(Not_Allow_Business_Man_View_Others_Order))
            {
                if (mem.RoleName == "User")
                {
                    ddlBusinessManId.SelectedValue = mem.UserId.ToString();
                    ddlBusinessManId.Enabled = false;
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
    }
}