﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InthudoService;
using BusinessObjects;

namespace Web.Module
{
    public partial class Login : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginCtrl_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (ValidateUser(loginCtrl.UserName, loginCtrl.Password))
            {
                // e.Authenticated = true;
                loginCtrl.Visible = false;
                Session["IsLogedin"] = true;
                Member mem = this.MemberService.GetMemberByUserName(loginCtrl.UserName);
                Session["UserId"] = mem.UserId;
            }
            else
            {
                e.Authenticated = false;
                Session["IsLogedin"] = false;
            }
        }

        private bool ValidateUser(string userName, string pass)
        {
            IMemberService memeberService = new MemberService();
            return memeberService.ValidateUser(userName, pass);
        }
    }
}