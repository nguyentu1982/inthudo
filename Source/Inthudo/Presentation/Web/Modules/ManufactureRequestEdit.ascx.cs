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
            if (manu != null)
            {
                if (!manu.BeginDate.HasValue || !manu.EndDate.HasValue)
                {
                    pnlManufactureCustomerApprove.Visible = false;
                }
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
    }
}