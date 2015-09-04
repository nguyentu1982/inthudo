<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfo.ascx.cs" Inherits="Web.Modules.CustomerInfo" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/EmailTextBox.ascx" TagName="EmailTextBox" %>

<div class="customer-info">
    <div runat="server" id="panelCustomerId">
        <span class="lbtitle">
            <asp:Label ID="lbCustomerIDTitle" runat="server" Text=""></asp:Label></span>
            <asp:Label runat="server" ID="lbCustomerID"></asp:Label>
    </div>
    <span class="lbtitle">
        <asp:Label ID="lbCustomerNameTitle" runat="server" Text=""></asp:Label> </span>
        <asp:TextBox runat="server" ID="txtCustomerName"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCustomerName" runat="server" ErrorMessage="Bạn hãy nhập tên khách hàng!" ForeColor="Red" ControlToValidate="txtCustomerName"></asp:RequiredFieldValidator>
    <br />
    <span class="lbtitle">Mobile </span>
    <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelephone" runat="server" ErrorMessage="Bạn hãy nhập số điện thoại!" ForeColor="Red" ControlToValidate="txtTelephone"></asp:RequiredFieldValidator>
    <br />
    <span class="lbtitle">Địa chỉ </span>
    <asp:TextBox ID="txtAddress" runat="server" Width="300px"></asp:TextBox>
    <br />
    <span class="lbtitle">Email </span>
    <inthudo:EmailTextBox runat="server" ID="ctrlEmailTextBox" />
    <br />
    <span class="lbtitle">Ghi chú</span>
    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <span class="lbtitle">Tên công ty </span>
    <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
    <br />
    <span class="lbtitle">Số điện thoại </span>
    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
    <br />
    <span class="lbtitle">Fax</span><asp:TextBox ID="txtFaxNumber" runat="server"></asp:TextBox>
    <br />
    <span class="lbtitle">Mã số thuế</span><asp:TextBox ID="txtTaxCode" runat="server"></asp:TextBox>
    <br />
</div>


<script type="text/javascript">
    function confirmDelete() {
        if (confirm("Bạn có chắc chắn xóa?") == true)
            return true;
        else
            return false;
    }
</script>

