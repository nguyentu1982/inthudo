﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailInfo.ascx.cs" Inherits="Web.Modules.OrderDetailInfo" %>
<%@ Register TagPrefix="inthudo" TagName="NumericTextBox" Src="~/Modules/NumericTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>

<div runat="server" id="panelOrderDetailId" class="order-detail-id">
    <span class="lbtitle">Mã nội dung đơn hàng: </span>
    <asp:Label ID="lbOrderDetailID" runat="server"></asp:Label>
</div>
<div class="order-info" id="panelOrderInfo" runat="server">
    <span class="lbtitle">Mã đơn hàng: </span>
    <asp:Label runat="server" ID="lbOrderId"></asp:Label>
    <br />
    <span class="lbtitle">Khách hàng: </span>
    <asp:Label ID="lbCustomer" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">NVKD: </span>
    <asp:Label ID="lbBusinessMan" runat="server"></asp:Label>
</div>

<div class="order-detail">
    <span class="lbtitle">Sản phẩm </span>
    <ajaxToolkit:ComboBox ID="cboxProduct" runat="server" ValidationGroup="val"></ajaxToolkit:ComboBox>
    <asp:RequiredFieldValidator ID="requiredFieldValidatorProduct" runat="server" ControlToValidate="cboxProduct" ErrorMessage="Bạn hãy nhập sản phẩm" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
    <br />
    <span class="lbtitle">Quy cách sản phẩm</span>
    <div>
        <asp:TextBox ID="txtProductRequirement" runat="server" Width="600" Height="300" ValidationGroup="val"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredFieldValidatorProductRequirement" runat="server" ErrorMessage="Bạn hãy nhập quy cách sản phẩm!" ControlToValidate="txtProductRequirement" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
        <ajaxToolkit:HtmlEditorExtender ID="txtProductRequirement_HtmlEditorExtender" runat="server" BehaviorID="txtProductRequirement_HtmlEditorExtender" TargetControlID="txtProductRequirement" EnableSanitization="false">
        </ajaxToolkit:HtmlEditorExtender>
    </div>
    <span class="lbtitle">Số lượng: </span>
    <inthudo:NumericTextBox runat="server" ID="ctrltxtQuantity" Value="1" MaximumValue="1000000" MinimumValue="1" RequiredErrorMessage="Bạn hãy nhập số lượng!" RangeErrorMessage="Số lượng từ 1 đến 1.000.000" />
    <br />
    <span class="lbtitle">Đơn giá: </span>
    <inthudo:DecimalTextBox runat="server" ID="ctrltxtPrice" Value="0" MaximumValue="1000000000" MinimumValue="0" RequiredErrorMessage="Bạn hãy nhập đơn giá!" RangeErrorMessage="Đơn giá từ 0 đến 1.000.000.000" />
    <br />
    <span class="lbtitle">Thành tiền:</span> <span id="total-money"></span>
</div>

<script type="text/javascript">
    function RefreshParent() {
        if (window.opener != null && !window.opener.closed) {
            window.opener.location.reload();
        }
    }
    window.onunload = RefreshParent;




</script>
