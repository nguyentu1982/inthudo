<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManufactureCustomerApprove.ascx.cs" Inherits="Web.Modules.ManufactureCustomerApprove" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DatePicker.ascx" TagName="DatePicker" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="customer-approve">
            <span class="lbtitle">Khách hàng duyệt sản phẩm</span><asp:CheckBox ID="cbCustomerApprove" runat="server" />
            <div runat="server" id="panelApprovedDate">
                 <span class="lbtitle">Ngày duyệt</span><inthudo:DatePicker ID="ctrlDatePickerCustomerApproveDate" runat="server" />
            </div>
            <br />
            <span class="lbtitle">Ghi chú</span><asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button runat="server" ID="btSave" OnClientClick="AreYouSure()" OnClick="btSave_Click" Text="Lưu" />
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btSave" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    function AreYouSure() {
        if (confirm("Bạn có chắc chắn?") == true)
            return true;
        else
            return false;
    }
</script>