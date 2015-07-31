using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using InthudoService;
using Common.Utils;

namespace Web.Modules
{
    public partial class MemberEdit : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected Member Save()
        {
            Member mem = ctrlMemberInfo.SaveInfo();
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
                    Member mem = Save();
                    Response.Redirect("MemberEdit.aspx?MemberId=" + mem.UserId.ToString());
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {

            IMemberService memberService = new MemberService();
            Member member = memberService.GetMember(this.MemeberId);
            if (member == null) return;
            try
            {
                memberService.DeleteMember(member);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public int MemeberId
        {
            get
            {
                return CommonHelper.QueryStringInt("MemberId");
            }
        }
    }
}