<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestInfo.ascx.cs" Inherits="Web.Modules.DesignRequestInfo" %>
<%@ Register TagPrefix="inthudo" TagName="CustomerSelect" Src="~/Modules/CustomerSelect.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DatePicker" Src="~/Modules/DatePicker.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>

<div runat="server" id="panelDesignRequestId" class="design-request-id">
    <span class="lbtitle">Mã yêu cầu thiết kế: </span>
    <asp:Label ID="lbDesignRequestId" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">Ngày tạo yêu cầu:</span><asp:Label ID="lbDesignRequestDate" runat="server"></asp:Label>
</div>

<div class="order-info">
    <span class="lbtitle">Mã đơn hàng: </span>
    <asp:Label runat="server" ID="lbOrderId"></asp:Label>
    <br />
    <span class="lbtitle">Khách hàng: </span>
    <asp:Label ID="lbCustomer" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">NVKD: </span>
    <asp:Label ID="lbBusinessMan" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">Mã nội dung đơn hàng: </span>
    <asp:Label ID="lbOrderDetailId" runat="server"></asp:Label>
</div>

<div class="design-request">
    <span class="lbtitle">NV thiết kế: </span>
    <asp:DropDownList runat="server" ID="ddlDesigner" ValidationGroup="val"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="requiredFieldValidatorDesigner" runat="server" ErrorMessage="Bạn hãy chọn NV Thiết kế" ControlToValidate="ddlDesigner" ValidationGroup="val" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <span class="lbtitle">Ngày nhận việc: </span>
    <inthudo:DatePicker runat="server" ID="ctrlDatePickerFrom" Format="dd/MM/yyyy" Visible="false" />
    <span class="lbtitle">Ngày hoàn thành: </span>
    <inthudo:DatePicker runat="server" ID="ctrlDatePickerTo" Format="dd/MM/yyyy" Visible="false" />
    <h3>Nội dung yêu cầu thiết kế:</h3>
    <asp:TextBox ID="txtDesignRequirement" runat="server" Width="600" Height="300"></asp:TextBox>
    <ajaxToolkit:HtmlEditorExtender ID="txtDesignRequirement_HtmlEditorExtender" runat="server" BehaviorID="txtDesignRequirement_HtmlEditorExtender" TargetControlID="txtDesignRequirement" EnableSanitization="false">
    </ajaxToolkit:HtmlEditorExtender>
    <br />
    <span class="lbtitle">Chi phí thiết kế:</span><inthudo:DecimalTextBox runat="server" ID="ctrlDecimalTextBoxDesignCost" MinimumValue="0" MaximumValue="1000000000" Value="0" RangeErrorMessage="Chi phí thiết kế từ 0 đến 1.000.000.000" RequiredErrorMessage="Bạn hãy nhập chi phí thiết kế!" />
</div>


<script type="text/javascript">
    function confirmDelete() {
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
