<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailInfo.ascx.cs" Inherits="Web.Modules.OrderDetailInfo" %>
<%@ Register TagPrefix="inthudo" TagName="NumericTextBox" Src="~/Modules/NumericTextBox.ascx" %>
<%@ Register TagPrefix="inthudo" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>

<div runat="server" id="panelOrderDetailId" class="order-detail-id">
    <span class="lbtitle">Mã nội dung đơn hàng: </span>
    <asp:Label ID="lbOrderDetailID" runat="server"></asp:Label>
</div>
<div class="order-info" id="panelOrderInfo" runat="server">
    <span class="lbtitle">Mã đơn hàng: </span>
    <asp:Label runat="server" ID="lbOrderId"></asp:Label>
    <br />
    <span class="lbtitle">Khách hàng: </span>
    <asp:Label ID="lbCustomer" runat="server"></asp:Label>
    <br />
    <span class="lbtitle">NVKD: </span>
    <asp:Label ID="lbBusinessMan" runat="server"></asp:Label>
</div>

<div class="order-detail">
    <span class="lbtitle">Sản phẩm </span>
    <asp:TextBox runat="server" ID="txtProduct"></asp:TextBox>
    <ajaxToolkit:AutoCompleteExtender 
        ID="txtProduct_AutoCompleteExtender" 
        runat="server"  
        ServicePath="~/WebService.asmx" 
        BehaviorID="txtProduct_AutoCompleteExtender" 
        DelimiterCharacters="" 
        ServiceMethod="GetProductNames" 
        TargetControlID="txtProduct"
        Enabled="true"
        UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3">
    </ajaxToolkit:AutoCompleteExtender>    
    <asp:RequiredFieldValidator ID="requiredFieldValidatorProduct" runat="server" ControlToValidate="txtProduct" ErrorMessage="Bạn hãy nhập sản phẩm" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
    <br />
    <span class="lbtitle">Quy cách sản phẩm</span>
    <div>
        <asp:TextBox ID="txtProductRequirement" runat="server" Width="600" Height="300" ValidationGroup="val"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredFieldValidatorProductRequirement" runat="server" ErrorMessage="Bạn hãy nhập quy cách sản phẩm!" ControlToValidate="txtProductRequirement" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
        <ajaxToolkit:HtmlEditorExtender ID="txtProductRequirement_HtmlEditorExtender" runat="server" BehaviorID="txtProductRequirement_HtmlEditorExtender" TargetControlID="txtProductRequirement" EnableSanitization="false">
        </ajaxToolkit:HtmlEditorExtender>
    </div>
    <span class="lbtitle">KH có mẫu thiết kế</span><asp:CheckBox runat="server" ID="cbIsCustomerHasDesign" />
    <br />
    <span class="lbtitle">Hình thức in</span><asp:CheckBoxList runat="server" ID="cblPrintingType" RepeatColumns="3" CssClass="checkboxlist"></asp:CheckBoxList>
    <br />
    <span class="lbtitle">Số lượng: </span>
    <inthudo:NumericTextBox runat="server" ID="ctrltxtQuantity" Value="1" MaximumValue="1000000" MinimumValue="1" RequiredErrorMessage="Bạn hãy nhập số lượng!" RangeErrorMessage="Số lượng từ 1 đến 1.000.000" />
    <br />
    <span class="lbtitle">Đơn giá: </span>
    <inthudo:DecimalTextBox runat="server" ID="ctrltxtPrice" Value="0" MaximumValue="1000000000" MinimumValue="0" RequiredErrorMessage="Bạn hãy nhập đơn giá!" RangeErrorMessage="Đơn giá từ 0 đến 1.000.000.000" />[VNĐ]

</div>

<script type="text/javascript">
    function RefreshParent() {
        if (window.opener != null && !window.opener.closed) {
            window.opener.location.reload();
        }
    }
    window.onunload = RefreshParent;

    function radioMe(e) {
        if (!e) e = window.event;
        var sender = e.target || e.srcElement;

        if (sender.nodeName != 'INPUT') return;
        var checker = sender;
        var chkBox = document.getElementById('<%= cblPrintingType.ClientID %>');
        var chks = chkBox.getElementsByTagName('INPUT');
        for (i = 0; i < chks.length; i++) {
            if (chks[i] != checker)
                chks[i].checked = false;
        }
    }
</script>
