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
    public partial class CustomerSearch : BaseUserControl
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
            CustomerSelect custSelect = this.Parent.Parent.Parent.Parent as CustomerSelect;
            int customerTypeId = custSelect.CustomerTypeId;
            btAddNew.Attributes.Add("onclick", string.Format("OpenWindow('CustomerAdd.aspx?{0}={1}')", Constant.Customer.QUERY_STRING_CUSTOMER_TYPE,customerTypeId));
            CustomerTypeBO custType = this.CustomerService.GetCustomerTypeById(customerTypeId);
            btAddNew.Value = string.Format("Thêm {0}", custType.Name);
        }

        protected void btFind_Click(object sender, EventArgs e)
        {
            CustomerSelect custSelect = this.Parent.Parent.Parent.Parent as CustomerSelect;
            int customerTypeId = custSelect.CustomerTypeId;
            string customerName = txtCustomerName.Text;
            string telephone = txtTelephone.Text;
            string email = txtEmail.Text;
            string companyName = txtCompanyName.Text;
            List<CustomerBO> customers = this.CustomerService.GetCustomers(customerName, telephone, email, companyName, customerTypeId);
            grvCustomers.DataSource = customers;
            grvCustomers.DataBind();
        }

        protected void grvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ChoseCustomer")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grvCustomers.Rows[index];
                var custId = row.Cells[0].Text;
                
                CustomerSelect ctrlCustomerSelect = this.Parent.Parent.Parent.Parent as CustomerSelect;                
                TextBox txtCustCode = ctrlCustomerSelect.FindControl("txtCustomerCode") as TextBox;
                txtCustCode.Text = custId;
                ctrlCustomerSelect.txtCustomerCode_TextChanged(new object(), new EventArgs());
                
            }
        }

        protected void grvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int custId = int.Parse(e.Row.Cells[0].Text);
                int customerTypeId = this.CustomerService.GetCustomerTypeId(Constant.Customer.KH);
                HyperLink hlEidtCustomer = e.Row.Cells[7].FindControl("hlEidtCustomer") as HyperLink;
                string url = string.Format("CustomerEdit.aspx?CustId={0}&{1}={2}", custId, Constant.Customer.QUERY_STRING_CUSTOMER_TYPE, customerTypeId);
                hlEidtCustomer.Attributes.Add("onclick", "OpenWindow('" + url + "')");
                hlEidtCustomer.Attributes.Add("class", "a-popup");
                hlEidtCustomer.Text = "Xem";
            }
        }
    }
}