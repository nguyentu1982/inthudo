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
    public partial class DesignRequestDesignerTask : BaseUserControl
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
            cbStartWork.Attributes.Add("onclick", "confirmChangeDesignRequestStatus()");
            cbCompleteWork.Attributes.Add("onclick", "confirmChangeDesignRequestStatus()");
            DesignRequestBO dr = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (dr != null)
            {
                if (dr.BeginDate.HasValue)
                {
                    cbStartWork.Checked = true;
                    ctrlDatePickerBeginDate.Visible = true;
                    ctrlDatePickerBeginDate.SelectedDate = dr.BeginDate;
                }

                if (dr.EndDate.HasValue)
                {
                    cbCompleteWork.Checked = true;
                    ctrlDatePickerEndDate.Visible = true;
                    ctrlDatePickerEndDate.SelectedDate = dr.EndDate;
                }
            }            
        }

        public int DesignRequestId
        {
            get
            {
                return CommonHelper.QueryStringInt("DesignRequestId");
            }
        }

        protected void cbStartWork_CheckedChanged(object sender, EventArgs e)
        {
            DesignRequestBO dr = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (dr != null)
            {
                if (cbStartWork.Checked)
                {
                    dr.BeginDate = DateTime.Now;                    
                }

                if (!cbStartWork.Checked)
                {
                    if (!dr.EndDate.HasValue)
                    {
                        dr.BeginDate = null;
                    }
                }

                this.OrderService.UpdateDesignRequest(dr);
                
            } 
        }

        protected void cbCompleteWork_CheckedChanged(object sender, EventArgs e)
        {
            DesignRequestBO dr = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (dr != null)
            {
                if (cbCompleteWork.Checked)
                {
                    if (dr.BeginDate.HasValue)
                    {
                        dr.EndDate = DateTime.Now;
                    }
                }

                if (!cbCompleteWork.Checked)
                {
                    dr.EndDate = null;
                }

                this.OrderService.UpdateDesignRequest(dr);
                
            } 
        }
    }
}