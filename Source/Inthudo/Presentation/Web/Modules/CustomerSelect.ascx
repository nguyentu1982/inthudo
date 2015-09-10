<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSelect.ascx.cs" Inherits="Web.Modules.CustomerSelect" %>
<%@ Register TagPrefix="inthudo" TagName="CustomerSearch"  Src="~/Modules/CustomerSearch.ascx"%>


<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    
    <ContentTemplate>
        <span class="lbtitle">
            <asp:Label ID="lbCustomerIdHeader" runat="server" Text=""></asp:Label></span> <asp:TextBox ID="txtCustomerCode" runat="server" OnTextChanged="txtCustomerCode_TextChanged" AutoPostBack="true" ></asp:TextBox>
        <span runat="server" id="panelFind"></span>
        
        <div runat="server" id="panelCustomerInfo">
            <asp:Label runat="server" ID="lbCustomerInfo" Text=""/>
        </div>
        
        <div id="panelCustomerSearch" style="display:none;border:1px solid #0094ff;padding:10px;" runat="server">
            
            <inthudo:CustomerSearch runat="server" ID="ctrlCustomerSearch" />
        </div>
        
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txtCustomerCode" EventName="TextChanged"/>

    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    $(function () {
        $(document).on("click", "#btFindCustomer", function () {
            var displayStatus = $("#panelCustomerSearch").css("display")
            if (displayStatus == "none") {
                $("#panelCustomerSearch").css("display", "block");
            }
            else {
                $("#panelCustomerSearch").css("display", "none");
            }
        })
    })
    
</script>
