using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GameInCome
{
    public partial class FirstChargeAnalyze : Admin_Page
    {
        #region 属性
        //获取DigGameDB连接字符串
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        //获取GssDB连接字符串
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        //DigGameDB
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        //GSSDB
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        //UserCoreDB
        DbHelperSQLP DBHelperUserCoreDB = new DbHelperSQLP();

        DataSet ds;
        #endregion

        #region 事件
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取DigGameDB连接字符串
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            //获取GssDB连接字符串
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            //获取UserCoreDB连接字符串
            GetUserCoreDBString();
            if (!IsPostBack)
            {
                //初始化开始时间
                tboxTimeB.Text = DateTime.Now.AddDays(-6).ToString(SmallDateTimeFormat);
                //初始化结束时间
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                //绑定数据
                Bind();
                //绑定渠道
                BindChannel();
                //绑定大区
                BindBigZone();
            }
            ControlOutFile1.ControlOut = GridView1;
            ControlOutFile1.VisibleExcel = false;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            System.Text.EncodingInfo[] ss = System.Text.Encoding.GetEncodings();
            ControlDateSelect1.SetSelectDate(tboxTimeB.Text);
            Bind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Bind();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");
                }
            }
            catch (System.Exception ex)
            {
            }
        }
        protected void ControlChartSelect_SelectChanged(object sender, EventArgs e)
        {
            if (ControlChartSelect1.State == 0)
            {
                GridView1.Visible = true;
                ControlChart1.Visible = false;
            }
            else
            {
                GridView1.Visible = false;
                ControlChart1.Visible = true;
                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + "所有大区" + " " + DateTime.Now.ToString("yyyy-MM-dd"), ControlChartSelect1.State, false, 0, 0);
            }
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            Bind();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void Bind()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-6);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";
            string battleZone = string.Empty;
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }
            string sql = string.Empty;
            if (ddlType.SelectedValue == "0")
            {
                sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY PlayerLevelThen) OrderNum,PlayerLevelThen Type,COUNT(DISTINCT UserID) RechargeNumOfPeople FROM(SELECT UserID,PlayerLevelThen,TakeTime,OrderNum From(SELECT UserID,PlayerLevelThen,TakeTime,ROW_NUMBER() OVER(PARTITION BY USERID ORDER BY TakeTime) AS OrderNum FROM OPENQUERY ([LKSV_3_gsdata_db_{0}_{1}],'SELECT UserID,PlayerLevelThen,TakeTime FROM takegoldrecord'))Temp WHERE OrderNum=1 AND CONVERT(VARCHAR(100),TakeTime,23)>='{2}' AND CONVERT(VARCHAR(100),TakeTime,23)<='{3}')A GROUP BY PlayerLevelThen Order BY PlayerLevelThen", bigZone, battleZone, searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            }
            else if (ddlType.SelectedValue == "1")
            {
                sql = string.Format(
                   @"
                    SELECT ROW_NUMBER() OVER(ORDER BY Type) OrderNum,Type,RechargeNumOfPeople FROM
                    (
                    SELECT CASE WHEN Interval>=0 AND Interval<=4 THEN 1
                                WHEN Interval>=5 AND Interval<=9 THEN 2
                                WHEN Interval>=10 AND Interval<=14 THEN 3
                                WHEN Interval>=15 AND Interval<=19 THEN 4
                                WHEN Interval>=20 AND Interval<=24 THEN 5
                                WHEN Interval>=25 AND Interval<=29 THEN 6
                                WHEN Interval>=30 AND Interval<=34 THEN 7
                                WHEN Interval>=35 AND Interval<=39 THEN 8
                                WHEN Interval>=40 AND Interval<=44 THEN 9
                                WHEN Interval>=45 AND Interval<=49 THEN 10
                                WHEN Interval>=50 AND Interval<=54 THEN 11
                                WHEN Interval>=55 AND Interval<=59 THEN 12
                                WHEN Interval>=60 AND Interval<=64 THEN 13
                                WHEN Interval>=65 AND Interval<=69 THEN 14
                                WHEN Interval>=70 AND Interval<=74 THEN 15
                                WHEN Interval>=75 AND Interval<=79 THEN 16
                                WHEN Interval>=80 AND Interval<=84 THEN 17
                                WHEN Interval>=85 AND Interval<=89 THEN 18
                                WHEN Interval>=90 AND Interval<=94 THEN 19
                                WHEN Interval>=95 AND Interval<=99 THEN 20
                                WHEN Interval>=100 AND Interval<=104 THEN 21
                                WHEN Interval>=105 AND Interval<=109 THEN 22
                                WHEN Interval>=110 AND Interval<=114 THEN 23
                                WHEN Interval>=115 AND Interval<=119 THEN 24
                                WHEN Interval>=120 AND Interval<=149 THEN 25
                                WHEN Interval>=150 AND Interval<=179 THEN 26
                                WHEN Interval>=180 AND Interval<=209 THEN 27
                                WHEN Interval>=210 AND Interval<=239 THEN 28
                                WHEN Interval>=240 THEN 29
                                ELSE '' END TYPE,
                         COUNT(DISTINCT GlobalID) RechargeNumOfPeople FROM
                    (
                    SELECT GlobalID,TakeTime,F_CreateTime,DATEDIFF(N,F_CreateTime,TakeTime) Interval FROM 
                    (
                    SELECT GlobalID,TakeTime,OrderNum From
                    (
                    SELECT GlobalID,TakeTime,ROW_NUMBER() OVER(PARTITION BY GlobalID ORDER BY TakeTime) AS OrderNum FROM OPENQUERY ([LKSV_3_gsdata_db_{0}_{1}],
                    'SELECT GlobalID,TakeTime FROM takegoldrecord'))Temp
                    WHERE OrderNum=1 AND CONVERT(VARCHAR(100),TakeTime,23)>='{2}' AND CONVERT(VARCHAR(100),TakeTime,23)<='{3}'
                    )A
                    LEFT JOIN 
                    (
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_0] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    UNION ALL
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_1] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    UNION ALL
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_2] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    UNION ALL
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_3] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    UNION ALL
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_4] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    )B ON A.GlobalID=B.[F_RoleID]
                    WHERE F_CreateTime IS NOT NULL)C
                    GROUP BY 
                    CASE WHEN Interval>=0 AND Interval<=4 THEN 1
                            WHEN Interval>=5 AND Interval<=9 THEN 2
                            WHEN Interval>=10 AND Interval<=14 THEN 3
                            WHEN Interval>=15 AND Interval<=19 THEN 4
                            WHEN Interval>=20 AND Interval<=24 THEN 5
                            WHEN Interval>=25 AND Interval<=29 THEN 6
                            WHEN Interval>=30 AND Interval<=34 THEN 7
                            WHEN Interval>=35 AND Interval<=39 THEN 8
                            WHEN Interval>=40 AND Interval<=44 THEN 9
                            WHEN Interval>=45 AND Interval<=49 THEN 10
                            WHEN Interval>=50 AND Interval<=54 THEN 11
                            WHEN Interval>=55 AND Interval<=59 THEN 12
                            WHEN Interval>=60 AND Interval<=64 THEN 13
                            WHEN Interval>=65 AND Interval<=69 THEN 14
                            WHEN Interval>=70 AND Interval<=74 THEN 15
                            WHEN Interval>=75 AND Interval<=79 THEN 16
                            WHEN Interval>=80 AND Interval<=84 THEN 17
                            WHEN Interval>=85 AND Interval<=89 THEN 18
                            WHEN Interval>=90 AND Interval<=94 THEN 19
                            WHEN Interval>=95 AND Interval<=99 THEN 20
                            WHEN Interval>=100 AND Interval<=104 THEN 21
                            WHEN Interval>=105 AND Interval<=109 THEN 22
                            WHEN Interval>=110 AND Interval<=114 THEN 23
                            WHEN Interval>=115 AND Interval<=119 THEN 24
                            WHEN Interval>=120 AND Interval<=149 THEN 25
                            WHEN Interval>=150 AND Interval<=179 THEN 26
                            WHEN Interval>=180 AND Interval<=209 THEN 27
                            WHEN Interval>=210 AND Interval<=239 THEN 28
                            WHEN Interval>=240 THEN 29
                            ELSE '' END) D
                            ORDER BY TYPE
                    ", bigZone, battleZone, searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            }
            else if (ddlType.SelectedValue == "2")
            {
                sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY Type) OrderNum,Type,RechargeNumOfPeople FROM(SELECT CASE WHEN CostMoney>=0 AND CostMoney<=9 THEN 1 WHEN CostMoney>=10 AND CostMoney<=29 THEN 2 WHEN CostMoney>=30 AND CostMoney<=49 THEN 3 WHEN CostMoney>=50 AND CostMoney<=99 THEN 4 WHEN CostMoney>=100 AND CostMoney<=199 THEN 5 WHEN CostMoney>=200 AND CostMoney<=299 THEN 6 WHEN CostMoney>=300 AND CostMoney<=499 THEN 7 WHEN CostMoney>=500 AND CostMoney<=799 THEN 8 WHEN CostMoney>=800 AND CostMoney<=999 THEN 9 WHEN CostMoney>=1000 AND CostMoney<=1999 THEN 10 WHEN CostMoney>=2000 AND CostMoney<=4999 THEN 11 WHEN CostMoney>=5000 AND CostMoney<=9999 THEN 12 WHEN CostMoney>=10000 THEN 13 ELSE '' END AS Type,COUNT(DISTINCT UserID) RechargeNumOfPeople FROM (SELECT UserID,CostMoney,TakeTime,ROW_NUMBER() OVER(PARTITION BY USERID ORDER BY TakeTime) AS OrderNum FROM OPENQUERY ([LKSV_3_gsdata_db_{0}_{1}],'SELECT UserID,CostMoney,TakeTime FROM takegoldrecord'))Temp WHERE OrderNum=1 AND CONVERT(VARCHAR(100),TakeTime,23)>='{2}' AND CONVERT(VARCHAR(100),TakeTime,23)<='{3}' GROUP BY CASE WHEN CostMoney>=0 AND CostMoney<=9 THEN 1 WHEN CostMoney>=10 AND CostMoney<=29 THEN 2 WHEN CostMoney>=30 AND CostMoney<=49 THEN 3 WHEN CostMoney>=50 AND CostMoney<=99 THEN 4 WHEN CostMoney>=100 AND CostMoney<=199 THEN 5 WHEN CostMoney>=200 AND CostMoney<=299 THEN 6 WHEN CostMoney>=300 AND CostMoney<=499 THEN 7 WHEN CostMoney>=500 AND CostMoney<=799 THEN 8 WHEN CostMoney>=800 AND CostMoney<=999 THEN 9 WHEN CostMoney>=1000 AND CostMoney<=1999 THEN 10 WHEN CostMoney>=2000 AND CostMoney<=4999 THEN 11 WHEN CostMoney>=5000 AND CostMoney<=9999 THEN 12 WHEN CostMoney>=10000 THEN 13 ELSE '' END )A ORDER BY TYPE", bigZone, battleZone, searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            }
            else
            {
                //显示等级
                sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY PlayerLevelThen) OrderNum,PlayerLevelThen Type,COUNT(DISTINCT UserID) RechargeNumOfPeople FROM(SELECT UserID,PlayerLevelThen,TakeTime,OrderNum From(SELECT UserID,PlayerLevelThen,TakeTime,ROW_NUMBER() OVER(PARTITION BY USERID ORDER BY TakeTime) AS OrderNum FROM OPENQUERY ([LKSV_3_gsdata_db_{0}_{1}],'SELECT UserID,PlayerLevelThen,TakeTime FROM takegoldrecord'))Temp WHERE OrderNum=1 AND CONVERT(VARCHAR(100),TakeTime,23)>='{2}' AND CONVERT(VARCHAR(100),TakeTime,23)<='{3}')A GROUP BY PlayerLevelThen Order BY PlayerLevelThen", bigZone, battleZone, searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            }

            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                DataView myView;
                if (ddlType.SelectedValue == "1")
                {
                    DataTable dt = new DataTable();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt.Columns.Add("OrderNum", System.Type.GetType("System.String"));
                        dt.Columns.Add("Type", System.Type.GetType("System.String"));
                        dt.Columns.Add("RechargeNumOfPeople", System.Type.GetType("System.String"));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["OrderNum"] = ds.Tables[0].Rows[i]["OrderNum"];
                            newRow["Type"] = GetInterval(ds.Tables[0].Rows[i]["Type"] + "");
                            newRow["RechargeNumOfPeople"] = ds.Tables[0].Rows[i]["RechargeNumOfPeople"];
                            dt.Rows.Add(newRow);
                        }
                    }
                    myView = dt.DefaultView;
                }
                else if(ddlType.SelectedValue == "2")
                {
                    DataTable dt = new DataTable();
                    if(ds!=null&&ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0)
                    {
                        dt.Columns.Add("OrderNum", System.Type.GetType("System.String"));
                        dt.Columns.Add("Type", System.Type.GetType("System.String"));
                        dt.Columns.Add("RechargeNumOfPeople", System.Type.GetType("System.String"));
                        for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["OrderNum"] = ds.Tables[0].Rows[i]["OrderNum"];
                            newRow["Type"] = GetPrice(ds.Tables[0].Rows[i]["Type"] + "");
                            newRow["RechargeNumOfPeople"] = ds.Tables[0].Rows[i]["RechargeNumOfPeople"];
                            dt.Rows.Add(newRow);
                        }
                    }
                    myView = dt.DefaultView;
                }
                else
                {
                    myView = ds.Tables[0].DefaultView;
                }
                
                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                    myView.AddNew();
                }
                else
                {
                    lblerro.Visible = false;
                }
                GridView1.DataSource = myView;
                GridView1.DataBind();
            }
            catch (System.Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ExportExcel(object sender, EventArgs e)
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-6);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";
            string battleZone = string.Empty;
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }
            string sql = string.Empty;
            if (ddlType.SelectedValue == "0")
            {
                sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY PlayerLevelThen) 序号,PlayerLevelThen 类型,COUNT(DISTINCT UserID) 人数 FROM(SELECT UserID,PlayerLevelThen,TakeTime,OrderNum From(SELECT UserID,PlayerLevelThen,TakeTime,ROW_NUMBER() OVER(PARTITION BY USERID ORDER BY TakeTime) AS OrderNum FROM OPENQUERY ([LKSV_3_gsdata_db_{0}_{1}],'SELECT UserID,PlayerLevelThen,TakeTime FROM takegoldrecord'))Temp WHERE OrderNum=1 AND CONVERT(VARCHAR(100),TakeTime,23)>='{2}' AND CONVERT(VARCHAR(100),TakeTime,23)<='{3}')A GROUP BY PlayerLevelThen Order BY PlayerLevelThen", bigZone, battleZone, searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            }
            else if (ddlType.SelectedValue == "1")
            {
                sql = string.Format(
                   @"
                    SELECT ROW_NUMBER() OVER(ORDER BY Type) OrderNum,Type,RechargeNumOfPeople FROM
                    (
                    SELECT CASE WHEN Interval>=0 AND Interval<=4 THEN 1
                                WHEN Interval>=5 AND Interval<=9 THEN 2
                                WHEN Interval>=10 AND Interval<=14 THEN 3
                                WHEN Interval>=15 AND Interval<=19 THEN 4
                                WHEN Interval>=20 AND Interval<=24 THEN 5
                                WHEN Interval>=25 AND Interval<=29 THEN 6
                                WHEN Interval>=30 AND Interval<=34 THEN 7
                                WHEN Interval>=35 AND Interval<=39 THEN 8
                                WHEN Interval>=40 AND Interval<=44 THEN 9
                                WHEN Interval>=45 AND Interval<=49 THEN 10
                                WHEN Interval>=50 AND Interval<=54 THEN 11
                                WHEN Interval>=55 AND Interval<=59 THEN 12
                                WHEN Interval>=60 AND Interval<=64 THEN 13
                                WHEN Interval>=65 AND Interval<=69 THEN 14
                                WHEN Interval>=70 AND Interval<=74 THEN 15
                                WHEN Interval>=75 AND Interval<=79 THEN 16
                                WHEN Interval>=80 AND Interval<=84 THEN 17
                                WHEN Interval>=85 AND Interval<=89 THEN 18
                                WHEN Interval>=90 AND Interval<=94 THEN 19
                                WHEN Interval>=95 AND Interval<=99 THEN 20
                                WHEN Interval>=100 AND Interval<=104 THEN 21
                                WHEN Interval>=105 AND Interval<=109 THEN 22
                                WHEN Interval>=110 AND Interval<=114 THEN 23
                                WHEN Interval>=115 AND Interval<=119 THEN 24
                                WHEN Interval>=120 AND Interval<=149 THEN 25
                                WHEN Interval>=150 AND Interval<=179 THEN 26
                                WHEN Interval>=180 AND Interval<=209 THEN 27
                                WHEN Interval>=210 AND Interval<=239 THEN 28
                                WHEN Interval>=240 THEN 29
                                ELSE '' END TYPE,
                         COUNT(DISTINCT GlobalID) RechargeNumOfPeople FROM
                    (
                    SELECT GlobalID,TakeTime,F_CreateTime,DATEDIFF(N,F_CreateTime,TakeTime) Interval FROM 
                    (
                    SELECT GlobalID,TakeTime,OrderNum From
                    (
                    SELECT GlobalID,TakeTime,ROW_NUMBER() OVER(PARTITION BY GlobalID ORDER BY TakeTime) AS OrderNum FROM OPENQUERY ([LKSV_3_gsdata_db_{0}_{1}],
                    'SELECT GlobalID,TakeTime FROM takegoldrecord'))Temp
                    WHERE OrderNum=1 AND CONVERT(VARCHAR(100),TakeTime,23)>='{2}' AND CONVERT(VARCHAR(100),TakeTime,23)<='{3}'
                    )A
                    LEFT JOIN 
                    (
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_0] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    UNION ALL
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_1] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    UNION ALL
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_2] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    UNION ALL
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_3] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    UNION ALL
                    SELECT [F_RoleID],[F_CreateTime] FROM LKSV_2_GameCoreDB_0_1.GameCoreDB.[dbo].[T_RoleBaseData_4] WHERE CONVERT(VARCHAR(100),[F_CreateTime],23)>='{2}' AND CONVERT(VARCHAR(100),[F_CreateTime],23)<='{3}'
                    )B ON A.GlobalID=B.[F_RoleID]
                    WHERE F_CreateTime IS NOT NULL)C
                    GROUP BY 
                    CASE WHEN Interval>=0 AND Interval<=4 THEN 1
                            WHEN Interval>=5 AND Interval<=9 THEN 2
                            WHEN Interval>=10 AND Interval<=14 THEN 3
                            WHEN Interval>=15 AND Interval<=19 THEN 4
                            WHEN Interval>=20 AND Interval<=24 THEN 5
                            WHEN Interval>=25 AND Interval<=29 THEN 6
                            WHEN Interval>=30 AND Interval<=34 THEN 7
                            WHEN Interval>=35 AND Interval<=39 THEN 8
                            WHEN Interval>=40 AND Interval<=44 THEN 9
                            WHEN Interval>=45 AND Interval<=49 THEN 10
                            WHEN Interval>=50 AND Interval<=54 THEN 11
                            WHEN Interval>=55 AND Interval<=59 THEN 12
                            WHEN Interval>=60 AND Interval<=64 THEN 13
                            WHEN Interval>=65 AND Interval<=69 THEN 14
                            WHEN Interval>=70 AND Interval<=74 THEN 15
                            WHEN Interval>=75 AND Interval<=79 THEN 16
                            WHEN Interval>=80 AND Interval<=84 THEN 17
                            WHEN Interval>=85 AND Interval<=89 THEN 18
                            WHEN Interval>=90 AND Interval<=94 THEN 19
                            WHEN Interval>=95 AND Interval<=99 THEN 20
                            WHEN Interval>=100 AND Interval<=104 THEN 21
                            WHEN Interval>=105 AND Interval<=109 THEN 22
                            WHEN Interval>=110 AND Interval<=114 THEN 23
                            WHEN Interval>=115 AND Interval<=119 THEN 24
                            WHEN Interval>=120 AND Interval<=149 THEN 25
                            WHEN Interval>=150 AND Interval<=179 THEN 26
                            WHEN Interval>=180 AND Interval<=209 THEN 27
                            WHEN Interval>=210 AND Interval<=239 THEN 28
                            WHEN Interval>=240 THEN 29
                            ELSE '' END) D
                            ORDER BY TYPE
                    ", bigZone, battleZone, searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            }
            else if (ddlType.SelectedValue == "2")
            {
                sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY Type) OrderNum,Type,RechargeNumOfPeople FROM(SELECT CASE WHEN CostMoney>=0 AND CostMoney<=9 THEN 1 WHEN CostMoney>=10 AND CostMoney<=29 THEN 2 WHEN CostMoney>=30 AND CostMoney<=49 THEN 3 WHEN CostMoney>=50 AND CostMoney<=99 THEN 4 WHEN CostMoney>=100 AND CostMoney<=199 THEN 5 WHEN CostMoney>=200 AND CostMoney<=299 THEN 6 WHEN CostMoney>=300 AND CostMoney<=499 THEN 7 WHEN CostMoney>=500 AND CostMoney<=799 THEN 8 WHEN CostMoney>=800 AND CostMoney<=999 THEN 9 WHEN CostMoney>=1000 AND CostMoney<=1999 THEN 10 WHEN CostMoney>=2000 AND CostMoney<=4999 THEN 11 WHEN CostMoney>=5000 AND CostMoney<=9999 THEN 12 WHEN CostMoney>=10000 THEN 13 ELSE '' END AS Type,COUNT(DISTINCT UserID) RechargeNumOfPeople FROM (SELECT UserID,CostMoney,TakeTime,ROW_NUMBER() OVER(PARTITION BY USERID ORDER BY TakeTime) AS OrderNum FROM OPENQUERY ([LKSV_3_gsdata_db_{0}_{1}],'SELECT UserID,CostMoney,TakeTime FROM takegoldrecord'))Temp WHERE OrderNum=1 AND CONVERT(VARCHAR(100),TakeTime,23)>='{2}' AND CONVERT(VARCHAR(100),TakeTime,23)<='{3}' GROUP BY CASE WHEN CostMoney>=0 AND CostMoney<=9 THEN 1 WHEN CostMoney>=10 AND CostMoney<=29 THEN 2 WHEN CostMoney>=30 AND CostMoney<=49 THEN 3 WHEN CostMoney>=50 AND CostMoney<=99 THEN 4 WHEN CostMoney>=100 AND CostMoney<=199 THEN 5 WHEN CostMoney>=200 AND CostMoney<=299 THEN 6 WHEN CostMoney>=300 AND CostMoney<=499 THEN 7 WHEN CostMoney>=500 AND CostMoney<=799 THEN 8 WHEN CostMoney>=800 AND CostMoney<=999 THEN 9 WHEN CostMoney>=1000 AND CostMoney<=1999 THEN 10 WHEN CostMoney>=2000 AND CostMoney<=4999 THEN 11 WHEN CostMoney>=5000 AND CostMoney<=9999 THEN 12 WHEN CostMoney>=10000 THEN 13 ELSE '' END )A ORDER BY TYPE", bigZone, battleZone, searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            }
            else
            {
                //显示等级
                sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY PlayerLevelThen) 序号,PlayerLevelThen 类型,COUNT(DISTINCT UserID) 人数 FROM(SELECT UserID,PlayerLevelThen,TakeTime,OrderNum From(SELECT UserID,PlayerLevelThen,TakeTime,ROW_NUMBER() OVER(PARTITION BY USERID ORDER BY TakeTime) AS OrderNum FROM OPENQUERY ([LKSV_3_gsdata_db_{0}_{1}],'SELECT UserID,PlayerLevelThen,TakeTime FROM takegoldrecord'))Temp WHERE OrderNum=1 AND CONVERT(VARCHAR(100),TakeTime,23)>='{2}' AND CONVERT(VARCHAR(100),TakeTime,23)<='{3}')A GROUP BY PlayerLevelThen Order BY PlayerLevelThen", bigZone, battleZone, searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            }


            ds = DBHelperDigGameDB.Query(sql);
            DataView myView;
            if (ddlType.SelectedValue == "1")
            {
                DataTable dt = new DataTable();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt.Columns.Add("序号", System.Type.GetType("System.String"));
                    dt.Columns.Add("类型", System.Type.GetType("System.String"));
                    dt.Columns.Add("人数", System.Type.GetType("System.String"));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["序号"] = ds.Tables[0].Rows[i]["OrderNum"];
                        newRow["类型"] = GetInterval(ds.Tables[0].Rows[i]["Type"] + "");
                        newRow["人数"] = ds.Tables[0].Rows[i]["RechargeNumOfPeople"];
                        dt.Rows.Add(newRow);
                    }
                }
                DataSet dsNew = new DataSet();
                dsNew.Tables.Add(dt);
                ExportExcelHelper.ExportDataSet(dsNew);
            }
            else if (ddlType.SelectedValue == "2")
            {
                DataTable dt = new DataTable();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt.Columns.Add("序号", System.Type.GetType("System.String"));
                    dt.Columns.Add("类型", System.Type.GetType("System.String"));
                    dt.Columns.Add("人数", System.Type.GetType("System.String"));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["序号"] = ds.Tables[0].Rows[i]["OrderNum"];
                        newRow["类型"] = GetPrice(ds.Tables[0].Rows[i]["Type"] + "");
                        newRow["人数"] = ds.Tables[0].Rows[i]["RechargeNumOfPeople"];
                        dt.Rows.Add(newRow);
                    }
                }
                DataSet dsNew = new DataSet();
                dsNew.Tables.Add(dt);
                ExportExcelHelper.ExportDataSet(dsNew);
            }
            else
            {
                ExportExcelHelper.ExportDataSet(ds);
            }
        }
        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
        }
        /// <summary>
        /// 绑定渠道
        /// </summary>
        public void BindChannel()
        {
            string sql = "SELECT DISTINCT F_ChannelID FROM T_Deposit_Verify_Result_Log ORDER BY F_ChannelID ";
            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                this.ddlChannel.DataSource = ds;
                this.ddlChannel.DataTextField = "F_ChannelID";
                this.ddlChannel.DataValueField = "F_ChannelID";
                this.ddlChannel.DataBind();
                this.ddlChannel.Items.Insert(0, new ListItem("全部", ""));
            }
            catch (System.Exception ex)
            {
                ddlChannel.Items.Clear();
                ddlChannel.Items.Add(new ListItem("", ""));
            }
        }
        /// <summary>
        /// 绑定大区
        /// </summary>
        public void BindBigZone()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;
            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = 1000) AND (F_IsUsed = 1)";
            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();
                newdr["F_Name"] = App_GlobalResources.Language.LblAllBigZone;
                newdr["F_ValueGame"] = "";
                ds.Tables[0].Rows.InsertAt(newdr, 0);
                this.DropDownListArea1.DataSource = ds;
                this.DropDownListArea1.DataTextField = "F_Name";
                this.DropDownListArea1.DataValueField = "F_ValueGame";
                this.DropDownListArea1.DataBind();
            }
            catch (System.Exception ex)
            {
                DropDownListArea1.Items.Clear();
                DropDownListArea1.Items.Add(new ListItem(App_GlobalResources.Language.LblAllBigZone, ""));
            }
        }
        /// <summary>
        /// 绑定战区
        /// </summary>
        /// <param name="id"></param>
        public void BindBattleZone(string id)
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = " + id + ") AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();
                newdr["F_Name"] = App_GlobalResources.Language.LblAllZone;

                newdr["F_ValueGame"] = "";
                ds.Tables[0].Rows.InsertAt(newdr, 0);
                this.DropDownListArea2.DataSource = ds;
                this.DropDownListArea2.DataTextField = "F_Name";
                this.DropDownListArea2.DataValueField = "F_ValueGame";
                this.DropDownListArea2.DataBind();

            }
            catch (System.Exception ex)
            {
                DropDownListArea2.Items.Clear();
                DropDownListArea2.Items.Add(new ListItem(App_GlobalResources.Language.LblAllZone, ""));
            }
        }
        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListArea2.Items.Clear();
            BindBattleZone(DropDownListArea1.SelectedValue.Split(',')[0]);
        }
        protected string GetPrice(string value)
        {
            switch (value)
            {
                case "1":
                    return "0~9";
                case "2":
                    return "10~29";
                case "3":
                    return "30~49";
                case "4":
                    return "50~99";
                case "5":
                    return "100~199";
                case "6":
                    return "200~299";
                case "7":
                    return "300~499";
                case "8":
                    return "500~799";
                case "9":
                    return "800~999";
                case "10":
                    return "1000~1999";
                case "11":
                    return "2000~4999";
                case "12":
                    return "5000~9999";
                case "13":
                    return "10000";
                default:
                    return value;
            }
        }
        protected string GetInterval(string value)
        {
            switch (value)
            {
                case "1":
                    return "0~4 min";
                case "2":
                    return "5~9 min";
                case "3":
                    return "10~14 min";
                case "4":
                    return "15~19 min";
                case "5":
                    return "20~24 min";
                case "6":
                    return "25~29 min";
                case "7":
                    return "30~34 min";
                case "8":
                    return "35~39 min";
                case "9":
                    return "40~44 min";
                case "10":
                    return "45~49 min";
                case "11":
                    return "50~54 min";
                case "12":
                    return "55~59 min";
                case "13":
                    return "60~64 min";
                case "14":
                    return "65~69 min";
                case "15":
                    return "70~74 min";
                case "16":
                    return "75~79 min";
                case "17":
                    return "80~84 min";
                case "18":
                    return "85~89 min";
                case "19":
                    return "90~94 min";
                case "20":
                    return "95~99 min";
                case "21":
                    return "100~104 min";
                case "22":
                    return "105~109 min";
                case "23":
                    return "110~114 min";
                case "24":
                    return "115~119 min";
                case "25":
                    return "120~149 min";
                case "26":
                    return "150~179 min";
                case "27":
                    return "180~209 min";
                case "28":
                    return "210~239 min";
                case "29":
                    return "240 min";
                default:
                    return value;
            }
        }
        #endregion

        #region 转到触发方法
        protected void Go_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = int.Parse(((TextBox)GridView1.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            Bind();   //重新绑定GridView
        }
        #endregion

        #region 分页触发方法
        protected void GvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Bind();  //重新绑定GridView
        }
        #endregion
    }
}
