using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Collections.Generic;

namespace WebWSS.Stats
{
    public partial class ChatTypeNumD : Admin_Page
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
            for (int i = 1; i <= 12; i++)
            {
                Button btn = new Button();
                btn.ID = "btndateselect" + i;
                btn.Visible = false;
                btn.Click += new EventHandler(btn_Click);
                DateSelect.Controls.Add(btn);
            }
            InitChatType();
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            if (!IsPostBack)
            {
                tboxTimeB.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                tboxTimeE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindDdl1();
                bind();
            }
            ControlOutFile1.ControlOut = GridView1;
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
            //设置日期按钮状态
            string dates = Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-01-01");
            DateTime dtb = Convert.ToDateTime(dates);

            for (int i = 1; i <= 12; i++)
            {
                Control ctl = DateSelect.FindControl("btndateselect" + i);
                if (ctl != null && ctl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctl;
                    if (dtb.AddMonths(i - 1) <= DateTime.Now)
                    {
                        btn.Text = dtb.AddMonths(i - 1).ToString("yyyy-MM");
                        btn.Visible = true;

                        DateTime dttb = Convert.ToDateTime(btn.Text);
                        DateTime dtte = dttb.AddMonths(1) > DateTime.Now ? DateTime.Now : dttb.AddMonths(1).AddDays(-1);

                        if (dttb.ToString("yyyy-MM-dd") == tboxTimeB.Text && dtte.ToString("yyyy-MM-dd") == tboxTimeE.Text)
                        {
                            btn.CssClass = "buttonblo";
                            btn.Enabled = false;
                        }
                        else
                        {
                            btn.CssClass = "buttonbl";
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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat)   +  " "+ App_GlobalResources.Language.LblTo+" "  + searchdateE.ToString(SmallDateTimeFormat)  ;

            string sqlwhere = " where 1=1 ";
            sqlwhere += @" and  F_Date>='" + searchdateB.ToString(SmallDateTimeFormat) + "' and F_Date<='" + searchdateE.ToString(SmallDateTimeFormat) + "' ";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                sqlwhere += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sqlwhere += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }

            string sql = "";
            sql = @"SELECT Convert(varchar(10),F_Date,120) as F_Date,F_ChatType, Sum(F_Num) as F_Num FROM T_ChatTypeNum " + sqlwhere + @" group by F_Date,F_ChatType";

            try
            {
                ds = DBHelperDigGameDB.Query(sql);


                List<string> lines = new List<string>();
                DataTable dtt = new DataTable();
                dtt.Columns.Add("F_Date", typeof(string));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string line = dr["F_ChatType"].ToString().Trim();
                    string time = dr["F_Date"].ToString();
                    string lineName = GetChatTypeName(line);
                    if (!lines.Contains(lineName) && !string.IsNullOrEmpty(lineName))
                    {
                        lines.Add(lineName);
                    }
                    if (dtt.Select("F_Date='" + time + "'").Length == 0)
                    {
                        DataRow drt = dtt.NewRow();
                        drt["F_Date"] = time;
                        dtt.Rows.Add(drt);
                    }

                }


                while (GridView1.Columns.Count > 1)
                {
                    GridView1.Columns.RemoveAt(1);
                }

                foreach (string linename in lines)
                {
                    BoundField col = new BoundField();
                    col.HeaderText = linename;
                    GridView1.Columns.Add(col);
                }
                BoundField coli = new BoundField();
                coli.HeaderText =App_GlobalResources.Language.LblSum;
                GridView1.Columns.Add(coli);



                DataView myView = dtt.DefaultView;
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

                if (ControlChart1.Visible == true)
                {
                    ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 1);
                }
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

                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 1);
            }
        }

        private void InitChatType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CID", Type.GetType("System.String"));
            dt.Columns.Add("CValue", Type.GetType("System.String"));
            string[] ctypes = new string[] { "1|" + App_GlobalResources.Language.LblNow, "2|" + App_GlobalResources.Language.LblTeam, "3|" + App_GlobalResources.Language.LblShout, "4|" + App_GlobalResources.Language.LblPrivateChat, "5|" + App_GlobalResources.Language.LblOfflineInfo, "6|" + App_GlobalResources.Language.LblEventInfo, "7|" + App_GlobalResources.Language.LblGameInfoPickup, "8|" + App_GlobalResources.Language.LblMessageBall, "9|GM", "10|NPC", "11|" + App_GlobalResources.Language.LblScene, "12|" + App_GlobalResources.Language.LbkLegion, "13|" + App_GlobalResources.Language.LblServiceCall, "14|" + App_GlobalResources.Language.LblZoneCall, "15|" + App_GlobalResources.Language.LblGoldenHorn, "16|" + App_GlobalResources.Language.LblSilverHorn, "17|" + App_GlobalResources.Language.LblGMTheaterDeclaration, "18|" + App_GlobalResources.Language.LblAllChannels, "19|" + App_GlobalResources.Language.LblSelfDefineChannel, "20|" + App_GlobalResources.Language.LblSuperDeclaration, "21|" + App_GlobalResources.Language.LblVoctionChannel, "22|" + App_GlobalResources.Language.LblInvalidChannel };
            foreach (String ctype in ctypes)
            {
                DataRow dr = dt.NewRow();
                dr["CID"] = ctype.Split('|')[0];
                dr["CValue"] = ctype.Split('|')[1];
                dt.Rows.Add(dr);
            }
            dtctype = dt;
        }

        /// <summary>
        /// 得到频道名字
        /// </summary>
        public string GetChatTypeName(string id)
        {
            try
            {
                string name = dtctype.Select("CID='" + id + "' ")[0]["CValue"].ToString();
                return string.IsNullOrEmpty(name) ? id : name;
            }
            catch
            {
                return id;
            }
        }

        /// <summary>
        /// 得到频道数量
        /// </summary>
        public string GetChatTypeNum(string date, string name)
        {
            try
            {
                string cid = dtctype.Select("CValue='" + name + "' ")[0]["CID"].ToString();
                string num = ds.Tables[0].Select("F_Date='" + date + "' and F_ChatType='" + cid + "'", "F_Num DESC")[0]["F_Num"].ToString();
                return string.IsNullOrEmpty(num) ? "0" : num;
            }
            catch
            {
                return "0";
            }
        }

        private string GetCount(string a, string b)
        {
            try
            {
                int aa = !isint(a) ? 0 : Convert.ToInt32(a);
                int bb = !isint(b) ? 0 : Convert.ToInt32(b);
                return (aa + bb).ToString();
            }
            catch
            {
                return "0";
            }
        }

        private bool isint(object value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DateTime dtb = Convert.ToDateTime(btn.Text);
            DateTime dte = dtb.AddMonths(1) > DateTime.Now ? DateTime.Now : dtb.AddMonths(1).AddDays(-1);
            tboxTimeB.Text = dtb.ToString("yyyy-MM-dd");
            tboxTimeE.Text = dte.ToString("yyyy-MM-dd");
            bind();
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

                newdr["F_Name"] = "所有战线";

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
                DropDownListArea3.Items.Add(new ListItem("所有战线", ""));
            }
        }

        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
        long sum1 = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");
                }

                if (e.Row.RowIndex >= 0 && !lblerro.Visible)
                {
                    for (int i = 1; i < GridView1.Columns.Count - 1; i++)
                    {
                        e.Row.Cells[i].Text = GetChatTypeNum(e.Row.Cells[0].Text.Replace("'", ""), GridView1.Columns[i].HeaderText);
                        e.Row.Cells[GridView1.Columns.Count-1].Text = GetCount(e.Row.Cells[GridView1.Columns.Count-1].Text, e.Row.Cells[i].Text);
                    }

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


    }
}
