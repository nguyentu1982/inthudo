<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSearch.ascx.cs" Inherits="Web.Modules.CustomerSearch" %>

<asp:UpdatePanel ID="updatePanel2" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btFind" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="grvCustomers" EventName="RowCommand" />
    </Triggers>
    <ContentTemplate>
        <span class="lbtitle">Tên khách hàng: </span>
        <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
        <br />
        <span class="lbtitle">Số điện thoại: </span>
        <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
        <br />
        <span class="lbtitle">Email: </span>
        <asp:TextBox ID="txtEmail" runat="server" />
        <br />
        <span class="lbtitle">Tên công ty:</span><asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btFind" runat="server" Text="Tìm" OnClick="btFind_Click" />
        <input type="button" value="Thêm khách hàng mới" onclick="OpenWindow('CustomerAdd.aspx')" runat="server" id="btAddNew" />
        <br />
        <asp:GridView ID="grvCustomers" runat="server" AutoGenerateColumns="False" OnRowCommand="grvCustomers_RowCommand" OnRowDataBound="grvCustomers_RowDataBound">
           
            <Columns>
                
                <asp:BoundField DataField="CustomerId" HeaderText="ID" />
                <asp:TemplateField HeaderText="Tên">
                    <EditItemTemplate>
                        <asp:Label ID="TextBox2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Điện thoại">
                    <EditItemTemplate>
                        <asp:Label ID="TextBox3" runat="server" Text='<%# Bind("Telephone") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Telephone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <EditItemTemplate>
                        <asp:Label ID="TextBox4" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Địa chỉ">
                    <EditItemTemplate>
                        <asp:Label ID="TextBox5" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Công ty">
                    <EditItemTemplate>
                        <asp:Label ID="TextBox6" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkButtonChose" runat="server" CausesValidation="false" CommandName="ChoseCustomer" Text="Chọn" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span>Sửa</span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HyperLink ID="hlEidtCustomer" runat="server"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function OpenWindow(url) {
        var w = 600;
        var h = screen.height - 100;
        var left = (screen.width / 2) - (w / 2);
        window.open(url, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=10, left=" + left + " width=" + w + ", height=" + h + "");
    }
</script>