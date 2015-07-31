<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberAdd.aspx.cs" Inherits="Web.MemberAdd" %>
<%@ Register TagPrefix="inthudo" TagName="MemberAdd" Src="~/Modules/MemberAdd.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <inthudo:MemberAdd runat="server" ID="ctrlMemberAdd" />
</asp:Content>
