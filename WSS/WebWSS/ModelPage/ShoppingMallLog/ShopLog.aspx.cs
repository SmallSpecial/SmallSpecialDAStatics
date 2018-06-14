using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.ShoppingMallLog
{
    public partial class ShopLog : Admin_Page
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
                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
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
                    sql += string.Format("SELECT \"{0}\" AS DateTime,SUBSTRING_INDEX(OP_BAK,\"	\",1) ShopType,PARA_1 ItemID,SUM(PARA_2) SoldNum,COUNT(CID) TradeNum,COUNT(DISTINCT CID) RoleNum,SUM(SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",7),\"	\",-1)) MoneyNum FROM {1}_item_log WHERE OPID=10000 GROUP BY SUBSTRING_INDEX(OP_BAK,\"	\",1),PARA_1 ", strSTime, strSTime);
                else
                    sql += string.Format(" UNION ALL SELECT \"{0}\" AS DateTime,SUBSTRING_INDEX(OP_BAK,\"	\",1) ShopType,PARA_1 ItemID,SUM(PARA_2) SoldNum,COUNT(CID) TradeNum,COUNT(DISTINCT CID) RoleNum,SUM(SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",7),\"	\",-1)) MoneyNum FROM {1}_item_log WHERE OPID=10000 GROUP BY SUBSTRING_INDEX(OP_BAK,\"	\",1),PARA_1 ", strSTime, strSTime);
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
                    sql += string.Format("SELECT \"{0}\" AS DateTime,SUBSTRING_INDEX(OP_BAK,\"	\",1) ShopType,PARA_1 ItemID,SUM(PARA_2) SoldNum,COUNT(CID) TradeNum,COUNT(DISTINCT CID) RoleNum,SUM(SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",7),\"	\",-1)) MoneyNum FROM {1}_item_log WHERE OPID=10000 GROUP BY SUBSTRING_INDEX(OP_BAK,\"	\",1),PARA_1 ", strSTime, strSTime);
                else
                    sql += string.Format(" UNION ALL SELECT \"{0}\" AS DateTime,SUBSTRING_INDEX(OP_BAK,\"	\",1) ShopType,PARA_1 ItemID,SUM(PARA_2) SoldNum,COUNT(CID) TradeNum,COUNT(DISTINCT CID) RoleNum,SUM(SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",7),\"	\",-1)) MoneyNum FROM {1}_item_log WHERE OPID=10000 GROUP BY SUBSTRING_INDEX(OP_BAK,\"	\",1),PARA_1 ", strSTime, strSTime);
            }

            sql += "')";

            ds = DBHelperDigGameDB.Query(sql);
            ExportExcelHelper.ExportDataSet(ds);
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
        protected string GetShopType(string value)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (value)
                {
                    case "1":
                        return "길드 상점";
                    case "2":
                        return "진영 상점";
                    case "3":
                        return "아레나 상점";
                    case "4":
                        return "인장 상점";
                    case "5":
                        return "명예 상점";
                    case "10":
                        return "실버 상점";
                    default:
                        return value;
                }
            }
            else
            {
                switch (value)
                {
                    case "1":
                        return "公会商店";
                    case "2":
                        return "阵营商店";
                    case "3":
                        return "竞技商店";
                    case "4":
                        return "猎魔商店";
                    case "5":
                        return "荣誉商店";
                    case "10":
                        return "银币商店";
                    default:
                        return value;
                }
            }
        }
        /// <summary>
        /// 获取道具文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetItem(string value)
        {
            try
            {
                if (PageLanguage == "ko-kr")
                {
                    #region 韩文
                    switch (value)
                    {
                        case "10000":
                            return "실버";
                        case "10001":
                            return "실버";
                        case "10002":
                            return "실버";
                        case "10003":
                            return "실버";
                        case "10004":
                            return "실버";
                        case "10005":
                            return "실버";
                        case "10006":
                            return "실버";
                        case "10007":
                            return "실버";
                        case "10008":
                            return "실버";
                        case "10009":
                            return "실버";
                        case "10010":
                            return "실버";
                        case "10011":
                            return "실버";
                        case "10100":
                            return "실버";
                        case "10101":
                            return "실버";
                        case "10102":
                            return "실버";
                        case "10103":
                            return "실버";
                        case "10104":
                            return "실버";
                        case "10105":
                            return "실버";
                        case "10106":
                            return "실버";
                        case "10107":
                            return "실버";
                        case "10108":
                            return "실버";
                        case "10109":
                            return "실버";
                        case "10110":
                            return "실버";
                        case "10111":
                            return "실버";
                        case "10112":
                            return "실버";
                        case "10113":
                            return "실버";
                        case "10114":
                            return "실버";
                        case "10115":
                            return "실버";
                        case "10116":
                            return "실버";
                        case "10117":
                            return "실버";
                        case "10118":
                            return "실버";
                        case "10119":
                            return "실버";
                        case "10120":
                            return "실버";
                        case "10121":
                            return "실버";
                        case "10122":
                            return "실버";
                        case "10123":
                            return "실버";
                        case "10124":
                            return "실버";
                        case "10125":
                            return "실버";
                        case "10126":
                            return "실버";
                        case "10127":
                            return "실버";
                        case "10128":
                            return "실버";
                        case "10129":
                            return "실버";
                        case "10130":
                            return "실버";
                        case "10131":
                            return "실버";
                        case "10132":
                            return "실버";
                        case "10133":
                            return "실버";
                        case "10134":
                            return "실버";
                        case "10135":
                            return "실버";
                        case "10136":
                            return "실버";
                        case "10137":
                            return "실버";
                        case "10138":
                            return "실버";
                        case "10139":
                            return "실버";
                        case "10140":
                            return "실버";
                        case "10141":
                            return "실버";
                        case "10142":
                            return "실버";
                        case "10143":
                            return "실버";
                        case "10144":
                            return "실버";
                        case "10145":
                            return "실버";
                        case "10146":
                            return "실버";
                        case "10147":
                            return "실버";
                        case "10148":
                            return "실버";
                        case "10149":
                            return "실버";
                        case "10150":
                            return "실버";
                        case "10151":
                            return "실버";
                        case "10152":
                            return "실버";
                        case "10153":
                            return "실버";
                        case "10154":
                            return "실버";
                        case "10155":
                            return "실버";
                        case "10156":
                            return "실버";
                        case "10157":
                            return "실버";
                        case "10158":
                            return "실버";
                        case "10159":
                            return "실버";
                        case "10160":
                            return "실버";
                        case "10161":
                            return "실버";
                        case "10162":
                            return "실버";
                        case "10163":
                            return "실버";
                        case "10164":
                            return "실버";
                        case "10165":
                            return "실버";
                        case "10166":
                            return "실버";
                        case "10167":
                            return "실버";
                        case "10168":
                            return "실버";
                        case "10169":
                            return "실버";
                        case "10170":
                            return "실버";
                        case "10171":
                            return "실버";
                        case "10172":
                            return "실버";
                        case "10173":
                            return "실버";
                        case "10174":
                            return "실버";
                        case "10175":
                            return "실버";
                        case "10176":
                            return "실버";
                        case "10177":
                            return "실버";
                        case "10178":
                            return "실버";
                        case "10179":
                            return "실버";
                        case "11000":
                            return "실버";
                        case "11001":
                            return "소량의 실버";
                        case "11002":
                            return "실버 더미";
                        case "11003":
                            return "대량의 실버";
                        case "11004":
                            return "실버 무더기";
                        case "12000":
                            return "블루 다이아";
                        case "12001":
                            return "소량의 블루 다이아";
                        case "12002":
                            return "블루 다이아 더미";
                        case "12003":
                            return "대량의 블루 다이아";
                        case "14000":
                            return "경험치";
                        case "14001":
                            return "소량의 경험치";
                        case "14002":
                            return "경험치 더미";
                        case "14003":
                            return "대량의 경험치";
                        case "14004":
                            return "경험치 무더기";
                        case "14011":
                            return "활력";
                        case "15000":
                            return "공훈";
                        case "16000":
                            return "공헌도";
                        case "17000":
                            return "길드 자금";
                        case "18000":
                            return "골드";
                        case "19000":
                            return "다이아";
                        case "19010":
                            return "아레나 휘장";
                        case "19020":
                            return "길드 목재";
                        case "19999":
                            return "드랍할 수 없는 아이템입니다.";
                        case "20000":
                            return "일반 강화석";
                        case "20001":
                            return "우수 강화석";
                        case "20002":
                            return "고급 강화석";
                        case "20003":
                            return "희귀 강화석";
                        case "20004":
                            return "영웅급 강화석";
                        case "20100":
                            return "마법서";
                        case "20200":
                            return "신비한 룬";
                        case "20300":
                            return "특성 초기화 약";
                        case "20400":
                            return "별의 정수";
                        case "20401":
                            return "빛의 정수";
                        case "20402":
                            return "찬란한 정수";
                        case "20403":
                            return "화염의 정수";
                        case "20410":
                            return "별의 정수";
                        case "20411":
                            return "빛의 정수";
                        case "20412":
                            return "찬란한 정수";
                        case "20413":
                            return "화염의 정수";
                        case "20500":
                            return "날개의 깃털";
                        case "20501":
                            return "날개의 깃털";
                        case "20600":
                            return "철광석";
                        case "20601":
                            return "구리석";
                        case "20602":
                            return "은광석";
                        case "20603":
                            return "금광석";
                        case "20604":
                            return "지옥 광석";
                        case "20605":
                            return "지옥 광석";
                        case "20620":
                            return "린넨";
                        case "20621":
                            return "벨벳";
                        case "20622":
                            return "방직포";
                        case "20623":
                            return "루닉";
                        case "20624":
                            return "신역의 천";
                        case "20625":
                            return "신역의 천";
                        case "20640":
                            return "단색 수정";
                        case "20641":
                            return "생명 수정";
                        case "20642":
                            return "하늘 수정";
                        case "20643":
                            return "마법 수정";
                        case "20644":
                            return "용신 수정";
                        case "20645":
                            return "용신 수정";
                        case "20660":
                            return "마법 정수";
                        case "20661":
                            return "기이한 정수";
                        case "20662":
                            return "공허의 정수";
                        case "20663":
                            return "별의 정수";
                        case "20664":
                            return "영원의 정수";
                        case "20700":
                            return "무기 스크롤";
                        case "20701":
                            return "무기 스크롤";
                        case "20702":
                            return "무기 스크롤";
                        case "20703":
                            return "무기 스크롤";
                        case "20704":
                            return "무기 스크롤";
                        case "20710":
                            return "목걸이 스크롤";
                        case "20711":
                            return "목걸이 스크롤";
                        case "20712":
                            return "목걸이 스크롤";
                        case "20713":
                            return "목걸이 스크롤";
                        case "20714":
                            return "목걸이 스크롤";
                        case "20720":
                            return "반지 스크롤";
                        case "20721":
                            return "반지 스크롤";
                        case "20722":
                            return "반지 스크롤";
                        case "20723":
                            return "반지 스크롤";
                        case "20724":
                            return "반지 스크롤";
                        case "20730":
                            return "장신구 스크롤";
                        case "20731":
                            return "장신구 스크롤";
                        case "20732":
                            return "장신구 스크롤";
                        case "20733":
                            return "장신구 스크롤";
                        case "20734":
                            return "장신구 스크롤";
                        case "20740":
                            return "투구 스크롤";
                        case "20741":
                            return "투구 스크롤";
                        case "20742":
                            return "투구 스크롤";
                        case "20743":
                            return "투구 스크롤";
                        case "20744":
                            return "투구 스크롤";
                        case "20750":
                            return "상의 스크롤";
                        case "20751":
                            return "상의 스크롤";
                        case "20752":
                            return "상의 스크롤";
                        case "20753":
                            return "상의 스크롤";
                        case "20754":
                            return "상의 스크롤";
                        case "20760":
                            return "장갑 스크롤";
                        case "20761":
                            return "장갑 스크롤";
                        case "20762":
                            return "장갑 스크롤";
                        case "20763":
                            return "장갑 스크롤";
                        case "20764":
                            return "장갑 스크롤";
                        case "20770":
                            return "신발 스크롤";
                        case "20771":
                            return "신발 스크롤";
                        case "20772":
                            return "신발 스크롤";
                        case "20773":
                            return "신발 스크롤";
                        case "20774":
                            return "신발 스크롤";
                        case "20780":
                            return "하의 스크롤";
                        case "20781":
                            return "하의 스크롤";
                        case "20782":
                            return "하의 스크롤";
                        case "20783":
                            return "하의 스크롤";
                        case "20784":
                            return "하의 스크롤";
                        case "20790":
                            return "귀걸이 스크롤";
                        case "20791":
                            return "귀걸이 스크롤";
                        case "20792":
                            return "귀걸이 스크롤";
                        case "20793":
                            return "귀걸이 스크롤";
                        case "20794":
                            return "귀걸이 스크롤";
                        case "20800":
                            return "각성 수정";
                        case "20900":
                            return "부활의 징표";
                        case "20999":
                            return "사냥 인장";
                        case "21000":
                            return "보호석";
                        case "21001":
                            return "엘리멘탈 수정석";
                        case "21002":
                            return "철광 곡괭이";
                        case "21003":
                            return "구리 곡괭이";
                        case "21004":
                            return "은광 곡괭이";
                        case "21005":
                            return "금광 곡괭이";
                        case "21006":
                            return "다이아 곡괭이";
                        case "21020":
                            return "악몽의 계약서";
                        case "21021":
                            return "어둠의 혼";
                        case "21101":
                            return "럼주";
                        case "21102":
                            return "화염 약주";
                        case "21103":
                            return "심해 열주";
                        case "21104":
                            return "니스 포도주";
                        case "21201":
                            return "길드 나팔";
                        case "21300":
                            return "화물 운송 티켓";
                        case "22001":
                            return "육성 엘릭서";
                        case "22002":
                            return "엘릭서 1";
                        case "22003":
                            return "엘릭서 2";
                        case "22101":
                            return "진화 엘릭서";
                        case "22201":
                            return "화이트";
                        case "22202":
                            return "베헤모스";
                        case "22203":
                            return "블랙 드래곤";
                        case "22204":
                            return "퍼플 유니콘 조각";
                        case "22205":
                            return "골드 유니콘 조각";
                        case "22206":
                            return "핑크 유니콘 조각";
                        case "22207":
                            return "고스트 치타";
                        case "22208":
                            return "마법 빗자루 조각";
                        case "22209":
                            return "화이트 유니콘 조각";
                        case "22210":
                            return "화이트 조각";
                        case "22211":
                            return "화이트 조각";
                        case "22212":
                            return "T3 바이크 조각";
                        case "22213":
                            return "귀여운 돼지 조각";
                        case "22214":
                            return "신의 왕좌 조각";
                        case "22215":
                            return "레드 유니콘 조각";
                        case "22216":
                            return "화이트 조각";
                        case "22217":
                            return "화이트 조각";
                        case "22218":
                            return "화이트 조각";
                        case "22300":
                            return "활성화 마법서";
                        case "22310":
                            return "일반 경험의 책";
                        case "22311":
                            return "고급 경험의 책";
                        case "22312":
                            return "희귀 경험의 책";
                        case "22313":
                            return "전설 경험의 책";
                        case "22314":
                            return "고대 경험의 책";
                        case "22320":
                            return "일반 각성의 책";
                        case "22321":
                            return "고급 각성의 책";
                        case "22322":
                            return "희귀 각성의 책";
                        case "22323":
                            return "전설 각성의 책";
                        case "22324":
                            return "고대 각성의 책";
                        case "22401":
                            return "50레벨 무기 도면";
                        case "22402":
                            return "60레벨 무기 도면";
                        case "22403":
                            return "70레벨 무기 도면";
                        case "22404":
                            return "80레벨 무기 도면";
                        case "22405":
                            return "90레벨 무기 도면";
                        case "22406":
                            return "100레벨 무기 도면";
                        case "22411":
                            return "50레벨 목걸이 도면";
                        case "22412":
                            return "60레벨 목걸이 도면";
                        case "22413":
                            return "70레벨 목걸이 도면";
                        case "22414":
                            return "80레벨 목걸이 도면";
                        case "22415":
                            return "90레벨 목걸이 도면";
                        case "22416":
                            return "100레벨 목걸이 도면";
                        case "22421":
                            return "50레벨 귀걸이 도면";
                        case "22422":
                            return "60레벨 귀걸이 도면";
                        case "22423":
                            return "70레벨 귀걸이 도면";
                        case "22424":
                            return "80레벨 귀걸이 도면";
                        case "22425":
                            return "90레벨 귀걸이 도면";
                        case "22426":
                            return "100레벨 귀걸이 도면";
                        case "22431":
                            return "50레벨 반지 도면";
                        case "22432":
                            return "60레벨 반지 도면";
                        case "22433":
                            return "70레벨 반지 도면";
                        case "22434":
                            return "80레벨 반지 도면";
                        case "22435":
                            return "90레벨 반지 도면";
                        case "22436":
                            return "100레벨 반지 도면";
                        case "22441":
                            return "50레벨 장신구 도면";
                        case "22442":
                            return "60레벨 장신구 도면";
                        case "22443":
                            return "70레벨 장신구 도면";
                        case "22444":
                            return "80레벨 장신구 도면";
                        case "22445":
                            return "90레벨 장신구 도면";
                        case "22446":
                            return "100레벨 장신구 도면";
                        case "22451":
                            return "50레벨 투구 도면";
                        case "22452":
                            return "60레벨 투구 도면";
                        case "22453":
                            return "70레벨 투구 도면";
                        case "22454":
                            return "80레벨 투구 도면";
                        case "22455":
                            return "90레벨 투구 도면";
                        case "22456":
                            return "100레벨 투구 도면";
                        case "22461":
                            return "50레벨 상의 도면";
                        case "22462":
                            return "60레벨 상의 도면";
                        case "22463":
                            return "70레벨 상의 도면";
                        case "22464":
                            return "80레벨 상의 도면";
                        case "22465":
                            return "90레벨 상의 도면";
                        case "22466":
                            return "100레벨 상의 도면";
                        case "22471":
                            return "50레벨 장갑 도면";
                        case "22472":
                            return "60레벨 장갑 도면";
                        case "22473":
                            return "70레벨 장갑 도면";
                        case "22474":
                            return "80레벨 장갑 도면";
                        case "22475":
                            return "90레벨 장갑 도면";
                        case "22476":
                            return "100레벨 장갑 도면";
                        case "22481":
                            return "50레벨 하의 도면";
                        case "22482":
                            return "60레벨 하의 도면";
                        case "22483":
                            return "70레벨 하의 도면";
                        case "22484":
                            return "80레벨 하의 도면";
                        case "22485":
                            return "90레벨 하의 도면";
                        case "22486":
                            return "100레벨 하의 도면";
                        case "22491":
                            return "50레벨 신발 도면";
                        case "22492":
                            return "60레벨 신발 도면";
                        case "22493":
                            return "70레벨 신발 도면";
                        case "22494":
                            return "80레벨 신발 도면";
                        case "22495":
                            return "90레벨 신발 도면";
                        case "22496":
                            return "100레벨 신발 도면";
                        case "23000":
                            return "수영장룩 헤어";
                        case "23001":
                            return "용전사 헤어 2";
                        case "23002":
                            return "용전사 헤어 3";
                        case "23003":
                            return "용전사 헤어 4";
                        case "23004":
                            return "용전사 헤어 5";
                        case "23050":
                            return "용전사 얼굴 1";
                        case "23051":
                            return "용전사 얼굴 2";
                        case "23052":
                            return "용전사 얼굴 3";
                        case "23053":
                            return "용전사 얼굴 4";
                        case "23054":
                            return "용전사 얼굴 5";
                        case "23100":
                            return "수영장룩 의상";
                        case "23101":
                            return "집사 수트";
                        case "23102":
                            return "폭주족룩 의상";
                        case "23103":
                            return "용전사 의상 4";
                        case "23104":
                            return "용전사 의상 5";
                        case "23150":
                            return "용전사 무기 1";
                        case "23151":
                            return "용전사 무기 2";
                        case "23152":
                            return "용전사 무기 3";
                        case "23153":
                            return "용전사 무기 4";
                        case "23154":
                            return "용전사 무기 5";
                        case "23200":
                            return "수영장룩 헤어";
                        case "23201":
                            return "마법사 헤어 2";
                        case "23202":
                            return "폭주족룩 헤어";
                        case "23203":
                            return "마법사 헤어 4";
                        case "23204":
                            return "마법사 헤어 5";
                        case "23250":
                            return "마법사 얼굴 1";
                        case "23251":
                            return "마법사 얼굴 2";
                        case "23252":
                            return "마법사 얼굴 3";
                        case "23253":
                            return "마법사 얼굴 4";
                        case "23254":
                            return "마법사 얼굴 5";
                        case "23300":
                            return "수영장룩 의상";
                        case "23301":
                            return "메이드복";
                        case "23302":
                            return "폭주족룩 의상";
                        case "23303":
                            return "마법사 의상 4";
                        case "23304":
                            return "마법사 의상 5";
                        case "23350":
                            return "마법사 무기 1";
                        case "23351":
                            return "마법사 무기 2";
                        case "23352":
                            return "마법사 무기 3";
                        case "23353":
                            return "마법사 무기 4";
                        case "23354":
                            return "마법사 무기 5";
                        case "23400":
                            return "수영장룩 헤어";
                        case "23401":
                            return "집사룩 헤어";
                        case "23402":
                            return "폭주족룩 헤어";
                        case "23403":
                            return "한복룩 헤어";
                        case "23404":
                            return "암살자 헤어 5";
                        case "23450":
                            return "암살자 얼굴 1";
                        case "23451":
                            return "암살자 얼굴 2";
                        case "23452":
                            return "암살자 얼굴 3";
                        case "23453":
                            return "암살자 얼굴 4";
                        case "23454":
                            return "암살자 얼굴 5";
                        case "23500":
                            return "수영장룩 의상";
                        case "23501":
                            return "집사 수트";
                        case "23502":
                            return "폭주족룩 의상";
                        case "23503":
                            return "한복";
                        case "23504":
                            return "암살자 의상 5";
                        case "23550":
                            return "암살자 무기 1";
                        case "23551":
                            return "암살자 무기 2";
                        case "23552":
                            return "암살자 무기 3";
                        case "23553":
                            return "암살자 무기 4";
                        case "23554":
                            return "암살자 무기 5";
                        case "23600":
                            return "수영장룩 헤어";
                        case "23601":
                            return "소환사 헤어 2";
                        case "23602":
                            return "폭주족룩 헤어";
                        case "23603":
                            return "소환사 헤어 4";
                        case "23604":
                            return "소환사 헤어 5";
                        case "23650":
                            return "소환사 얼굴 1";
                        case "23651":
                            return "소환사 얼굴 2";
                        case "23652":
                            return "소환사 얼굴 3";
                        case "23653":
                            return "소환사 얼굴 4";
                        case "23654":
                            return "소환사 얼굴 5";
                        case "23700":
                            return "수영장룩 의상";
                        case "23701":
                            return "메이드복";
                        case "23702":
                            return "폭주족룩 의상";
                        case "23703":
                            return "소환사 의상 4";
                        case "23704":
                            return "소환사 의상 5";
                        case "23750":
                            return "소환사 무기 1";
                        case "23751":
                            return "소환사 무기 2";
                        case "23752":
                            return "소환사 무기 3";
                        case "23753":
                            return "소환사 무기 4";
                        case "23754":
                            return "소환사 무기 5";
                        case "24000":
                            return "정령의 날개";
                        case "24001":
                            return "질서의 날개";
                        case "24002":
                            return "화염의 날개";
                        case "24003":
                            return "환상 계약 날개";
                        case "24004":
                            return "얼음 깃털 날개";
                        case "24005":
                            return "핏빛 마신 날개";
                        case "25000":
                            return "일반 경험치 약";
                        case "25001":
                            return "고급 경험치 약";
                        case "25002":
                            return "희귀 경험치 약";
                        case "25003":
                            return "전설 경험치 약";
                        case "25100":
                            return "소형 활력 포션";
                        case "25101":
                            return "중형 활력 포션";
                        case "25102":
                            return "대형 활력 포션";
                        case "25200":
                            return "일반 생명 포션";
                        case "25201":
                            return "중급 생명 포션";
                        case "25202":
                            return "고급 생명 포션";
                        case "25203":
                            return "영웅 생명 포션";
                        case "25204":
                            return "전설 생명 포션";
                        case "25205":
                            return "정제 생명물약";
                        case "25206":
                            return "특제 생명물약";
                        case "25207":
                            return "차원 생명물약";
                        case "25300":
                            return "소형 고급 물약";
                        case "25301":
                            return "중형 고급 물약";
                        case "25302":
                            return "대형 고급 물약";
                        case "25303":
                            return "영웅 고급 물약";
                        case "25304":
                            return "슈퍼 고급 물약";
                        case "25305":
                            return "정제 고급 물약";
                        case "25306":
                            return "특제 고급 물약";
                        case "25307":
                            return "이계 고급 물약";
                        case "25308":
                            return "전설 고급 물약";
                        case "25309":
                            return "고대 고급 물약";
                        case "25400":
                            return "韩文文本";
                        case "25401":
                            return "韩文文本";
                        case "25500":
                            return "안개 이슬";
                        case "25501":
                            return "한라산 개울물";
                        case "25502":
                            return "폭풍 샘물";
                        case "25503":
                            return "여신 성수";
                        case "25600":
                            return "활명수";
                        case "25601":
                            return "강건";
                        case "25602":
                            return "희귀 강건";
                        case "25603":
                            return "석화";
                        case "25604":
                            return "희귀 석화";
                        case "25605":
                            return "항마";
                        case "25606":
                            return "희귀 항마";
                        case "25607":
                            return "괴력";
                        case "25608":
                            return "희귀 괴력";
                        case "25609":
                            return "영체";
                        case "25610":
                            return "희귀 영체";
                        case "25611":
                            return "잔혹";
                        case "25612":
                            return "희귀 잔혹";
                        case "25613":
                            return "교활";
                        case "25614":
                            return "희귀 교활";
                        case "25615":
                            return "정확";
                        case "25616":
                            return "희귀 정확";
                        case "25617":
                            return "강인";
                        case "25618":
                            return "희귀 강인";
                        case "25619":
                            return "치명상";
                        case "25620":
                            return "희귀 치명상";
                        case "25621":
                            return "흡혈";
                        case "25622":
                            return "희귀 흡혈";
                        case "25623":
                            return "해진 갑옷";
                        case "25624":
                            return "희귀 해진 갑옷";
                        case "25625":
                            return "갑옷";
                        case "25626":
                            return "희귀 갑옷";
                        case "25627":
                            return "맹습";
                        case "25628":
                            return "희귀 맹습";
                        case "25629":
                            return "인내";
                        case "25630":
                            return "희귀 인내";
                        case "25631":
                            return "기절 숙련";
                        case "25632":
                            return "희귀 기절 숙련";
                        case "25633":
                            return "기절 저항";
                        case "25634":
                            return "희귀 기절 저항";
                        case "25635":
                            return "감속 숙련";
                        case "25636":
                            return "희귀 감속 숙련";
                        case "25637":
                            return "감속 저항";
                        case "25638":
                            return "희귀 감속 저항";
                        case "25639":
                            return "속박 숙련";
                        case "25640":
                            return "희귀 속박 숙련";
                        case "25641":
                            return "속박 저항";
                        case "25642":
                            return "희귀 속박 저항";
                        case "25643":
                            return "침묵 숙련";
                        case "25644":
                            return "희귀 침묵 숙련";
                        case "25645":
                            return "침묵 저항";
                        case "25646":
                            return "희귀 침묵 저항";
                        case "25647":
                            return "연소 숙련";
                        case "25648":
                            return "희귀 연소 숙련";
                        case "25649":
                            return "연소 저항";
                        case "25650":
                            return "희귀 연소 저항";
                        case "25700":
                            return "솔트 비스킷";
                        case "25701":
                            return "크림 비스킷";
                        case "25702":
                            return "초코 비스킷";
                        case "25800":
                            return "가시 올가미";
                        case "25801":
                            return "강철 올가미";
                        case "25802":
                            return "마법 올가미";
                        case "25805":
                            return "가시 올가미";
                        case "25806":
                            return "강철 올가미";
                        case "25807":
                            return "마법 올가미";
                        case "25900":
                            return "마법사과";
                        case "25901":
                            return "펫 스킬 각인석";
                        case "26000":
                            return "고급 방울";
                        case "26001":
                            return "희귀 방울";
                        case "26002":
                            return "영웅 방울";
                        case "26003":
                            return "전설 방울";
                        case "26010":
                            return "고급 원반";
                        case "26011":
                            return "희귀 원반";
                        case "26012":
                            return "영웅 원반";
                        case "26013":
                            return "전설 원반";
                        case "26020":
                            return "고급 블록";
                        case "26021":
                            return "희귀 블록";
                        case "26022":
                            return "영웅 블록";
                        case "26023":
                            return "전설 블록";
                        case "26030":
                            return "고급 가죽공";
                        case "26031":
                            return "희귀 가죽공";
                        case "26032":
                            return "영웅 가죽공";
                        case "26033":
                            return "전설 가죽공";
                        case "26040":
                            return "고급 곰인형";
                        case "26041":
                            return "희귀 곰인형";
                        case "26042":
                            return "영웅 곰인형";
                        case "26043":
                            return "전설 곰인형";
                        case "26310":
                            return "지혈초";
                        case "26311":
                            return "지혈초";
                        case "26312":
                            return "지혈초";
                        case "26313":
                            return "지혈초";
                        case "26314":
                            return "지혈초";
                        case "26330":
                            return "조미료";
                        case "26331":
                            return "조미료";
                        case "26332":
                            return "조미료";
                        case "26333":
                            return "조미료";
                        case "26334":
                            return "조미료";
                        case "26340":
                            return "빈 스크롤";
                        case "26350":
                            return "조련 지침";
                        case "26400":
                            return "머맨 사냥꾼 조각";
                        case "26401":
                            return "리빙 아머 조각";
                        case "26402":
                            return "드레이크 조각";
                        case "27000":
                            return "코믹콘 패키지";
                        case "27001":
                            return "CBT 스페셜 패키지";
                        case "27002":
                            return "CBT 랭킹 Top10 보상";
                        case "27003":
                            return "CBT 설문 참여 보상";
                        case "27004":
                            return "CBT 참여 보상";
                        case "27005":
                            return "사전예약 보상";
                        case "27006":
                            return "가르시아의 선물";
                        case "27007":
                            return "델리아의 첫번째 선물";
                        case "27008":
                            return "델리아의 두번째 선물";
                        case "27009":
                            return "델리아의 세번째 선물";
                        case "27010":
                            return "에디의 선물";
                        case "27011":
                            return "헤일리의 선물";
                        case "27012":
                            return "카타리나의 선물";
                        case "27013":
                            return "촌장의 소중한 선물";
                        case "27014":
                            return "촌장의 진심이 담긴 선물";
                        case "27015":
                            return "제라드의 선물";
                        case "27016":
                            return "사전예약 공유 선물";
                        case "27017":
                            return "원스토어 사전예약 선물";
                        case "27100":
                            return "코믹콘 패키지";
                        case "27900":
                            return "지상 최강";
                        case "27901":
                            return "이너 서클";
                        case "27910":
                            return "드래곤 로드";
                        case "27911":
                            return "마스터 매직";
                        case "27912":
                            return "섀도우 워리어";
                        case "27913":
                            return "디바인 세이지";
                        case "27940":
                            return "전설의 결투사";
                        case "27941":
                            return "영예의 결투사";
                        case "27942":
                            return "집념의 결투사";
                        case "27943":
                            return "용기의 결투사";
                        case "27950":
                            return "아타크의 주인";
                        case "27951":
                            return "아타크의 기사";
                        case "27952":
                            return "아타크";
                        case "27953":
                            return "레드 우드의 주인";
                        case "27954":
                            return "레드 우드의 기사";
                        case "27955":
                            return "레드 우드";
                        case "27956":
                            return "블루 마운틴의 주인";
                        case "27957":
                            return "블루 마운틴의 기사";
                        case "27958":
                            return "블루 마운틴";
                        case "27959":
                            return "빅 해머의 주인";
                        case "27960":
                            return "빅 해머의 기사";
                        case "27961":
                            return "빅 해머";
                        case "27962":
                            return "파이어 벨리의 주인";
                        case "27963":
                            return "파이어 벨리의 기사";
                        case "27964":
                            return "파이어 벨리";
                        case "27965":
                            return "스노우 캐슬의 주인";
                        case "27966":
                            return "스노우 캐슬의 기사";
                        case "27967":
                            return "스노우 캐슬";
                        case "30000":
                            return "Lv1 루비";
                        case "30001":
                            return "Lv2 루비";
                        case "30002":
                            return "Lv3 루비";
                        case "30003":
                            return "Lv4 루비";
                        case "30004":
                            return "Lv5 루비";
                        case "30005":
                            return "Lv6 루비";
                        case "30006":
                            return "Lv7 루비";
                        case "30007":
                            return "Lv8 루비";
                        case "30008":
                            return "Lv9 루비";
                        case "30009":
                            return "Lv10 루비";
                        case "30100":
                            return "Lv1 사파이어";
                        case "30101":
                            return "Lv2 사파이어";
                        case "30102":
                            return "Lv3 사파이어";
                        case "30103":
                            return "Lv4 사파이어";
                        case "30104":
                            return "Lv5 사파이어";
                        case "30105":
                            return "Lv6 사파이어";
                        case "30106":
                            return "Lv7 사파이어";
                        case "30107":
                            return "Lv8 사파이어";
                        case "30108":
                            return "Lv9 사파이어";
                        case "30109":
                            return "Lv10 사파이어";
                        case "30200":
                            return "Lv1 가넷";
                        case "30201":
                            return "Lv2 가넷";
                        case "30202":
                            return "Lv3 가넷";
                        case "30203":
                            return "Lv4 가넷";
                        case "30204":
                            return "Lv5 가넷";
                        case "30205":
                            return "Lv6 가넷";
                        case "30206":
                            return "Lv7 가넷";
                        case "30207":
                            return "Lv8 가넷";
                        case "30208":
                            return "Lv9 가넷";
                        case "30209":
                            return "Lv10 가넷";
                        case "30300":
                            return "Lv1 아쿠아마린";
                        case "30301":
                            return "Lv2 아쿠아마린";
                        case "30302":
                            return "Lv3 아쿠아마린";
                        case "30303":
                            return "Lv4 아쿠아마린";
                        case "30304":
                            return "Lv5 아쿠아마린";
                        case "30305":
                            return "Lv6 아쿠아마린";
                        case "30306":
                            return "Lv7 아쿠아마린";
                        case "30307":
                            return "Lv8 아쿠아마린";
                        case "30308":
                            return "Lv9 아쿠아마린";
                        case "30309":
                            return "Lv10 아쿠아마린";
                        case "30400":
                            return "Lv1 카넬리안";
                        case "30401":
                            return "Lv2 카넬리안";
                        case "30402":
                            return "Lv3 카넬리안";
                        case "30403":
                            return "Lv4 카넬리안";
                        case "30404":
                            return "Lv5 카넬리안";
                        case "30405":
                            return "Lv6 카넬리안";
                        case "30406":
                            return "Lv7 카넬리안";
                        case "30407":
                            return "Lv8 카넬리안";
                        case "30408":
                            return "Lv9 카넬리안";
                        case "30409":
                            return "Lv10 카넬리안";
                        case "30500":
                            return "Lv1 자수정";
                        case "30501":
                            return "Lv2 자수정";
                        case "30502":
                            return "Lv3 자수정";
                        case "30503":
                            return "Lv4 자수정";
                        case "30504":
                            return "Lv5 자수정";
                        case "30505":
                            return "Lv6 자수정";
                        case "30506":
                            return "Lv7 자수정";
                        case "30507":
                            return "Lv8 자수정";
                        case "30508":
                            return "Lv9 자수정";
                        case "30509":
                            return "Lv10 자수정";
                        case "30600":
                            return "Lv1 토파즈";
                        case "30601":
                            return "Lv2 토파즈";
                        case "30602":
                            return "Lv3 토파즈";
                        case "30603":
                            return "Lv4 토파즈";
                        case "30604":
                            return "Lv5 토파즈";
                        case "30605":
                            return "Lv6 토파즈";
                        case "30606":
                            return "Lv7 토파즈";
                        case "30607":
                            return "Lv8 토파즈";
                        case "30608":
                            return "Lv9 토파즈";
                        case "30609":
                            return "Lv10 토파즈";
                        case "30700":
                            return "Lv1 문스톤";
                        case "30701":
                            return "Lv2 문스톤";
                        case "30702":
                            return "Lv3 문스톤";
                        case "30703":
                            return "Lv4 문스톤";
                        case "30704":
                            return "Lv5 문스톤";
                        case "30705":
                            return "Lv6 문스톤";
                        case "30706":
                            return "Lv7 문스톤";
                        case "30707":
                            return "Lv8 문스톤";
                        case "30708":
                            return "Lv9 문스톤";
                        case "30709":
                            return "Lv10 문스톤";
                        case "30800":
                            return "Lv1 에메랄드";
                        case "30801":
                            return "Lv2 에메랄드";
                        case "30802":
                            return "Lv3 에메랄드";
                        case "30803":
                            return "Lv4 에메랄드";
                        case "30804":
                            return "Lv5 에메랄드";
                        case "30805":
                            return "Lv6 에메랄드";
                        case "30806":
                            return "Lv7 에메랄드";
                        case "30807":
                            return "Lv8 에메랄드";
                        case "30808":
                            return "Lv9 에메랄드";
                        case "30809":
                            return "Lv10 에메랄드";
                        case "31000":
                            return "여신의 선물상자";
                        case "31001":
                            return "강화";
                        case "31002":
                            return "승급";
                        case "31003":
                            return "무기 인챈트";
                        case "31004":
                            return "목걸이 인챈트";
                        case "31005":
                            return "반지 인챈트";
                        case "31006":
                            return "장신구 인챈트";
                        case "31007":
                            return "투구 인챈트";
                        case "31008":
                            return "상의 인챈트";
                        case "31009":
                            return "장갑 인챈트";
                        case "31010":
                            return "신발 인챈트";
                        case "31011":
                            return "하의 인챈트";
                        case "31012":
                            return "귀걸이 인챈트";
                        case "31013":
                            return "1Lv 보석";
                        case "31014":
                            return "2Lv 보석";
                        case "31015":
                            return "3Lv 보석";
                        case "31016":
                            return "4Lv 보석";
                        case "31017":
                            return "5Lv 보석";
                        case "31018":
                            return "6Lv 보석";
                        case "31019":
                            return "7Lv 보석";
                        case "31020":
                            return "8Lv 보석";
                        case "31021":
                            return "9Lv 보석";
                        case "31022":
                            return "10Lv 보석";
                        case "31023":
                            return "개조";
                        case "31024":
                            return "각성";
                        case "31025":
                            return "스킬";
                        case "31026":
                            return "코스튬 교환";
                        case "31027":
                            return "탈것 육성";
                        case "31028":
                            return "탈것 조각";
                        case "31029":
                            return "상점물약";
                        case "31030":
                            return "경험치 약";
                        case "31031":
                            return "술 마시기";
                        case "31032":
                            return "활력 약";
                        case "31033":
                            return "물약 제작";
                        case "31034":
                            return "음식 요리";
                        case "31035":
                            return "생활스킬 레벨업";
                        case "31036":
                            return "생활스킬 조제 재료";
                        case "31037":
                            return "생활스킬 요리 재료";
                        case "31038":
                            return "생활스킬 인챈트 재료";
                        case "31039":
                            return "생활스킬 조련 재료";
                        case "31040":
                            return "무기 제작";
                        case "31041":
                            return "방어구 제작";
                        case "31042":
                            return "장신구 제작";
                        case "31043":
                            return "무기 도면";
                        case "31044":
                            return "목걸이 도면";
                        case "31045":
                            return "귀걸이 도면";
                        case "31046":
                            return "반지 도면";
                        case "31047":
                            return "장신구 도면";
                        case "31048":
                            return "투구 도면";
                        case "31049":
                            return "상의 도면";
                        case "31050":
                            return "장갑 도면";
                        case "31051":
                            return "하의 도면";
                        case "31052":
                            return "신발 도면";
                        case "31053":
                            return "펫 자질";
                        case "31054":
                            return "펫 경험치";
                        case "31055":
                            return "펫 포획";
                        case "31056":
                            return "펫 각성";
                        case "31057":
                            return "펫 장난감-강인";
                        case "31058":
                            return "펫 장난감-용감";
                        case "31059":
                            return "펫 장난감-총명";
                        case "31060":
                            return "펫 장난감-충성";
                        case "31061":
                            return "펫 장난감-친절함";
                        case "31062":
                            return "펫 장난감-친절함";
                        case "31101":
                            return "10Lv 선물";
                        case "31102":
                            return "20Lv 선물";
                        case "31103":
                            return "30Lv 선물";
                        case "31104":
                            return "40Lv 선물";
                        case "31105":
                            return "50Lv 선물";
                        case "31106":
                            return "60Lv 선물";
                        case "31107":
                            return "70Lv 선물";
                        case "31108":
                            return "80Lv 선물";
                        case "31109":
                            return "90Lv 선물";
                        case "31110":
                            return "100Lv 선물";
                        case "31121":
                            return "1Lv 보석 팩";
                        case "31122":
                            return "2Lv 보석 팩";
                        case "31123":
                            return "3Lv 보석 팩";
                        case "31124":
                            return "4Lv 보석 팩";
                        case "31125":
                            return "5Lv 보석 팩";
                        case "31140":
                            return "랜덤 승급 상자";
                        case "31141":
                            return "일반 승급 상자";
                        case "31142":
                            return "고급 승급 상자";
                        case "31143":
                            return "희귀 승급 상자";
                        case "31144":
                            return "영웅 승급 상자";
                        case "31145":
                            return "전설 승급 상자";
                        case "31150":
                            return "일반 스크롤 팩";
                        case "31151":
                            return "희귀 스크롤 팩";
                        case "31152":
                            return "영웅 스크롤 팩";
                        case "31161":
                            return "고급 강화석 팩";
                        case "31162":
                            return "희귀 강화석 팩";
                        case "31163":
                            return "영웅 강화석 팩";
                        case "31164":
                            return "전설 강화석 팩";
                        case "31170":
                            return "특성강화 패키지";
                        case "31181":
                            return "일반승급재료팩";
                        case "31182":
                            return "고급승급재료팩";
                        case "31183":
                            return "희귀승급재료팩";
                        case "31184":
                            return "영웅승급재료팩";
                        case "31185":
                            return "전설승급재료팩";
                        case "31186":
                            return "고대승급재료팩";
                        case "31191":
                            return "Lv1 보석 팩";
                        case "31192":
                            return "Lv2 보석 팩";
                        case "31193":
                            return "Lv3 보석 팩";
                        case "31194":
                            return "Lv4 보석 팩";
                        case "31195":
                            return "Lv5 보석 팩";
                        case "31196":
                            return "Lv6 보석 팩";
                        case "31197":
                            return "Lv7 보석 팩";
                        case "31198":
                            return "Lv8 보석 팩";
                        case "31199":
                            return "Lv9 보석 팩";
                        case "31200":
                            return "Lv10 보석 팩";
                        case "31301":
                            return "하급 보물지도";
                        case "31302":
                            return "희귀 보물지도";
                        case "31303":
                            return "영웅 보물지도";
                        case "31304":
                            return "전설 보물지도";
                        case "31401":
                            return "Lv1 제국물자함";
                        case "31402":
                            return "Lv2 제국물자함";
                        case "31403":
                            return "Lv3 제국물자함";
                        case "31404":
                            return "Lv4 제국물자함";
                        case "31405":
                            return "Lv5 제국물자함";
                        case "31406":
                            return "Lv6 제국물자함";
                        case "31411":
                            return "Lv1 우정 선물";
                        case "31412":
                            return "Lv2 우정 선물";
                        case "31413":
                            return "Lv3 우정 선물";
                        case "31414":
                            return "Lv4 우정 선물";
                        case "31415":
                            return "Lv5 우정 선물";
                        case "31416":
                            return "Lv6 우정 선물";
                        case "31417":
                            return "Lv7 우정 선물";
                        case "31418":
                            return "Lv8 우정 선물";
                        case "31419":
                            return "Lv9 우정 선물";
                        case "31420":
                            return "Lv10 우정 선물";
                        case "31430":
                            return "사랑의 후라이";
                        case "31431":
                            return "치킨 버거";
                        case "31432":
                            return "과일 샐러드";
                        case "31433":
                            return "지하 버섯 스프";
                        case "31434":
                            return "주점 빵";
                        case "31435":
                            return "오아시스 피자";
                        case "31436":
                            return "터틀 스테이크";
                        case "31437":
                            return "심해의 랍스터";
                        case "31438":
                            return "통닭구이";
                        case "31439":
                            return "엘프 드래곤 구이";
                        case "31450":
                            return "마법서 선물함";
                        case "31451":
                            return "마법서 선물함";
                        case "31452":
                            return "마법서 선물함";
                        case "31453":
                            return "마법서 선물함";
                        case "31454":
                            return "마법서 선물함";
                        case "31455":
                            return "마법서 선물함";
                        case "31456":
                            return "마법서 선물함";
                        case "31457":
                            return "마법서 선물함";
                        case "31458":
                            return "마법서 선물함";
                        case "31459":
                            return "마법서 선물함";
                        case "31460":
                            return "1Lv 진영 퀘스트 상자";
                        case "31461":
                            return "2Lv 진영 퀘스트 상자";
                        case "31462":
                            return "3Lv 진영 퀘스트 상자";
                        case "31463":
                            return "4Lv 진영 퀘스트 상자";
                        case "31464":
                            return "5Lv 진영 퀘스트 상자";
                        case "31465":
                            return "6Lv 진영 퀘스트 상자";
                        case "31470":
                            return "1레벨 길드 퀘스트 보물상자";
                        case "31471":
                            return "2레벨 길드 퀘스트 보물상자";
                        case "31472":
                            return "3레벨 길드 퀘스트 보물상자";
                        case "31473":
                            return "4레벨 길드 퀘스트 보물상자";
                        case "31474":
                            return "5레벨 길드 퀘스트 보물상자";
                        case "31475":
                            return "6레벨 길드 퀘스트 보물상자";
                        case "31480":
                            return "1레벨 펫 퀘스트 보물상자";
                        case "31481":
                            return "2레벨 펫 퀘스트 보물상자";
                        case "31482":
                            return "3레벨 펫 퀘스트 보물상자";
                        case "31483":
                            return "4레벨 펫 퀘스트 보물상자";
                        case "31484":
                            return "5레벨 펫 퀘스트 보물상자";
                        case "31488":
                            return "펫 스킬북 보물상자";
                        case "31489":
                            return "희귀 펫 스킬북 보물상자";
                        case "31501":
                            return "1 D등급-전사";
                        case "31502":
                            return "1 C등급-전사";
                        case "31503":
                            return "1 B등급-전사";
                        case "31504":
                            return "1 A등급-전사";
                        case "31505":
                            return "10 D등급-전사";
                        case "31506":
                            return "10 C등급-전사";
                        case "31507":
                            return "10 B등급-전사";
                        case "31508":
                            return "10 A등급-전사";
                        case "31509":
                            return "30 D등급-전사";
                        case "31510":
                            return "30 C등급-전사";
                        case "31511":
                            return "30 B등급-전사";
                        case "31512":
                            return "30 A등급-전사";
                        case "31513":
                            return "30 S등급-전사";
                        case "31514":
                            return "40 D등급-전사";
                        case "31515":
                            return "40 C등급-전사";
                        case "31516":
                            return "40 B등급-전사";
                        case "31517":
                            return "40 A등급-전사";
                        case "31518":
                            return "40 S등급-전사";
                        case "31519":
                            return "50 D등급-전사";
                        case "31520":
                            return "50 C등급-전사";
                        case "31521":
                            return "50 B등급-전사";
                        case "31522":
                            return "50 A등급-전사";
                        case "31523":
                            return "50 S등급-전사";
                        case "31524":
                            return "50 A등급 세트-전사";
                        case "31525":
                            return "50 S등급 세트-전사";
                        case "31526":
                            return "60 D등급-전사";
                        case "31527":
                            return "60 C등급-전사";
                        case "31528":
                            return "60 B등급-전사";
                        case "31529":
                            return "60 A등급-전사";
                        case "31530":
                            return "60 S등급-전사";
                        case "31531":
                            return "60 A등급 세트-전사";
                        case "31532":
                            return "60 S등급 세트-전사";
                        case "31533":
                            return "70 D등급-전사";
                        case "31534":
                            return "70 C등급-전사";
                        case "31535":
                            return "70 B등급-전사";
                        case "31536":
                            return "70 A등급-전사";
                        case "31537":
                            return "70 S등급-전사";
                        case "31538":
                            return "70 A등급 세트-전사";
                        case "31539":
                            return "70 S등급 세트-전사";
                        case "31540":
                            return "80 D등급-전사";
                        case "31541":
                            return "80 C등급-전사";
                        case "31542":
                            return "80 B등급-전사";
                        case "31543":
                            return "80 A등급-전사";
                        case "31544":
                            return "80 S등급-전사";
                        case "31545":
                            return "80 A등급 세트-전사";
                        case "31546":
                            return "80 S등급 세트-전사";
                        case "31547":
                            return "90 D등급-전사";
                        case "31548":
                            return "90 C등급-전사";
                        case "31549":
                            return "90 B등급-전사";
                        case "31550":
                            return "90 A등급-전사";
                        case "31551":
                            return "90 S등급-전사";
                        case "31552":
                            return "90 A등급 세트-전사";
                        case "31553":
                            return "90 S등급 세트-전사";
                        case "31554":
                            return "100 D등급-전사";
                        case "31555":
                            return "100 C등급-전사";
                        case "31556":
                            return "100 B등급-전사";
                        case "31557":
                            return "100 A등급-전사";
                        case "31558":
                            return "100 S등급-전사";
                        case "31559":
                            return "100 A등급 세트-전사";
                        case "31560":
                            return "100 S등급 세트-전사";
                        case "31561":
                            return "1 D등급-마법사";
                        case "31562":
                            return "1 C등급-마법사";
                        case "31563":
                            return "1 B등급-마법사";
                        case "31564":
                            return "1 A등급-마법사";
                        case "31565":
                            return "10 D등급-마법사";
                        case "31566":
                            return "10 C등급-마법사";
                        case "31567":
                            return "10 B등급-마법사";
                        case "31568":
                            return "10 A등급-마법사";
                        case "31569":
                            return "30 D등급-마법사";
                        case "31570":
                            return "30 C등급-마법사";
                        case "31571":
                            return "30 B등급-마법사";
                        case "31572":
                            return "30 A등급-마법사";
                        case "31573":
                            return "30 S등급-마법사";
                        case "31574":
                            return "40 D등급-마법사";
                        case "31575":
                            return "40 C등급-마법사";
                        case "31576":
                            return "40 B등급-마법사";
                        case "31577":
                            return "40 A등급-마법사";
                        case "31578":
                            return "40 S등급-마법사";
                        case "31579":
                            return "50 D등급-마법사";
                        case "31580":
                            return "50 C등급-마법사";
                        case "31581":
                            return "50 B등급-마법사";
                        case "31582":
                            return "50 A등급-마법사";
                        case "31583":
                            return "50 S등급-마법사";
                        case "31584":
                            return "50 A등급 세트-마법사";
                        case "31585":
                            return "50 S등급 세트-마법사";
                        case "31586":
                            return "60 D등급-마법사";
                        case "31587":
                            return "60 C등급-마법사";
                        case "31588":
                            return "60 B등급-마법사";
                        case "31589":
                            return "60 A등급-마법사";
                        case "31590":
                            return "60 S등급-마법사";
                        case "31591":
                            return "60 A등급 세트-마법사";
                        case "31592":
                            return "60 S등급 세트-마법사";
                        case "31593":
                            return "70 D등급-마법사";
                        case "31594":
                            return "70 C등급-마법사";
                        case "31595":
                            return "70 B등급-마법사";
                        case "31596":
                            return "70 A등급-마법사";
                        case "31597":
                            return "70 S등급-마법사";
                        case "31598":
                            return "70 A등급 세트-마법사";
                        case "31599":
                            return "70 S등급 세트-마법사";
                        case "31600":
                            return "80 D등급-마법사";
                        case "31601":
                            return "80 C등급-마법사";
                        case "31602":
                            return "80 B등급-마법사";
                        case "31603":
                            return "80 A등급-마법사";
                        case "31604":
                            return "80 S등급-마법사";
                        case "31605":
                            return "80 A등급 세트-마법사";
                        case "31606":
                            return "80 S등급 세트-마법사";
                        case "31607":
                            return "90 D등급-마법사";
                        case "31608":
                            return "90 C등급-마법사";
                        case "31609":
                            return "90 B등급-마법사";
                        case "31610":
                            return "90 A등급-마법사";
                        case "31611":
                            return "90 S등급-마법사";
                        case "31612":
                            return "90 A등급 세트-마법사";
                        case "31613":
                            return "90 S등급 세트-마법사";
                        case "31614":
                            return "100 D등급-마법사";
                        case "31615":
                            return "100 C등급-마법사";
                        case "31616":
                            return "100 B등급-마법사";
                        case "31617":
                            return "100 A등급-마법사";
                        case "31618":
                            return "100 S등급-마법사";
                        case "31619":
                            return "100 A등급 세트-마법사";
                        case "31620":
                            return "100 S등급 세트-마법사";
                        case "31621":
                            return "1 D등급-암살자";
                        case "31622":
                            return "1 C등급-암살자";
                        case "31623":
                            return "1 B등급-암살자";
                        case "31624":
                            return "1 A등급-암살자";
                        case "31625":
                            return "10 D등급-암살자";
                        case "31626":
                            return "10 C등급-암살자";
                        case "31627":
                            return "10 B등급-암살자";
                        case "31628":
                            return "10 A등급-암살자";
                        case "31629":
                            return "30 D등급-암살자";
                        case "31630":
                            return "30 C등급-암살자";
                        case "31631":
                            return "30 B등급-암살자";
                        case "31632":
                            return "30 A등급-암살자";
                        case "31633":
                            return "30 S등급-암살자";
                        case "31634":
                            return "40 D등급-암살자";
                        case "31635":
                            return "40 C등급-암살자";
                        case "31636":
                            return "40 B등급-암살자";
                        case "31637":
                            return "40 A등급-암살자";
                        case "31638":
                            return "40 S등급-암살자";
                        case "31639":
                            return "50 D등급-암살자";
                        case "31640":
                            return "50 C등급-암살자";
                        case "31641":
                            return "50 B등급-암살자";
                        case "31642":
                            return "50 A등급-암살자";
                        case "31643":
                            return "50 S등급-암살자";
                        case "31644":
                            return "50 A등급 세트-암살자";
                        case "31645":
                            return "50 S등급 세트-암살자";
                        case "31646":
                            return "60 D등급-암살자";
                        case "31647":
                            return "60 C등급-암살자";
                        case "31648":
                            return "60 B등급-암살자";
                        case "31649":
                            return "60 A등급-암살자";
                        case "31650":
                            return "60 S등급-암살자";
                        case "31651":
                            return "60 A등급 세트-암살자";
                        case "31652":
                            return "60 S등급 세트-암살자";
                        case "31653":
                            return "70 D등급-암살자";
                        case "31654":
                            return "70 C등급-암살자";
                        case "31655":
                            return "70 B등급-암살자";
                        case "31656":
                            return "70 A등급-암살자";
                        case "31657":
                            return "70 S등급-암살자";
                        case "31658":
                            return "70 A등급 세트-암살자";
                        case "31659":
                            return "70 S등급 세트-암살자";
                        case "31660":
                            return "80 D등급-암살자";
                        case "31661":
                            return "80 C등급-암살자";
                        case "31662":
                            return "80 B등급-암살자";
                        case "31663":
                            return "80 A등급-암살자";
                        case "31664":
                            return "80 S등급-암살자";
                        case "31665":
                            return "80 A등급 세트-암살자";
                        case "31666":
                            return "80 S등급 세트-암살자";
                        case "31667":
                            return "90 D등급-암살자";
                        case "31668":
                            return "90 C등급-암살자";
                        case "31669":
                            return "90 B등급-암살자";
                        case "31670":
                            return "90 A등급-암살자";
                        case "31671":
                            return "90 S등급-암살자";
                        case "31672":
                            return "90 A등급 세트-암살자";
                        case "31673":
                            return "90 S등급 세트-암살자";
                        case "31674":
                            return "100 D등급-암살자";
                        case "31675":
                            return "100 C등급-암살자";
                        case "31676":
                            return "100 B등급-암살자";
                        case "31677":
                            return "100 A등급-암살자";
                        case "31678":
                            return "100 S등급-암살자";
                        case "31679":
                            return "100 A등급 세트-암살자";
                        case "31680":
                            return "100 S등급 세트-암살자";
                        case "31681":
                            return "1 D등급-소환사";
                        case "31682":
                            return "1 C등급-소환사";
                        case "31683":
                            return "1 B등급-소환사";
                        case "31684":
                            return "1 A등급-소환사";
                        case "31685":
                            return "10 D등급-소환사";
                        case "31686":
                            return "10 C등급-소환사";
                        case "31687":
                            return "10 B등급-소환사";
                        case "31688":
                            return "10 A등급-소환사";
                        case "31689":
                            return "30 D등급-소환사";
                        case "31690":
                            return "30 C등급-소환사";
                        case "31691":
                            return "30 B등급-소환사";
                        case "31692":
                            return "30 A등급-소환사";
                        case "31693":
                            return "30 S등급-소환사";
                        case "31694":
                            return "40 D등급-소환사";
                        case "31695":
                            return "40 C등급-소환사";
                        case "31696":
                            return "40 B등급-소환사";
                        case "31697":
                            return "40 A등급-소환사";
                        case "31698":
                            return "40 S등급-소환사";
                        case "31699":
                            return "50 D등급-소환사";
                        case "31700":
                            return "50 C등급-소환사";
                        case "31701":
                            return "50 B등급-소환사";
                        case "31702":
                            return "50 A등급-소환사";
                        case "31703":
                            return "50 S등급-소환사";
                        case "31704":
                            return "50 A등급 세트-소환사";
                        case "31705":
                            return "50 S등급 세트-소환사";
                        case "31706":
                            return "60 D등급-소환사";
                        case "31707":
                            return "60 C등급-소환사";
                        case "31708":
                            return "60 B등급-소환사";
                        case "31709":
                            return "60 A등급-소환사";
                        case "31710":
                            return "60 S등급-소환사";
                        case "31711":
                            return "60 A등급 세트-소환사";
                        case "31712":
                            return "60 S등급 세트-소환사";
                        case "31713":
                            return "70 D등급-소환사";
                        case "31714":
                            return "70 C등급-소환사";
                        case "31715":
                            return "70 B등급-소환사";
                        case "31716":
                            return "70 A등급-소환사";
                        case "31717":
                            return "70 S등급-소환사";
                        case "31718":
                            return "70 A등급 세트-소환사";
                        case "31719":
                            return "70 S등급 세트-소환사";
                        case "31720":
                            return "80 D등급-소환사";
                        case "31721":
                            return "80 C등급-소환사";
                        case "31722":
                            return "80 B등급-소환사";
                        case "31723":
                            return "80 A등급-소환사";
                        case "31724":
                            return "80 S등급-소환사";
                        case "31725":
                            return "80 A등급 세트-소환사";
                        case "31726":
                            return "80 S등급 세트-소환사";
                        case "31727":
                            return "90 D등급-소환사";
                        case "31728":
                            return "90 C등급-소환사";
                        case "31729":
                            return "90 B등급-소환사";
                        case "31730":
                            return "90 A등급-소환사";
                        case "31731":
                            return "90 S등급-소환사";
                        case "31732":
                            return "90 A등급 세트-소환사";
                        case "31733":
                            return "90 S등급 세트-소환사";
                        case "31734":
                            return "100 D등급-소환사";
                        case "31735":
                            return "100 C등급-소환사";
                        case "31736":
                            return "100 B등급-소환사";
                        case "31737":
                            return "100 A등급-소환사";
                        case "31738":
                            return "100 S등급-소환사";
                        case "31739":
                            return "100 A등급 세트-소환사";
                        case "31740":
                            return "100 S등급 세트-소환사";
                        case "31741":
                            return "1 D등급-공용";
                        case "31742":
                            return "1 C등급-공용";
                        case "31743":
                            return "1 B등급-공용";
                        case "31744":
                            return "1 A등급-공용";
                        case "31745":
                            return "10 D등급-공용";
                        case "31746":
                            return "10 C등급-공용";
                        case "31747":
                            return "10 B등급-공용";
                        case "31748":
                            return "10 A등급-공용";
                        case "31749":
                            return "30 D등급-공용";
                        case "31750":
                            return "30 C등급-공용";
                        case "31751":
                            return "30 B등급-공용";
                        case "31752":
                            return "30 A등급-공용";
                        case "31753":
                            return "30 S등급-공용";
                        case "31754":
                            return "40 D등급-공용";
                        case "31755":
                            return "40 C등급-공용";
                        case "31756":
                            return "40 B등급-공용";
                        case "31757":
                            return "40 A등급-공용";
                        case "31758":
                            return "40 S등급-공용";
                        case "31759":
                            return "50 D등급-공용";
                        case "31760":
                            return "50 C등급-공용";
                        case "31761":
                            return "50 B등급-공용";
                        case "31762":
                            return "50 A등급-공용";
                        case "31763":
                            return "50 S등급-공용";
                        case "31764":
                            return "50 A등급 물리공격 세트-공용";
                        case "31765":
                            return "50 S등급 물리공격 세트--공용";
                        case "31766":
                            return "50 A등급 마법공격 세트-공용";
                        case "31767":
                            return "50 S등급 마법공격 세트-공용";
                        case "31768":
                            return "60 D등급-공용";
                        case "31769":
                            return "60 C등급-공용";
                        case "31770":
                            return "60 B등급-공용";
                        case "31771":
                            return "60 A등급-공용";
                        case "31772":
                            return "60 S등급-공용";
                        case "31773":
                            return "60 A등급 물리공격 세트-공용";
                        case "31774":
                            return "60 S등급 물리공격 세트-공용";
                        case "31775":
                            return "60 A등급 마법공격 세트-공용";
                        case "31776":
                            return "60 S등급 마법공격 세트-공용";
                        case "31777":
                            return "70 D등급-공용";
                        case "31778":
                            return "70 C등급-공용";
                        case "31779":
                            return "70 B등급-공용";
                        case "31780":
                            return "70 A등급-공용";
                        case "31781":
                            return "70 S등급-공용";
                        case "31782":
                            return "70 A등급 물리공격 세트-공용";
                        case "31783":
                            return "70 S등급 물리공격 세트-공용";
                        case "31784":
                            return "70 A등급 마법공격 세트-공용";
                        case "31785":
                            return "70 S등급 마법공격 세트-공용";
                        case "31786":
                            return "80 D등급-공용";
                        case "31787":
                            return "80 C등급-공용";
                        case "31788":
                            return "80 B등급-공용";
                        case "31789":
                            return "80 A등급-공용";
                        case "31790":
                            return "80 S등급-공용";
                        case "31791":
                            return "80 A등급 물리공격 세트-공용";
                        case "31792":
                            return "80 S등급 물리공격 세트-공용";
                        case "31793":
                            return "80 A등급 마법공격 세트-공용";
                        case "31794":
                            return "80 S등급 마법공격 세트-공용";
                        case "31795":
                            return "90 D등급-공용";
                        case "31796":
                            return "90 C등급-공용";
                        case "31797":
                            return "90 B등급-공용";
                        case "31798":
                            return "90 A등급-공용";
                        case "31799":
                            return "90 S등급-공용";
                        case "31800":
                            return "90 A등급 물리공격 세트-공용";
                        case "31801":
                            return "90 S등급 물리공격 세트-공용";
                        case "31802":
                            return "90 A등급 마법공격 세트-공용";
                        case "31803":
                            return "90 S등급 마법공격 세트-공용";
                        case "31804":
                            return "100 D등급-공용";
                        case "31805":
                            return "100 C등급-공용";
                        case "31806":
                            return "100 B등급-공용";
                        case "31807":
                            return "100 A등급-공용";
                        case "31808":
                            return "100 S등급-공용";
                        case "31809":
                            return "100 A등급 물리공격 세트-공용";
                        case "31810":
                            return "100 S등급 물리공격 세트-공용";
                        case "31811":
                            return "100 A등급 마법공격 세트-공용";
                        case "31812":
                            return "100 S등급 마법공격 세트-공용";
                        case "31840":
                            return "참회 스크롤";
                        case "31850":
                            return "코라크의 보물";
                        case "31851":
                            return "오아시스 유적";
                        case "31852":
                            return "황실의 보물";
                        case "31856":
                            return "왕성전 승리 보상 상자";
                        case "31857":
                            return "상급 거점전 승리 상자";
                        case "31858":
                            return "중급 거점전 승리 상자";
                        case "31859":
                            return "일반 거점전 승리 상자";
                        case "31900":
                            return "진영전 입장 스크롤";
                        case "31901":
                            return "1-1 던전 입장 스크롤";
                        case "31902":
                            return "1-2 던전 입장 스크롤";
                        case "31903":
                            return "1-3 던전 입장 스크롤";
                        case "31904":
                            return "1-4 던전 입장 스크롤";
                        case "31905":
                            return "1-5 던전 입장 스크롤";
                        case "31906":
                            return "1-6 던전 입장 스크롤";
                        case "31907":
                            return "2-1 던전 입장 스크롤";
                        case "31908":
                            return "2-2 던전 입장 스크롤";
                        case "31909":
                            return "2-3 던전 입장 스크롤";
                        case "31910":
                            return "2-4 던전 입장 스크롤";
                        case "31911":
                            return "2-5 던전 입장 스크롤";
                        case "31912":
                            return "2-6 던전 입장 스크롤";
                        case "31913":
                            return "3-1 던전 입장 스크롤";
                        case "31914":
                            return "3-2 던전 입장 스크롤";
                        case "31915":
                            return "3-3 던전 입장 스크롤";
                        case "31916":
                            return "3-4 던전 입장 스크롤";
                        case "31917":
                            return "3-5 던전 입장 스크롤";
                        case "31918":
                            return "3-6 던전 입장 스크롤";
                        case "31919":
                            return "20 마왕 일반 던전 입장 스크롤";
                        case "31920":
                            return "30 마왕 일반 던전 입장 스크롤";
                        case "31921":
                            return "30 마왕 어려움 던전 입장 스크롤";
                        case "31922":
                            return "40 마왕 일반 던전 입장 스크롤";
                        case "31923":
                            return "40 마왕 어려움 던전 입장 스크롤";
                        case "31924":
                            return "40 마왕 악몽 던전 입장 스크롤";
                        case "31925":
                            return "50 마왕 일반 던전 입장 스크롤";
                        case "31926":
                            return "50 마왕 어려움 던전 입장 스크롤";
                        case "31927":
                            return "50 마왕 악몽 던전 입장 스크롤";
                        case "31928":
                            return "60 마왕 일반 던전 입장 스크롤";
                        case "31929":
                            return "60 마왕 어려움 던전 입장 스크롤";
                        case "31930":
                            return "60 마왕 악몽 던전 입장 스크롤";
                        case "31931":
                            return "올타이 비경 던전 입장 스크롤";
                        case "32000":
                            return "타임 더스트";
                        case "32001":
                            return "타이스의 심장";
                        case "32002":
                            return "나이트메어";
                        case "32003":
                            return "운명의 별";
                        case "32004":
                            return "치료물약";
                        case "32005":
                            return "횃불";
                        case "32006":
                            return "실버";
                        case "32007":
                            return "기폭장치";
                        case "32008":
                            return "레일 부품";
                        case "32009":
                            return "헬파이어";
                        case "32010":
                            return "포스";
                        case "32011":
                            return "물의 힘";
                        case "32012":
                            return "마안의 힘";
                        case "32013":
                            return "해독약";
                        case "32014":
                            return "횃불";
                        case "32015":
                            return "엘프의 펜던트";
                        case "32016":
                            return "자연의 빛";
                        case "32017":
                            return "인장";
                        case "32018":
                            return "횃불";
                        case "32019":
                            return "횃불";
                        case "32020":
                            return "레일 부품";
                        case "32021":
                            return "레일 부품";
                        case "32022":
                            return "횃불";
                        case "32023":
                            return "탄약 상자";
                        case "32024":
                            return "횃불";
                        case "32025":
                            return "레일 부품";
                        case "32026":
                            return "횃불";
                        case "32027":
                            return "레일 부품";
                        case "32028":
                            return "인장";
                        case "32029":
                            return "인장";
                        case "32030":
                            return "횃불";
                        case "32031":
                            return "횃불";
                        case "32032":
                            return "횃불";
                        case "32033":
                            return "치료물약";
                        case "32034":
                            return "맑은 물";
                        case "32035":
                            return "약초";
                        case "32036":
                            return "인장";
                        case "32037":
                            return "호루라기";
                        case "32038":
                            return "인장";
                        case "32039":
                            return "인장";
                        case "32040":
                            return "영패";
                        case "32041":
                            return "성물";
                        case "32042":
                            return "대청소";
                        case "32043":
                            return "술";
                        case "32044":
                            return "요리";
                        case "32045":
                            return "솔트 비스킷";
                        case "32046":
                            return "횃불";
                        case "32047":
                            return "레일 부품";
                        case "32048":
                            return "횃불";
                        case "32049":
                            return "레일 부품";
                        case "32050":
                            return "횃불";
                        case "32051":
                            return "레일 부품";
                        case "32052":
                            return "횃불";
                        case "32053":
                            return "레일 부품";
                        case "32054":
                            return "횃불";
                        case "32055":
                            return "레일 부품";
                        case "32056":
                            return "횃불";
                        case "32057":
                            return "레일 부품";
                        case "32058":
                            return "횃불";
                        case "32059":
                            return "레일 부품";
                        case "32060":
                            return "횃불";
                        case "32061":
                            return "레일 부품";
                        case "40001":
                            return "실버";
                        case "40002":
                            return "소량의 실버";
                        case "40003":
                            return "실버 더미";
                        case "40004":
                            return "대량의 실버";
                        case "40005":
                            return "실버 무더기";
                        case "40101":
                            return "블루 다이아";
                        case "40102":
                            return "소량의 블루 다이아";
                        case "40103":
                            return "블루 다이아 더미";
                        case "40104":
                            return "대량의 블루 다이아";
                        case "40201":
                            return "경험치";
                        case "40202":
                            return "소량의 경험치";
                        case "40203":
                            return "경험치 더미";
                        case "40204":
                            return "대량의 경험치";
                        case "40205":
                            return "경험치 무더기";
                        case "40301":
                            return "Lv1 D등급 장비";
                        case "40302":
                            return "Lv1 C등급 장비";
                        case "40303":
                            return "Lv1 B등급 장비";
                        case "40304":
                            return "Lv1 A등급 장비";
                        case "40305":
                            return "Lv1 S등급 장비";
                        case "40306":
                            return "Lv1 SS등급 장비";
                        case "40311":
                            return "Lv10 D등급 장비";
                        case "40312":
                            return "Lv10 C등급 장비";
                        case "40313":
                            return "Lv10 B등급 장비";
                        case "40314":
                            return "Lv10 A등급 장비";
                        case "40315":
                            return "Lv10 S등급 장비";
                        case "40316":
                            return "Lv10 SS등급 장비";
                        case "40331":
                            return "Lv30 D등급 장비";
                        case "40332":
                            return "Lv30 C등급 장비";
                        case "40333":
                            return "Lv30 B등급 장비";
                        case "40334":
                            return "Lv30 A등급 장비";
                        case "40335":
                            return "Lv30 S등급 장비";
                        case "40336":
                            return "Lv30 SS등급 장비";
                        case "40341":
                            return "Lv40 D등급 장비";
                        case "40342":
                            return "Lv40 C등급 장비";
                        case "40343":
                            return "Lv40 B등급 장비";
                        case "40344":
                            return "Lv40 A등급 장비";
                        case "40345":
                            return "Lv40 S등급 장비";
                        case "40346":
                            return "Lv40 SS등급 장비";
                        case "40351":
                            return "Lv50 D등급 장비";
                        case "40352":
                            return "Lv50 C등급 장비";
                        case "40353":
                            return "Lv50 B등급 장비";
                        case "40354":
                            return "Lv50 A등급 장비";
                        case "40355":
                            return "Lv50 S등급 장비";
                        case "40356":
                            return "Lv50 SS등급 장비";
                        case "40361":
                            return "Lv60 D등급 장비";
                        case "40362":
                            return "Lv60 C등급 장비";
                        case "40363":
                            return "Lv60 B등급 장비";
                        case "40364":
                            return "Lv60 A등급 장비";
                        case "40365":
                            return "Lv60 S등급 장비";
                        case "40366":
                            return "Lv60 SS등급 장비";
                        case "40371":
                            return "Lv70 D등급 장비";
                        case "40372":
                            return "Lv70 C등급 장비";
                        case "40373":
                            return "Lv70 B등급 장비";
                        case "40374":
                            return "Lv70 A등급 장비";
                        case "40375":
                            return "Lv70 S등급 장비";
                        case "40376":
                            return "Lv70 SS등급 장비";
                        case "40381":
                            return "Lv80 D등급 장비";
                        case "40382":
                            return "Lv80 C등급 장비";
                        case "40383":
                            return "Lv80 B등급 장비";
                        case "40384":
                            return "Lv80 A등급 장비";
                        case "40385":
                            return "Lv80 S등급 장비";
                        case "40386":
                            return "Lv80 SS등급 장비";
                        case "40391":
                            return "D등급 장비";
                        case "40392":
                            return "C등급 장비";
                        case "40393":
                            return "B등급 장비";
                        case "40394":
                            return "A등급 장비";
                        case "40395":
                            return "S등급 장비";
                        case "40396":
                            return "SS등급 장비";
                        case "40401":
                            return "Lv1 보석";
                        case "40402":
                            return "Lv2 보석";
                        case "40403":
                            return "Lv3 보석";
                        case "40404":
                            return "Lv4 보석";
                        case "40405":
                            return "Lv5 보석";
                        case "40406":
                            return "Lv6 보석";
                        case "40407":
                            return "Lv7 보석";
                        case "40408":
                            return "Lv8 보석";
                        case "40409":
                            return "Lv9 보석";
                        case "40410":
                            return "Lv10 보석";
                        case "41001":
                            return "부서진 무기";
                        case "41002":
                            return "낡은 회중시계";
                        case "41003":
                            return "원광석";
                        case "41004":
                            return "암흑의 수정구";
                        case "41005":
                            return "낡은 황금단지";
                        case "41010":
                            return "Lv10 보석";
                        case "41011":
                            return "Lv10 보석";
                        case "42001":
                            return "공성병기";
                        case "42002":
                            return "수성전차";
                        case "42020":
                            return "회복 버프";
                        case "42999":
                            return "회복 버프";
                        default:
                            return value;
                    }
                    #endregion
                }
                else
                {
                    #region 中文
                    switch (value)
                    {
                        case "10000":
                            return "5银币";
                        case "10001":
                            return "10银币";
                        case "10002":
                            return "15银币";
                        case "10003":
                            return "25银币";
                        case "10004":
                            return "50银币";
                        case "10005":
                            return "100银币";
                        case "10006":
                            return "250银币";
                        case "10007":
                            return "500银币";
                        case "10008":
                            return "1000银币";
                        case "10009":
                            return "2500银币";
                        case "10010":
                            return "5000银币";
                        case "10011":
                            return "10000银币";
                        case "10100":
                            return "银币活动Lv20~24第1档";
                        case "10101":
                            return "银币活动Lv25~29第1档";
                        case "10102":
                            return "银币活动Lv30~34第1档";
                        case "10103":
                            return "银币活动Lv35~39第1档";
                        case "10104":
                            return "银币活动Lv40~44第1档";
                        case "10105":
                            return "银币活动Lv45~49第1档";
                        case "10106":
                            return "银币活动Lv50~54第1档";
                        case "10107":
                            return "银币活动Lv55~59第1档";
                        case "10108":
                            return "银币活动Lv60~64第1档";
                        case "10109":
                            return "银币活动Lv65~69第1档";
                        case "10110":
                            return "银币活动Lv70~74第1档";
                        case "10111":
                            return "银币活动Lv75~79第1档";
                        case "10112":
                            return "银币活动Lv80~84第1档";
                        case "10113":
                            return "银币活动Lv85~89第1档";
                        case "10114":
                            return "银币活动Lv90~94第1档";
                        case "10115":
                            return "银币活动Lv95~100第1档";
                        case "10116":
                            return "银币活动Lv20~24第2档";
                        case "10117":
                            return "银币活动Lv25~29第2档";
                        case "10118":
                            return "银币活动Lv30~34第2档";
                        case "10119":
                            return "银币活动Lv35~39第2档";
                        case "10120":
                            return "银币活动Lv40~44第2档";
                        case "10121":
                            return "银币活动Lv45~49第2档";
                        case "10122":
                            return "银币活动Lv50~54第2档";
                        case "10123":
                            return "银币活动Lv55~59第2档";
                        case "10124":
                            return "银币活动Lv60~64第2档";
                        case "10125":
                            return "银币活动Lv65~69第2档";
                        case "10126":
                            return "银币活动Lv70~74第2档";
                        case "10127":
                            return "银币活动Lv75~79第2档";
                        case "10128":
                            return "银币活动Lv80~84第2档";
                        case "10129":
                            return "银币活动Lv85~89第2档";
                        case "10130":
                            return "银币活动Lv90~94第2档";
                        case "10131":
                            return "银币活动Lv95~100第2档";
                        case "10132":
                            return "银币活动Lv20~24第3档";
                        case "10133":
                            return "银币活动Lv25~29第3档";
                        case "10134":
                            return "银币活动Lv30~34第3档";
                        case "10135":
                            return "银币活动Lv35~39第3档";
                        case "10136":
                            return "银币活动Lv40~44第3档";
                        case "10137":
                            return "银币活动Lv45~49第3档";
                        case "10138":
                            return "银币活动Lv50~54第3档";
                        case "10139":
                            return "银币活动Lv55~59第3档";
                        case "10140":
                            return "银币活动Lv60~64第3档";
                        case "10141":
                            return "银币活动Lv65~69第3档";
                        case "10142":
                            return "银币活动Lv70~74第3档";
                        case "10143":
                            return "银币活动Lv75~79第3档";
                        case "10144":
                            return "银币活动Lv80~84第3档";
                        case "10145":
                            return "银币活动Lv85~89第3档";
                        case "10146":
                            return "银币活动Lv90~94第3档";
                        case "10147":
                            return "银币活动Lv95~100第3档";
                        case "10148":
                            return "银币活动Lv20~24第4档";
                        case "10149":
                            return "银币活动Lv25~29第4档";
                        case "10150":
                            return "银币活动Lv30~34第4档";
                        case "10151":
                            return "银币活动Lv35~39第4档";
                        case "10152":
                            return "银币活动Lv40~44第4档";
                        case "10153":
                            return "银币活动Lv45~49第4档";
                        case "10154":
                            return "银币活动Lv50~54第4档";
                        case "10155":
                            return "银币活动Lv55~59第4档";
                        case "10156":
                            return "银币活动Lv60~64第4档";
                        case "10157":
                            return "银币活动Lv65~69第4档";
                        case "10158":
                            return "银币活动Lv70~74第4档";
                        case "10159":
                            return "银币活动Lv75~79第4档";
                        case "10160":
                            return "银币活动Lv80~84第4档";
                        case "10161":
                            return "银币活动Lv85~89第4档";
                        case "10162":
                            return "银币活动Lv90~94第4档";
                        case "10163":
                            return "银币活动Lv95~100第4档";
                        case "10164":
                            return "银币活动Lv20~24第5档";
                        case "10165":
                            return "银币活动Lv25~29第5档";
                        case "10166":
                            return "银币活动Lv30~34第5档";
                        case "10167":
                            return "银币活动Lv35~39第5档";
                        case "10168":
                            return "银币活动Lv40~44第5档";
                        case "10169":
                            return "银币活动Lv45~49第5档";
                        case "10170":
                            return "银币活动Lv50~54第5档";
                        case "10171":
                            return "银币活动Lv55~59第5档";
                        case "10172":
                            return "银币活动Lv60~64第5档";
                        case "10173":
                            return "银币活动Lv65~69第5档";
                        case "10174":
                            return "银币活动Lv70~74第5档";
                        case "10175":
                            return "银币活动Lv75~79第5档";
                        case "10176":
                            return "银币活动Lv80~84第5档";
                        case "10177":
                            return "银币活动Lv85~89第5档";
                        case "10178":
                            return "银币活动Lv90~94第5档";
                        case "10179":
                            return "银币活动Lv95~100第5档";
                        case "11000":
                            return "银币";
                        case "11001":
                            return "银币堆-1";
                        case "11002":
                            return "银币堆-2";
                        case "11003":
                            return "银币堆-3";
                        case "11004":
                            return "银币堆-4";
                        case "12000":
                            return "蓝钻";
                        case "12001":
                            return "蓝钻石袋子-1";
                        case "12002":
                            return "蓝钻石袋子-2";
                        case "12003":
                            return "蓝钻石袋子-3";
                        case "14000":
                            return "经验(纯显示用)";
                        case "14001":
                            return "经验(纯显示用)";
                        case "14002":
                            return "经验(纯显示用)";
                        case "14003":
                            return "经验(纯显示用)";
                        case "14004":
                            return "经验(纯显示用)";
                        case "14011":
                            return "体力（纯显示）";
                        case "15000":
                            return "阵营货币(是货币不进背包)";
                        case "16000":
                            return "公会贡献货币(是自己的公会货币不进背包)";
                        case "17000":
                            return "公会资金货币(是公会总的货币不进背包)";
                        case "18000":
                            return "金币";
                        case "19000":
                            return "红钻";
                        case "19010":
                            return "竞技徽章";
                        case "19020":
                            return "公会木材(是公会总的木材不进背包)";
                        case "19999":
                            return "无效道具(禁止配置掉落)";
                        case "20000":
                            return "强化石-绿(装备强化用)";
                        case "20001":
                            return "强化石-蓝(装备强化用)";
                        case "20002":
                            return "强化石-紫(装备强化用)";
                        case "20003":
                            return "强化石-橙(装备强化用)";
                        case "20004":
                            return "强化石-红(装备强化用)";
                        case "20100":
                            return "魔法书(技能升级用)";
                        case "20200":
                            return "切换符文(废弃)";
                        case "20300":
                            return "洗点水(天赋重置用)";
                        case "20400":
                            return "洗炼石_蓝(绑)";
                        case "20401":
                            return "洗炼石_紫(绑)";
                        case "20402":
                            return "洗炼石_橙(绑)";
                        case "20403":
                            return "洗炼石_橙+(绑)";
                        case "20410":
                            return "洗炼石_蓝";
                        case "20411":
                            return "洗炼石_紫";
                        case "20412":
                            return "洗炼石_橙";
                        case "20413":
                            return "洗炼石_橙+";
                        case "20500":
                            return "时装兑换券(非绑定)";
                        case "20501":
                            return "时装兑换券(绑定)";
                        case "20512":
                            return "万圣节时装兑换券（头）绑定";
                        case "20513":
                            return "万圣节时装兑换券（衣服）绑定";
                        case "20514":
                            return "万圣节时装兑换券（武器）绑定";
                        case "20515":
                            return "万圣节时装兑换券（头）绑定";
                        case "20516":
                            return "万圣节时装兑换券（衣服）绑定";
                        case "20517":
                            return "万圣节时装兑换券（武器）绑定";
                        case "20600":
                            return "铁矿石(武器打造用)";
                        case "20601":
                            return "铜矿石(武器打造用)";
                        case "20602":
                            return "银矿石(武器打造用)";
                        case "20603":
                            return "金矿石(武器打造用)";
                        case "20604":
                            return "炼狱矿石(武器打造用)";
                        case "20605":
                            return "炼狱矿石(武器打造用)";
                        case "20620":
                            return "亚麻布(防具打造用)";
                        case "20621":
                            return "毛绒布(防具打造用)";
                        case "20622":
                            return "纺丝布(防具打造用)";
                        case "20623":
                            return "符文布(防具打造用)";
                        case "20624":
                            return "神域之布(防具打造用)";
                        case "20625":
                            return "神域之布(防具打造用)";
                        case "20640":
                            return "纯色水晶(饰品打造用)";
                        case "20641":
                            return "生命水晶(饰品打造用)";
                        case "20642":
                            return "天空水晶(饰品打造用)";
                        case "20643":
                            return "秘法水晶(饰品打造用)";
                        case "20644":
                            return "龙神水晶(饰品打造用)";
                        case "20645":
                            return "龙神水晶(饰品打造用)";
                        case "20660":
                            return "魔法精华(装备精炼用)";
                        case "20661":
                            return "奇异精华(装备精炼用)";
                        case "20662":
                            return "虚空精华(装备精炼用)";
                        case "20663":
                            return "星界精华(装备精炼用)";
                        case "20664":
                            return "永恒精华(装备精炼用)";
                        case "20700":
                            return "武器卷轴-白(装备附魔用)";
                        case "20701":
                            return "武器卷轴-绿(装备附魔用)";
                        case "20702":
                            return "武器卷轴-蓝(装备附魔用)";
                        case "20703":
                            return "武器卷轴-紫(装备附魔用)";
                        case "20704":
                            return "武器卷轴-橙(装备附魔用)";
                        case "20710":
                            return "项链卷轴-白(装备附魔用)";
                        case "20711":
                            return "项链卷轴-绿(装备附魔用)";
                        case "20712":
                            return "项链卷轴-蓝(装备附魔用)";
                        case "20713":
                            return "项链卷轴-紫(装备附魔用)";
                        case "20714":
                            return "项链卷轴-橙(装备附魔用)";
                        case "20720":
                            return "戒指卷轴-白(装备附魔用)";
                        case "20721":
                            return "戒指卷轴-绿(装备附魔用)";
                        case "20722":
                            return "戒指卷轴-蓝(装备附魔用)";
                        case "20723":
                            return "戒指卷轴-紫(装备附魔用)";
                        case "20724":
                            return "戒指卷轴-橙(装备附魔用)";
                        case "20730":
                            return "饰品卷轴-白(装备附魔用)";
                        case "20731":
                            return "饰品卷轴-绿(装备附魔用)";
                        case "20732":
                            return "饰品卷轴-蓝(装备附魔用)";
                        case "20733":
                            return "饰品卷轴-紫(装备附魔用)";
                        case "20734":
                            return "饰品卷轴-橙(装备附魔用)";
                        case "20740":
                            return "头盔卷轴-白(装备附魔用)";
                        case "20741":
                            return "头盔卷轴-绿(装备附魔用)";
                        case "20742":
                            return "头盔卷轴-蓝(装备附魔用)";
                        case "20743":
                            return "头盔卷轴-紫(装备附魔用)";
                        case "20744":
                            return "头盔卷轴-橙(装备附魔用)";
                        case "20750":
                            return "胸甲卷轴-白(装备附魔用)";
                        case "20751":
                            return "胸甲卷轴-绿(装备附魔用)";
                        case "20752":
                            return "胸甲卷轴-蓝(装备附魔用)";
                        case "20753":
                            return "胸甲卷轴-紫(装备附魔用)";
                        case "20754":
                            return "胸甲卷轴-橙(装备附魔用)";
                        case "20760":
                            return "护手卷轴-白(装备附魔用)";
                        case "20761":
                            return "护手卷轴-绿(装备附魔用)";
                        case "20762":
                            return "护手卷轴-蓝(装备附魔用)";
                        case "20763":
                            return "护手卷轴-紫(装备附魔用)";
                        case "20764":
                            return "护手卷轴-橙(装备附魔用)";
                        case "20770":
                            return "鞋子卷轴-白(装备附魔用)";
                        case "20771":
                            return "鞋子卷轴-绿(装备附魔用)";
                        case "20772":
                            return "鞋子卷轴-蓝(装备附魔用)";
                        case "20773":
                            return "鞋子卷轴-紫(装备附魔用)";
                        case "20774":
                            return "鞋子卷轴-橙(装备附魔用)";
                        case "20780":
                            return "裤子卷轴-白(装备附魔用)";
                        case "20781":
                            return "裤子卷轴-绿(装备附魔用)";
                        case "20782":
                            return "裤子卷轴-蓝(装备附魔用)";
                        case "20783":
                            return "裤子卷轴-紫(装备附魔用)";
                        case "20784":
                            return "裤子卷轴-橙(装备附魔用)";
                        case "20790":
                            return "耳环卷轴-白(装备附魔用)";
                        case "20791":
                            return "耳环卷轴-绿(装备附魔用)";
                        case "20792":
                            return "耳环卷轴-蓝(装备附魔用)";
                        case "20793":
                            return "耳环卷轴-紫(装备附魔用)";
                        case "20794":
                            return "耳环卷轴-橙(装备附魔用)";
                        case "20800":
                            return "通用魂石(角色进阶用)";
                        case "20900":
                            return "复苏之泪(复活石废弃)";
                        case "20999":
                            return "猎魔印记";
                        case "21000":
                            return "庇佑之令(免战牌)";
                        case "21001":
                            return "夺宝活动“元素晶石”";
                        case "21002":
                            return "夺宝活动：铁龙钥";
                        case "21003":
                            return "铜龙钥";
                        case "21004":
                            return "银龙钥";
                        case "21005":
                            return "金龙钥";
                        case "21006":
                            return "钻石龙钥";
                        case "21020":
                            return "长夜梦魇-入场券";
                        case "21021":
                            return "长夜梦魇-兑换道具";
                        case "21101":
                            return "啤酒-绿";
                        case "21102":
                            return "啤酒-蓝";
                        case "21103":
                            return "啤酒-紫";
                        case "21104":
                            return "啤酒-橙";
                        case "21201":
                            return "号令之角(建帮令)";
                        case "21300":
                            return "运输令牌";
                        case "22001":
                            return "坐骑培养-通用";
                        case "22002":
                            return "坐骑培养-蓝(废弃)";
                        case "22003":
                            return "坐骑培养-紫(废弃)";
                        case "22101":
                            return "坐骑进阶-通用";
                        case "22201":
                            return "白马";
                        case "22202":
                            return "晶石巨兽";
                        case "22203":
                            return "黑龙";
                        case "22204":
                            return "独角兽--紫";
                        case "22205":
                            return "独角兽--黄";
                        case "22206":
                            return "独角兽--粉";
                        case "22207":
                            return "黑豹";
                        case "22208":
                            return "魔法扫把//资源暂无";
                        case "22209":
                            return "独角兽--蓝";
                        case "22210":
                            return "暂未资源";
                        case "22211":
                            return "暂未资源";
                        case "22212":
                            return "疾风飞艇";
                        case "22213":
                            return "小萌猪";
                        case "22214":
                            return "蒸汽飞行器";
                        case "22215":
                            return "独角兽--红";
                        case "22216":
                            return "暂未资源";
                        case "22217":
                            return "暂未资源";
                        case "22218":
                            return "暂未资源";
                        case "22300":
                            return "激活生活技能所需道具1";
                        case "22310":
                            return "升级生活技能所需道具1";
                        case "22311":
                            return "升级生活技能所需道具2";
                        case "22312":
                            return "升级生活技能所需道具3";
                        case "22313":
                            return "升级生活技能所需道具4";
                        case "22314":
                            return "升级生活技能所需道具5";
                        case "22320":
                            return "突破生活技能所需道具1";
                        case "22321":
                            return "突破生活技能所需道具2";
                        case "22322":
                            return "突破生活技能所需道具3";
                        case "22323":
                            return "突破生活技能所需道具4";
                        case "22324":
                            return "突破生活技能所需道具5";
                        case "22401":
                            return "50级_武器图纸";
                        case "22402":
                            return "60级_武器图纸";
                        case "22403":
                            return "70级_武器图纸";
                        case "22404":
                            return "80级_武器图纸";
                        case "22405":
                            return "90级_武器图纸";
                        case "22406":
                            return "100级_武器图纸";
                        case "22411":
                            return "50级_项链图纸";
                        case "22412":
                            return "60级_项链图纸";
                        case "22413":
                            return "70级_项链图纸";
                        case "22414":
                            return "80级_项链图纸";
                        case "22415":
                            return "90级_项链图纸";
                        case "22416":
                            return "100级_项链图纸";
                        case "22421":
                            return "50级_耳环图纸";
                        case "22422":
                            return "60级_耳环图纸";
                        case "22423":
                            return "70级_耳环图纸";
                        case "22424":
                            return "80级_耳环图纸";
                        case "22425":
                            return "90级_耳环图纸";
                        case "22426":
                            return "100级_耳环图纸";
                        case "22431":
                            return "50级_戒指图纸";
                        case "22432":
                            return "60级_戒指图纸";
                        case "22433":
                            return "70级_戒指图纸";
                        case "22434":
                            return "80级_戒指图纸";
                        case "22435":
                            return "90级_戒指图纸";
                        case "22436":
                            return "100级_戒指图纸";
                        case "22441":
                            return "50级_饰品图纸";
                        case "22442":
                            return "60级_饰品图纸";
                        case "22443":
                            return "70级_饰品图纸";
                        case "22444":
                            return "80级_饰品图纸";
                        case "22445":
                            return "90级_饰品图纸";
                        case "22446":
                            return "100级_饰品图纸";
                        case "22451":
                            return "50级_头盔图纸";
                        case "22452":
                            return "60级_头盔图纸";
                        case "22453":
                            return "70级_头盔图纸";
                        case "22454":
                            return "80级_头盔图纸";
                        case "22455":
                            return "90级_头盔图纸";
                        case "22456":
                            return "100级_头盔图纸";
                        case "22461":
                            return "50级_胸甲图纸";
                        case "22462":
                            return "60级_胸甲图纸";
                        case "22463":
                            return "70级_胸甲图纸";
                        case "22464":
                            return "80级_胸甲图纸";
                        case "22465":
                            return "90级_胸甲图纸";
                        case "22466":
                            return "100级_胸甲图纸";
                        case "22471":
                            return "50级_护手图纸";
                        case "22472":
                            return "60级_护手图纸";
                        case "22473":
                            return "70级_护手图纸";
                        case "22474":
                            return "80级_护手图纸";
                        case "22475":
                            return "90级_护手图纸";
                        case "22476":
                            return "100级_护手图纸";
                        case "22481":
                            return "50级_裤子图纸";
                        case "22482":
                            return "60级_裤子图纸";
                        case "22483":
                            return "70级_裤子图纸";
                        case "22484":
                            return "80级_裤子图纸";
                        case "22485":
                            return "90级_裤子图纸";
                        case "22486":
                            return "100级_裤子图纸";
                        case "22491":
                            return "50级_鞋子图纸";
                        case "22492":
                            return "60级_鞋子图纸";
                        case "22493":
                            return "70级_鞋子图纸";
                        case "22494":
                            return "80级_鞋子图纸";
                        case "22495":
                            return "90级_鞋子图纸";
                        case "22496":
                            return "100级_鞋子图纸";
                        case "23000":
                            return "发型名称(龙战士-发型1)";
                        case "23001":
                            return "发型名称(龙战士-发型2)";
                        case "23002":
                            return "发型名称(龙战士-发型3)";
                        case "23003":
                            return "发型名称(龙战士-发型4)";
                        case "23004":
                            return "发型名称(龙战士-发型5)";
                        case "23050":
                            return "脸型名称(龙战士-脸型1)";
                        case "23051":
                            return "脸型名称(龙战士-脸型2)";
                        case "23052":
                            return "脸型名称(龙战士-脸型3)";
                        case "23053":
                            return "脸型名称(龙战士-脸型4)";
                        case "23054":
                            return "脸型名称(龙战士-脸型5)";
                        case "23100":
                            return "衣服名称(龙战士-衣服1)";
                        case "23101":
                            return "衣服名称(龙战士-衣服2)";
                        case "23102":
                            return "衣服名称(龙战士-衣服3)";
                        case "23103":
                            return "衣服名称(龙战士-衣服4)";
                        case "23104":
                            return "衣服名称(龙战士-衣服5)";
                        case "23150":
                            return "武器名称(龙战士-武器1)";
                        case "23151":
                            return "武器名称(龙战士-武器2)";
                        case "23152":
                            return "武器名称(龙战士-武器3)";
                        case "23153":
                            return "武器名称(龙战士-武器4)";
                        case "23154":
                            return "武器名称(龙战士-武器5)";
                        case "23200":
                            return "发型名称(法师-发型1)";
                        case "23201":
                            return "发型名称(法师-发型2)";
                        case "23202":
                            return "发型名称(法师-发型3)";
                        case "23203":
                            return "发型名称(法师-发型4)";
                        case "23204":
                            return "发型名称(法师-发型5)";
                        case "23250":
                            return "脸型名称(法师-脸型1)";
                        case "23251":
                            return "脸型名称(法师-脸型2)";
                        case "23252":
                            return "脸型名称(法师-脸型3)";
                        case "23253":
                            return "脸型名称(法师-脸型4)";
                        case "23254":
                            return "脸型名称(法师-脸型5)";
                        case "23300":
                            return "衣服名称(法师-衣服1)";
                        case "23301":
                            return "衣服名称(法师-衣服2)";
                        case "23302":
                            return "衣服名称(法师-衣服3)";
                        case "23303":
                            return "衣服名称(法师-衣服4)";
                        case "23304":
                            return "衣服名称(法师-衣服5)";
                        case "23350":
                            return "武器名称(法师-武器1)";
                        case "23351":
                            return "武器名称(法师-武器2)";
                        case "23352":
                            return "武器名称(法师-武器3)";
                        case "23353":
                            return "武器名称(法师-武器4)";
                        case "23354":
                            return "武器名称(法师-武器5)";
                        case "23400":
                            return "发型名称(刺客-发型1)";
                        case "23401":
                            return "发型名称(刺客-发型2)";
                        case "23402":
                            return "发型名称(刺客-发型3)";
                        case "23403":
                            return "发型名称(刺客-发型4)";
                        case "23404":
                            return "发型名称(刺客-发型5)";
                        case "23450":
                            return "脸型名称(刺客-脸型1)";
                        case "23451":
                            return "脸型名称(刺客-脸型2)";
                        case "23452":
                            return "脸型名称(刺客-脸型3)";
                        case "23453":
                            return "脸型名称(刺客-脸型4)";
                        case "23454":
                            return "脸型名称(刺客-脸型5)";
                        case "23500":
                            return "衣服名称(刺客-衣服1)";
                        case "23501":
                            return "衣服名称(刺客-衣服2)";
                        case "23502":
                            return "衣服名称(刺客-衣服3)";
                        case "23503":
                            return "衣服名称(刺客-衣服4)";
                        case "23504":
                            return "衣服名称(刺客-衣服5)";
                        case "23550":
                            return "武器名称(刺客-武器1)";
                        case "23551":
                            return "武器名称(刺客-武器2)";
                        case "23552":
                            return "武器名称(刺客-武器3)";
                        case "23553":
                            return "武器名称(刺客-武器4)";
                        case "23554":
                            return "武器名称(刺客-武器5)";
                        case "23600":
                            return "发型名称(召唤-发型1)";
                        case "23601":
                            return "发型名称(召唤-发型2)";
                        case "23602":
                            return "发型名称(召唤-发型3)";
                        case "23603":
                            return "发型名称(召唤-发型4)";
                        case "23604":
                            return "发型名称(召唤-发型5)";
                        case "23650":
                            return "脸型名称(召唤-脸型1)";
                        case "23651":
                            return "脸型名称(召唤-脸型2)";
                        case "23652":
                            return "脸型名称(召唤-脸型3)";
                        case "23653":
                            return "脸型名称(召唤-脸型4)";
                        case "23654":
                            return "脸型名称(召唤-脸型5)";
                        case "23700":
                            return "衣服名称(召唤-衣服1)";
                        case "23701":
                            return "衣服名称(召唤-衣服2)";
                        case "23702":
                            return "衣服名称(召唤-衣服3)";
                        case "23703":
                            return "衣服名称(召唤-衣服4)";
                        case "23704":
                            return "衣服名称(召唤-衣服5)";
                        case "23750":
                            return "武器名称(召唤-武器1)";
                        case "23751":
                            return "武器名称(召唤-武器2)";
                        case "23752":
                            return "武器名称(召唤-武器3)";
                        case "23753":
                            return "武器名称(召唤-武器4)";
                        case "23754":
                            return "武器名称(召唤-武器5)";
                        case "24000":
                            return "翅膀名称(通用-翅膀1)";
                        case "24001":
                            return "翅膀名称(通用-翅膀2)";
                        case "24002":
                            return "翅膀名称(通用-翅膀3)";
                        case "24003":
                            return "翅膀名称(通用-翅膀4)";
                        case "24004":
                            return "翅膀名称(通用-翅膀5)";
                        case "24005":
                            return "翅膀名称(通用-翅膀6)";
                        case "25000":
                            return "经验药-小";
                        case "25001":
                            return "经验药-中";
                        case "25002":
                            return "经验药-大";
                        case "25003":
                            return "经验药-超级";
                        case "25100":
                            return "小体力药水";
                        case "25101":
                            return "中体力药水";
                        case "25102":
                            return "大体力药水";
                        case "25200":
                            return "小型生命药水(商店)";
                        case "25201":
                            return "中型生命药水(商店)";
                        case "25202":
                            return "大型生命药水(商店)";
                        case "25203":
                            return "稀有生命药水(商店)";
                        case "25204":
                            return "超级生命药水(商店)";
                        case "25205":
                            return "精制生命药水(商店)";
                        case "25206":
                            return "秘制生命药水(商店)";
                        case "25207":
                            return "异界生命药水(商店)";
                        case "25300":
                            return "1级制药产出道具(生活)";
                        case "25301":
                            return "2级制药产出道具(生活)";
                        case "25302":
                            return "3级制药产出道具(生活)";
                        case "25303":
                            return "4级制药产出道具(生活)";
                        case "25304":
                            return "5级制药产出道具(生活)";
                        case "25305":
                            return "6级制药产出道具(生活)";
                        case "25306":
                            return "7级制药产出道具(生活)";
                        case "25307":
                            return "8级制药产出道具(生活)";
                        case "25308":
                            return "9级制药产出道具(生活)";
                        case "25309":
                            return "10级制药产出道具(生活)";
                        case "25400":
                            return "稀有药水(商城)";
                        case "25401":
                            return "超级药水(商城)";
                        case "25500":
                            return "资质药水-绿(宠物)";
                        case "25501":
                            return "资质药水-蓝(宠物)";
                        case "25502":
                            return "资质药水-紫(宠物)";
                        case "25503":
                            return "资质药水-橙(宠物)";
                        case "25600":
                            return "资质刷新药水(宠物)";
                        case "25601":
                            return "强壮";
                        case "25602":
                            return "高级强壮";
                        case "25603":
                            return "石化";
                        case "25604":
                            return "高级石化";
                        case "25605":
                            return "抗魔";
                        case "25606":
                            return "高级抗魔";
                        case "25607":
                            return "蛮力";
                        case "25608":
                            return "高级蛮力";
                        case "25609":
                            return "灵体";
                        case "25610":
                            return "高级灵体";
                        case "25611":
                            return "残暴";
                        case "25612":
                            return "高级残暴";
                        case "25613":
                            return "狡猾";
                        case "25614":
                            return "高级狡猾";
                        case "25615":
                            return "精准";
                        case "25616":
                            return "高级精准";
                        case "25617":
                            return "坚韧";
                        case "25618":
                            return "高级坚韧";
                        case "25619":
                            return "蚀骨";
                        case "25620":
                            return "高级蚀骨";
                        case "25621":
                            return "嗜血";
                        case "25622":
                            return "高级嗜血";
                        case "25623":
                            return "破甲";
                        case "25624":
                            return "高级破甲";
                        case "25625":
                            return "铠化";
                        case "25626":
                            return "高级铠化";
                        case "25627":
                            return "猛袭";
                        case "25628":
                            return "高级猛袭";
                        case "25629":
                            return "贪生";
                        case "25630":
                            return "高级贪生";
                        case "25631":
                            return "眩晕精通";
                        case "25632":
                            return "高级眩晕精通";
                        case "25633":
                            return "眩晕抗性";
                        case "25634":
                            return "高级眩晕抗性";
                        case "25635":
                            return "减速精通";
                        case "25636":
                            return "高级减速精通";
                        case "25637":
                            return "减速抗性";
                        case "25638":
                            return "高级减速抗性";
                        case "25639":
                            return "定身精通";
                        case "25640":
                            return "高级定身精通";
                        case "25641":
                            return "定身抗性";
                        case "25642":
                            return "高级定身抗性";
                        case "25643":
                            return "沉默精通";
                        case "25644":
                            return "高级沉默精通";
                        case "25645":
                            return "沉默抗性";
                        case "25646":
                            return "高级沉默抗性";
                        case "25647":
                            return "诅咒精通";
                        case "25648":
                            return "高级诅咒精通";
                        case "25649":
                            return "诅咒抗性";
                        case "25650":
                            return "高级诅咒抗性";
                        case "25700":
                            return "经验药水-绿(宠物)";
                        case "25701":
                            return "经验药水-蓝(宠物)";
                        case "25702":
                            return "经验药水-紫(宠物)";
                        case "25800":
                            return "捕兽夹-绿(宠物)绑定";
                        case "25801":
                            return "捕兽夹-蓝(宠物)绑定";
                        case "25802":
                            return "捕兽夹-紫(宠物)绑定";
                        case "25805":
                            return "捕兽夹-绿(宠物)非绑定";
                        case "25806":
                            return "捕兽夹-蓝(宠物)非绑定";
                        case "25807":
                            return "捕兽夹-紫(宠物)非绑定";
                        case "25900":
                            return "悟性药(宠物)";
                        case "25901":
                            return "宠物技能锁定瓶";
                        case "26000":
                            return "坚强_玩具_绿";
                        case "26001":
                            return "坚强_玩具_蓝";
                        case "26002":
                            return "坚强_玩具_紫";
                        case "26003":
                            return "坚强_玩具_橙";
                        case "26010":
                            return "勇敢_玩具_绿";
                        case "26011":
                            return "勇敢_玩具_蓝";
                        case "26012":
                            return "勇敢_玩具_紫";
                        case "26013":
                            return "勇敢_玩具_橙";
                        case "26020":
                            return "聪明_玩具_绿";
                        case "26021":
                            return "聪明_玩具_蓝";
                        case "26022":
                            return "聪明_玩具_紫";
                        case "26023":
                            return "聪明_玩具_橙";
                        case "26030":
                            return "忠诚_玩具_绿";
                        case "26031":
                            return "忠诚_玩具_蓝";
                        case "26032":
                            return "忠诚_玩具_紫";
                        case "26033":
                            return "忠诚_玩具_橙";
                        case "26040":
                            return "仁慈_玩具_绿";
                        case "26041":
                            return "仁慈_玩具_蓝";
                        case "26042":
                            return "仁慈_玩具_紫";
                        case "26043":
                            return "仁慈_玩具_橙";
                        case "26310":
                            return "制药道具材料1";
                        case "26311":
                            return "制药道具材料2";
                        case "26312":
                            return "制药道具材料3";
                        case "26313":
                            return "制药道具材料4";
                        case "26314":
                            return "制药道具材料5";
                        case "26330":
                            return "烹饪道具材料1";
                        case "26331":
                            return "烹饪道具材料2";
                        case "26332":
                            return "烹饪道具材料3";
                        case "26333":
                            return "烹饪道具材料4";
                        case "26334":
                            return "烹饪道具材料5";
                        case "26340":
                            return "空附魔卷轴1";
                        case "26350":
                            return "驯兽指南1";
                        case "26400":
                            return "鱼人猎手碎片";
                        case "26401":
                            return "魔甲之灵碎片";
                        case "26402":
                            return "地行龙碎片";
                        case "27000":
                            return "Comic-con限定礼包道具-测试";
                        case "27001":
                            return "CBT Special";
                        case "27002":
                            return "CBT 排行榜";
                        case "27003":
                            return "CBT 调查";
                        case "27004":
                            return "CBT 参加称号";
                        case "27005":
                            return "事前预定";
                        case "27006":
                            return "推广应用-事前预定";
                        case "27007":
                            return "推广应用-登录1";
                        case "27008":
                            return "推广应用-登录2";
                        case "27009":
                            return "推广应用-登录3";
                        case "27010":
                            return "推广应用-Premium";
                        case "27011":
                            return "推广应用-Plus";
                        case "27012":
                            return "推广应用-Unique";
                        case "27013":
                            return "推广应用-VIP";
                        case "27014":
                            return "推广应用 -VVIP";
                        case "27015":
                            return "推广应用-SpecialZone";
                        case "27016":
                            return "事前预定共享奖励";
                        case "27017":
                            return "Onestore 事前预定";
                        case "27018":
                            return "玩家安慰奖励";
                        case "27019":
                            return "玩家安慰奖励";
                        case "27020":
                            return "银币袋100";
                        case "27021":
                            return "银币袋1000";
                        case "27022":
                            return "银币袋1W";
                        case "27023":
                            return "银币袋5W";
                        case "27024":
                            return "银币袋10W";
                        case "27100":
                            return "称号礼包-测试";
                        case "27800":
                            return "预留礼包";
                        case "27801":
                            return "预留礼包";
                        case "27802":
                            return "预留礼包";
                        case "27803":
                            return "预留礼包";
                        case "27804":
                            return "预留礼包";
                        case "27805":
                            return "预留礼包";
                        case "27806":
                            return "预留礼包";
                        case "27807":
                            return "预留礼包";
                        case "27808":
                            return "预留礼包";
                        case "27809":
                            return "预留礼包";
                        case "27810":
                            return "预留礼包";
                        case "27811":
                            return "预留礼包";
                        case "27812":
                            return "预留礼包";
                        case "27813":
                            return "预留礼包";
                        case "27814":
                            return "预留礼包";
                        case "27815":
                            return "预留礼包";
                        case "27816":
                            return "预留礼包";
                        case "27817":
                            return "预留礼包";
                        case "27818":
                            return "预留礼包";
                        case "27819":
                            return "预留礼包";
                        case "27820":
                            return "预留礼包";
                        case "27821":
                            return "预留礼包";
                        case "27822":
                            return "预留礼包";
                        case "27823":
                            return "预留礼包";
                        case "27824":
                            return "预留礼包";
                        case "27825":
                            return "预留礼包";
                        case "27826":
                            return "预留礼包";
                        case "27827":
                            return "预留礼包";
                        case "27828":
                            return "预留礼包";
                        case "27829":
                            return "预留礼包";
                        case "27830":
                            return "预留礼包";
                        case "27831":
                            return "预留礼包";
                        case "27832":
                            return "预留礼包";
                        case "27833":
                            return "预留礼包";
                        case "27834":
                            return "预留礼包";
                        case "27835":
                            return "预留礼包";
                        case "27836":
                            return "预留礼包";
                        case "27837":
                            return "预留礼包";
                        case "27838":
                            return "预留礼包";
                        case "27839":
                            return "预留礼包";
                        case "27840":
                            return "预留礼包";
                        case "27841":
                            return "预留礼包";
                        case "27842":
                            return "预留礼包";
                        case "27843":
                            return "预留礼包";
                        case "27844":
                            return "预留礼包";
                        case "27845":
                            return "预留礼包";
                        case "27846":
                            return "预留礼包";
                        case "27847":
                            return "预留礼包";
                        case "27848":
                            return "预留礼包";
                        case "27849":
                            return "预留礼包";
                        case "27850":
                            return "预留礼包";
                        case "27851":
                            return "预留礼包";
                        case "27852":
                            return "预留礼包";
                        case "27853":
                            return "预留礼包";
                        case "27854":
                            return "预留礼包";
                        case "27855":
                            return "预留礼包";
                        case "27856":
                            return "预留礼包";
                        case "27857":
                            return "预留礼包";
                        case "27858":
                            return "预留礼包";
                        case "27859":
                            return "预留礼包";
                        case "27860":
                            return "预留礼包";
                        case "27861":
                            return "预留礼包";
                        case "27862":
                            return "预留礼包";
                        case "27863":
                            return "预留礼包";
                        case "27864":
                            return "预留礼包";
                        case "27865":
                            return "预留礼包";
                        case "27866":
                            return "预留礼包";
                        case "27867":
                            return "预留礼包";
                        case "27868":
                            return "预留礼包";
                        case "27869":
                            return "预留礼包";
                        case "27870":
                            return "预留礼包";
                        case "27871":
                            return "预留礼包";
                        case "27872":
                            return "预留礼包";
                        case "27873":
                            return "预留礼包";
                        case "27874":
                            return "预留礼包";
                        case "27875":
                            return "预留礼包";
                        case "27876":
                            return "预留礼包";
                        case "27877":
                            return "预留礼包";
                        case "27878":
                            return "预留礼包";
                        case "27879":
                            return "预留礼包";
                        case "27880":
                            return "预留礼包";
                        case "27881":
                            return "预留礼包";
                        case "27882":
                            return "预留礼包";
                        case "27883":
                            return "预留礼包";
                        case "27884":
                            return "预留礼包";
                        case "27885":
                            return "预留礼包";
                        case "27886":
                            return "预留礼包";
                        case "27887":
                            return "预留礼包";
                        case "27888":
                            return "预留礼包";
                        case "27889":
                            return "预留礼包";
                        case "27890":
                            return "预留礼包";
                        case "27891":
                            return "预留礼包";
                        case "27892":
                            return "预留礼包";
                        case "27893":
                            return "预留礼包";
                        case "27894":
                            return "预留礼包";
                        case "27895":
                            return "预留礼包";
                        case "27896":
                            return "预留礼包";
                        case "27897":
                            return "预留礼包";
                        case "27898":
                            return "预留礼包";
                        case "27899":
                            return "预留礼包";
                        case "27900":
                            return "称号-排行榜-第1";
                        case "27901":
                            return "称号-排行榜-第2-10";
                        case "27910":
                            return "称号-排行榜-龙战士第1";
                        case "27911":
                            return "称号-排行榜-魔导师第1";
                        case "27912":
                            return "称号-排行榜-猎魔者第1";
                        case "27913":
                            return "称号-排行榜-召唤师第1";
                        case "27940":
                            return "称号-竞技场-第1";
                        case "27941":
                            return "称号-竞技场-第2-10";
                        case "27942":
                            return "称号-竞技场-第11-50";
                        case "27943":
                            return "称号-竞技场-第51-100";
                        case "27950":
                            return "称号-据点战-阿塔克据点-会长";
                        case "27951":
                            return "称号-据点战-阿塔克据点-副会长";
                        case "27952":
                            return "称号-据点战-阿塔克据点-成员";
                        case "27953":
                            return "称号-据点战-红松镇据点-会长";
                        case "27954":
                            return "称号-据点战-红松镇据点-副会长";
                        case "27955":
                            return "称号-据点战-红松镇据点-成员";
                        case "27956":
                            return "称号-据点战-青云峰据点-会长";
                        case "27957":
                            return "称号-据点战-青云峰据点-副会长";
                        case "27958":
                            return "称号-据点战-青云峰据点-成员";
                        case "27959":
                            return "称号-据点战-巨锤坊据点-会长";
                        case "27960":
                            return "称号-据点战-巨锤坊据点-副会长";
                        case "27961":
                            return "称号-据点战-巨锤坊据点-成员";
                        case "27962":
                            return "称号-据点战-烈焰谷据点-会长";
                        case "27963":
                            return "称号-据点战-烈焰谷据点-副会长";
                        case "27964":
                            return "称号-据点战-烈焰谷据点-成员";
                        case "27965":
                            return "称号-据点战-雪岭堡据点-会长";
                        case "27966":
                            return "称号-据点战-雪岭堡据点-副会长";
                        case "27967":
                            return "称号-据点战-雪岭堡据点-成员";
                        case "27980":
                            return "称号-活动-万圣节";
                        case "30000":
                            return "1级物攻宝石";
                        case "30001":
                            return "2级物攻宝石";
                        case "30002":
                            return "3级物攻宝石";
                        case "30003":
                            return "4级物攻宝石";
                        case "30004":
                            return "5级物攻宝石";
                        case "30005":
                            return "6级物攻宝石";
                        case "30006":
                            return "7级物攻宝石";
                        case "30007":
                            return "8级物攻宝石";
                        case "30008":
                            return "9级物攻宝石";
                        case "30009":
                            return "10级物攻宝石";
                        case "30100":
                            return "1级魔攻宝石";
                        case "30101":
                            return "2级魔攻宝石";
                        case "30102":
                            return "3级魔攻宝石";
                        case "30103":
                            return "4级魔攻宝石";
                        case "30104":
                            return "5级魔攻宝石";
                        case "30105":
                            return "6级魔攻宝石";
                        case "30106":
                            return "7级魔攻宝石";
                        case "30107":
                            return "8级魔攻宝石";
                        case "30108":
                            return "9级魔攻宝石";
                        case "30109":
                            return "10级魔攻宝石";
                        case "30200":
                            return "1级物防宝石";
                        case "30201":
                            return "2级物防宝石";
                        case "30202":
                            return "3级物防宝石";
                        case "30203":
                            return "4级物防宝石";
                        case "30204":
                            return "5级物防宝石";
                        case "30205":
                            return "6级物防宝石";
                        case "30206":
                            return "7级物防宝石";
                        case "30207":
                            return "8级物防宝石";
                        case "30208":
                            return "9级物防宝石";
                        case "30209":
                            return "10级物防宝石";
                        case "30300":
                            return "1级魔防宝石";
                        case "30301":
                            return "2级魔防宝石";
                        case "30302":
                            return "3级魔防宝石";
                        case "30303":
                            return "4级魔防宝石";
                        case "30304":
                            return "5级魔防宝石";
                        case "30305":
                            return "6级魔防宝石";
                        case "30306":
                            return "7级魔防宝石";
                        case "30307":
                            return "8级魔防宝石";
                        case "30308":
                            return "9级魔防宝石";
                        case "30309":
                            return "10级魔防宝石";
                        case "30400":
                            return "1级暴击宝石";
                        case "30401":
                            return "2级暴击宝石";
                        case "30402":
                            return "3级暴击宝石";
                        case "30403":
                            return "4级暴击宝石";
                        case "30404":
                            return "5级暴击宝石";
                        case "30405":
                            return "6级暴击宝石";
                        case "30406":
                            return "7级暴击宝石";
                        case "30407":
                            return "8级暴击宝石";
                        case "30408":
                            return "9级暴击宝石";
                        case "30409":
                            return "10级暴击宝石";
                        case "30500":
                            return "1级闪避宝石";
                        case "30501":
                            return "2级闪避宝石";
                        case "30502":
                            return "3级闪避宝石";
                        case "30503":
                            return "4级闪避宝石";
                        case "30504":
                            return "5级闪避宝石";
                        case "30505":
                            return "6级闪避宝石";
                        case "30506":
                            return "7级闪避宝石";
                        case "30507":
                            return "8级闪避宝石";
                        case "30508":
                            return "9级闪避宝石";
                        case "30509":
                            return "10级闪避宝石";
                        case "30600":
                            return "1级命中宝石";
                        case "30601":
                            return "2级命中宝石";
                        case "30602":
                            return "3级命中宝石";
                        case "30603":
                            return "4级命中宝石";
                        case "30604":
                            return "5级命中宝石";
                        case "30605":
                            return "6级命中宝石";
                        case "30606":
                            return "7级命中宝石";
                        case "30607":
                            return "8级命中宝石";
                        case "30608":
                            return "9级命中宝石";
                        case "30609":
                            return "10级命中宝石";
                        case "30700":
                            return "1级韧性宝石";
                        case "30701":
                            return "2级韧性宝石";
                        case "30702":
                            return "3级韧性宝石";
                        case "30703":
                            return "4级韧性宝石";
                        case "30704":
                            return "5级韧性宝石";
                        case "30705":
                            return "6级韧性宝石";
                        case "30706":
                            return "7级韧性宝石";
                        case "30707":
                            return "8级韧性宝石";
                        case "30708":
                            return "9级韧性宝石";
                        case "30709":
                            return "10级韧性宝石";
                        case "30800":
                            return "1级生命宝石";
                        case "30801":
                            return "2级生命宝石";
                        case "30802":
                            return "3级生命宝石";
                        case "30803":
                            return "4级生命宝石";
                        case "30804":
                            return "5级生命宝石";
                        case "30805":
                            return "6级生命宝石";
                        case "30806":
                            return "7级生命宝石";
                        case "30807":
                            return "8级生命宝石";
                        case "30808":
                            return "9级生命宝石";
                        case "30809":
                            return "10级生命宝石";
                        case "31000":
                            return "超级无敌大礼包(GM)";
                        case "31001":
                            return "强化(GM礼包)";
                        case "31002":
                            return "精炼(GM礼包)";
                        case "31003":
                            return "武器附魔(GM礼包)";
                        case "31004":
                            return "项链附魔(GM礼包)";
                        case "31005":
                            return "戒指附魔(GM礼包)";
                        case "31006":
                            return "饰品附魔(GM礼包)";
                        case "31007":
                            return "头盔附魔(GM礼包)";
                        case "31008":
                            return "胸甲附魔(GM礼包)";
                        case "31009":
                            return "护手附魔(GM礼包)";
                        case "31010":
                            return "鞋子附魔(GM礼包)";
                        case "31011":
                            return "裤子附魔(GM礼包)";
                        case "31012":
                            return "耳环附魔(GM礼包)";
                        case "31013":
                            return "1级宝石(GM礼包)";
                        case "31014":
                            return "2级宝石(GM礼包)";
                        case "31015":
                            return "3级宝石(GM礼包)";
                        case "31016":
                            return "4级宝石(GM礼包)";
                        case "31017":
                            return "5级宝石(GM礼包)";
                        case "31018":
                            return "6级宝石(GM礼包)";
                        case "31019":
                            return "7级宝石(GM礼包)";
                        case "31020":
                            return "8级宝石(GM礼包)";
                        case "31021":
                            return "9级宝石(GM礼包)";
                        case "31022":
                            return "10级宝石(GM礼包)";
                        case "31023":
                            return "洗练(GM礼包)";
                        case "31024":
                            return "进阶(GM礼包)";
                        case "31025":
                            return "技能(GM礼包)";
                        case "31026":
                            return "时装兑换(GM礼包)";
                        case "31027":
                            return "坐骑培养(GM礼包)";
                        case "31028":
                            return "坐骑碎片(GM礼包)";
                        case "31029":
                            return "商店药水(GM礼包)";
                        case "31030":
                            return "经验药(GM礼包)";
                        case "31031":
                            return "饮酒(GM礼包)";
                        case "31032":
                            return "体力药(GM礼包)";
                        case "31033":
                            return "制造药水(GM礼包)";
                        case "31034":
                            return "烹饪食物(GM礼包)";
                        case "31035":
                            return "生活技能提升(GM礼包)";
                        case "31036":
                            return "生活技能制药材料(GM礼包)";
                        case "31037":
                            return "生活技能烹饪材料(GM礼包)";
                        case "31038":
                            return "生活技能附魔材料(GM礼包)";
                        case "31039":
                            return "生活技能驯兽材料(GM礼包)";
                        case "31040":
                            return "武器打造(GM礼包)";
                        case "31041":
                            return "防具打造(GM礼包)";
                        case "31042":
                            return "饰品打造(GM礼包)";
                        case "31043":
                            return "武器图纸(GM礼包)";
                        case "31044":
                            return "项链图纸(GM礼包)";
                        case "31045":
                            return "耳环图纸(GM礼包)";
                        case "31046":
                            return "戒指图纸(GM礼包)";
                        case "31047":
                            return "饰品图纸(GM礼包)";
                        case "31048":
                            return "头盔图纸(GM礼包)";
                        case "31049":
                            return "胸甲图纸(GM礼包)";
                        case "31050":
                            return "护手图纸(GM礼包)";
                        case "31051":
                            return "裤子图纸(GM礼包)";
                        case "31052":
                            return "鞋子图纸(GM礼包)";
                        case "31053":
                            return "宠物资质(GM礼包)";
                        case "31054":
                            return "宠物经验(GM礼包)";
                        case "31055":
                            return "宠物捕捉(GM礼包)";
                        case "31056":
                            return "宠物悟性(GM礼包)";
                        case "31057":
                            return "宠物玩具-坚强(GM礼包)";
                        case "31058":
                            return "宠物玩具-勇敢(GM礼包)";
                        case "31059":
                            return "宠物玩具-聪明(GM礼包)";
                        case "31060":
                            return "宠物玩具-忠诚(GM礼包)";
                        case "31061":
                            return "宠物玩具-仁慈(GM礼包)";
                        case "31062":
                            return "宠物技能(GM礼包)";
                        case "31101":
                            return "10级礼包";
                        case "31102":
                            return "20级礼包";
                        case "31103":
                            return "30级礼包";
                        case "31104":
                            return "40级礼包";
                        case "31105":
                            return "50级礼包";
                        case "31106":
                            return "60级礼包";
                        case "31107":
                            return "70级礼包";
                        case "31108":
                            return "80级礼包";
                        case "31109":
                            return "90级礼包";
                        case "31110":
                            return "100级礼包";
                        case "31121":
                            return "1级宝石袋(分解)";
                        case "31122":
                            return "2级宝石袋(分解)";
                        case "31123":
                            return "3级宝石袋(分解)";
                        case "31124":
                            return "4级宝石袋(分解)";
                        case "31125":
                            return "5级宝石袋(分解)";
                        case "31140":
                            return "升星宝盒-神秘(商城)";
                        case "31141":
                            return "升星宝盒-白(商城)";
                        case "31142":
                            return "升星宝盒-绿(商城)";
                        case "31143":
                            return "升星宝盒-蓝(商城)";
                        case "31144":
                            return "升星宝盒-紫(商城)";
                        case "31145":
                            return "升星宝盒-橙(商城)";
                        case "31150":
                            return "附魔宝盒-蓝(商城)";
                        case "31151":
                            return "附魔宝盒-紫(商城)";
                        case "31152":
                            return "附魔宝盒-橙(商城)";
                        case "31161":
                            return "强化礼包-绿(商城)";
                        case "31162":
                            return "强化礼包-蓝(商城)";
                        case "31163":
                            return "强化礼包-紫(商城)";
                        case "31164":
                            return "强化礼包-橙(商城)";
                        case "31170":
                            return "洗练礼包(商城)";
                        case "31181":
                            return "打造袋子-白(分解)";
                        case "31182":
                            return "打造袋子-绿(分解)";
                        case "31183":
                            return "打造袋子-蓝(分解)";
                        case "31184":
                            return "打造袋子-紫(分解)";
                        case "31185":
                            return "打造袋子-橙(分解)";
                        case "31186":
                            return "打造袋子-红(分解)";
                        case "31191":
                            return "宝石宝箱-1(商城)";
                        case "31192":
                            return "宝石宝箱-2(商城)";
                        case "31193":
                            return "宝石宝箱-3(商城)";
                        case "31194":
                            return "宝石宝箱-4(商城)";
                        case "31195":
                            return "宝石宝箱-5(商城)";
                        case "31196":
                            return "宝石宝箱-6(商城)";
                        case "31197":
                            return "宝石宝箱-7(商城)";
                        case "31198":
                            return "宝石宝箱-8(商城)";
                        case "31199":
                            return "宝石宝箱-9(商城)";
                        case "31200":
                            return "宝石宝箱-10(商城)";
                        case "31301":
                            return "藏宝图";
                        case "31302":
                            return "藏宝图_蓝（废弃）";
                        case "31303":
                            return "藏宝图_紫（废弃）";
                        case "31304":
                            return "藏宝图_橙（废弃）";
                        case "31401":
                            return "帝国悬赏奖励宝箱Lv.1";
                        case "31402":
                            return "帝国悬赏奖励宝箱Lv.2";
                        case "31403":
                            return "帝国悬赏奖励宝箱Lv.3";
                        case "31404":
                            return "帝国悬赏奖励宝箱Lv.4";
                        case "31405":
                            return "帝国悬赏奖励宝箱Lv.5";
                        case "31406":
                            return "帝国悬赏奖励宝箱Lv.6";
                        case "31411":
                            return "友好度礼包-1";
                        case "31412":
                            return "友好度礼包-2";
                        case "31413":
                            return "友好度礼包-3";
                        case "31414":
                            return "友好度礼包-4";
                        case "31415":
                            return "友好度礼包-5";
                        case "31416":
                            return "友好度礼包-6";
                        case "31417":
                            return "友好度礼包-7";
                        case "31418":
                            return "友好度礼包-8";
                        case "31419":
                            return "友好度礼包-9";
                        case "31420":
                            return "友好度礼包-10";
                        case "31430":
                            return "1级烹饪产出道具(生活)";
                        case "31431":
                            return "2级烹饪产出道具(生活)";
                        case "31432":
                            return "3级烹饪产出道具(生活)";
                        case "31433":
                            return "4级烹饪产出道具(生活)";
                        case "31434":
                            return "5级烹饪产出道具(生活)";
                        case "31435":
                            return "6级烹饪产出道具(生活)";
                        case "31436":
                            return "7级烹饪产出道具(生活)";
                        case "31437":
                            return "8级烹饪产出道具(生活)";
                        case "31438":
                            return "9级烹饪产出道具(生活)";
                        case "31439":
                            return "10级烹饪产出道具(生活)";
                        case "31450":
                            return "游戏宝箱_魔法书_2";
                        case "31451":
                            return "游戏宝箱_魔法书_5";
                        case "31452":
                            return "游戏宝箱_魔法书_8";
                        case "31453":
                            return "游戏宝箱_魔法书_10";
                        case "31454":
                            return "游戏宝箱_魔法书_15";
                        case "31455":
                            return "游戏宝箱_魔法书_20";
                        case "31456":
                            return "游戏宝箱_魔法书_30";
                        case "31457":
                            return "游戏宝箱_魔法书_50";
                        case "31458":
                            return "游戏宝箱_魔法书_80";
                        case "31459":
                            return "游戏宝箱_魔法书_99";
                        case "31460":
                            return "30-39阵营任务宝箱";
                        case "31461":
                            return "40-49阵营任务宝箱";
                        case "31462":
                            return "50-59阵营任务宝箱";
                        case "31463":
                            return "60-69阵营任务宝箱";
                        case "31464":
                            return "70-79阵营任务宝箱";
                        case "31465":
                            return "80-89阵营任务宝箱";
                        case "31470":
                            return "20-29公会任务宝箱";
                        case "31471":
                            return "30-39公会任务宝箱";
                        case "31472":
                            return "40-49公会任务宝箱";
                        case "31473":
                            return "50-59公会任务宝箱";
                        case "31474":
                            return "60-69公会任务宝箱";
                        case "31475":
                            return "70-80公会任务宝箱";
                        case "31480":
                            return "30-39宠物岛任务宝箱";
                        case "31481":
                            return "40-49宠物岛任务宝箱";
                        case "31482":
                            return "50-59宠物岛任务宝箱";
                        case "31483":
                            return "60-69宠物岛任务宝箱";
                        case "31484":
                            return "70-80宠物岛任务宝箱";
                        case "31488":
                            return "低级宠物技能宝箱";
                        case "31489":
                            return "高级宠物技能宝箱";
                        case "31501":
                            return "1级-白-龙战士(GM礼包)";
                        case "31502":
                            return "1级-绿-龙战士(GM礼包)";
                        case "31503":
                            return "1级-蓝-龙战士(GM礼包)";
                        case "31504":
                            return "1级-白-龙战士(GM礼包)";
                        case "31505":
                            return "10级-白-龙战士(GM礼包)";
                        case "31506":
                            return "10级-绿-龙战士(GM礼包)";
                        case "31507":
                            return "10级-蓝-龙战士(GM礼包)";
                        case "31508":
                            return "10级-紫-龙战士(GM礼包)";
                        case "31509":
                            return "30级-白-龙战士(GM礼包)";
                        case "31510":
                            return "30级-绿-龙战士(GM礼包)";
                        case "31511":
                            return "30级-蓝-龙战士(GM礼包)";
                        case "31512":
                            return "30级-紫-龙战士(GM礼包)";
                        case "31513":
                            return "30级-橙-龙战士(GM礼包)";
                        case "31514":
                            return "40级-白-龙战士(GM礼包)";
                        case "31515":
                            return "40级-绿-龙战士(GM礼包)";
                        case "31516":
                            return "40级-蓝-龙战士(GM礼包)";
                        case "31517":
                            return "40级-紫-龙战士(GM礼包)";
                        case "31518":
                            return "40级-橙-龙战士(GM礼包)";
                        case "31519":
                            return "50级-白-龙战士(GM礼包)";
                        case "31520":
                            return "50级-绿-龙战士(GM礼包)";
                        case "31521":
                            return "50级-蓝-龙战士(GM礼包)";
                        case "31522":
                            return "50级-紫-龙战士(GM礼包)";
                        case "31523":
                            return "50级-橙-龙战士(GM礼包)";
                        case "31524":
                            return "50级-紫套装-龙战士(GM礼包)";
                        case "31525":
                            return "50级-橙套装-龙战士(GM礼包)";
                        case "31526":
                            return "60级-白-龙战士(GM礼包)";
                        case "31527":
                            return "60级-绿-龙战士(GM礼包)";
                        case "31528":
                            return "60级-蓝-龙战士(GM礼包)";
                        case "31529":
                            return "60级-紫-龙战士(GM礼包)";
                        case "31530":
                            return "60级-橙-龙战士(GM礼包)";
                        case "31531":
                            return "60级-紫套装-龙战士(GM礼包)";
                        case "31532":
                            return "60级-橙套装-龙战士(GM礼包)";
                        case "31533":
                            return "70级-白-龙战士(GM礼包)";
                        case "31534":
                            return "70级-绿-龙战士(GM礼包)";
                        case "31535":
                            return "70级-蓝-龙战士(GM礼包)";
                        case "31536":
                            return "70级-紫-龙战士(GM礼包)";
                        case "31537":
                            return "70级-橙-龙战士(GM礼包)";
                        case "31538":
                            return "70级-紫套装-龙战士(GM礼包)";
                        case "31539":
                            return "70级-橙套装-龙战士(GM礼包)";
                        case "31540":
                            return "80级-白-龙战士(GM礼包)";
                        case "31541":
                            return "80级-绿-龙战士(GM礼包)";
                        case "31542":
                            return "80级-蓝-龙战士(GM礼包)";
                        case "31543":
                            return "80级-紫-龙战士(GM礼包)";
                        case "31544":
                            return "80级-橙-龙战士(GM礼包)";
                        case "31545":
                            return "80级-紫套装-龙战士(GM礼包)";
                        case "31546":
                            return "80级-橙套装-龙战士(GM礼包)";
                        case "31547":
                            return "90级-白-龙战士(GM礼包)";
                        case "31548":
                            return "90级-绿-龙战士(GM礼包)";
                        case "31549":
                            return "90级-蓝-龙战士(GM礼包)";
                        case "31550":
                            return "90级-紫-龙战士(GM礼包)";
                        case "31551":
                            return "90级-橙-龙战士(GM礼包)";
                        case "31552":
                            return "90级-紫套装-龙战士(GM礼包)";
                        case "31553":
                            return "90级-橙套装-龙战士(GM礼包)";
                        case "31554":
                            return "100级-白-龙战士(GM礼包)";
                        case "31555":
                            return "100级-绿-龙战士(GM礼包)";
                        case "31556":
                            return "100级-蓝-龙战士(GM礼包)";
                        case "31557":
                            return "100级-紫-龙战士(GM礼包)";
                        case "31558":
                            return "100级-橙-龙战士(GM礼包)";
                        case "31559":
                            return "100级-紫套装-龙战士(GM礼包)";
                        case "31560":
                            return "100级-橙套装-龙战士(GM礼包)";
                        case "31561":
                            return "1级-白-魔法师(GM礼包)";
                        case "31562":
                            return "1级-绿-魔法师(GM礼包)";
                        case "31563":
                            return "1级-蓝-魔法师(GM礼包)";
                        case "31564":
                            return "1级-白-魔法师(GM礼包)";
                        case "31565":
                            return "10级-白-魔法师(GM礼包)";
                        case "31566":
                            return "10级-绿-魔法师(GM礼包)";
                        case "31567":
                            return "10级-蓝-魔法师(GM礼包)";
                        case "31568":
                            return "10级-紫-魔法师(GM礼包)";
                        case "31569":
                            return "30级-白-魔法师(GM礼包)";
                        case "31570":
                            return "30级-绿-魔法师(GM礼包)";
                        case "31571":
                            return "30级-蓝-魔法师(GM礼包)";
                        case "31572":
                            return "30级-紫-魔法师(GM礼包)";
                        case "31573":
                            return "30级-橙-魔法师(GM礼包)";
                        case "31574":
                            return "40级-白-魔法师(GM礼包)";
                        case "31575":
                            return "40级-绿-魔法师(GM礼包)";
                        case "31576":
                            return "40级-蓝-魔法师(GM礼包)";
                        case "31577":
                            return "40级-紫-魔法师(GM礼包)";
                        case "31578":
                            return "40级-橙-魔法师(GM礼包)";
                        case "31579":
                            return "50级-白-魔法师(GM礼包)";
                        case "31580":
                            return "50级-绿-魔法师(GM礼包)";
                        case "31581":
                            return "50级-蓝-魔法师(GM礼包)";
                        case "31582":
                            return "50级-紫-魔法师(GM礼包)";
                        case "31583":
                            return "50级-橙-魔法师(GM礼包)";
                        case "31584":
                            return "50级-紫套装-魔法师(GM礼包)";
                        case "31585":
                            return "50级-橙套装-魔法师(GM礼包)";
                        case "31586":
                            return "60级-白-魔法师(GM礼包)";
                        case "31587":
                            return "60级-绿-魔法师(GM礼包)";
                        case "31588":
                            return "60级-蓝-魔法师(GM礼包)";
                        case "31589":
                            return "60级-紫-魔法师(GM礼包)";
                        case "31590":
                            return "60级-橙-魔法师(GM礼包)";
                        case "31591":
                            return "60级-紫套装-魔法师(GM礼包)";
                        case "31592":
                            return "60级-橙套装-魔法师(GM礼包)";
                        case "31593":
                            return "70级-白-魔法师(GM礼包)";
                        case "31594":
                            return "70级-绿-魔法师(GM礼包)";
                        case "31595":
                            return "70级-蓝-魔法师(GM礼包)";
                        case "31596":
                            return "70级-紫-魔法师(GM礼包)";
                        case "31597":
                            return "70级-橙-魔法师(GM礼包)";
                        case "31598":
                            return "70级-紫套装-魔法师(GM礼包)";
                        case "31599":
                            return "70级-橙套装-魔法师(GM礼包)";
                        case "31600":
                            return "80级-白-魔法师(GM礼包)";
                        case "31601":
                            return "80级-绿-魔法师(GM礼包)";
                        case "31602":
                            return "80级-蓝-魔法师(GM礼包)";
                        case "31603":
                            return "80级-紫-魔法师(GM礼包)";
                        case "31604":
                            return "80级-橙-魔法师(GM礼包)";
                        case "31605":
                            return "80级-紫套装-魔法师(GM礼包)";
                        case "31606":
                            return "80级-橙套装-魔法师(GM礼包)";
                        case "31607":
                            return "90级-白-魔法师(GM礼包)";
                        case "31608":
                            return "90级-绿-魔法师(GM礼包)";
                        case "31609":
                            return "90级-蓝-魔法师(GM礼包)";
                        case "31610":
                            return "90级-紫-魔法师(GM礼包)";
                        case "31611":
                            return "90级-橙-魔法师(GM礼包)";
                        case "31612":
                            return "90级-紫套装-魔法师(GM礼包)";
                        case "31613":
                            return "90级-橙套装-魔法师(GM礼包)";
                        case "31614":
                            return "100级-白-魔法师(GM礼包)";
                        case "31615":
                            return "100级-绿-魔法师(GM礼包)";
                        case "31616":
                            return "100级-蓝-魔法师(GM礼包)";
                        case "31617":
                            return "100级-紫-魔法师(GM礼包)";
                        case "31618":
                            return "100级-橙-魔法师(GM礼包)";
                        case "31619":
                            return "100级-紫套装-魔法师(GM礼包)";
                        case "31620":
                            return "100级-橙套装-魔法师(GM礼包)";
                        case "31621":
                            return "1级-白-猎魔者(GM礼包)";
                        case "31622":
                            return "1级-绿-猎魔者(GM礼包)";
                        case "31623":
                            return "1级-蓝-猎魔者(GM礼包)";
                        case "31624":
                            return "1级-白-猎魔者(GM礼包)";
                        case "31625":
                            return "10级-白-猎魔者(GM礼包)";
                        case "31626":
                            return "10级-绿-猎魔者(GM礼包)";
                        case "31627":
                            return "10级-蓝-猎魔者(GM礼包)";
                        case "31628":
                            return "10级-紫-猎魔者(GM礼包)";
                        case "31629":
                            return "30级-白-猎魔者(GM礼包)";
                        case "31630":
                            return "30级-绿-猎魔者(GM礼包)";
                        case "31631":
                            return "30级-蓝-猎魔者(GM礼包)";
                        case "31632":
                            return "30级-紫-猎魔者(GM礼包)";
                        case "31633":
                            return "30级-橙-猎魔者(GM礼包)";
                        case "31634":
                            return "40级-白-猎魔者(GM礼包)";
                        case "31635":
                            return "40级-绿-猎魔者(GM礼包)";
                        case "31636":
                            return "40级-蓝-猎魔者(GM礼包)";
                        case "31637":
                            return "40级-紫-猎魔者(GM礼包)";
                        case "31638":
                            return "40级-橙-猎魔者(GM礼包)";
                        case "31639":
                            return "50级-白-猎魔者(GM礼包)";
                        case "31640":
                            return "50级-绿-猎魔者(GM礼包)";
                        case "31641":
                            return "50级-蓝-猎魔者(GM礼包)";
                        case "31642":
                            return "50级-紫-猎魔者(GM礼包)";
                        case "31643":
                            return "50级-橙-猎魔者(GM礼包)";
                        case "31644":
                            return "50级-紫套装-猎魔者(GM礼包)";
                        case "31645":
                            return "50级-橙套装-猎魔者(GM礼包)";
                        case "31646":
                            return "60级-白-猎魔者(GM礼包)";
                        case "31647":
                            return "60级-绿-猎魔者(GM礼包)";
                        case "31648":
                            return "60级-蓝-猎魔者(GM礼包)";
                        case "31649":
                            return "60级-紫-猎魔者(GM礼包)";
                        case "31650":
                            return "60级-橙-猎魔者(GM礼包)";
                        case "31651":
                            return "60级-紫套装-猎魔者(GM礼包)";
                        case "31652":
                            return "60级-橙套装-猎魔者(GM礼包)";
                        case "31653":
                            return "70级-白-猎魔者(GM礼包)";
                        case "31654":
                            return "70级-绿-猎魔者(GM礼包)";
                        case "31655":
                            return "70级-蓝-猎魔者(GM礼包)";
                        case "31656":
                            return "70级-紫-猎魔者(GM礼包)";
                        case "31657":
                            return "70级-橙-猎魔者(GM礼包)";
                        case "31658":
                            return "70级-紫套装-猎魔者(GM礼包)";
                        case "31659":
                            return "70级-橙套装-猎魔者(GM礼包)";
                        case "31660":
                            return "80级-白-猎魔者(GM礼包)";
                        case "31661":
                            return "80级-绿-猎魔者(GM礼包)";
                        case "31662":
                            return "80级-蓝-猎魔者(GM礼包)";
                        case "31663":
                            return "80级-紫-猎魔者(GM礼包)";
                        case "31664":
                            return "80级-橙-猎魔者(GM礼包)";
                        case "31665":
                            return "80级-紫套装-猎魔者(GM礼包)";
                        case "31666":
                            return "80级-橙套装-猎魔者(GM礼包)";
                        case "31667":
                            return "90级-白-猎魔者(GM礼包)";
                        case "31668":
                            return "90级-绿-猎魔者(GM礼包)";
                        case "31669":
                            return "90级-蓝-猎魔者(GM礼包)";
                        case "31670":
                            return "90级-紫-猎魔者(GM礼包)";
                        case "31671":
                            return "90级-橙-猎魔者(GM礼包)";
                        case "31672":
                            return "90级-紫套装-猎魔者(GM礼包)";
                        case "31673":
                            return "90级-橙套装-猎魔者(GM礼包)";
                        case "31674":
                            return "100级-白-猎魔者(GM礼包)";
                        case "31675":
                            return "100级-绿-猎魔者(GM礼包)";
                        case "31676":
                            return "100级-蓝-猎魔者(GM礼包)";
                        case "31677":
                            return "100级-紫-猎魔者(GM礼包)";
                        case "31678":
                            return "100级-橙-猎魔者(GM礼包)";
                        case "31679":
                            return "100级-紫套装-猎魔者(GM礼包)";
                        case "31680":
                            return "100级-橙套装-猎魔者(GM礼包)";
                        case "31681":
                            return "1级-白-召唤师(GM礼包)";
                        case "31682":
                            return "1级-绿-召唤师(GM礼包)";
                        case "31683":
                            return "1级-蓝-召唤师(GM礼包)";
                        case "31684":
                            return "1级-白-召唤师(GM礼包)";
                        case "31685":
                            return "10级-白-召唤师(GM礼包)";
                        case "31686":
                            return "10级-绿-召唤师(GM礼包)";
                        case "31687":
                            return "10级-蓝-召唤师(GM礼包)";
                        case "31688":
                            return "10级-紫-召唤师(GM礼包)";
                        case "31689":
                            return "30级-白-召唤师(GM礼包)";
                        case "31690":
                            return "30级-绿-召唤师(GM礼包)";
                        case "31691":
                            return "30级-蓝-召唤师(GM礼包)";
                        case "31692":
                            return "30级-紫-召唤师(GM礼包)";
                        case "31693":
                            return "30级-橙-召唤师(GM礼包)";
                        case "31694":
                            return "40级-白-召唤师(GM礼包)";
                        case "31695":
                            return "40级-绿-召唤师(GM礼包)";
                        case "31696":
                            return "40级-蓝-召唤师(GM礼包)";
                        case "31697":
                            return "40级-紫-召唤师(GM礼包)";
                        case "31698":
                            return "40级-橙-召唤师(GM礼包)";
                        case "31699":
                            return "50级-白-召唤师(GM礼包)";
                        case "31700":
                            return "50级-绿-召唤师(GM礼包)";
                        case "31701":
                            return "50级-蓝-召唤师(GM礼包)";
                        case "31702":
                            return "50级-紫-召唤师(GM礼包)";
                        case "31703":
                            return "50级-橙-召唤师(GM礼包)";
                        case "31704":
                            return "50级-紫套装-召唤师(GM礼包)";
                        case "31705":
                            return "50级-橙套装-召唤师(GM礼包)";
                        case "31706":
                            return "60级-白-召唤师(GM礼包)";
                        case "31707":
                            return "60级-绿-召唤师(GM礼包)";
                        case "31708":
                            return "60级-蓝-召唤师(GM礼包)";
                        case "31709":
                            return "60级-紫-召唤师(GM礼包)";
                        case "31710":
                            return "60级-橙-召唤师(GM礼包)";
                        case "31711":
                            return "60级-紫套装-召唤师(GM礼包)";
                        case "31712":
                            return "60级-橙套装-召唤师(GM礼包)";
                        case "31713":
                            return "70级-白-召唤师(GM礼包)";
                        case "31714":
                            return "70级-绿-召唤师(GM礼包)";
                        case "31715":
                            return "70级-蓝-召唤师(GM礼包)";
                        case "31716":
                            return "70级-紫-召唤师(GM礼包)";
                        case "31717":
                            return "70级-橙-召唤师(GM礼包)";
                        case "31718":
                            return "70级-紫套装-召唤师(GM礼包)";
                        case "31719":
                            return "70级-橙套装-召唤师(GM礼包)";
                        case "31720":
                            return "80级-白-召唤师(GM礼包)";
                        case "31721":
                            return "80级-绿-召唤师(GM礼包)";
                        case "31722":
                            return "80级-蓝-召唤师(GM礼包)";
                        case "31723":
                            return "80级-紫-召唤师(GM礼包)";
                        case "31724":
                            return "80级-橙-召唤师(GM礼包)";
                        case "31725":
                            return "80级-紫套装-召唤师(GM礼包)";
                        case "31726":
                            return "80级-橙套装-召唤师(GM礼包)";
                        case "31727":
                            return "90级-白-召唤师(GM礼包)";
                        case "31728":
                            return "90级-绿-召唤师(GM礼包)";
                        case "31729":
                            return "90级-蓝-召唤师(GM礼包)";
                        case "31730":
                            return "90级-紫-召唤师(GM礼包)";
                        case "31731":
                            return "90级-橙-召唤师(GM礼包)";
                        case "31732":
                            return "90级-紫套装-召唤师(GM礼包)";
                        case "31733":
                            return "90级-橙套装-召唤师(GM礼包)";
                        case "31734":
                            return "100级-白-召唤师(GM礼包)";
                        case "31735":
                            return "100级-绿-召唤师(GM礼包)";
                        case "31736":
                            return "100级-蓝-召唤师(GM礼包)";
                        case "31737":
                            return "100级-紫-召唤师(GM礼包)";
                        case "31738":
                            return "100级-橙-召唤师(GM礼包)";
                        case "31739":
                            return "100级-紫套装-召唤师(GM礼包)";
                        case "31740":
                            return "100级-橙套装-召唤师(GM礼包)";
                        case "31741":
                            return "1级-白-通用(GM礼包)";
                        case "31742":
                            return "1级-绿-通用(GM礼包)";
                        case "31743":
                            return "1级-蓝-通用(GM礼包)";
                        case "31744":
                            return "1级-白-通用(GM礼包)";
                        case "31745":
                            return "10级-白-通用(GM礼包)";
                        case "31746":
                            return "10级-绿-通用(GM礼包)";
                        case "31747":
                            return "10级-蓝-通用(GM礼包)";
                        case "31748":
                            return "10级-紫-通用(GM礼包)";
                        case "31749":
                            return "30级-白-通用(GM礼包)";
                        case "31750":
                            return "30级-绿-通用(GM礼包)";
                        case "31751":
                            return "30级-蓝-通用(GM礼包)";
                        case "31752":
                            return "30级-紫-通用(GM礼包)";
                        case "31753":
                            return "30级-橙-通用(GM礼包)";
                        case "31754":
                            return "40级-白-通用(GM礼包)";
                        case "31755":
                            return "40级-绿-通用(GM礼包)";
                        case "31756":
                            return "40级-蓝-通用(GM礼包)";
                        case "31757":
                            return "40级-紫-通用(GM礼包)";
                        case "31758":
                            return "40级-橙-通用(GM礼包)";
                        case "31759":
                            return "50级-白-通用(GM礼包)";
                        case "31760":
                            return "50级-绿-通用(GM礼包)";
                        case "31761":
                            return "50级-蓝-通用(GM礼包)";
                        case "31762":
                            return "50级-紫-通用(GM礼包)";
                        case "31763":
                            return "50级-橙-通用(GM礼包)";
                        case "31764":
                            return "50级-紫物攻套装-通用(GM礼包)";
                        case "31765":
                            return "50级-橙物攻套装--通用(GM礼包)";
                        case "31766":
                            return "50级-紫魔攻套装-通用(GM礼包)";
                        case "31767":
                            return "50级-橙魔攻套装-通用(GM礼包)";
                        case "31768":
                            return "60级-白-通用(GM礼包)";
                        case "31769":
                            return "60级-绿-通用(GM礼包)";
                        case "31770":
                            return "60级-蓝-通用(GM礼包)";
                        case "31771":
                            return "60级-紫-通用(GM礼包)";
                        case "31772":
                            return "60级-橙-通用(GM礼包)";
                        case "31773":
                            return "60级-紫物攻套装-通用(GM礼包)";
                        case "31774":
                            return "60级-橙物攻套装-通用(GM礼包)";
                        case "31775":
                            return "60级-紫魔攻套装-通用(GM礼包)";
                        case "31776":
                            return "60级-橙魔攻套装-通用(GM礼包)";
                        case "31777":
                            return "70级-白-通用(GM礼包)";
                        case "31778":
                            return "70级-绿-通用(GM礼包)";
                        case "31779":
                            return "70级-蓝-通用(GM礼包)";
                        case "31780":
                            return "70级-紫-通用(GM礼包)";
                        case "31781":
                            return "70级-橙-通用(GM礼包)";
                        case "31782":
                            return "70级-紫物攻套装-通用(GM礼包)";
                        case "31783":
                            return "70级-橙物攻套装-通用(GM礼包)";
                        case "31784":
                            return "70级-紫魔攻套装-通用(GM礼包)";
                        case "31785":
                            return "70级-橙魔攻套装-通用(GM礼包)";
                        case "31786":
                            return "80级-白-通用(GM礼包)";
                        case "31787":
                            return "80级-绿-通用(GM礼包)";
                        case "31788":
                            return "80级-蓝-通用(GM礼包)";
                        case "31789":
                            return "80级-紫-通用(GM礼包)";
                        case "31790":
                            return "80级-橙-通用(GM礼包)";
                        case "31791":
                            return "80级-紫物攻套装-通用(GM礼包)";
                        case "31792":
                            return "80级-橙物攻套装-通用(GM礼包)";
                        case "31793":
                            return "80级-紫魔攻套装-通用(GM礼包)";
                        case "31794":
                            return "80级-橙魔攻套装-通用(GM礼包)";
                        case "31795":
                            return "90级-白-通用(GM礼包)";
                        case "31796":
                            return "90级-绿-通用(GM礼包)";
                        case "31797":
                            return "90级-蓝-通用(GM礼包)";
                        case "31798":
                            return "90级-紫-通用(GM礼包)";
                        case "31799":
                            return "90级-橙-通用(GM礼包)";
                        case "31800":
                            return "90级-紫物攻套装-通用(GM礼包)";
                        case "31801":
                            return "90级-橙物攻套装-通用(GM礼包)";
                        case "31802":
                            return "90级-紫魔攻套装-通用(GM礼包)";
                        case "31803":
                            return "90级-橙魔攻套装-通用(GM礼包)";
                        case "31804":
                            return "100级-白-通用(GM礼包)";
                        case "31805":
                            return "100级-绿-通用(GM礼包)";
                        case "31806":
                            return "100级-蓝-通用(GM礼包)";
                        case "31807":
                            return "100级-紫-通用(GM礼包)";
                        case "31808":
                            return "100级-橙-通用(GM礼包)";
                        case "31809":
                            return "100级-紫物攻套装-通用(GM礼包)";
                        case "31810":
                            return "100级-橙物攻套装-通用(GM礼包)";
                        case "31811":
                            return "100级-紫魔攻套装-通用(GM礼包)";
                        case "31812":
                            return "100级-橙魔攻套装-通用(GM礼包)";
                        case "31840":
                            return "忏悔卷轴";
                        case "31850":
                            return "金币卷轴50";
                        case "31851":
                            return "金币卷轴300";
                        case "31852":
                            return "金币卷轴1000";
                        case "31856":
                            return "据点战-王城";
                        case "31857":
                            return "据点战-高级据点";
                        case "31858":
                            return "据点战-中级据点";
                        case "31859":
                            return "据点战-初级据点";
                        case "31860":
                            return "初级生活技能材料宝箱";
                        case "31861":
                            return "初级宠物玩具宝箱";
                        case "31862":
                            return "饼干宝箱";
                        case "31900":
                            return "进入阵营对抗副本道具";
                        case "31901":
                            return "进入1-1副本道具";
                        case "31902":
                            return "进入1-2副本道具";
                        case "31903":
                            return "进入1-3副本道具";
                        case "31904":
                            return "进入1-4副本道具";
                        case "31905":
                            return "进入1-5副本道具";
                        case "31906":
                            return "进入1-6副本道具";
                        case "31907":
                            return "进入2-1副本道具";
                        case "31908":
                            return "进入2-2副本道具";
                        case "31909":
                            return "进入2-3副本道具";
                        case "31910":
                            return "进入2-4副本道具";
                        case "31911":
                            return "进入2-5副本道具";
                        case "31912":
                            return "进入2-6副本道具";
                        case "31913":
                            return "进入3-1副本道具";
                        case "31914":
                            return "进入3-2副本道具";
                        case "31915":
                            return "进入3-3副本道具";
                        case "31916":
                            return "进入3-4副本道具";
                        case "31917":
                            return "进入3-5副本道具";
                        case "31918":
                            return "进入3-6副本道具";
                        case "31919":
                            return "进入20魔王普通副本道具";
                        case "31920":
                            return "进入30魔王普通副本道具";
                        case "31921":
                            return "进入30魔王困难副本道具";
                        case "31922":
                            return "进入40魔王普通副本道具";
                        case "31923":
                            return "进入40魔王困难副本道具";
                        case "31924":
                            return "进入40魔王炼狱副本道具";
                        case "31925":
                            return "进入50魔王普通副本道具";
                        case "31926":
                            return "进入50魔王困难副本道具";
                        case "31927":
                            return "进入50魔王炼狱副本道具";
                        case "31928":
                            return "进入60魔王普通副本道具";
                        case "31929":
                            return "进入60魔王困难副本道具";
                        case "31930":
                            return "进入60魔王炼狱副本道具";
                        case "31931":
                            return "进入奥泰幻境副本道具";
                        case "32000":
                            return "(任务)时间胞";
                        case "32001":
                            return "(任务)奥泰之心";
                        case "32002":
                            return "(任务)无尽梦魇";
                        case "32003":
                            return "(任务)酋长之心";
                        case "32004":
                            return "(任务)药箱";
                        case "32005":
                            return "(任务)火把";
                        case "32006":
                            return "(任务)银币";
                        case "32007":
                            return "(任务)引爆杆";
                        case "32008":
                            return "(任务)零件";
                        case "32009":
                            return "(任务)火之力";
                        case "32010":
                            return "(任务)禁锢之力";
                        case "32011":
                            return "(任务)水之力";
                        case "32012":
                            return "(任务)魔眼之力";
                        case "32013":
                            return "(任务)解毒药水";
                        case "32014":
                            return "(任务)火把";
                        case "32015":
                            return "(任务)精灵项坠";
                        case "32016":
                            return "(任务)绿之光";
                        case "32017":
                            return "(任务)符印";
                        case "32018":
                            return "(任务)火把//地宫";
                        case "32019":
                            return "26-29(任务)火把//危险场景-联盟";
                        case "32020":
                            return "26-29(任务)零件//危险场景-联盟";
                        case "32021":
                            return "26-29(任务)零件//危险场景-军团";
                        case "32022":
                            return "26-29(任务)火把//危险场景-军团";
                        case "32023":
                            return "(对抗副本)弹药箱";
                        case "32024":
                            return "30-39(任务)火把//危险场景-联盟";
                        case "32025":
                            return "30-39(任务)零件//危险场景-联盟";
                        case "32026":
                            return "30-39(任务)火把//危险场景-军团";
                        case "32027":
                            return "30-39(任务)零件//危险场景-军团";
                        case "32028":
                            return "(任务)小镇--修复封印1";
                        case "32029":
                            return "(任务)小镇--修复封印2";
                        case "32030":
                            return "(任务)小镇--烧毁战车1";
                        case "32031":
                            return "(任务)小镇--烧毁战车2";
                        case "32032":
                            return "(任务)小镇--烧毁战车3";
                        case "32033":
                            return "(任务)小镇--药箱";
                        case "32034":
                            return "(任务)蛮荒--清水";
                        case "32035":
                            return "(任务)蛮荒--草药";
                        case "32036":
                            return "(任务)蛮荒--修复封印1";
                        case "32037":
                            return "(任务)蛮荒--哨子";
                        case "32038":
                            return "(任务)蛮荒--修复封印1";
                        case "32039":
                            return "(任务)蛮荒--修复封印2";
                        case "32040":
                            return "(任务)海底--展示令牌";
                        case "32041":
                            return "(任务)海底--展示神器";
                        case "32042":
                            return "(任务)公会--打扫";
                        case "32043":
                            return "(任务)公会--膜拜";
                        case "32044":
                            return "(任务)公会--菜";
                        case "32045":
                            return "(任务)宠物岛--饼干";
                        case "32046":
                            return "40-49(任务)火把//危险场景-联盟";
                        case "32047":
                            return "40-49(任务)零件//危险场景-联盟";
                        case "32048":
                            return "40-49(任务)火把//危险场景-军团";
                        case "32049":
                            return "40-49(任务)零件//危险场景-军团";
                        case "32050":
                            return "50-59(任务)火把//危险场景-联盟";
                        case "32051":
                            return "50-59(任务)零件//危险场景-联盟";
                        case "32052":
                            return "50-59(任务)火把//危险场景-军团";
                        case "32053":
                            return "50-59(任务)零件//危险场景-军团";
                        case "32054":
                            return "60-69(任务)火把//危险场景-联盟";
                        case "32055":
                            return "60-69(任务)零件//危险场景-联盟";
                        case "32056":
                            return "60-69(任务)火把//危险场景-军团";
                        case "32057":
                            return "60-69(任务)零件//危险场景-军团";
                        case "32058":
                            return "70-80(任务)火把//危险场景-联盟";
                        case "32059":
                            return "70-80(任务)零件//危险场景-联盟";
                        case "32060":
                            return "70-80(任务)火把//危险场景-军团";
                        case "32061":
                            return "70-80(任务)零件//危险场景-军团";
                        case "40001":
                            return "(预览)银币";
                        case "40002":
                            return "(预览)银币堆-1";
                        case "40003":
                            return "(预览)银币堆-2";
                        case "40004":
                            return "(预览)银币堆-3";
                        case "40005":
                            return "(预览)银币堆-4";
                        case "40101":
                            return "(预览)钻石";
                        case "40102":
                            return "(预览)钻石袋子-1";
                        case "40103":
                            return "(预览)钻石袋子-2";
                        case "40104":
                            return "(预览)钻石袋子-3";
                        case "40201":
                            return "(预览)经验(纯显示用)";
                        case "40202":
                            return "(预览)经验(纯显示用)";
                        case "40203":
                            return "(预览)经验(纯显示用)";
                        case "40204":
                            return "(预览)经验(纯显示用)";
                        case "40205":
                            return "(预览)经验(纯显示用)";
                        case "40301":
                            return "(预览)装备1级-白";
                        case "40302":
                            return "(预览)装备1级-绿";
                        case "40303":
                            return "(预览)装备1级-蓝";
                        case "40304":
                            return "(预览)装备1级-紫";
                        case "40305":
                            return "(预览)装备1级-橙";
                        case "40306":
                            return "(预览)装备1级-红";
                        case "40311":
                            return "(预览)装备10级-白";
                        case "40312":
                            return "(预览)装备10级-绿";
                        case "40313":
                            return "(预览)装备10级-蓝";
                        case "40314":
                            return "(预览)装备10级-紫";
                        case "40315":
                            return "(预览)装备10级-橙";
                        case "40316":
                            return "(预览)装备10级-红";
                        case "40331":
                            return "(预览)装备30级-白";
                        case "40332":
                            return "(预览)装备30级-绿";
                        case "40333":
                            return "(预览)装备30级-蓝";
                        case "40334":
                            return "(预览)装备30级-紫";
                        case "40335":
                            return "(预览)装备30级-橙";
                        case "40336":
                            return "(预览)装备30级-红";
                        case "40341":
                            return "(预览)装备40级-白";
                        case "40342":
                            return "(预览)装备40级-绿";
                        case "40343":
                            return "(预览)装备40级-蓝";
                        case "40344":
                            return "(预览)装备40级-紫";
                        case "40345":
                            return "(预览)装备40级-橙";
                        case "40346":
                            return "(预览)装备40级-红";
                        case "40351":
                            return "(预览)装备50级-白";
                        case "40352":
                            return "(预览)装备50级-绿";
                        case "40353":
                            return "(预览)装备50级-蓝";
                        case "40354":
                            return "(预览)装备50级-紫";
                        case "40355":
                            return "(预览)装备50级-橙";
                        case "40356":
                            return "(预览)装备50级-红";
                        case "40361":
                            return "(预览)装备60级-白";
                        case "40362":
                            return "(预览)装备60级-绿";
                        case "40363":
                            return "(预览)装备60级-蓝";
                        case "40364":
                            return "(预览)装备60级-紫";
                        case "40365":
                            return "(预览)装备60级-橙";
                        case "40366":
                            return "(预览)装备60级-红";
                        case "40371":
                            return "(预览)装备70级-白";
                        case "40372":
                            return "(预览)装备70级-绿";
                        case "40373":
                            return "(预览)装备70级-蓝";
                        case "40374":
                            return "(预览)装备70级-紫";
                        case "40375":
                            return "(预览)装备70级-橙";
                        case "40376":
                            return "(预览)装备70级-红";
                        case "40381":
                            return "(预览)装备80级-白";
                        case "40382":
                            return "(预览)装备80级-绿";
                        case "40383":
                            return "(预览)装备80级-蓝";
                        case "40384":
                            return "(预览)装备80级-紫";
                        case "40385":
                            return "(预览)装备80级-橙";
                        case "40386":
                            return "(预览)装备80级-红";
                        case "40391":
                            return "(预览)装备-白";
                        case "40392":
                            return "(预览)装备-绿";
                        case "40393":
                            return "(预览)装备-蓝";
                        case "40394":
                            return "(预览)装备-紫";
                        case "40395":
                            return "(预览)装备-橙";
                        case "40396":
                            return "(预览)装备-红";
                        case "40401":
                            return "(预览)宝石袋子-1";
                        case "40402":
                            return "(预览)宝石袋子-2";
                        case "40403":
                            return "(预览)宝石袋子-3";
                        case "40404":
                            return "(预览)宝石袋子-4";
                        case "40405":
                            return "(预览)宝石袋子-5";
                        case "40406":
                            return "(预览)宝石袋子-6";
                        case "40407":
                            return "(预览)宝石袋子-7";
                        case "40408":
                            return "(预览)宝石袋子-8";
                        case "40409":
                            return "(预览)宝石袋子-9";
                        case "40410":
                            return "(预览)宝石袋子-10";
                        case "41001":
                            return "(卖钱1)破碎的武器";
                        case "41002":
                            return "(卖钱2)陈旧的怀表";
                        case "41003":
                            return "(卖钱3)原矿石";
                        case "41004":
                            return "(卖钱4)暗淡的水晶球";
                        case "41005":
                            return "(卖钱5)古老的黄金罐";
                        case "41010":
                            return "宠物岛活动奖励预览1";
                        case "41011":
                            return "宠物岛活动奖励预览2";
                        case "42001":
                            return "领地战道具";
                        case "42002":
                            return "领地战道具";
                        case "42020":
                            return "领地战buff卷轴";
                        case "42999":
                            return "领地战buff卷轴";
                        case "43000":
                            return "活动-万圣节-诅咒骨头";
                        case "43001":
                            return "活动-万圣节-蝙蝠翅膀";
                        case "43002":
                            return "活动-万圣节-蜘蛛网";
                        case "43003":
                            return "活动-万圣节-魔法帽";
                        case "43004":
                            return "活动-万圣节-南瓜糖";
                        default:
                            return value;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
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
