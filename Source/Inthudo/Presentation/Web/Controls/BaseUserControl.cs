using BusinessObjects;
using Common;
using InthudoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public class BaseUserControl:UserControl
    {
        protected void ProcessException(Exception exc)
        {
            //this.LogService.InsertLog(LogTypeEnum.AdministrationArea, exc.Message, exc);
            //if (showError)
            //{
            //    if (this.SettingManager.GetSettingValueBoolean("Display.AdminArea.ShowFullErrors"))
            //    {
            //        ShowError(exc.Message, exc.ToString());
            //    }
            //    else
            //    {
            //        ShowError(exc.Message, string.Empty);
            //    }
            //}
            ShowError(exc.Message, string.Empty);
            
        }


        protected void CheckNotAllowOtherUserEditOrder(List<WebControl> buttons, int createdBy)
        {
            //Check roles
            if (this.SettingService.GetBoolSetting(Constant.Setting.Not_Allow_Other_User_Edit_Order))
            {
                if (LoggedInUserId != createdBy && LoggedInMember.RoleName.ToLower()!= Constant.ADMIN_ROLE_NAME.ToLower())
                {
                    foreach (WebControl b in buttons)
                    {
                        if (b is Button || b is HyperLink)
                        {
                            b.Visible = false;
                        }
                        else
                        {
                            b.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void DisableDeleteAndEditButton(List<WebControl> controls)
        {
            if (LoggedInMember.RoleName.ToLower() != Constant.ADMIN_ROLE_NAME.ToLower())
            {
                foreach (WebControl c in controls)
                {
                    if (c is Button || c is HyperLink)
                    {
                        c.Visible = false;
                    }
                    else
                    {
                        c.Enabled = false;
                    }
                }
            }
        }

        protected void ShowError(string message, string completeMessage)
        {
            if (this.Page == null)
                return;

            MasterPage masterPage = this.Page.Master;
            if (masterPage == null)
                return;

            BaseMasterPage inthudoMasterPage = masterPage as BaseMasterPage;
            if (inthudoMasterPage != null)
                inthudoMasterPage.ShowError(message, completeMessage);
        }

        public int LoggedInUserId
        {
            get
            {
                return int.Parse(Session["UserId"].ToString());
            }
        }

        public string DepartmentName
        {
            get
            {
                MemberBO mem = this.MemberService.GetMember(this.LoggedInUserId);
                return mem.DepartmentName;
            }
        }

        public MemberBO LoggedInMember
        {
            get
            {
                return this.MemberService.GetMember(this.LoggedInUserId);
            }
        }

        public List<int> LoggedInOrganizationIds
        {
            get
            {
                List<int> result = new List<int>();
                List<OrganizationBO> orgs = this.MemberService.GetOrganizationsByMemberId(LoggedInUserId);
                foreach (OrganizationBO o in orgs)
                {
                    result.Add(o.OrganizationId);
                }
                return result;
            }
        }

        public IMemberService MemberService
        {
            get
            {
                return new MemberService();
            }
        }

        public IOrderService OrderService
        {
            get { return new OrderService(); }
        }

        public IProductService ProductService
        {
            get
            { return new ProductService(); }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return new CustomerService();
            }
        }

        public ISettingService SettingService
        {
            get
            {
                return new SettingService();
            }
        }
    }
}