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
    public partial class CustomerAdd : BaseUserControl
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
            lbCustomerAddHeader.Text = string.Format("Thêm {0} mới", custType.Name);
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    CustomerBO cust = ctrlCustomerInfo.SaveInfo();
                    Response.Redirect(string.Format("/CustomerEdit.aspx?CustId={0}",cust.CustomerId));
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        public int CustomerTypeId {
            get
            {
                return Common.Utils.CommonHelper.QueryStringInt(Constant.Customer.QUERY_STRING_CUSTOMER_TYPE);
            }
        }
    }
}