<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorpsLog.aspx.cs" Inherits="WebWSS.Stats.CorpsLog" %>

<%@ Register src="../Common/ControlChart.ascx" tagname="ControlChart" tagprefix="uc1" %>
<%@ Register src="../Common/ControlChartSelect.ascx" tagname="ControlChartSelect" tagprefix="uc2" %>
<%@ Register src="../Common/ControlOutFile.ascx" tagname="ControlOutFile" tagprefix="uc3" %>

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
<body onload="remove_loading();">
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
            <asp:Label ID="LabelTitle" runat="server" Text="<%$ Resources:Language,LblUserStatis %>"></asp:Label>
            >>
            <%--<asp:Label runat="server" Text="<%$ Resources:Language,LblLoginAreaStatis %>"></asp:Label>--%>
            <asp:Label runat="server" Text="<%$ Resources:Language,LblCorpsLog %>"></asp:Label>
        </div>
        <div class="search">
            <asp:Label runat="server" Text="<%$Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                 <asp:ListItem Text="<%$Resources:Language,LblAllBigZone%>"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea2" runat="server" Width="120" AutoPostBack="False">
                 <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <%--<asp:Label runat="server" Text="<%$ Resources:Language,LblBigZone %>"></asp:Label>--%>
            <asp:Label runat="server" Text="<%$ Resources:Language,LblCorps %>"></asp:Label>:
            <%--<asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True">
            </asp:DropDownList>--%>
            <asp:TextBox ID="tbCorps" runat="server" Width="120"></asp:TextBox>
             &nbsp;
            <%--<asp:Literal runat="server" Text="<%$ Resources:Language,LblStaticTime %>"></asp:Literal>:
            <asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10">
            </asp:TextBox>
            <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'><img src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'>
            </a>
            <asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="10" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate" Visible="False">
            </asp:TextBox>--%>
            <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7><div id="DateSelect" runat="Server"></div>
               <%--<uc2:ControlChartSelect ID="ControlChartSelect1" runat="server" OnSelectChanged="ControlChartSelect_SelectChanged"  />&nbsp; --%>
                <asp:Button ID="btnOutExcel" runat="server" Text="EXCEL" CssClass="buttonbl" onclick="ExportExcel" />
                 <uc3:ControlOutFile ID="ControlOutFile1" runat="server" />
           <%-- <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>:--%>
            <%--<span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>--%>
           <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>:
            <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" ShowFooter="true" AllowPaging="true"> 
                <Columns>
                    <asp:BoundField DataField="szName" HeaderText="<%$Resources:Language,LbPlayerName %>" />
                    <asp:BoundField DataField="CorpsName" HeaderText="<%$Resources:Language,LbCorpsName %>" />
                    <asp:BoundField DataField="CorpsNickName" HeaderText="<%$Resources:Language,LbCorpsNickName %>" />
                    <asp:BoundField DataField="CurActivity" HeaderText="<%$Resources:Language,LbCurActivity %>" />

                    <asp:BoundField DataField="CorpsLevel" HeaderText="<%$Resources:Language,LbCorpsLevel %>" />
                    <asp:BoundField DataField="CorpsSkillLv" HeaderText="<%$Resources:Language,LbCorpsSkillLv %>" />
                    <asp:BoundField DataField="CorpsShopLv" HeaderText="<%$Resources:Language,LbCorpsShopLv %>" />
                    <asp:BoundField DataField="CorpsWoodlv" HeaderText="<%$Resources:Language,LbCorpsWoodlv %>" /> 
                    <asp:BoundField DataField="CorpsWoodStorageLv" HeaderText="<%$Resources:Language,LbCorpsWoodStorageLv %>" /> 
                    <asp:BoundField DataField="CorpsInteriorLv" HeaderText="<%$Resources:Language,LbCorpsInteriorLv %>" />
                </Columns>

                <PagerTemplate>
                    <div style="text-align: right; color: Blue">
                        <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" Text="<%$Resources:Language,LblHomePage %>">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" Text="<%$Resources:Language,LblPrviousPage %>">
                        </asp:LinkButton>
                        &nbsp;
                        第
                        <asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'>
                        </asp:Label>
                        页/共
                        &nbsp;
                        <asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'>
                        </asp:Label>
                        &nbsp;
                        页
                        <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" Text="<%$Resources:LAnguage,LblNextPage %>">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" Text="<%$Resources:Language,LblEndPage %>"></asp:LinkButton>
                        &nbsp;
                    <asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>' Width="32px" CssClass="inputmini"></asp:TextBox>页<asp:Button ID="Button3" runat="server" OnClick="Go_Click" Text="<%$Resources:Language,LblGoto %>" />&nbsp;
                    </div>
                </PagerTemplate>

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
            <uc1:ControlChart ID="ControlChart1" runat="server" Visible="False" />
        </div>
    </div>
    </form>
</body>
</html>
