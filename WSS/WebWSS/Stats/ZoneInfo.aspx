<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZoneInfo.aspx.cs" Inherits="WebWSS.Stats.ZoneInfo" %>

<%@ Register Src="../Common/ControlMonthSelect.ascx" TagName="ControlMonthSelect"
    TagPrefix="uc1" %>
<%@ Register Src="../Common/ControlOutFile.ascx" TagName="ControlOutFile" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <asp:Label ID="LabelTitle" runat="server" Text=" 服务>>服务器统计 服务器基本状态统计"></asp:Label>
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="100" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="战 区">
                        <ItemTemplate>
                            <%#GetZoneName(Eval("F_BigZone"), Eval("F_ZoneID"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="武神宗昌盛度" DataField="F_TJM_CSD" />
                    <asp:BoundField HeaderText="逍遥林昌盛度" DataField="F_WSZ_CSD" />
                    <asp:BoundField HeaderText="天机盟昌盛度" DataField="F_XYL_CSD" />
                    <asp:TemplateField HeaderText="昨日登陆角色数">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblLoginRoleCount" Text='<%#GetRoleCount(Eval("F_BigZone"),Eval("F_ZoneID"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="昨日最高在线数">
                        <ItemTemplate>
                            <%#GetMaxRoleCount(Eval("F_BigZone"),Eval("F_ZoneID"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="昨日总充值数">
                        <ItemTemplate>
                            <%#GetMaxMoneyCount(Eval("F_BigZone"), Eval("F_ZoneID"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="昨日活跃值宝箱1领取数">
                        <ItemTemplate>
                            <%#GetHuoYueCount(Eval("F_BigZone"), Eval("F_ZoneID"),1)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="昨日活跃值宝箱2领取数">
                        <ItemTemplate>
                            <%#GetHuoYueCount(Eval("F_BigZone"), Eval("F_ZoneID"),2)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="昨日活跃值宝箱3领取数">
                        <ItemTemplate>
                            <%#GetHuoYueCount(Eval("F_BigZone"), Eval("F_ZoneID"),3)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="昨日活跃值宝箱4领取数">
                        <ItemTemplate>
                            <%#GetHuoYueCount(Eval("F_BigZone"), Eval("F_ZoneID"),4)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="当前封印等级">
                        <ItemTemplate>
                            <%#GetLevel((byte[])Eval("F_FY_DataContent"))%>级
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="距离开封印剩余天数">
                        <ItemTemplate>
                            <%#GetDays(GetLevel((byte[])Eval("F_FY_DataContent")), Convert.ToDateTime(Eval("F_FY_DateTime")), Convert.ToInt32(Eval("F_BigZone")), Convert.ToInt32(Eval("F_ZoneID")))%>天
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True"
                    ForeColor="White" />
                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="#D1DDF1" />
            </asp:GridView>
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
