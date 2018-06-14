using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class DiamondLog : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
                DBHelperGSSDB.connectionString = ConnStrGSSDB;
                if (!IsPostBack)
                {
                    tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                    tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                    BindDdl1();
                    bind();
                    BindDdl2("100001");
                }
                ControlOutFile1.ControlOut = GridView1;
                ControlOutFile1.VisibleExcel = false;
            }
            catch (Exception ex)
            {
                lblerro.Text = ex.StackTrace + "--" + ex.Message;
            }
        }


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
            if (DropDownListArea3.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea3.SelectedItem.Text;
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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat) + " ";

            string sqlwhere = " where 1=1 ";
            if (!string.IsNullOrEmpty(tbRoleId.Text.Trim()))
            {
                sqlwhere += @" and CID=" + tbRoleId.Text.Trim();
            }
            if(!string.IsNullOrEmpty(txtPARA_1.Text.Trim()))
            {
                sqlwhere += @" and PARA_1=" + txtPARA_1.Text.Trim();
            }
            //sqlwhere += @" and F_Date='" + searchdateB.ToString(SmallDateTimeFormat) + "'";
            //if (DropDownListArea1.SelectedIndex > 0)
            //{
            //    sqlwhere += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            //}
            //if (DropDownListArea2.SelectedIndex > 0)
            //{
            //    sqlwhere += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            //}
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
            string sql = "";
            string sqlCount = "";
            sql = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT * FROM " + searchdateB.ToString("yyyy_MM_dd") + "_gold_log" + sqlwhere + " limit " + (Convert.ToInt32(lblcurPage.Text) - 1) * 20 + ",20')";
            sqlCount = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT count(*) FROM " + searchdateB.ToString("yyyy_MM_dd") + "_gold_log " + sqlwhere + " ')";
            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                object obj = DBHelperDigGameDB.GetSingle(sqlCount);
                int objCount=0;
                if(obj!=null)
                {
                    objCount = Convert.ToInt32(obj);
                }
                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                    myView.AddNew();

                    lblPageCount.Text = (objCount % GridView1.PageSize == 0 ? objCount / GridView1.PageSize : objCount / GridView1.PageSize + 1).ToString();
                }
                else
                {
                    lblerro.Visible = false;
                    //lblcurPage.Text = "1";
                    lblPageCount.Text = (objCount % GridView1.PageSize == 0 ? objCount / GridView1.PageSize : objCount / GridView1.PageSize + 1).ToString();
                    cmdFirstPage.Enabled = true;
                    cmdPreview.Enabled = true;
                    cmdNext.Enabled = true;
                    cmdLastPage.Enabled = true;
                    if (lblcurPage.Text == "1")
                    {
                        cmdFirstPage.Enabled = false;
                        cmdPreview.Enabled = false;
                    }
                    else if (lblcurPage.Text == lblPageCount.Text)
                    {
                        cmdLastPage.Enabled = false;
                    }
                }
                GridView1.DataSource = myView;
                GridView1.DataBind();

                //GridView2.DataSource = myView;
                //GridView2.DataBind();

                //if (ControlChart1.Visible == true)
                //{
                //    ControlChart1.SetChart(GridView2, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
                //}
            }
            catch (System.Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
                lblerro.Text = sql+"=="+ex.Message+""+ex.StackTrace;
            }

        }
        protected void ExportExcel(object sender,EventArgs e)
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
            if (DropDownListArea3.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea3.SelectedItem.Text;
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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat) + " ";

            string sqlwhere = " where 1=1 ";
            if (!string.IsNullOrEmpty(tbRoleId.Text.Trim()))
            {
                sqlwhere += @" and CID=" + tbRoleId.Text.Trim();
            }
            if(!string.IsNullOrEmpty(txtPARA_1.Text.Trim()))
            {
                sqlwhere += @" and PARA_1=" + txtPARA_1.Text.Trim();
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
            string sql = "";
            sql = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT UID 用户编号,CID 角色编号,CID 角色名称,PARA_1 获取途径,PARA_2 变化值,OPID 货币类型,OP_BAK 备注,OP_TIME 时间 FROM " + searchdateB.ToString("yyyy_MM_dd") + "_gold_log" + sqlwhere + "')";
            ds = DBHelperDigGameDB.Query(sql);

            DataTable dt = new DataTable();
            dt.Columns.Add("用户编号", System.Type.GetType("System.String"));
            dt.Columns.Add("角色编号", System.Type.GetType("System.String"));
            dt.Columns.Add("角色名称", System.Type.GetType("System.String"));
            dt.Columns.Add("获取途径", System.Type.GetType("System.String"));
            dt.Columns.Add("变化值", System.Type.GetType("System.String"));
            dt.Columns.Add("货币类型", System.Type.GetType("System.String"));
            dt.Columns.Add("备注", System.Type.GetType("System.String"));
            dt.Columns.Add("时间", System.Type.GetType("System.String"));

            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dt.Rows.Add(
                        ds.Tables[0].Rows[i][0],
                        ds.Tables[0].Rows[i][1],
                        GetRoleName(ds.Tables[0].Rows[i][2].ToString()),
                        GetWay(ds.Tables[0].Rows[i][3].ToString()),
                        ds.Tables[0].Rows[i][4],
                        GetOPID(ds.Tables[0].Rows[i][5].ToString()),
                        ds.Tables[0].Rows[i][6],
                        ds.Tables[0].Rows[i][7]
                        );
                }
            }
            DataSet dsNew = new DataSet();
            dsNew.Tables.Add(dt);
            ExportExcelHelper.ExportDataSet(dsNew);
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            bind();
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



        public string TransDe(string value)
        {
            if (lblDecoding.Visible)
            {
                return CodingTran.Tran(lblDeType.Text, value);
            }
            else
            {
                return value;
            }
        }
        //得到角色名称
        public string GetRoleName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;
                string sql = @"SELECT F_RoleName FROM [LKSV_2_GameCoreDB_0_1].Gamecoredb.dbo.T_RoleCreate with(nolock) where F_RoleID=" + value + "";
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }

        public void BindDdl1()
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
        public void BindDdl2(string id)
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
        public void BindDdl3(string id)
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = " + id + ") AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();

                newdr["F_Name"] = App_GlobalResources.Language.LblAllZoneLine;

                newdr["F_ValueGame"] = "";
                ds.Tables[0].Rows.InsertAt(newdr, 0);
                this.DropDownListArea3.DataSource = ds;
                this.DropDownListArea3.DataTextField = "F_Name";
                this.DropDownListArea3.DataValueField = "F_ValueGame";
                this.DropDownListArea3.DataBind();

            }
            catch (System.Exception ex)
            {
                DropDownListArea3.Items.Clear();
                DropDownListArea3.Items.Add(new ListItem(App_GlobalResources.Language.LblAllZoneLine, ""));
            }
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            System.Text.EncodingInfo[] ss = System.Text.Encoding.GetEncodings();
            ControlDateSelect1.SetSelectDate(tboxTimeB.Text);
            bind();
        }

        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListArea2.Items.Clear();
            DropDownListArea3.Items.Clear();
            BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);
            BindDdl3(DropDownListArea2.SelectedValue.Split(',')[0]);

        }

        protected void DropDownListArea2_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListArea3.Items.Clear();
            BindDdl3(DropDownListArea2.SelectedValue.Split(',')[0]);

        }
        #region 分页触发方法
        protected void GvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();  //重新绑定GridView
        }
        #endregion
        protected void lbtnF_Click(object sender, EventArgs e)
        {
            lblcurPage.Text = "1";
            bind();
        }

        protected void lbtnP_Click(object sender, EventArgs e)
        {
            if (lblcurPage.Text != "1")
            {
                lblcurPage.Text = (Convert.ToInt32(lblcurPage.Text) - 1).ToString();
                bind();
            }
        }

        protected void lbtnN_Click(object sender, EventArgs e)
        {
            if (lblcurPage.Text != lblPageCount.Text)
            {
                lblcurPage.Text = (Convert.ToInt32(lblcurPage.Text) + 1).ToString();
                bind();
            }
        }

        protected void lbtnE_Click(object sender, EventArgs e)
        {
            lblcurPage.Text = lblPageCount.Text;
            bind();
        }

        protected void Go_Click(object sender, EventArgs e)
        {
            try
            {
                int pindex = Convert.ToInt32(lblcurPage.Text);
                if (pindex > 0 && pindex <= Convert.ToInt32(lblPageCount.Text))
                {
                    lblcurPage.Text = Convert.ToInt32(lblcurPage.Text).ToString();
                    bind();
                }

            }
            catch (System.Exception ex)
            {

            }
        }
        protected string GetOPID(string opid)
        {
            string str = string.Empty;
            if (PageLanguage == "ko-kr")
            {
                switch (opid)
                {
                    case "10055":
                        str = "10055-레드 다이아";
                        break;
                    case "10056":
                        str = "10056-블루 다이아";
                        break;
                    default:
                        str = opid;
                        break;
                }
            }
            else
            {
                switch (opid)
                {
                    case "10055":
                        str = "10055-红钻";
                        break;
                    case "10056":
                        str = "10056-蓝钻";
                        break;
                    default:
                        str = opid;
                        break;
                }
            }
            return str;
        }
        public string GetWay(string value)
        {
            try
            {
                if (PageLanguage == "ko-kr")
                {
                    #region 中文
                    switch (value)
                    {
                        case "0":
                            return "시스템 교환";
                        case "1":
                            return "거래";
                        case "2":
                            return "상점 구매";
                        case "3":
                            return "LGC스크립트 획득";
                        case "4":
                            return "고급 시스템 구매";
                        case "5":
                            return "우편";
                        case "6":
                            return "거래소 아이템 구매";
                        case "7":
                            return "거래소 수수료";
                        case "8":
                            return "길드 기부";
                        case "9":
                            return "호송 보증금 소모";
                        case "10":
                            return "호송 승급 소모";
                        case "11":
                            return "오프라인 경험치";
                        case "12":
                            return "GM명령 생성";
                        case "13":
                            return "다이아 거래 등록";
                        case "14":
                            return "다이아 거래 반환";
                        case "15":
                            return "다이아 거래 수수료";
                        case "16":
                            return "물품 교환 소모";
                        case "17":
                            return "여신의 은총 소모";
                        case "18":
                            return "캐릭터명 변경 소모";
                        case "19":
                            return "길드명 변경 소모";
                        case "20":
                            return "소모(易容）";
                        case "21":
                            return "법보 스킬 갱신 소모";
                        case "22":
                            return "장비/법보 합성";
                        case "24":
                            return "보석 정수 승급 소모";
                        case "25":
                            return "보석 분해 소모";
                        case "27":
                            return "합성";
                        case "28":
                            return "합성";
                        case "29":
                            return "보물지도";
                        case "30":
                            return "보스던전 갱신 소모";
                        case "31":
                            return "캐릭터 부화";
                        case "32":
                            return "업적 시스템 보상 수령";
                        case "33":
                            return "활력 구매";
                        case "34":
                            return "던전 카드 뒤집기";
                        case "35":
                            return "보석 승급 소모";
                        case "36":
                            return "보석 각인 소모";
                        case "37":
                            return "보석 해제 소모";
                        case "38":
                            return "인챈트 소모";
                        case "39":
                            return "인챈트 슬롯 오픈";
                        case "40":
                            return "특성 소모";
                        case "41":
                            return "줍기";
                        case "42":
                            return "챕터 던전";
                        case "43":
                            return "친구/적대 워프";
                        case "44":
                            return "NPC상점 거래";
                        case "45":
                            return "보서 던전 구매 소비";
                        case "46":
                            return "길드 생성 소비";
                        case "47":
                            return "골드 구매 소모";
                        case "48":
                            return "다이아로 비귀속 골드 구매";
                        case "49":
                            return "다이아로 귀속 골드 구매";
                        case "50":
                            return "비귀속다이아로 귀속 골드 구매";
                        case "51":
                            return "비귀속다이아로 비귀속 골드 구매";
                        case "52":
                            return "보상 획득";
                        case "53":
                            return "결제 획득";
                        case "54":
                            return "주간/월간 프리미엄 카드 획득";
                        case "55":
                            return "제국 토벌 블루 다이아 소모";
                        case "56":
                            return "첫 충전/주간/월간 누적 획득";
                        case "57":
                            return "30일 출석";
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
                        case "0":
                            return "系统兑换";
                        case "1":
                            return "玩家交易";
                        case "2":
                            return "商城购物";
                        case "3":
                            return "通过LGC 脚本提取绑定元宝";
                        case "4":
                            return "玩家购买高级挂机功能";
                        case "5":
                            return "通过邮件";
                        case "6":
                            return "购买拍卖行道具";
                        case "7":
                            return "拍卖行手续费";
                        case "8":
                            return "帮会捐献";
                        case "9":
                            return "押镖投保消耗";
                        case "10":
                            return "镖车升级消耗";
                        case "11":
                            return "离线经验";
                        case "12":
                            return "GM指令生成的元宝";
                        case "13":
                            return "元宝交易行挂单";
                        case "14":
                            return "元宝交易行返还";
                        case "15":
                            return "元宝交易行购买的手续费";
                        case "16":
                            return "兑换物品消耗";
                        case "17":
                            return "命魂五择消耗";
                        case "18":
                            return "角色改名消耗";
                        case "19":
                            return "帮会改名消耗";
                        case "20":
                            return "易容消耗";
                        case "21":
                            return "刷新法宝技能消耗";
                        case "22":
                            return "装备/法宝融合";
                        case "24":
                            return "宝石精华升级消耗";
                        case "25":
                            return "宝石分解消耗";
                        case "27":
                            return "合成";
                        case "28":
                            return "合成";
                        case "29":
                            return "藏宝图";
                        case "30":
                            return "Boss副本重置消耗";
                        case "31":
                            return "主角复活";
                        case "32":
                            return "成就系统领取奖励";
                        case "33":
                            return "购买体力";
                        case "34":
                            return "副本翻牌";
                        case "35":
                            return "符石升级消耗";
                        case "36":
                            return "符石镶嵌消耗";
                        case "37":
                            return "符石卸下消耗";
                        case "38":
                            return "附魔消耗";
                        case "39":
                            return "附魔开孔";
                        case "40":
                            return "天赋消耗";
                        case "41":
                            return "捡取";
                        case "42":
                            return "章节副本";
                        case "43":
                            return "好友仇人传送";
                        case "44":
                            return "NPC商店交易";
                        case "45":
                            return "购买Boss副本花费";
                        case "46":
                            return "创建公会花费";
                        case "47":
                            return "买金币花费钻石";
                        case "48":
                            return "货币兑换，用钻石购买非绑定金币";
                        case "49":
                            return "货币兑换，用钻石购买绑定金币";
                        case "50":
                            return "货币兑换，用绑定钻石购买绑定金币";
                        case "51":
                            return "货币兑换，用非绑定钻石购买绑定钻石";
                        case "52":
                            return "从奖励表产生";
                        case "53":
                            return "充值产生的钻石";
                        case "54":
                            return "购买周卡月卡产生的钻石";
                        case "55":
                            return "帝国悬赏刷星消耗蓝钻";
                        case "56":
                            return "首充，周月累积提取获得";
                        case "57":
                            return "三十日签到";
                        default:
                            return value;
                    }
                    #endregion
                }
                
            }
            catch (Exception ex)
            {
                this.lblerro.Text = ex.InnerException.Message;
                return value;
                
            }
        }
    }
}
