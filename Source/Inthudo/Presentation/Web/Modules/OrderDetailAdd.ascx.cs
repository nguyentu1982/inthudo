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
    public partial class OrderDetailAdd : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    OrderDetailBO od = ctrlOrderDetailInfo.SaveInfo(this.OrderId);
                    Response.Redirect(string.Format("/OrderDetailEdit.aspx?OrderDetailId={0}",od.OrderItemId));
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
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