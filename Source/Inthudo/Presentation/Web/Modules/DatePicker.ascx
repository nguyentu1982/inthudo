<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="Web.Modules.DatePicker" %>

<asp:TextBox runat="server" ID="txtDateTime" />
<asp:ImageButton runat="Server" ID="btnCalendar" ImageUrl="~/Images/Calendar_scheduleHS.png" AlternateText="Xem lịch" />
<br />
<ajaxToolkit:CalendarExtender runat="server" ID="ajaxCalendar" TargetControlID="txtDateTime" PopupButtonID="btnCalendar" /> 