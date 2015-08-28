<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManufactureCustomerApprove.ascx.cs" Inherits="Web.Modules.ManufactureCustomerApprove" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DatePicker.ascx" TagName="DatePicker" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/NumericTextBox.ascx" TagName="NumericTextBox" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DecimalTextBox.ascx" TagName="DecimalTextBox" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <span class="lbtitle">Khách hàng duyệt sản phẩm</span><asp:CheckBox ID="cbCustomerApprove" runat="server" AutoPostBack="true" OnCheckedChanged="cbCustomerApprove_CheckedChanged" />
        <span class="lbtitle">Khách hàng từ chối sản phẩm</span><asp:CheckBox ID="cbCustomerRefuse" runat="server" AutoPostBack="true" OnCheckedChanged="cbCustomerRefuese_CheckedChanged" />
        <div runat="server" id="panelApprovedDate">
            <span class="lbtitle">Ngày duyệt</span><inthudo:DatePicker ID="ctrlDatePickerCustomerApproveDate" runat="server" />
        </div>
        <br />
        <div runat="server" id="panelApproveDetail">
            <span class="lbtitle">Số lượng duyệt</span><inthudo:NumericTextBox runat="server" ID="ctrlNumericTextBoxQuantity" Value="0" MinimumValue="0" MaximumValue="1000000" RequiredErrorMessage="Bạn hãy nhập số lượng KH duyệt" RangeErrorMessage="Số lượng từ 0 đến 1.000.000" />
            <br />
            <span class="lbtitle">Giá KH duyệt:</span><inthudo:DecimalTextBox runat="server" ID="ctrlDecimalTextBoxPrice" Value="0" MinimumValue="0" MaximumValue="10000000000" RequiredErrorMessage="Bạn hãy nhập giá" RangeErrorMessage="Giá từ 0 đến 1.000.000.000" AutoPostBack="false" />

        </div>
        <br />
        <span class="lbtitle">Ghi chú</span><asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine"></asp:TextBox>
        <br />


    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cbCustomerRefuse" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="cbCustomerApprove" EventName="CheckedChanged" />
    </Triggers>
</asp:UpdatePanel>
<asp:Button runat="server" ID="btSave" OnClientClick="AreYouSure()" OnClick="btSave_Click" Text="Lưu" />

<script type="text/javascript">
    function AreYouSure() {
        if (confirm("Bạn có chắc chắn?") == true)
            return true;
        else
            return false;
    }

    
</script>


