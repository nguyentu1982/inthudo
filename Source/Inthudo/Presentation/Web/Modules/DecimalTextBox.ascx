<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.Modules.DecimalTextBox" Codebehind="DecimalTextBox.ascx.cs" %>

 
<asp:TextBox ID="txtValue" runat="server">0</asp:TextBox>
<ajaxToolkit:FilteredTextBoxExtender ID="ftbeValue" runat="server" TargetControlID="txtValue"
    FilterType="Custom, Numbers" ValidChars="-." />
<asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="txtValue" Font-Name="verdana"
    Font-Size="9pt" runat="server" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
<asp:RangeValidator ID="rvValue" runat="server" ControlToValidate="txtValue" Type="Double"
    Display="Dynamic" ForeColor="Red"></asp:RangeValidator>
<ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvValueE" TargetControlID="rfvValue"
    HighlightCssClass="validatorCalloutHighlight" />
<ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rvValueE" TargetControlID="rvValue"
    HighlightCssClass="validatorCalloutHighlight" />
