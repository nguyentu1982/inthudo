using BusinessObjects;
using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Modules
{
    public partial class ManufactureRequestInfo : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDefautData();
                BindData();
            }
        }

        private void BindData()
        {
            ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
            if (manu != null)
            {
                lbManufactureRequestId.Text = manu.ManufactureRequestId.ToString();
                lbManufactureRequestDate.Text = manu.CreatedOn.ToShortDateString();
                ctrlDatePickerBeginDate.SelectedDate = manu.BeginDate;
                ctrlDatePickerEndDate.SelectedDate = manu.EndDate;
                txtRequirement.Text = manu.Description;
                ctrlNumbericTextBoxQuantity.Value = manu.Quantity;

                decimal price = 0;
                decimal.TryParse(manu.Cost.ToString(), out price);
                ctrlDecimalTextBoxCost.Value = price;
                ctrlManufactureSelect.CustomerId = manu.ManufactureId.ToString();
                ctrlManufactureSelect.txtCustomerCode_TextChanged(new object(), new EventArgs());
            }
            else
            {
                panelManufactureRequestID.Visible = false;
                OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
                if (orderDetail != null)
                {
                    ctrlNumbericTextBoxQuantity.Value = orderDetail.Quantity;
                }
                ctrlDatePickerBeginDate.Visible= false;
                ctrlDatePickerEndDate.Visible = false;
            }
        }

        private void LoadDefautData()
        {
            lbOrderId.Text = this.OrderId.ToString();
            lbOrderDetailId.Text = this.OrderDetailId.ToString();
            lbDesignRequestId.Text = this.DesignRequestId.ToString();
            MemberBO mem = this.MemberService.GetMemberByOrder(this.OrderId);
            lbBusinessMan.Text = mem.FullName;
            CustomerBO cust = this.CustomerService.GetCustomerByOrder(this.OrderId);
            lbCustomer.Text = string.Format("Tên: {0}, Địa chỉ: {1}, SĐT: {2}", cust.Name, cust.Address, cust.Telephone);
            
        }

        public ManufactureRequestBO SaveInfo()
        {
            if (ctrlManufactureSelect.CustomerId == string.Empty)
            {
                throw new Exception("Bạn hãy nhập mã số khách hàng!");
            }

            int custId = 0;
            int.TryParse(ctrlManufactureSelect.CustomerId, out custId);
            if (this.CustomerService.GetCustomerById(custId) == null)
            {
                throw new Exception("Mã số khách hàng không đúng!");
            }

            ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(this.ManufactureRequestId);
            if (manu != null)
            {
                manu.Description = txtRequirement.Text;
                //manu.BeginDate = ctrlDatePickerBeginDate.SelectedDate;
                //manu.EndDate = ctrlDatePickerEndDate.SelectedDate;
                manu.Quantity = ctrlNumbericTextBoxQuantity.Value;
                manu.Cost = ctrlDecimalTextBoxCost.Value;
                manu.LastEditedBy = this.LoggedInUserId;
                manu.LastEditedOn = DateTime.Now;
                manu.ManufactureId = int.Parse(ctrlManufactureSelect.CustomerId);

                this.OrderService.UpdateManufactureRequest(manu);
            }
            else
            { 
                manu = new ManufactureRequestBO()
                {
                    Description = txtRequirement.Text,
                    //BeginDate = ctrlDatePickerBeginDate.SelectedDate,
                    //EndDate = ctrlDatePickerEndDate.SelectedDate,
                    Quantity = ctrlNumbericTextBoxQuantity.Value,
                    Cost = ctrlDecimalTextBoxCost.Value,
                    CreatedBy = this.LoggedInUserId,
                    CreatedOn = DateTime.Now,
                    DesignRequestId = this.DesignRequestId,
                    ManufactureId = int.Parse(ctrlManufactureSelect.CustomerId)
                };

                int id = this.OrderService.InsertManufactureRequest(manu);
                manu.ManufactureRequestId = id;
            }

            return manu;
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
    }
}