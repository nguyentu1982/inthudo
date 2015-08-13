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

                decimal deposit = 0;
                if(order.Deposit !=null)
                    decimal.Parse(order.Deposit.ToString());
                ctrlDepositAmount.Value = deposit;
                ddlBusinessManId.SelectedValue = order.UserId.ToString();
                //Order Items
                List<OrderDetailBO> orderItems = this.OrderService.GetOrderItemsByOrderId(this.OrderId);
                grvOrderDetails.DataSource = orderItems;
                grvOrderDetails.DataBind();
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

                this.OrderService.UpdateOrderInfo(order);
                ctrlOrderDetailInfo.SaveInfo(order.OrderId);
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
                order = this.OrderService.InsertOrderInfo(order);
                //OrderStatusBO continueOrderStatus = this.OrderService.GetContinueOrderStatusByOrderId(order.OrderId);

                //OrderStatusMappingBO orderStatusMapping = new OrderStatusMappingBO()
                //{
                //    OrderId = order.OrderId,
                //    OrderStatusId = continueOrderStatus.OrderStatusId,
                //    StatusDate = DateTime.Now,
                //    IsFailed = false
                //};
                //this.OrderService.InsertOrderStatusMapping(orderStatusMapping);
                ctrlOrderDetailInfo.SaveInfo(order.OrderId);
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
    }
}