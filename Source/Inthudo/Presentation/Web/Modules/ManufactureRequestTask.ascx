<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManufactureRequestTask.ascx.cs" Inherits="Web.Modules.ManufactureRequestTask" %>

<%@ Register TagPrefix="inthudo" Src="~/Modules/DatePicker.ascx" TagName="DatePicker" %>


<span class="lbtitle">Nhận việc</span><asp:CheckBox runat="server" ID="cbStartWork" AutoPostBack="true" OnCheckedChanged="cbStartWork_CheckedChanged" />
<span class="lbtitle">Ngày</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerBeginDate" Visible="false" />
<br />
<span class="lbtitle">Hoàn thành</span><asp:CheckBox runat="server" ID="cbCompleteWork" AutoPostBack="true" OnCheckedChanged="cbCompleteWork_CheckedChanged" />
<span class="lbtitle">Ngày</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerEndDate" Visible="false" />


<script type="text/javascript">
    function confirmChangeManufactureRequestStatus() {
        if (confirm("Bạn có chắc chắn chuyển trạng thái yêu cầu thiết kế?") == true)
            return true;
        else
            return false;
    }
</script>
