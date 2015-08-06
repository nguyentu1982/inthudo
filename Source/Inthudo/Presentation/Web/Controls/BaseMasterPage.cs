using InthudoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Web
{
    public class BaseMasterPage:MasterPage
    {
        public virtual void ShowMessage(string message)
        {

        }

        public virtual void ShowError(string message, string completeMessage)
        {

        }

        public IMemberService MemberService
        {
            get {
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
    }
}