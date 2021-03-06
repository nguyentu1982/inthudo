﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InthudoService;
using BusinessObjects;
using Common;

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
                MemberBO mem = this.MemberService.GetMemberByUserName(loginCtrl.UserName);
                Session["UserId"] = mem.UserId;
                mem = this.MemberService.GetMember(mem.UserId);
                if (string.IsNullOrEmpty(ReturnURL))
                {
                    if (mem.RoleName.ToLower() == Constant.USER_ROLE_NAME.ToLower())
                    {
                        if (mem.Department.Code == Constant.PKD)
                            Response.Redirect("Orders.aspx");

                        if (mem.Department.Code == Constant.PTK)
                            Response.Redirect("DesignRequests.aspx");
                    }
                    else
                    {
                        Response.Redirect("Dashboard.aspx");
                    }
                }

                Response.Redirect(ReturnURL);
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

        private string ReturnURL
        {
            get { return Common.Utils.CommonHelper.QueryString("ReturnURL"); }
        }
    }
}