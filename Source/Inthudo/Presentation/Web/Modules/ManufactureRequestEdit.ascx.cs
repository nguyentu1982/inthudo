using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Common.Utils;
using Common;

namespace Web.Modules
{
    public partial class ManufactureRequestEdit : BaseUserControl
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
            ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (manu != null)
            {
                if (!manu.BeginDate.HasValue || !manu.EndDate.HasValue)
                {
                    pnlManufactureCustomerApprove.Enabled = false;
                }
            }

            //Check whether other user can edit order
            List<WebControl> buttons = new List<WebControl>();
            buttons.Add(btSave);
            buttons.Add(btDelete);
            base.CheckNotAllowOtherUserEditOrder(buttons, order.BusinessManId);

            OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
            if (orderDetail.OrderItemStatus >= OrderItemStatusEnum.ManufactureCompleted)
            {
                List<WebControl> manufactureRequestTaskControls = new List<WebControl>();
                manufactureRequestTaskControls.Add(btSave);
                manufactureRequestTaskControls.Add(btDelete);
                base.DisableDeleteAndEditButton(manufactureRequestTaskControls);
                
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    ManufactureRequestBO manu = ctrlManufactureRequestInfo.SaveInfo();                    
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
                this.OrderService.MarkManufactureRequestAsDeleted(this.ManufactureRequestId);
                Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script>window.close()</script>");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public int ManufactureRequestId
        {
            get
            {
                return CommonHelper.QueryStringInt("ManufactureRequestId");
            }
        }

        public int OrderDetailId
        {
            get
            {
                return CommonHelper.QueryStringInt("OrderDetailId");
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