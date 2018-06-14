using System;
using System.Data;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class ShopSaleB_DayALL : Admin_Page
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
            ControlMonthSelect1.SelectDateChanged += new EventHandler(ControlDateSelect_SelectDateChanged);
            if (!IsPostBack)
            {
                tboxTimeB.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
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
            sql = @"
SELECT     F_Date, SUM(F_TradeCount) AS F_TradeCount, SUM(F_RoleCount) AS F_RoleCount, SUM(F_MoneyCount) AS F_MoneyCount
FROM         T_ShopSale with(nolock)
where F_ShopType=1 AND F_Date>='" + searchdateB.ToString(SmallDateTimeFormat)   + "' and F_Date<='" + searchdateE.ToString(SmallDateTimeFormat)   + "' ";

            sql += " GROUP BY F_Date order by F_Date";

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


        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
        System.Collections.Generic.Dictionary<int, int> dicSum = new System.Collections.Generic.Dictionary<int, int>();
        System.Drawing.Color color1 = System.Drawing.Color.White;
        System.Drawing.Color color2 = System.Drawing.Color.FromArgb(209,221,241);
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");

                    if (e.Row.RowIndex == 0)
                    {
                        e.Row.BackColor = color1;
                    }
                    else if (e.Row.Cells[0].Text == GridView1.Rows[e.Row.RowIndex - 1].Cells[0].Text)
                    {
                        e.Row.BackColor = GridView1.Rows[e.Row.RowIndex - 1].BackColor;
                    }
                    else
                    {
                        e.Row.BackColor = GridView1.Rows[e.Row.RowIndex - 1].BackColor == color1 ? color2 : color1;
                    }

                    for (int i = 3; i < 6; i++)
                    {
                        int num = Convert.ToInt32(e.Row.Cells[i].Text.Replace(",", ""));
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

                    for (int i = 3; i < 6; i++)
                    {
                        e.Row.Cells[i].Text = dicSum[i].ToString("#,#0");
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
            ControlMonthSelect1.SetSelectDate(tboxTimeB.Text, tboxTimeE.Text);
            bind();
        }
    }
}
