﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Orders.ascx.cs" Inherits="Web.Modules.Orders" %>
<%@ Register TagPrefix="inthudo" TagName="CustomerSelect" Src="~/Modules/CustomerSelect.ascx" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DatePicker.ascx" TagName="DatePicker" %>


<h1>Quản lý đơn hàng</h1>
<div class="search-input" style="float:left">
    <span class="lbtitle">Từ ngày</span>
    <inthudo:DatePicker runat="server" ID="ctrlDatePickerFrom" Format="dd/MM/yyyy" />
    <span class="lbtitle">Đến ngày</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerTo" Format="dd/MM/yyyy" />
    <span class="lbtitle">Mã đơn hàng: </span>
    <asp:TextBox ID="txtOrderCode" runat="server"></asp:TextBox>
    <br />
    <inthudo:CustomerSelect runat="server" ID="ctrlCustomerSelect" />
    <br />
    <span class="lbtitle">Sản phẩm: </span>
    <asp:DropDownList ID="ddlProduct" runat="server"></asp:DropDownList>
    <br />
    <span class="lbtitle">Vận chuyển: </span>
    <asp:DropDownList ID="ddlShipping" runat="server"></asp:DropDownList>
    <br />
    <span class="lbtitle">Đặt cọc: </span>
    <asp:DropDownList ID="ddlDeposit" runat="server"></asp:DropDownList>
    <br />
    <span class="lbtitle">Trạng thái nội dung ĐH: </span>
    <asp:DropDownList ID="ddlOrderDetailStatus" runat="server"></asp:DropDownList>
    <br />
    <span class="lbtitle">Trạng thái đơn hàng: </span>
    <asp:CheckBoxList ID="cblOrderStatus" runat="server" RepeatColumns="3" CssClass="order-status">
        <asp:ListItem Value="0">Tất cả</asp:ListItem>
        <asp:ListItem Value="1">Chưa hoàn thành</asp:ListItem>
        <asp:ListItem Value="2">Đã hoàn thành</asp:ListItem>
    </asp:CheckBoxList>
    <br />
    <span class="lbtitle">Nhân viên KD:</span>
    <asp:DropDownList ID="ddlBusinessManId" runat="server"></asp:DropDownList>
    <br />
    <span class="lbtitle">Nhân viên TK:</span>
    <asp:DropDownList ID="ddlDesingerId" runat="server"></asp:DropDownList>
    <br />
</div>
<div class="total-result" style="float:right;min-width:600px">
    <span class="lbtitle">Số đơn hàng: </span><asp:Label runat="server" ID="lbNumberOfOrders"></asp:Label>
    <br />
    <span class="lbtitle">Doanh số: </span><asp:Label runat="server" ID="lbOrderTotal"></asp:Label>
</div>
<div class="buttons">
    <asp:Button runat="server" ID="btFind" Text="Tìm" OnClick="btFind_Click" />
<asp:Button ID="btAdd" runat="server" Text="Tạo đơn hàng" OnClick="btAdd_Click" />
<asp:Button ID="btDelete" runat="server" Text="Xóa đơn hàng đã chọn" OnClick="btDelete_Click" OnClientClick="return confirmDelete()" />
<br />
</div>

<div class="orders-grid">
    <asp:GridView ID="grvOrders" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" id="cbSelectAll" onclick="CheckAll(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="cbMember" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="OrderId" />
            <asp:BoundField HeaderText="Ngày" DataField="OrderDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField HeaderText="Khách hàng" DataField="CustomerName" />
            <asp:BoundField HeaderText="NVKD" DataField="BusinessManName" />
            <asp:BoundField HeaderText="PT Đặt cọc" DataField="DepositTypeName" />
            <asp:BoundField HeaderText="Đặt cọc" DataField="Deposit" DataFormatString="{0:#,0} VNĐ" />
            <asp:BoundField HeaderText="Giao hàng" DataField="ShippingMethodName" />
            <asp:BoundField HeaderText="Giá trị ĐH" DataField="OrderTotal" DataFormatString="{0:#,0} VNĐ" />
            <asp:BoundField HeaderText="Hẹn trả" DataField="ExpectedCompleteDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField HeaderText="Tình trạng ĐH" DataField="OrderStatusString" />
            <asp:HyperLinkField HeaderText="Sửa" DataNavigateUrlFields="OrderId" DataNavigateUrlFormatString="~/OrderEdit.aspx?OrderId={0}" Text="Sửa" />
        </Columns>
    </asp:GridView>
</div>
<script type="text/javascript">
    function CheckAll(oCheckbox) {
        var grvMembers = document.getElementById("<%=grvOrders.ClientID %>");
        for (i = 1; i < grvMembers.rows.length; i++) {
            grvMembers.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
        }
    }

    function confirmDelete() {
        if (confirm("Bạn có chắc chắn xóa?") == true)
            return true;
        else
            return false;
    }

    function radioMe(e) {
        if (!e) e = window.event;
        var sender = e.target || e.srcElement;

        if (sender.nodeName != 'INPUT') return;
        var checker = sender;
        var chkBox = document.getElementById('<%= cblOrderStatus.ClientID %>');
        var chks = chkBox.getElementsByTagName('INPUT');
        for (i = 0; i < chks.length; i++) {
            if (chks[i] != checker)
                chks[i].checked = false;
        }
    }
</script>
