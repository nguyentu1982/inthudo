<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailAdd.ascx.cs" Inherits="Web.Modules.OrderDetailAdd" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailInfo" Src="~/Modules/OrderDetailInfo.ascx" %>

<h1>Tạo nội dung chi tiết đơn hàng</h1>
<inthudo:OrderDetailInfo runat="server" ID="ctrlOrderDetailInfo" ActionButtonIsDisplay="true" />