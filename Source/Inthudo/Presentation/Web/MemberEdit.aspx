<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberEdit.aspx.cs" Inherits="Web.MemberEdit" %>
<%@ Register TagPrefix="inthudo" TagName="MemberEdit" Src="~/Modules/MemberEdit.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <inthudo:MemberEdit runat="server" ID="ctrlMemberEdit" />
</asp:Content>
