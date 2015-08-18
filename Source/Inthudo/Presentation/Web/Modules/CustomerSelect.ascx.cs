using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;


namespace Web.Modules
{
    public partial class CustomerSelect : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void txtCustomerCode_TextChanged(object sender, EventArgs e)
        {
            int custId = 0;
            int.TryParse(txtCustomerCode.Text, out custId);
            CustomerBO cust = this.CustomerService.GetCustomerById(custId);
            if (cust == null)
            {
                lbCustomerInfo.Text = string.Empty;
                return;
            }
            lbCustomerInfo.Text = string.Format("Tên: {0}, Địa chỉ: {1}, SĐT: {2}", cust.Name, cust.Address, cust.Telephone);
        }

        public string CustomerCode
        {
            get
            {
                return txtCustomerCode.Text;
            }
            set
            {
                txtCustomerCode.Text = value;
            }
        }

        public bool Enable
        {
            set
            {
                txtCustomerCode.Enabled = value;

            }
        }
    }
}