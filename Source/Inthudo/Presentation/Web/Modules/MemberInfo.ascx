<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberInfo.ascx.cs" Inherits="Web.Modules.MemberInfo" %>
<%@ Register TagPrefix="inthudo" TagName="EmailTextBox" Src="~/Modules/EmailTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx" %>


<div runat="server" id="pnlUserId">
<span class="lbtitle">UserId: </span>
<asp:Label ID="lbUserId" runat="server"></asp:Label>
</div>
<br />

<span class="lbtitle">Tên đăng nhập: </span>

<inthudo:SimpleTextBox runat="server" ID="txtUserName" ErrorMessage="Bạn phải nhập tên đăng nhập." />
<br />
<span class="lbtitle">Password: </span>
<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><asp:Button runat="server" ID="btChangePass" Text="Đổi Password" OnClick="btChangePass_Click" OnClientClick="return confirm('Bạn có chắc chắn đổi pass không?')"/>
<br />
<span class="lbtitle">Họ tên: </span>
<inthudo:SimpleTextBox ID="txtFullName" runat="server" ErrorMessage="Bạn phải nhập họ tên."/>
<br />
<span class="lbtitle">Địa chỉ: </span>
<asp:TextBox ID="txtAdress" runat="server"></asp:TextBox>
<br />
<span class="lbtitle">Telephone: </span>
<asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
<br />
<span class="lbtitle">Email: </span>
<inthudo:EmailTextBox runat="server" ID="txtEmail" />
<%--<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
<br />--%>
<br />
<span class="lbtitle">Quyền: </span>
<asp:DropDownList ID="ddlRoleType" runat="server"></asp:DropDownList>