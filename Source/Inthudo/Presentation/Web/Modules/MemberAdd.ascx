<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberAdd.ascx.cs" Inherits="Web.Modules.MemberAdd" %>
<%@ Register TagPrefix="inthudo" TagName="MemberInfo"  Src="~/Modules/MemberInfo.ascx"%>
<div class="page-title">
    Tạo User mới</div><a href="Members.aspx">Trở lại danh sách User</a>

<p>
    <asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" />
    <asp:Button ID="btSaveAndContinueEdit" runat="server" Text="Lưu và tiếp tục sửa" OnClick="btSaveAndContinueEdit_Click" />
    <a href="/Members.aspx" >Trở lại danh sách user</a>
</p>

<div>
    <inthudo:MemberInfo runat="server" ID="ctrlMemberInfo" />    
</div>

