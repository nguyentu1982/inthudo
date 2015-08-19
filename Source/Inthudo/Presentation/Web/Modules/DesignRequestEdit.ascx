<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestEdit.ascx.cs" Inherits="Web.Modules.DesignRequestEdit" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DesignRequestInfo.ascx" TagName="DesignRequestInfo" %>

<h1>Sửa yêu cầu thiết kế</h1>
    
    <inthudo:DesignRequestInfo ID="ctrlDesignRequestInfo" runat="server" />
<asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" ValidationGroup="val" />
    <asp:Button ID="btDelete" runat="server" Text="Xóa" OnClientClick="return confirmDelete()" OnClick="btDelete_Click" />



