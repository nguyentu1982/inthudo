<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestDesignerTask.ascx.cs" Inherits="Web.Modules.DesignRequestDesignerTask" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DatePicker.ascx" TagName="DatePicker" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <span class="lbtitle">Nhận việc</span><asp:CheckBox runat="server" ID="cbStartWork" AutoPostBack="true" OnCheckedChanged="cbStartWork_CheckedChanged" />
        <span class="lbtitle">Ngày</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerBeginDate" Visible="false" />
        <br />
        <span class="lbtitle">Hoàn thành</span><asp:CheckBox runat="server" ID="cbCompleteWork" AutoPostBack="true" OnCheckedChanged="cbCompleteWork_CheckedChanged"  />
        <span class="lbtitle">Ngày</span><inthudo:DatePicker runat="server" ID="ctrlDatePickerEndDate" Visible="false" />
       
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cbStartWork" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="cbCompleteWork" EventName="CheckedChanged" />
    </Triggers>
</asp:UpdatePanel>

<script type="text/javascript">
    function confirmChangeDesignRequestStatus() {
        if (confirm("Bạn có chắc chắn chuyển trạng thái yêu cầu thiết kế?") == true)
            return true;
        else
            return false;
    }
</script>