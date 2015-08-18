<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailInfo.ascx.cs" Inherits="Web.Modules.OrderDetailInfo" %>
<%@ Register TagPrefix="inthudo" TagName="NumericTextBox" Src="~/Modules/NumericTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx"%>

<div runat="server" id="panelOrderDetailId">
    <asp:HiddenField runat="server" ID="hdfOrderDetailId" />
</div>
<div runat="server" id="panelOrderInfo">
    <span class="lbtitle">Mã đơn hàng: </span>
    <asp:Label runat="server" ID="lbOrderId"></asp:Label>
    <br />
   
</div>
 <span class="lbtitle" >Nhân viên thiết kế:</span><asp:DropDownList runat="server" ID="ddlDesigner"></asp:DropDownList>
<br />
<span class="lbtitle">Sản phẩm </span>
<ajaxToolkit:ComboBox ID="cboxProduct" runat="server"></ajaxToolkit:ComboBox>
<br />
<span class="lbtitle">Quy cách sản phẩm</span>
<div>
    <asp:TextBox ID="txtProductRequirement" runat="server" Width="600" Height="300"></asp:TextBox>
<ajaxToolkit:HtmlEditorExtender ID="txtProductRequirement_HtmlEditorExtender" runat="server" BehaviorID="txtProductRequirement_HtmlEditorExtender" TargetControlID="txtProductRequirement" EnableSanitization="false">
</ajaxToolkit:HtmlEditorExtender>
</div>
<span class="lbtitle">Số lượng: </span><inthudo:NumericTextBox runat="server" ID="ctrltxtQuantity" Value="1" MaximumValue="1000000" MinimumValue="1"  RequiredErrorMessage="Bạn hãy nhập số lượng!" RangeErrorMessage="Số lượng từ 1 đến 1.000.000"/>
<br />
<span class="lbtitle">Đơn giá: </span><inthudo:DecimalTextBox runat="server" ID="ctrltxtPrice" Value="0" MaximumValue="1000000000" MinimumValue="0"  RequiredErrorMessage="Bạn hãy nhập đơn giá!" RangeErrorMessage="Đơn giá từ 0 đến 1.000.000.000" />
<br />
<span class="lbtitle">Thành tiền:</span> <span id="total-money"></span>
<asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" />
<asp:Button ID="btCancel" runat="server" Text="Hủy" OnClientClick="window.close(); return false"/>
<asp:Button ID="btDelete" runat="server" Text="Xóa" OnClick="btDelete_Click" OnClientClick="return confirmDelete()" />

<script type="text/javascript">
    function RefreshParent() {
        if (window.opener != null && !window.opener.closed) {
            window.opener.location.reload();
        }
    }
    window.onunload = RefreshParent;
</script>