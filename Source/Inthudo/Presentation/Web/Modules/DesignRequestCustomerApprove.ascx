<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestCustomerApprove.ascx.cs" Inherits="Web.Modules.DesignRequestCustomerApprove" %>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div>
            <asp:CheckBoxList ID="cblApprovedByCustomer" runat="server" RepeatColumns="2">
                <asp:ListItem Value="1">Khách hàng đã duyệt mẫu TK</asp:ListItem>
                <asp:ListItem Value="0">Khách hàng không duyệt mẫu thiết kế</asp:ListItem>
            </asp:CheckBoxList>
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

    function radioMe(e) {
        if (!e) e = window.event;
        var sender = e.target || e.srcElement;

        if (sender.nodeName != 'INPUT') return;
        var checker = sender;
        var chkBox = document.getElementById('<%= cblApprovedByCustomer.ClientID %>');
        var chks = chkBox.getElementsByTagName('INPUT');
        for (i = 0; i < chks.length; i++) {
            if (chks[i] != checker)
                chks[i].checked = false;
        }
    }
</script>

