<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManufactureRequestInfo.ascx.cs" Inherits="Web.Modules.ManufactureRequestInfo" %>
<%@ Register TagPrefix="inthudo" TagName="NumericTextBox" Src="~/Modules/NumericTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DatePicker" Src="~/Modules/DatePicker.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>

<div runat="server" id="panelManufactureRequestID" class="manufacture-request-id">
    <span class="lbtitle">Mã yêu cầu sản xuất </span><asp:Label ID="lbManufactureRequestId" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">Ngày tạo yêu cầu: </span><asp:Label ID="lbManufactureRequestDate" runat="server"></asp:Label>
</div>

<div class="order-info">
    <span class="lbtitle">Mã đơn hàng: </span><asp:Label ID="lbOrderId" runat="server"></asp:Label>    
    <br />
    <span class="lbtitle">Khách hàng: </span><asp:Label ID="lbCustomer" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">NVKD: </span><asp:Label ID="lbBusinessMan" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">Mã nội dung: </span><asp:Label ID="lbOrderDetailId" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">Mã yêu cầu thiết kế: </span><asp:Label ID="lbDesignRequestId" runat="server"></asp:Label>    
</div>
<div class="manufacture-request">
    <span class="lbtitle">Ngày bắt đầu</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerBeginDate" Format="dd/MM/yyyy" />
    <br />
    <span class="lbtitle">Ngày hoàn thành</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerEndDate" Format="dd/MM/yyyy" />
    <h3>Quy cách sản phẩm</h3>
    <asp:TextBox runat="server" ID="txtRequirement" Width="600" Height="300" ></asp:TextBox>
    <ajaxToolkit:HtmlEditorExtender ID="txtRequirement_HtmlEditorExtender" runat="server" BehaviorID="txtRequirement_HtmlEditorExtender" TargetControlID="txtRequirement" EnableSanitization="false">
    </ajaxToolkit:HtmlEditorExtender>
    <br />
    <span class="lbtitle">Số lượng</span><inthudo:NumericTextBox runat="server" ID="ctrlNumbericTextBoxQuantity" Value="1" MinimumValue="0" MaximumValue="9999999" RequiredErrorMessage="Bạn hãy nhập số lượng" RangeErrorMessage="Số lượng từ 1 đến 9.999.999" />
    <br />
    <span class="lbtitle">Đơn giá</span><inthudo:DecimalTextBox runat="server" ID="ctrlDecimalTextBoxCost" Value="0" MinimumValue="0" MaximumValue="1000000000" RequiredErrorMessage="Bạn hãy nhập đơn giá" RangeErrorMessage="Đơn giá từ 0 đến 1.000.000.000" />
</div>

<script type="text/javascript">
    function confirmDeleteManufactureRequest() {
        if (confirm("Bạn có chắc chắn xóa?") == true)
            return true;
        else
            return false;
    }
</script>

<script type="text/javascript">
    function RefreshParent() {
        if (window.opener != null && !window.opener.closed) {
            window.opener.location.reload();
        }
    }
    window.onunload = RefreshParent;
</script>