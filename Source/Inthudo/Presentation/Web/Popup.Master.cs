using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Popup : BaseMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void ShowMessage(string message)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = "messageBox messageBoxSuccess";
            lMessage.Text = message;
        }

        public override void ShowError(string message, string completeMessage)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = "messageBox messageBoxError";
            lMessage.Text = message;
            lMessageComplete.Text = completeMessage;
        }
    }
}