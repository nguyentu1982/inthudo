<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="OrderDetailEdit.aspx.cs" Inherits="Web.OrderDetailEdit" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailEdit" Src="~/Modules/OrderDetailEdit.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:OrderDetailEdit runat="server" ID="ctrlOrderDetailInfo"/>
</asp:Content>
