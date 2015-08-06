<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderAdd.aspx.cs" Inherits="Web.OrderAdd" %>
<%@ Register TagPrefix="inthudo" TagName="OrderAdd" Src="~/Modules/OrderAdd.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <inthudo:OrderAdd ID="ctrlOrderAdd" runat="server" />
</asp:Content>
