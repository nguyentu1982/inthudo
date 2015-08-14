<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderEdit.aspx.cs" Inherits="Web.OrderEdit" ValidateRequest="false" %>
<%@ Register TagPrefix="inthudo" TagName="OrderEdit" Src="~/Modules/OrderEdit.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <inthudo:OrderEdit runat="server" ID="ctrlOrderEdit"/>
</asp:Content>
