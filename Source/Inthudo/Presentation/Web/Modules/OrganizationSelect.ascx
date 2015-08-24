<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationSelect.ascx.cs" Inherits="Web.Modules.OrganizationSelect" %>

<h3>Trực thuộc công ty</h3>
<asp:GridView runat="server" ID="grvUserOrganiztion" AutoGenerateColumns="false" OnRowCommand="grvUserOrganiztion_RowCommand">
    <Columns>
        <asp:BoundField HeaderText="ID" DataField="OrganizationId"/>
        <asp:BoundField HeaderText="Tên công ty" DataField="Name" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btDeleteUserOrganizationMap" runat="server" Text="Xóa" CommandName="DeleteUserOrganizationMap" OnClientClick="return confirmDelete()" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>
<asp:Label ID="lbUserNotInAnyOrganization" runat="server" Text="User chưa trực thuộc công ty nào, bạn hãy chọn công ty sau đó nhấn THÊM"></asp:Label>
<span class="lbtitle">Công ty:</span><asp:DropDownList ID="ddlOrgainzation" runat="server"></asp:DropDownList>
<asp:Button ID="btAddNewOrganization" runat="server" Text="Thêm" OnClick="btAddNewOrganization_Click" />


<script type="text/javascript">
    function CheckAll(oCheckbox) {
        var grvMembers = document.getElementById("<%=grvUserOrganiztion.ClientID %>");
        for (i = 1; i < grvMembers.rows.length; i++) {
            grvMembers.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
        }
    }

    function confirmDelete() {
        if (confirm("Bạn có chắc chắn xóa?") == true)
            return true;
        else
            return false;
    }
</script>


