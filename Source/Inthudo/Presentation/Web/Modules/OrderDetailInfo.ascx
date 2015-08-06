<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailInfo.ascx.cs" Inherits="Web.Modules.OrderDetailInfo" %>
<%@ Register TagPrefix="inthudo" TagName="NumericTextBox" Src="~/Modules/NumericTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx"%>

<div runat="server" id="panelOrderDetailId">
    <asp:HiddenField runat="server" ID="hdfOrderDetailId" />
</div>
<div runat="server" id="panelOrderInfo">
    <span class="lbtitle">Mã đơn hàng: </span>
    <asp:Label runat="server" ID="lbOrderId"></asp:Label>
</div>
<span class="lbtitle">Sản phẩm </span><asp:DropDownList runat="server" ID="ddlProduct"></asp:DropDownList>
<br />
<span class="lbtitle">Quy cách sản phẩm</span>
<div>
    <asp:TextBox ID="txtProductRequirement" runat="server" Width="600" Height="400"></asp:TextBox>
<ajaxToolkit:HtmlEditorExtender ID="txtProductRequirement_HtmlEditorExtender" runat="server" BehaviorID="txtProductRequirement_HtmlEditorExtender" TargetControlID="txtProductRequirement" EnableSanitization="false">
</ajaxToolkit:HtmlEditorExtender>
</div>
<span class="lbtitle">Số lượng: </span><inthudo:NumericTextBox runat="server" ID="ctrltxtQuantity" />
<br />
<span class="lbtitle">Đơn giá: </span><inthudo:DecimalTextBox runat="server" ID="ctrltxtPrice" />
<br />
<span class="lbtitle">Thành tiền:</span> <span id="total-money"></span>
