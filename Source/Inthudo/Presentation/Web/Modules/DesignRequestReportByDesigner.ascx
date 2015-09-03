<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestReportByDesigner.ascx.cs" Inherits="Web.Modules.DesignRequestReportByDesigner" %>
<table>
    <tr>
        <td colspan="5"><strong>Nhân viên thiết kế:</strong>
            <asp:Label ID="lbDesingnerName" runat="server"></asp:Label>
&nbsp; <strong>Account:</strong>
            <asp:Label ID="lbUserName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Số yêu cầu mới tạo</td>
        <td>
            <asp:Label ID="lbNumberOfRequestCreated" runat="server"></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            <asp:HyperLink ID="hplCreated" runat="server">Xem</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>Đang thiết kế</td>
        <td>
            <asp:Label ID="lbNumberOfRequestDesigning" runat="server"></asp:Label>
        </td>
        <td>Chi phí</td>
        <td>
            <asp:Label ID="lbCostDesinging" runat="server"></asp:Label>
        </td>
        <td>
            <asp:HyperLink ID="hplDesigning" runat="server">Xem</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>Đã thiết kế xong</td>
        <td>
            <asp:Label ID="lbNumberOfRequestDesigningCompleted" runat="server"></asp:Label>
        </td>
        <td>Chi phí</td>
        <td>
            <asp:Label ID="lbCostDesingingCompleted" runat="server"></asp:Label>
        </td>
        <td>
            <asp:HyperLink ID="hplCompleted" runat="server">Xem</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>Đang thiết kế lại</td>
        <td>
            <asp:Label ID="lbNumberOfRequestReDesigning" runat="server"></asp:Label>
        </td>
        <td>Chi phí</td>
        <td>
            <asp:Label ID="lbCostReDesigning" runat="server"></asp:Label>
        </td>
        <td>
            <asp:HyperLink ID="hplReDesigning" runat="server">Xem</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>Khách đã duyệt mẫu TK</td>
        <td>
            <asp:Label ID="lbNumberOfRequestApproved" runat="server"></asp:Label>
        </td>
        <td>Chi phí</td>
        <td>
            <asp:Label ID="lbCostApproved" runat="server"></asp:Label>
        </td>
        <td>
            <asp:HyperLink ID="hplApproved" runat="server">Xem</asp:HyperLink>
        </td>
    </tr>
</table>

