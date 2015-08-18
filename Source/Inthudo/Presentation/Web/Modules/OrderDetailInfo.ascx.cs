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
                hdfOrderDetailId.Value = orderDetail.OrderItemId.ToString();
                lbOrderId.Text = orderDetail.OrderId.ToString();
                if (orderDetail.DesignerId != null)
                    ddlDesigner.SelectedValue = orderDetail.DesignerId.ToString();
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
            }
        }

        private void FillDropDowns()
        {
            btSave.Visible = ActionButtonIsDisplay;
            btCancel.Visible = ActionButtonIsDisplay;
            btDelete.Visible = ActionButtonIsDisplay;
            //Product dropdown
            List<ProductBO> products = this.ProductService.GetAllProucts();
            cboxProduct.Items.Clear();
            foreach (ProductBO p in products)
            {
                cboxProduct.Items.Add(new ListItem(p.Name, p.ProductId.ToString()));
            }

            //Designer drop down
            string orderBy = "UserId ASC";
            List<MemberBO> designers = this.MemberService.GetMembers(orderBy);
            ddlDesigner.Items.Clear();
            ddlDesigner.Items.Add(new ListItem("", "0"));
            foreach (MemberBO m in designers)
            { 
                ddlDesigner.Items.Add(new ListItem(m.UserName,m.UserId.ToString()));
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
                int designerId = int.Parse(ddlDesigner.SelectedValue);
                if (designerId == 0)
                { 
                    orderDetail.DesignerId = null;
                }
                else{
                    orderDetail.DesignerId = designerId;
                }               
                
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
                     OrderId = string.IsNullOrEmpty(lbOrderId.Text) == true ? orderId : int.Parse(lbOrderId.Text),
                     ProductId = int.Parse(cboxProduct.SelectedValue),
                     Specification = txtProductRequirement.Text,
                     Quantity = ctrltxtQuantity.Value,
                     Price = ctrltxtPrice.Value,
                     CreatedBy = this.UserId,
                     CreatedOn = DateTime.Now,
                     
                };

                int designerId = int.Parse(ddlDesigner.SelectedValue);
                if (designerId == 0)
                {
                    orderDetail.DesignerId = null;
                }
                else
                {
                    orderDetail.DesignerId = designerId;
                }
               
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
            if (Page.IsValid)
            {
                try
                {
                    SaveInfo(this.OrderId);
                    
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int orderDetailId = 0;
                int.TryParse(hdfOrderDetailId.Value,out orderDetailId);
                this.OrderService.MarkOrderDetailAsDeleted(orderDetailId);
                Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script>window.close()</script>");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}