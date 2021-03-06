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
    public partial class CustomerInfo : BaseUserControl
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
            CustomerBO customer = this.CustomerService.GetCustomerById(this.CustomerId);
            if (customer != null)
            {
                lbCustomerIDTitle.Text = string.Format("Mã số {0} :", customer.CustomerType.Name);
                lbCustomerID.Text = customer.CustomerId.ToString();
                lbCustomerNameTitle.Text = string.Format("Tên {0} :", customer.CustomerType.Name);
                txtCustomerName.Text = customer.Name;
                txtTelephone.Text = customer.Telephone;
                txtAddress.Text = customer.Address;
                ctrlEmailTextBox.Text = customer.Email;
                txtNote.Text = customer.Note;
                txtCompanyName.Text = customer.Company;
                txtPhoneNumber.Text = customer.PhoneNumber;
                txtFaxNumber.Text = customer.FaxNumber;
                txtTaxCode.Text = customer.TaxCode;
            }
            else
            {
                panelCustomerId.Visible = false;
                CustomerTypeBO custType = this.CustomerService.GetCustomerTypeById(this.CustomerTypeId);
                if (custType == null) throw new Exception("Bạn hãy liên hệ với Admin");
                lbCustomerNameTitle.Text = string.Format("Tên {0} :", custType.Name);
                
            }
        }

        public CustomerBO SaveInfo()
        {
            CustomerBO customer = this.CustomerService.GetCustomerById(this.CustomerId);
            if (customer != null)
            {
                customer.Name = txtCustomerName.Text;
                customer.Telephone = txtTelephone.Text;
                customer.Address = txtAddress.Text;
                customer.Email = ctrlEmailTextBox.Text;
                customer.Note = txtNote.Text;
                customer.Company = txtCompanyName.Text;
                customer.PhoneNumber = txtPhoneNumber.Text;
                customer.FaxNumber = txtFaxNumber.Text;
                customer.TaxCode = txtTaxCode.Text;
                customer.LastEditedOn = DateTime.Now;
                customer.LastEditedBy = this.LoggedInUserId;
                customer.CustomerTypeId = this.CustomerTypeId;

                this.CustomerService.UpdateCustomer(customer);
            }
            else
            {
                customer = new CustomerBO() { 
                    Name = txtCustomerName.Text,
                    Telephone = txtTelephone.Text,
                    Address = txtAddress.Text,
                    Email = ctrlEmailTextBox.Text,
                    Note = txtNote.Text,
                    Company = txtCompanyName.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    FaxNumber = txtFaxNumber.Text,
                    TaxCode = txtTaxCode.Text,
                    CreatedOn = DateTime.Now,
                    CreatedBy = this.LoggedInUserId,
                    CustomerTypeId = this.CustomerTypeId
                };

                customer.CustomerId = this.CustomerService.InsertCustomer(customer);
            }

            return customer;
        }

        public int CustomerId
        {
            get
            {
                return CommonHelper.QueryStringInt(Constant.QUERY_STRING_CUST_ID);
            }
        }

        public int CustomerTypeId
        {
            get {
                return CommonHelper.QueryStringInt(Constant.Customer.QUERY_STRING_CUSTOMER_TYPE);
            }
        }
    }
}