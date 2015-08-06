﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderAdd.ascx.cs" Inherits="Web.Modules.OrderAdd" %>
<%@ Register TagPrefix="inthudo" TagName="OrderInfo" Src="~/Modules/OrderInfo.ascx" %>
<h1>Tạo đơn hàng</h1>
<div class="">
    <asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" />
    <asp:Button ID="btSaveAndContinueEdit" runat="server" Text="Lưu và tiếp tục sửa" OnClick="btSaveAndContinueEdit_Click" />
    <a href="Orders.ascx" class="a-button">Trở lại danh sách đơn hàng</a>    
</div>

<div>
    <inthudo:OrderInfo ID="ctrlOrderInfo" runat="server" />
</div>