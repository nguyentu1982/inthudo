<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesignRequestEdit.ascx.cs" Inherits="Web.Modules.DesignRequestEdit" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DesignRequestInfo.ascx" TagName="DesignRequestInfo" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DesignRequestCustomerApprove.ascx" TagName="DesignRequestCustomerApprove" %>
<%@ Register TagPrefix="inthudo" Src="~/Modules/DesignRequestDesignerTask.ascx" TagName="DesignRequestDesignerTask" %>

<h1>Sửa yêu cầu thiết kế</h1>
<asp:Button ID="btSave" runat="server" Text="Lưu" OnClick="btSave_Click" ValidationGroup="val" />
<asp:Button ID="btDelete" runat="server" Text="Xóa" OnClientClick="return confirmDelete()" OnClick="btDelete_Click" />

<ajaxToolkit:TabContainer ID="DesignRequestTab" runat="server" ActiveTabIndex="0">
    <ajaxToolkit:TabPanel runat="server" ID="pnlDesignRequestInfo" HeaderText="Yêu cầu thiết kế">
        <ContentTemplate>
            <inthudo:DesignRequestInfo ID="ctrlDesignRequestInfo" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>

    <ajaxToolkit:TabPanel runat="server" ID="pnlDesignRequestDesignerTask" HeaderText="Xác nhận của thiết kế">
        <ContentTemplate>
            <inthudo:DesignRequestDesignerTask runat="server" ID="ctrlDesignRequestDesignerTask" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>

    <ajaxToolkit:TabPanel runat="server" ID="pnlDesignRequestCustomerApprove" HeaderText="Khách hàng duyệt">
        <ContentTemplate>
            <inthudo:DesignRequestCustomerApprove runat="server" ID="ctrlDesignRequestCustomerApprove" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>  
    


