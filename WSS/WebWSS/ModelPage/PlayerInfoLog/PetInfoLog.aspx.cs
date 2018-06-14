using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.PlayerInfoLog
{
    public partial class PetInfoLog : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        #endregion

        #region 事件
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            if (!IsPostBack)
            {
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                bind();
                BindDBigZone();
            }
            ControlOutFile1.ControlOut = GridView1;
            ControlOutFile1.VisibleExcel = false;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            System.Text.EncodingInfo[] ss = System.Text.Encoding.GetEncodings();
            ControlDateSelect1.SetSelectDate(tboxTimeB.Text);
            bind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListArea2.Items.Clear();
            BindBattleZone(DropDownListArea1.SelectedValue.Split(',')[0]);
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
                //ControlChart1.SetChart(GridView2, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
            }
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            bind();
        }
        #endregion

        #region 方法
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
            int num = searchdateE.Subtract(searchdateB).Days;
            if (num < 0)
            {
                Response.Write("<Script Language=JavaScript>alert('结束时间不能晚于开始时间！');</Script>");
                return;
            }

            string strOPID = "50137";
            string strParam1 = ddlOPID.SelectedValue;//操作类型
            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";//大区
            string battleZone = string.Empty;//战区
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }
            string sql = string.Format("SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_{0}_{1}],'", bigZone, battleZone);

            for (int i = 0; i <= num; i++)
            {
                string strSTime = searchdateB.AddDays(i).ToString("yyyy_MM_dd");
                if (i == 0)
                    sql += string.Format("SELECT \"{0}\" AS DateTime,COUNT(DISTINCT CID) AS PeopleNum,COUNT(CID) AS Num FROM {1}_other_log WHERE OPID={2} AND PARA_1={3}", strSTime, strSTime, strOPID, strParam1);
                else
                    sql += string.Format(" UNION ALL SELECT \"{0}\" AS DateTime,COUNT(DISTINCT CID) AS PeopleNum,COUNT(CID) AS Num FROM {1}_other_log WHERE OPID={2} AND PARA_1={3}", strSTime, strSTime, strOPID, strParam1);
            }

            sql += "')";
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
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }

        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ExportExcel(object sender, EventArgs e)
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
            int num = searchdateE.Subtract(searchdateB).Days;
            if (num < 0)
            {
                Response.Write("<Script Language=JavaScript>alert('结束时间不能晚于开始时间！');</Script>");
                return;
            }

            string strOPID = "50137";
            string strParam1 = ddlOPID.SelectedValue;//操作类型
            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";//大区
            string battleZone = string.Empty;//战区
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }
            string sql = string.Format("SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_{0}_{1}],'", bigZone, battleZone);

            for (int i = 0; i <= num; i++)
            {
                string strSTime = searchdateB.AddDays(i).ToString("yyyy_MM_dd");
                if (i == 0)
                    sql += string.Format("SELECT \"{0}\" AS DateTime,COUNT(DISTINCT CID) AS PeopleNum,COUNT(CID) AS Num FROM {1}_other_log WHERE OPID={2} AND PARA_1={3}", strSTime, strSTime, strOPID, strParam1);
                else
                    sql += string.Format(" UNION ALL SELECT \"{0}\" AS DateTime,COUNT(DISTINCT CID) AS PeopleNum,COUNT(CID) AS Num FROM {1}_other_log WHERE OPID={2} AND PARA_1={3}", strSTime, strSTime, strOPID, strParam1);
            }

            sql += "')";
          
            ds = DBHelperDigGameDB.Query(sql);
            ExportExcelHelper.ExportDataSet(ds);
        }
        /// <summary>
        /// 绑定大区
        /// </summary>
        public void BindDBigZone()
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
        /// <summary>
        /// 绑定战区
        /// </summary>
        /// <param name="id"></param>
        public void BindBattleZone(string id)
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
        #endregion

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
