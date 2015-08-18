<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManufactureRequestAdd.ascx.cs" Inherits="Web.Modules.ManufactureRequestAdd" %>
<%@ Register TagPrefix="inthudo" TagName="ManufactureRequestInfo" Src="~/Modules/ManufactureRequestInfo.ascx" %>
<h1>Tạo yêu cầu sản xuất</h1>
<inthudo:ManufactureRequestInfo runat="server" ID="ctrlManufactureRequestInfo" />
<asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" /><input type="button" value="Hủy"/>
