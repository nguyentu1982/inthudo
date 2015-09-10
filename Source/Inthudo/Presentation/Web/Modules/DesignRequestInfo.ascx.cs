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
            OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
            if (design != null)
            {
                lbDesignRequestId.Text = design.DesignRequestId.ToString();                
                lbDesignRequestDate.Text = design.CreatedOn.ToShortDateString();
                if (design.DesignerId != null)
                    ddlDesigner.SelectedValue = design.DesignerId.ToString();
                ctrlDatePickerFrom.Visible = true;
                ctrlDatePickerFrom.SelectedDate = design.BeginDate;
                ctrlDatePickerTo.Visible = true;
                ctrlDatePickerTo.SelectedDate = design.EndDate;
                txtDesignRequirement.Text = design.Description;
                decimal cost = 0;
                decimal.TryParse(design.Cost.ToString(), out cost);
                ctrlDecimalTextBoxDesignCost.Value = cost;
                cbIsDesignOfCustomer.Checked = orderDetail.IsCustomerHasDesign;
            }
            else
            {
                panelDesignRequestId.Visible = false;
                //OrderDetailId
                lbOrderDetailId.Text = this.OrderDetailId.ToString();
                //OrderId
                lbOrderId.Text = orderDetail.OrderId.ToString();                
            }
        }

        private void FillDropDowns()
        {

            lbOrderId.Text = this.OrderId.ToString();
            MemberBO mem = this.MemberService.GetMemberByOrder(this.OrderId);
            lbBusinessMan.Text = mem.FullName;
            CustomerBO cust = this.CustomerService.GetCustomerByOrder(this.OrderId);
            lbCustomer.Text = string.Format("Tên: {0}, Địa chỉ: {1}, SĐT: {2}", cust.Name, cust.Address, cust.Telephone);

            
            List<MemberBO> mems = this.MemberService.GetDesigners(base.LoggedInOrganizationIds);
           
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
                //designReq.DesignerId = int.Parse(ddlDesigner.SelectedValue);
                //designReq.BeginDate = ctrlDatePickerFrom.SelectedDate;
                //designReq.EndDate = ctrlDatePickerTo.SelectedDate;
                designReq.Cost = ctrlDecimalTextBoxDesignCost.Value;
                designReq.LastEditedBy = LoggedInUserId;
                designReq.LastEditedOn = DateTime.Now;

                int designerId = int.Parse(ddlDesigner.SelectedValue);

                if (designerId != 0)
                {
                    designReq.DesignerId = designerId;
                }
                else
                {
                    designReq.DesignerId = null;
                }

                if (cbIsDesignOfCustomer.Checked && designerId == 0)
                {
                    designReq.BeginDate = DateTime.Now;
                    designReq.EndDate = DateTime.Now;
                    designReq.ApprovedByCustomer = true;
                    designReq.ApprovedDate = DateTime.Now;
                }   

                this.OrderService.UpdateDesignRequest(designReq);
            }
            else
            {
                designReq = new DesignRequestBO()
                {                    
                    Description = txtDesignRequirement.Text,
                    //DesignerId = int.Parse(ddlDesigner.SelectedValue),
                    //BeginDate = ctrlDatePickerFrom.SelectedDate,
                    //EndDate = ctrlDatePickerTo.SelectedDate,
                    Cost = ctrlDecimalTextBoxDesignCost.Value,
                    CreatedBy = LoggedInUserId,
                    CreatedOn = DateTime.Now,
                    OrderItemId = int.Parse(lbOrderDetailId.Text)
                };
                int designerId = int.Parse(ddlDesigner.SelectedValue);

                if (designerId != 0)
                {
                    designReq.DesignerId = designerId;
                }
                else
                {
                    designReq.DesignerId = null;
                }

                if (cbIsDesignOfCustomer.Checked && designerId == 0)
                {
                    designReq.BeginDate = DateTime.Now;
                    designReq.EndDate = DateTime.Now;
                    designReq.ApprovedByCustomer = true;
                    designReq.ApprovedDate = DateTime.Now;
                }               

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