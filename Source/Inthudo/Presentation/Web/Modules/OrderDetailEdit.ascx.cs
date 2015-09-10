using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Common;

namespace Web.Modules
{
    public partial class OrderDetailEdit : BaseUserControl
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
            //Check whether other user can edit order
            OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
            
            List<WebControl> buttons = new List<WebControl>();
            buttons.Add(btSave);
            buttons.Add(btDelete);
            this.CheckNotAllowOtherUserEditOrder(buttons, orderDetail.CreatedBy);

            if (orderDetail.OrderItemStatus >= OrderItemStatusEnum.Designing)
            {                
                List<WebControl> controls = new List<WebControl>();
                controls.Add(btSave);
                controls.Add(btDelete);
                this.DisableDeleteAndEditButton(controls);               
            }
        }

        

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    ctrlOrderDetailInfo.SaveInfo(this.OrderId);

                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {                
                this.OrderService.MarkOrderDetailAsDeleted(this.OrderDetailId);
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

        public int OrderDetailId
        {
            get
            {
                return CommonHelper.QueryStringInt("OrderDetailId");
            }
        }
    }
}