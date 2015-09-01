<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Web.Dashboard" %>
<%@ Register TagPrefix="inthudo"  Src="~/Modules/Dashboard.ascx" TagName="Dashboard"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <inthudo:Dashboard runat="server" ID ="ctrlDashboard" />
</asp:Content>
