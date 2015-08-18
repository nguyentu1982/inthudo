<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailEdit.ascx.cs" Inherits="Web.Modules.OrderDetailEdit" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailInfo" Src="~/Modules/OrderDetailInfo.ascx" %>

<h1>Sửa nội dung chi tiết đơn hàng</h1>
<inthudo:OrderDetailInfo runat="server" ID="ctrlOrderDetailInfo" ActionButtonIsDisplay="true" />