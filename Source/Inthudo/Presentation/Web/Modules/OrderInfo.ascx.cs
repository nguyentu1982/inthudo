using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Common.Utils;
using System.Transactions;

namespace Web.Modules
{
    public partial class OrderInfo : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillDropDowns();
                BindData();
            }
        }

        private void BindData()
        {
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (order != null)
            {
                lbOrderId.Text = order.OrderId.ToString();
                ctrlDatePicker.SelectedDate = order.OrderDate;
                ddlOrderStatus.SelectedValue = order.ShippingMethodId.ToString();
                ctrlCustomerSelect.CustomerCode = order.CustomerId.ToString();
                ctrlCustomerSelect.txtCustomerCode_TextChanged(new object(), new EventArgs());
                //deposit
                ddlDepositMethod.SelectedValue = order.DepositTypeId.ToString();
                decimal deposit = 0;               
                decimal.TryParse(order.Deposit.ToString(), out deposit);
                ctrlDepositAmount.Value = deposit;
                //Business man
                ddlBusinessManId.SelectedValue = order.UserId.ToString();
                //Order Items
                List<OrderDetailBO> orderItems = this.OrderService.GetOrderItemsByOrderId(this.OrderId);
                if (orderItems.Count > 1)
                {
                    grvOrderDetails.DataSource = orderItems;
                    grvOrderDetails.DataBind();
                    ctrlOrderDetailInfo.Visible = false;
                }
                else
                {
                    grvOrderDetails.Visible = false;
                }
            }
            else
            { 
                
            }
        }

        private void FillDropDowns()
        {
            //Order Status
             ddlOrderStatus.Items.Clear();
             ddlOrderStatus.Items.Add(new ListItem("","0"));
             List<OrderStatusBO> status = this.OrderService.GetAllOrderStatus();
             foreach (OrderStatusBO s in status)
             { 
                ddlOrderStatus.Items.Add(new ListItem(s.Name, s.OrderStatusId.ToString()));
             }
            
            //Deposit Method
            ddlDepositMethod.Items.Clear();
            List<DepositMethodBO> methods = this.OrderService.GetAllDepositMethod();
            foreach(DepositMethodBO me in methods)
            {
                ddlDepositMethod.Items.Add(new ListItem(me.Name, me.DepositTypeId.ToString()));
            }
            //Business Man
            ddlBusinessManId.Items.Clear();
            string orderby = "UserId ASC";
            List<Member> mems = this.MemberService.GetMembers(orderby);
            foreach (Member m in mems)
            {
                ddlBusinessManId.Items.Add(new ListItem(m.UserName, m.UserId.ToString()));
            }
            //Shipping Method
            ddlShippingMethod.Items.Clear();
            List<ShippingMethodBO> ships = this.OrderService.GetAllShippingMehod();
            foreach (ShippingMethodBO sh in ships)
            {
                ddlShippingMethod.Items.Add(new ListItem(sh.Name, sh.ShippingMethodId.ToString()));
            }
        }

        

        public OrderBO SaveInfo()
        {
            if (ctrlCustomerSelect.CustomerCode == string.Empty)
            {
                throw new Exception("Bạn hãy nhập mã số khách hàng!");
            }
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (order != null)
            {
                order.OrderDate = ctrlDatePicker.SelectedDate;
                order.Deposit = ctrlDepositAmount.Value;
                order.DepositTypeId = int.Parse(ddlDepositMethod.SelectedValue);
                order.ShippingMethodId = int.Parse(ddlShippingMethod.SelectedValue);
                order.CustomerId = int.Parse(ctrlCustomerSelect.CustomerCode);
                order.UserId = int.Parse(ddlBusinessManId.SelectedValue);
                order.LastEditedBy = LoggedInUserId;
                order.LastEditedDate = DateTime.Now;
                
                using(TransactionScope scope = new TransactionScope())
                {                    
                    this.OrderService.UpdateOrderInfo(order);
                    ctrlOrderDetailInfo.SaveInfo(order.OrderId);
                    scope.Complete();                    
                }                  
            }
            else
            {
                
                order = new OrderBO()
                {
                    OrderDate = ctrlDatePicker.SelectedDate,
                    Deposit = ctrlDepositAmount.Value,
                    DepositTypeId = int.Parse(ddlDepositMethod.SelectedValue),
                    ShippingMethodId = int.Parse(ddlShippingMethod.SelectedValue),
                    CustomerId = int.Parse(ctrlCustomerSelect.CustomerCode),
                    UserId = int.Parse(ddlBusinessManId.SelectedValue),
                    CreatedBy = LoggedInUserId,
                    CreatedDate = DateTime.Now
                };

                using (TransactionScope scope = new TransactionScope())
                {                    
                    order = this.OrderService.InsertOrderInfo(order);
                    ctrlOrderDetailInfo.SaveInfo(order.OrderId);
                    scope.Complete();                    
                }
            }
            return order;
        }

        public int LoggedInUserId
        {
            get
            {
                return int.Parse(Session["UserId"].ToString());
            }
        }

        public int OrderId
        {
            get
            {
                return CommonHelper.QueryStringInt("OrderId");
            }
        }

        protected void grvOrderDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteOrderDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grvOrderDetails.Rows[index];
                int orderDetailId = int.Parse(row.Cells[0].Text);

                this.OrderService.MarkOrderDetailAsDeleted(orderDetailId);
            }
        }
    }
}