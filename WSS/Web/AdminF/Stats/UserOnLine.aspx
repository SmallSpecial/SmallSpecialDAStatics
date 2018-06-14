<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOnLine.aspx.cs" Inherits="WSS.Web.AdminF.Stats.UserOnLine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="../CSS/wssstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <h3>
                <asp:Label ID="LabelTitle" runat="server" Text=" 当前在线人数"></asp:Label></h3>
        </div>
        <div class="search">
            功能选择:
            <asp:DropDownList ID="DropDownListNavi" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListNavi_SelectedIndexChanged">
                <asp:ListItem>历史在线人数</asp:ListItem>
                <asp:ListItem>当前在线人数</asp:ListItem>
            </asp:DropDownList>
            &nbsp; 对象选择:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged" Visible="False">
                <asp:ListItem>所有大区</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged" Visible="False">
                <asp:ListItem>所有战区</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListArea3" runat="server" Width="120" Visible="False">
                <asp:ListItem>所有战线</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <asp:Label ID="LabelSTime" runat="server" Text="时间选择:"></asp:Label>
            <asp:DropDownList ID="DropDownListYear" runat="server" Width="80">
                <asp:ListItem>2012</asp:ListItem>
                <asp:ListItem>2011</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListMonth" runat="server" Width="60">
                <asp:ListItem>03</asp:ListItem>
                <asp:ListItem>02</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListDay" runat="server" Width="60">
                <asp:ListItem>日</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7>
                区域: <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text="Label"></asp:Label></span>  时间: <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text="Label"></asp:Label></span></h7>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25">
                <Columns>
                    <asp:TemplateField HeaderText="服务器名称">
                        <ItemTemplate>
                            <%#GetZoneNameFull(Eval("F_ZoneStr"))%>
                        </ItemTemplate>
                        <ItemStyle Width="280px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_OnlineNum" HeaderText="服务器在线" SortExpression="F_ID">
                        <HeaderStyle Width="180px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F_OnlineIpNum" HeaderText="IP数量" SortExpression="F_ID">
                        <HeaderStyle Width="180px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="IP/用户数量比">
                        <ItemTemplate>
                            <%#GetScale(Eval("F_OnlineIpNum"), Eval("F_OnlineNum"))%>
                        </ItemTemplate>
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="图XX（备用）" />
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
            <asp:Label ID="lblerro2" runat="server" Text="提示:没有相关信息!" ForeColor="#FF6600"></asp:Label>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView2" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                AllowSorting="False" CellPadding="3" BorderWidth="1px" BorderStyle="None" AllowPaging="true"
                BackColor="White" BorderColor="#CCCCCC" OnPageIndexChanging="GridView1_PageIndexChanging"
                PageSize="25">
                <Columns>
                    <asp:TemplateField HeaderText="服务器名称">
                        <ItemTemplate>
                            <%#GetZoneNameFull(Eval("F_ZoneStr"))%>
                        </ItemTemplate>
                        <ItemStyle Width="280px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_OnlineNum" HeaderText="服务器在线" SortExpression="F_ID">
                        <HeaderStyle Width="180px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F_OnlineIpNum" HeaderText="IP数量" SortExpression="F_ID">
                        <HeaderStyle Width="180px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="IP/用户数量比">
                        <ItemTemplate>
                            <%#GetScale(Eval("F_OnlineIpNum"), Eval("F_OnlineNum"))%>
                        </ItemTemplate>
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="图XX（备用）" />
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
            <asp:Label ID="lblerro" runat="server" Text="提示:没有相关信息!" ForeColor="#FF6600"></asp:Label>
        </div>
        <div>
            <asp:Button ID="ButtonBack" runat="server" Text="返回" CssClass="button" OnClick="ButtonBack_Click" />
        </div>
    </div>
    </form>
</body>
</html>
