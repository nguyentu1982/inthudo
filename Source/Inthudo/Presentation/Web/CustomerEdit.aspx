<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="Web.CustomerEdit" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/CustomerEdit.ascx" TagName="CustomerEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:CustomerEdit runat="server" ID="ctrlCustomerEdit" />
</asp:Content>
