<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDay.aspx.cs" Inherits="WSS.Web.StatF.UserDay" %>

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
                this.FromDate.Value = DateTime.Now.AddDays(-300).ToShortDateString();
                this.ToDate.Value = DateTime.Now.ToShortDateString();
                SetChart();
            }

        }
        private void SetChart()
        {

            string sql = @"SELECT F_ID,F_Year, F_Month, F_Day, F_UserNum,  F_NewUserNum, F_NewActivationNum, F_NewLoginNum, F_ActiveUserNum, F_NewActiveUserNum, F_LoseUserNum,F_NewLoseUserNum, F_ReturnUserNum, F_NewReturnUserNum FROM T_UserBaseDig_Day WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='" + this.FromDate.Value + "'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='" + this.ToDate.Value + "'";


            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStr;
            ds = sp.Query(sql);

            if (ds == null)
            {
                return;
            }

            chartitle = "用户状态走势[" + Convert.ToDateTime(this.FromDate.Value).ToShortDateString() + "至" + Convert.ToDateTime(this.ToDate.Value).ToShortDateString() + "]";


            xmlData.Append("<chart caption='" + chartitle + "' xAxisName='日期' yAxisName='数量' showValues= '0' numberPrefix=''  aboutMenuItemLabel='关于 神龙游' aboutMenuItemLink='#' exportEnabled='1' exportAction='Download' exportShowMenuItem = '0'  exportfilename='SLYCharts' exportAtClient='0' exportHandler='Export_Handler/FCExporter.aspx' exportDialogMessage='数据转换中,请稍候...' animation='1'  numVDivLines='10' labelDisplay='ROTATE' rotateLabels='1' slantLabels='0'  >");

            string str0 = "<categories>";
            string str1 = "<dataset seriesName='新增用户'>";
            string str2 = "<dataset seriesName='新增激活用户'>";
            string str3 = "<dataset seriesName='活跃用户'>";
            string str4 = "<dataset seriesName='新增活跃用户'>";
            string str5 = "<dataset seriesName='流失用户'>";
            string str6 = "<dataset seriesName='新增流失用户'>";
            string str7 = "<dataset seriesName='回归用户'>";
            string str8 = "<dataset seriesName='新增回归用户'>";
            string str9 = "<dataset seriesName='用户总量'>";
            string str10 = "<dataset seriesName='新增进入用户'>";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DateTime dateS = Convert.ToDateTime(dr["F_Month"] + "-" + dr["F_Month"] + "-" + dr["F_Day"] + "");
                str0 += " <category label='" + dateS.ToString("MM-dd周ddd") + "' />";
                str1 += "<set value='" + dr["F_NewUserNum"] + "' />";
                str2 += "<set value='" + dr["F_NewActivationNum"] + "' />";
                str3 += "<set value='" + dr["F_ActiveUserNum"] + "' />";
                str4 += "<set value='" + dr["F_NewActiveUserNum"] + "' />";
                str5 += "<set value='" + dr["F_LoseUserNum"] + "' />";
                str6 += "<set value='" + dr["F_NewLoseUserNum"] + "' />";
                str7 += "<set value='" + dr["F_ReturnUserNum"] + "' />";
                str8 += "<set value='" + dr["F_NewReturnUserNum"] + "' />";
                str9 += "<set value='" + dr["F_UserNum"] + "' />";
                str10 += "<set value='" + dr["F_NewLoginNum"] + "' />";

            }
            str0 += " </categories>";
            str1 += "</dataset>";
            str2 += "</dataset>";
            str3 += "</dataset>";
            str4 += "</dataset>";
            str5 += "</dataset>";
            str6 += "</dataset>";
            str7 += "</dataset>";
            str8 += "</dataset>";
            str9 += "</dataset>";
            str10 += "</dataset>";

            xmlData.Append(str9 + str0 + str1 + str2 + str10 + str3 + str4 + str5 + str6 + str7 + str8);
            xmlData.Append("</chart>");

            Literal1.Text = FusionCharts.RenderChart("FusionCharts/MSLine.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "1000", "500", false, true);


            Store1.DataSource = ds.Tables[0].DefaultView;
            Store1.DataBind();
        }
        [AjaxMethod]
        public string GetXmlstr1()
        {
            SetChart();
            return xmlData.ToString();
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
    <ext:ScriptManager ID="ScriptManager1" runat="server" StateProvider="None" CleanResourceUrl="False"  />
    <ext:Store ID="Store1" runat="server" OnRefreshData="MyData_Refresh">
        <Reader>
            <ext:JsonReader ReaderID="F_ID">
                <Fields>
                    <ext:RecordField Name="F_ID" Type="int" />
                    <ext:RecordField Name="F_Year" Type="int" />
                    <ext:RecordField Name="F_Month" Type="int" />
                    <ext:RecordField Name="F_Day" Type="int" />
                    <ext:RecordField Name="F_UserNum" Type="int" />
                    <ext:RecordField Name="F_NewUserNum" Type="int" />
                    <ext:RecordField Name="F_NewActivationNum" Type="int" />
                    <ext:RecordField Name="F_ActiveUserNum" Type="int" />
                    <ext:RecordField Name="F_NewActiveUserNum" Type="int" />
                    <ext:RecordField Name="F_LoseUserNum" Type="int" />
                    <ext:RecordField Name="F_NewLoseUserNum" Type="int" />
                    <ext:RecordField Name="F_ReturnUserNum" Type="int" />
                    <ext:RecordField Name="F_NewReturnUserNum" Type="int" />
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
                           用户状态走势  &nbsp;&nbsp;&nbsp;活跃用户:每7天至少有3天登录;流失用户:7天以上无登录;回归用户:流失用户重新登录 &nbsp;[统计日期默认为最近30天]
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
                                            <Click Handler="if(#{FromDate}.isValid() && #{ToDate}.isValid()){ Coolite.AjaxMethods.GetXmlstr1({success: function(result){updateChart(result);},eventMask: {showMask: true,minDelay: 500,msg:'正在获取数据并执行操作...',customtarget:'TabPanel1'}});}else Ext.Msg.alert('提示', '请确认日期填写符合要求!');" />
                                        </Listeners>
                                    </ext:Button>
                                    <%--<ext:ToolbarFill ID="ToolbarFill2" runat="server" />--%>
                                    <ext:Button ID="Button5" runat="server" Text="刷新" Icon="ArrowRefresh">
                                        <Listeners>
                                            <Click Handler="Coolite.AjaxMethods.GetXmlstr1({success: function(result){updateChart(result);},eventMask: {showMask: true,minDelay: 500,msg:'正在获取数据并执行操作...',customtarget:'TabPanel1'}});" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button ID="Button6" runat="server" Text="导出Excel" Icon="PageExcel" AutoPostBack="True"
                                        OnClick="SaveExcel">
                                    </ext:Button>
                                    <ext:Button ID="Button7" runat="server" Text="导出图片" Icon="Picture">
                                        <Listeners>
                                            <Click Handler="JavaScript:#{TabPanel1}.setActiveTab('Tab1');exportChart('PNG')" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Tabs>
                            <ext:Tab ID="Tab1" runat="server" Title="折线图" Icon="ChartCurve" AutoScroll="true"
                                BodyStyle="padding: 2px;">
                                <Body>
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </Body>
                            </ext:Tab>
                            <ext:Tab ID="Tab2" runat="server" Title="数据" Icon="DatabaseGo" BodyStyle="padding: 2px;">
                                <Body>
                                    <%-- <ext:Panel ID="Panel3" runat="server" Height="300" Header="false">
                                        <Body>--%>
                                    <ext:FitLayout ID="FitLayout2" runat="server">
                                        <ext:GridPanel ID="GridPanel2" runat="server" Title="" StoreID="Store1" Border="false"
                                            SelectionMemory="Disabled" TrackMouseOver="True" AutoExpandColumn="F_NewReturnUserNum">
                                            <ColumnModel ID="ColumnModel2" runat="server">
                                                <Columns>
                                                    <ext:Column ColumnID="F_Year" DataIndex="F_Year" Header="年">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_Month" Header="月">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_Day" Header="日">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_UserNum" Header="用户总量">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_NewUserNum" Header="新增用户">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_NewActivationNum" Header="新增激活用户">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_ActiveUserNum" Header="活跃用户">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_NewActiveUserNum" Header="新增活跃用户">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_LoseUserNum" Header="流失用户">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_NewLoseUserNum" Header="新增流失用户">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_ReturnUserNum" Header="回归用户">
                                                    </ext:Column>
                                                    <ext:Column DataIndex="F_NewReturnUserNum" Header="新增回归用户">
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
                                    <%--</Body>
                                    </ext:Panel>--%>
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
