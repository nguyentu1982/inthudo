using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Common.Utils;
using System.Transactions;
using Common;
using System.Globalization;

namespace Web.Modules
{
    public partial class OrderInfo : BaseUserControl
    {
        const string Not_Allow_Select_Business_Man_When_Create_Order = "NotAllowSelectBusinessManWhenCreateOrder";
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
                ctrlCustomerSelect.CustomerId = order.CustomerId.ToString();
                ctrlCustomerSelect.txtCustomerCode_TextChanged(new object(), new EventArgs());
                lbOrderStatus.Text = order.OrderStatusString;
                lbOrderTotal.Text = order.OrderTotal.ToString("C0");                
                ctrlDatePickerEstimatedComplteDate.SelectedDate = order.ExpectedCompleteDate;
                txtDeliveryAddress.Text = order.DeliveryAddress;
                decimal vat = 0;
                decimal.TryParse(order.VAT.ToString(), out vat);
                ctrlDecimalTextBoxVAT.Value = vat;
                //deposit
                ddlDepositMethod.SelectedValue = order.DepositTypeId.ToString();
                decimal deposit = 0;               
                decimal.TryParse(order.Deposit.ToString(), out deposit);
                ctrlDepositAmount.Value = deposit;
                lbOrderTotalIncludeVAT.Text = (order.OrderTotal + vat).ToString("C0");
                lbRemaining.Text = (order.OrderTotal + vat - deposit).ToString("C0");
                //Business man
                ddlBusinessManId.SelectedValue = order.BusinessManId.ToString();
                //Order Items

                List<OrderItemlBO> orderItems = this.OrderService.GetOrderDetailsByOrderId(this.OrderId);
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

                if (order.OrderStatus == OrderStatusEnum.IsFailed)
                {
                    panelOrderDetailAddButtonReProduce.Visible = true;
                }

