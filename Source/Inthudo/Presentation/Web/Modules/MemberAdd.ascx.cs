using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using InthudoService;

namespace Web.Modules
{
    public partial class MemberAdd : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected MemberBO Save()
        {
            MemberBO mem = ctrlMemberInfo.SaveInfo();
            return mem;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Save();
                    Response.Redirect("Members.aspx");
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected void btSaveAndContinueEdit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    MemberBO mem = Save();
                    Response.Redirect("Members.aspx?MemberId=" + mem.UserId.ToString());
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected void btReturnUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("Members.aspx");
        }

        
    }
}