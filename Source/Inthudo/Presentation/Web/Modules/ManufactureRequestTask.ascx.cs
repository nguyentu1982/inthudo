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
    public partial class ManufactureRequestTask : BaseUserControl
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
            cbStartWork.Attributes.Add("onclick", "if(!confirmChangeManufactureRequestStatus()) return false");
            cbCompleteWork.Attributes.Add("onclick", "if(!confirmChangeManufactureRequestStatus()) return false");
            ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
            if (manu != null)
            {
                if (manu.BeginDate.HasValue)
                {
                    cbStartWork.Checked = true;
                    ctrlDatePickerBeginDate.Visible = true;
                    ctrlDatePickerBeginDate.SelectedDate = manu.BeginDate;
                }

                if (manu.EndDate.HasValue)
                {
                    cbCompleteWork.Checked = true;
                    ctrlDatePickerEndDate.Visible = true;
                    ctrlDatePickerEndDate.SelectedDate = manu.EndDate;
                }


            }
        }

        protected void cbStartWork_CheckedChanged(object sender, EventArgs e)
        {
            ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
            if (manu != null)
            {
                if (cbStartWork.Checked)
                {
                    manu.BeginDate = DateTime.Now;
                }

                if (!cbStartWork.Checked)
                {
                    if (!manu.EndDate.HasValue)
                    {
                        manu.BeginDate = null;
                    }
                }

                this.OrderService.UpdateManufactureRequest(manu);

            } 
        }

        protected void cbCompleteWork_CheckedChanged(object sender, EventArgs e)
        {
            ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
            if (manu != null)
            {
                if (cbCompleteWork.Checked)
                {
                    if (manu.BeginDate.HasValue)
                    {
                        manu.EndDate = DateTime.Now;
                    }
                }

                if (!cbCompleteWork.Checked)
                {
                    manu.EndDate = null;
                }

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