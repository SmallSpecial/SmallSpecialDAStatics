using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.Stats
{
    public partial class ChatQuery_Hours : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        DataTable dtctype;
        protected void Page_Load(object sender, EventArgs e)
        {
            //加入当月日期按钮控件
            for (int i = 0; i <= 32; i++)
            {
                Button btn = new Button();
                btn.ID = "btndateselect" + i;
                btn.Text = i.ToString().PadLeft(2, '0').Replace("00", "<<").Replace("32", ">>");
                btn.Visible = false;
                btn.Click += new EventHandler(btn_Click);
                DateSelect.Controls.Add(btn);
            }
          
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            InitChatType();
            if (!IsPostBack)
            {
                txtHourse.Text = (DateTime.Now.Hour - 1) + "";
                tboxTimeB.Text = DateTime.Now.AddDays(-1).ToString(SmallDateTimeFormat)  ;
                tboxTimeE.Text = DateTime.Now.AddDays(-1).ToString(SmallDateTimeFormat)  ;
                if (Request.QueryString["cid"] != null)
                {
                    tboxCID.Text = Request.QueryString["cid"].ToString();
                }
                if (Request.QueryString["date"] != null)
                {
                    tboxTimeB.Text = Request.QueryString["date"].ToString();
                }
                BindDdl1();
                if (Request.QueryString["bigzone"] != null)
                {
                    DropDownListArea1.SelectedValue = Request.QueryString["bigzone"].ToString();
                    DropDownListArea1_SelectedIndexChanged(null, null);
                    if (Request.QueryString["zone"] != null)
                    {
                        DropDownListArea2.SelectedValue = Request.QueryString["zone"].ToString();
                    }
                }
                else
                {
                    DropDownListArea1_SelectedIndexChanged(null, null);
                }



                BindDdlCtype();
                bind();
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
            //if (Convert.ToDateTime(tboxTimeB.Text) > DateTime.Now.AddDays(-1))
            //{
            //    tboxTimeB.Text = DateTime.Now.AddDays(-1).ToString(SmallDateTimeFormat)  ;
            //}

            //设置日期按钮状态
            string dates = Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-01");
            DateTime dtb = Convert.ToDateTime(dates);
            DateTime dte = dtb.AddMonths(1).AddDays(-1);
            for (int i = 0; i <= 32; i++)
            {
                Control ctl = DateSelect.FindControl("btndateselect" + i);
                if (ctl != null && ctl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctl;
                    if (dtb.AddDays(i - 1) <= DateTime.Now && (i <= dte.Day || i == 32))
                    {
                        btn.Visible = true;
                        if (btn.Text == Convert.ToDateTime(tboxTimeB.Text).ToString("dd"))
                        {
                            btn.CssClass = "buttonblueo";
                            btn.Enabled = false;
                        }
                        else
                        {
                            btn.CssClass = "buttonblue";
                            btn.Enabled = true;
                        }
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }
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


            DateTime searchdateB = DateTime.Now.AddDays(-1);
            DateTime searchdateE = DateTime.Now;



            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat)   + " ";

            string sqlwhere = string.Format(@"SELECT UID,CID,OP_BAK,count(1) AS RCOUNT FROM {0:yyyy_MM_dd}_chat_log ", searchdateB);


            sqlwhere += " where (para_1 =3 or para_1 =4)";
            if (tboxUID.Text.Trim().Length > 0)
            {

                sqlwhere += @" and UID =" + tboxUID.Text.Replace("'", "") + "";
            }
            if (tboxCID.Text.Trim().Length > 0)
            {

                sqlwhere += @" and CID =" + tboxCID.Text.Replace("'", "") + "";
            }
            if (DropDownListCType.SelectedIndex > 0)
            {
                sqlwhere += @" and PARA_1 = " + DropDownListCType.SelectedValue + "";
            }
            if (tboxContent.Text.Trim().Length > 0)
            {
                string content = tboxContent.Text.Replace("'", "");

                content = TransEn(content);

                sqlwhere += @" and OP_BAK like ''%" + content + "%''";
            }

            if (txtHourse.Text.Trim().Length > 0)
            {
                int hourse = 0;
                if (int.TryParse(txtHourse.Text, out hourse))
                {

                    sqlwhere += @" and hour(OP_TIME) =" + hourse;
                }

            }


            sqlwhere += @" group by CID,OP_BAK HAVING count(1)>3 order by count(1) DESC";

            try
            {
                int pcount = 0;
                string bigzoneid = DropDownListArea1.SelectedValue.Split(',')[1];
                string zoneid = DropDownListArea2.SelectedValue.Split(',')[1];

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
                parameters[2].Value = 0;//gslog_db
                parameters[3].Value = sqlwhere.Replace("'", "''");
                parameters[4].Value = 1;
                parameters[5].Value = 1000;
                parameters[6].Direction = ParameterDirection.Output;

                ds = DBHelperDigGameDB.RunProcedure("_Query_SQLCustom", parameters, "ds", 180);
                DataView myView = ds.Tables[0].DefaultView;
                pcount = (int)parameters[6].Value;

                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                    myView.AddNew();

                    lblCount.Text = pcount.ToString();
                    lblPageCount.Text = (pcount % GridView1.PageSize == 0 ? pcount / GridView1.PageSize : pcount / GridView1.PageSize + 1).ToString();
                }
                else
                {
                    lblerro.Visible = false;
                    lblPageCount.Text = (pcount % GridView1.PageSize == 0 ? pcount / GridView1.PageSize : pcount / GridView1.PageSize + 1).ToString();
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

        public string TransEn(string value)
        {
            if (lblEncoding.Visible)
            {
                return TranG2I(value);
            }
            else
            {
                return value;
            }
        }

        public string TransDe(string value)
        {
            if (lblDecoding.Visible)
            {
                return TranI2G(value);
            }
            else
            {
                return value;
            }
        }


        //字符集转换   
        public string TranI2G(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
                gb2312 = System.Text.Encoding.GetEncoding("gbk");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        //字符集转换   
        public string TranG2I(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("gbk");
                gb2312 = System.Text.Encoding.GetEncoding("iso8859-1");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        //得到文本名称
        public string GetTextName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT ( cast(F_ExcelID as varchar(9)) +' '+F_Name+' '+F_Type+' '+F_TypeP ) as F_Name  FROM T_BaseGameName with(nolock) WHERE (F_ExcelID = " + value + ") ";
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }
        //得到角色名称
        public string GetRoleName(string bigzone, string roleid)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT  F_RoleName FROM T_BaseRoleCreate with(nolock) WHERE (F_BigZoneID =" + bigzone + ") AND (F_RoleID = " + roleid + ")  ";
                return string.Format("({0}) {1}", roleid, spg.GetSingle(sql));
            }
            catch (System.Exception ex)
            {
                return roleid;
            }
        }

        private void InitChatType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CID", Type.GetType("System.String"));
            dt.Columns.Add("CValue", Type.GetType("System.String"));
            string[] ctypes = new string[] { "1|当前", "2|队伍", "3|大喊", "4|私聊", "5|离线信息", "6|事件信息", "7|游戏信息(拾取)", "8|消息球", "9|GM", "10|NPC", "11|场景", "12|军团", "13|服务器喊话", "14|战区喊话", "15|金号角", "16|银号角", "17|GM战区宣言", "18|全部频道", "19|自定义频道", "20|超级宣言", "21|职业频道", "22|无效频道" };
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
            try
            {
                Button btn = (Button)sender;
                string date = Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-") + btn.Text;
                if (date.IndexOf("<<") >= 0)
                {
                    date = Convert.ToDateTime(date.Replace("<<", "01")).AddDays(-1).ToString("yyyy-MM-dd");
                }
                if (date.IndexOf(">>") >= 0)
                {
                    date = Convert.ToDateTime(date.Replace(">>", "01")).AddMonths(1).ToString("yyyy-MM-dd");
                }
                tboxTimeB.Text = date;
                lblPageIndex.Text = "1";
                bind();
            }
            catch (System.Exception ex)
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
            }

        }


        private void BindDdlCtype()
        {
            try
            {
                DataRow newdr = dtctype.NewRow();
                newdr["CID"] = App_GlobalResources.Language.LblAllSelect;
                newdr["CValue"] = App_GlobalResources.Language.LblAllSelect;
                dtctype.Rows.InsertAt(newdr, 0);
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
