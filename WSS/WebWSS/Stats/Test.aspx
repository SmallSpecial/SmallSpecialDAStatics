<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs"
    Inherits="WebWSS.Stats.Test" %>

<%@ Register Src="../Common/ControlDateSelect.ascx" TagName="ControlDateSelect" TagPrefix="uc1" %>
<%@ Register Src="../Common/ControlChartSelect.ascx" TagName="ControlChartSelect"
    TagPrefix="uc2" %>
<%@ Register Src="../Common/ControlChart.ascx" TagName="ControlChart" TagPrefix="uc3" %>
<%@ Register src="../Common/ControlOutFile.ascx" tagname="ControlOutFile" tagprefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../img/Style.css" rel="stylesheet" type="text/css"></link>
    <script language='JavaScript' src='../img/Admin.Js'></script>
    <script type="text/javascript" src='../img/GetDate.Js'></script>

    <style type="text/css">
        a.tooltips
        {
            clear: both;
            color: #006699;
            position: relative;
            z-index: 2;
            font-size: 12pt;
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
            left: -20px;
            width: 15em;
            border: 1px solid #006699;
            background-color: #ccffff;
            padding: 3px;
            color: black;
            font-weight: normal;
            font-size:9pt;
            filter:alpha(opacity=80)
        }
    </style>
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
<body style="overflow-x: hidden; overflow-y: scroll;" onload="remove_loading();">
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
            
                <asp:Label ID="LabelTitle" runat="server" Text=" 角色统计>>等级排行统计"></asp:Label>
        </div>
        <div class="search">
          <asp:Label runat="server" Text="<%$ Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                <asp:ListItem Text="<%$ Resources:Language,LblAllBigZone %>"></asp:ListItem>
            </asp:DropDownList>
             <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:<asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            <%-- 线路:--%><asp:DropDownList ID="DropDownListArea3" runat="server" Width="120" Visible="False">
                <asp:ListItem Text="<%$ Resources:Language,LblAllZoneLine %>"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; <asp:Label runat="server" Text="<%$ Resources:Language,LblStaticTime %>"></asp:Label>:<asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10"></asp:TextBox><a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'><img src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>
            <asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="10" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate" Visible="False"></asp:TextBox><asp:TextBox ID="tboxRoleID" runat="server" Text="1"></asp:TextBox>
                <asp:TextBox ID="tboxRoleName" runat="server" Text=""></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7>            
            <uc1:ControlDateSelect ID="ControlDateSelect1" runat="server" OnSelectDateChanged="ControlDateSelect_SelectDateChanged" />
            <uc2:ControlChartSelect ID="ControlChartSelect1" runat="server" OnSelectChanged="ControlChartSelect_SelectChanged" UsePie=false /> &nbsp;
                <uc4:ControlOutFile ID="ControlOutFile1" runat="server" />
           <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>: <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>   <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>:<span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
     <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="true"></asp:Label>
     <asp:Label ID="lblDeType" runat="server" Text="300" Visible="true"></asp:Label>
        </div>
        <div class="gridv" style="margin-right:100px">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12pt" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="100">
                <Columns>
                    <asp:BoundField DataField="rownum" HeaderText="排 行" ItemStyle-Width="80px" DataFormatString="第 {0} 名">
                        <ItemStyle Width="140px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="虎贲">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#">
                                <%#TransDe(Eval("F_RoleName6").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID6").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID6") + " 等级:" + Eval("F_Level6") + " <br />经验:" + Eval("F_Experience6") + "  " + GetZoneName(Eval("F_BigZone6"),Eval("F_ZoneID6")) + " 升级时间:" + Eval("F_LevelUpTime6") + " 更新时间:" + Eval("F_LastTime6") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="浪人">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TransDe(Eval("F_RoleName2").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID2").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID2") + " 等级:" + Eval("F_Level2") + " <br />经验:" + Eval("F_Experience2") + "  " + GetZoneName(Eval("F_BigZone2"), Eval("F_ZoneID2")) + " 升级时间:" + Eval("F_LevelUpTime2") + " 更新时间:" + Eval("F_LastTime2") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="龙胆">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TransDe(Eval("F_RoleName3").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID3").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID3") + " 等级:" + Eval("F_Level3") + " <br />经验:" + Eval("F_Experience3") + "  " + GetZoneName(Eval("F_BigZone3"), Eval("F_ZoneID3")) + " 升级时间:" + Eval("F_LevelUpTime3") + " 更新时间:" + Eval("F_LastTime3") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="巧工">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TransDe(Eval("F_RoleName4").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID4").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID4") + " 等级:" + Eval("F_Level4") + " <br />经验:" + Eval("F_Experience4") + "  " + GetZoneName(Eval("F_BigZone4"), Eval("F_ZoneID4")) + " 升级时间:" + Eval("F_LevelUpTime4") + " 更新时间:" + Eval("F_LastTime4") + "  "%></span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="斗仙">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TransDe(Eval("F_RoleName7").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID7").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID7") + " 等级:" + Eval("F_Level7") + " <br />经验:" + Eval("F_Experience7") + "  " + GetZoneName(Eval("F_BigZone7"), Eval("F_ZoneID7")) + " 升级时间:" + Eval("F_LevelUpTime7") + " 更新时间:" + Eval("F_LastTime7") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="花灵">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TransDe(Eval("F_RoleName0").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID0").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID0") + " 等级:" + Eval("F_Level0") + " <br />经验:" + Eval("F_Experience0") + "  " + GetZoneName(Eval("F_BigZone0"), Eval("F_ZoneID0")) + " 升级时间:" + Eval("F_LevelUpTime0") + " 更新时间:" + Eval("F_LastTime0") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="天师">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TransDe(Eval("F_RoleName1").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID1").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID1") + " 等级:" + Eval("F_Level1") + " <br />经验:" + Eval("F_Experience1") + "  " + GetZoneName(Eval("F_BigZone1"), Eval("F_ZoneID1")) + " 升级时间:" + Eval("F_LevelUpTime1") + " 更新时间:" + Eval("F_LastTime1") + "  "%></span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="行者">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TransDe(Eval("F_RoleName5").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID5").ToString().Length == 0 ? "" : " 编号:" + Eval("F_RoleID5") + " 等级:" + Eval("F_Level5") + " <br />经验:" + Eval("F_Experience5") + "  " + GetZoneName(Eval("F_BigZone5"), Eval("F_ZoneID5")) + " 升级时间:" + Eval("F_LevelUpTime5") + " 更新时间:" + Eval("F_LastTime5") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="全部职业">
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#TransDe(Eval("F_RoleName99").ToString())%>
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
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
            <uc3:ControlChart ID="ControlChart1" runat="server" Visible="False" />
            <div style="display: none">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="rownum" HeaderText="等级"  DataFormatString="第 {0} 名">
                        </asp:BoundField>
                        <asp:BoundField DataField="F_Level0" HeaderText="花灵" />
                        <asp:BoundField DataField="F_Level1" HeaderText="天师" />
                        <asp:BoundField DataField="F_Level2" HeaderText="浪人" />
                        <asp:BoundField DataField="F_Level3" HeaderText="龙胆" />
                        <asp:BoundField DataField="F_Level4" HeaderText="巧工" />
                        <asp:BoundField DataField="F_Level5" HeaderText="行者" />
                        <asp:BoundField DataField="F_Level6" HeaderText="虎贲" />
                        <asp:BoundField DataField="F_Level7" HeaderText="斗仙" />
                        <asp:BoundField DataField="F_Level99" HeaderText="全部" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <br /><br /><br /><br /><br />
    </form>
</body>
</html>
