<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderCustomerApprove.ascx.cs" Inherits="Web.Modules.OrderCustomerApprove" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <span class="lbtitle">Khách hàng đã duyệt đơn hàng: </span><asp:CheckBox runat="server" ID="cbOrderApproved" />
        <br />
        <span class="lbtitle">Ghi chú: </span> <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button runat="server" ID="btSave" Text="Lưu" OnClick="btSave_Click" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btSave" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
