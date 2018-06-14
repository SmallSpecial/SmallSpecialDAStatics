using System;
using System.Data;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Text;

namespace WebWSS.Stats
{
    public partial class ShopSaleItem_Goods_Day_TradeCount : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        DataSet queryDateDS;
        protected void Page_Load(object sender, EventArgs e)
        {

            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            ControlMonthSelect1.SelectDateChanged += new EventHandler(ControlDateSelect_SelectDateChanged);
            if (!IsPostBack)
            {
                tboxTimeB.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                BindDdl1();
                DropDownListArea1_SelectedIndexChanged(null, null);
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



            string queryDateSql = @"select f_Date from T_ShopSale where F_ShopType=0 AND F_Date>='" + searchdateB.ToString(SmallDateTimeFormat) + "' and F_Date<='" + searchdateE.ToString(SmallDateTimeFormat) + "'";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                queryDateSql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            queryDateSql += " GROUP BY F_Date order by F_Date";
            queryDateDS = DBHelperDigGameDB.Query(queryDateSql);
            DataView queryDateView = queryDateDS.Tables[0].DefaultView;
            StringBuilder inSqlBuilder = new StringBuilder();
            if (queryDateView != null && queryDateView.Count > 0)
            {
                for (int i = 0; i < queryDateView.Count; i++)
                {
                    if (i == queryDateView.Count - 1)
                        inSqlBuilder.AppendFormat("[{0}]", Convert.ToDateTime(queryDateView[i]["f_Date"]).ToString("yyyy-MM-dd"));
                    else
                        inSqlBuilder.AppendFormat("[{0}],", Convert.ToDateTime(queryDateView[i]["f_Date"]).ToString("yyyy-MM-dd"));
                }
                string inSql = inSqlBuilder.ToString();
                string sql = @"SELECT F_ItemExcelID,F_ItemChildNum," + inSql + "FROM (SELECT F_ItemExcelID,F_ItemChildNum,F_Date, SUM(F_TradeCount) AS F_TradeCount FROM T_ShopSaleItem with(nolock) where F_ShopType=0 AND F_Date>='" + searchdateB.ToString(SmallDateTimeFormat)   + "' and F_Date<='" + searchdateE.ToString(SmallDateTimeFormat)   + "' ";

                if (DropDownListArea1.SelectedIndex > 0)
                {
                    sql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
                }
                sql += " GROUP BY F_Date,F_ItemExcelID,F_ItemChildNum) as TempData pivot (sum(F_TradeCount) for F_Date in (" + inSql + ")) as ourpivot order by F_ItemExcelID";
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
            else
            {
                lblerro.Visible = true;
                GridView1.DataSource = null;
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

                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, true, 0, 0);
            }
        }

        /// <summary>
        /// 得到类型名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetTypeNameT(DropDownList ddl, object value)
        {
            try
            {
                if (value == null)
                {
                    return "";
                }
                foreach (ListItem itemT in ddl.Items)
                {
                    if (itemT.Value.Substring(itemT.Value.IndexOf(',') + 1) == value.ToString())
                    {
                        return itemT.Text;
                    }
                }
                return value.ToString();
            }
            catch (System.Exception ex)
            {
                return value.ToString();
            }

        }

        //得到文本名称
        public string GetTextName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT top 1 F_Name  FROM T_BaseGameName WHERE (F_ExcelID = " + value + ") ";
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

        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
        System.Collections.Generic.Dictionary<int, int> dicSum = new System.Collections.Generic.Dictionary<int, int>();
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");

                    for (int i = 3; i < e.Row.Cells.Count; i++)
                    {
                        string numStr = e.Row.Cells[i].Text.Replace(",", "").Replace("&nbsp;", "").Trim();
                        if (numStr.Length == 0)
                        {
                            e.Row.Cells[i].Text = "0";
                            continue;
                        }
                        int num = Convert.ToInt32(numStr);
                        if (dicSum.ContainsKey(i))
                        {
                            dicSum[i] += num;
                        }
                        else
                        {
                            dicSum.Add(i, num);
                        }
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = App_GlobalResources.Language.LblSum;

                    for (int i = 3; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Text = dicSum[i].ToString("#,#0");
                    }
                }

                if (e.Row.Cells[1].Visible)
                {
                }

                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;


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
            ControlMonthSelect1.SetSelectDate(tboxTimeB.Text, tboxTimeE.Text);
            bind();
        }

        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListArea2.Items.Clear();
            BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);

        }



    }
}
