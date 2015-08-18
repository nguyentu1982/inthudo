<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="OrderDetailAdd.aspx.cs" Inherits="Web.OrderDetailAdd" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailAdd" Src="~/Modules/OrderDetailAdd.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:OrderDetailAdd runat="server" ID="ctrlOrderDetailInfo" />
</asp:Content>