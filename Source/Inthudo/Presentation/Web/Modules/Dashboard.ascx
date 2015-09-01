<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="Web.Modules.Dashboard" %>
<%@ Register TagPrefix="inthuo" Src="~/Modules/DatePicker.ascx" TagName="DatePicker" %>
<%@ Reference Control="~/Modules/BussinessReportByUser.ascx" %>

<span class="lbtitle">Từ ngày</span><inthuo:DatePicker runat="server" ID="ctrlDateFrom" />
<span class="lbtitle">Đến ngày</span><inthuo:DatePicker runat="server" ID="ctrlDateTo" />
<br />
<asp:DropDownList runat="server" ID="ddlCompany"></asp:DropDownList>
<br />
<asp:Button runat="server" ID="btFind" Text="Xem"  OnClick="btFind_Click"/>
<div runat="server" id="panelBusiness"></div>
