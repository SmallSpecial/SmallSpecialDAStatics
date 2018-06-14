using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class CampLog : Admin_Page
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
                btn.Text = i.ToString().PadLeft(2, '0').Replace("00", "<<").Replace("32", ">>");
                btn.Visible = false;
                //btn.Click += new EventHandler(btn_Click);
                DateSelect.Controls.Add(btn);
            }
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            if (!IsPostBack)
            {
                //tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                //tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                bind();
                BindDdl1();
            }
            ControlOutFile1.ControlOut = GridView1;
            ControlOutFile1.VisibleExcel = false;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            //if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            //{
            //    Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
            //    return;
            //}
            //设置日期按钮状态
            //string dates = Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-01");
            //DateTime dtb = Convert.ToDateTime(dates);
            //DateTime dte = dtb.AddMonths(1).AddDays(-1);
            for (int i = 0; i <= 32; i++)
            {
                Control ctl = DateSelect.FindControl("btndateselect" + i);
                if (ctl != null && ctl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctl;
                    //if (dtb.AddDays(i - 1) <= DateTime.Now && (i <= dte.Day || i == 32))
                    //{
                    //    btn.Visible = true;
                    //    if (btn.Text == Convert.ToDateTime(tboxTimeB.Text).ToString("dd"))
                    //    {
                    //        btn.CssClass = "buttonblueo";
                    //        btn.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        btn.CssClass = "buttonblue";
                    //        btn.Enabled = true;
                    //    }
                    //}
                    //else
                    //{
                    //    btn.Visible = false;
                    //}
                }
            }
            //LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            
            


            DateTime searchdateB = DateTime.Now;
            DateTime searchdateE = DateTime.Now;
            //if (tboxTimeB.Text.Length > 0)
            //{
            //    searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            //}
            //if (tboxTimeE.Text.Length > 0)
            //{
            //    searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            //}
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat) + " ";

            string sqlwhere = "";
            //sqlwhere += @" and F_Date='" + searchdateB.ToString(SmallDateTimeFormat) + "'";
            if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                sqlwhere += @"where 1=1 and UnionId=" + DropDownList1.SelectedValue;
            }
            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";
            string battleZone = string.Empty;
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }
            string sql = "";
            sql = @"SELECT * FROM OPENQUERY ([LKSV_3_gsdata_db_"+bigZone+"_"+battleZone+"],'SELECT szName,nLevel,UnionId,MeritLevel FROM playerbaseinfo WHERE 1=1' ) " + sqlwhere + "order by szName";
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
                //if (ControlChart1.Visible == true)
                //{
                //    ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 1, 1);
                //}
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
        protected void ExportExcel(object sender,EventArgs e)
        {
            DateTime searchdateB = DateTime.Now;
            DateTime searchdateE = DateTime.Now;

            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat) + " ";

            string sqlwhere = "";
            if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                sqlwhere += @"where 1=1 and UnionId=" + DropDownList1.SelectedValue;
            }
            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";
            string battleZone = string.Empty;
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }
            string sql = "";
            sql = @"SELECT * FROM OPENQUERY ([LKSV_3_gsdata_db_" + bigZone + "_" + battleZone + "],'SELECT szName 玩家名称,nLevel 玩家等级,(CASE WHEN UnionId=1 THEN \"联盟阵营\" WHEN UnionId=2 THEN \"混沌军团\" ELSE \"\" END) 阵营,MeritLevel 玩家军衔等级 FROM playerbaseinfo WHERE 1=1' ) ";

            ds = DBHelperDigGameDB.Query(sql);
            ExportExcelHelper.ExportDataSet(ds);
        }
        //protected void ControlChartSelect_SelectChanged(object sender, EventArgs e)
        //{
        //    if (ControlChartSelect1.State == 0)
        //    {
        //        GridView1.Visible = true;
        //        ControlChart1.Visible = false;
        //    }
        //    else
        //    {
        //        GridView1.Visible = false;
        //        ControlChart1.Visible = true;
        //        ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 1, 1);
        //    }
        //}
        //protected void btn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Button btn = (Button)sender;
        //        string date = Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-") + btn.Text;
        //        if (date.IndexOf("<<") >= 0)
        //        {
        //            date = Convert.ToDateTime(date.Replace("<<", "01")).AddDays(-1).ToString("yyyy-MM-dd");
        //        }
        //        if (date.IndexOf(">>") >= 0)
        //        {
        //            date = Convert.ToDateTime(date.Replace(">>", "01")).AddMonths(1).ToString("yyyy-MM-dd");
        //        }
        //        tboxTimeB.Text = date;
        //        bind();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
        //    }

        //}
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
        protected string GetCampName(string campID)
        {
            if (campID=="1")
            {
                return "联盟阵营";
            }
            else if (campID == "2")
            {
                return "混沌军团";
            }
            else
            {
                return "";
            }
        }
        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListArea2.Items.Clear();
            BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);

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
        #region 转到触发方法
        protected void Go_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = int.Parse(((TextBox)GridView1.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            bind();   //重新绑定GridView
        }
        #endregion

        #region 分页触发方法
        protected void GvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();  //重新绑定GridView
        }
        #endregion
    }
}