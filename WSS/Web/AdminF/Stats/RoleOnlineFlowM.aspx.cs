using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WSS.DBUtility;

namespace WSS.Web.AdminF.Stats
{
    public partial class RoleOnlineFlowM : System.Web.UI.Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FID"] == null)
            {
                Response.Redirect("../login.aspx");
            }
            //加入当月日期按钮控件
            for (int i = -1; i <= 31; i++)
            {
                Button btn = new Button();
                btn.ID = "btndateselect" + i;
                btn.Visible = false;
                btn.Click += new EventHandler(btn_Click);
                DateSelect.Controls.Add(btn);
            }
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            if (!IsPostBack)
            {
                tboxTimeB.Text = DateTime.Now.ToShortDateString();
                tboxTimeE.Text = DateTime.Now.ToShortDateString();
                BindDdl1();
                DropDownListArea1_SelectedIndexChanged(null, null);
                bind();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            //设置日期按钮状态
            string dates = Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-01");
            DateTime dtb = Convert.ToDateTime(dates);
            DateTime dte = dtb.AddMonths(1).AddDays(-1);
            for (int i = -1; i <= 31; i++)
            {
                Control ctl = DateSelect.FindControl("btndateselect" + i);
                if (ctl != null && ctl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctl;
                    if (i <= dte.Day && dtb.AddDays(i) <= DateTime.Now)
                    {
                        btn.Text = dtb.AddDays(i).ToString("MM-dd");
                        btn.Visible = true;

                        if (btn.Text == Convert.ToDateTime(tboxTimeB.Text).ToString("MM-dd"))
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
            if (DropDownListArea2.SelectedIndex >= 0)
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
            LabelTime.Text = searchdateB.ToShortDateString() + " ";

            string sqlwhere = "";
            sqlwhere += @" and F_Date='" + searchdateB.ToShortDateString() + "'";
            if (DropDownListArea1.SelectedIndex >= 0)
            {
                sqlwhere += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex >= 0)
            {
                sqlwhere += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }

            string sql = "";
            sql = @"SELECT replace(convert(nvarchar(2), F_CreateTime,108)+':'+convert(varchar(4),substring(convert(nvarchar(8), F_CreateTime,108),4,2)/15*15),':0',':00')   as F_CreateTime,  F_GGSNAME, F_GNGSID, F_GNGSNAME, F_GZONEID, F_GZONENAME, 
                      F_PlayerNumOnline, F_MaxPlayerNumHistory, F_DateTimeMaxPlayerNum from T_RoleOnLineFlow where 1=1 " + sqlwhere + "  order by F_CreateTime asc";

            try
            {

                ds = DBHelperDigGameDB.Query(sql);

                List<string> lines = new List<string>();

                DataTable dtt = new DataTable();
                dtt.Columns.Add("F_Time", typeof(string));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string line = dr["F_GNGSNAME"].ToString();
                    string time = dr["F_CreateTime"].ToString();
                    if (!lines.Contains(line) && !string.IsNullOrEmpty(line))
                    {
                        lines.Add(line);
                    }
                    if (dtt.Select("F_Time='" + time + "'").Length == 0)
                    {
                        DataRow drt = dtt.NewRow();
                        drt["F_Time"] = time;
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

                    lblHistory.Text += GetHistoryOnlineInfo(linename);
                }
                BoundField coli = new BoundField();
                coli.HeaderText = "总 计";
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


                for (int i = 1; i < GridView1.Columns.Count; i++)
                {
                    GridView1.FooterRow.Cells[i].Text = "0";
                    for (int y = 0; y < GridView1.Rows.Count; y++)
                    {
                        string max = GetMax(GridView1.FooterRow.Cells[i].Text, GridView1.Rows[y].Cells[i].Text);
                        string now = GridView1.FooterRow.Cells[i].Text.Split(' ')[0];
                        if (Convert.ToInt32(max) > Convert.ToInt32(now))
                        {
                            GridView1.FooterRow.Cells[i].Text = "" + max + " [" + GridView1.Rows[y].Cells[0].Text + "]";
                        }

                    }
                }

                GridView1.HeaderRow.Cells[GridView1.Columns.Count - 1].Text = "总 计 <span class=tyellow>" + GridView1.FooterRow.Cells[GridView1.Columns.Count - 1].Text + "</span>";

            }
            catch (System.Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = "出错:" + ex.Message;
                //lblerro.Text = sql;
            }

        }
        protected void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            tboxTimeB.Text = Convert.ToDateTime(tboxTimeB.Text).Year.ToString() + "-" + btn.Text;
            bind();
        }

        /// <summary>
        /// 得到历史信息
        /// </summary>
        public string GetHistoryOnlineInfo(string linename)
        {
            try
            {
                DataRow dr = ds.Tables[0].Select("F_GNGSNAME='" + linename + "'", "F_MaxPlayerNumHistory DESC")[0];
                string maxnum = dr["F_MaxPlayerNumHistory"].ToString();
                string maxtime = Convert.ToDateTime(dr["F_DateTimeMaxPlayerNum"]).ToString("yyyyMMdd HH:mm");
                return string.Format("【{0} 最大<span class=tyellow>{1}</span> 时间{2}】 ", linename, maxnum, maxtime);
            }
            catch
            {
                return " ";
            }
        }

        /// <summary>
        /// 得到在线人数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="linename"></param>
        /// <returns></returns>
        public string GetOnlineNum(string time, string linename)
        {
            try
            {
                //int num = 0;
                //foreach (DataRow dr in ds.Tables[0].Select("F_CreateTime='" + time + "' and F_GNGSNAME='" + linename + "'"))
                //{
                //    int numt = Convert.ToInt32(dr["F_PlayerNumOnline"]);
                //    if (numt > num)
                //    {
                //        num = numt;
                //    }
                //}
                //return num.ToString();

                string num = ds.Tables[0].Select("F_CreateTime='" + time + "' and F_GNGSNAME='" + linename + "'", "F_PlayerNumOnline DESC")[0]["F_PlayerNumOnline"].ToString();
                return string.IsNullOrEmpty(num)?"0":num;
            }
            catch
            {
                return "0";
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
                //newdr["F_Name"] = "所有大区";
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
                DropDownListArea1.Items.Add(new ListItem("所有大区", ""));
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
                //newdr["F_Name"] = "所有战区";
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
                DropDownListArea2.Items.Add(new ListItem("所有战区", ""));
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
        private string GetMax(string a, string b)
        {
            try
            {
                int aa = !isint(a) ? 0 : Convert.ToInt32(a);
                int bb = !isint(b) ? 0 : Convert.ToInt32(b);
                return aa > bb ? aa.ToString() : bb.ToString();
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
        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex >= 0 && !lblerro.Visible)
                {
                    for (int i = 1; i < GridView1.Columns.Count - 1; i++)
                    {
                        e.Row.Cells[i].Text = GetOnlineNum(e.Row.Cells[0].Text.Replace("点",""), GridView1.Columns[i].HeaderText);
                        e.Row.Cells[GridView1.Columns.Count-1].Text = GetCount(e.Row.Cells[GridView1.Columns.Count-1].Text, e.Row.Cells[i].Text);
                    }

                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "最大在线";

                }
            }
            catch (System.Exception ex)
            {
                lblinfo.Text = ex.Message;
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
