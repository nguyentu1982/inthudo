using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class OrderCustomerApprove : BaseUserControl
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
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (order != null)
            {
                bool approvedByCustomer = false;
                bool.TryParse(order.ApprovedByCustomer.ToString(), out approvedByCustomer);
                cbOrderApproved.Checked = approvedByCustomer;
                txtNote.Text = order.Note;
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (order != null)
            {
                order.ApprovedByCustomer = cbOrderApproved.Checked;
                order.ApprovedDate = DateTime.Now;
                order.Note = txtNote.Text;

                this.OrderService.UpdateOrderInfo(order);
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