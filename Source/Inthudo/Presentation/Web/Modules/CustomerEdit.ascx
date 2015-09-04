<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerEdit.ascx.cs" Inherits="Web.Modules.CustomerEdit" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/CustomerInfo.ascx" TagName="CustomerInfo" %>

<h1>
    <asp:Label ID="lbCustomerEditHeader" runat="server" ></asp:Label>
</h1>
<asp:Button runat="server" ID="btSave" Text="Lưu" OnClick="btSave_Click" />
<asp:Button runat="server" ID="btDelete" Text="Xóa" OnClick="btDelete_Click" OnClientClick="return confirmDelete()" />
<inthudo:CustomerInfo runat="server" ID="ctrlCustomerInfo" />