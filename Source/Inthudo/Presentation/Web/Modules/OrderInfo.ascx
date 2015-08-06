<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.ascx.cs" Inherits="Web.Modules.OrderInfo" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailInfo" Src="~/Modules/OrderDetailInfo.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>

<div runat="server" id="plOrderId">
    <span class="lbtitle">Mã đơn hàng:</span>
    <asp:Label ID="lbOrderId" runat="server"></asp:Label>
</div>
<span class="lbtitle">Ngày:</span>
<asp:TextBox runat="server" ID="txtOrderDate"></asp:TextBox>

<ajaxToolkit:CalendarExtender ID="txtOrderDate_CalendarExtender" runat="server" TargetControlID="txtOrderDate" />
<span class="lbtitle">Tình trạng ĐH:</span><asp:DropDownList runat="server" ID="ddlOrderStatus"></asp:DropDownList>

<br />
<span class="lbtitle">Khách hàng:</span> 
<br />
<span class="lbtitle">Đặt cọc</span><asp:DropDownList ID="ddlDepositMethod" runat="server"></asp:DropDownList>
<span class="lbtitle">Số tiền:</span><inthudo:DecimalTextBox runat="server" ID="ctrlDepositAmount"/>
<br />
<span class="lbtitle">Nhân viên KD:</span><asp:DropDownList runat="server" ID="ddlBusinessManId" ></asp:DropDownList>
<span class="lbtitle">Nhân viên TK:</span><asp:DropDownList runat ="server" ID="ddlDesignerId"></asp:DropDownList>
<br />

<h3>Nội dung chi tiết đơn đặt hàng</h3>
<br />
<div id="panelOrderDetails" runat="server">
    <asp:GridView ID="grvOrderDetails" runat="server"></asp:GridView>
</div>

<div runat="server" id="panelOrderDetailAdd">
    <inthudo:OrderDetailInfo runat="server" ID ="ctrlOrderDetailInfo" />
</div>