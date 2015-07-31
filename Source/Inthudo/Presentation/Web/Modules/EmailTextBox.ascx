<%@ Control Language="C#" AutoEventWireup="true" Inherits="Web.Modules.EmailTextBox"
    CodeBehind="EmailTextBox.ascx.cs" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit"%> 

<asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="txtValue"
    ErrorMessage="Bạn phải nhập Email!" Display="Dynamic" ForeColor="Red" />
<asp:RegularExpressionValidator ID="revValue" runat="server" ControlToValidate="txtValue"
    ValidationExpression=".+@.+\..+" ErrorMessage="Kiểm tra định dạng email!"
    Display="Dynamic" ForeColor="Red" />
<ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvValueE" TargetControlID="rfvValue"
    HighlightCssClass="validatorCalloutHighlight" />
<ajaxtoolkit:validatorcalloutextender runat="Server" id="revValueE" targetcontrolid="revValue"
    highlightcssclass="validatorCalloutHighlight" />
