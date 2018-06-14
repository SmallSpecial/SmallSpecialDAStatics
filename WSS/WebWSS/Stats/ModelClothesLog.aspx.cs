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
    public partial class ModelClothesLog : Admin_Page
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
                BindDdl121();
                BindDdl2("100001");
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

            if (!string.IsNullOrEmpty(ddlOpid.SelectedValue))
            {
                sqlwhere += @" and OPID=" + ddlOpid.SelectedValue;
            }
            if (!string.IsNullOrEmpty(txtPARA_1.Text.Trim()))
            {
                sqlwhere += @" and PARA_1=" + txtPARA_1.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtPARA_2.Text.Trim()))
            {
                sqlwhere += @" and PARA_2=" + txtPARA_2.Text.Trim();
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
            sql = "SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT PARA_2,SUBSTRING_INDEX(OP_BAK,\"	\",3) OP_TYPE,COUNT(SUBSTRING_INDEX(OP_BAK,\"	\",3)) Number FROM " + searchdateB.ToString("yyyy_MM_dd") + "_other_log  WHERE OPID = 50144 AND SUBSTRING_INDEX(OP_BAK,\"	\",1)=1 GROUP BY PARA_2,OP_TYPE')";

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

        public void BindDdl121()
        {
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
            sql = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT distinct OPID FROM " + Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy_MM_dd") + "_item_log')";

            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();
                this.ddlOpid.DataSource = ds;
                this.ddlOpid.DataTextField = "OPID";
                this.ddlOpid.DataValueField = "OPID";
                this.ddlOpid.DataBind();
                ddlOpid.Items.Insert(0, new ListItem("", ""));
            }
            catch (System.Exception ex)
            {
                ddlOpid.Items.Clear();
                ddlOpid.Items.Add(new ListItem("", ""));
            }
        }

        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            bind();
            BindDdl121();
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

            if (!string.IsNullOrEmpty(ddlOpid.SelectedValue))
            {
                sqlwhere += @" and OPID=" + ddlOpid.SelectedValue;
            }
            if (!string.IsNullOrEmpty(txtPARA_1.Text.Trim()))
            {
                sqlwhere += @" and PARA_1=" + txtPARA_1.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtPARA_2.Text.Trim()))
            {
                sqlwhere += @" and PARA_2=" + txtPARA_2.Text.Trim();
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
            sql = "SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT CONCAT(PARA_2,\"#\") 职业,\"激活\",SUBSTRING_INDEX(OP_BAK,\"	\",3) 时装分类,SUBSTRING_INDEX(OP_BAK,\"	\",3) 时装对应部位,COUNT(SUBSTRING_INDEX(OP_BAK,\"	\",3)) 数量 FROM " + searchdateB.ToString("yyyy_MM_dd") + "_other_log  WHERE OPID = 50144 AND SUBSTRING_INDEX(OP_BAK,\"	\",1)=1 GROUP BY PARA_2,SUBSTRING_INDEX(OP_BAK,\"	\",3)')";

            ds = DBHelperDigGameDB.Query(sql);

            if(ds!=null)
            {
                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    ds.Tables[0].Rows[i][0] = GetPro1(ds.Tables[0].Rows[i][0].ToString());
                    ds.Tables[0].Rows[i][2] = GetOPTYPE(ds.Tables[0].Rows[i][2].ToString());
                    ds.Tables[0].Rows[i][3] = GetOPTYPEITEMS(ds.Tables[0].Rows[i][3].ToString());
                }
            }
            ExportExcelHelper.ExportDataSet(ds);
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

        //得到角色名称
        public string GetRoleName(string bigzone, string roleid)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT  F_RoleName FROM [LKSV_2_GameCoreDB_0_1].Gamecoredb.dbo.T_RoleCreate with(nolock) WHERE (F_BigZoneID =" + bigzone + ") AND (F_RoleID = " + roleid + ")  ";
                return string.Format("({0}) {1}", roleid, spg.GetSingle(sql));
            }
            catch (System.Exception ex)
            {
                return roleid;
            }
        }
        public string GetPro(string value)
        {
            try
            {
                if (PageLanguage == "ko-kr")
                {
                    switch (value)
                    {
                        case "1":
                            return "암살자";
                        case "2":
                            return "마법사";
                        case "3":
                            return "용전사";
                        case "4":
                            return "소환사";
                        default:
                            return value;
                    }
                }
                else
                {
                    switch (value)
                    {
                        case "1":
                            return "刺客";
                        case "2":
                            return "魔法师";
                        case "3":
                            return "战士";
                        case "4":
                            return "萝莉";
                        default:
                            return value;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        public string GetPro1(string value)
        {
            try
            {
                if (PageLanguage == "ko-kr")
                {
                    switch (value)
                    {
                        case "1#":
                            return "암살자";
                        case "2#":
                            return "마법사";
                        case "3#":
                            return "용전사";
                        case "4#":
                            return "소환사";
                        default:
                            return value;
                    }
                }
                else
                {
                    switch (value)
                    {
                        case "1#":
                            return "刺客";
                        case "2#":
                            return "魔法师";
                        case "3#":
                            return "战士";
                        case "4#":
                            return "萝莉";
                        default:
                            return value;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        public string GetOPTYPE(string value)
        {
            try
            {
                string[] items = value.Split(' ');
                if (items.Length == 1)
                {
                    items = value.Split('\t');
                }
                if (PageLanguage == "ko-kr")
                {
                    switch (items[1])
                    {
                        case "0":
                            return "무효";
                        case "1":
                            return "헤어";
                        case "2":
                            return "얼굴형";
                        case "3":
                            return "의상";
                        case "4":
                            return "날개";
                        case "5":
                            return "무기";
                        default:
                            return items[1];
                    }
                }
                else
                {
                    switch (items[1])
                    {
                        case "0":
                            return "无效";
                        case "1":
                            return "发型";
                        case "2":
                            return "脸型";
                        case "3":
                            return "服装";
                        case "4":
                            return "翅膀";
                        case "5":
                            return "武器";
                        default:
                            return items[1];
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        public string GetOPTYPEITEMS(string value)
        {
            try
            {
                string[] items = value.Split(' ');
                if (items.Length == 1)
                {
                    items = value.Split('\t');
                }
                if (PageLanguage == "ko-kr")
                {
                    switch(items[2])
                    {
                        case "430101":
                            return "소환사-수영복의상1";
                        case "410101":
                            return "소환사-수영복헤어1";
                        case "450101":
                            return "소환사-수영복무기1";
                        case "450105":
                            return "소환사-무기5";
                        case "450102":
                            return "소환사-무기2";
                        case "430104":
                            return "소환사-할로윈의상4";
                        case "450104":
                            return "소환사-할로윈무기4";
                        case "410401":
                            return "소환사-할로윈헤어4";
                        case "440106":
                            return "소환사-핏빛 마신 날개6";
                        case "420105":
                            return "소환사-얼굴형5";
                        case "420104":
                            return "소환사-얼굴형4";
                        case "420103":
                            return "소환사-얼굴형3";
                        case "420102":
                            return "소환사-얼굴형2";
                        case "420101":
                            return "소환사-얼굴형1";
                        case "440102":
                            return "소환사-정령의 날개2";
                        case "440103":
                            return "소환사-화염의 날개3";
                        case "430102":
                            return "소환사-메이드의상2";
                        case "410201":
                            return "소환사-메이드헤어2";
                        case "440105":
                            return "소환사-얼음 깃털 날개5";
                        case "430103":
                            return "소환사-폭주족의상3";
                        case "450103":
                            return "소환사-폭주족무기3";
                        case "410301":
                            return "소환사-폭주족헤어3";
                        case "440101":
                            return "소환사-질서의 날개1";
                        case "440104":
                            return "소환사-환상 계약 날개4";
                        case "330101":
                            return "용전사-수영복의상1";
                        case "310101":
                            return "용전사-수영복헤어1";
                        case "350101":
                            return "용전사-수영복무기1";
                        case "350105":
                            return "용전사-무기5";
                        case "350102":
                            return "용전사-무기2";
                        case "330104":
                            return "용전사-할로윈의상4";
                        case "350104":
                            return "용전사-할로윈무기4";
                        case "310104":
                            return "용전사-할로윈헤어4";
                        case "340106":
                            return "용전사-핏빛 마신 날개6";
                        case "320105":
                            return "용전사-얼굴형5";
                        case "320104":
                            return "용전사-얼굴형4";
                        case "320103":
                            return "용전사-얼굴형3";
                        case "320102":
                            return "용전사-얼굴형2";
                        case "320101":
                            return "용전사-얼굴형1";
                        case "340102":
                            return "용전사-정령의 날개2";
                        case "340103":
                            return "용전사-화염의 날개3";
                        case "330102":
                            return "용전사-메이드의상2";
                        case "310102":
                            return "용전사-메이드헤어2";
                        case "340105":
                            return "용전사-얼음 깃털 날개5";
                        case "350103":
                            return "용전사-폭주족무기3";
                        case "310103":
                            return "용전사-폭주족헤어3";
                        case "340101":
                            return "용전사-질서의 날개1";
                        case "340104":
                            return "용전사-환상 계약 날개4";
                        case "330103":
                            return "용전사_폭주족의상3";
                        case "230101":
                            return "마법사-수영복의상1";
                        case "210101":
                            return "마법사-수영복헤어1";
                        case "250501":
                            return "마법사-수영복무기1";
                        case "250505":
                            return "마법사-무기5";
                        case "250502":
                            return "마법사-무기2";
                        case "230104":
                            return "마법사-할로윈의상4";
                        case "250504":
                            return "마법사-할로윈무기4";
                        case "210401":
                            return "마법사-할로윈헤어4";
                        case "240106":
                            return "마법사-핏빛 마신 날개6";
                        case "220105":
                            return "마법사-얼굴형5";
                        case "220104":
                            return "마법사-얼굴형4";
                        case "220103":
                            return "마법사-얼굴형3";
                        case "220102":
                            return "마법사-얼굴형2";
                        case "220101":
                            return "마법사-얼굴형1";
                        case "240102":
                            return "마법사-정령의 날개2";
                        case "240103":
                            return "마법사-화염의 날개3";
                        case "230102":
                            return "마법사-메이드의상2";
                        case "210201":
                            return "마법사-메이드헤어2";
                        case "240105":
                            return "마법사-얼음 깃털 날개5";
                        case "230103":
                            return "마법사-폭주족의상3";
                        case "250503":
                            return "마법사-폭주족무기3";
                        case "210301":
                            return "마법사-폭주족헤어3";
                        case "240101":
                            return "마법사-질서의 날개1";
                        case "240104":
                            return "마법사-환상 계약 날개4";
                        case "130101":
                            return "암살자-수영복의상1";
                        case "110101":
                            return "암살자-수영복헤어1";
                        case "150101":
                            return "암살자-수영복무기1";
                        case "150105":
                            return "암살자-무기5";
                        case "150102":
                            return "암살자-무기2";
                        case "130104":
                            return "암살자-할로윈의상4";
                        case "150104":
                            return "암살자-할로윈무기4";
                        case "110401":
                            return "암살자-할로윈헤어4";
                        case "140106":
                            return "암살자-핏빛 마신 날개6";
                        case "120105":
                            return "암살자-얼굴형5";
                        case "120104":
                            return "암살자-얼굴형4";
                        case "120103":
                            return "암살자-얼굴형3";
                        case "120102":
                            return "암살자-얼굴형2";
                        case "120101":
                            return "암살자-얼굴형1";
                        case "140102":
                            return "암살자-정령의 날개2";
                        case "140103":
                            return "암살자-화염의 날개3";
                        case "130102":
                            return "암살자-메이드의상2";
                        case "110201":
                            return "암살자-메이드헤어2";
                        case "140105":
                            return "암살자-얼음 깃털 날개5";
                        case "130103":
                            return "암살자-폭주족의상3";
                        case "150103":
                            return "암살자-폭주족무기3";
                        case "110301":
                            return "암살자-폭주족헤어3";
                        case "140101":
                            return "암살자-질서의 날개1";
                        case "140104":
                            return "암살자-환상 계약 날개4";
                        default:
                            return items[2];
                    }
                }
                else
                {
                    #region 中文
                    switch (items[2])
                    {
                        case "430101":
                            return "召唤-泳装衣服1";
                        case "410101":
                            return "召唤-泳装发型1";
                        case "450101":
                            return "召唤-夏日武器1";
                        case "450105":
                            return "召唤-武器5";
                        case "450102":
                            return "召唤-武器2";
                        case "430104":
                            return "召唤-万圣节衣服4";
                        case "450104":
                            return "召唤-万圣节武器4";
                        case "410401":
                            return "召唤-万圣节发型4";
                        case "440106":
                            return "召唤-破碎翅膀6";
                        case "420105":
                            return "召唤-脸型5";
                        case "420104":
                            return "召唤-脸型4";
                        case "420103":
                            return "召唤-脸型3";
                        case "420102":
                            return "召唤-脸型2";
                        case "420101":
                            return "召唤-脸型1";
                        case "440102":
                            return "召唤-金羽毛翅膀2";
                        case "440103":
                            return "召唤-火焰翅膀3";
                        case "430102":
                            return "召唤-管家衣服2";
                        case "410201":
                            return "召唤-管家发型2";
                        case "440105":
                            return "召唤-冰霜翅膀5";
                        case "430103":
                            return "召唤-暴走衣服3";
                        case "450103":
                            return "召唤-暴走武器3";
                        case "410301":
                            return "召唤-暴走发型3";
                        case "440101":
                            return "召唤-白羽毛翅膀1";
                        case "440104":
                            return "召唤-暗影翅膀4";
                        case "330101":
                            return "龙战士-泳装衣服1";
                        case "310101":
                            return "龙战士-泳装发型1";
                        case "350101":
                            return "龙战士-夏日武器1";
                        case "350105":
                            return "龙战士-武器5";
                        case "350102":
                            return "龙战士-武器2";
                        case "330104":
                            return "龙战士-万圣节衣服4";
                        case "350104":
                            return "龙战士-万圣节武器4";
                        case "310104":
                            return "龙战士-万圣节发型4";
                        case "340106":
                            return "龙战士-破碎翅膀6";
                        case "320105":
                            return "龙战士-脸型5";
                        case "320104":
                            return "龙战士-脸型4";
                        case "320103":
                            return "龙战士-脸型3";
                        case "320102":
                            return "龙战士-脸型2";
                        case "320101":
                            return "龙战士-脸型1";
                        case "340102":
                            return "龙战士-金羽毛翅膀2";
                        case "340103":
                            return "龙战士-火焰翅膀3";
                        case "330102":
                            return "龙战士-管家衣服2";
                        case "310102":
                            return "龙战士-管家发型2";
                        case "340105":
                            return "龙战士-冰霜翅膀5";
                        case "350103":
                            return "龙战士-暴走武器3";
                        case "310103":
                            return "龙战士-暴走发型3";
                        case "340101":
                            return "龙战士-白羽毛翅膀1";
                        case "340104":
                            return "龙战士-暗影翅膀4";
                        case "330103":
                            return "龙战士_暴走衣服3";
                        case "230101":
                            return "法师-泳装衣服1";
                        case "210101":
                            return "法师-泳装发型1";
                        case "250501":
                            return "法师-夏日武器1";
                        case "250505":
                            return "法师-武器5";
                        case "250502":
                            return "法师-武器2";
                        case "230104":
                            return "法师-万圣节衣服4";
                        case "250504":
                            return "法师-万圣节武器4";
                        case "210401":
                            return "法师-万圣节发型4";
                        case "240106":
                            return "法师-破碎翅膀6";
                        case "220105":
                            return "法师-脸型5";
                        case "220104":
                            return "法师-脸型4";
                        case "220103":
                            return "法师-脸型3";
                        case "220102":
                            return "法师-脸型2";
                        case "220101":
                            return "法师-脸型1";
                        case "240102":
                            return "法师-金羽毛翅膀2";
                        case "240103":
                            return "法师-火焰翅膀3";
                        case "230102":
                            return "法师-管家衣服2";
                        case "210201":
                            return "法师-管家发型2";
                        case "240105":
                            return "法师-冰霜翅膀5";
                        case "230103":
                            return "法师-暴走衣服3";
                        case "250503":
                            return "法师-暴走武器3";
                        case "210301":
                            return "法师-暴走发型3";
                        case "240101":
                            return "法师-白羽毛翅膀1";
                        case "240104":
                            return "法师-暗影翅膀4";
                        case "130101":
                            return "刺客-泳装衣服1";
                        case "110101":
                            return "刺客-泳装发型1";
                        case "150101":
                            return "刺客-夏日武器1";
                        case "150105":
                            return "刺客-武器5";
                        case "150102":
                            return "刺客-武器2";
                        case "130104":
                            return "刺客-万圣节衣服4";
                        case "150104":
                            return "刺客-万圣节武器4";
                        case "110401":
                            return "刺客-万圣节发型4";
                        case "140106":
                            return "刺客-破碎翅膀6";
                        case "120105":
                            return "刺客-脸型5";
                        case "120104":
                            return "刺客-脸型4";
                        case "120103":
                            return "刺客-脸型3";
                        case "120102":
                            return "刺客-脸型2";
                        case "120101":
                            return "刺客-脸型1";
                        case "140102":
                            return "刺客-金羽毛翅膀2";
                        case "140103":
                            return "刺客-火焰翅膀3";
                        case "130102":
                            return "刺客-管家衣服2";
                        case "110201":
                            return "刺客-管家发型2";
                        case "140105":
                            return "刺客-冰霜翅膀5";
                        case "130103":
                            return "刺客-暴走衣服3";
                        case "150103":
                            return "刺客-暴走武器3";
                        case "110301":
                            return "刺客-暴走发型3";
                        case "140101":
                            return "刺客-白羽毛翅膀1";
                        case "140104":
                            return "刺客-暗影翅膀4";
                        default:
                            return items[2];
                    }
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
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
