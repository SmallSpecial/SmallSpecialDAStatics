﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopLog.aspx.cs" Inherits="WebWSS.ModelPage.ShoppingMallLog.ShopLog" %>

<%@ Register Src="~/Common/ControlDateSelect.ascx" TagName="ControlDateSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Common/ControlChartSelect.ascx" TagName="ControlChartSelect" TagPrefix="uc2" %>
<%@ Register Src="~/Common/ControlChart.ascx" TagName="ControlChart" TagPrefix="uc3" %>
<%@ Register Src="~/Common/ControlOutFile.ascx" TagName="ControlOutFile" TagPrefix="uc4" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../img/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='../../img/Admin.Js'></script>
    <script type="text/javascript" src='../../img/GetDate.Js'></script>
    <style type="text/css">
        a.tooltips {
            clear: both;
            color: #006699;
            position: relative;
            z-index: 2;
            font-size: 12pt;
        }

            a.tooltips:hover {
                z-index: 3;
                background: none;
            }

            a.tooltips span {
                display: none;
            }

            a.tooltips:hover span {
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
            >>
            <asp:Label runat="server" Text="<%$Resources:Language,LblShopLog %>"></asp:Label>
        </div>
        <div class="search">
                <asp:Label runat="server" Text="<%$Resources:Language,LblBigZone %>"></asp:Label>:
                <asp:DropDownList ID="DropDownListArea1" runat="server" Width="120" AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownListArea1_SelectedIndexChanged">
                    <asp:ListItem Text="<%$Resources:Language,LblAllBigZone%>"></asp:ListItem>
                </asp:DropDownList>

                <asp:Label runat="server" Text="<%$Resources:Language,LblService %>"></asp:Label>:
                <asp:DropDownList ID="DropDownListArea2" runat="server" Width="120">
                    <asp:ListItem Text="<%$ Resources:Language,LblAllZone %>"></asp:ListItem>
                </asp:DropDownList>

                &nbsp;
                <asp:Literal runat="server" Text="<%$ Resources:Language,LblStartTime %>"></asp:Literal>:
                <asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10">
                </asp:TextBox>
                <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'>
                    <img src='../../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>

                <asp:Literal runat="server" Text="<%$ Resources:Language,LblEndTime %>"></asp:Literal>:
                <asp:TextBox ID="tboxTimeE" runat="server" Width="80px" MaxLength="10">
                </asp:TextBox>
                <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeE"), "yyyy-mm-dd", null, null, null, "11");'>
                    <img src='../../img/Calendar.gif' border='0' style='padding-top: 10px' align='absmiddle'></a>

                <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
            </div>
        <div class="titletip">
            <h7>            
            <uc1:ControlDateSelect ID="ControlDateSelect1" runat="server" OnSelectDateChanged="ControlDateSelect_SelectDateChanged"/>
            <uc2:ControlChartSelect ID="ControlChartSelect1" runat="server" OnSelectChanged="ControlChartSelect_SelectChanged" UsePie=false /> &nbsp;
                <asp:Button ID="btnOutExcel" runat="server" Text="EXCEL" CssClass="buttonbl" onclick="ExportExcel" />
                <uc4:ControlOutFile ID="ControlOutFile1" runat="server" />
            <asp:Label runat="server" Text="<%$ Resources:Language,LblArea %>"></asp:Label>:
            <span class="tyellow"><asp:Label ID="LabelArea" runat="server" Text=" "></asp:Label></span>   
            <asp:Label runat="server" Text="<%$Resources:Language,LblTime %>"></asp:Label>: 
            <span class="tyellow"><asp:Label ID="LabelTime" runat="server" Text=""></asp:Label></span></h7>
     <asp:Label ID="lblDecoding" runat="server" Text="<%$Resources:Language,LblDecodeOpen %>" Visible="false"></asp:Label>
     <asp:Label ID="lblDeType" runat="server" Text="43" Visible="true"></asp:Label>
            <asp:Label runat="server" Text="建议查询的时间不要超过一周" ForeColor="Red"></asp:Label>
        </div>
        <div class="gridv" style="margin-right:100px">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"
                AutoGenerateColumns="False" Font-Size="12pt" Width="95%" OnSorting="GridView1_Sorting"
                CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC"
                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="30" AllowPaging="true">
                <Columns>
                    <asp:BoundField DataField="DateTime" HeaderText="<%$ Resources:Language,LblDate %>" />
                    <asp:TemplateField HeaderText="<%$ Resources:Language,LblShopName %>">
                        <ItemStyle Width="130px" />
                        <ItemTemplate>
                            <%#GetShopType(Eval("ShopType").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
                    <asp:TemplateField HeaderText="<%$Resources:Language,LblProductName %>">
                        <ItemStyle Width="130px" />
                        <ItemTemplate>
                            <%#GetItem(Eval("ItemID").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SoldNum" HeaderText="<%$Resources:Language,LblSoldNum %>" />
                    <asp:BoundField DataField="TradeNum" HeaderText="<%$Resources:Language,LblTradeNum %>" />
                    <asp:BoundField DataField="RoleNum" HeaderText="<%$Resources:Language,LblRoleNum %>" />
                    <asp:BoundField DataField="MoneyNum" HeaderText="<%$Resources:Language,LblMoneyNum %>" />
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
            <asp:Label ID="lblerro" runat="server"  Text="<%$Resources:Language,Tip_Sign %>"  ForeColor="#FF6600"></asp:Label>
            <uc3:ControlChart ID="ControlChart1" runat="server" Visible="False" />
            
        </div>
    </div>
    <br /><br />
    <span style="display:none"><asp:Label ID="lblDebug" runat="server" Text="" ForeColor="#FF6600"></asp:Label></span>
    </form>
</body>
</html>
