using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Modules
{
    public partial class OrderAdd : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected OrderBO Save()
        {
            OrderBO order = ctrlOrderInfo.SaveInfo();
            return order;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            Page.Validate();
            
            if (Page.IsValid)
            {
                try
                {
                    OrderBO order = Save();
                    
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected void btSaveAndContinueEdit_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                try
                {
                    OrderBO order = Save();
                    
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }
    }
}