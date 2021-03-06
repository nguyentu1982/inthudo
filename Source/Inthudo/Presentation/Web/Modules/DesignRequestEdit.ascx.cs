﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Common.Utils;
using Common;

namespace Web.Modules
{
    public partial class DesignRequestEdit : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();                
            }
        }

        private void BindData()
        {
            DesignRequestBO designRequest = this.OrderService.GetDesignRequestById(this.DesignRequestId);
            OrderBO order = this.OrderService.GetOrderById(this.OrderId);
            if (designRequest != null)
            {
                if (!designRequest.BeginDate.HasValue || !designRequest.EndDate.HasValue)
                {
                    pnlDesignRequestCustomerApprove.Visible = false;
                }
            }

            //Check whether other user can edit design request
            List<WebControl> buttons = new List<WebControl>();
            buttons.Add(btSave);
            buttons.Add(btDelete);
            base.CheckNotAllowOtherUserEditOrder(buttons, order.BusinessManId);

            OrderItemlBO orderDetail = this.OrderService.GetOrderDetailById(this.OrderDetailId);
            if (orderDetail.OrderItemStatus >= OrderItemStatusEnum.Designing)
            {
                List<WebControl> controls = new List<WebControl>();
                controls.Add(btSave);
                controls.Add(btDelete);
                base.DisableDeleteAndEditButton(controls);               
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    DesignRequestBO designRequest = ctrlDesignRequestInfo.SaveDesignRequestInfo();
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.OrderService.MaskDesignRequestAsDeleted(this.DesignRequestId, this.LoggedInUserId);
                Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script>window.close()</script>");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public int DesignRequestId
        {
            get { 
                 return CommonHelper.QueryStringInt("DesignRequestId");
            }
        }

        public int OrderDetailId
        {
            get
            {
                return CommonHelper.QueryStringInt("OrderDetailId");
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