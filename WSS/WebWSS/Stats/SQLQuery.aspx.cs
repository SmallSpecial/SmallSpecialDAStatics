using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.Stats
{
    public partial class SQLQuery : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        DataTable dtctype;
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;

            if (!IsPostBack)
            {
                InitChatType();
                BindDdl1();
                DropDownListArea1_SelectedIndexChanged(null, null);
                BindDdlCtype();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            string sqlStr=tboxSQL.Text.Trim();
            if (sqlStr.Length == 0)
            {
                Common.MsgBox.Show(this, "SQL语句不能为空");
                return;
            }
            if (sqlStr.ToLower().IndexOf("t_user ") != -1 || sqlStr.ToLower().IndexOf("t_user]") != -1 || sqlStr.ToLower().EndsWith("t_user"))
            {
                Common.MsgBox.Show(this, "SQL语句不能包含用户表");
                return;
            }

            try
            {
                string bigzoneid = DropDownListArea1.SelectedValue.Split(',')[1];
                string zoneid = DropDownListArea2.SelectedValue.Split(',')[1];
                string dbtype = DropDownListCType.SelectedValue;
                if (dbtype == "1" || dbtype == "2" || dbtype == "4" || dbtype == "5" || dbtype == "8")
                {
                    zoneid = "-1";
                }
                int pcount = 0;

                SqlParameter[] parameters = {
					new SqlParameter("@BigZoneID", SqlDbType.Int),
					new SqlParameter("@ZoneID", SqlDbType.Int),
                    new SqlParameter("@DBType", SqlDbType.Int),
					new SqlParameter("@Query",SqlDbType.NVarChar),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PCount", SqlDbType.Int),
					};

                parameters[0].Value = bigzoneid;
                parameters[1].Value = zoneid;
                parameters[2].Value = DropDownListCType.SelectedValue;
                parameters[3].Value = tboxSQL.Text.Replace("'", "''");
                parameters[4].Value = lblPageIndex.Text;
                parameters[5].Value = GridView1.PageSize;
                parameters[6].Direction = ParameterDirection.Output;


                ds = DBHelperDigGameDB.RunProcedure("_Query_SQLCustom", parameters, "ds", 180);
                DataView myView = ds.Tables[0].DefaultView;
                pcount = (int)parameters[6].Value;

                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                    myView.AddNew();
                    lblCount.Text = "0";
                    lblPageCount.Text = "1";
                }
                else
                {
                    lblerro.Visible = false;
                    lblPageCount.Text = (pcount / Convert.ToInt32(GridView1.PageSize) == 0 ? 1 / Convert.ToInt32(GridView1.PageSize) : pcount / Convert.ToInt32(GridView1.PageSize)).ToString();
                    lblCount.Text = pcount.ToString();

                    lbtnF.Enabled = true;
                    lbtnP.Enabled = true;
                    lbtnN.Enabled = true;
                    lbtnE.Enabled = true;
                    if (lblPageIndex.Text == "1")
                    {
                        lbtnF.Enabled = false;
                        lbtnP.Enabled = false;
                    }
                    else if (lblPageIndex.Text == lblPageCount.Text)
                    {
                        lbtnN.Enabled = false;
                        lbtnE.Enabled = false;
                    }
                    tboxPageIndex.Text = lblPageIndex.Text;
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
                lblinfo.Text = ex.Message;
            }

        }


        private void InitChatType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CID", Type.GetType("System.String"));
            dt.Columns.Add("CValue", Type.GetType("System.String"));
            string[] ctypes = new string[] { "0|[MYSQL] GSLOG_DB", "3|[MYSQL] GSDATA_DB", "7|[MYSQL] GSPARA_DB", "6|[SQLSERVER] ZONEROLEDATADB", "1|[SQLSERVER] GAMELOGDB", "2|[SQLSERVER] GAMECOREDB", "5|[SQLSERVER] USERCOREDB", "8|[SQLSERVER] MAINDB", "4|[SQLSERVER] GSSDB" };
            foreach (String ctype in ctypes)
            {
                DataRow dr = dt.NewRow();
                dr["CID"] = ctype.Split('|')[0];
                dr["CValue"] = ctype.Split('|')[1];
                dt.Rows.Add(dr);
            }
            dtctype = dt;
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            bind();
        }


        private void BindDdlCtype()
        {
            try
            {
                //DataRow newdr = dtctype.NewRow();
                //newdr["CID"] = App_GlobalResources.Language.LblAllSelect;
                //newdr["CValue"] = App_GlobalResources.Language.LblAllSelect;
                //dtctype.Rows.InsertAt(newdr, 0);
                this.DropDownListCType.DataSource = dtctype;
                this.DropDownListCType.DataTextField = "CValue";
                this.DropDownListCType.DataValueField = "CID";
                this.DropDownListCType.DataBind();

            }
            catch (System.Exception ex)
            {
                DropDownListCType.Items.Clear();
                DropDownListCType.Items.Add(new ListItem(App_GlobalResources.Language.LblAllSelect, ""));
            }
        }

        public string GetCType(string cid)
        {
            try
            {
                DataRow dr = dtctype.Select("CID='" + cid + "'")[0];
                return dr["CValue"].ToString();
            }
            catch (System.Exception ex)
            {
                return cid;
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
                //DataRow newdr = ds.Tables[0].NewRow();
                //newdr["F_Name"] = App_GlobalResources.Language.LblAllBigZone;
                //newdr["F_ValueGame"] = "";
                //ds.Tables[0].Rows.InsertAt(newdr, 0);
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
                //DataRow newdr = ds.Tables[0].NewRow();
                //newdr["F_Name"] = App_GlobalResources.Language.LblAllZone;

                //newdr["F_ValueGame"] = "";
                //ds.Tables[0].Rows.InsertAt(newdr, 0);
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
            //DbHelperSQLP spg = new DbHelperSQLP();
            //spg.connectionString = ConnStrGSSDB;

            //string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = " + id + ") AND (F_IsUsed = 1)";

            //try
            //{
            //    ds = spg.Query(sql);
            //    //DataRow newdr = ds.Tables[0].NewRow();

            //    //newdr["F_Name"] = "所有战线";

            //    //newdr["F_ValueGame"] = "";
            //    //ds.Tables[0].Rows.InsertAt(newdr, 0);
            //    this.DropDownListArea3.DataSource = ds;
            //    this.DropDownListArea3.DataTextField = "F_Name";
            //    this.DropDownListArea3.DataValueField = "F_ValueGame";
            //    this.DropDownListArea3.DataBind();

            //}
            //catch (System.Exception ex)
            //{
            //    DropDownListArea3.Items.Clear();
            //    DropDownListArea3.Items.Add(new ListItem("所有战线", ""));
            //}
        }

        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");
            }

        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = "1";
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
            lblPageIndex.Text = "1";
            bind();
        }

        protected void lbtnP_Click(object sender, EventArgs e)
        {
            if (lblPageIndex.Text != "1")
            {
                lblPageIndex.Text = (Convert.ToInt32(lblPageIndex.Text) - 1).ToString();
                bind();
            }
        }

        protected void lbtnN_Click(object sender, EventArgs e)
        {
            if (lblPageIndex.Text != lblPageCount.Text)
            {
                lblPageIndex.Text = (Convert.ToInt32(lblPageIndex.Text) + 1).ToString();
                bind();
            }
        }

        protected void lbtnE_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = lblPageCount.Text;
            bind();
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pindex = Convert.ToInt32(tboxPageIndex.Text);
                if (pindex > 0 && pindex <= Convert.ToInt32(lblPageCount.Text))
                {
                    lblPageIndex.Text = Convert.ToInt32(tboxPageIndex.Text).ToString();
                    bind();
                }

            }
            catch (System.Exception ex)
            {

            }
        }


    }
}
