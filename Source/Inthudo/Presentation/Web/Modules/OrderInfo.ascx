<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.ascx.cs" Inherits="Web.Modules.OrderInfo" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailInfo" Src="~/Modules/OrderDetailInfo.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="CustomerSelect" Src="~/Modules/CustomerSelect.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx"  %>
<%@ Register TagPrefix="inthudo" TagName="DatePicker" Src="~/Modules/DatePicker.ascx" %>

<div runat="server" id="plOrderId">
    <span class="lbtitle">Mã đơn hàng:</span>
    <asp:Label ID="lbOrderId" runat="server"></asp:Label>
</div>
<span class="lbtitle">Ngày:</span>

<inthudo:DatePicker runat="server" ID="ctrlDatePicker"  Format="dd/MM/yyyy"></inthudo:DatePicker>
<span class="lbtitle">Tình trạng ĐH:</span><asp:DropDownList runat="server" ID="ddlOrderStatus"></asp:DropDownList>

<br />
<inthudo:CustomerSelect runat="server" ID="ctrlCustomerSelect" />
<br />
<span class="lbtitle">Đặt cọc</span><asp:DropDownList ID="ddlDepositMethod" runat="server"></asp:DropDownList>
<span class="lbtitle">Số tiền:</span><inthudo:DecimalTextBox runat="server" ID="ctrlDepositAmount" Value="0" RequiredErrorMessage="Bạn phải nhập số tiền đặt cọc!" MinimumValue="0" MaximumValue="1000000000" RangeErrorMessage="Số tiền đặt cọc từ 0 đến 1.000.000.000!" /> [VNĐ]
<br />
<span class="lbtitle">Phương thức giao hàng: </span><asp:DropDownList runat="server" ID="ddlShippingMethod"></asp:DropDownList>
<span class="lbtitle">Nhân viên KD:</span><asp:DropDownList runat="server" ID="ddlBusinessManId" ></asp:DropDownList>
<br />

<h3>Nội dung chi tiết đơn đặt hàng</h3>
<br />
<div id="panelOrderDetails" runat="server">
    <asp:GridView ID="grvOrderDetails" runat="server" AutoGenerateColumns="false" OnRowCommand="grvOrderDetails_RowCommand" >
        <Columns>
            <%--<asp:TemplateField>
                <HeaderTemplate>
                     <input type="checkbox" ID="cbSelectAll" onclick="CheckAll(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="cbOrderDetail"/>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField HeaderText="ID" DataField="OrderItemId" />
            <asp:BoundField HeaderText="Sản phẩm" DataField="ProductName" />
            <asp:BoundField HeaderText="Quy cách" DataField="Specification" />
            <asp:BoundField HeaderText="Số lượng" DataField="Quantity" />
            <asp:BoundField HeaderText="Đơn giá" DataField="Price" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label runat="server" Text="Sửa" ID="lbEditOrderDetail"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   
                    <a onclick="OpenOrderDetailEditWindow(<%#Eval("OrderItemId")%>); return false" href="">Sửa</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btDeleteOrderDetail" runat="server" Text="Xóa" CommandName="DeleteOrderDetail" OnClientClick="return confirmDelete()" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
</div>

<div runat="server" id="panelOrderDetailAdd">
    <inthudo:OrderDetailInfo runat="server" ID ="ctrlOrderDetailInfo" ActionButtonIsDisplay="false" />
</div>
<div runat="server" id="panelOrderDetailAddButton">
    <asp:Button runat="server" ID="btAddNewOrderDetail" Text="Tạo nội dung chi tiết đơn đặt hàng mới" OnClientClick="OpenOrderDetailAddWindow(); return false" />
</div>

<script type="text/javascript">
    function OpenOrderDetailAddWindow()
    {
        var orderId = getParameterByName("OrderId");
        
        window.open("/OrderDetailAdd.aspx?OrderId=" + orderId + "&AddNew=1", 800, 600, true);
        
    }

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    function OpenOrderDetailEditWindow(orderDetailId) {
        //var orderDetailId = getParameterByName("OrderDetailId");

        window.open("/OrderDetailEdit.aspx?OrderDetailId=" + orderDetailId, 800, 600, true);

    }

    function confirmDelete() {
        if (confirm("Bạn có chắc chắn xóa?") == true)
            return true;
        else
            return false;
    }
</script>