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
        public int CustomerTypeId
        {
            get
            {
                return this.CustomerService.GetCustomerTypeId(this.CustomerTypeCode);
            }
        }

        public string CustomerTypeCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Button findButton = new Button();
            findButton.ID=string.Format("btFind{0}",this.UniqueID);
            findButton.Text = "Tìm";
            var panelSearchId = panelCustomerSearch.ClientID;
            findButton.Attributes.Add("onclick", "displayOrHidePanelSearch(" + panelSearchId +"); return false");
            panelFind.Controls.Add(findButton);
            
            var displayOrHidePanelSearch = "<script type=\"text/javascript\">" +"\n"+
                        "function displayOrHidePanelSearch(panelSearchId){" + "\n"+
                        "var panelId = panelSearchId" +"\n"+
                        "var displayStatus = $(panelId).css(\"display\")" + "\n" +
                        "if (displayStatus == \"none\") {"+ "\n"+
                            "  $(panelId).css(\"display\", \"block\");" + "\n" +
                        "}"+ "\n"+
                        "else {"+ "\n" +
                            "  $(panelId).css(\"display\", \"none\");" + "\n" +
                        "}"+ "\n"+
                        
                    "}"+"\n"+
                    "</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "displayOrHidePanelSearch", displayOrHidePanelSearch);
            if (!Page.IsPostBack)
            { 
                CustomerTypeBO custType = this.CustomerService.GetCustomerTypeById(this.CustomerTypeId);
                lbCustomerIdHeader.Text = string.Format("Mã {0}", custType.Name);  
                
            }
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

        public string CustomerId
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