<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestInfo.ascx.cs" Inherits="Web.Modules.DesignRequestInfo" %>
<%@ Register TagPrefix="inthudo" TagName="CustomerSelect" Src="~/Modules/CustomerSelect.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DatePicker" Src="~/Modules/DatePicker.ascx"  %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>
<div class="orderInfo">
    <span class="lbtitle">Mã đơn hàng: </span><asp:Label runat="server" ID="lbOrderId"></asp:Label>
    <br />
    <div runat="server" id="panelDesignRequestId">
        <span class="lbtitle">Mã nội dung đơn hàng: </span><asp:Label ID="lbOrderDetailId" runat="server"></asp:Label>
    </div>   
    
    <inthudo:CustomerSelect runat="server" ID="ctrlCustomerSelect" Enable="false" />
    <span class="lbtitle">NVKD: </span><asp:DropDownList runat="server" ID="ddlBussinessMan"></asp:DropDownList>
</div>

<div class="design-request">
    <span class="lbtitle">NV thiết kế: </span><asp:DropDownList runat="server" ID="ddlDesigner"></asp:DropDownList>
    <br />
    <span class="lbtitle">Ngày nhận việc: </span><inthudo:DatePicker runat="server" ID="ctrlDatePickerFrom" Format="dd/MM/yyyy" />
    <span class="lbtitle">Ngày hoàn thành: </span><inthudo:DatePicker runat="server" ID="ctrlDatePickerTo" Format="dd/MM/yyyy" />
    <h3>Nội dung yêu cầu thiết kế:</h3>
    <asp:TextBox ID="txtDesignRequirement" runat="server" Width="600" Height="300"></asp:TextBox>
    <ajaxToolkit:HtmlEditorExtender ID="txtDesignRequirement_HtmlEditorExtender" runat="server" BehaviorID="txtDesignRequirement_HtmlEditorExtender" TargetControlID="txtDesignRequirement" EnableSanitization ="false">
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