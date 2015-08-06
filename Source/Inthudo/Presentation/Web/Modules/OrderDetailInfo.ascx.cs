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
                hdfOrderDetailId.Value = orderDetail.OrderItemId.ToString();
                lbOrderId.Text = orderDetail.Order.OrderId.ToString();
                ddlProduct.SelectedValue = orderDetail.ProductId.ToString();
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
            }
        }

        private void FillDropDowns()
        {
            //Product dropdown
            List<ProductBO> products = this.ProductService.GetAllProucts();
            ddlProduct.Items.Clear();
            foreach (ProductBO p in products)
            { 
                ddlProduct.Items.Add(new ListItem(p.Name, p.ProductId.ToString()));
            }

        }

        public OrderDetailBO SaveInfo(int orderId)
        {
            OrderDetailBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
            if (orderDetail != null)
            {
                orderDetail.ProductId = int.Parse(ddlProduct.SelectedValue);
                orderDetail.Specification = txtProductRequirement.Text;
                orderDetail.Quantity = ctrltxtQuantity.Value;
                orderDetail.Price = ctrltxtPrice.Value;
                this.OrderService.UpdateOrderDetail(orderDetail);
            }
            else
            {
                orderDetail = new OrderDetailBO() 
                { 
                     OrderId = string.IsNullOrEmpty(lbOrderId.Text) == true ? orderId : int.Parse(lbOrderId.Text),
                     ProductId = int.Parse(ddlProduct.SelectedValue),
                     Specification = txtProductRequirement.Text,
                     Quantity = ctrltxtQuantity.Value,
                     Price = ctrltxtPrice.Value,
                     CreatedBy = this.UserId,
                     CreatedOn = DateTime.Now
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
                return CommonHelper.QueryStringInt("OrderDetailId");
            } 
        }

        public int UserId
        {
            get
            { 
                return int.Parse(Session["UserId"].ToString());
            }
        }
    }
}