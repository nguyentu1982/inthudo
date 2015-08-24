<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.ascx.cs" Inherits="Web.Modules.OrderInfo" %>
<%@ Register TagPrefix="inthudo" TagName="OrderDetailInfo" Src="~/Modules/OrderDetailInfo.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="CustomerSelect" Src="~/Modules/CustomerSelect.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DatePicker" Src="~/Modules/DatePicker.ascx" %>

<div runat="server" id="plOrderId">
    <span class="lbtitle">Mã đơn hàng:</span>
    <asp:Label ID="lbOrderId" runat="server"></asp:Label>
</div>
<span class="lbtitle">Ngày:</span><inthudo:DatePicker runat="server" ID="ctrlDatePicker" Format="dd/MM/yyyy"></inthudo:DatePicker>
<span class="lbtitle">Nhân viên KD:</span><asp:DropDownList runat="server" ID="ddlBusinessManId"></asp:DropDownList>
<br />
<span class="lbtitle">Tình trạng ĐH:</span><asp:Label runat="server" ID="lbOrderStatus"></asp:Label>
<br />
<inthudo:CustomerSelect runat="server" ID="ctrlCustomerSelect" />
<br />

<span class="lbtitle">Phương thức giao hàng: </span>
<asp:DropDownList runat="server" ID="ddlShippingMethod"></asp:DropDownList>
<br />
<div id="delivery-address">
    <span class="lbtitle">Địa chỉ giao hàng: </span>
    <asp:TextBox runat="server" ID="txtDeliveryAddress" TextMode="MultiLine"></asp:TextBox>
</div>

<br />

<h3>Nội dung chi tiết đơn đặt hàng</h3>
<br />
<div id="panelOrderDetails" runat="server">
    <asp:GridView ID="grvOrderDetails" runat="server"
        AutoGenerateColumns="False" OnRowCommand="grvOrderDetails_RowCommand"
        OnRowDataBound="grvOrderDetails_RowDataBound" CellPadding="10">
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
            <asp:BoundField HeaderText="Quy cách" DataField="Specification" HtmlEncode="False" />
            <asp:BoundField HeaderText="Số lượng" DataField="Quantity" />
            <asp:BoundField HeaderText="Đơn giá" DataField="Price" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label runat="server" Text="Sửa" ID="lbEditOrderDetail"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <a onclick="OpenOrderDetailEditWindow(<%#Eval("OrderItemId")%>); return false" class="a-popup">Sửa</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btDeleteOrderDetail" runat="server" Text="Xóa" CommandName="DeleteOrderDetail" OnClientClick="return confirmDelete()" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="lbDesignRequest" Text="Thiết Kế" runat="server"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hlDesignRequest" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <span>Sản Xuất</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hlManufactureRequest" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</div>


<div runat="server" id="panelOrderDetailAdd">
    <inthudo:OrderDetailInfo runat="server" ID="ctrlOrderDetailInfo" ActionButtonIsDisplay="false" />
</div>
<div runat="server" id="panelOrderDetailAddButton">
    <asp:Button runat="server" ID="btAddNewOrderDetail" Text="Tạo nội dung chi tiết đơn đặt hàng mới" OnClientClick="OpenOrderDetailAddWindow(); return false" />
</div>


<div id="panelOrderSumary" runat="server">
    <h3></h3>
    <span class="lbtitle">Giá trị đơn hàng </span>
    <asp:Label ID="lbOrderTotal" runat="server"></asp:Label>[VNĐ]
    <br />
    <span class="lbtitle">Thuế GTGT </span>
    <asp:Label ID="lbVAT" runat="server"></asp:Label><inthudo:DecimalTextBox runat="server" ID="ctrlDecimalTextBoxVAT" Value="0" RequiredErrorMessage="Bạn hãy nhập thuế VAT" MinimumValue="0" MaximumValue="100000000" RangeErrorMessage="Thuế VAT từ 0 đến 100.000.000" />
    [VNĐ]
    <br />
    <span class="lbtitle">Tổng</span><asp:Label ID="lbOrderTotalIncludeVAT" runat="server"></asp:Label>[VNĐ]
    <br />
    <span class="lbtitle">Đặt cọc</span><asp:DropDownList ID="ddlDepositMethod" runat="server"></asp:DropDownList>
    <span class="lbtitle">Số tiền:</span><inthudo:DecimalTextBox runat="server" ID="ctrlDepositAmount" Value="0" RequiredErrorMessage="Bạn phải nhập số tiền đặt cọc!" MinimumValue="0" MaximumValue="1000000000" RangeErrorMessage="Số tiền đặt cọc từ 0 đến 1.000.000.000!" />
    [VNĐ]
    <br />
    <span class="lbtitle">Ngày hẹn trả</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerEstimatedComplteDate" Format="dd/MM/yyyy"></inthudo:DatePicker>

</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#<% =ctrlDecimalTextBoxVAT.ClientID %>_txtValue").on('onchange change keyup paste', function () {
            var total = $("#<%=lbOrderTotal.ClientID%>").val;
            var vat = $("#<%=ctrlDecimalTextBoxVAT.ClientID%>").val
            $("#<%= lbOrderTotalIncludeVAT.ClientID%>").val = total + vat;
        })


        $("#<%=ddlShippingMethod.ClientID%>").on('change onchange', function () {
            var deliveryMethodId = this.value;
            if (deliveryMethodId == 1) {
                $("#delivery-address").css("display", "block");
            }

            if (deliveryMethodId == 2) {
                $("#delivery-address").css("display", "none");
            }
        })
    })

    function OpenOrderDetailAddWindow() {
        var orderId = getParameterByName("OrderId");
        var w = 600;
        var left = (screen.width / 2) - (w / 2);
        var h = screen.height - 100;
        window.open("/OrderDetailAdd.aspx?OrderId=" + orderId + "&AddNew=1", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=10, left=" + left + " width=" + w + ", height=" + h + "");

    }

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    function OpenOrderDetailEditWindow(orderDetailId) {
        var orderId = getParameterByName("OrderId");
        var w = 600;
        var h = screen.height - 100;
        var left = (screen.width / 2) - (w / 2);
        window.open("/OrderDetailEdit.aspx?OrderId=" + orderId + "&OrderDetailId=" + orderDetailId, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=10, left=" + left + " width=" + w + ", height=" + h + "");

    }

    function confirmDelete() {
        if (confirm("Bạn có chắc chắn xóa?") == true)
            return true;
        else
            return false;
    }

    function OpenWindow(url) {
        var w = 600;
        var h = screen.height - 100;
        var left = (screen.width / 2) - (w / 2);
        window.open(url, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=10, left=" + left + " width=" + w + ", height=" + h + "");
    }

</script>
