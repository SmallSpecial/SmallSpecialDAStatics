﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoneDropDay.aspx.cs" Inherits="WSS.Web.AdminF.Stats.StoneDropDay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../CSS/wssstyle.css" rel="stylesheet" type="text/css" />

    <script src="../../js/DataPicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="itemtitle">
            <h3>
                <asp:Label ID="LabelTitle" runat="server" Text=" 贵重物品掉落统计>>精炼石(宝石)"></asp:Label></h3>
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
            &nbsp; 开始时间:<asp:TextBox ID="tboxTimeB" runat="server" Width="120px" MaxLength="20"
                onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate"></asp:TextBox>
            结束时间:<asp:TextBox ID="tboxTimeE" runat="server" Width="120px" MaxLength="20" onFocus="new WdatePicker({skin:'default',startDate:'2011-01-01',isShowClear:false,readOnly:false})"
                class="Wdate"></asp:TextBox>
            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
        </div>
        <div class="titletip">
            <h7>
                区域: <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>  时间: <span clas时间: <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
        </div>
        <div class="gridv">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12px" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25" ShowFooter=true>
                <Columns>
                    <asp:BoundField DataField="date" HeaderText="日期" HeaderStyle-Width="120px" />
                    <asp:BoundField DataField="num1" HeaderText="1档精炼石" />
                    <asp:BoundField DataField="num2" HeaderText="2档精炼石" />
                    <asp:BoundField DataField="num3" HeaderText="3档精炼石" />
                    <asp:BoundField DataField="num4" HeaderText="4档精炼石" />
                    <asp:BoundField DataField="num5" HeaderText="5档精炼石" />
                    <asp:BoundField DataField="num6" HeaderText="6档精炼石" />
                    <asp:BoundField DataField="num7" HeaderText="7档精炼石" />
                    <asp:BoundField DataField="num8" HeaderText="8档精炼石" />
                    <asp:BoundField DataField="numB1" HeaderText="1档精炼石(绑定)" />
                    <asp:BoundField DataField="numB2" HeaderText="2档精炼石(绑定)" />
                    <asp:BoundField DataField="numB3" HeaderText="3档精炼石(绑定)" />
                    <asp:BoundField DataField="numB4" HeaderText="4档精炼石(绑定)" />
                    <asp:BoundField DataField="numB5" HeaderText="5档精炼石(绑定)" />
                    <asp:BoundField DataField="numB6" HeaderText="6档精炼石(绑定)" />
                    <asp:BoundField DataField="numB7" HeaderText="7档精炼石(绑定)" />
                    <asp:BoundField DataField="numB8" HeaderText="8档精炼石(绑定)" />
                    <asp:BoundField HeaderText="总计" />
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
