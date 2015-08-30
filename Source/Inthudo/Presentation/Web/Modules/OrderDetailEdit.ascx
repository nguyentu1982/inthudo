<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailEdit.ascx.cs" Inherits="Web.Modules.OrderDetailEdit" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailInfo" Src="~/Modules/OrderDetailInfo.ascx" %>

<h1>Sửa nội dung chi tiết đơn hàng</h1>
<asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" ValidationGroup="val"/>
<asp:Button ID="btCancel" runat="server" Text="Hủy" OnClientClick="window.close(); return false" OnClick="btCancel_Click"/>
<asp:Button ID="btDelete" runat="server" Text="Xóa" OnClick="btDelete_Click" OnClientClick="return confirmDelete()" />

<inthudo:OrderDetailInfo runat="server" ID="ctrlOrderDetailInfo" />
