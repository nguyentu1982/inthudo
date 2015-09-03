<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BussinessReportByUser.ascx.cs" Inherits="Web.Modules.BussinessReportByUser" %>
<table>
    <thead>
        <tr>
            <td colspan="9">
                <span><strong>Tên NVKD: </strong></span><strong><asp:Label runat="server" ID="lbUserName"></asp:Label>
                </strong>
                <br />
                <span><strong>Mã NV - Account: </strong></span><strong><asp:Label runat="server" ID="lbUserId"></asp:Label>
                </strong>
            </td>

        </tr>

    </thead>
    <tr>
        <td>
            <span class="lbtitle-total-dashboard">Số đơn hàng chưa hoàn thành</span>
        </td>
        <td>
            <asp:Label runat="server" ID="lbNumberOfOrderNOTCompleted" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Doanh số</span>
        </td>
        <td>
            <asp:Label runat="server" ID="lbOrderTotalNOTCompleted" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Đặt cọc</span></td>
        <td>
            <asp:Label runat="server" ID="lbDepositNOTCompleted" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Còn lại</span></td>
        <td>
            <asp:Label runat="server" ID="lbRemainingNOTCompleted" CssClass="total"></asp:Label>
        </td>
        <td>
            <asp:HyperLink runat="server" ID="hplViewDetailNotCompleted">Xem</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>

            <span class="lbtitle-total-dashboard">Số đơn hàng hoàn thành</span></td>
        <td>
            <asp:Label runat="server" ID="lbNumberOfOrderCompleted" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Doanh số hợp đồng</span></td>
        <td>
            <asp:Label runat="server" ID="lbOrderTotalContractCompleted" CssClass="total"></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            <span class="lbtitle-total-dashboard">Doanh số khách duyệt</span></td>
        <td>
            <asp:Label runat="server" ID="lbOrderTotalCompleted" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Đặt cọc</span></td>
        <td>
            <asp:Label runat="server" ID="lbDepositCompleted" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Còn lại</span></td>
        <td>
            <asp:Label runat="server" ID="lbRemainingCompleted" CssClass="total"></asp:Label>
        </td>
        <td>
            <asp:HyperLink runat="server" ID="hplViewDetailCompleted">Xem</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>

            <span class="lbtitle-total-dashboard">Số đơn hàng có lỗi</span></td>
        <td>
            <asp:Label runat="server" ID="lbNumberOfOrderFailed" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Doanh số</span></td>
        <td>
            <asp:Label runat="server" ID="lbOrderTotalFailded" CssClass="total"></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            <span class="lbtitle-total-dashboard">Doanh số lỗi</span></td>
        <td>
            <asp:Label runat="server" ID="lbOrderDetailTotalFailed" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Đặt cọc</span></td>
        <td>
            <asp:Label runat="server" ID="lbDepositFailed" CssClass="total"></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            <asp:HyperLink runat="server" ID="hplViewDetailFailed">Xem</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>

            <span class="lbtitle-total-dashboard">Số đơn hàng quá hạn</span></td>
        <td>
            <asp:Label runat="server" ID="lbNumberOfOrderOverdue" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Doanh số</span></td>
        <td>
            <asp:Label runat="server" ID="lbOrderTotalOverdue" CssClass="total"></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            <span class="lbtitle-total-dashboard">Doanh số quá hạn</span></td>
        <td>
            <asp:Label runat="server" ID="lbOrderDetailTotalOverdue" CssClass="total"></asp:Label>
        </td>
        <td>
            <span class="lbtitle-total-dashboard">Đặt cọc</span></td>
        <td>
            <asp:Label runat="server" ID="lbDepositOverdue" CssClass="total"></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            <asp:HyperLink runat="server" ID="hplViewDetailOverdue">Xem</asp:HyperLink>
        </td>
    </tr>
</table>
