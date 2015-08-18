<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="DesignRequestAdd.aspx.cs" Inherits="Web.DesignRequestAdd" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DesignRequestAdd.ascx" TagName="DesignRequestAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:DesignRequestAdd ID="ctrlDesignRequestInfo" runat="server" />
</asp:Content>
