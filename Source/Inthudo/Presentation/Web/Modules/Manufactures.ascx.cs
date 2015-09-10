using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class Manufactures : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDefaultData();
                BindData();
            }
        }

        private void LoadDefaultData()
        {
            Array manuVals = Enum.GetValues(typeof(ManufactureRequestStatusEnum));
            foreach (ManufactureRequestStatusEnum m in manuVals)
            {
                ddlManufactureStatus.Items.Add(new ListItem(this.GetManufactureRequestStatusString(m), ((int)m).ToString()));
            }

            //Products
            ddlProducts.Items.Clear();
            ddlProducts.Items.Add(new ListItem("Tất cả", "0"));
            List<ProductBO> prods = this.ProductService.GetAllProucts();
            foreach (ProductBO p in prods)
            {
                ddlProducts.Items.Add(new ListItem(p.Name, p.ProductId.ToString()));
            }
            //Business Man
            ddlBusinessMan.Items.Clear();
            ddlBusinessMan.Items.Add(new ListItem("Tất cả", "0"));
            List<MemberBO> businessMen = this.MemberService.GetBusinessMen(this.LoggedInOrganizationIds);
            foreach (MemberBO m in businessMen)
            { 
                ddlBusinessMan.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }
            //Designers
            ddlDesigners.Items.Clear();
            ddlDesigners.Items.Add(new ListItem("Tất cả","0"));
            List<MemberBO> designers = this.MemberService.GetDesigners(this.LoggedInOrganizationIds);
            foreach (MemberBO m in designers)
            {
                ddlDesigners.Items.Add(new ListItem(m.FullName, m.UserId.ToString()));
            }
        }

        private void BindData()
        {
            
        }

        protected void btFind_Click(object sender, EventArgs e)
        {
            DateTime? from = ctrlDatePickerRequestFrom.SelectedDate;
            DateTime? to = ctrlDatePickerRequestTo.SelectedDate;

            int orderId = 0;
            int.TryParse(txtOrderId.Text, out orderId);

            int custId = 0;
            int.TryParse(ctrlCustomerSelect.CustomerId, out custId);

            int productId = 0;
            int.TryParse(ddlProducts.SelectedValue, out productId);

            int manufactureId = 0;
            int.TryParse(ctrlManufactureSelect.CustomerId, out manufactureId);

            List<int> printingTypeIds = ctrlPrintingTypeSelect.SelectedValues;

            int manuRequestStatus = 0;
            int.TryParse(ddlManufactureStatus.SelectedValue, out manuRequestStatus);

            int businessManId = 0;
            int.TryParse(ddlBusinessMan.SelectedValue, out businessManId);

            int designerId = 0;
            int.TryParse(ddlDesigners.SelectedValue, out designerId);

            ManufactureRequestSearch manuSearchObj = new ManufactureRequestSearch() { 
                From = from,
                To = to,
                OrderId = orderId,
                CustomerId = custId,
                ProductId = productId,
                ManufactureId = manufactureId,
                PrintTypeIds = printingTypeIds,
                ManufactureRequestStatus = (ManufactureRequestStatusEnum)manuRequestStatus,
                BusinessManId = businessManId,
                DesignerId = designerId
            };

            List<ManufactureRequestBO> manus = this.OrderService.GetManufactureRequests(manuSearchObj);
            grvManufactureRequest.DataSource = manus;
            grvManufactureRequest.DataBind();
        }

        public string GetManufactureRequestStatusString(ManufactureRequestStatusEnum ManufactureRequestStatus)
        {
            
            string result = "Tất cả";

            switch (ManufactureRequestStatus)
            {
                case ManufactureRequestStatusEnum.ManufactureRequestCreated:
                    return "Yêu cầu mới";
                case ManufactureRequestStatusEnum.Manufacturing:
                    return "Đang sản xuất";
                case ManufactureRequestStatusEnum.ManufactureCopmleted:
                    return "Sản xuất xong";
                case ManufactureRequestStatusEnum.ApprovedByCustomer:
                    return "Khách hàng đã duyệt sản phẩm";
                case ManufactureRequestStatusEnum.NotApproved:
                    return "Khách hàng KHÔNG duyệt sản phẩm";
            }

            return result;
            
        }

        protected void grvManufactureRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hiddenManuRequestId = e.Row.Cells[0].FindControl("hdfManufactureRequestId") as HiddenField;
                int manuRequestId = int.Parse(hiddenManuRequestId.Value);
                ManufactureRequestBO manu = this.OrderService.GetManufactureRequestById(manuRequestId);

                if (manu != null)
                {
                    int designRequestId = manu.DesignRequestId;
                    int orderDetailId = this.OrderService.GetDesignRequestById(designRequestId).OrderItemId;
                    int orderId = this.OrderService.GetOrderDetailById(orderDetailId).OrderId;                    
                    HyperLink manuRequestHyperLink = e.Row.Cells[11].FindControl("hlManufactureRequest") as HyperLink;
                    string url = string.Format("/ManufactureRequestEdit.aspx?OrderId={0}&OrderDetailId={1}&DesignRequestId={2}&ManufactureRequestId={3}", orderId, orderDetailId, designRequestId, manuRequestId);
                    manuRequestHyperLink.Attributes.Add("onclick", "OpenWindow('" + url + "')");
                    manuRequestHyperLink.Attributes.Add("class", "a-popup");
                    manuRequestHyperLink.Text = "Xem";
                }
            }
        }
    }
}