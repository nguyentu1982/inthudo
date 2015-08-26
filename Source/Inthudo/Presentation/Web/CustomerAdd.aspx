<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CustomerAdd.aspx.cs" Inherits="Web.CustomerAdd" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/CustomerAdd.ascx" TagName="CustomerAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:CustomerAdd runat="server" ID="ctrlCustomerAdd" />
</asp:Content>
