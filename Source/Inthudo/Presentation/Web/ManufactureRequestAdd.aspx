<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ManufactureRequestAdd.aspx.cs" Inherits="Web.ManufactureRequestAdd" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/ManufactureRequestAdd.ascx" TagName="ManufactureRequestAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:ManufactureRequestAdd runat="server" ID="ctrlManufactureRequestAdd"></inthudo:ManufactureRequestAdd>
</asp:Content>
