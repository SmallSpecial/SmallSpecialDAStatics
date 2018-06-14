using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class RoleVocationLevelRank : Admin_Page
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
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat)   + " ";

            string sqlwhere = " where 1=1 ";
            sqlwhere += @" and F_Date='" + searchdateB.ToString(SmallDateTimeFormat) + "'";
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
select * from (SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Level desc,F_LevelUpTime asc) AS rownum,F_BigZone, F_ZoneID,F_Level, F_Experience, F_RoleID, F_Pro, F_RoleName, F_LevelUpTime, F_LastTime
FROM RVcte) a
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Level desc,F_LevelUpTime asc) AS rownum1,F_BigZone as F_BigZone1,F_ZoneID as F_ZoneID1,F_Level as F_Level1, F_Experience as F_Experience1, F_RoleID as F_RoleID1, F_Pro as F_Pro1, F_RoleName as F_RoleName1, F_LevelUpTime as F_LevelUpTime1, F_LastTime as F_LastTime1
FROM RVcte where F_Pro=1)  a1
on a.rownum=a1.rownum1
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Level desc,F_LevelUpTime asc) AS rownum2,F_BigZone as F_BigZone2,F_ZoneID as F_ZoneID2,F_Level as F_Level2, F_Experience as F_Experience2, F_RoleID as F_RoleID2, F_Pro as F_Pro2, F_RoleName as F_RoleName2, F_LevelUpTime as F_LevelUpTime2, F_LastTime as F_LastTime2
FROM RVcte where F_Pro=2)  a2
on a.rownum=a2.rownum2
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Level desc,F_LevelUpTime asc) AS rownum3,F_BigZone as F_BigZone3,F_ZoneID as F_ZoneID3,F_Level as F_Level3, F_Experience as F_Experience3, F_RoleID as F_RoleID3, F_Pro as F_Pro3, F_RoleName as F_RoleName3, F_LevelUpTime as F_LevelUpTime3, F_LastTime as F_LastTime3
FROM RVcte where F_Pro=3)  a3
on a.rownum=a3.rownum3
left join
(SELECT  TOP 54   ROW_NUMBER() OVER(order by F_Level desc,F_LevelUpTime asc) AS rownum4,F_BigZone as F_BigZone4,F_ZoneID as F_ZoneID4,F_Level as F_Level4, F_Experience as F_Experience4, F_RoleID as F_RoleID4, F_Pro as F_Pro4, F_RoleName as F_RoleName4, F_LevelUpTime as F_LevelUpTime4, F_LastTime as F_LastTime4
FROM RVcte where F_Pro=4) a4
on a.rownum=a4.rownum4
left join
(SELECT  TOP 50   ROW_NUMBER() OVER(order by F_Level desc,F_LevelUpTime asc) AS rownum99,F_BigZone as F_BigZone99,F_ZoneID as F_ZoneID99,F_Level as F_Level99, F_Experience as F_Experience99, F_RoleID as F_RoleID99, F_Pro as F_Pro99, F_RoleName as F_RoleName99, F_LevelUpTime as F_LevelUpTime99, F_LastTime as F_LastTime99
FROM RVcte )  a99
on a.rownum=a99.rownum99
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

                GridView2.DataSource = myView;
                GridView2.DataBind();

                if (ControlChart1.Visible == true)
                {
                    ControlChart1.SetChart(GridView2, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
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

        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            bind();
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

                ControlChart1.SetChart(GridView2, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
            }
        }



        public string TransDe(string value)
        {
            if (lblDecoding.Visible)
            {
                return CodingTran.Tran(lblDeType.Text, value);
            }
            else
            {
                return value;
            }
        }
        //得到角色名称
        public string GetRoleName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT F_RoleName FROM T_BaseRoleCreate with(nolock) where F_RoleID=" + value + "";
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
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
           System.Text.EncodingInfo[] ss=  System.Text.Encoding.GetEncodings();
            ControlDateSelect1.SetSelectDate(tboxTimeB.Text);
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
