﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicSaleRankFight.aspx.cs"
    Inherits="WebWSS.Stats.PublicSaleRankFight" %>

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
<body onload="remove_loading();">
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
            <asp:Label ID="LabelTitle" runat="server" Text=" 拍卖行统计>>道具战斗力排行"></asp:Label>
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
            &nbsp; <asp:Label runat="server" Text="<%$ Resources:Language,LblStaticTime %>"></asp:Label>:<asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10"></asp:TextBox><a
                style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'><img
                    src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>
            <asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="10" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate" Visible="False"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <uc1:ControlDateSelect ID="ControlDateSelect" runat="server" OnSelectDateChanged="ControlDateSelect_SelectDateChanged" />
        <div class="titletip">
            <h7>
               <uc2:ControlChartSelect ID="ControlChartSelect1" runat="server" OnSelectChanged="ControlChartSelect_SelectChanged" UsePie=false Visible=false />
            &nbsp; 
            <uc4:ControlOutFile ID="ControlOutFile1" runat="server" />
           <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>: <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>   
            <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>:<span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
            <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="true"></asp:Label>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="100" ShowFooter="false">
                <Columns>
                    <asp:BoundField DataField="rownum" HeaderText="排 名" DataFormatString="第 {0} 名" >
                    <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F_FightValue" HeaderText="道具战斗力" >
                    <ItemStyle Width="180px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F_ExcelID" HeaderText="道具编号" >
                     <ItemStyle Width="60px" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="道具名称">
                            <ItemTemplate>
                                <%#GetItemName(Eval("F_ExcelID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="F_RoleID" HeaderText="拍卖者角色编号" />
                     <asp:TemplateField HeaderText="拍卖者角色名">
  
                        <ItemTemplate>
                            <%#TransDe(Eval("F_RoleName").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="F_SaleID" HeaderText="拍卖行编号" />
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
            <span style="display: none">
                <asp:Label ID="lblinfo" runat="server" Text="<%$Resources:Language,Tip_Sign %>" ForeColor="#FF6600"></asp:Label></span>
            <uc3:ControlChart ID="ControlChart1" runat="server" Visible="False" />
        </div>
    </div>
    </form>
</body>
</html>
