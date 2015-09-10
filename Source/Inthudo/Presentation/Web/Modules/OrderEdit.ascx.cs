using BusinessObjects;
using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Web.Modules
{
    public partial class OrderEdit : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {           
            List<OrderItemlBO> orderDetails = this.OrderService.GetOrderDetailsByOrderId(this.OrderId);

            int numberOfOrderItemManufactureCompleted = 0;
            foreach(OrderItemlBO od in orderDetails)
            {
                if (od.OrderItemStatus == OrderItemStatusEnum.ManufactureCompleted)
                {
                    numberOfOrderItemManufactureCompleted++;
                }
            }

            if (numberOfOrderItemManufactureCompleted != orderDetails.Count)
            {
                pnlOrderCustomerApprove.Visible = false;
            }

            //Check whether other user can edit order
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (this.LoggedInUserId != order.BusinessManId)
            {
                List<WebControl> buttons = new List<WebControl>();
                buttons.Add(btSave);
                buttons.Add(btDelete);
                base.CheckNotAllowOtherUserEditOrder(buttons, order.BusinessManId);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    OrderBO order = Save();

                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected OrderBO Save()
        {
            OrderBO order = ctrlOrderInfo.SaveInfo();
            return order;
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.OrderService.MarkOrderAsDeleted(this.OrderId);
                Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script>window.close()</script>");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
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