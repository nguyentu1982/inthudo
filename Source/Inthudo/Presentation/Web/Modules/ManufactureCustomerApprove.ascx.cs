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
            OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
            if (manu != null)
            {
                bool isCustomerApproved = false;
                bool.TryParse(manu.CustomerApproved.ToString(), out isCustomerApproved);
                cbCustomerApprove.Checked = isCustomerApproved;
                panelApprovedDate.Visible = isCustomerApproved;

                ctrlDatePickerCustomerApproveDate.SelectedDate = manu.CustomerApprovedDate;
                txtNote.Text = manu.Note;

                bool isFailed = false;
                bool.TryParse(manu.IsFailed.ToString(), out isFailed);
                cbCustomerRefuse.Checked = isFailed;

                if (manu.IsFailed == true)
                {
                    panelApprovedDate.Visible = false;
                    panelApproveDetail.Visible = false;                    
                }

                if (manu.CustomerApproved == true)
                {
                    int approvedQuantity = 0;
                    int.TryParse(manu.CustomerApprovedQuantity.ToString(), out approvedQuantity);
                    ctrlNumericTextBoxQuantity.Value = approvedQuantity;

                    decimal approvedPrice = 0;
                    decimal.TryParse(manu.CustomerApprovedPrice.ToString(), out approvedPrice);
                    ctrlDecimalTextBoxPrice.Value = approvedPrice;
                }
                else
                {                    
                    ctrlNumericTextBoxQuantity.Value = orderDetail.Quantity;
                    ctrlDecimalTextBoxPrice.Value = orderDetail.Price;
                }

                List<WebControl> buttons = new List<WebControl>();
                buttons.Add(btSave);
                base.CheckNotAllowOtherUserEditOrder(buttons, manu.CreatedBy);

               
                if (orderDetail.OrderItemStatus >= OrderItemStatusEnum.CustomerApproved)
                {                    
                    List<WebControl> manufactureRequestTaskControls = new List<WebControl>();
                    manufactureRequestTaskControls.Add(btSave);
                    base.DisableDeleteAndEditButton(manufactureRequestTaskControls);                    
                }
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
            if (manu != null)
            {
                manu.CustomerApproved = cbCustomerApprove.Checked;
                manu.IsFailed = cbCustomerRefuse.Checked;
                if (cbCustomerApprove.Checked)
                {
                    manu.CustomerApprovedDate = DateTime.Now;
                    manu.CustomerApprovedQuantity = ctrlNumericTextBoxQuantity.Value;
                    manu.CustomerApprovedPrice = ctrlDecimalTextBoxPrice.Value;
                }
                else
                {
                    manu.CustomerApprovedDate = null;
                    manu.CustomerApprovedQuantity = 0;
                    manu.CustomerApprovedPrice = 0;
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

        public int OrderId
        {
            get
            {
                return CommonHelper.QueryStringInt("OrderId");
            }
        }

        protected void cbCustomerRefuese_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCustomerRefuse.Checked == true)
            {
                cbCustomerApprove.Checked = false;
                ctrlNumericTextBoxQuantity.Value = 0;                
                ctrlDecimalTextBoxPrice.Value = 0;
                panelApproveDetail.Visible = false;
                panelApprovedDate.Visible = false;
            }
            else
            {
                cbCustomerApprove.Checked = true;
            }
        }

        protected void cbCustomerApprove_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCustomerApprove.Checked == true)
            {
                cbCustomerRefuse.Checked = false;
                panelApproveDetail.Visible = true;
                
                ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
                if (manu != null)
                {
                    if (manu.CustomerApproved == true)
                    {
                        int approvedQuantity = 0;
                        decimal approvedPrice = 0;
                        int.TryParse(manu.CustomerApprovedQuantity.ToString(), out approvedQuantity);
                        decimal.TryParse(manu.CustomerApprovedPrice.ToString(), out approvedPrice);
                        ctrlNumericTextBoxQuantity.Value = approvedQuantity;
                        ctrlDecimalTextBoxPrice.Value = approvedPrice;
                        
                    }
                    else
                    {
                        OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
                        ctrlNumericTextBoxQuantity.Value = orderDetail.Quantity;
                        ctrlDecimalTextBoxPrice.Value = orderDetail.Price;
                        panelApprovedDate.Visible = false;
                    }
                }
            }
            else
            {
                cbCustomerRefuse.Checked = true;
            }
        }


    }
}