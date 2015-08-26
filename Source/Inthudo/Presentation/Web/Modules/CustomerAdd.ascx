<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdd.ascx.cs" Inherits="Web.Modules.CustomerAdd" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/CustomerInfo.ascx" TagName="CustomerInfo" %>

<h1>Thêm khách hàng mới</h1>
<asp:Button runat="server" ID="btSave" Text="Lưu" OnClick="btSave_Click" />
<inthudo:CustomerInfo runat="server" ID="ctrlCustomerInfo" />