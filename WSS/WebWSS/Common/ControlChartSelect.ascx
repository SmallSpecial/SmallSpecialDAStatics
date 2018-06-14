<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlChartSelect.ascx.cs" Inherits="WebWSS.Common.ControlChartSelect" %>
<asp:Button ID="btnchartselectData" runat="server" Text="<%$Resources:Language,BtnData %>" CssClass="buttonblo" onclick="btn_Click" Enabled="False" />
<asp:Button ID="btnchartselectLine" runat="server" Text="<%$Resources:Language,BtnLineChart %>"  CssClass="buttonbl" onclick="btn_Click" />
<asp:Button ID="btnchartselectcol" runat="server" Text="<%$Resources:Language,BtnRectangularChart %>"  CssClass="buttonbl" onclick="btn_Click" />
<asp:Button ID="btnchartselectpie" runat="server" Text="<%$Resources:Language,BtnPieChart %>"  CssClass="buttonbl" onclick="btn_Click" />
<asp:Button ID="btnchartselectarea" runat="server" Text="<%$Resources:Language,BtnCloudChart %>"  CssClass="buttonbl" onclick="btn_Click" />
