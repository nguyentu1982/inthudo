<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ManufactureRequestEdit.aspx.cs" Inherits="Web.ManufactureRequestEdit" %>
<%@ Register TagPrefix="inthudo" TagName="ManufactureRequestEdit" Src="~/Modules/ManufactureRequestEdit.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:ManufactureRequestEdit runat="server" ID="ManufactureRequestEdit" />
</asp:Content>
