using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class DesignRequestCustomerApprove : BaseUserControl
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
            DesignRequestBO designRequest = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (designRequest != null)
            {
                bool approvedByCustomer = false;
                bool.TryParse(designRequest.ApprovedByCustomer.ToString(), out approvedByCustomer);
                cbApprovedByCustomer.Checked = approvedByCustomer;
                txtApprovedByCustomerNote.Text = designRequest.Note;
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            DesignRequestBO designRequest = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (designRequest != null)
            {
                designRequest.ApprovedByCustomer = cbApprovedByCustomer.Checked;
                designRequest.Note = txtApprovedByCustomerNote.Text;
                designRequest.ApprovedDate = DateTime.Now;

                this.OrderService.UpdateDesignRequest(designRequest);
            }
        }

        public int DesignRequestId
        {
            get
            {
                return CommonHelper.QueryStringInt("DesignRequestId");
            }
        }
    }
}