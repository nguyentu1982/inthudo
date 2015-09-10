using Common.Utils;
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
            cblApprovedByCustomer.Attributes.Add("onclick", "radioMe(event);");

            DesignRequestBO designRequest = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (designRequest != null)
            {
                bool approvedByCustomer = false;
                if (designRequest.ApprovedByCustomer != null)
                {
                    bool.TryParse(designRequest.ApprovedByCustomer.ToString(), out approvedByCustomer);
                    cblApprovedByCustomer.SelectedValue = approvedByCustomer == true ? "1" : "0";
                }
                txtApprovedByCustomerNote.Text = designRequest.Note;

                List<WebControl> buttons = new List<WebControl>();
                buttons.Add(btSave);
                base.CheckNotAllowOtherUserEditOrder(buttons, designRequest.CreatedBy);

                if (this.SettingService.GetBoolSetting(Constant.Setting.Allow_Designer_Input_Approved_By_Customer_Info))
                {
                    if (designRequest.DesignerId == LoggedInUserId)
                    {
                        btSave.Visible = true;
                    }
                }

                OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
                if (orderDetail.OrderItemStatus >= OrderItemStatusEnum.DesignApprovedByCustomer)
                {                    
                    List<WebControl> designRequestTaskControls = new List<WebControl>();
                    designRequestTaskControls.Add(btSave);
                    base.DisableDeleteAndEditButton(designRequestTaskControls);                    
                }
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            DesignRequestBO designRequest = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (designRequest != null)
            {
                int approvedByCustomer = 0;
                if (cblApprovedByCustomer.SelectedValue != string.Empty)
                {
                    int.TryParse(cblApprovedByCustomer.SelectedValue, out approvedByCustomer);
                    designRequest.ApprovedByCustomer = approvedByCustomer == 1 ? true : false;
                    if (approvedByCustomer == 0)
                    {
                        designRequest.EndDate = null;
                    }
                }
                else
                {
                    designRequest.ApprovedByCustomer = null;
                }
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

        public int OrderDetailId
        {
            get
            {
                return CommonHelper.QueryStringInt("OrderDetailId");
            }
        }
    }
}