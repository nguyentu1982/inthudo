<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="DesignRequestEdit.aspx.cs" Inherits="Web.DesignRequestEdit" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DesignRequestEdit.ascx" TagName="DesignRequestEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <inthudo:DesignRequestEdit runat="server" ID="ctrlDesignRequestEdit" />
</asp:Content>
