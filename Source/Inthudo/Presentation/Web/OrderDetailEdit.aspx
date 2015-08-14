<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="OrderDetailEdit.aspx.cs" Inherits="Web.OrderDetailEdit" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailInfo" Src="~/Modules/OrderDetailInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:OrderDetailInfo runat="server" ID="ctrlOrderDetailInfo" ActionButtonIsDisplay="true" />
</asp:Content>
