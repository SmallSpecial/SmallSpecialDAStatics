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
    public partial class GoldLog : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
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
                string str = PageLanguage;
            }
            ControlOutFile1.ControlOut = GridView1;
            ControlOutFile1.VisibleExcel = false;
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
            if (!string.IsNullOrEmpty(txtPARA_1.Text.Trim()))
            {
                sqlwhere += @" and PARA_1=" + txtPARA_1.Text.Trim();
            }
            if(!string.IsNullOrEmpty(ddlOPID.SelectedValue))
            {
                sqlwhere += @" and OPID=" + ddlOPID.SelectedValue;
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
            string battleZone=string.Empty;
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
            sql = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT * FROM " + searchdateB.ToString("yyyy_MM_dd") + "_money_log " + sqlwhere + " limit " + (Convert.ToInt32(lblcurPage.Text) - 1)*20 + ",20')";
            sqlCount = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT count(*) FROM " + searchdateB.ToString("yyyy_MM_dd") + "_money_log " + sqlwhere + " ')";
            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                object obj = DBHelperDigGameDB.GetSingle(sqlCount);
                int objCount = Convert.ToInt32(obj);
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
                //lblerro.Text = sql;
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
            if (!string.IsNullOrEmpty(txtPARA_1.Text.Trim()))
            {
                sqlwhere += @" and PARA_1=" + txtPARA_1.Text.Trim();
            }
            if(!string.IsNullOrEmpty(ddlOPID.SelectedValue))
            {
                sqlwhere += @" and OPID=" + ddlOPID.SelectedValue;
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

            sql = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT UID 用户编号,CID 角色编号,CID 角色名称,PARA_1 获取途径,PARA_2 变化值,OPID 货币类型,OP_BAK 备注,OP_TIME 时间 FROM " + searchdateB.ToString("yyyy_MM_dd") + "_money_log " + sqlwhere + "')";
            
            
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
                        GetWay(ds.Tables[0].Rows[i][5].ToString(), ds.Tables[0].Rows[i][3].ToString()),
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
                    case "10038":
                        str = "10038-실버";
                        break;
                    case "10037":
                        str = "10037-골드";
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
                    case "10038":
                        str = "10038-银币";
                        break;
                    case "10037":
                        str = "10037-金币";
                        break;
                    default:
                        str = opid;
                        break;
                }
            }
            return str;
        }
        public string GetWay(string type,string value)
        {
            if (type=="10038")
            {
                try
                {
                    if (PageLanguage == "ko-kr")
                    {
                        #region 韩文
                        switch (value)
                        {
                            case "0":
                                return "획득";
                            case "1":
                                return "GM명령으로 수정";
                            case "2":
                                return "우편 획득";
                            case "3":
                                return "퀘스트 보상";
                            case "4":
                                return "귀속 골드 보상";
                            case "5":
                                return "상점 거래";
                            case "6":
                                return "장비 성장";
                            case "7":
                                return "보석 합성";
                            case "8":
                                return "장비/법보 합성";
                            case "9":
                                return "법보 승급";
                            case "10":
                                return "법보 돌파";
                            case "11":
                                return "장비 마법부여";
                            case "12":
                                return "수리";
                            case "13":
                                return "LGC조작";
                            case "14":
                                return "이벤트";
                            case "15":
                                return "제련 이전";
                            case "16":
                                return "생활 스킬";
                            case "17":
                                return "오프라인 경험치";
                            case "18":
                                return "진영 변경";
                            case "19":
                                return "장비 각인";
                            case "20":
                                return "용혼";
                            case "21":
                                return "명혼 상자";
                            case "22":
                                return "탈것 번식";
                            case "23":
                                return "교환 소모";
                            case "24":
                                return "탈것 등급 새로고침";
                            case "25":
                                return "탈것 자질 비율 새로고침";
                            case "26":
                                return "탈것 각성(선천)";
                            case "27":
                                return "탈것 각성(후천)";
                            case "28":
                                return "자동 사냥";
                            case "29":
                                return "탈것 진화";
                            case "30":
                                return "스킬 승급";
                            case "31":
                                return "이벤트 보상(乱武)";
                            case "32":
                                return "사부 인사";
                            case "33":
                                return "사제 아이템 제작";
                            case "35":
                                return "길드 생성";
                            case "36":
                                return "탈것 경험치 전이";
                            case "37":
                                return "보석 정수 승급";
                            case "38":
                                return "영혼 보충";
                            case "39":
                                return "장비 슬롯 오픈";
                            case "40":
                                return "보석 각인";
                            case "41":
                                return "보석 해제";
                            case "44":
                                return "챕터 던전";
                            case "45":
                                return "협객 장비 승급";
                            case "46":
                                return "캐릭터 장비 강화";
                            case "47":
                                return "보물지도";
                            case "48":
                                return "합성 기능";
                            case "49":
                                return "합성 기능";
                            case "50":
                                return "스킬 승급";
                            case "51":
                                return "협객 스킬 승급";
                            case "52":
                                return "보스 던전 새로고침 소모";
                            case "53":
                                return "업적 시스템 보상 수령";
                            case "54":
                                return "활력 구매";
                            case "55":
                                return "보상 테이블 수령";
                            case "56":
                                return "서버 인증 실패";
                            case "57":
                                return "던전 카드 뒤집기";
                            case "58":
                                return "보석 승급 소모";
                            case "59":
                                return "보석 슬롯 오픈 소모";
                            case "60":
                                return "보석 각인 소모";
                            case "61":
                                return "보석 해제 소모";
                            case "62":
                                return "인챈트 소모";
                            case "63":
                                return "인챈트 소모";
                            case "64":
                                return "승급";
                            case "65":
                                return "장비 분해";
                            case "66":
                                return "던전 카드 뒤집기";
                            case "67":
                                return "신의 시련";
                            case "68":
                                return "탈것 활성화 소모";
                            case "69":
                                return "탈것 육성 소모";
                            case "70":
                                return "탈것 육성 소모";
                            case "71":
                                return "연금으로 골드 획득";
                            case "72":
                                return "장비 개조 소모 골드";
                            case "73":
                                return "길드 스킬";
                            case "74":
                                return "LongY[ Zck ] 거래소";
                            case "75":
                                return "친구/적대 워프";
                            case "76":
                                return "신의 시련 소탕";
                            case "77":
                                return "가방 오픈";
                            case "78":
                                return "순환 퀘스트 등급 새로고침";
                            case "79":
                                return "장비 강화 차감 골드";
                            case "80":
                                return "장비 인챈트 차감 골드";
                            case "81":
                                return "제국 토벌 승급 차감 골드";
                            case "82":
                                return "생활 스킬 학습 소모";
                            case "83":
                                return "비귀속 골드를 귀속 골드로 변경";
                            case "84":
                                return "다이아를 귀속 골드로 변경";
                            case "85":
                                return "마법 용광로 새로고침 소모 귀속 골드";
                            case "86":
                                return "귀속 다이아를 귀속 골드로 변경";
                            case "87":
                                return "길드 생성";
                            case "88":
                                return "개인 호송 획득";
                            case "89":
                                return "호송 보증금 차감";
                            case "90":
                                return "호송 성공 보증금 반환";
                            case "91":
                                return "호송 실패 일정 비율 보증금 반환";
                            case "92":
                                return "누적 충전 획득 수령";
                            case "93":
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
                                return "拾取";
                            case "1":
                                return "GM用指令修改的";
                            case "2":
                                return "邮件获取";
                            case "3":
                                return "任务奖励";
                            case "4":
                                return "绑定金钱洪荒奖励";
                            case "5":
                                return "商店交易";
                            case "6":
                                return "装备精炼";
                            case "7":
                                return "宝石合成";
                            case "8":
                                return "装备/法宝融合";
                            case "9":
                                return "法宝升档";
                            case "10":
                                return "法宝升星";
                            case "11":
                                return "装备重铸";
                            case "12":
                                return "修理";
                            case "13":
                                return "LGC 操作";
                            case "14":
                                return "活动";
                            case "15":
                                return "精炼转移";
                            case "16":
                                return "生活技能";
                            case "17":
                                return "离线经验";
                            case "18":
                                return "切换联盟";
                            case "19":
                                return "装备铭刻";
                            case "20":
                                return "龙魂";
                            case "21":
                                return "命魂盒";
                            case "22":
                                return "坐骑繁殖";
                            case "23":
                                return "兑换消耗";
                            case "24":
                                return "坐骑刷新星级";
                            case "25":
                                return "坐骑刷新资质比例";
                            case "26":
                                return "坐骑先天悟道";
                            case "27":
                                return "坐骑后天悟道";
                            case "28":
                                return "自动挂机";
                            case "29":
                                return "坐骑转生";
                            case "30":
                                return "技能进阶";
                            case "31":
                                return "乱武奖励";
                            case "32":
                                return "孝敬师傅";
                            case "33":
                                return "制作师徒道具";
                            case "35":
                                return "创建帮会";
                            case "36":
                                return "坐骑经验转移";
                            case "37":
                                return "宝石精华升级";
                            case "38":
                                return "补灵";
                            case "39":
                                return "装备打孔";
                            case "40":
                                return "宝石镶嵌";
                            case "41":
                                return "宝石拆除";
                            case "44":
                                return "章节副本";
                            case "45":
                                return "侠客装备升级";
                            case "46":
                                return "主角装备强化(冶炼)";
                            case "47":
                                return "藏宝图";
                            case "48":
                                return "合成功能";
                            case "49":
                                return "合成功能";
                            case "50":
                                return "技能升级";
                            case "51":
                                return "侠客技能升级";
                            case "52":
                                return "Boss副本重置消耗";
                            case "53":
                                return "成就系统领取奖励";
                            case "54":
                                return "购买体力";
                            case "55":
                                return "奖励表奖励";
                            case "56":
                                return "服务器验证失败纠正";
                            case "57":
                                return "副本翻牌";
                            case "58":
                                return "符石升级消耗";
                            case "59":
                                return "符石开孔消耗";
                            case "60":
                                return "符石镶嵌消耗";
                            case "61":
                                return "符石卸下消耗";
                            case "62":
                                return "附魔消耗";
                            case "63":
                                return "附魔消耗";
                            case "64":
                                return "升星";
                            case "65":
                                return "装备分解";
                            case "66":
                                return "副本翻牌";
                            case "67":
                                return "奥泰副本（为成功奖励为扫荡）";
                            case "68":
                                return "坐骑激活消耗";
                            case "69":
                                return "坐骑培养次消耗";
                            case "70":
                                return "坐骑培养次消耗";
                            case "71":
                                return "炼金得金币";
                            case "72":
                                return "装备洗练消耗金币";
                            case "73":
                                return "公会技能";
                            case "74":
                                return "LongY[ Zck ] 拍卖行";
                            case "75":
                                return "好友仇人传送";
                            case "76":
                                return "奥泰扫荡";
                            case "77":
                                return "开启背包";
                            case "78":
                                return "循环任务刷星";
                            case "79":
                                return "装备强化扣除金币";
                            case "80":
                                return "装备附魔扣除金币";
                            case "81":
                                return "帝国悬赏刷星扣除金币";
                            case "82":
                                return "学习生活技能消耗";
                            case "83":
                                return "货币兑换非绑定金币换绑定金币";
                            case "84":
                                return "货币兑换钻石换绑定金币";
                            case "85":
                                return "魔法熔炉重置消耗绑定金币";
                            case "86":
                                return "货币兑换绑定钻石换绑定金币";
                            case "87":
                                return "创建公会";
                            case "88":
                                return "个人押镖所得";
                            case "89":
                                return "押镖扣抵押金";
                            case "90":
                                return "押镖成功返还抵押金";
                            case "91":
                                return "押镖失败返还一定比率的抵押金";
                            case "92":
                                return "累积充值获得领取";
                            case "93":
                                return "三十天签到";
                            default:
                                return value;
                        }
                        #endregion
                    }
                }
                catch(Exception ex)
                {
                    return value;
                }
            }
            else if(type=="10037")
            {
                try
                {
                    if (PageLanguage == "ko-kr")
                    {
                        switch(value)
                        {
                            case "0":
                                return "창고 조작";
                            case "1":
                                return "유저간 거래";
                            case "2":
                                return "우편 첨부";
                            case "3":
                                return "경매 아이템 구매";
                            case "4":
                                return "줍기";
                            case "5":
                                return "퀘스트 보상";
                            case "6":
                                return "길드 생성";
                            case "7":
                                return "맵 전환";
                            case "8":
                                return "GM명령으로 수정";
                            case "9":
                                return "시스템 툴 발송";
                            case "10":
                                return "NPC거래	";
                            case "11":
                                return "장비 수리 소비";
                            case "12":
                                return "가족 생성";
                            case "13":
                                return "스킬 학습";
                            case "14":
                                return "法宝升星";
                            case "15":
                                return "가족 기부";
                            case "16":
                                return "회위병 부활";
                            case "17":
                                return "퀴즈 보상";
                            case "18":
                                return "장비강화";
                            case "19":
                                return "장비 装备幻化";
                            case "20":
                                return "보석 합성";
                            case "21":
                                return "장비 인챈트";
                            case "22":
                                return "보상 퀘스트 수령 차감";
                            case "23":
                                return "장비 감정";
                            case "24":
                                return "lgc아이템 추가";
                            case "25	 ":
                                return "法宝升档";
                            case "26":
                                return "전장 포인트 교환";
                            case "27":
                                return "보상 교환";
                            case "28":
                                return "邮资";
                            case "29":
                                return "퀘스트 보상";
                            case "30":
                                return "法宝技能激活";
                            case "31":
                                return "거래소 수수료";
                            case "32":
                                return "퀘스트 즉시 완료 차감";
                            case "33":
                                return "퀘스트 갱신 차감";
                            case "34":
                                return "퀘스트 등급 갱신 차감";
                            case "35":
                                return "LGC 작업";
                            case "36":
                                return "길드 레벨업";
                            case "37":
                                return "천계 이벤트";
                            case "38":
                                return "이벤트 참가 신청";
                            case "39":
                                return "호송 보증금 소모";
                            case "40":
                                return "호송 승급 소모";
                            case "41":
                                return "길드 기부";
                            case "43	 ":
                                return "수호성 직업 변경";
                            case "44":
                                return "장비 성장 이전";
                            case "45":
                                return "法宝技能刷新";
                            case "46":
                                return "생화스킬";
                            case "47":
                                return "오프라인 경험치";
                            case "48":
                                return "灵数猎手";
                            case "49":
                                return "원보 거래소 등록";
                            case "50":
                                return "원보 거래소 등록 반환";
                            case "51":
                                return "원보 거래소 구매 수수료";
                            case "52":
                                return "장비 각인";
                            case "53":
                                return "용혼";
                            case "54":
                                return "성전 워프";
                            case "55":
                                return "命魂盒";
                            case "56":
                                return "교환 아이템 소모";
                            case "57":
                                return "진영 수정";
                            case "58":
                                return "탈 것 번식";
                            case "59":
                                return "탈 것 등급 갱신";
                            case "60":
                                return "탈 것 자질 갱신";
                            case "61":
                                return "탈 것 선청 각성";
                            case "62":
                                return "탈 것 후천 각성";
                            case "63":
                                return "전직";
                            case "64":
                                return "연맹 오픈 소모";
                            case "65":
                                return "자동 사냥";
                            case "66":
                                return "길드 분배 시 경매 아이템 소모";
                            case "67":
                                return "길드 분배 시 경매 포기 반환";
                            case "68":
                                return "길드 분배 시 경매 실패 반환";
                            case "69":
                                return "길드 분배 시 길드 탈퇴 반환";
                            case "70":
                                return "길드 분배 시 시간 종료 반환";
                            case "71":
                                return "길드 분배 시 경매 수익 획득";
                            case "72":
                                return "탈 것 진화";
                            case "73":
                                return "神迹争夺报名消耗	";
                            case "74":
                                return "스킬 승급 소모";
                            case "76":
                                return "孝敬师傅";
                            case "77":
                                return "사제 아이템 제작";
                            case "79":
                                return "탈 것 경험치 이전";
                            case "80":
                                return "보석 정수 승급";
                            case "81":
                                return "补灵";
                            case "82":
                                return "장비 슬롯 오픈";
                            case "83":
                                return "보석 각인";
                            case "84":
                                return "보석 해제";
                            case "85":
                                return "보석 분해";
                            case "87":
                                return "스킹 승급";
                            case "88":
                                return "활력 구매";
                            case "89":
                                return "던전 카드 뒤집기";
                            case "90":
                                return "다이아로 골드 교환";
                            case "91":
                                return "골드로 실버 교환";
                            case "92":
                                return "던전 카드 뒤집기";
                            case "93":
                                return "길드 생성";
                            case "94":
                                return "보상 테이블 획득";
                            case "95":
                                return "누적 충전 보상";
                            case "96":
                                return "30일 출석";
                            default:
                                return value;

                        }
                    }
                    else
                    {
                        #region 中文
                        switch (value)
                        {
                            case "0":
                                return "仓库操作";
                            case "1":
                                return "玩家间交易";
                            case "2":
                                return "邮件附件";
                            case "3":
                                return "购买拍卖的物品";
                            case "4":
                                return "拾取";
                            case "5":
                                return "任务奖励";
                            case "6":
                                return "创建帮会";
                            case "7":
                                return "切换场景";
                            case "8":
                                return "GM用指令修改的";
                            case "9":
                                return "系统后台发奖等";
                            case "10":
                                return "NPC交易	";
                            case "11":
                                return "修理装备花费";
                            case "12":
                                return "创建家族";
                            case "13":
                                return "技能学习";
                            case "14":
                                return "法宝升星";
                            case "15":
                                return "家族捐钱";
                            case "16":
                                return "侍卫复活";
                            case "17":
                                return "答题得钱";
                            case "18":
                                return "装备精炼";
                            case "19":
                                return "装备幻化";
                            case "20":
                                return "宝石合成";
                            case "21":
                                return "装备重铸";
                            case "22":
                                return "接奖励任务扣除";
                            case "23":
                                return "装备鉴定";
                            case "24":
                                return "lgc道具包给的";
                            case "25	 ":
                                return "法宝升档";
                            case "26":
                                return "战场积分兑换";
                            case "27":
                                return "奖励兑换";
                            case "28":
                                return "邮资";
                            case "29":
                                return "任务奖励";
                            case "30":
                                return "法宝技能激活";
                            case "31":
                                return "拍卖行手续费";
                            case "32":
                                return "直接完成洪荒任务扣钱";
                            case "33":
                                return "刷新洪荒任务扣钱";
                            case "34":
                                return "刷新洪荒任务星级扣钱";
                            case "35":
                                return "LGC 操作";
                            case "36":
                                return "军团升级";
                            case "37":
                                return "天启活动";
                            case "38":
                                return "活动报名";
                            case "39":
                                return "押镖投保消耗";
                            case "40":
                                return "镖车升级消耗";
                            case "41":
                                return "帮会捐献";
                            case "43	 ":
                                return "切换守护星职业";
                            case "44":
                                return "装备精炼转移";
                            case "45":
                                return "法宝技能刷新";
                            case "46":
                                return "生活技能";
                            case "47":
                                return "离线经验";
                            case "48":
                                return "灵数猎手";
                            case "49":
                                return "元宝交易行挂单";
                            case "50":
                                return "元宝交易行挂单返还";
                            case "51":
                                return "元宝交易行购买手续费";
                            case "52":
                                return "装备铭刻";
                            case "53":
                                return "龙魂";
                            case "54":
                                return "城战传送";
                            case "55":
                                return "命魂盒";
                            case "56":
                                return "兑换物品消耗";
                            case "57":
                                return "修改联盟";
                            case "58":
                                return "坐骑繁殖";
                            case "59":
                                return "坐骑刷新星级";
                            case "60":
                                return "坐骑刷新资质比例";
                            case "61":
                                return "坐骑先天悟道";
                            case "62":
                                return "坐骑后天悟道";
                            case "63":
                                return "转职";
                            case "64":
                                return "联盟开启消耗";
                            case "65":
                                return "自动挂机";
                            case "66":
                                return "帮会分配时竞拍道具消耗";
                            case "67":
                                return "帮会分配时放弃竞拍返还";
                            case "68":
                                return "帮会分配时竞拍失败返还";
                            case "69":
                                return "帮会分配时离开帮会返还";
                            case "70":
                                return "帮会分配时时间结束返还";
                            case "71":
                                return "帮会分配时获得竞拍收益";
                            case "72":
                                return "坐骑转生";
                            case "73":
                                return "神迹争夺报名消耗	";
                            case "74":
                                return "技能进阶消耗";
                            case "76":
                                return "孝敬师傅";
                            case "77":
                                return "制作师徒道具";
                            case "79":
                                return "坐骑经验转移";
                            case "80":
                                return "宝石精华升级";
                            case "81":
                                return "补灵";
                            case "82":
                                return "装备打孔";
                            case "83":
                                return "宝石镶嵌";
                            case "84":
                                return "宝石拆除";
                            case "85":
                                return "宝石分解";
                            case "87":
                                return "技能升级";
                            case "88":
                                return "购买体力";
                            case "89":
                                return "副本翻牌";
                            case "90":
                                return "货币兑换钻石兑换非绑定金币";
                            case "91":
                                return "货币兑换非绑定金币兑换绑定金币";
                            case "92":
                                return "副本翻牌";
                            case "93":
                                return "创建公会";
                            case "94":
                                return "从奖励表创建";
                            case "95":
                                return "累积充值奖励";
                            case "96":
                                return "三十日签到";
                            default:
                                return value;
                        }
                        #endregion
                    }
                }
                catch (System.Exception ex)
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }
    }
}
