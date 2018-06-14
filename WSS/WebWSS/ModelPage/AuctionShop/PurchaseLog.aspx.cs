using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.AuctionShop
{
    public partial class PurchaseLog : Admin_Page
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

            string sqlwhere = " where 1=1 and OPID=50126";
            if (!string.IsNullOrEmpty(tbRoleId.Text.Trim()))
            {
                sqlwhere += @" and CID=" + tbRoleId.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtPARA_1.Text.Trim()))
            {
                sqlwhere += @" and PARA_1=" + txtPARA_1.Text.Trim();
            }
            if (!string.IsNullOrEmpty(ddlOPID.SelectedValue))
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
            string sqlCount = "";
            sql = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT * FROM " + searchdateB.ToString("yyyy_MM_dd") + "_other_log " + sqlwhere + " limit " + (Convert.ToInt32(lblcurPage.Text) - 1) * 20 + ",20')";
            sqlCount = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT count(*) FROM " + searchdateB.ToString("yyyy_MM_dd") + "_other_log " + sqlwhere + " ')";
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

            string sqlwhere = " where 1=1 and OPID=50126";
            if (!string.IsNullOrEmpty(tbRoleId.Text.Trim()))
            {
                sqlwhere += @" and CID=" + tbRoleId.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtPARA_1.Text.Trim()))
            {
                sqlwhere += @" and PARA_1=" + txtPARA_1.Text.Trim();
            }
            if (!string.IsNullOrEmpty(ddlOPID.SelectedValue))
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

            sql = @"SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_" + bigZone + "_" + battleZone + "],'SELECT UID 用户编号,CID 角色编号,CID 角色名称,PARA_1 角色等级,PARA_2 角色职业,OP_BAK 备注,OP_TIME 时间 FROM " + searchdateB.ToString("yyyy_MM_dd") + "_other_log " + sqlwhere + "')";


            ds = DBHelperDigGameDB.Query(sql);

            DataTable dt = new DataTable();
            dt.Columns.Add("用户编号", System.Type.GetType("System.String"));
            dt.Columns.Add("角色编号", System.Type.GetType("System.String"));
            dt.Columns.Add("角色名称", System.Type.GetType("System.String"));
            dt.Columns.Add("角色等级", System.Type.GetType("System.String"));
            dt.Columns.Add("角色职业", System.Type.GetType("System.String"));
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
                        ds.Tables[0].Rows[i][3],
                        GetPro(ds.Tables[0].Rows[i][4].ToString()),
                        GetOPBakFormate(ds.Tables[0].Rows[i][5].ToString()),
                        ds.Tables[0].Rows[i][6]
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

                string sql = @"SELECT F_RoleName FROM T_BaseRoleCreate with(nolock) where F_RoleID=" + value + "";
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
        public string GetOPBakFormate(string value)
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
                    return String.Format("상품 GlobalID:{0} 상품 Key:{1} 상품 Excel ID:{2} 상품 Number:{3} 상품 판매 재화 유형：{4} 아이템 구매 소비 골드：{5} 진영 작업 시간：{6}", items[0], items[1], items[2], items[3], items[4], items[5], items[6]);
                }
                else
                {
                    return String.Format("商品GlobalID:{0} 商品唯一Key:{1} 商品ExcelID:{2} 商品Number:{3} 商品出售货币类型：{4} 购买道具花费的金钱：{5} 阵营操作时间：{6}", items[0], items[1], items[2], items[3], items[4], items[5], items[6]);
                }
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }
    }
}