                List<ProductApprovedBO> approvedProducts = this.OrderService.GetApprovedProductByOrderId(this.OrderId);
                if (approvedProducts.Count > 0)
                {
                    panelProductApprovedSummary.Visible = true;
                    grvApprovedProducts.DataSource = approvedProducts;
                    grvApprovedProducts.DataBind();

                    lbDepositAmount.Text = ctrlDepositAmount.Value.ToString("C0");
                    lbRemainAmount.Text = (total - ctrlDepositAmount.Value).ToString("C0");
                }
                //Check whether other user can edit order
                if (this.LoggedInUserId != order.BusinessManId)
                {
                    List<WebControl> buttons = new List<WebControl>();
                    buttons.Add(btAddNewOrderDetail);
                    buttons.Add(btAddNewOrderDetailReproduce);
                    base.CheckNotAllowOtherUserEditOrder(buttons, order.BusinessManId);
                }
            }
            else
            {
                btAddNewOrderDetail.Visible = false;
                panelOrderSumary.Visible = false;
                ctrlDatePicker.SelectedDate = DateTime.Now;
            }
        }

        private void FillDropDowns()
        {
            //Customer          
            
            //Deposit Method
            ddlDepositMethod.Items.Clear();
            List<DepositMethodBO> methods = this.OrderService.GetAllDepositMethod();
            foreach(DepositMethodBO me in methods)
            {
                ddlDepositMethod.Items.Add(new ListItem(me.Name, me.DepositTypeId.ToString()));
            }
            //Business Man
            ddlBusinessManId.Items.Clear();

            List<MemberBO> mems = this.MemberService.GetBusinessMen(this.LoggedInOrganizationIds);
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

            //Check roles an setting
            MemberBO mem = this.MemberService.GetMember(this.LoggedInUserId);

            if (this.SettingService.GetBoolSetting(Not_Allow_Select_Business_Man_When_Create_Order))
            {
                if (mem.RoleName.ToLower() == Constant.USER_ROLE_NAME.ToLower() || mem.RoleName.ToLower() == Constant.SUPPERVISOR_ROLE_NAME.ToLower())
                {
                    ddlBusinessManId.SelectedValue = mem.UserId.ToString();
                    ddlBusinessManId.Enabled = false;
                }
            }
        }

        

        public OrderBO SaveInfo()
        {
            if (ctrlCustomerSelect.CustomerId == string.Empty)
            {
                throw new Exception("Bạn hãy nhập mã số khách hàng!");
            }

            int custId = 0;
            int.TryParse(ctrlCustomerSelect.CustomerId, out custId);
            if (this.CustomerService.GetCustomerById(custId) == null)
            {
                throw new Exception("Mã số khách hàng không đúng!");
            }

            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (order != null)
            {
                order.OrderDate = ctrlDatePicker.SelectedDate;
                order.Deposit = ctrlDepositAmount.Value;
                order.DepositTypeId = int.Parse(ddlDepositMethod.SelectedValue);
                order.ShippingMethodId = int.Parse(ddlShippingMethod.SelectedValue);
                order.CustomerId = int.Parse(ctrlCustomerSelect.CustomerId);
                order.BusinessManId = int.Parse(ddlBusinessManId.SelectedValue);
                order.LastEditedBy = LoggedInUserId;
                order.LastEditedDate = DateTime.Now;
                order.ExpectedCompleteDate = ctrlDatePickerEstimatedComplteDate.SelectedDate;
                order.DeliveryAddress = txtDeliveryAddress.Text;
                order.VAT = ctrlDecimalTextBoxVAT.Value;
                order.Note = txtNote.Text;
                
                using(TransactionScope scope = new TransactionScope())
                {                    
                    this.OrderService.UpdateOrderInfo(order);
                    if (ctrlOrderDetailInfo.Visible)
                    {
                        ctrlOrderDetailInfo.SaveInfo(order.OrderId);
                    }
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
                    CustomerId = int.Parse(ctrlCustomerSelect.CustomerId),
                    BusinessManId = int.Parse(ddlBusinessManId.SelectedValue),
                    CreatedBy = LoggedInUserId,
                    CreatedDate = DateTime.Now,
                    ExpectedCompleteDate = ctrlDatePickerEstimatedComplteDate.SelectedDate,
                    DeliveryAddress = txtDeliveryAddress.Text,
                    VAT = ctrlDecimalTextBoxVAT.Value,
                    Note = txtNote.Text
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
                OrderBO order = this.OrderService.GetOrderById(this.OrderId);
                OrderItemlBO od = this.OrderService.GetOrderDetailById(orderDetailId);
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
                        if (ds.ApprovedByCustomer==true)
                        {
                            string ManuRequestURL = string.Format("/ManufactureRequestAdd.aspx?OrderId={0}&OrderDetailId={1}&DesignRequestId={2}", this.OrderId, orderDetailId, ds.DesignRequestId);
                            manufactureRequestLink.Attributes.Add("onclick", "OpenWindow('" + ManuRequestURL + "')");
                            manufactureRequestLink.Attributes.Add("class", "a-popup");
                            manufactureRequestLink.Text = "Tạo";
                            List<WebControl> buttons = new List<WebControl>();
                            buttons.Add(manufactureRequestLink);
                            CheckNotAllowOtherUserEditOrder(buttons, order.BusinessManId);
                        }
                    }
                }
                else
                {
                    string url = string.Format("/DesignRequestAdd.aspx?OrderId={0}&OrderDetailId={1}", OrderId, orderDetailId);                   
                    designRequestHyperLink.Attributes.Add("onclick", "OpenWindow('" + url + "')");
                    designRequestHyperLink.Attributes.Add("class", "a-popup");
                    designRequestHyperLink.Text = "Tạo";
                    List<WebControl> buttons = new List<WebControl>();
                    buttons.Add(designRequestHyperLink);
                    CheckNotAllowOtherUserEditOrder(buttons, order.BusinessManId);
                }


                //Delete button disable if not allow other user edit
                Button deleteButton = e.Row.Cells[6].FindControl("btDeleteOrderDetail") as Button;
                //Check whether other user can edit order
                List<WebControl> buts = new List<WebControl>();
                buts.Add(deleteButton);
                CheckNotAllowOtherUserEditOrder(buts, order.BusinessManId);

                if (od.OrderItemStatus >= OrderItemStatusEnum.Designing)
                {
                    if (LoggedInMember.RoleName.ToLower() != Constant.ADMIN_ROLE_NAME.ToLower())
                    {
                        List<WebControl> controls = new List<WebControl>();
                        controls.Add(deleteButton);
                        this.DisableDeleteAndEditButton(controls);
                    }
                }
            }
        }

        protected void ctrlDecimalTextBoxVAT_TextChanged(object sender, EventArgs e)
        {
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (order != null)
            {
                decimal orderTotal = order.OrderTotal;
                decimal vat = ctrlDecimalTextBoxVAT.Value;
                lbOrderTotalIncludeVAT.Text = (orderTotal + vat).ToString("C0");

                ctrlDepositAmount_TextChanged(new object(), new EventArgs());
            }
        }

        protected void ctrlDepositAmount_TextChanged(object sender, EventArgs e)
        {
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (order != null)
            {
                decimal orderTotal = order.OrderTotal;
                decimal vat = ctrlDecimalTextBoxVAT.Value;
                decimal orderTotalIncludedVat = orderTotal + vat;
                decimal deposit = ctrlDepositAmount.Value;
                lbRemaining.Text = (orderTotalIncludedVat - deposit).ToString("C0");


            }
        }

        decimal total = 0;
        protected void grvApprovedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string subtotal = ((Label)e.Row.FindControl("lbSubTotal")).Text;
                total += Common.Utils.CommonHelper.Parse(subtotal);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label totalFooter = (Label)e.Row.FindControl("lbTotalFooter");
                totalFooter.Text = string.Format("{0:C0}", total);
            }
        }
    }
}