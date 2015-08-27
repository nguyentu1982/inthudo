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
    public partial class ManufactureCustomerApprove : BaseUserControl
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
                bool isCustomerApproved = false;
                bool.TryParse(manu.CustomerApproved.ToString(), out isCustomerApproved);
                cbCustomerApprove.Checked = isCustomerApproved;
                panelApprovedDate.Visible = isCustomerApproved;
                ctrlDatePickerCustomerApproveDate.SelectedDate = manu.CustomerApprovedDate;
                txtNote.Text = manu.Note;
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
            if (manu != null)
            {
                manu.CustomerApproved = cbCustomerApprove.Checked;
                if (cbCustomerApprove.Checked)
                {
                    manu.CustomerApprovedDate = DateTime.Now;
                }
                else
                {
                    manu.CustomerApprovedDate = null;
                }
                manu.Note = txtNote.Text;

                this.OrderService.UpdateManufactureRequest(manu);
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