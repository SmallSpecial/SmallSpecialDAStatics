<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleVocationLevelRank.aspx.cs"
    Inherits="WSS.Web.AdminF.Stats.RoleVocationLevelRank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../CSS/wssstyle.css" rel="stylesheet" type="text/css" />

    <script src="../../js/DataPicker/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        a.tooltips
        {
            clear: both;
            color: #006699;
            position: relative;
            z-index: 2;
            font-size: medium;
        }
        a.tooltips:hover
        {
            z-index: 3;
            background: none;
        }
        a.tooltips span
        {
            display: none;
        }
        a.tooltips:hover span
        {
            display: block;
            position: absolute;
            top: 20px;
            left: 10px;
            width: 15em;
            border: 1px solid #006699;
            background-color: #ccffff;
            padding: 3px;
            color: black;
            font-weight: normal;
            font-size:smaller;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <h3>
                <asp:Label ID="LabelTitle" runat="server" Text=" 角色统计>>等级排行统计"></asp:Label></h3>
        </div>
        <div class="search">
            大区:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                <asp:ListItem>所有大区</asp:ListItem>
            </asp:DropDownList>
            服务器:<asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged">
                <asp:ListItem>所有战区</asp:ListItem>
            </asp:DropDownList>
            <%-- 线路:--%><asp:DropDownList ID="DropDownListArea3" runat="server" Width="120" Visible="False">
                <asp:ListItem>所有战线</asp:ListItem>
            </asp:DropDownList>
            &nbsp; 统计时间:<asp:TextBox ID="tboxTimeB" runat="server" Width="120px" MaxLength="20"
                onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate"></asp:TextBox>
            <asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="20" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate" Visible="False"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7><div id="DateSelect" runat="Server"></div>
                区域: <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>   时间: <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="100">
                <Columns>
                    <asp:BoundField DataField="rownum" HeaderText="等级" ItemStyle-Width="80px" DataFormatString="第 {0} 名">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Width="80px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="虎贲">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#">
                                <%#TranI2G(Eval("F_RoleName6").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID6").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID6") + " 等级:" + Eval("F_Level6") + " <br />经验:" + Eval("F_Experience6") + "  " + GetZoneName(Eval("F_BigZone6"),Eval("F_ZoneID6")) + " 升级时间:" + Eval("F_LevelUpTime6") + " 更新时间:" + Eval("F_LastTime6") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="浪人">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TranI2G(Eval("F_RoleName2").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID2").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID2") + " 等级:" + Eval("F_Level2") + " <br />经验:" + Eval("F_Experience2") + "  " + GetZoneName(Eval("F_BigZone2"), Eval("F_ZoneID2")) + " 升级时间:" + Eval("F_LevelUpTime2") + " 更新时间:" + Eval("F_LastTime2") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="龙胆">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TranI2G(Eval("F_RoleName3").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID3").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID3") + " 等级:" + Eval("F_Level3") + " <br />经验:" + Eval("F_Experience3") + "  " + GetZoneName(Eval("F_BigZone3"), Eval("F_ZoneID3")) + " 升级时间:" + Eval("F_LevelUpTime3") + " 更新时间:" + Eval("F_LastTime3") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="巧工">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TranI2G(Eval("F_RoleName4").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID4").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID4") + " 等级:" + Eval("F_Level4") + " <br />经验:" + Eval("F_Experience4") + "  " + GetZoneName(Eval("F_BigZone4"), Eval("F_ZoneID4")) + " 升级时间:" + Eval("F_LevelUpTime4") + " 更新时间:" + Eval("F_LastTime4") + "  "%></span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="气功师">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TranI2G(Eval("F_RoleName7").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID7").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID7") + " 等级:" + Eval("F_Level7") + " <br />经验:" + Eval("F_Experience7") + "  " + GetZoneName(Eval("F_BigZone7"), Eval("F_ZoneID7")) + " 升级时间:" + Eval("F_LevelUpTime7") + " 更新时间:" + Eval("F_LastTime7") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="花灵">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TranI2G(Eval("F_RoleName0").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID0").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID0") + " 等级:" + Eval("F_Level0") + " <br />经验:" + Eval("F_Experience0") + "  " + GetZoneName(Eval("F_BigZone0"), Eval("F_ZoneID0")) + " 升级时间:" + Eval("F_LevelUpTime0") + " 更新时间:" + Eval("F_LastTime0") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="天师">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TranI2G(Eval("F_RoleName1").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID1").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID1") + " 等级:" + Eval("F_Level1") + " <br />经验:" + Eval("F_Experience1") + "  " + GetZoneName(Eval("F_BigZone1"), Eval("F_ZoneID1")) + " 升级时间:" + Eval("F_LevelUpTime1") + " 更新时间:" + Eval("F_LastTime1") + "  "%></span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="行者">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TranI2G(Eval("F_RoleName5").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID5").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID5") + " 等级:" + Eval("F_Level5") + " <br />经验:" + Eval("F_Experience5") + "  " + GetZoneName(Eval("F_BigZone5"), Eval("F_ZoneID5")) + " 升级时间:" + Eval("F_LevelUpTime5") + " 更新时间:" + Eval("F_LastTime5") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="全部职业">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TranI2G(Eval("F_RoleName99").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID99").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID99") + " 等级:" + Eval("F_Level99") + " <br />经验:" + Eval("F_Experience99") + "  " + GetZoneName(Eval("F_BigZone99"), Eval("F_ZoneID99")) + " 升级时间:" + Eval("F_LevelUpTime99") + " 更新时间:" + Eval("F_LastTime99") + "  "%></span>
                            </a>
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
            <asp:Label ID="lblerro" runat="server" Text="提示:没有相关数据!" ForeColor="#FF6600"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
