<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintingTypeSelect.ascx.cs" Inherits="Web.Modules.PrintingTypeSelect" %>
<span class="lbtitle">Hình thức In</span><asp:CheckBoxList runat="server" ID="cblPrintingType" RepeatColumns="4" CssClass="checkboxlist"></asp:CheckBoxList>

<script type="text/javascript">
    function selectPrintingType(e) {
        if (!e) e = window.event;
        var sender = e.target || e.srcElement;

        if (sender.nodeName != 'INPUT') return;
        var checker = sender;
        var chkBox = document.getElementById('<%= cblPrintingType.ClientID %>');
        var chks = chkBox.getElementsByTagName('INPUT');

        if (checker.value == 0) {
            for (i = 0; i < chks.length; i++) {
                chks[i].checked = checker.checked;
            }
        }
        else {
            chks[0].checked = false;
        }
    }
</script>
