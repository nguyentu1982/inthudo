using BusinessObjects;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Modules
{
    public partial class OrderDetailInfo : BaseUserControl
    {
        public bool ActionButtonIsDisplay { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.FillDropDowns();
                this.BindData();
            }
        }

        private void BindData()
        {
            
            OrderDetailBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);            

            if (orderDetail != null)
            {
                lbOrderDetailID.Text = orderDetail.OrderItemId.ToString();
                lbOrderId.Text = orderDetail.OrderId.ToString();       
                cboxProduct.SelectedValue = orderDetail.ProductId.ToString();
                cboxProduct.Text = orderDetail.ProductName;

                txtProductRequirement.Text = orderDetail.Specification;
                ctrltxtQuantity.Value = orderDetail.Quantity;
                ctrltxtPrice.Value = orderDetail.Price;
                
            }
            else
            {
                if (this.OrderId > 0)
                {
                    lbOrderId.Text = this.OrderId.ToString();
                }
                else
                {
                    panelOrderInfo.Visible = false;
                }

                panelOrderDetailId.Visible = false;
            }
        }

        private void FillDropDowns()
        {
            lbOrderId.Text = this.OrderId.ToString();           
            MemberBO mem = this.MemberService.GetMemberByOrder(this.OrderId);
            if (mem != null)
            {
                lbBusinessMan.Text = mem.FullName;
            }
            CustomerBO cust = this.CustomerService.GetCustomerByOrder(this.OrderId);
            if (cust != null)
            {
                lbCustomer.Text = string.Format("Tên: {0}, Địa chỉ: {1}, SĐT: {2}", cust.Name, cust.Address, cust.Telephone);
            }
            //Product dropdown
            List<ProductBO> products = this.ProductService.GetAllProucts();
            cboxProduct.Items.Clear();
            foreach (ProductBO p in products)
            {
                cboxProduct.Items.Add(new ListItem(p.Name, p.ProductId.ToString()));
            }            
        }

        public OrderDetailBO SaveInfo(int orderId)
        {
            if (cboxProduct.SelectedValue == string.Empty)
            {
                throw new Exception("Bạn phải nhập sản phẩm");
            }

            OrderDetailBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
            if (orderDetail != null)
            {  
                orderDetail.ProductId = int.Parse(cboxProduct.SelectedValue);
                orderDetail.Specification = txtProductRequirement.Text;
                orderDetail.Quantity = ctrltxtQuantity.Value;
                orderDetail.Price = ctrltxtPrice.Value;                
                this.OrderService.UpdateOrderDetail(orderDetail);
            }
            else
            {
                
                orderDetail = new OrderDetailBO() 
                { 
                     OrderId = orderId,
                     ProductId = int.Parse(cboxProduct.SelectedValue),
                     Specification = txtProductRequirement.Text,
                     Quantity = ctrltxtQuantity.Value,
                     Price = ctrltxtPrice.Value,
                     CreatedBy = this.UserId,
                     CreatedOn = DateTime.Now,
                     
                };                
               
                this.OrderService.InsertOrderDetail(orderDetail);
            }
            return orderDetail;
        }

        public int OrderId
        { 
            get
            {
                return CommonHelper.QueryStringInt("OrderId");
            }            
        }

        public int OrderDetailId
        {
            get
            {
                int orderDetailId;
                orderDetailId = CommonHelper.QueryStringInt("OrderDetailId");
                if (orderDetailId > 0) return orderDetailId;

                if (CommonHelper.QueryStringInt("AddNew") == 1) return 0;

                List<OrderDetailBO> orderDetails = this.OrderService.GetOrderDetailsByOrderId(this.OrderId);

                if (orderDetails != null)
                {
                    if (orderDetails.Count == 1)
                    {
                        return orderDetails[0].OrderItemId;
                    }
                }

                return orderDetailId;
            } 
        }

        public int UserId
        {
            get
            { 
                return int.Parse(Session["UserId"].ToString());
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            
        }
    }
}