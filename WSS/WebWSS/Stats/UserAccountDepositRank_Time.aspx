<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAccountDepositRank_Time.aspx.cs"
    Inherits="WebWSS.Stats.UserAccountDepositRank_Time" %>

<%@ Register Src="../Common/ControlMonthSelect.ascx" TagName="ControlMonthSelect"
    TagPrefix="uc1" %>
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
            商城统计>>充值次数排行
        </div>
        <div class="search">
          <asp:Label runat="server" Text="<%$ Resources:Language,LblBigZone %>"></asp:Label>:
            <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120">
                <asp:ListItem Text="<%$ Resources:Language,LblAllBigZone %>"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; <asp:Literal runat="server" Text="<%$ Resources:Language,LblStaticTime %>"></asp:Literal>:<asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10"></asp:TextBox><a
                style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'><img
                    src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>
            <asp:Literal runat="server" Text="<%$ Resources:Language,LblEndTime %>"></asp:Literal>:<asp:TextBox ID="tboxTimeE" runat="server" Width="80px" MaxLength="10"></asp:TextBox><a
                style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeE"), "yyyy-mm-dd", null, null, null, "11");'><img
                    src='../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>
            &nbsp; <asp:Label runat="server" Text="<%$Resources:Language,LblUserNo %>"></asp:Label>:<asp:TextBox ID="tboxUserID" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
            &nbsp;<asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <uc1:ControlMonthSelect ID="ControlMonthSelect1" runat="server" />
        <div class="titletip">
        </div>
        <div class="gridv">
            <div id="PanelPage" runat="server" style="background-color: #005A8C; width: 96%;">
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                    AutoGenerateColumns="False" Font-Size="12px" Width="100%" OnSorting="GridView1_Sorting"
                    CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="100" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="ROWNUM" HeaderText="<%$Resources:Language,LblRank %>" DataFormatString="第{0}名"></asp:BoundField>
                        <asp:BoundField DataField="F_UserID" HeaderText="<%$Resources:Language,LblUserNo %>"></asp:BoundField>
                        <asp:BoundField DataField="F_Count" HeaderText="充值总次数"></asp:BoundField>
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
            </div>
            <asp:Label ID="lblerro" runat="server" Text="<%$Resources:Language,Tip_Nodata %>" ForeColor="#FF6600"></asp:Label>
            <br />
            &nbsp;提示:输入用户编号可查询此用户在一定时间段内的数据(即使不在前100名)
            <br />
        </div>
    </div>
    </form>
</body>
</html>
