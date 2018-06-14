<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOnlineMain.aspx.cs"
    Inherits="WSS.Web.StatF.UserOnlineMain" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="WSS.DBUtility" %>
<%@ Import Namespace="InfoSoftGlobal" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>统计系统</title>

    <script type="text/javascript" src="FusionCharts/FusionCharts.js"></script>

    <script type="text/javascript">
        function exportChart(exportFormat) {
            if (FusionCharts("mychart").exportChart)
                FusionCharts("mychart").exportChart({ "exportFormat": exportFormat });
            else
                Ext.Msg.alert('提示', '请等待图表加载完毕...');
        }
        function updateChart(xmlstr) {
            FusionCharts("mychart").setDataXML(xmlstr);
        }
    </script>

    <script runat="server">
        string ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        DataSet ds;
        StringBuilder xmlData = new StringBuilder();
        string chartitle = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetChart();
            }
        }
        private void SetChart()
        {
            string fromdate = DateTime.Now.AddDays(-300).ToShortDateString();
            string todate = DateTime.Now.ToShortDateString();

            string sql = @"SELECT top 30 F_ID, F_Year, F_Month, F_Day, F_BigZone, F_ZoneID, F_LoginNGSID, F_ACU, F_PCU, F_PCUTime FROM T_GameOnlineBaseDig_Day WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='" + fromdate + "'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='" + todate + "'";

            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStr;
            ds = sp.Query(sql);

            if (ds == null)
            {
                return;
            }

            chartitle = "最近30天ACU&PCU走势图[" + fromdate + "至" + todate + "]";

            xmlData.Append("<chart caption='" + chartitle + "' xAxisName='日期' yAxisName='数量' showValues= '0' labelDisplay='ROTATE' rotateLabels='1' slantLabels='1' numberPrefix=''  aboutMenuItemLabel='关于 神龙游' aboutMenuItemLink='#' exportEnabled='1' exportAction='Download' exportShowMenuItem = '0'  exportfilename='SLYCharts' exportAtClient='0' exportHandler='Export_Handler/FCExporter.aspx' exportDialogMessage='数据转换中,请稍候...' animation='1'  numVDivLines='10'  >");

            string str0 = "<categories>";
            string str1 = "<dataset seriesName='平均在线人数' color='F1683C' >";
            string str2 = "<dataset seriesName='最高在线人数' color='1D8BD1'>";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DateTime dateS=Convert.ToDateTime(dr["F_Month"] +"-"+ dr["F_Month"] + "-" + dr["F_Day"] + "");
                str0 += " <category label='" + dateS.ToString("MM-dd周ddd") + "' value='" + dr["F_Day"] + "' />";
                str1 += "<set value='" + dr["F_ACU"] + "' />";
                str2 += "<set value='" + dr["F_PCU"] + "' />";
            }
            str0 += " </categories>";
            str1 += "</dataset>";
            str2 += "</dataset>";


            xmlData.Append(str0 + str1 + str2);

            xmlData.Append("</chart>");

            Literal1.Text = FusionCharts.RenderChart("FusionCharts/MSLine.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!&LoadDataErrorText=" + DateTime.Now.Millisecond.ToString() + "", "", xmlData.ToString(), "mychart", "1000PX", "450PX", false, true);

        }
        [AjaxMethod]
        public string GetXmlstr1()
        {
            SetChart();
            return xmlData.ToString();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" StateProvider="None"  CleanResourceUrl="False" />
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North MarginsSummary="5 5 5 5">
                    <ext:Panel ID="Panel1" runat="server" Title="描述" BodyStyle="padding: 5px;" Frame="true"
                        Icon="CalendarViewDay">
                        <Body>
                           最新统计日期:2012-03-10
<br />
                                            [总量] 注册:3253 激活:3253 进入:256 活跃:36 流失: 1 回归 :5
<br />
                                            [新增] 注册:3 激活:3 进入:2 活跃:0 流失: 0 回归 :0
<br />
                                            [在线] 平均在线:3 最高在线:59 总在线时长:530 登录人次:361 退出人次: 361 登录IP数:23 退出IP数:23
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 0 5">
                    <ext:Panel ID="Panel2" runat="server" Title="图例" BodyStyle="padding: 5px;" Frame="true"
                        Icon="ChartCurve" AutoScroll="True">
                        <TopBar>
                            <ext:Toolbar ID="Toolbar2" runat="server" BodyStyle="padding:0px;" Flat="True">
                                <Items>
                                 <ext:Label ID="Label11" Height="30" Text="&nbsp;&nbsp;" runat="server" />
<ext:Button ID="Button5" runat="server" Text="刷新" Icon="ArrowRefresh" Flat="true">
                                <Listeners>
                                    <Click Handler="Coolite.AjaxMethods.GetXmlstr1({success: function(result){updateChart(result);},eventMask: {showMask: true,minDelay: 500,msg:'正在获取数据并执行操作...',customtarget:'TabPanel1'}});" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="Button1" runat="server" Text="导出图片" Icon="Picture" Flat="true">
                                <Listeners>
                                    <Click Handler="JavaScript:exportChart('PNG')" />
                                </Listeners>
                            </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Body>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </Body>
                    </ext:Panel>
                </Center>
            </ext:BorderLayout>
        </Body>
    </ext:ViewPort>
    </form>
</body>
</html>
