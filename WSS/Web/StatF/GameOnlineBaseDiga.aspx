<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameOnlineBaseDiga.aspx.cs" Inherits="WSS.Web.StatF.GameOnlineBaseDiga" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="FusionCharts/FusionCharts.js"></script>
    <%--<script type="text/javascript" src="FusionCharts/jquery.min.js"></script>--%>
    <%--<script type="text/javascript" src="FusionCharts/firebug-lite.js"></script>--%>
    <%--<script type="text/javascript" src="FusionCharts/FusionCharts.HC.Charts.js"></script>
    <script type="text/javascript" src="FusionCharts/FusionCharts.HC.js"></script>
    <script type="text/javascript" src="FusionCharts/FusionCharts.jqueryplugin.js"></script>
    <script type="text/javascript" src="FusionCharts/FusionChartsExportComponent.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <asp:Literal ID="Literal1" runat="server"></asp:Literal><br />  
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem Selected="True">线形图</asp:ListItem>
            <asp:ListItem>矩形图</asp:ListItem>
            <asp:ListItem>饼形图</asp:ListItem>
            <asp:ListItem>云图</asp:ListItem>
        </asp:RadioButtonList>     
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
            RepeatDirection="Horizontal" Width="680px">
            <asp:ListItem>登录次数</asp:ListItem>
            <asp:ListItem>登录IP数</asp:ListItem>
            <asp:ListItem>退出次数</asp:ListItem>
            <asp:ListItem>退出IP数</asp:ListItem>
            <asp:ListItem Selected="True">在线人数</asp:ListItem>
            <asp:ListItem>在线IP数</asp:ListItem>
            <asp:ListItem>在线时长</asp:ListItem>
        </asp:CheckBoxList>  <asp:TextBox ID="TextBox1" runat="server" Width="119px">2011-12-28</asp:TextBox>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="查 询" />
        时间为空则查询运营期间总数据</div>
    </form>
</body>
</html>
