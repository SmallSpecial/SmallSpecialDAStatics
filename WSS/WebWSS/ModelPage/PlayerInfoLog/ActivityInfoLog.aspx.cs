using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.PlayerInfoLog
{
    public partial class ActivityInfoLog : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

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
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            if (!IsPostBack)
            {
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                bind();
                BindDBigZone();
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
            bind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListArea2.Items.Clear();
            BindBattleZone(DropDownListArea1.SelectedValue.Split(',')[0]);
        }
        /// <summary>
        /// 在单击某个用于对列进行排序的超链接时发生
        /// </summary>
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sPage = e.SortExpression;
            if (ViewState["SortOrder"].ToString() == sPage)
            {
                if (ViewState["OrderDire"].ToString() == "DESC")
                    ViewState["OrderDire"] = "ASC";
                else
                    ViewState["OrderDire"] = "DESC";
            }
            else
            {
                ViewState["SortOrder"] = e.SortExpression;
            }
            bind();
        }
        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
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
                //ControlChart1.SetChart(GridView2, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
            }
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            bind();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }

            LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            if (DropDownListArea2.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            }

            DateTime searchdateB = DateTime.Now;
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            int num = searchdateE.Subtract(searchdateB).Days;
            if (num < 0)
            {
                Response.Write("<Script Language=JavaScript>alert('结束时间不能晚于开始时间！');</Script>");
                return;
            }
            string sqlWhere = GetSelectValue(ddlPara.SelectedValue);

            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";//大区
            string battleZone = string.Empty;//战区
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }

            string sql = string.Format("SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_{0}_{1}],'", bigZone, battleZone);

            for (int i = 0; i <= num; i++)
            {
                string strSTime = searchdateB.AddDays(i).ToString("yyyy_MM_dd");
                if (i == 0)
                    sql += string.Format("SELECT \"{0}\" AS DateTime,PARA_1,COUNT(DISTINCT CID) AS PeopleNum,COUNT(CID) AS Num FROM {1}_other_log WHERE OPID=50080 {2} GROUP BY PARA_1", strSTime, strSTime,sqlWhere);
                else
                    sql += string.Format(" UNION ALL SELECT \"{0}\" AS DateTime,PARA_1,COUNT(DISTINCT CID) AS PeopleNum,COUNT(CID) AS Num FROM {1}_other_log WHERE OPID=50080 {2} GROUP BY PARA_1", strSTime, strSTime, sqlWhere);
            }

            sql += "')";
            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                DataView myView = ds.Tables[0].DefaultView;
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

            LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            if (DropDownListArea2.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            }

            DateTime searchdateB = DateTime.Now;
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            int num = searchdateE.Subtract(searchdateB).Days;
            if (num < 0)
            {
                Response.Write("<Script Language=JavaScript>alert('结束时间不能晚于开始时间！');</Script>");
                return;
            }
            string sqlWhere = GetSelectValue(ddlPara.SelectedValue);

            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";//大区
            string battleZone = string.Empty;//战区
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }

            string sql = string.Format("SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_{0}_{1}],'", bigZone, battleZone);

            for (int i = 0; i <= num; i++)
            {
                string strSTime = searchdateB.AddDays(i).ToString("yyyy_MM_dd");
                if (i == 0)
                    sql += string.Format("SELECT \"{0}\" AS DateTime,PARA_1,COUNT(DISTINCT CID) AS PeopleNum,COUNT(CID) AS Num FROM {1}_other_log WHERE OPID=50080 {2} GROUP BY PARA_1", strSTime, strSTime,sqlWhere);
                else
                    sql += string.Format(" UNION ALL SELECT \"{0}\" AS DateTime,PARA_1,COUNT(DISTINCT CID) AS PeopleNum,COUNT(CID) AS Num FROM {1}_other_log WHERE OPID=50080 {2} GROUP BY PARA_1", strSTime, strSTime, sqlWhere);
            }

            sql += "')";
         
                ds = DBHelperDigGameDB.Query(sql);
            ExportExcelHelper.ExportDataSet(ds);
        }
        protected string GetSelectValue(string value)
        {
            if (ddlPara.SelectedValue == "MHFX")//蛮荒废墟
            {
                return " AND (PARA_1=21 OR PARA_1=22 OR PARA_1=23)";
            }
            else if (ddlPara.SelectedValue == "GHCJ")//公会场景
            {
                return " AND (PARA_1=30 OR PARA_1=31)";
            }
            else if (ddlPara.SelectedValue == "LYYZ")//炼狱远征
            {
                return " AND (PARA_1=40 OR PARA_1=41 OR PARA_1=42 OR PARA_1=43 OR PARA_1=44 OR PARA_1=45)";
            }
            else if (ddlPara.SelectedValue == "CWD") //宠物岛
            {
                return " AND (PARA_1=50 OR PARA_1=51)";
            }
            else if (ddlPara.SelectedValue == "GHJDZZBCJ") //公会据点战准备场景
            {
                return " AND (PARA_1=60 OR PARA_1=61 OR PARA_1=62)";
            }
            else if (ddlPara.SelectedValue == "GHJDZ") //公会据点战
            {
                return " AND (PARA_1=63 OR PARA_1=64 OR PARA_1=65 OR PARA_1=66 OR PARA_1=67 OR PARA_1=68)";
            }
            else if (ddlPara.SelectedValue == "JJC") //竞技场
            {
                return " AND (PARA_1=71 OR PARA_1=72 OR PARA_1=73)";
            }
            else if (ddlPara.SelectedValue == "MWFB") //魔王副本
            {
                return " AND (PARA_1=80 OR PARA_1=81 OR PARA_1=82 OR PARA_1=83 OR PARA_1=84 OR PARA_1=85 OR PARA_1=86 OR PARA_1=87 OR PARA_1=88 OR PARA_1=89 OR PARA_1=90 OR PARA_1=91 OR PARA_1=92 OR PARA_1=93 OR PARA_1=94 OR PARA_1=95 OR PARA_1=96 OR PARA_1=97)";
            }
            else if (ddlPara.SelectedValue == "XZJQB") //小镇-剧情本
            {
                return " AND (PARA_1=101 OR PARA_1=102)";
            }
            else if (ddlPara.SelectedValue == "SMJQB") //沙漠-剧情本
            {
                return " AND (PARA_1=103 OR PARA_1=112)";
            }
            else if (ddlPara.SelectedValue == "HDJQB") //海底-剧情本
            {
                return " AND (PARA_1=106 OR PARA_1=111)";
            }
            else if (ddlPara.SelectedValue == "SLJQB") //森林-剧情本
            {
                return " AND (PARA_1=114 OR PARA_1=115)";
            }
            else if (ddlPara.SelectedValue == "DGJQB") //地宫-剧情本
            {
                return " AND (PARA_1=121 OR PARA_1=122)";
            }
            else if (ddlPara.SelectedValue == "KCJQB") //矿场-剧情本
            {
                return " AND (PARA_1=124 OR PARA_1=125)";
            }
            else if (ddlPara.SelectedValue == "BYJQB") //冰原-剧情副本
            {
                return " AND (PARA_1=131 OR PARA_1=132 OR PARA_1=133 OR PARA_1=134 OR PARA_1=135 OR PARA_1=136)";
            }
            else if (ddlPara.SelectedValue == "LMDS") //猎魔大师
            {
                return " AND (PARA_1=150 OR PARA_1=151 OR PARA_1=152 OR PARA_1=153 OR PARA_1=154 OR PARA_1=155 OR PARA_1=156 OR PARA_1=157 OR PARA_1=158 OR PARA_1=159 OR PARA_1=160 OR PARA_1=161 OR PARA_1=162 OR PARA_1=163 OR PARA_1=164 OR PARA_1=165)";
            }
            else if (ddlPara.SelectedValue == "SQRC") //神器日常活动
            {
                return " AND (PARA_1=180 OR PARA_1=181 OR PARA_1=182 OR PARA_1=183 OR PARA_1=184 OR PARA_1=185 OR PARA_1=186 OR PARA_1=187 OR PARA_1=188 OR PARA_1=189 OR PARA_1=190)";
            }
            else if (ddlPara.SelectedValue == "SZSL") //神之试炼
            {
                return " AND (PARA_1=211 OR PARA_1=213 OR PARA_1=215 OR PARA_1=217 OR PARA_1=219)";
            }
            else if (ddlPara.SelectedValue == "GHGWRQ") //（公会）怪物入侵
            {
                return " AND (PARA_1=241 OR PARA_1=242 OR PARA_1=243)";
            }
            else
            {
                return string.Format(" AND PARA_1={0}", ddlPara.SelectedValue);
            }
        }
        /// <summary>
        /// 绑定大区
        /// </summary>
        public void BindDBigZone()
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
        protected string GetActiveName(string value)
        {
            switch(value)
            {
                case "2":
                    return "迷雾森林";
                case "3":
                    return "神之领域";
                case "4":
                    return "幽暗矿山";
                case "5":
                    return "蛮荒沙漠";
                case "6":
                    return "守望镇（新手村）";
                case "7":
                    return "遗忘废墟(模拟战场)";
                case "8":
                    return "永恒之城(主城）";
                case "9":
                    return "诅咒之海";
                case "10":
                    return "悲鸣地宫";
                case "11":
                    return "永夜平原";
                case "12":
                    return "天堂圣域";
                case "21":
                    return "蛮荒废墟-危险场景1(26-39)";
                case "22":
                    return "蛮荒废墟-危险场景2(40-59)";
                case "23":
                    return "蛮荒废墟-危险场景3(60-80)";
                case "26":
                    return "守护阵营边境";
                case "27":
                    return "纷争阵营边境";
                case "30":
                    return "公会场景";
                case "31":
                    return "公会场景";
                case "40":
                    return "炼狱远征30";
                case "41":
                    return "炼狱远征40";
                case "42":
                    return "炼狱远征50";
                case "43":
                    return "炼狱远征60";
                case "44":
                    return "炼狱远征70";
                case "45":
                    return "炼狱远征80";
                case "50":
                    return "宠物岛(副本)26";
                case "51":
                    return "宠物岛25级";
                case "60":
                    return "公会据点战准备场景";
                case "61":
                    return "公会据点战准备场景2";
                case "62":
                    return "公会据点战准备场景3";
                case "63":
                    return "公会据点战";
                case "64":
                    return "公会据点战";
                case "65":
                    return "公会据点战";
                case "66":
                    return "公会据点战";
                case "67":
                    return "公会据点战";
                case "68":
                    return "公会据点战";
                case "71":
                    return "竞技场1 35级";
                case "72":
                    return "竞技场2 35级";
                case "73":
                    return "竞技场3 35级";
                case "80":
                    return "魔王副本20级-普通难度";
                case "81":
                    return "魔王副本30级-普通难度";
                case "82":
                    return "魔王副本30级-困难难度";
                case "83":
                    return "魔王副本40级-普通难度";
                case "84":
                    return "魔王副本40级-困难难度";
                case "85":
                    return "魔王副本40级-炼狱难度";
                case "86":
                    return "魔王副本50级-普通难度";
                case "87":
                    return "魔王副本50级-困难难度";
                case "88":
                    return "魔王副本50级-炼狱难度";
                case "89":
                    return "魔王副本60级-普通难度";
                case "90":
                    return "魔王副本60级-困难难度";
                case "91":
                    return "魔王副本60级-炼狱难度";
                case "92":
                    return "魔王副本70级-普通难度";
                case "93":
                    return "魔王副本70级-困难难度";
                case "94":
                    return "魔王副本70级-炼狱难度";
                case "95":
                    return "魔王副本80级-普通难度";
                case "96":
                    return "魔王副本80级-困难难度";
                case "97":
                    return "魔王副本80级-炼狱难度";
                case "101":
                    return "小镇-剧情本1-1";
                case "102":
                    return "小镇-剧情本1-2";
                case "103":
                    return "沙漠-剧情本2-1";
                case "112":
                    return "沙漠-剧情本2-2";
                case "106":
                    return "海底-剧情本3-1";
                case "111":
                    return "海底-剧情本3-1";
                case "114":
                    return "森林-剧情本4-1";
                case "115":
                    return "森林-剧情本4-2";
                case "121":
                    return "地宫-剧情本6-1";
                case "122":
                    return "地宫-剧情本6-2";
                case "124":
                    return "矿场-剧情本5-1";
                case "125":
                    return "矿场-剧情本5-2";
                case "131":
                    return "冰原-剧情副本7-1";
                case "132":
                    return "冰原-剧情副本7-2";
                case "133":
                    return "冰原-剧情副本7-3";
                case "134":
                    return "冰原-剧情副本7-4";
                case "135":
                    return "冰原-剧情副本7-5";
                case "136":
                    return "冰原-剧情副本7-6";
                case "150":
                    return "猎魔大师-战场1-训练场（守望镇）";
                case "151":
                    return "猎魔大师-战场2-地宫（主城）";
                case "152":
                    return "猎魔大师-战场3-海底（主城）";
                case "153":
                    return "猎魔大师-战场4-藏宝洞（守望镇）";
                case "154":
                    return "猎魔大师-战场5-沙漠（沙漠）";
                case "155":
                    return "猎魔大师-战场6-无主之地（沙漠）";
                case "156":
                    return "猎魔大师-战场7-红甲将军（海底）";
                case "157":
                    return "猎魔大师-战场8-鲛人祭祀（海底）";
                case "158":
                    return "猎魔大师-战场9-村里亚精灵（森林）";
                case "159":
                    return "猎魔大师-战场10-岔口亚精灵（森林）";
                case "160":
                    return "猎魔大师-战场11-门口黑妹（矿场）";
                case "161":
                    return "猎魔大师-战场12-倔老头附近黑妹（矿场）";
                case "162":
                    return "猎魔大师-战场13-笼子附近士兵（地宫）";
                case "163":
                    return "猎魔大师-战场14-大祭司附近士兵（地宫）";
                case "164":
                    return "猎魔大师-战场15-丛林营地（冰原）";
                case "165":
                    return "猎魔大师-战场14-雪山村（冰原）";
                case "180":
                    return "神器日常活动//层数1";
                case "181":
                    return "神器日常活动//层数2";
                case "182":
                    return "神器日常活动//层数3";
                case "183":
                    return "神器日常活动//层数4";
                case "184":
                    return "神器日常活动//层数5";
                case "185":
                    return "神器日常活动//层数6";
                case "186":
                    return "神器日常活动//层数7";
                case "187":
                    return "神器日常活动//层数8";
                case "188":
                    return "神器日常活动//层数9";
                case "189":
                    return "神器日常活动//层数10";
                case "190":
                    return "神器高难度活动";
                case "201":
                    return "经验活动 30+";
                case "202":
                    return "金钱活动 30+";
                case "205":
                    return "坐骑材料本 25级";
                case "207":
                    return "藏宝图副本 10级+";
                case "211":
                    return "神之试炼1+5X层";
                case "213":
                    return "神之试炼2+5X层";
                case "215":
                    return "神之试炼3+5X层";
                case "217":
                    return "神之试炼4+5X层";
                case "219":
                    return "神之试炼5+5X层";
                case "241":
                    return "（公会）怪物入侵-普通 20";
                case "242":
                    return "（公会）怪物入侵-精英 20";
                case "243":
                    return "（公会）怪物入侵-BOSS 20";
                default:
                    return value;
            }
        }
        #endregion

        #region 转到触发方法
        protected void Go_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = int.Parse(((TextBox)GridView1.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            bind();   //重新绑定GridView
        }
        #endregion

        #region 分页触发方法
        protected void GvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();  //重新绑定GridView
        }
        #endregion
    }
}
