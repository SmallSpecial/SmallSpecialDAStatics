<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyLog.aspx.cs" Inherits="WebWSS.Stats.MoneyLog" %>

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
            top: -140px;
            left: 50px;
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
            >>
            <%--<asp:Label runat="server" Text=" <%$Resources:Language,LblCorpsbattleRank %>"></asp:Label>--%>
            <asp:Label runat="server" Text=" <%$Resources:Language,LblItemLog %>"></asp:Label>

        </div>
        <div class="search">
             <asp:Label runat="server" Text="<%$Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                 <asp:ListItem Text="<%$Resources:Language,LblAllBigZone%>"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="False"
                OnSelectedIndexChanged="DropDownListArea2_SelectedIndexChanged">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            <%-- 线路:--%><asp:DropDownList ID="DropDownListArea3" runat="server" Width="120" Visible="False">
                <asp:ListItem Text="<%$ Resources:Language,LblAllZoneLine %>"></asp:ListItem>
            </asp:DropDownList>

            <asp:Label ID="lblRoleId" runat="server" Text="<%$Resources:Language,LblRoleNo %>"></asp:Label>
            <asp:TextBox ID="tbRoleId" runat="server"></asp:TextBox>

            <asp:Label ID="lblPARA_1" runat="server" Text="PARA_1"></asp:Label>
            <asp:TextBox ID="txtPARA_1" runat="server"></asp:TextBox>
            <asp:Label ID="lblPARA_2" runat="server" Text="PARA_2"></asp:Label>
            <asp:TextBox ID="txtPARA_2" runat="server"></asp:TextBox>

            <asp:Label ID="lblOpid" runat="server" Text="OPID"></asp:Label>
            <asp:DropDownList ID="ddlOpid" runat="server" Width="120" AutoPostBack="True">

            </asp:DropDownList>
            &nbsp;<asp:Literal runat="server" Text="<%$ Resources:Language,LblStaticTime %>"></asp:Literal>:
            <asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10"></asp:TextBox><a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'><img src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>
            <asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="10" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate" Visible="False"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>"  CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7>            
            <uc1:ControlDateSelect ID="ControlDateSelect1" runat="server" OnSelectDateChanged="ControlDateSelect_SelectDateChanged" />
            <uc2:ControlChartSelect ID="ControlChartSelect1" runat="server" OnSelectChanged="ControlChartSelect_SelectChanged" UsePie=false /> &nbsp;
                <asp:Button ID="btnOutExcel" runat="server" Text="EXCEL" CssClass="buttonbl" onclick="ExportExcel" />
                <uc4:ControlOutFile ID="ControlOutFile1" runat="server" />
            <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>:
            <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>   
            <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>: 
            <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
     <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="false"></asp:Label>
     <asp:Label ID="lblDeType" runat="server" Text="43" Visible="true"></asp:Label>
        </div>
        <div class="gridv" style="margin-right:100px">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12pt" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" AllowPaging="true">
                <Columns>
                    <asp:BoundField DataField="UID" HeaderText="<%$Resources:Language,LblUserNo %>" />
                    <asp:BoundField DataField="CID" HeaderText="<%$Resources:Language,LblRoleNo %>" />
                    <asp:TemplateField HeaderText="<%$Resources:Language,LblRoleName %>">
                        <ItemStyle Width="130px" />
                        <ItemTemplate>
                            <%#GetRoleName(Eval("CID").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PARA_1" HeaderText="PARA_1" />
                    <asp:TemplateField HeaderText="PARA_1">
                        <ItemStyle Width="130px" />
                        <ItemTemplate>
                            <%#GetItem(Eval("PARA_1").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PARA_2" HeaderText="PARA_2" />
                    <asp:TemplateField HeaderText="PARA_2">
                        <ItemStyle Width="130px" />
                        <ItemTemplate>
                            <%#GetWay(Eval("PARA_2").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OPID">
                        <ItemStyle Width="130px" />
                        <ItemTemplate>
                            <%#GetOPID(Eval("OPID").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OP_BAK" HeaderText="OP_BAK" />
                    <asp:BoundField DataField="OP_TIME" HeaderText="OP_TIME" />
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
            <div style="text-align: right; color: Blue;margin-right:70px;">
                        <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First" Text="<%$Resources:Language,LblHomePage %>" OnClick="lbtnF_Click">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page" Text="<%$Resources:Language,LblPrviousPage %>" OnClick="lbtnP_Click">
                        </asp:LinkButton>
                        &nbsp;
                        第
                        <asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='1'>
                        </asp:Label>
                        页/共
                        &nbsp;
                        <asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text=''>
                        </asp:Label>
                        &nbsp;
                        页
                        <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next" Text="<%$Resources:LAnguage,LblNextPage %>" OnClick="lbtnN_Click">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                             Text="<%$Resources:Language,LblEndPage %>" OnClick="lbtnE_Click"></asp:LinkButton>
                        &nbsp;
                    <asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>' Width="32px" CssClass="inputmini"></asp:TextBox>页<asp:Button ID="Button3" runat="server" OnClick="Go_Click" Text="<%$Resources:Language,LblGoto %>" />&nbsp;
                    </div>
            <asp:Label ID="lblerro" runat="server"  Text="<%$Resources:Language,Tip_Sign %>"  ForeColor="#FF6600"></asp:Label>
            <uc3:ControlChart ID="ControlChart1" runat="server" Visible="False" />
            
        </div>
    </div>
    <br /><br /><br /><br /><br />
    <span style="display:none"><asp:Label ID="lblDebug" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
    </form>
</body>
</html>
