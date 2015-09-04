using BusinessObjects;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace Web.Modules
{
    public partial class CustomerEdit : BaseUserControl
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
            CustomerTypeBO custType = this.CustomerService.GetCustomerTypeById(this.CustomerTypeId);
            lbCustomerEditHeader.Text = string.Format("Sửa {0} ", custType.Name);
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    CustomerBO cust = ctrlCustomerInfo.SaveInfo();                    
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
                this.CustomerService.MarkCustomerAsDeleted(this.CustomerId);
                Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script>window.close()</script>");

            }
            catch (Exception ex)
            { 
                ProcessException(ex);
            }
        }

        public int CustomerId
        {
            get
            {
                return CommonHelper.QueryStringInt(Constant.QUERY_STRING_CUST_ID);
            }
        }

        public int CustomerTypeId
        {
            get
            {
                return Common.Utils.CommonHelper.QueryStringInt(Constant.Customer.QUERY_STRING_CUSTOMER_TYPE);
            }
        }
    }
}