using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using InthudoService;
using Common.Utils;

namespace Web.Modules
{
    public partial class DesignRequestInfo : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                FillDropDowns();
                BindData();
            }
        }

        private void BindData()
        {
            DesignRequestBO design = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (design != null)
            {
                lbDesignRequestId.Text = design.DesignRequestId.ToString();                
                lbDesignRequestDate.Text = design.CreatedOn.ToShortDateString();
                ddlDesigner.SelectedValue = design.DesignerId.ToString();
                ctrlDatePickerFrom.Visible = true;
                ctrlDatePickerFrom.SelectedDate = design.BeginDate;
                ctrlDatePickerTo.Visible = true;
                ctrlDatePickerTo.SelectedDate = design.EndDate;
                txtDesignRequirement.Text = design.Description;
                decimal cost = 0;
                decimal.TryParse(design.Cost.ToString(), out cost);
                ctrlDecimalTextBoxDesignCost.Value = cost;                 
            }
            else
            {
                panelDesignRequestId.Visible = false;
                //OrderDetailId
                lbOrderDetailId.Text = this.OrderDetailId.ToString();
                //OrderId
                OrderDetailBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
                lbOrderId.Text = orderDetail.OrderId.ToString();
                
                //Designer man
                if (orderDetail.DesignerId != null)
                {
                    ddlDesigner.SelectedValue = orderDetail.DesignerId.ToString();
                }
            }
        }

        private void FillDropDowns()
        {

            lbOrderId.Text = this.OrderId.ToString();
            MemberBO mem = this.MemberService.GetMemberByOrder(this.OrderId);
            lbBusinessMan.Text = mem.FullName;
            CustomerBO cust = this.CustomerService.GetCustomerByOrder(this.OrderId);
            lbCustomer.Text = string.Format("Tên: {0}, Địa chỉ: {1}, SĐT: {2}", cust.Name, cust.Address, cust.Telephone);
            string orderBy = "UserId ASC";
            
            List<MemberBO> mems = this.MemberService.GetMembers(orderBy);
           
            //Designer man
            ddlDesigner.Items.Clear();
            ddlDesigner.Items.Add(new ListItem("", "0"));
            foreach (MemberBO m in mems)
            {
                ddlDesigner.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            } 
           
            lbOrderDetailId.Text = this.OrderDetailId.ToString();
            
        }

        public DesignRequestBO SaveDesignRequestInfo()
        {
            DesignRequestBO designReq = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (designReq != null)
            {
                designReq.Description = txtDesignRequirement.Text;
                designReq.DesignerId = int.Parse(ddlDesigner.SelectedValue);
                //designReq.BeginDate = ctrlDatePickerFrom.SelectedDate;
                //designReq.EndDate = ctrlDatePickerTo.SelectedDate;
                designReq.Cost = ctrlDecimalTextBoxDesignCost.Value;
                designReq.LastEditedBy = LoggedInUserId;
                designReq.LastEditedOn = DateTime.Now;                

                this.OrderService.UpdateDesignRequest(designReq);
            }
            else
            {
                designReq = new DesignRequestBO()
                {                    
                    Description = txtDesignRequirement.Text,
                    DesignerId = int.Parse(ddlDesigner.SelectedValue),
                    //BeginDate = ctrlDatePickerFrom.SelectedDate,
                    //EndDate = ctrlDatePickerTo.SelectedDate,
                    Cost = ctrlDecimalTextBoxDesignCost.Value,
                    CreatedBy = LoggedInUserId,
                    CreatedOn = DateTime.Now,
                    OrderItemId = int.Parse(lbOrderDetailId.Text)
                };

                designReq.DesignRequestId = this.OrderService.InsertDesignRequest(designReq);
            }
            return designReq;
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