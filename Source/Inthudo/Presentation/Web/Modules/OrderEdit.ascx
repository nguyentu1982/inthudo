﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderEdit.ascx.cs" Inherits="Web.Modules.OrderEdit" %>
<%@ Register TagPrefix="inthudo" TagName="OrderInfo" Src="~/Modules/OrderInfo.ascx" %>
<h1>Sửa đơn hàng</h1>
<div class="">
    <asp:Button ID="btSave" runat="server" Text="Lưu" />
    <asp:Button ID="btSaveAndContinueEdit" runat="server" Text="Lưu và tiếp tục sửa" />
    <a href="Orders.ascx" class="a-button">Trở lại danh sách đơn hàng</a>    
</div>

<div>
    <inthudo:OrderInfo ID="ctrlOrderInfo" runat="server" />
</div>
