﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Collections;
using System.Collections.Generic;
namespace WebWSS.Stats
{
    public partial class RoleVocation : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        List<long> columnValue = new List<long>();
        DataSet ds;
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
                Common.MsgBox.Show(this,App_GlobalResources.Language.Tip_TimeError);
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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat)   + " " + App_GlobalResources.Language.LblTo + " " + searchdateE.ToString(SmallDateTimeFormat)  ;

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
            sql = @"with RVcte(F_Date,F_VocationType, F_VocationNum)
as
(
SELECT convert(varchar(10),F_Date,120),F_VocationType, Sum(F_VocationNum) as F_VocationNum FROM T_RoleVocation " + sqlwhere + @"
 group by F_Date,F_VocationType 
)
select distinct(a.F_Date)  as '"+App_GlobalResources.Language.LblDate+@"',
{0},(select SUM( F_VocationNum) from RVcte where F_Date=a.F_Date ) as '"+App_GlobalResources.Language.LblSum+"' FROM RVcte a order by a.F_Date desc";

            try
            {
                string vocationTemplate = "(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType={0}) as '{1}'";
                List<string> fields = new List<string>();
                GridView1.Columns.Clear();
                foreach (KeyValuePair<string, string> item in MapVacationType())
                {
                    fields.Add(string.Format(vocationTemplate, item.Key, item.Value));
                }
                
                sql = string.Format(sql, string.Join(",", fields));
                ds = DBHelperDigGameDB.Query(sql);
                DataTable table = ds.Tables[0];
                DataView myView = table.DefaultView;
                GridView1.Columns.Clear();
                foreach (DataColumn item in table.Columns)
                {
                      GridView1.Columns.Add(new BoundField() { DataField = item.ColumnName, HeaderText = item.ColumnName });
                }
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
                    ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, true);
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

        protected void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DateTime dtb = Convert.ToDateTime(btn.Text);
            DateTime dte = dtb.AddMonths(1) > DateTime.Now ? DateTime.Now : dtb.AddMonths(1).AddDays(-1);
            tboxTimeB.Text = dtb.ToString("yyyy-MM-dd");
            tboxTimeE.Text = dte.ToString("yyyy-MM-dd");
            bind();
        }

        protected void ControlChartSelect_SelectChanged(object sender, EventArgs e)
        {
            if (ControlChartSelect1.State==0)
            {
                GridView1.Visible = true;
                ControlChart1.Visible = false;
            }
            else
            {
                GridView1.Visible = false;
                ControlChart1.Visible = true;

                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State,true);
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
                newdr["F_Name"] =App_GlobalResources.Language.LblAllZone;

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

                newdr["F_Name"] =App_GlobalResources.Language.LblAllZoneLine;

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