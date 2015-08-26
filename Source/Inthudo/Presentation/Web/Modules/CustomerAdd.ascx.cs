using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class CustomerAdd : BaseUserControl
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
                    CustomerBO cust = ctrlCustomerInfo.SaveInfo();
                    Response.Redirect(string.Format("/CustomerEdit.aspx?CustId={0}",cust.CustomerId));
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }
    }
}