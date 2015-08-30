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
    public partial class ManufactureRequestAdd : BaseUserControl
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
                    
                    ManufactureRequestBO manu = ctrlManufactureRequestInfo.SaveInfo();
                    string manuRequestURL = string.Format("/ManufactureRequestEdit.aspx?OrderId={0}&OrderDetailId={1}&DesignRequestId={2}&ManufactureRequestId={3}", this.OrderId, this.OrderDetailId, this.DesignRequestId, manu.ManufactureRequestId);
                    Response.Redirect(manuRequestURL);
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
                    
            }
        }

        public int DesignRequestId
        {
            get
            {
                return CommonHelper.QueryStringInt("DesignRequestId");
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