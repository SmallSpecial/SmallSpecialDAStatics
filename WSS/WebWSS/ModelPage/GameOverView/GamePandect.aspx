<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GamePandect.aspx.cs" Inherits="WebWSS.ModelPage.GameOverView.GamePandect" %>

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
    <script src="../../Script/jquery-1.12.3.min.js"></script>
    <script src="echarts/echarts.js"></script>
    <script type="text/javascript">
        var t_id = setInterval(animate, 20);
        var pos = 0;
        var dir = 2;
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
        function toDate(fDate){  
            var fullDate = fDate.split("-");  
            return new Date(fullDate[0], fullDate[1]-1, fullDate[2], 0, 0, 0);  
        }
        function getNextDay(d) {
            d = new Date(d);
            d = +d + 1000 * 60 * 60 * 24;
            d = new Date(d);
            //格式化
            return d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();

        }
    </script>
    <style>
        #loader_container {
            text-align: center;
            position: absolute;
            top: 40%;
            width: 100%;
            left: 0;
        }

        #loader {
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

        #progress {
            height: 5px;
            font-size: 1px;
            width: 1px;
            position: relative;
            top: 1px;
            left: 0px;
            background-color: #8894a8;
        }

        #loader_bg {
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
                <asp:Label ID="LabelTitle" runat="server" Text="游戏概览"></asp:Label>
                >>
                <asp:Label runat="server" Text="总览"></asp:Label>
            </div>
            <div class="search" style="margin-top: 8px; margin-bottom: 4px;">
                <%--渠道--%>
                <asp:Label runat="server">渠道：</asp:Label>
                <asp:DropDownList runat="server" ID="ddlChannel" Width="80"></asp:DropDownList>
                <%--开始时间--%>
                <asp:Literal runat="server" Text="<%$ Resources:Language,LblStartTime %>"></asp:Literal>:
                <asp:TextBox ID="tboxTimeB" runat="server" Width="80px" MaxLength="10">
                </asp:TextBox>
                <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeB"), "yyyy-mm-dd", null, null, null, "11");'>
                    <img src='../../img/Calendar.gif' border='0' style='padding-bottom: 4px;' align='absmiddle'>
                </a>
                <%--结束时间--%>
                <asp:Literal runat="server" Text="<%$ Resources:Language,LblEndTime %>"></asp:Literal>:
                <asp:TextBox ID="tboxTimeE" runat="server" Width="80px" MaxLength="10">
                </asp:TextBox>
                <a style='cursor: pointer;' onclick='PopCalendar.show(document.getElementById("tboxTimeE"), "yyyy-mm-dd", null, null, null, "11");'>
                    <img src='../../img/Calendar.gif' border='0' style='padding-bottom: 4px;' align='absmiddle'>
                </a>
                <%--查询--%>
                <asp:Button ID="ButtonSearch" runat="server" Text="<%$Resources:Language,BtnQuery %>" CssClass="button" OnClick="ButtonSearch_Click" />
            </div>
            <div class="titletip">
                <%--日期控件--%>
                <uc1:ControlDateSelect ID="ControlDateSelect1" runat="server" OnSelectDateChanged="ControlDateSelect_SelectDateChanged" Visible="false" />
                <%--图表控件--%>
                <uc2:ControlChartSelect ID="ControlChartSelect1" runat="server" OnSelectChanged="ControlChartSelect_SelectChanged" UsePie="false" Visible="false" />
                &nbsp;
                <%--导出Excel/Word--%>
                <%--<asp:Button ID="btnOutExcel" runat="server" Text="EXCEL" CssClass="buttonbl" OnClick="ExportExcel" Visible="false" />--%>
                <uc4:ControlOutFile ID="ControlOutFile1" runat="server" />
            </div>
            <div style="margin-bottom: 20px;">
                总览
                <table border="0" style="border: 1px solid #a0c6e5; width: 97%; text-align: center;" cellpadding="0" cellspacing="1">
                    <tr>
                        <td>
                            <asp:Label ID="lblRegister" runat="server" Text="0"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCreate" runat="server" Text="0"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblRechargePeopleOfNum" runat="server" Text="0"></asp:Label>
                        </td>
                        <td>￥ 
                            <asp:Label ID="lblRechargeAmount" runat="server" Text="0"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblRegisterLv" runat="server" Text="0%"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCreateLv" runat="server" Text="0%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>注册(激活)玩家数</td>
                        <td>创建玩家数</td>
                        <td>充值人数</td>
                        <td>充值总额</td>
                        <td>注册付费率</td>
                        <td>创建付费率</td>
                    </tr>
                </table>
            </div>
            <div>
                <div style="display: inline; float: left; width: 50%; height: 500px;">
                    <div>
                        <asp:Label runat="server" Text="注册（激活）玩家数"></asp:Label>
                        <span style="float: right; margin-right: 90px;">
                            <asp:Button ID="btnRegisterTableTransform" runat="server" Text="表" CssClass="button" OnClick="btnRegisterTableTransform_Click" />
                            <asp:Button ID="btnRegisterMapTransform" runat="server" Text="图" CssClass="button" OnClick="btnRegisterMapTransform_Click" />
                            <asp:Button ID="btnRegisterToExcel" runat="server" Text="导出Excel" CssClass="button" OnClick="btnRegisterToExcel_Click" />
                        </span>
                    </div>
                    <div id="divTableRegister" runat="server" visible="false">
                        <table style="width: 94%;">
                            <tr>
                                <td>
                                    <div class="gridv">
                                        <asp:GridView ID="gvRegister" OnRowDataBound="gvRegister_RowDataBound" runat="server" AutoGenerateColumns="False" Font-Size="12pt" Width="95%" CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" OnPageIndexChanging="gvRegister_PageIndexChanging" PageSize="7" AllowPaging="true">
                                            <Columns>
                                                <asp:BoundField DataField="DateTime" HeaderText="时间" />
                                                <asp:BoundField DataField="Num" HeaderText="玩家数" />
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
                                                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" Text="<%$Resources:Language,LblEndPage %>">
                                                    </asp:LinkButton>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>' Width="32px" CssClass="inputmini">
                                                    </asp:TextBox>
                                                    页
                                                    <asp:Button ID="Button3" runat="server" OnClick="gvRegister_Go_Click" Text="<%$Resources:Language,LblGoto %>" />
                                                    &nbsp;
                                                </div>
                                            </PagerTemplate>
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                            <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                                            <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                                            <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="#D1DDF1" />
                                        </asp:GridView>
                                        <uc3:ControlChart ID="ControlChart1" runat="server" Visible="False" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divMapRegister" style="height: 400px; width: 94%;" runat="server" visible="true">
                        <script type="text/javascript">
                            var xAxisData = new Array();//横坐标
                            var sTime = $("#tboxTimeB").val();//开始时间
                            var eTime = $("#tboxTimeE").val();//结束时间
                            var dates = (Math.abs(toDate(eTime) - toDate(sTime)) / (1000 * 60 * 60 * 24)) + 1;//时间间隔
                            for (var i = 0; i < dates; i++) {
                                if (i == 0) {
                                    xAxisData[i] = sTime;
                                }
                                else {
                                    xAxisData[i] = getNextDay(xAxisData[i - 1]);
                                }
                            };
                            for (var j = 0; j < xAxisData.length; j++) {
                                xAxisData[j] = (xAxisData[j] + "").substring(5);
                            }
                            // 路径配置
                            require.config({
                                paths: {
                                    echarts: 'echarts'
                                }
                            });
                            // 注册玩家数
                            require(
                                [
                                    'echarts',
                                    'echarts/chart/bar', // 使用柱状图就加载bar模块，按需加载
                                    'echarts/chart/line'
                                ],
                                 function (ecRegister) {
                                     // 基于准备好的dom，初始化echarts图表
                                     var myChart = ecRegister.init(document.getElementById('divMapRegister'));

                                     var option = {
                                         tooltip: {
                                             trigger: 'axis',
                                             show: true
                                         },
                                         //右侧工具栏
                                         toolbox: {
                                             show: true,
                                             feature: {
                                                 mark: { show: true },
                                                 dataView: { show: true, readOnly: false },
                                                 magicType: { show: true, type: ['line', 'bar'] },
                                                 restore: { show: true },
                                                 saveAsImage: { show: true }
                                             }
                                         },
                                         calculable: true,
                                         legend: {
                                             data: ['玩家数']
                                         },
                                         xAxis: [
                                             {
                                                 type: 'category',
                                                 name: '时间',
                                                 data: xAxisData//['12-07', '12-08', '12-09', '12-10', '12-11', '12-12', '12-13', '12-14']
                                             }
                                         ],
                                         yAxis: [
                                             {
                                                 type: 'value',//Y轴显示的类型,默认为value
                                                 name: '玩家数',
                                             }
                                         ],
                                         series: []
                                     };
                                     //ajax动态获取数据
                                     $.ajax({
                                         type: 'post',
                                         url: 'Ajax.ashx?action=ShowChartRegister&STime=' + sTime + '&ETime=' + eTime,
                                         data: {},
                                         dataType: 'json',
                                         async: false,
                                         success: function (result) {
                                             if (result) {
                                                 // 获取json值
                                                 option.series = result.series;
                                                 // 为echarts对象加载数据 
                                                 myChart.setOption(option);
                                             }
                                         },
                                         error: function () {
                                             alert("Error");
                                         }
                                     });
                                 }
                            );
                        </script>
                    </div>
                </div>
            </div>
            <div>
                <div style="display: inline; float: right; width: 50%; height: 500px;">
                    <div>
                        <asp:Label runat="server" Text="创建玩家数"></asp:Label>
                        <span style="float: right; margin-right: 90px;">
                            <asp:Button ID="btnCreateTableTransform" runat="server" Text="表" CssClass="button" OnClick="btnCreateTableTransform_Click" />
                            <asp:Button ID="btnCreateMapTransform" runat="server" Text="图" CssClass="button" OnClick="btnCreateMapTransform_Click" />
                            <asp:Button ID="btnCreateToExcel" runat="server" Text="导出Excel" CssClass="button" OnClick="btnCreateToExcel_Click" />
                        </span>
                    </div>
                    <div id="divTableCreate" runat="server" visible="false">
                        <table style="width: 94%">
                            <tr>
                                <td>
                                    <div class="gridv">
                                        <asp:GridView ID="gvCreate" OnRowDataBound="gvCreate_RowDataBound" runat="server" AutoGenerateColumns="False" Font-Size="12pt" Width="95%" CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" OnPageIndexChanging="gvCreate_PageIndexChanging" PageSize="7" AllowPaging="true">
                                            <Columns>
                                                <asp:BoundField DataField="DateTime" HeaderText="时间" />
                                                <asp:BoundField DataField="Num" HeaderText="玩家数" />
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
                                                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" Text="<%$Resources:Language,LblEndPage %>">
                                                    </asp:LinkButton>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>' Width="32px" CssClass="inputmini">
                                                    </asp:TextBox>
                                                    页
                                                    <asp:Button ID="Button3" runat="server" OnClick="gvCreate_Go_Click" Text="<%$Resources:Language,LblGoto %>" />
                                                    &nbsp;
                                                </div>
                                            </PagerTemplate>
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                            <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                                            <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                                            <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="#D1DDF1" />
                                        </asp:GridView>
                                        <uc3:ControlChart ID="ControlChart3" runat="server" Visible="False" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divMapCreate" style="height: 400px; width: 94%;" runat="server" visible="true">
                        <script type="text/javascript">
                            var xAxisData = new Array();//横坐标
                            var sTime = $("#tboxTimeB").val();//开始时间
                            var eTime = $("#tboxTimeE").val();//结束时间
                            var dates = (Math.abs(toDate(eTime) - toDate(sTime)) / (1000 * 60 * 60 * 24)) + 1;//时间间隔
                            for (var i = 0; i < dates; i++) {
                                if (i == 0) {
                                    xAxisData[i] = sTime;
                                }
                                else {
                                    xAxisData[i] = getNextDay(xAxisData[i - 1]);
                                }
                            };
                            for (var j = 0; j < xAxisData.length; j++) {
                                xAxisData[j] = (xAxisData[j] + "").substring(5);
                            }
                            // 路径配置
                            require.config({
                                paths: {
                                    echarts: 'echarts'
                                }
                            });
                            // 创建玩家数
                            require(
                                [
                                    'echarts',
                                    'echarts/chart/bar', // 使用柱状图就加载bar模块，按需加载
                                    'echarts/chart/line'
                                ],
                                 function (ecCreate) {
                                     // 基于准备好的dom，初始化echarts图表
                                     var myChart = ecCreate.init(document.getElementById('divMapCreate'));

                                     var option = {
                                         tooltip: {
                                             trigger: 'axis',
                                             show: true
                                         },
                                         //右侧工具栏
                                         toolbox: {
                                             show: true,
                                             feature: {
                                                 mark: { show: true },
                                                 dataView: { show: true, readOnly: false },
                                                 magicType: { show: true, type: ['line', 'bar'] },
                                                 restore: { show: true },
                                                 saveAsImage: { show: true }
                                             }
                                         },
                                         calculable: true,
                                         legend: {
                                             data: ['玩家数']
                                         },
                                         xAxis: [
                                             {
                                                 type: 'category',
                                                 name: '时间',
                                                 data: xAxisData//['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
                                             }
                                         ],
                                         yAxis: [
                                             {
                                                 type: 'value',//Y轴显示的类型,默认为value
                                                 name: '玩家数',
                                             }
                                         ],
                                         series: []
                                     };
                                     //ajax动态获取数据
                                     $.ajax({
                                         type: 'post',
                                         url: 'Ajax.ashx?action=ShowChartCreate&STime='+sTime+'&ETime='+eTime,
                                         data: {},
                                         dataType: 'json',
                                         async: false,
                                         success: function (result) {
                                             if (result) {
                                                 // 获取json值
                                                 option.series = result.series;
                                                 // 为echarts对象加载数据 
                                                 myChart.setOption(option);
                                             }
                                         },
                                         error: function () {
                                             alert("Error");
                                         }
                                     });
                                 }
                            );
                        </script>
                    </div>
                </div>
            </div>
            <div>
                <div style="display: inline; float: left; width: 50%; height: 500px;">
                    <div>
                        <asp:Label runat="server" Text="充值人数"></asp:Label>
                        <span style="float: right; margin-right: 90px;">
                            <asp:Button ID="btnRechargePeopleOfNumTableTransform" runat="server" Text="表" CssClass="button" OnClick="btnRechargePeopleOfNumTableTransform_Click" />
                            <asp:Button ID="btnRechargePeopleOfNumMapTransform" runat="server" Text="图" CssClass="button" OnClick="btnRechargePeopleOfNumMapTransform_Click" />
                            <asp:Button ID="btnRechargePeopleOfNumToExcel" runat="server" Text="导出Excel" CssClass="button" OnClick="btnRechargePeopleOfNumToExcel_Click" />
                        </span>
                    </div>
                    <div id="divTableRechargePeopleOfNum" runat="server" visible="false">
                        <table style="width: 94%;">
                            <tr>
                                <td>
                                    <div class="gridv">
                                        <asp:GridView ID="gvRechargePeopleOfNum" OnRowDataBound="gvRechargePeopleOfNum_RowDataBound" runat="server" AutoGenerateColumns="False" Font-Size="12pt" Width="95%" CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" OnPageIndexChanging="gvRechargePeopleOfNum_PageIndexChanging" PageSize="30" AllowPaging="true">
                                            <Columns>
                                                <asp:BoundField DataField="DateTime" HeaderText="时间" />
                                                <asp:BoundField DataField="Num" HeaderText="玩家数" />
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
                                                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" Text="<%$Resources:Language,LblEndPage %>">
                                                    </asp:LinkButton>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>' Width="32px" CssClass="inputmini">
                                                    </asp:TextBox>
                                                    页
                                                    <asp:Button ID="Button3" runat="server" OnClick="gvRechargePeopleOfNum_Go_Click" Text="<%$Resources:Language,LblGoto %>" />
                                                    &nbsp;
                                                </div>
                                            </PagerTemplate>
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                            <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                                            <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                                            <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="#D1DDF1" />
                                        </asp:GridView>
                                        <uc3:ControlChart ID="ControlChart2" runat="server" Visible="False" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divMapRechargePeopleOfNum" style="height: 400px; width: 94%;" runat="server" visible="true">
                        <script type="text/javascript">
                            var xAxisData = new Array();//横坐标
                            var sTime = $("#tboxTimeB").val();//开始时间
                            var eTime = $("#tboxTimeE").val();//结束时间
                            var dates = (Math.abs(toDate(eTime) - toDate(sTime)) / (1000 * 60 * 60 * 24)) + 1;//时间间隔
                            for (var i = 0; i < dates; i++) {
                                if (i == 0) {
                                    xAxisData[i] = sTime;
                                }
                                else {
                                    xAxisData[i] = getNextDay(xAxisData[i - 1]);
                                }
                            };
                            for (var j = 0; j < xAxisData.length; j++) {
                                xAxisData[j] = (xAxisData[j] + "").substring(5);
                            }
                            // 路径配置
                            require.config({
                                paths: {
                                    echarts: 'echarts'
                                }
                            });
                            // 充值人数
                            require(
                                [
                                    'echarts',
                                    'echarts/chart/bar', // 使用柱状图就加载bar模块，按需加载
                                    'echarts/chart/line'
                                ],
                                 function (ecRechargePeopleOfNum) {
                                     // 基于准备好的dom，初始化echarts图表
                                     var myChart = ecRechargePeopleOfNum.init(document.getElementById('divMapRechargePeopleOfNum'));

                                     var option = {
                                         tooltip: {
                                             trigger: 'axis',
                                             show: true
                                         },
                                         //右侧工具栏
                                         toolbox: {
                                             show: true,
                                             feature: {
                                                 mark: { show: true },
                                                 dataView: { show: true, readOnly: false },
                                                 magicType: { show: true, type: ['line', 'bar'] },
                                                 restore: { show: true },
                                                 saveAsImage: { show: true }
                                             }
                                         },
                                         calculable: true,
                                         legend: {
                                             data: ['充值人数']
                                         },
                                         xAxis: [
                                             {
                                                 type: 'category',
                                                 name: '时间',
                                                 data: xAxisData//['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
                                             }
                                         ],
                                         yAxis: [
                                             {
                                                 type: 'value',//Y轴显示的类型,默认为value
                                                 name: '充值人数',
                                             }
                                         ],
                                         series: []
                                     };
                                     //ajax动态获取数据
                                     $.ajax({
                                         type: 'post',
                                         url: 'Ajax.ashx?action=ShowChartRechargePeopleOfNum&STime='+sTime+'&ETime='+eTime,
                                         data: {},
                                         dataType: 'json',
                                         async: false,
                                         success: function (result) {
                                             if (result) {
                                                 // 获取json值
                                                 option.series = result.series;
                                                 // 为echarts对象加载数据 
                                                 myChart.setOption(option);
                                             }
                                         },
                                         error: function () {
                                             alert("Error");
                                         }
                                     });
                                 }
                            );
                        </script>
                    </div>
                </div>
            </div>
            <div>
                <div style="display: inline; float: right; width: 50%; height: 800px;">
                    <div>
                        <asp:Label runat="server" Text="充值总额"></asp:Label>
                        <span style="float: right; margin-right: 90px;">
                            <asp:Button ID="btnRechargeAmountTableTransform" runat="server" Text="表" CssClass="button" OnClick="btnRechargeAmountTableTransform_Click" />
                            <asp:Button ID="btnRechargeAmountMapTransform" runat="server" Text="图" CssClass="button" OnClick="btnRechargeAmountMapTransform_Click" />
                            <asp:Button ID="btnRechargeAmountToExcel" runat="server" Text="导出Excel" CssClass="button" OnClick="btnRechargeAmountToExcel_Click" />
                        </span>
                    </div>
                    
                    <div id="divTableRechargeAmount" runat="server" visible="false">
                        <table style="width: 94%">
                            <tr>
                                <td>
                                    <div class="gridv">
                                        <asp:GridView ID="gvRechargeAmount" OnRowDataBound="gvRechargeAmount_RowDataBound" runat="server" AutoGenerateColumns="False" Font-Size="12pt" Width="95%" CellPadding="3" BorderWidth="1px" BorderStyle="None" BackColor="White" BorderColor="#CCCCCC" OnPageIndexChanging="gvRechargeAmount_PageIndexChanging" PageSize="30" AllowPaging="true">
                                            <Columns>
                                                <asp:BoundField DataField="DateTime" HeaderText="时间" />
                                                <asp:BoundField DataField="Amount" HeaderText="金额" />
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
                                                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" Text="<%$Resources:Language,LblEndPage %>">
                                                    </asp:LinkButton>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>' Width="32px" CssClass="inputmini">
                                                    </asp:TextBox>
                                                    页
                                                    <asp:Button ID="Button3" runat="server" OnClick="gvRechargeAmount_Go_Click" Text="<%$Resources:Language,LblGoto %>" />
                                                    &nbsp;
                                                </div>
                                            </PagerTemplate>
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#006699" Font-Size="12px" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                            <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                                            <FooterStyle HorizontalAlign="Center" BackColor="#005a8c" ForeColor="#FFFFFF" Font-Size="Medium" />
                                            <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="#D1DDF1" />
                                        </asp:GridView>
                                        <uc3:ControlChart ID="ControlChart4" runat="server" Visible="False" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divMapRechargeAmount" style="height: 400px; width: 94%;" runat="server" visible="true">
                        <script type="text/javascript">
                            var xAxisData = new Array();//横坐标
                            var sTime = $("#tboxTimeB").val();//开始时间
                            var eTime = $("#tboxTimeE").val();//结束时间
                            var dates = (Math.abs(toDate(eTime) - toDate(sTime)) / (1000 * 60 * 60 * 24)) + 1;//时间间隔
                            for (var i = 0; i < dates; i++) {
                                if (i == 0) {
                                    xAxisData[i] = sTime;
                                }
                                else {
                                    xAxisData[i] = getNextDay(xAxisData[i - 1]);
                                }
                            };
                            for (var j = 0; j < xAxisData.length; j++) {
                                xAxisData[j] = (xAxisData[j] + "").substring(5);
                            }
                            // 路径配置
                            require.config({
                                paths: {
                                    echarts: 'echarts'
                                }
                            });
                            // 充值金额
                            require(
                                [
                                    'echarts',
                                    'echarts/chart/bar', // 使用柱状图就加载bar模块，按需加载
                                    'echarts/chart/line'
                                ],
                                 function (ecRechargeAmount) {
                                     // 基于准备好的dom，初始化echarts图表
                                     var myChart = ecRechargeAmount.init(document.getElementById('divMapRechargeAmount'));

                                     var option = {
                                         tooltip: {
                                             trigger: 'axis',
                                             show: true
                                         },
                                         //右侧工具栏
                                         toolbox: {
                                             show: true,
                                             feature: {
                                                 mark: { show: true },
                                                 dataView: { show: true, readOnly: false },
                                                 magicType: { show: true, type: ['line', 'bar'] },
                                                 restore: { show: true },
                                                 saveAsImage: { show: true }
                                             }
                                         },
                                         calculable: true,
                                         legend: {
                                             data: ['充值金额']
                                         },
                                         xAxis: [
                                             {
                                                 type: 'category',
                                                 name: '时间',
                                                 data: xAxisData//['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
                                             }
                                         ],
                                         yAxis: [
                                             {
                                                 type: 'value',//Y轴显示的类型,默认为value
                                                 name: '充值金额',
                                             }
                                         ],
                                         series: []
                                     };
                                     //ajax动态获取数据
                                     $.ajax({
                                         type: 'post',
                                         url: 'Ajax.ashx?action=ShowChartRechargeAmount&STime=' + sTime + '&ETime=' + eTime,
                                         data: {},
                                         dataType: 'json',
                                         async: false,
                                         success: function (result) {
                                             if (result) {
                                                 // 获取json值
                                                 option.series = result.series;
                                                 // 为echarts对象加载数据 
                                                 myChart.setOption(option);
                                             }
                                         },
                                         error: function () {
                                             alert("Error");
                                         }
                                     });
                                 }
                            );
                        </script>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
