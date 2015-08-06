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
                txtOrderDate.Text = order.OrderDate.ToString();
                ddlOrderStatus.SelectedValue = order.ShippingMethodId.ToString();
            }
            else
            { 
                
            }
        }

        private void FillDropDowns()
        {
            //Order Status

            //Deposit Method

            //Business Man

            //Designer
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