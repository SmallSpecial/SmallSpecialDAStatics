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
    public partial class StoneDropDay : System.Web.UI.Page
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
            sql = @"
WITH T_EquipStarVacation_CTE ( F_Date,F_StarLevel, F_ItemCount,F_Vocation)
AS
(
SELECT     F_Date,F_StarLevel, F_ItemCount,isnull(F_Vocation,'')
FROM         T_EquipLevelVacation  where  F_Date >='" + searchdateB.ToShortDateString() + @"' and F_Date<='" + searchdateE.ToShortDateString() + @"' and F_SuitName='精炼' and F_EquipType='宝石' ";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                sql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sql += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }

            sql += @")
select convert(VARCHAR(10), F_date,111) as date
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='1档' and len(F_Vocation)=0) as num1
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='2档' and len(F_Vocation)=0) as num2
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='3档' and len(F_Vocation)=0) as num3
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='4档' and len(F_Vocation)=0) as num4
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='5档' and len(F_Vocation)=0) as num5
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='6档' and len(F_Vocation)=0) as num6
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='7档' and len(F_Vocation)=0) as num7
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='8档' and len(F_Vocation)=0) as num8
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='1档' and len(F_Vocation)=2) as numB1
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='2档' and len(F_Vocation)=2) as numB2
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='3档' and len(F_Vocation)=2) as numB3
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='4档' and len(F_Vocation)=2) as numB4
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='5档' and len(F_Vocation)=2) as numB5
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='6档' and len(F_Vocation)=2) as numB6
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='7档' and len(F_Vocation)=2) as numB7
,(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_Date=a.F_Date and F_StarLevel='8档' and len(F_Vocation)=2) as numB8
from T_EquipStarVacation_CTE a group by F_Date order by F_Date desc
";


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
        long sum9 = 0;
        long sum10 = 0;
        long sum11 = 0;
        long sum12 = 0;
        long sum13 = 0;
        long sum14 = 0;
        long sum15 = 0;
        long sum16 = 0;
        long sum17 = 0;
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
                    sumi += Convert.ToInt64(e.Row.Cells[9].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[10].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[11].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[12].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[13].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[14].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[15].Text);
                    sumi += Convert.ToInt64(e.Row.Cells[16].Text);
                    e.Row.Cells[17].Text = sumi.ToString("#,#0");


                    sum1 += Convert.ToInt64(e.Row.Cells[1].Text);
                    sum2 += Convert.ToInt64(e.Row.Cells[2].Text);
                    sum3 += Convert.ToInt64(e.Row.Cells[3].Text);
                    sum4 += Convert.ToInt64(e.Row.Cells[4].Text);
                    sum5 += Convert.ToInt64(e.Row.Cells[5].Text);
                    sum6 += Convert.ToInt64(e.Row.Cells[6].Text);
                    sum7 += Convert.ToInt64(e.Row.Cells[7].Text);
                    sum8 += Convert.ToInt64(e.Row.Cells[8].Text);
                    sum9 += Convert.ToInt64(e.Row.Cells[9].Text);
                    sum10 += Convert.ToInt64(e.Row.Cells[10].Text);
                    sum11 += Convert.ToInt64(e.Row.Cells[11].Text);
                    sum12 += Convert.ToInt64(e.Row.Cells[12].Text);
                    sum13 += Convert.ToInt64(e.Row.Cells[13].Text);
                    sum14 += Convert.ToInt64(e.Row.Cells[14].Text);
                    sum15 += Convert.ToInt64(e.Row.Cells[15].Text);
                    sum16 += Convert.ToInt64(e.Row.Cells[16].Text);
                    sum17 += Convert.ToInt64(e.Row.Cells[17].Text.Replace(",", ""));
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
                    e.Row.Cells[10].Text = sum10.ToString("#,#0");
                    e.Row.Cells[11].Text = sum11.ToString("#,#0");
                    e.Row.Cells[12].Text = sum12.ToString("#,#0");
                    e.Row.Cells[13].Text = sum13.ToString("#,#0");
                    e.Row.Cells[14].Text = sum14.ToString("#,#0");
                    e.Row.Cells[15].Text = sum15.ToString("#,#0");
                    e.Row.Cells[16].Text = sum16.ToString("#,#0");
                    e.Row.Cells[17].Text = sum17.ToString("#,#0");
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
