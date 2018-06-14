<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOnlineDayAreaCol.aspx.cs"
    Inherits="WSS.Web.StatF.UserOnlineDayAreaCol" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="WSS.DBUtility" %>
<%@ Import Namespace="InfoSoftGlobal" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>统计系统</title>

    <script type="text/javascript" src="FusionCharts/FusionCharts.js"></script>

    <script language="JavaScript" src="FusionCharts/FusionMaps.js"></script>

    <%-- <script language="JavaScript" src="FusionCharts/FusionMapsExportComponent.js"></script>--%>

    <script type="text/javascript">
        var showResult = function(btn) {
            Ext.example.msg('Button Click', 'You clicked the {0} button', btn);
        };

        var showResultText = function(btn, text) {
            Ext.example.msg('Button Click', 'You clicked the {0} button and entered the text "{1}".', btn, text);
        };

        var submitValue = function(grid, hiddenFormat, format) {
            hiddenFormat.setValue(format);
            grid.submitData(false);
        }

        function exportChart(exportFormat) {
            if (FusionCharts("mychartz").exportChart)
                FusionCharts("mychartz").exportChart({ "exportFormat": exportFormat });
            else
                Ext.Msg.alert('提示', '请等待图表加载完毕...');
        }
        function updateChart(xmlstr) {
            //            FusionCharts("mychartz").setDataXML(xmlstr.Xmlz);
            //            var mapObject = getMapFromId('mychartm');
            //            mapObject.setDataXML(xmlstr.Xmlm);
        }

    </script>

    <script runat="server">
        string ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        DataSet ds;
        StringBuilder xmlDatam = new StringBuilder();
        StringBuilder xmlDataz = new StringBuilder();
        string chartitle = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.FromDate.Value = DateTime.Now.AddDays(-300).ToShortDateString();
                this.ToDate.Value = DateTime.Now.ToShortDateString();
                SetChart();
            }

        }
        private void SetChart()
        {

            string sql = @"SELECT top 35  F_Area, MAX(F_AreaCode) as F_AreaCode,(avg(F_OnlineTime)/60/24) as F_OnlineTimeU, avg(F_OnlineTime)/60  as F_OnlineTimeA, Sum(F_OnlineTime)/60  as F_OnlineTimeS FROM   T_GameOnlineBaseDig_Area WHERE 1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='" + this.FromDate.Value + "'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='" + this.ToDate.Value + "' group by F_Area";

            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStr;
            ds = sp.Query(sql);

            if (ds == null)
            {
                return;
            }

            chartitle = "用户分布区域[" + Convert.ToDateTime(this.FromDate.Value).ToShortDateString() + "至" + Convert.ToDateTime(this.ToDate.Value).ToShortDateString() + "]";


           


            xmlDataz.Append("<chart  exportEnabled='1'  exportAction='Download' exportShowMenuItem = '1'  exportfilename='SLYCharts' exportAtClient='0' exportHandler='Export_Handler/FCExporter.aspx' exportDialogMessage='数据转换中,请稍候...' animation='1' caption='" + chartitle + "' xAxisName='统计地区' yAxisName='[平均在线人数ACU]数量' showValues='1' labelDisplay='ROTATE' slantLabels='1'  formatNumberScale='0' showBorder='1' aboutMenuItemLabel='关于 神龙游' aboutMenuItemLink='#' lineColor='#129bee' ColumnColor='#129bee'>");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                xmlDataz.Append("<set label='" + dr["F_Area"] + "' value='" + dr["F_OnlineTimeU"] + "' />");
            }

            xmlDataz.Append("</chart>");

            //FusionCharts.SetRenderer("javascript");
            FusionCharts.SetRenderer("flash");

            // Literal2.Text += FusionCharts.RenderChart("FusionCharts/Column3D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!&LoadDataErrorText=" + DateTime.Now.Millisecond.ToString() + "", "", xmlDataz.ToString(), "mychartz"+DateTime.Now.Millisecond.ToString(), "1000PX", "500PX", false, true);

            Literal2.Text = FusionCharts.RenderChart("FusionCharts/Column3D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!&LoadDataErrorText=" + DateTime.Now.Millisecond.ToString() + "", "", xmlDataz.ToString(), "mychartz", "1000PX", "500PX", false, true, true);


            //xmlDatam = xmlDataz;
            //Literal1.Text = FusionCharts.RenderChart("FusionCharts/Column3D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!&LoadDataErrorText=" + DateTime.Now.Millisecond.ToString() + "", "", xmlDataz.ToString(), "mychartm", "1000PX", "500PX", false, true);


            Store1.DataSource = ds.Tables[0].DefaultView;
            Store1.DataBind();
        }

        public class RValues
        {
            public string Xmlz { get; set; }
            public string Xmlm { get; set; }
            public string Xmlx { get; set; }
        }

        [AjaxMethod]
        public RValues GetXmlstr()
        {
            SetChart();
            //Store1.DataSource = ds.Tables[0].DefaultView;
            //Store1.DataBind();

            //GridPanel2.Reload();
            RValues rvalues = new RValues();
            rvalues.Xmlm = xmlDatam.ToString();
            rvalues.Xmlz = xmlDataz.ToString();
            return rvalues;
        }


        protected void MyData_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            SetChart();
        }

        protected void SaveExcel(object sender, EventArgs e)
        {
            SetChart();
            SetExcelFromData(ds.Tables[0], chartitle);
        }

        public void SetExcelFromData(System.Data.DataTable dt, string FileName)
        {
            string data = FileName + "\n";

            string[] colms = { "年|F_Year", "月|F_Month", "日|F_Day", "大区|F_BigZone", "战区|F_ZoneID", "战线|F_LoginNGSID", "平均在线|F_ACU", "最高在线|F_PCU", "最高在线时间|F_PCUTime" };


            //写出列名 
            foreach (DataColumn column in dt.Columns)
            {
                //data += column.ColumnName + "", "";
                foreach (string colm in colms)
                {
                    if (colm.Split('|')[1] == column.ColumnName)
                    {
                        data += colm.Split('|')[0] + ",";
                        break;
                    }
                }
            }
            data += "\n";

            //写出数据 
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    //data += row[column].ToString() + ", ";
                    foreach (string colm in colms)
                    {
                        if (colm.Split('|')[1] == column.ColumnName)
                        {
                            data += row[column].ToString() + ",";
                            break;
                        }
                    }

                }
                data += "\n";
            }
            data += "\n";

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Context.Server.UrlEncode(FileName) + ".csv");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Write(data);
            Response.End();
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" StateProvider="None"  CleanResourceUrl="False" />
    <ext:Store ID="Store1" runat="server" OnRefreshData="MyData_Refresh">
        <Reader>
            <ext:JsonReader ReaderID="F_Area">
                <Fields>
                    <ext:RecordField Name="F_Area" Type="string" />
                    <ext:RecordField Name="F_AreaCode" Type="string" />
                    <ext:RecordField Name="F_OnlineTimeU" Type="int" />
                    <ext:RecordField Name="F_OnlineTimeA" Type="int" />
                    <ext:RecordField Name="F_OnlineTimeS" Type="int" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
            <LoadException Handler="Ext.Msg.alert('数据加载失败,请稍候重试', e.message )" />
        </Listeners>
    </ext:Store>
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North MarginsSummary="5 5 5 5">
                    <ext:Panel ID="Panel1" runat="server" Title="描述" Height="60" BodyStyle="padding: 5px;"
                        Frame="true" Icon="Information">
                        <Body>
                           用户分布区域  &nbsp;&nbsp;&nbsp;根据IP地址判断用户所在区域  [统计日期默认为最近30天]
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 0 5">
                    <ext:TabPanel ID="TabPanel1" runat="server">
                        <TopBar>
                            <ext:Toolbar ID="Toolbar2" runat="server" BodyStyle="padding:15px;">
                                <Items>
                                    <ext:Label ID="Label11" Height="30" Text="开始日期:&nbsp;" runat="server" />
                                    <ext:DateField runat="server" ID="FromDate" Vtype="daterange" AllowBlank="false"
                                        FieldLabel="To">
                                    </ext:DateField>
                                    <ext:Label ID="Label13" Text="&nbsp;结束日期:&nbsp;" runat="server" />
                                    <ext:DateField runat="server" ID="ToDate" Vtype="daterange" AllowBlank="false" FieldLabel="From">
                                        <Listeners>
                                            <Render Handler="#{FromDate}.endDateField = '#{ToDate}';this.startDateField = '#{FromDate}';#{FromDate}.isValid()" />
                                        </Listeners>
                                    </ext:DateField>
                                    <ext:Label ID="Label1" Text="&nbsp;" runat="server" />
                                    <ext:Button ID="Button1" runat="server" Text="统计" Icon="ApplicationViewGallery">
                                        <Listeners>
                                            <Click Handler="#{TabPanel1}.setActiveTab('Tab1');if(#{FromDate}.isValid() && #{ToDate}.isValid()){ Coolite.AjaxMethods.GetXmlstr({success: function(result){updateChart(result);},eventMask: {showMask: true,minDelay: 500,msg:'正在获取数据并执行操作...',customtarget:'TabPanel1'}});}else Ext.Msg.alert('提示', '请确认日期填写符合要求!');" />
                                        </Listeners>
                                    </ext:Button>
                                    <%--<ext:ToolbarFill ID="ToolbarFill2" runat="server" />--%>
                                    <ext:Button ID="Button5" runat="server" Text="刷新" Icon="ArrowRefresh">
                                        <Listeners>
                                            <Click Handler="Coolite.AjaxMethods.GetXmlstr({success: function(result){updateChart(result);},eventMask: {showMask: true,customtarget:'TabPanel1',minDelay: 500,msg:'正在获取数据并执行操作...'}});" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button ID="Button6" runat="server" Text="导出Excel" Icon="PageExcel" AutoPostBack="True"
                                        OnClick="SaveExcel">
                                    </ext:Button>
                                    <ext:Button ID="Button7" runat="server" Text="导出图片" Icon="Picture">
                                        <Listeners>
                                            <Click Handler="JavaScript:#{TabPanel1}.setActiveTab('Tab1');exportChart('PNG');" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Tabs>
                            <ext:Tab ID="Tab1" runat="server" Title="矩形图" Icon="ChartBar" AutoScroll="true" BodyStyle="padding: 2px;">
                                <Body>
                                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab2" runat="server" Title="数据" Icon="DatabaseGo" BodyStyle="padding: 2px;">
                                <Body>
                                    <ext:FitLayout ID="FitLayout2" runat="server">
                                        <ext:GridPanel ID="GridPanel2" runat="server" Title="" StoreID="Store1" Border="false"
                                            SelectionMemory="Disabled" TrackMouseOver="True" AutoExpandColumn="F_OnlineTimeS">
                                            <ColumnModel ID="ColumnModel2" runat="server">
                                                <Columns>
                                                    <ext:Column DataIndex="F_AreaCode" Header="地区编码">
                                                    </ext:Column>
                                                    <ext:Column ColumnID="F_Area" DataIndex="F_Area" Header="地区">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_OnlineTimeU" Header="平均在线人数">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_OnlineTimeA" Header="平均在线时长">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_OnlineTimeS" Header="总在线时长">
                                                    </ext:Column>
                                                </Columns>
                                            </ColumnModel>
                                            <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" />
                                            </SelectionModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolBar2" runat="server" PageSize="20" StoreID="Store1"
                                                    DisplayInfo="false" />
                                            </BottomBar>
                                            <LoadMask ShowMask="true" />
                                        </ext:GridPanel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                        </Tabs>
                    </ext:TabPanel>
                </Center>

            </ext:BorderLayout>
        </Body>
    </ext:ViewPort>
    </form>
</body>
</html>
