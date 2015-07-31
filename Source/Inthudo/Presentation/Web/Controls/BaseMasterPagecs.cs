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
    }
}