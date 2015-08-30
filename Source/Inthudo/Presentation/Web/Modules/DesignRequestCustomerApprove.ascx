<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestCustomerApprove.ascx.cs" Inherits="Web.Modules.DesignRequestCustomerApprove" %>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div>
            <span class="lbtitle">Khách hàng đã duyệt mẫu TK</span><asp:CheckBox ID="cbApprovedByCustomer" runat="server" />
            <br />
            <span class="lbtitle">Ghi chú</span>
            <asp:TextBox ID="txtApprovedByCustomerNote" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Button runat="server" ID="btSave" OnClick="btSave_Click" Text="Lưu" OnClientClick="return confirmChangeCustomerApproveStatus()" />

<script type="text/javascript">
    function confirmChangeCustomerApproveStatus() {
        if (confirm("Bạn có chắc chắn?") == true)
            return true;
        else
            return false;
    }
</script>

