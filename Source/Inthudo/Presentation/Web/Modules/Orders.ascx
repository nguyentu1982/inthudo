<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Orders.ascx.cs" Inherits="Web.Modules.Orders" %>
<h1>Quản lý đơn hàng</h1>
<span class="lbtitle">Mã đơn hàng: </span> <asp:TextBox ID="txtOrderCode" runat="server"></asp:TextBox>
<br />
<span class="lbtitle">Khách hàng: </span> <asp:TextBox ID="txtCustomerCode" runat="server"></asp:TextBox>
<br />
<span class="lbtitle">Sản phẩm: </span><asp:DropDownList ID="ddlProduct" runat="server"></asp:DropDownList>
<br />
<span class="lbtitle">Vận chuyển: </span><asp:DropDownList ID="dllShipping" runat="server"></asp:DropDownList>
<br />
<span class="lbtitle">Đặt cọc: </span><asp:DropDownList ID="dllDeposit" runat="server" ></asp:DropDownList>
<br />
<span class="lbtitle">Trạng thái: </span><asp:DropDownList ID="dllStatus" runat="server"></asp:DropDownList>
<br />
<span class="lbtitle">Nhân viên KD:</span> <asp:DropDownList ID="ddlBusinessManId" runat="server"></asp:DropDownList>
<br />
 <span class="lbtitle" >Nhân viên TK:</span> <asp:DropDownList ID="ddlDesingerId" runat="server"></asp:DropDownList>
<br />
<asp:Button runat="server" ID="btFind" Text="Tìm"/><asp:Button ID="btAdd" runat="server" Text="Tạo đơn hàng" /><asp:Button ID="btDelete" runat="server" Text="Xóa đơn hàng đã chọn" />
<br />
<div class="orders-grid">
    <asp:GridView ID="grvOrders" runat="server">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                     <input type="checkbox" ID="cbSelectAll" onclick="CheckAll(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="cbMember"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<script type="text/javascript">
    function CheckAll(oCheckbox) {
        var grvMembers = document.getElementById("<%=grvOrders.ClientID %>");
        for (i = 1; i < grvMembers.rows.length; i++) {
            grvMembers.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
        }
    }
</script>