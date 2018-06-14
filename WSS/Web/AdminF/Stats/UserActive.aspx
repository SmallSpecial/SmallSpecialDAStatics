<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserActive.aspx.cs" Inherits="WSS.Web.AdminF.Stats.UserActive" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script language="javascript" type="text/javascript" src="../javascript/base.js"></script>

    <script language="javascript" type="text/javascript" src="../javascript/CheckedAll.js"></script>

    <link href="../CSS/wssstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <h3>
                用户登陆统计</h3>
        </div>
        <div class="search">
            功能选择:
            <asp:DropDownList ID="DropDownList1" runat="server" Width="120">
                <asp:ListItem>30天用户数据</asp:ListItem>
                <asp:ListItem>30天用户数据</asp:ListItem>
            </asp:DropDownList>
            &nbsp; 对象选择:
            <asp:DropDownList ID="DropDownList2" runat="server" Width="120">
                <asp:ListItem>第一区</asp:ListItem>
                <asp:ListItem>第二区</asp:ListItem>
            </asp:DropDownList>
            &nbsp;时间选择:
            <asp:DropDownList ID="DropDownList3" runat="server" Width="120">
                <asp:ListItem>2012</asp:ListItem>
                <asp:ListItem>2011</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList4" runat="server" Width="120">
                <asp:ListItem>03</asp:ListItem>
                <asp:ListItem>02</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList5" runat="server" Width="120">
                <asp:ListItem>日</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
            </asp:DropDownList>
        
            <asp:Button ID="Button1" runat="server" Text="查 询" CssClass="button" />
        </div>
        <div class="gridv">
         <asp:Label ID="lblerro" runat="server" Text="提示:没有相关信息!" ForeColor="#CC0066"></asp:Label>
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                AllowSorting="True" CellPadding="3" BorderWidth="1px" BorderStyle="None" AllowPaging="True"
                BackColor="White" BorderColor="#CCCCCC" OnPageIndexChanging="GridView1_PageIndexChanging"
                PageSize="25">
                <Columns>
                    <asp:BoundField DataField="F_ID" HeaderText="日期" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="登录用户量" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="IP数量" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="IP数量比" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="平均在线" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="最高在线" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="最高在线时间" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="最低在线" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="最低在线时间" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="活跃用户数量" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="活跃用户在线比" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="连续两天登陆" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="连续三天登陆" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户数量" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="新增用户数量" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="新增10内角色" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="新增20内角色" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="新增30内角色" SortExpression="F_ID" />

                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle BackColor="#D1DDF1" ForeColor="#000066" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#D1DDF1" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
