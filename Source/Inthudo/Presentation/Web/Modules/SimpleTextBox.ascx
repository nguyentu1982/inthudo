<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.Modules.SimpleTextBox" Codebehind="SimpleTextBox.ascx.cs" %>
 
<asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="txtValue" Font-Name="verdana"
    Font-Size="9pt" ForeColor="Red" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
<ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvValueE" TargetControlID="rfvValue"
    HighlightCssClass="validatorCalloutHighlight" />
