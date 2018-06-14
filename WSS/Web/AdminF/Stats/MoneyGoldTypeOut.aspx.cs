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
    public partial class MoneyGoldTypeOut : System.Web.UI.Page
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
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            if (!IsPostBack)
            {
                tboxTimeB.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
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
            LabelTime.Text = searchdateB.ToShortDateString() + " 至 " + searchdateE.ToShortDateString();

            string sql = "";
            sql = @"SELECT convert(varchar(10),F_Date,120) as 日期, F_GoldSum0, F_GoldSum1,F_GoldSum2,F_GoldSum3,F_GoldSum4 ,F_GoldSum5 ,F_GoldSum6 ,F_GoldSum7 ,F_GoldSum8 ,F_GoldSum9 ,F_GoldSum10  FROM T_MoneyGoldType where F_Date>='" + searchdateB.ToShortDateString() + "' and F_Date<='" + searchdateE.ToShortDateString() + "' ";

            if (DropDownListArea1.SelectedIndex > 0)
            {
                sql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sql += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }
            sql += " order by F_Date desc";
           // sql += " union all select '总计' as 日期,sum(F_GoldSurplus) as 结余,sum(F_GoldOut) as 产出, sum(F_GoldIn) as 消耗";

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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex >= 0 && !lblerro.Visible)
                {
                    //sum1 += Convert.ToInt64(e.Row.Cells[1].Text);
                    //sum2 += Convert.ToInt64(e.Row.Cells[2].Text);
                    //sum3 += Convert.ToInt64(e.Row.Cells[3].Text);
                    //sum4 += Convert.ToInt64(e.Row.Cells[4].Text);
                    sum5 += Convert.ToInt64(e.Row.Cells[5].Text);
                    sum6 += Convert.ToInt64(e.Row.Cells[6].Text);
                    sum7 += Convert.ToInt64(e.Row.Cells[7].Text);
                    sum8 += Convert.ToInt64(e.Row.Cells[8].Text);
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "总计:";
                    //e.Row.Cells[1].Text = sum1.ToString("#,#0");
                    //e.Row.Cells[2].Text = sum2.ToString("#,#0");
                    //e.Row.Cells[3].Text = sum3.ToString("#,#0");
                    //e.Row.Cells[4].Text = sum4.ToString("#,#0");
                    e.Row.Cells[5].Text = sum5.ToString("#,#0");
                    e.Row.Cells[6].Text = sum6.ToString("#,#0");
                    e.Row.Cells[7].Text = sum7.ToString("#,#0");
                    e.Row.Cells[8].Text = sum8.ToString("#,#0");
                }
            }
            catch (System.Exception ex)
            {
                lblerro.Text = ex.Message;
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
