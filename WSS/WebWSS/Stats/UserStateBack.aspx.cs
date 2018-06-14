using System;
using System.Data;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Collections.Generic;

namespace WebWSS.Stats
{
    public partial class UserStateBack : Admin_Page
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
                tboxTimeB.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                BindDdl1();
                bind();
            }
            ControlOutFile1.ControlOut = GridView1;
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlMonthSelect1.SelectDateB.ToString("yyyy-MM-dd");
            tboxTimeE.Text = ControlMonthSelect1.SelectDateE.ToString("yyyy-MM-dd");
            bind();
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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat)   +  " "+ App_GlobalResources.Language.LblTo+" "  + searchdateE.ToString(SmallDateTimeFormat)  ;

            string sql = "";
            sql = @"SELECT F_id  ,F_Year,F_Month,F_Day,F_Date,F_BigZone,
ISNULL(F_BackIntervalDay3,0) as F_BackIntervalDay3,
ISNULL(F_BackIntervalDay4,0) as F_BackIntervalDay4,
ISNULL(F_BackIntervalDay5,0) as F_BackIntervalDay5,
ISNULL(F_BackIntervalDay6,0) as F_BackIntervalDay6,
ISNULL(F_BackIntervalDay7AM,0) as F_BackIntervalDay7AM FROM T_UserStateBack where F_Date>='" + searchdateB.ToString(DateTimeFormat) + "' and F_Date<='" + searchdateE.ToString(DateTimeFormat) + "' ";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                sql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sql += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }
            sql += " order by F_Date desc";

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

                if (ControlChart1.Visible == true)
                {
                    ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, true, 0, 0);
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

                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State,true,0,0);
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
            //DbHelperSQLP spg = new DbHelperSQLP();
            //spg.connectionString = ConnStrGSSDB;

            //string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = " + id + ") AND (F_IsUsed = 1)";

            //try
            //{
            //    ds = spg.Query(sql);
            //    DataRow newdr = ds.Tables[0].NewRow();
            //    newdr["F_Name"] = App_GlobalResources.Language.LblAllZone;

            //    newdr["F_ValueGame"] = "";
            //    ds.Tables[0].Rows.InsertAt(newdr, 0);
            //    this.DropDownListArea2.DataSource = ds;
            //    this.DropDownListArea2.DataTextField = "F_Name";
            //    this.DropDownListArea2.DataValueField = "F_ValueGame";
            //    this.DropDownListArea2.DataBind();

            //}
            //catch (System.Exception ex)
            //{
            //    DropDownListArea2.Items.Clear();
            //    DropDownListArea2.Items.Add(new ListItem(App_GlobalResources.Language.LblAllZone, ""));
            //}
        }
        public void BindDdl3(string id)
        {
            //DbHelperSQLP spg = new DbHelperSQLP();
            //spg.connectionString = ConnStrGSSDB;

            //string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = " + id + ") AND (F_IsUsed = 1)";

            //try
            //{
            //    ds = spg.Query(sql);
            //    DataRow newdr = ds.Tables[0].NewRow();

            //    newdr["F_Name"] = "所有战线";

            //    newdr["F_ValueGame"] = "";
            //    ds.Tables[0].Rows.InsertAt(newdr, 0);
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
        Dictionary<int, int> dic = new Dictionary<int, int>();
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");

                    for (int i = 1; i < 7; i++)
                    {
                        int num = Convert.ToInt32(e.Row.Cells[i].Text);
                        if (dic.ContainsKey(i))
                        {
                            dic[i] = dic[i] + num;
                        }
                        else
                        {
                            dic.Add(i, num);
                        }
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "平均:";
  
                    for (int i = 1; i < 7; i++)
                    {
                        e.Row.Cells[i].Text = (dic[i] / GridView1.Rows.Count).ToString();
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
            ControlMonthSelect1.SetSelectDate(tboxTimeB.Text,tboxTimeE.Text);
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
