<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLoginLose.aspx.cs"
    Inherits="WebWSS.Stats.UserLoginLose" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>
    <script language='JavaScript' src='../img/Admin.Js'></script>
    <script type="text/javascript" src='../img/GetDate.Js'></script>
    <script type="text/javascript">
        var t_id = setInterval(animate, 20);
        var pos = 0; var dir = 2;
        var len = 0;
        function animate() {
            var elem = document.getElementById('progress');
            if (elem != null) {
                if (pos == 0)
                    len += dir;
                if (len > 32 || pos > 79)
                    pos += dir;
                if (pos > 79)
                    len -= dir;
                if (pos > 79 && len == 0)
                    pos = 0;
                elem.style.left = pos; elem.style.width = len;
            }
        }
        function remove_loading() {
            this.clearInterval(t_id);
            var targelem = document.getElementById('loader_container');
            targelem.style.display = 'none';
            targelem.style.visibility = 'hidden';
        }
    </script>

    <style>
        #loader_container
        {
            text-align: center;
            position: absolute;
            top: 40%;
            width: 100%;
            left: 0;
        }
        #loader
        {
            font-family: Tahoma, Helvetica, sans;
            font-size: 11.5px;
            color: #000000;
            background-color: #FFFFFF;
            padding: 10px 0 16px 0;
            margin: 0 auto;
            display: block;
            width: 130px;
            border: 1px solid #5a667b;
            text-align: left;
            z-index: 2;
        }
        #progress
        {
            height: 5px;
            font-size: 1px;
            width: 1px;
            position: relative;
            top: 1px;
            left: 0px;
            background-color: #8894a8;
        }
        #loader_bg
        {
            background-color: #e4e7eb;
            position: relative;
            top: 8px;
            left: 8px;
            height: 7px;
            width: 113px;
            font-size: 1px;
        }
    </style>
</head>
<body onLoad="remove_loading();">
    <div id="loader_container">
        <div id="loader">
            <div align="center">
                <asp:Label runat="server" Text="<%$ Resources:Language,Tip_PageLoading %>"></asp:Label></div>
            <div id="loader_bg">
                <div id="progress">
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            
                同时用户登陆统计
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
                <asp:ListItem Text="<%$ Resources:Language,LblAllBigZone %>"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged" Visible="False">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListArea3" runat="server" Width="120" Visible="False">
                <asp:ListItem Text="<%$ Resources:Language,LblAllZoneLine %>"></asp:ListItem>
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
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7>
               <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>: <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text="Label"></asp:Label></span>  
            <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>:<span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text="Label"></asp:Label></span></h7>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView2" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                AllowSorting="false" CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White"
                BorderColor="#CCCCCC" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25"
                Visible="False">
                <Columns>
                    <asp:BoundField DataField="F_ID" HeaderText="服务器名称" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="登录用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="整体流失率周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失新增用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="新增流失率周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失活跃用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="活跃用户流失率周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="回归用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户等级10级以内" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户等级20级以内" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户等级30级以内" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户离线前未完成任务ID" SortExpression="F_ID" />

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
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                AllowSorting="True" CellPadding="3" BorderWidth="1px" BorderStyle="None" AllowPaging="True"
                BackColor="White" BorderColor="#CCCCCC" OnPageIndexChanging="GridView1_PageIndexChanging"
                PageSize="25">
                <Columns>
                    <asp:BoundField DataField="F_ID" HeaderText="<%$Resources:Language,LblDate %>" SortExpression="F_ID" DataFormatString="2012-3-{0}" ItemStyle-Width="100" />
                    <asp:HyperLinkField DataNavigateUrlFields="F_ID" DataNavigateUrlFormatString="UserOnLine.aspx?area1=100000,0&isnow=1&U_ID={0}&BackUrl=2333"
                        DataTextField="F_ID" HeaderText="服务器名称" SortExpression="F_ID" DataTextFormatString="服务器{0}" visible=false>
                        <%--                    <ItemStyle CssClass="action" Width="10%" />
                    <HeaderStyle CssClass="gvLeft" />--%>
                    </asp:HyperLinkField>
                                   
                    <asp:BoundField DataField="F_ID" HeaderText="登录用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="整体流失率周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失新增用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="新增流失率周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失活跃用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="活跃用户流失率周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="回归用户周计算" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户等级10级以内" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户等级20级以内" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户等级30级以内" SortExpression="F_ID" />
                    <asp:BoundField DataField="F_ID" HeaderText="流失用户离线前未完成任务ID" SortExpression="F_ID" />

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
    </div>
    </form>
</body>
</html>
