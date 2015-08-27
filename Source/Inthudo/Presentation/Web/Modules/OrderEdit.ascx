<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderEdit.ascx.cs" Inherits="Web.Modules.OrderEdit" %>
<%@ Register TagPrefix="inthudo" TagName="OrderInfo" Src="~/Modules/OrderInfo.ascx" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/OrderCustomerApprove.ascx" TagName="OrderCustomerApprove" %>
<h1>Sửa đơn hàng</h1>
<div class="">
    <asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" />
    <asp:Button ID="btDelete" runat="server" Text="Xóa" OnClientClick="return confirmDelete()" OnClick="btDelete_Click"  />
    <a href="Orders.aspx" class="a-button">Trở lại danh sách đơn hàng</a>    
</div>
<ajaxToolkit:TabContainer ID="OrderTab" runat="server">
    <ajaxToolkit:TabPanel runat="server" ID="pnlOrderInfo" HeaderText="Thông tin đơn hàng">
        <ContentTemplate>
            <inthudo:OrderInfo ID="ctrlOrderInfo" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>

    <ajaxToolkit:TabPanel runat="server" ID="pnlOrderCustomerApprove" HeaderText="Khách hàng duyệt đơn hàng"  Visible="false">
        <ContentTemplate>
            <inthudo:OrderCustomerApprove runat="server" ID="ctrlOrderCustomerApprove"></inthudo:OrderCustomerApprove>
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>


<script type="text/javascript">
    function confirmDelete() {
        if (confirm("Bạn có chắc chắn xóa?") == true)
            return true;
        else
            return false;
    }
</script>