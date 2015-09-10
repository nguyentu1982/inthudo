<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manufactures.ascx.cs" Inherits="Web.Modules.Manufactures" %>
<%@ Register TagPrefix="inthudo" TagName="DatePicker" Src="~/Modules/DatePicker.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="CustomerSelect" Src="~/Modules/CustomerSelect.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="PrintingTypeSelect" Src="~/Modules/PrintingTypeSelect.ascx" %>

<span class="lbtitle">Từ ngày</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerRequestFrom" Format="dd/MM/yyyy" />
<span class="lbtitle">Đến ngày</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerRequestTo" Format="dd/MM/yyyy"/>
<span class="lbtitle">Mã đơn hàng</span><asp:TextBox runat="server" ID="txtOrderId"></asp:TextBox>
<br />
<inthudo:CustomerSelect runat="server" ID="ctrlCustomerSelect" CustomerTypeCode="KH" />
<br/>
<span class="lbtitle">Sản phẩm</span><asp:DropDownList runat="server" ID="ddlProducts"></asp:DropDownList>
<br />
<inthudo:CustomerSelect runat="server" ID="ctrlManufactureSelect" CustomerTypeCode="DVSX" />
<inthudo:PrintingTypeSelect runat="server" ID="ctrlPrintingTypeSelect" />
<br />
<span class="lbtitle">Trạng thái sản xuất</span><asp:DropDownList runat="server" ID="ddlManufactureStatus"></asp:DropDownList>
<br />
<span class="lbtitle">NVKD</span><asp:DropDownList runat="server" ID="ddlBusinessMan"></asp:DropDownList>
<br />
<span class="lbtitle">NV Thiết kế</span><asp:DropDownList runat="server" ID="ddlDesigners"></asp:DropDownList>
<br />
<asp:Button runat="server" ID="btFind" OnClick="btFind_Click" Text="Tìm" />

<asp:GridView runat="server" ID="grvManufactureRequest" AutoGenerateColumns="false" OnRowDataBound="grvManufactureRequest_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:HiddenField runat="server" ID="hdfManufactureRequestId" Value='<%#Bind("ManufactureRequestId") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Mã ĐH" DataField="OrderId" />
        <asp:BoundField HeaderText="Sản phẩm" DataField="ProductName" />
        <asp:BoundField HeaderText="Quy cách" DataField="Description" />
        <asp:BoundField HeaderText="Số lượng" DataField="Quantity" />
        <asp:BoundField HeaderText="Giá" DataField="Cost" DataFormatString="{0:C0}" />
        <asp:BoundField HeaderText="Hình thức" DataField="PrintingTypeName" />
        <asp:BoundField HeaderText="Nơi SX" DataField="ManufactureName" />
        <asp:BoundField HeaderText="Nhận việc" DataField="BeginDate" DataFormatString="{0:dd-MM-yyyy}" />
        <asp:BoundField HeaderText="Hoàn thành" DataField="EndDate" DataFormatString="{0:dd-MM-yyyy}" />
        <asp:BoundField HeaderText="Trạng thái" DataField="ManufactureRequestStatusString" />
        <asp:TemplateField>
                <HeaderTemplate>
                    <span>Xem</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hlManufactureRequest" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>
</asp:GridView>


<script type="text/javascript">
    function OpenWindow(url) {
        var w = 600;
        var h = screen.height - 100;
        var left = (screen.width / 2) - (w / 2);
        window.open(url, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=10, left=" + left + " width=" + w + ", height=" + h + "");
    }
</script>