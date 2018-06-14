using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class RoleOnlineFlow_Zone : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            //加入当月日期按钮控件
            for (int i = 0; i <= 32; i++)
            {
                Button btn = new Button();
                btn.ID = "btndateselect" + i;
                btn.Text = i.ToString().PadLeft(2,'0').Replace("00", "<<").Replace("32", ">>");
                btn.Visible = false;
                btn.Click += new EventHandler(btn_Click);
                DateSelect.Controls.Add(btn);
            }

            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            if (!IsPostBack)
            {
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                BindDdl1();
                //DropDownListArea1_SelectedIndexChanged(null, null);
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
                Common.MsgBox.Show(this,App_GlobalResources.Language.Tip_TimeError);
                return;
            }

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
                    if (dtb.AddDays(i - 1) <= DateTime.Now && (i <= dte.Day|| i==32))
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
            //if (DropDownListArea2.SelectedIndex >= 0)
            //{
            //    LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            //}
            //if (DropDownListArea3.SelectedIndex > 0)
            //{
            //    LabelArea.Text += "|" + DropDownListArea3.SelectedItem.Text;
            //}


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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat)   + " ";
            QueryZoneRole(searchdateB);
            return;
            string sqlwhere = "";
            sqlwhere += @" and F_Date='" + searchdateB.ToString(SmallDateTimeFormat) + "'";
            if (DropDownListArea1.SelectedIndex >= 0)
            {
                sqlwhere += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            //if (DropDownListArea2.SelectedIndex >= 0)
            //{
            //    sqlwhere += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            //}
            string sql = "";
            sql = @"SELECT convert(nvarchar(2), F_CreateTime,108) as F_CreateTime,  F_GZONEID
, max(F_GZONENAME) as F_GZONENAME
from T_RoleOnLineFlow  with(nolock)
where 1=1  " + sqlwhere + "  group by convert(nvarchar(2), F_CreateTime,108),F_GZONEID order by F_CreateTime asc";

            try
            {

                ds = DBHelperDigGameDB.Query(sql);

                lblDebug.Text = sql;

                List<string> lines = new List<string>();

                DataTable dtt = new DataTable();
                dtt.Columns.Add("F_Time", typeof(string));
                dtt.Columns.Add("num", typeof(int));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string line = dr["F_GZONENAME"].ToString();
                    string time = dr["F_CreateTime"].ToString();
                    if (!lines.Contains(line) && !string.IsNullOrEmpty(line))
                    {
                        lines.Add(line);
                    }
                    if (dtt.Select("F_Time='" + time + "'").Length == 0)
                    {
                        DataRow drt = dtt.NewRow();
                        drt["F_Time"] = time;
                        drt["num"] = 1;
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
                coli.HeaderText = App_GlobalResources.Language.LblSum;
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

                GridView1.HeaderRow.Cells[GridView1.Columns.Count - 1].Text =App_GlobalResources.Language.LblSum+ " <span class=tyellow>" + GridView1.FooterRow.Cells[GridView1.Columns.Count - 1].Text + "</span>";

                if (ControlChart1.Visible == true)
                {
                    ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State);
                }

            }
            catch (System.Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }

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
                bind();
            }
            catch (System.Exception ex)
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
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

                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State);
            }
        }
        /// <summary>
        /// 得到历史信息
        /// </summary>
        public string GetHistoryOnlineInfo(string linename)
        {
            try
            {
                //DataRow dr = ds.Tables[0].Select("F_GNGSNAME='" + linename + "'", "F_MaxPlayerNumHistory DESC")[0];
                //string maxnum = dr["F_MaxPlayerNumHistory"].ToString();
                //string maxtime = Convert.ToDateTime(dr["F_DateTimeMaxPlayerNum"]).ToString("yyyyMMdd HH:mm");
                //return string.Format("【{0} 最大<span class=tyellow>{1}</span> 时间{2}】 ", linename, maxnum, maxtime);
                return " ";
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
        public string GetOnlineNum(string bigzone,string time, string linename)
        {
            try
            {
                string sql = @"select sum(F_PlayerNumOnline)  as F_PlayerNumOnline from (
SELECT    max(F_PlayerNumOnline) as F_PlayerNumOnline
FROM         T_RoleOnLineFlow with(nolock)
where 
F_BigZone=" + bigzone + @"
and
F_GZONENAME='" + linename + @"'
and F_Date='" +Convert.ToDateTime(tboxTimeB.Text).ToString(SmallDateTimeFormat)   + @"'
and
convert(nvarchar(2), F_CreateTime,108)='" + time + @"'
group by F_GNGSID) a";
                string num = DBHelperDigGameDB.GetSingle(sql).ToString();
                return string.IsNullOrEmpty(num) ? "0" : num;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");
            }
            try
            {
                if (e.Row.RowIndex >= 0 && !lblerro.Visible)
                {
                    for (int i = 1; i < GridView1.Columns.Count - 1; i++)
                    {
                        //e.Row.Cells[i].Text = GetOnlineNum(DropDownListArea1.SelectedValue.Split(',')[1],e.Row.Cells[0].Text.Replace("点", ""), GridView1.Columns[i].HeaderText);
                        //e.Row.Cells[GridView1.Columns.Count - 1].Text = GetCount(e.Row.Cells[GridView1.Columns.Count - 1].Text, e.Row.Cells[i].Text);
                    }

                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = App_GlobalResources.Language.LblMaxOnline;

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

        void QueryZoneRole(DateTime timeWhere) 
        {
            string cmd = @"select convert(nvarchar(2),F_Createtime,108) as F_CreateTime,F_GZONENAME,MAX(F_PlayerNumOnline) as F_PlayerNumOnline
from
(
	select t.F_CreateTime,t.F_GZONENAME,SUM(t.F_PlayerNumOnline) as F_PlayerNumOnline
	from
	(
		SELECT     F_CreateTime,F_GGSID,   F_GZONENAME ,F_PlayerNumOnline
		from T_RoleOnLineFlow
		where F_Date=CONVERT(nvarchar(10),'{time}',120)
		group by F_CreateTime,F_GGSID, F_GZONEID,F_GZONENAME,F_PlayerNumOnline
		 
	) as  t
	group by t.F_CreateTime,t.F_GZONENAME
) res
group by  convert(nvarchar(2),F_Createtime,108),F_GZONENAME";
            cmd = cmd.Replace("{time}", timeWhere.ToString(SmallDateTimeFormat));
            DataSet ds = DBHelperDigGameDB.Query(cmd);
            DataTable table = ds.Tables[0];
            if (table.Rows.Count == 0) 
            {
                GridView1.DataSource = (new DataTable());
                GridView1.DataBind();
                lblerro.Visible = true;
                return;
            }
            List<string> yd = new List<string>();
            List<string> xd = new List<string>();
            int x = 0, y = 0;
            GridView1.Columns.Clear();
            GridView1.Columns.Add(new BoundField() { HeaderText = App_GlobalResources.Language.LblTime, DataField = "0" });
            string columnName = "F_GZONENAME", rowName = "F_CreateTime";
            int columnIndex = table.DefaultView.ToTable(true, new string[] { columnName }).Rows.Count;
            int rowIndex = table.DefaultView.ToTable(true, new string[] { rowName }).Rows.Count;
            string[,] source = new string[rowIndex,columnIndex];
            foreach (DataRow item in table.Rows)
            {
                string num = ((int)item["F_PlayerNumOnline"]).ToString();
                string xi = (string)item[columnName];
                string yi = (string)item[rowName];
                if (!xd.Contains(xi))
                {
                    x++;
                    BoundField column = new BoundField();
                    column.HeaderText = xi;
                    column.DataField = x.ToString();
                    GridView1.Columns.Add(column);
                    xd.Add(xi);
                }
                if (!yd.Contains(yi))
                {
                    yd.Add(yi);
                    y++;
                }
                source[ Array.IndexOf(yd.ToArray(), yi),Array.IndexOf(xd.ToArray(), xi)] = num;
            }
            table.Clear();
            table.Dispose();
            GridView1.Columns.Add(new BoundField() { HeaderText = App_GlobalResources.Language.LblSumOfMax, DataField = (xd.Count + 1).ToString() });
            DataTable res = new DataTable();
            for (int i = 0; i <=xd.Count+1; i++)
            {//额外增加两列： 首列>名称 尾列>总计 
                res.Columns.Add(i.ToString());
            }
            for (int i = 0; i < yd.Count; i++)
            {
                DataRow row = res.NewRow();
                object[] l = new object[xd.Count+2];
                l[0] = yd[i]+App_GlobalResources.Language.LblHourUnit;
                int sum = 0;
                for (int j = 0; j < xd.Count; j++)
                {
                    int t = 0;
                    int.TryParse(source[i, j], out t);
                    sum = sum < t ? t : sum;
                    l[j+1] = t;
                }
                l[xd.Count+1] = sum;
                row.ItemArray = l;
                res.Rows.Add(row);
            }
            GridView1.DataSource = res;
            GridView1.DataBind();


            if (ControlChart1.Visible == true)
            {
                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State);
            }
            lblerro.Visible = false;
        }
    }
}
