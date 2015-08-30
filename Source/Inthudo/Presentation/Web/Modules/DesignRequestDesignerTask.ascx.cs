using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Common.Utils;
using Common;

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
            cbStartWork.Attributes.Add("onclick", "if(!confirmChangeDesignRequestStatus()) return false");
            cbCompleteWork.Attributes.Add("onclick", "if(!confirmChangeDesignRequestStatus()) return false");
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

                List<WebControl> controls = new List<WebControl>();
                controls.Add(cbStartWork);
                controls.Add(cbCompleteWork);
                int designerId = dr.CreatedBy;
                int.TryParse(dr.DesignerId.ToString(), out designerId);
                base.CheckNotAllowOtherUserEditOrder(controls, designerId);

                OrderDetailBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
                if (orderDetail.OrderDetailStatus >= OrderDetailStatusEnum.DesignCopmleted)
                {                    
                    List<WebControl> designRequestTaskControls = new List<WebControl>();
                    designRequestTaskControls.Add(cbStartWork);
                    designRequestTaskControls.Add(cbCompleteWork);
                    base.DisableDeleteAndEditButton(designRequestTaskControls);                    
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

        public int OrderDetailId
        {
            get
            {
                return CommonHelper.QueryStringInt("OrderDetailId");
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