﻿<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Web.Modules.NumericTextBox" Codebehind="NumericTextBox.ascx.cs" %>
 
<asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="txtValue"
    Font-Name="verdana" Font-Size="9pt" runat="server" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
<asp:RangeValidator ID="rvValue" runat="server" ControlToValidate="txtValue"
    Type="Integer" Display="Dynamic" ForeColor="Red" ></asp:RangeValidator>
<ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvValueE" TargetControlID="rfvValue"
    HighlightCssClass="validatorCalloutHighlight" />
<ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rvValueE" TargetControlID="rvValue"
    HighlightCssClass="validatorCalloutHighlight" />
