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
    public partial class DesignRequestAdd : BaseUserControl
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
            
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    DesignRequestBO designRequest = ctrlDesignRequestInfo.SaveDesignRequestInfo();
                    Response.Redirect(string.Format("/DesignRequestEdit.aspx?OrderId={0}&OrderDetailId={1}&DesignRequestId={2}", OrderId, OrderDetailId, designRequest.DesignRequestId));
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
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