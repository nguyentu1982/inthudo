<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberEdit.ascx.cs" Inherits="Web.Modules.MemberEdit" %>
<%@ Register TagPrefix="inthudo" TagName="MemberInfo" Src="~/Modules/MemberInfo.ascx" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit"%> 
<div class="page-title">
    Thay đổi thông tin User</div>
<p>
    <asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" />
    <asp:Button ID="btSaveAndContinueEdit" runat="server" Text="Lưu và tiếp tục sửa" OnClick="btSaveAndContinueEdit_Click" />
    <asp:Button ID="btDelete" runat="server" Text="Xóa" OnClick="btDelete_Click" />
    
    <ajaxToolkit:ConfirmButtonExtender ID="btDelete_ConfirmButtonExtender" runat="server" BehaviorID="btDelete_ConfirmButtonExtender" ConfirmText="Bạn có chắc chắn xóa không?" TargetControlID="btDelete" />
    
</p>
<div>
    <inthudo:MemberInfo runat="server" ID="ctrlMemberInfo" />
    
</div>


