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
    public partial class RoleVocationLevelRank : System.Web.UI.Page
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
            LabelTime.Text = searchdateB.ToShortDateString() + " ";

            string sqlwhere = " where 1=1 ";
            sqlwhere += @" and F_Date='" + searchdateB.ToShortDateString() + "'";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                sqlwhere += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sqlwhere += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }

            string sql = "";
            sql = @"with RVcte(F_BigZone, F_ZoneID,F_Level, F_Experience, F_RoleID, F_Pro, F_RoleName, F_LevelUpTime, F_LastTime)
as
(
SELECT F_BigZone, F_ZoneID,F_Level, F_Experience, F_RoleID, F_Pro, F_RoleName, F_LevelUpTime, F_LastTime FROM T_RoleLevelRank " + sqlwhere + @"

)
select * from (SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum,F_BigZone, F_ZoneID,F_Level, F_Experience, F_RoleID, F_Pro, F_RoleName, F_LevelUpTime, F_LastTime
FROM RVcte) a
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum0,F_BigZone as F_BigZone0,F_ZoneID as F_ZoneID0,F_Level as F_Level0, F_Experience as F_Experience0, F_RoleID as F_RoleID0, F_Pro as F_Pro0, F_RoleName as F_RoleName0, F_LevelUpTime as F_LevelUpTime0, F_LastTime as F_LastTime0
FROM RVcte where F_Pro/6=0) a0
on a.rownum=a0.rownum0
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum1,F_BigZone as F_BigZone1,F_ZoneID as F_ZoneID1,F_Level as F_Level1, F_Experience as F_Experience1, F_RoleID as F_RoleID1, F_Pro as F_Pro1, F_RoleName as F_RoleName1, F_LevelUpTime as F_LevelUpTime1, F_LastTime as F_LastTime1
FROM RVcte where F_Pro/6=1)  a1
on a.rownum=a1.rownum1
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum2,F_BigZone as F_BigZone2,F_ZoneID as F_ZoneID2,F_Level as F_Level2, F_Experience as F_Experience2, F_RoleID as F_RoleID2, F_Pro as F_Pro2, F_RoleName as F_RoleName2, F_LevelUpTime as F_LevelUpTime2, F_LastTime as F_LastTime2
FROM RVcte where F_Pro/6=2)  a2
on a.rownum=a2.rownum2
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum3,F_BigZone as F_BigZone3,F_ZoneID as F_ZoneID3,F_Level as F_Level3, F_Experience as F_Experience3, F_RoleID as F_RoleID3, F_Pro as F_Pro3, F_RoleName as F_RoleName3, F_LevelUpTime as F_LevelUpTime3, F_LastTime as F_LastTime3
FROM RVcte where F_Pro/6=3)  a3
on a.rownum=a3.rownum3
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum4,F_BigZone as F_BigZone4,F_ZoneID as F_ZoneID4,F_Level as F_Level4, F_Experience as F_Experience4, F_RoleID as F_RoleID4, F_Pro as F_Pro4, F_RoleName as F_RoleName4, F_LevelUpTime as F_LevelUpTime4, F_LastTime as F_LastTime4
FROM RVcte where F_Pro/6=4)  a4
on a.rownum=a4.rownum4
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum5,F_BigZone as F_BigZone5,F_ZoneID as F_ZoneID5,F_Level as F_Level5, F_Experience as F_Experience5, F_RoleID as F_RoleID5, F_Pro as F_Pro5, F_RoleName as F_RoleName5, F_LevelUpTime as F_LevelUpTime5, F_LastTime as F_LastTime5
FROM RVcte where F_Pro/6=5)  a5
on a.rownum=a5.rownum5
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum6,F_BigZone as F_BigZone6,F_ZoneID as F_ZoneID6,F_Level as F_Level6, F_Experience as F_Experience6, F_RoleID as F_RoleID6, F_Pro as F_Pro6, F_RoleName as F_RoleName6, F_LevelUpTime as F_LevelUpTime6, F_LastTime as F_LastTime6
FROM RVcte where F_Pro/6=6)  a6
on a.rownum=a6.rownum6
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc) AS rownum7,F_BigZone as F_BigZone7,F_ZoneID as F_ZoneID7,F_Level as F_Level7, F_Experience as F_Experience7, F_RoleID as F_RoleID7, F_Pro as F_Pro7, F_RoleName as F_RoleName7, F_LevelUpTime as F_LevelUpTime7, F_LastTime as F_LastTime7
FROM RVcte where F_Pro/6=7)  a7
on a.rownum=a7.rownum7
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Experience desc,F_LastTime desc) AS rownum99,F_BigZone as F_BigZone99,F_ZoneID as F_ZoneID99,F_Level as F_Level99, F_Experience as F_Experience99, F_RoleID as F_RoleID99, F_Pro as F_Pro99, F_RoleName as F_RoleName99, F_LevelUpTime as F_LevelUpTime99, F_LastTime as F_LastTime99
FROM RVcte )  a99
on a.rownum=a99.rownum99
";

            //            select 
            //(select isnull(Sum(F_VocationNum),0) from RVcte where F_Level=a.F_Level and F_VocationType/6=6) as 虎贲,
            //(select isnull(Sum(F_VocationNum),0) from RVcte where F_Level=a.F_Level and F_VocationType/6=2) as 浪人,
            //(select isnull(Sum(F_VocationNum),0) from RVcte where F_Level=a.F_Level and F_VocationType/6=3) as 龙胆,
            //(select isnull(Sum(F_VocationNum),0) from RVcte where F_Level=a.F_Level and F_VocationType/6=4) as 巧工,
            //(select isnull(Sum(F_VocationNum),0) from RVcte where F_Level=a.F_Level and F_VocationType/6=7) as 气功师,
            //(select isnull(Sum(F_VocationNum),0) from RVcte where F_Level=a.F_Level and F_VocationType/6=0) as 花灵,
            //(select isnull(Sum(F_VocationNum),0) from RVcte where F_Level=a.F_Level and F_VocationType/6=1) as 天师,
            //(select isnull(Sum(F_VocationNum),0) from RVcte where F_Level=a.F_Level and F_VocationType/6=5) as 行者
            // FROM RVcte a order by a.F_Level asc

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
        //字符集转换   
        public string TranI2G(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["C"]))
                {
                    return value;
                }
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
                gb2312 = System.Text.Encoding.GetEncoding("gb2312");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }


        /// <summary>
        /// 得到游戏大区名字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetZoneName(object valuea, object valueb)
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT '大区:'+isnull(a.F_Name,'')+' 战区:'+isNull((SELECT TOP 1 b.F_Name FROM T_GameConfig b WHERE (b.F_ParentID = a.F_ID) AND (b.F_IsUsed = 1) 
and (b.F_ValueGame='" + valueb + "')),'') FROM T_GameConfig a WHERE (a.F_ParentID = 1000) AND (a.F_IsUsed = 1) and (a.F_ValueGame='" + valuea + "')";

            try
            {
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return "";
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
                newdr["F_Name"] = "所有大区";
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
                DataRow newdr = ds.Tables[0].NewRow();
                newdr["F_Name"] = "所有战区";

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

        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
        long sum1 = 0;
        long sum2 = 0;
        long sum3 = 0;
        long sum4 = 0;
        long sum5 = 0;
        long sum6 = 0;
        long sum7 = 0;
        long sum8 = 0;
        long sum9 = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex >= 0 && !lblerro.Visible)
                {
                    long sumi = 0;
                    sumi += Convert.ToInt64(e.Row.Cells[1].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[2].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[3].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[4].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[5].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[6].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[7].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[8].Text);
                    e.Row.Cells[9].Text = sumi.ToString("#,#0");


                    sum1 += Convert.ToInt64(e.Row.Cells[1].Text);
                    sum2 += Convert.ToInt64(e.Row.Cells[2].Text);
                    sum3 += Convert.ToInt64(e.Row.Cells[3].Text);
                    sum4 += Convert.ToInt64(e.Row.Cells[4].Text);
                    sum5 += Convert.ToInt64(e.Row.Cells[5].Text);
                    sum6 += Convert.ToInt64(e.Row.Cells[6].Text);
                    sum7 += Convert.ToInt64(e.Row.Cells[7].Text);
                    sum8 += Convert.ToInt64(e.Row.Cells[8].Text);
                    sum9 += Convert.ToInt64(e.Row.Cells[9].Text);
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "总计:";
                    e.Row.Cells[1].Text = sum1.ToString("#,#0");
                    e.Row.Cells[2].Text = sum2.ToString("#,#0");
                    e.Row.Cells[3].Text = sum3.ToString("#,#0");
                    e.Row.Cells[4].Text = sum4.ToString("#,#0");
                    e.Row.Cells[5].Text = sum5.ToString("#,#0");
                    e.Row.Cells[6].Text = sum6.ToString("#,#0");
                    e.Row.Cells[7].Text = sum7.ToString("#,#0");
                    e.Row.Cells[8].Text = sum8.ToString("#,#0");
                    e.Row.Cells[9].Text = sum9.ToString("#,#0");
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
