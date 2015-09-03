using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class BussinessReportByUser : BaseUserControl
    {
        public int MemberId;
        public DateTime? From;
        public DateTime? To;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            OrderSearch searchObj = new OrderSearch() { 
                BusManId = this.MemberId,
                OrderFrom = this.From,
                OrderTo = this.To,
                OrderId = 0,
                CustId = 0,
                ProductId = 0,
                ShipMethodId = 0,
                DepositMethodId = 0,
                OrderDetailStatus = 0,
                OrderStatus = 0,            
                DesignerManId = 0
            };
            List<OrderBO> orders = this.OrderService.GetOrders(searchObj);
            MemberBO mem = this.MemberService.GetMember(this.MemberId);
            if(mem!=null)
            {
                lbUserName.Text = mem.FullName;
                lbUserId.Text = string.Format("{0} - {1}", mem.UserId.ToString(), mem.UserName);
            }

            //NOT Completed
            lbNumberOfOrderNOTCompleted.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.NotCompleted).Count().ToString();
            decimal orderTotalNotComleted = orders.Where(o => o.OrderStatus == OrderStatusEnum.NotCompleted).Sum(o => o.OrderTotal);
            lbOrderTotalNOTCompleted.Text = orderTotalNotComleted.ToString("C0");
            decimal depositNotCompleted = 0;
            decimal.TryParse(orders.Where(o => o.OrderStatus == OrderStatusEnum.NotCompleted).Sum(o => o.Deposit).ToString(), out depositNotCompleted);
            lbDepositNOTCompleted.Text = depositNotCompleted.ToString("C0");
            lbRemainingNOTCompleted.Text = (orderTotalNotComleted - depositNotCompleted).ToString("C0");

            hplViewDetailNotCompleted.NavigateUrl = CreateOrderUrl(OrderStatusEnum.NotCompleted);
            hplViewDetailNotCompleted.Target = "_blanks";

            //Completed
            lbNumberOfOrderCompleted.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.Completed).Count().ToString();
            lbOrderTotalContractCompleted.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.Completed).Sum(o => o.OrderTotal).ToString("C0");
            
            var ordersCompleted = orders.Where(o => o.OrderStatus == OrderStatusEnum.Completed);
            List<ProductApprovedBO> approved = new List<ProductApprovedBO>();
            foreach (OrderBO o in ordersCompleted)
            {
                List<ProductApprovedBO> approvedProducts = this.OrderService.GetApprovedProductByOrderId(o.OrderId);
                foreach (ProductApprovedBO p in approvedProducts)
                {
                    approved.Add(p);
                }
            }
            lbOrderTotalCompleted.Text = approved.Sum(a => a.Total).ToString("C0");
            decimal depositCompleted =0;
            decimal.TryParse(orders.Where(o => o.OrderStatus == OrderStatusEnum.Completed).Sum(o => o.Deposit).ToString(),out depositCompleted);
            lbDepositCompleted.Text = depositCompleted.ToString("C0");
            lbRemainingCompleted.Text = (approved.Sum(a => a.Total) - depositCompleted).ToString("C0");
            hplViewDetailCompleted.NavigateUrl = this.CreateOrderUrl(OrderStatusEnum.Completed);
            hplViewDetailCompleted.Target = "_blank";
            
            //Failed
            lbNumberOfOrderFailed.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.IsFailed).Count().ToString();
            lbOrderTotalFailded.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.IsFailed).Sum(o => o.OrderTotal).ToString("C0");
            var failedOrders = orders.Where(o => o.OrderStatus == OrderStatusEnum.IsFailed);
            List<ProductApprovedBO> failedProductsResult = new List<ProductApprovedBO>();
            foreach (OrderBO o in failedOrders)
            {
                List<ProductApprovedBO> failedProducts = this.OrderService.GetFailedProductByOrderId(o.OrderId);
                foreach (ProductApprovedBO pa in failedProducts)
                {
                    failedProductsResult.Add(pa);
                }
            }

            lbOrderDetailTotalFailed.Text = failedProductsResult.Sum(fd => fd.Total).ToString("C0");
            decimal depositFailed = 0;
            decimal.TryParse(orders.Where(o => o.OrderStatus == OrderStatusEnum.IsFailed).Sum(o => o.Deposit).ToString(), out depositFailed);
            lbDepositFailed.Text = depositFailed.ToString("C0");
            hplViewDetailFailed.NavigateUrl = this.CreateOrderUrl(OrderStatusEnum.IsFailed);
            hplViewDetailFailed.Target = "_blank";
            //Overdue
            lbNumberOfOrderOverdue.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.Overdue).Count().ToString();
            lbOrderTotalOverdue.Text = orders.Where(o => o.OrderStatus == OrderStatusEnum.Overdue).Sum(o => o.OrderTotal).ToString("C0");
            var overdueOrders = orders.Where(o => o.OrderStatus == OrderStatusEnum.Overdue);
            decimal orderDetailOverdue = 0;
            foreach (OrderBO o in overdueOrders)
            {
                foreach (OrderDetailBO od in o.OrderItems)
                {
                    if (od.OrderDetailStatus == OrderDetailStatusEnum.Overdue)
                    {
                        orderDetailOverdue+= od.Quantity*od.Price;
                    }
                }
            }
            lbOrderDetailTotalOverdue.Text = orderDetailOverdue.ToString("C0");
            decimal depositOverdue = 0;
            decimal.TryParse(orders.Where(o => o.OrderStatus == OrderStatusEnum.Overdue).Sum(o => o.Deposit).ToString(), out depositOverdue);
            lbDepositOverdue.Text = depositOverdue.ToString("C0");
            hplViewDetailOverdue.NavigateUrl = this.CreateOrderUrl(OrderStatusEnum.Overdue);
            hplViewDetailOverdue.Target = "_blank";
        }

        private string CreateOrderUrl(OrderStatusEnum orderStatus)
        {
            string url = string.Format("~/Orders.aspx?MemId={0}", this.MemberId);
            if (From != null)
                url = url + string.Format("&From={0}", From.ToString());
            if(To!=null)
                url = url + string.Format("&To={0}", To.ToString());
            if (orderStatus != 0)
            {
                url = url + string.Format("&OrderStatus={0}", (int)orderStatus);
            }
            return url;
        }
    }
}