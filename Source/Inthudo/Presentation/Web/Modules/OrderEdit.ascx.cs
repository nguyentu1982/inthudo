using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Modules
{
    public partial class OrderEdit : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btSave_Click(object sender, EventArgs e)
        {
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

        protected OrderBO Save()
        {
            OrderBO order = ctrlOrderInfo.SaveInfo();
            return order;
        }
    }
}