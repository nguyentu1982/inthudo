<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequests.ascx.cs" Inherits="Web.Modules.DesignRequests" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DatePicker.ascx" TagName="DatePicker" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/CustomerSelect.ascx" TagName="CustomerSelect" %>

<h1>Quản lý yêu cầu thiết kế</h1>
<div class="search-input">
    <span class="lbtitle">Từ ngày</span>
    <inthudo:DatePicker runat="server" ID="ctrlDatePickerFrom" Format="dd/MM/yyyy" />
    <span class="lbtitle">Đến ngày</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerTo" Format="dd/MM/yyyy" />
    <inthudo:CustomerSelect runat="server" ID="ctrlCustomerSelect" CustomerTypeCode="KH" />
    <span class="lbtitle">Sản phẩm</span><asp:DropDownList runat="server" ID="ddlProducts"></asp:DropDownList>
    <br />
    <span class="lbtitle">Trạng thái</span><asp:DropDownList runat="server" ID="ddlDesignRequestStatus">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem Value="1">Yêu cầu mới</asp:ListItem>
        <asp:ListItem Value="2">Đang thiết kế</asp:ListItem>
        <asp:ListItem Value="3">Thiết kế xong</asp:ListItem>
        <asp:ListItem Value="4">Khách đã duyệt</asp:ListItem>
        <asp:ListItem Value="5">Khách KHÔNG duyệt</asp:ListItem>
    </asp:DropDownList>
    <br />
    <span class="lbtitle">NV Thiết kế</span><asp:DropDownList runat="server" ID="ddlDesigner"></asp:DropDownList>

    <div class="buttons">
        <asp:Button runat="server" ID="btFind" Text="Tìm" OnClick="btFind_Click" />
    </div>
</div>
<div class="total-result">
    <span class="lbtitle-total">Tổng số yêu cầu</span><asp:Label runat="server" ID="lbTotalRequest" CssClass="total"></asp:Label>
    <h3>Trong đó</h3>
    <span class="lbtitle-total">Yêu cầu mới</span><asp:Label runat="server" ID="lbTotalDesignRequestCreated" CssClass="total"></asp:Label>
    <br />
    <span class="lbtitle-total">Đang thiết kế</span><asp:Label runat="server" ID="lbTotalDesignRequestDesigning" CssClass="total"></asp:Label>
    <br />
    <span class="lbtitle-total">Đã thiết kế xong</span><asp:Label runat="server" ID="lbTotalDesignRequestWaitForApproved" CssClass="total"></asp:Label>
    <br />
    <span class="lbtitle-total">Khách đã duyệt mẫu</span><asp:Label runat="server" ID="lbTotalDesignRequestApproved" CssClass="total"></asp:Label>
    <br />
    <span class="lbtitle-total">Khách KHÔNG duyệt mẫu</span><asp:Label runat="server" ID="lbTotalDesignRequestNOTApproved" CssClass="total"></asp:Label>
    <br />
</div>
<div class="clear"></div>
<div class="design-request-grid">
    <asp:GridView ID="grvDesignRequest" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvDesignRequest_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="DesignRequestId" />
            <asp:BoundField HeaderText="Đơn hàng" DataField="OrderDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField HeaderText="Sản phẩm" DataField="ProductName" />
            <asp:BoundField HeaderText="Yêu cầu" DataField="Description" HtmlEncode="False" />
            <asp:BoundField HeaderText="Trạng thái" DataField="DesignRequestStatusString" />
            <asp:BoundField HeaderText="Nhận việc" DataField="BeginDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField HeaderText="Hoàn thành" DataField="EndDate" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <span>Xem</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hlDesignRequest" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<script type="text/javascript">
    function OpenWindow(url) {
        var w = 600;
        var h = screen.height - 100;
        var left = (screen.width / 2) - (w / 2);
        window.open(url, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=10, left=" + left + " width=" + w + ", height=" + h + "");
    }
</script>
