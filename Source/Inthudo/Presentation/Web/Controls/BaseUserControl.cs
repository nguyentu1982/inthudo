using InthudoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

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
    }
}