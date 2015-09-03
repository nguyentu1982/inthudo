<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="Web.Modules.Dashboard" %>
<%@ Register TagPrefix="inthuo" Src="~/Modules/DatePicker.ascx" TagName="DatePicker" %>
<%@ Reference Control="~/Modules/BussinessReportByUser.ascx" %>

<span class="lbtitle">Từ ngày</span><inthuo:DatePicker runat="server" ID="ctrlDateFrom" Format="dd/MM/yyyy" />
<span class="lbtitle">Đến ngày</span><inthuo:DatePicker runat="server" ID="ctrlDateTo" Format="dd/MM/yyyy" />
<br />
<span class="lbtitle">Công ty</span><asp:DropDownList runat="server" ID="ddlCompany"></asp:DropDownList>
<br />
<asp:Button runat="server" ID="btFind" Text="Xem"  OnClick="btFind_Click"/>
<div>
    <h3>Kinh doanh</h3>
    <div runat="server" id="panelBusiness"></div>
</div>
<div>
    <h3>Thiết kế</h3>
    <div runat="server" id="panelDesignRequest"></div>
</div>
