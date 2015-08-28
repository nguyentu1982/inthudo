<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManufactureRequestEdit.ascx.cs" Inherits="Web.Modules.ManufactureRequestEdit" %>
<%@ Register TagPrefix="inthudo" TagName="ManufactureRequestInfo" Src="~/Modules/ManufactureRequestInfo.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="ManufactureCustomerApprove" Src="~/Modules/ManufactureCustomerApprove.ascx" %>
<h1>Sửa yêu cầu sản xuất</h1>
<asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" /><asp:Button ID="btDelete" runat="server" Text="Xóa" OnClientClick="return confirmDeleteManufactureRequest()" OnClick="btDelete_Click" />
<ajaxToolkit:TabContainer ID="ManufactureTabContainer" runat="server" ActiveTabIndex="0">
    <ajaxToolkit:TabPanel runat="server" ID="pnlManufactureInfo" HeaderText="Yêu cầu sản xuất">
        <ContentTemplate>
            <inthudo:ManufactureRequestInfo runat="server" ID="ctrlManufactureRequestInfo" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>

    <ajaxToolkit:TabPanel runat="server" ID="pnlManufactureCustomerApprove" HeaderText="Khách hàng duyệt sản phẩm">
        <ContentTemplate>
            <inthudo:ManufactureCustomerApprove runat="server" ID="ctrlManufactureCustomerApprove" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>


