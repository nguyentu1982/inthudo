using BusinessObjects;
using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            
            OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);            

            if (orderDetail != null)
            {
                lbOrderDetailID.Text = orderDetail.OrderItemId.ToString();
                lbOrderId.Text = orderDetail.OrderId.ToString();
                txtProduct.Text = orderDetail.ProductName;
                txtProductRequirement.Text = orderDetail.Specification;
                ctrltxtQuantity.Value = orderDetail.Quantity;
                ctrltxtPrice.Value = orderDetail.Price;
                cbIsCustomerHasDesign.Checked = orderDetail.IsCustomerHasDesign;
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

                if (this.Reproduce == 1)
                {
                    ctrltxtPrice.Value = 0;
                    ctrltxtPrice.Enabled = false;
                }
            }
        }

        private void FillDropDowns()
        {
            cblPrintingType.Attributes.Add("onclick", "radioMe(event);");            

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
            //Printing Type
            List<PrintingTypeBO> printingTypes = this.OrderService.GetAllPrintingType();
            cblPrintingType.Items.Clear();
            foreach (PrintingTypeBO pt in printingTypes)
            {
                cblPrintingType.Items.Add(new ListItem(pt.Name, pt.Id.ToString()));
            }
            string defaultPrintingTypeCode = this.SettingService.GetStringSetting(Constant.Setting.Default_PrintingType_Code);
            cblPrintingType.SelectedValue = this.OrderService.GetPrintTypeByCode(defaultPrintingTypeCode).Id.ToString();
        }

        public OrderItemlBO SaveInfo(int orderId)
        {
            if (txtProduct.Text == string.Empty)
            {
                throw new Exception("Bạn phải nhập sản phẩm");                
            }

            //Add product if not exist
            int productId = 0;
            ProductBO product = this.ProductService.GetProductByName(txtProduct.Text);

            if (product == null)
            {
                productId = this.ProductService.InsertProduct(txtProduct.Text);
            }
            else
            {
                productId = product.ProductId;
            }

            OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
            if (orderDetail != null)
            {  
                orderDetail.ProductId = productId;
                orderDetail.Specification = txtProductRequirement.Text;
                orderDetail.Quantity = ctrltxtQuantity.Value;
                orderDetail.Price = ctrltxtPrice.Value;
                orderDetail.LastEditedOn = DateTime.Now;
                orderDetail.LastEditedBy = this.LoggedInUserId;
                orderDetail.IsCustomerHasDesign = cbIsCustomerHasDesign.Checked;
                orderDetail.PrintingTypeId = int.Parse(cblPrintingType.SelectedValue);
              
                this.OrderService.UpdateOrderDetail(orderDetail);
            }
            else
            {
                
                orderDetail = new OrderItemlBO() 
                { 
                     OrderId = orderId,
                     ProductId = productId,
                     Specification = txtProductRequirement.Text,
                     Quantity = ctrltxtQuantity.Value,
                     Price = ctrltxtPrice.Value,
                     CreatedBy = this.UserId,
                     CreatedOn = DateTime.Now,
                     IsCustomerHasDesign = cbIsCustomerHasDesign.Checked,
                     PrintingTypeId = int.Parse(cblPrintingType.SelectedValue)
                };

                orderDetail.OrderItemId = this.OrderService.InsertOrderDetail(orderDetail);
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

        public int Reproduce
        {
            get
            {
                return CommonHelper.QueryStringInt("Reproduce");
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

                List<OrderItemlBO> orderDetails = this.OrderService.GetOrderDetailsByOrderId(this.OrderId);

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