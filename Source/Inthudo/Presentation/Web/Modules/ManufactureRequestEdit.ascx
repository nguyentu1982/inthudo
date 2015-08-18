<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManufactureRequestEdit.ascx.cs" Inherits="Web.Modules.ManufactureRequestEdit" %>
<%@ Register TagPrefix="inthudo" TagName="ManufactureRequestInfo" Src="~/Modules/ManufactureRequestInfo.ascx" %>
<h1>Sửa yêu cầu sản xuất</h1>
<inthudo:ManufactureRequestInfo runat="server" ID="ctrlManufactureRequestInfo" />
<asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" /><asp:Button ID="btDelete" runat="server" Text="Xóa" OnClientClick="confirmDelete()" OnClick="btDelete_Click" />


