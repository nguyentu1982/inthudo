using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class BasePage : System.Web.UI.Page
    {
        public void CheckLogin()
        {
            string url = Request.Url.ToString();
            if (Session["IsLogedin"] == null)
            {
                Response.Redirect(string.Format("Login.aspx?ReturnURL={0}", Server.UrlEncode(url)));
                return;
            }

            if ((bool)Session["IsLogedin"] == false)
            {
                Response.Redirect(string.Format("Login.aspx?ReturnURL={0}", Server.UrlEncode(url)));
            }
        }
    }
}