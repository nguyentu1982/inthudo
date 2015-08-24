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
                ddlShippingMethod.SelectedValue = order.ShippingMethodId.ToString();
                ctrlCustomerSelect.CustomerCode = order.CustomerId.ToString();
                ctrlCustomerSelect.txtCustomerCode_TextChanged(new object(), new EventArgs());
                lbOrderStatus.Text = order.OrderStatusString;
                lbOrderTotal.Text = order.OrderTotal.ToString();                
                ctrlDatePickerEstimatedComplteDate.SelectedDate = order.ExpectedCompleteDate;
                txtDeliveryAddress.Text = order.DeliveryAddress;
                //deposit
                ddlDepositMethod.SelectedValue = order.DepositTypeId.ToString();
                decimal deposit = 0;               
                decimal.TryParse(order.Deposit.ToString(), out deposit);
                ctrlDepositAmount.Value = deposit;
                //Business man
                ddlBusinessManId.SelectedValue = order.UserId.ToString();
                //Order Items

                List<OrderDetailBO> orderItems = this.OrderService.GetOrderItemsByOrderId(this.OrderId);
                if (orderItems.Count >= 1)
                {
                    grvOrderDetails.DataSource = orderItems;
                    grvOrderDetails.DataBind();
                    ctrlOrderDetailInfo.Visible = false;
                }
                else
                {
                    grvOrderDetails.Visible = false;
                    btAddNewOrderDetail.Visible = false;
                }
            }
            else
            {
                btAddNewOrderDetail.Visible = false;
                panelOrderSumary.Visible = false;
            }
        }

        private void FillDropDowns()
        {
            //Order Status
             //ddlOrderStatus.Items.Clear();
             //ddlOrderStatus.Items.Add(new ListItem("","0"));
             //List<OrderStatusBO> status = this.OrderService.GetAllOrderStatus();
             //foreach (OrderStatusBO s in status)
             //{ 
             //   ddlOrderStatus.Items.Add(new ListItem(s.Name, s.OrderStatusId.ToString()));
             //}
            
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
            List<MemberBO> mems = this.MemberService.GetMembers(orderby);
            foreach (MemberBO m in mems)
            {
                ddlBusinessManId.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
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
                order.ExpectedCompleteDate = ctrlDatePickerEstimatedComplteDate.SelectedDate;
                order.DeliveryAddress = txtDeliveryAddress.Text;
                
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
                    CreatedDate = DateTime.Now,
                    ExpectedCompleteDate = ctrlDatePickerEstimatedComplteDate.SelectedDate,
                    DeliveryAddress = txtDeliveryAddress.Text,
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
                BindData();
            }
        }

        protected void grvOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Design Request
                int orderDetailId = int.Parse(e.Row.Cells[0].Text);
                DesignRequestBO ds = this.OrderService.GetDesignRequestByOrderDetailId(orderDetailId);
                HyperLink designRequestHyperLink = e.Row.Cells[7].FindControl("hlDesignRequest") as HyperLink;
                HyperLink manufactureRequestLink = e.Row.Cells[8].FindControl("hlManufactureRequest") as HyperLink;

                if (ds != null)
                {
                    string url=string.Format("/DesignRequestEdit.aspx?OrderId={0}&OrderDetailId={1}&DesignRequestId={2}",OrderId, orderDetailId, ds.DesignRequestId);
                    designRequestHyperLink.Attributes.Add("onclick", "OpenWindow('"+url+"')");
                    designRequestHyperLink.Attributes.Add("class", "a-popup");
                    designRequestHyperLink.Text = "Xem";

                    //Manufacture Request
                    ManufactureRequestBO manu = this.OrderService.GetManufactureRequestByDesignRequest(ds.DesignRequestId);
                    if (manu != null)
                    {
                        string ManuRequestURL = string.Format("/ManufactureRequestEdit.aspx?OrderId={0}&OrderDetailId={1}&DesignRequestId={2}&ManufactureRequestId={3}", this.OrderId, orderDetailId, ds.DesignRequestId, manu.ManufactureRequestId);
                        manufactureRequestLink.Attributes.Add("onclick", "OpenWindow('" + ManuRequestURL + "')");
                        manufactureRequestLink.Attributes.Add("class", "a-popup");
                        manufactureRequestLink.Text = "Xem";
                    }
                    else
                    {
                        string ManuRequestURL = string.Format("/ManufactureRequestAdd.aspx?OrderId={0}&OrderDetailId={1}&DesignRequestId={2}", this.OrderId, orderDetailId, ds.DesignRequestId);
                        manufactureRequestLink.Attributes.Add("onclick", "OpenWindow('" + ManuRequestURL + "')");
                        manufactureRequestLink.Attributes.Add("class", "a-popup");
                        manufactureRequestLink.Text = "Tạo";
                    }
                }
                else
                {
                    string url = string.Format("/DesignRequestAdd.aspx?OrderId={0}&OrderDetailId={1}", OrderId, orderDetailId);                   
                    designRequestHyperLink.Attributes.Add("onclick", "OpenWindow('" + url + "')");
                    designRequestHyperLink.Attributes.Add("class", "a-popup");
                    designRequestHyperLink.Text = "Tạo";
                }

                

            }
        }
    }
}