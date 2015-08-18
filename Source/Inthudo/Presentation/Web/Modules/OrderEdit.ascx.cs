﻿using BusinessObjects;
using Common.Utils;
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

        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.OrderService.MarkOrderAsDeleted(this.OrderId);
                Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script>window.close()</script>");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public int OrderId
        {
            get
            {
                return CommonHelper.QueryStringInt("OrderId");
            }
        }
    }
}