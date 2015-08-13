<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.ascx.cs" Inherits="Web.Modules.OrderInfo" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailInfo" Src="~/Modules/OrderDetailInfo.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="CustomerSelect" Src="~/Modules/CustomerSelect.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx"  %>
<%@ Register TagPrefix="inthudo" TagName="DatePicker" Src="~/Modules/DatePicker.ascx" %>

<div runat="server" id="plOrderId">
    <span class="lbtitle">Mã đơn hàng:</span>
    <asp:Label ID="lbOrderId" runat="server"></asp:Label>
</div>
<span class="lbtitle">Ngày:</span>

<inthudo:DatePicker runat="server" ID="ctrlDatePicker"></inthudo:DatePicker>
<span class="lbtitle">Tình trạng ĐH:</span><asp:DropDownList runat="server" ID="ddlOrderStatus"></asp:DropDownList>

<br />
<inthudo:CustomerSelect runat="server" ID="ctrlCustomerSelect" />
<br />
<span class="lbtitle">Đặt cọc</span><asp:DropDownList ID="ddlDepositMethod" runat="server"></asp:DropDownList>
<span class="lbtitle">Số tiền:</span><inthudo:DecimalTextBox runat="server" ID="ctrlDepositAmount" Value="0" RequiredErrorMessage="Bạn phải nhập số tiền đặt cọc!" MinimumValue="0" MaximumValue="1000000000" RangeErrorMessage="Số tiền đặt cọc từ 0 đến 1.000.000.000!" /> [VNĐ]
<br />
<span class="lbtitle">Phương thức giao hàng: </span><asp:DropDownList runat="server" ID="ddlShippingMethod"></asp:DropDownList>
<span class="lbtitle">Nhân viên KD:</span><asp:DropDownList runat="server" ID="ddlBusinessManId" ></asp:DropDownList>
<br />

<h3>Nội dung chi tiết đơn đặt hàng</h3>
<br />
<div id="panelOrderDetails" runat="server">
    <asp:GridView ID="grvOrderDetails" runat="server"></asp:GridView>
</div>

<div runat="server" id="panelOrderDetailAdd">
    <inthudo:OrderDetailInfo runat="server" ID ="ctrlOrderDetailInfo" />
</div>