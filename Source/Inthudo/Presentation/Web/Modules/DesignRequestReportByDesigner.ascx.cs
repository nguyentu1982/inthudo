using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class DesignRequestReportByDesigner : BaseUserControl
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
            MemberBO mem = this.MemberService.GetMember(this.MemberId);
            if (mem != null)
            {
                lbDesingnerName.Text = mem.FullName;
                lbUserName.Text = mem.UserName;
            }

            List<DesignRequestBO> designRequest = this.OrderService.GetDesignRequests(new DesignRequestSearch()
            {
                CustomerId = 0,
                DesignerId = this.MemberId,
                DesignRequestStatus = 0,
                ProductId = 0,
                RequestFrom = this.From,
                RequestTo = this.To
            });

            //Created
            var dsCreated = designRequest.Where(ds => ds.DesignRequestStatus == DesignRequestStatusEnum.DesignRequestCreated);
            lbNumberOfRequestCreated.Text = dsCreated.Count().ToString();
            hplCreated.NavigateUrl = CreateDesignRequestUrl(DesignRequestStatusEnum.DesignRequestCreated);
            hplCreated.Target = "_blank";
            
            //Desiging
            var dsDesigning = designRequest.Where(ds => ds.DesignRequestStatus == DesignRequestStatusEnum.Designing);
            lbNumberOfRequestDesigning.Text = dsDesigning.Count().ToString();
            decimal costDesigning = 0;
            decimal.TryParse(dsDesigning.Sum(ds => ds.Cost).ToString(), out costDesigning);
            lbCostDesinging.Text = costDesigning.ToString("C0");
            hplDesigning.NavigateUrl = this.CreateDesignRequestUrl(DesignRequestStatusEnum.Designing);
            hplDesigning.Target = "_blank";
            
            //Completed
            var dsCompleted = designRequest.Where(ds => ds.DesignRequestStatus == DesignRequestStatusEnum.DesignCopmleted);
            lbNumberOfRequestDesigningCompleted.Text = dsCompleted.Count().ToString();
            decimal costCompleted = 0;
            decimal.TryParse(dsCompleted.Sum(ds => ds.Cost).ToString(), out costCompleted);
            lbCostDesingingCompleted.Text = costCompleted.ToString("C0");
            hplCompleted.NavigateUrl = this.CreateDesignRequestUrl(DesignRequestStatusEnum.DesignCopmleted);
            hplCompleted.Target = "_blank";

            //Re-Designing
            var dsReDesinging = designRequest.Where(ds => ds.DesignRequestStatus == DesignRequestStatusEnum.DesignNotApproved);
            lbNumberOfRequestReDesigning.Text = dsReDesinging.Count().ToString();
            decimal costReDesigning = 0;
            decimal.TryParse(dsReDesinging.Sum(ds => ds.Cost).ToString(), out costReDesigning);
            lbCostReDesigning.Text = costReDesigning.ToString("C0");
            hplReDesigning.NavigateUrl = this.CreateDesignRequestUrl(DesignRequestStatusEnum.DesignNotApproved);
            hplReDesigning.Target = "_blank";

            //Approved
            var dsApproved = designRequest.Where(ds => ds.DesignRequestStatus == DesignRequestStatusEnum.DesignApprovedByCustomer);
            lbNumberOfRequestApproved.Text = dsApproved.Count().ToString();
            decimal costApproved = 0;
            decimal.TryParse(dsApproved.Sum(ds => ds.Cost).ToString(), out costApproved);
            lbCostApproved.Text = costApproved.ToString("C0");
            hplApproved.NavigateUrl = this.CreateDesignRequestUrl(DesignRequestStatusEnum.DesignApprovedByCustomer);
            hplApproved.Target = "_blank";
        }

        private string CreateDesignRequestUrl(DesignRequestStatusEnum designRequestStatus)
        {
            string url = string.Format("~/DesignRequests.aspx?MemberId={0}", this.MemberId);
            if (From != null)
                url = url + string.Format("&From={0}", From.ToString());
            if (To != null)
                url = url + string.Format("&To={0}", To.ToString());
            if (designRequestStatus != 0)
            {
                url = url + string.Format("&designRequestStatus={0}", (int)designRequestStatus);
            }
            return url;
        }
    }
}