<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleVocationDefenceRank.aspx.cs"
    Inherits="WebWSS.Stats.RoleVocationDefenceRank" %>

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
            font-size: 9pt;
            filter: alpha(opacity=80);
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
                <asp:Label runat="server" Text="<%$ Resources:Language,Tip_PageLoading %>"></asp:Label>
            </div>
            <div id="loader_bg">
                <div id="progress">
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
           <asp:Label ID="LabelTitle" runat="server" Text=" <%$Resources:Language,LblRoleStatic %>"></asp:Label>
            >><asp:Label runat="server" Text="<%$Resources:Language,LblRoleStatic %>"></asp:Label>
        </div>
        <div class="search">
            <asp:Label runat="server" Text="<%$Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                <asp:ListItem Text="<%$Resources:Language,LblAllBigZone%>"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged">
               <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            <%-- 线路:--%><asp:DropDownList ID="DropDownListArea3" runat="server" Width="120" Visible="False">
                <asp:ListItem Text="<%$ Resources:Language,LblAllZoneLine %>"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; <asp:Literal runat="server" Text="<%$ Resources:Language,LblStaticTime %>"></asp:Literal>:
            <asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10"></asp:TextBox><a
                style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'><img
                    src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>
            <asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="10" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate" Visible="False"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>"  CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7>
            <uc1:ControlDateSelect ID="ControlDateSelect1" runat="server" OnSelectDateChanged="ControlDateSelect_SelectDateChanged" />
            <uc2:ControlChartSelect ID="ControlChartSelect1" runat="server" OnSelectChanged="ControlChartSelect_SelectChanged" UsePie=false /> &nbsp;
                <uc4:ControlOutFile ID="ControlOutFile1" runat="server" />
              <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>:
                <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>   
                 <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>: 
                 <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
            <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="false"></asp:Label><asp:Label ID="lblDeType" runat="server" Text="35" Visible="true"></asp:Label>
        </div>
        <div class="gridv" style="margin-right: 100px">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12pt" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="100">
                <Columns>
                     <asp:BoundField DataField="rownum" HeaderText="<%$Resources:Language,LblRank %>" ItemStyle-Width="80px" DataFormatString="<%$Resources:Language,Msg_RankFormat %>">
                        <ItemStyle Width="140px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <HeaderTemplate><%#GetVacationTypeName("1")  %></HeaderTemplate>
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#GetRoleName(Eval("F_RoleID1").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID1").ToString().Length == 0 ? "" : GetResource("LblNo")+ " :" + Eval("F_RoleID1") + GetResource("LblLevel")+ " :" + Eval("F_Level1") + " <br />"+GetResource("LblDefence")+":" + Eval("F_Defence1") + "<br/>"+ GetZoneName(Eval("F_BigZone1"), Eval("F_ZoneID1")) + GetResource("LblUpGradeTime") +" :"  + Eval("F_LevelUpTime1") +  "<br/>"+GetResource("LblUpdateTime")+ " :"   + Eval("F_LastTime1") + "  "%>
                                </span>
                            </a>
                              </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                       <HeaderTemplate><%#GetVacationTypeName("2")  %></HeaderTemplate>
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#GetRoleName(Eval("F_RoleID2").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID2").ToString().Length == 0 ? "" :  GetResource("LblNo")+ " :"  + Eval("F_RoleID2") +  GetResource("LblLevel")+ " :"  + Eval("F_Level2") +" <br />"+GetResource("LblDefence")+":" + Eval("F_Defence2") +  "<br/>" + GetZoneName(Eval("F_BigZone2"), Eval("F_ZoneID2")) + GetResource("LblUpGradeTime") +" :" + Eval("F_LevelUpTime2") +  "<br/>"+GetResource("LblUpdateTime")+ " :" + Eval("F_LastTime2") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                       <HeaderTemplate><%#GetVacationTypeName("3")  %></HeaderTemplate>
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#GetRoleName(Eval("F_RoleID3").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID3").ToString().Length == 0 ? "" :  GetResource("LblNo")+ " :"  + Eval("F_RoleID3") +  GetResource("LblLevel")+ " :"  + Eval("F_Level3") +" <br />"+GetResource("LblDefence")+":" + Eval("F_Defence3") +  "<br/>" + GetZoneName(Eval("F_BigZone3"), Eval("F_ZoneID3")) + GetResource("LblUpGradeTime") +" :" + Eval("F_LevelUpTime3") + "<br/>"+GetResource("LblUpdateTime")+ " :"+ Eval("F_LastTime3") + "  "%></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate><%#GetVacationTypeName("4")  %></HeaderTemplate>
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#GetRoleName(Eval("F_RoleID4").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID4").ToString().Length == 0 ? "" : GetResource("LblNo")+ " :"  + Eval("F_RoleID4") +  GetResource("LblLevel")+ " :"  + Eval("F_Level4") + " <br />"+GetResource("LblDefence")+":" + Eval("F_Defence4") +  "<br/>" + GetZoneName(Eval("F_BigZone4"), Eval("F_ZoneID4")) + GetResource("LblUpGradeTime") +" :" + Eval("F_LevelUpTime4") + "<br/>"+GetResource("LblUpdateTime")+ " :" + Eval("F_LastTime4") + "  "%></span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$ Resources:Language,LblAllVocation %>">
                        <ItemTemplate>
                            <a class="tooltips" href="#tooltips">
                                <%#GetRoleName(Eval("F_RoleID99").ToString())%>
                                <span>
                                    <%#Eval("F_RoleID99").ToString().Length == 0 ? "" :  GetResource("LblNo")+ " :"  + Eval("F_RoleID99") +  GetResource("LblLevel")+ " :"  + Eval("F_Level99") + " <br />"+GetResource("LblDefence")+":" + Eval("F_Defence99") +  "<br/>" + GetZoneName(Eval("F_BigZone99"), Eval("F_ZoneID99")) + GetResource("LblUpGradeTime") +" :"+ Eval("F_LevelUpTime99") + "<br/>"+GetResource("LblUpdateTime")+ " :" + Eval("F_LastTime99") + "  "%></span>
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
                        <asp:BoundField DataField="F_Defence1" HeaderText="猎魔者" />
                        <asp:BoundField DataField="F_Defence2" HeaderText="魔导师" />
                        <asp:BoundField DataField="F_Defence3" HeaderText="龙战士" />
                        <asp:BoundField DataField="F_Defence3" HeaderText="召唤师" />
                        <asp:BoundField DataField="F_Defence99" HeaderText="全部" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <br />
    </form>
</body>
</html>
