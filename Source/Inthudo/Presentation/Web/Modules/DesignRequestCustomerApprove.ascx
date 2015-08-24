<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestCustomerApprove.ascx.cs" Inherits="Web.Modules.DesignRequestCustomerApprove" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <span class="lbtitle">Khách hàng đã duyệt mẫu TK</span><asp:CheckBox ID="cbApprovedByCustomer" runat="server" />
            <br />
            <span class="lbtitle">Ghi chú</span>
            <asp:TextBox ID="txtApprovedByCustomerNote" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button runat="server" ID="btSave" OnClick="btSave_Click" Text="Lưu" />
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btSave" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div>Đang xử lý yêu cầu...</div>
    </ProgressTemplate>
</asp:UpdateProgress>

