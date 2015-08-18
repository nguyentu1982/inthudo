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
                
                ddlDesigner.SelectedValue = design.OrderItem.DesignerId.ToString();
                ctrlDatePickerFrom.SelectedDate = design.BeginDate;
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
                //Business man
                MemberBO bussinessMan = this.MemberService.GetMemberByOrder(orderDetail.OrderId);
                ddlBussinessMan.SelectedValue = bussinessMan.UserId.ToString();
                //Designer man
                if (orderDetail.DesignerId != null)
                {
                    ddlDesigner.SelectedValue = orderDetail.DesignerId.ToString();
                }
            }
        }

        private void FillDropDowns()
        {
            

            string orderBy = "UserId ASC";
            //Business man
            ddlBussinessMan.Items.Clear();
            ddlBussinessMan.Items.Add(new ListItem("", "0"));
            List<MemberBO> mems = this.MemberService.GetMembers(orderBy);
            foreach (MemberBO m in mems)
            { 
                ddlBussinessMan.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }
            //Designer man
            ddlDesigner.Items.Clear();
            ddlDesigner.Items.Add(new ListItem("", "0"));
            foreach (MemberBO m in mems)
            {
                ddlDesigner.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }

            //Default Data
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            ctrlCustomerSelect.CustomerCode = order.CustomerId.ToString();
            ctrlCustomerSelect.txtCustomerCode_TextChanged(new object(), new EventArgs());

            lbOrderId.Text = order.OrderId.ToString();
            lbOrderDetailId.Text = this.OrderDetailId.ToString();
            MemberBO businessMan = this.MemberService.GetMemberByOrder(order.OrderId);
            ddlBussinessMan.SelectedValue = businessMan.UserId.ToString();
            ddlBussinessMan.Enabled = false;
        }

        public DesignRequestBO SaveDesignRequestInfo()
        {
            DesignRequestBO designReq = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            if (designReq != null)
            {
                designReq.Description = txtDesignRequirement.Text;
                designReq.DesignerId = int.Parse(ddlDesigner.SelectedValue);
                designReq.BeginDate = ctrlDatePickerFrom.SelectedDate;
                designReq.EndDate = ctrlDatePickerTo.SelectedDate;
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
                    BeginDate = ctrlDatePickerFrom.SelectedDate,
                    EndDate = ctrlDatePickerTo.SelectedDate,
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