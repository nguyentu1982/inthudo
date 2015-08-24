<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Members.ascx.cs" Inherits="Web.Modules.Members" %>
<h1 class="page-title">
    QUẢN LÝ USER
</h1>
<div>
    <span class="lbtitle">Tên đăng nhập:</span> <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    <br/>
     <span class="lbtitle">Email:</span> <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <br/>
    <span class="lbtitle">Tên:</span> <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
    <br/>
    <span class="lbtitle">Số điện thoại:</span>  <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
    <br />
    <span class="lbtitle">Quyền: </span>
    <asp:DropDownList ID="ddlRoles" runat="server">
    </asp:DropDownList>
    <br />
    <span class="lbtitle">Phòng: </span>
    <asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>
    <br />
    <span class="lbtitle">Trực thuộc công ty:</span>
    <asp:DropDownList ID="ddlOrganization" runat="server"></asp:DropDownList>
</div>

<div>
    <asp:Button runat="server" ID="btFindMember" Text="Tìm" OnClick="btFindMember_Click"/>
    <asp:Button runat="server" ID="btAddMember" Text="Tạo User" OnClick="btAddMember_Click"/>
     <asp:Button runat="server" ID="btDeleteUser" Text="Xóa User" OnClick="btDeleteUser_Click" OnClientClick=""/>
    <ajaxToolkit:ConfirmButtonExtender ID="btDeleteUser_ConfirmButtonExtender" runat="server" TargetControlID="btDeleteUser" ConfirmText="Bạn có chắc chắn xóa User đã chọn?" />
</div>

<div>
    <asp:GridView ID="grvMembers" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                     <input type="checkbox" ID="cbSelectAll" onclick="CheckAll(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="cbMember"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Id" DataField="UserId" Visible="true" />
            <asp:BoundField HeaderText="Tên đăng nhập" DataField="UserName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField HeaderText="Họ Tên" DataField="FullName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField HeaderText="Số Điện Thoại" DataField="Telephone" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Quyền" DataField="RoleName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
            <asp:HyperLinkField HeaderText="Sửa" DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="~/MemberEdit.aspx?MemberId={0}" Text="Sửa"/>
            
        </Columns>
    </asp:GridView>
</div>
<script type="text/javascript">
    function CheckAll(oCheckbox)
     {
        var grvMembers = document.getElementById("<%=grvMembers.ClientID %>");
        for (i = 1; i < grvMembers.rows.length; i++)
         {
             grvMembers.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
          }
    }
</script>