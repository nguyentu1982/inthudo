﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.ascx.cs" Inherits="Web.Modules.OrderInfo" %>
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
<inthudo:CustomerSelect runat="server" ID="ctrlCustomerSelect" CustomerTypeCode="KH" />
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
        OnRowDataBound="grvOrderDetails_RowDataBound" CellPadding="15" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
            <asp:BoundField HeaderText="Đơn giá" DataField="Price"  DataFormatString="{0:C0}"  />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label runat="server" Text="Đơn hàng" ID="lbEditOrderDetail"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <a onclick="OpenOrderDetailEditWindow(<%#Eval("OrderItemId")%>); return false" class="a-popup">Xem</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Xóa</HeaderTemplate>
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
            <asp:BoundField HeaderText="Trạng thái" DataField="OrderItemStatusString" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</div>


<div runat="server" id="panelOrderDetailAdd">
    <inthudo:OrderDetailInfo runat="server" ID="ctrlOrderDetailInfo" ActionButtonIsDisplay="false" />
</div>
<div runat="server" id="panelOrderDetailAddButton" style="float:left">
    <asp:Button runat="server" ID="btAddNewOrderDetail" Text="Tạo nội dung chi tiết đơn đặt hàng mới" OnClientClick="OpenOrderDetailAddWindow(); return false" />
</div>
<div runat="server" id="panelOrderDetailAddButtonReProduce" Visible="false">
    <asp:Button runat="server" ID="btAddNewOrderDetailReproduce" Text="Tạo nội dung làm lại cho sản phẩm lỗi" OnClientClick="OpenOrderDetailAddWindowReproduce(); return false"  />
</div>

<div class="clear"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="panelOrderSumary" runat="server">
    <h3>Tổng kết đơn hàng</h3>
    <span class="lbtitle">Giá trị đơn hàng </span>
    <asp:Label ID="lbOrderTotal" runat="server"  ></asp:Label>
    <br />
    <span class="lbtitle">Thuế GTGT </span>
    <asp:Label ID="lbVAT" runat="server"></asp:Label><inthudo:DecimalTextBox runat="server" ID="ctrlDecimalTextBoxVAT" Value="0" RequiredErrorMessage="Bạn hãy nhập thuế VAT" MinimumValue="0" MaximumValue="100000000" RangeErrorMessage="Thuế VAT từ 0 đến 100.000.000" AutoPostBack="true" OnTextChanged="ctrlDecimalTextBoxVAT_TextChanged" />
    [VNĐ]
    <br />
    <span class="lbtitle">Tổng</span><asp:Label ID="lbOrderTotalIncludeVAT" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">Đặt cọc</span><asp:DropDownList ID="ddlDepositMethod" runat="server"></asp:DropDownList>
    <span class="lbtitle">Số tiền:</span><inthudo:DecimalTextBox runat="server" ID="ctrlDepositAmount" Value="0" RequiredErrorMessage="Bạn phải nhập số tiền đặt cọc!" MinimumValue="0" MaximumValue="1000000000" RangeErrorMessage="Số tiền đặt cọc từ 0 đến 1.000.000.000!" AutoPostBack="true" OnTextChanged="ctrlDepositAmount_TextChanged" />
    [VNĐ]
    <br />
    <span class="lbtitle">Còn lại </span><asp:Label runat="server" ID="lbRemaining"></asp:Label>
            <br />
    <span class="lbtitle">Ngày hẹn trả</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerEstimatedComplteDate" Format="dd/MM/yyyy"></inthudo:DatePicker>
    <span class="lbtitle">Ghi chú</span><asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine"></asp:TextBox>
</div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ctrlDecimalTextBoxVAT" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="ctrlDepositAmount" EventName="TextChanged" />
    </Triggers>
</asp:UpdatePanel>

<div class="product-approved-sumary" runat="server" id="panelProductApprovedSummary" visible="false">
    <h3>Chi tiết duyệt đơn hàng</h3>
    <asp:GridView runat="server" ID="grvApprovedProducts" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="grvApprovedProducts_RowDataBound" ShowFooter="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
        <asp:BoundField HeaderText="Sản phẩm" DataField="ProductName" />
        <asp:BoundField HeaderText="Số lượng" DataField="Quantity" />
        <asp:BoundField HeaderText="Đơn giá" DataField="Price"  DataFormatString="{0:C0}"  />
        <asp:TemplateField>
            <HeaderTemplate>
                Thành tiền
            </HeaderTemplate>
            <FooterTemplate>
                <asp:Label runat="server" ID="lbTotalFooter" ></asp:Label>
            </FooterTemplate>
            <ItemTemplate>
                <asp:Label runat="server" ID="lbSubTotal" Text='<%# string.Format("{0:C0}", Eval("Total")) %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField> 
        </Columns>        
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <span class="lbtitle-product-approve">Đặt cọc</span><asp:Label runat="server" ID="lbDepositAmount"></asp:Label>
    <br />
    <span class="lbtitle-product-approve">Còn lại</span><asp:Label runat="server" ID="lbRemainAmount"></asp:Label>
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

    function OpenOrderDetailAddWindowReproduce() {
        var orderId = getParameterByName("OrderId");
        var w = 600;
        var left = (screen.width / 2) - (w / 2);
        var h = screen.height - 100;
        window.open("/OrderDetailAdd.aspx?OrderId=" + orderId + "&AddNew=1&Reproduce=1", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=10, left=" + left + " width=" + w + ", height=" + h + "");

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
