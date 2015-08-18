<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderEdit.ascx.cs" Inherits="Web.Modules.OrderEdit" %>
<%@ Register TagPrefix="inthudo" TagName="OrderInfo" Src="~/Modules/OrderInfo.ascx" %>
<h1>Sửa đơn hàng</h1>
<div class="">
    <asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" />
    <asp:Button ID="btDelete" runat="server" Text="Xóa" OnClientClick="return confirmDelete()" OnClick="btDelete_Click"  />
    <a href="Orders.aspx" class="a-button">Trở lại danh sách đơn hàng</a>    
</div>

<div>
    <inthudo:OrderInfo ID="ctrlOrderInfo" runat="server" />
</div>

<script type="text/javascript">
    function confirmDelete() {
        if (confirm("Bạn có chắc chắn xóa?") == true)
            return true;
        else
            return false;
    }
</script>