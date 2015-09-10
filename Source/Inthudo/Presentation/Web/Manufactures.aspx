<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manufactures.aspx.cs" Inherits="Web.Manufactures" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/Manufactures.ascx" TagName="Manufactures" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <inthudo:Manufactures runat="server" ID="ctrlManufactures" />
</asp:Content>
