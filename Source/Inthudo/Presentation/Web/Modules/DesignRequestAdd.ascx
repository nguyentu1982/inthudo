<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestAdd.ascx.cs" Inherits="Web.Modules.DesignRequestAdd" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DesignRequestInfo.ascx" TagName="DesignRequestInfo" %>

<h1>Tạo yêu cầu thiết kế</h1>
    <asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click"  />
    <input type="button" id="btCancel" value="Hủy" onclick="window.close()" />
    <inthudo:DesignRequestInfo ID="ctrlDesignRequestInfo" runat="server" />
