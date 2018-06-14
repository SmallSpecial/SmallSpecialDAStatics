using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.ShoppingMallLog
{
    public partial class GrowthFund : Admin_Page
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
                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
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

            DataTable dt = new DataTable();
            dt.Columns.Add("DateTime", System.Type.GetType("System.String"));
            dt.Columns.Add("Type", System.Type.GetType("System.String"));
            dt.Columns.Add("SoldNum", System.Type.GetType("System.String"));
            dt.Columns.Add("ConsumeNum", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases1", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases2", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases3", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases4", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases5", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases6", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases7", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases8", System.Type.GetType("System.String"));

            int n = 0;
            for (int m = 0; m <= num; m++)
            {
                //日期
                string strSTime = searchdateB.AddDays(m).ToString("yyyy_MM_dd");

                #region 查询商品名称；销售数量；消费元宝数
                string sql = string.Format("SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_{0}_{1}],'SELECT \"{2}\" AS DateTime,(CASE WHEN PARA_2=-980 THEN 0 WHEN PARA_2=-2880 THEN 1 ELSE \"\" END) TYPE,SoldNum,ConsumeNum FROM (SELECT PARA_2,COUNT(CID) SoldNum,-SUM(PARA_2) ConsumeNum FROM {3}_gold_log WHERE PARA_1=63 AND PARA_2<0 GROUP BY PARA_2) TEMP ORDER BY TYPE')", bigZone, battleZone, strSTime, strSTime);
                ds = DBHelperDigGameDB.Query(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow newRow;
                    newRow = dt.NewRow();
                    newRow["DateTime"] = ds.Tables[0].Rows[i]["DateTime"];
                    newRow["Type"] = ds.Tables[0].Rows[i]["Type"];
                    newRow["SoldNum"] = ds.Tables[0].Rows[i]["SoldNum"];
                    newRow["ConsumeNum"] = ds.Tables[0].Rows[i]["ConsumeNum"];
                    dt.Rows.Add(newRow);
                }
                #region 如果缺少某类基金，补全
                if (dt.Rows.Count==n)
                {
                    DataRow newRow;
                    newRow = dt.NewRow();
                    newRow["DateTime"] = strSTime;
                    newRow["Type"] = "1";
                    newRow["SoldNum"] = "0";
                    newRow["ConsumeNum"] = "0";
                    dt.Rows.Add(newRow);

                    DataRow newRow1;
                    newRow1 = dt.NewRow();
                    newRow1["DateTime"] = strSTime;
                    newRow1["Type"] = "0";
                    newRow1["SoldNum"] = "0";
                    newRow1["ConsumeNum"] = "0";
                    dt.Rows.Add(newRow1);
                }
                else if(dt.Rows.Count==n+1)
                {
                    if(dt.Rows[n]["TYPE"].ToString()=="0")
                    {
                        DataRow newRow;
                        newRow = dt.NewRow();
                        newRow["DateTime"] = strSTime;
                        newRow["Type"] = "1";
                        newRow["SoldNum"] = "0";
                        newRow["ConsumeNum"] = "0";
                        dt.Rows.Add(newRow);
                    }
                    else if(dt.Rows[n]["TYPE"].ToString()=="1")
                    {
                        DataRow newRow;
                        newRow = dt.NewRow();
                        newRow["DateTime"] = strSTime;
                        newRow["Type"] = "0";
                        newRow["SoldNum"] = "0";
                        newRow["ConsumeNum"] = "0";
                        dt.Rows.Add(newRow);
                    }
                }
                #endregion

                #endregion

                //查询各个阶段领取次数
                string sql1 = string.Format("SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_{0}_{1}],'SELECT * FROM(SELECT \"{2}\" AS DateTime,SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",3),\"	\",-1) TYPE,SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",4),\"	\",-1) SUBTYPE,COUNT(SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",4),\"	\",-1)) SUBNUM FROM {3}_gold_log WHERE PARA_1=63 AND PARA_2>0 GROUP BY SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",3),\"	\",-1),SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",4),\"	\",-1)) TEMP ORDER BY TYPE')", bigZone, battleZone, strSTime, strSTime);
                ds = DBHelperDigGameDB.Query(sql1);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    #region 各阶段成长基金赋值
                    if (ds.Tables[0].Rows[j]["TYPE"].ToString() == "0")//基础
                    {
                        switch (ds.Tables[0].Rows[j]["SUBTYPE"].ToString())
                        {
                            case "1":
                                dt.Rows[n]["Phases1"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "2":
                                dt.Rows[n]["Phases2"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "3":
                                dt.Rows[n]["Phases3"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "4":
                                dt.Rows[n]["Phases4"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "5":
                                dt.Rows[n]["Phases5"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "6":
                                dt.Rows[n]["Phases6"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "7":
                                dt.Rows[n]["Phases7"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "8":
                                dt.Rows[n]["Phases8"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                        }
                    }
                    else if (ds.Tables[0].Rows[j]["TYPE"].ToString() == "1")//高阶
                    {
                        switch (ds.Tables[0].Rows[j]["SUBTYPE"].ToString())
                        {
                            case "1":
                                dt.Rows[n + 1]["Phases1"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "2":
                                dt.Rows[n + 1]["Phases2"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "3":
                                dt.Rows[n + 1]["Phases3"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "4":
                                dt.Rows[n + 1]["Phases4"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "5":
                                dt.Rows[n + 1]["Phases5"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "6":
                                dt.Rows[n + 1]["Phases6"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "7":
                                dt.Rows[n + 1]["Phases7"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "8":
                                dt.Rows[n + 1]["Phases8"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                        }
                    }
                    #endregion
                }
                n += 2;
            }

                try
                {
                    DataView myView = dt.DefaultView;
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

            DataTable dt = new DataTable();
            dt.Columns.Add("DateTime", System.Type.GetType("System.String"));
            dt.Columns.Add("Type", System.Type.GetType("System.String"));
            dt.Columns.Add("SoldNum", System.Type.GetType("System.String"));
            dt.Columns.Add("ConsumeNum", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases1", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases2", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases3", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases4", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases5", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases6", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases7", System.Type.GetType("System.String"));
            dt.Columns.Add("Phases8", System.Type.GetType("System.String"));

            int n = 0;
            for (int m = 0; m <= num; m++)
            {
                //日期
                string strSTime = searchdateB.AddDays(m).ToString("yyyy_MM_dd");

                #region 查询商品名称；销售数量；消费元宝数
                string sql = string.Format("SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_{0}_{1}],'SELECT \"{2}\" AS DateTime,(CASE WHEN PARA_2=-980 THEN 0 WHEN PARA_2=-2880 THEN 1 ELSE \"\" END) TYPE,SoldNum,ConsumeNum FROM (SELECT PARA_2,COUNT(CID) SoldNum,-SUM(PARA_2) ConsumeNum FROM {3}_gold_log WHERE PARA_1=63 AND PARA_2<0 GROUP BY PARA_2) TEMP ORDER BY TYPE')", bigZone, battleZone, strSTime, strSTime);
                ds = DBHelperDigGameDB.Query(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow newRow;
                    newRow = dt.NewRow();
                    newRow["DateTime"] = ds.Tables[0].Rows[i]["DateTime"];
                    newRow["Type"] = ds.Tables[0].Rows[i]["Type"];
                    newRow["SoldNum"] = ds.Tables[0].Rows[i]["SoldNum"];
                    newRow["ConsumeNum"] = ds.Tables[0].Rows[i]["ConsumeNum"];
                    dt.Rows.Add(newRow);
                }
                //如果缺少某类基金，补全
                if(dt.Rows.Count==n+1)
                {
                    if(dt.Rows[n]["TYPE"].ToString()=="0")
                    {
                        DataRow newRow;
                        newRow = dt.NewRow();
                        newRow["DateTime"] = strSTime;
                        newRow["Type"] = "1";
                        newRow["SoldNum"] = "0";
                        newRow["ConsumeNum"] = "0";
                        dt.Rows.Add(newRow);
                    }
                    else if(dt.Rows[n]["TYPE"].ToString()=="1")
                    {
                        DataRow newRow;
                        newRow = dt.NewRow();
                        newRow["DateTime"] = strSTime;
                        newRow["Type"] = "0";
                        newRow["SoldNum"] = "0";
                        newRow["ConsumeNum"] = "0";
                        dt.Rows.Add(newRow);
                    }
                }
                #endregion

                //查询各个阶段领取次数
                string sql1 = string.Format("SELECT * FROM OPENQUERY ([LKSV_0_GSLOG_DB_{0}_{1}],'SELECT * FROM(SELECT \"{2}\" AS DateTime,SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",3),\"	\",-1) TYPE,SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",4),\"	\",-1) SUBTYPE,COUNT(SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",4),\"	\",-1)) SUBNUM FROM {3}_gold_log WHERE PARA_1=63 AND PARA_2>0 GROUP BY SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",3),\"	\",-1),SUBSTRING_INDEX(SUBSTRING_INDEX(OP_BAK,\"	\",4),\"	\",-1)) TEMP ORDER BY TYPE')", bigZone, battleZone, strSTime, strSTime);
                ds = DBHelperDigGameDB.Query(sql1);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    #region 各阶段成长基金赋值
                    if (ds.Tables[0].Rows[j]["TYPE"].ToString() == "0")//基础
                    {
                        switch (ds.Tables[0].Rows[j]["SUBTYPE"].ToString())
                        {
                            case "1":
                                dt.Rows[n]["Phases1"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "2":
                                dt.Rows[n]["Phases2"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "3":
                                dt.Rows[n]["Phases3"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "4":
                                dt.Rows[n]["Phases4"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "5":
                                dt.Rows[n]["Phases5"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "6":
                                dt.Rows[n]["Phases6"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "7":
                                dt.Rows[n]["Phases7"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "8":
                                dt.Rows[n]["Phases8"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                        }
                    }
                    else if (ds.Tables[0].Rows[j]["TYPE"].ToString() == "1")//高阶
                    {
                        switch (ds.Tables[0].Rows[j]["SUBTYPE"].ToString())
                        {
                            case "1":
                                dt.Rows[n + 1]["Phases1"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "2":
                                dt.Rows[n + 1]["Phases2"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "3":
                                dt.Rows[n + 1]["Phases3"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "4":
                                dt.Rows[n + 1]["Phases4"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "5":
                                dt.Rows[n + 1]["Phases5"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "6":
                                dt.Rows[n + 1]["Phases6"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "7":
                                dt.Rows[n + 1]["Phases7"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                            case "8":
                                dt.Rows[n + 1]["Phases8"] = ds.Tables[0].Rows[j]["SUBNUM"].ToString();
                                break;
                        }
                    }
                    #endregion
                }
                n += 2;
            }

            ds.Tables.Add(dt);
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
        protected string GetType(string value)
        {
            switch(value)
            {
                case "0":
                    return "新手基金";
                case "1":
                    return "成长基金";
                default:
                    return value;
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
