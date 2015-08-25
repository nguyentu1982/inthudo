<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DesignRequests.aspx.cs" Inherits="Web.DesignRequests" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DesignRequests.ascx" TagName="DesignRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <inthudo:DesignRequests runat="server" ID="ctrlDesignRequests" />
</asp:Content>
