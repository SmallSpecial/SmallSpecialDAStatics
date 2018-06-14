using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.Stats
{
    public partial class UserAccountInterzone : Admin_Page
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
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlMonthSelect1.SelectDateB.ToString("yyyy-MM-dd 00:00:00");
            tboxTimeE.Text = ControlMonthSelect1.SelectDateE.ToString("yyyy-MM-dd 23:59:59");
            bind();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
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

            try
            {

                SqlParameter[] parameters = {
                    new SqlParameter("@DigDate", SqlDbType.DateTime),
                    new SqlParameter("@DigDateEnd", SqlDbType.DateTime),
                    new SqlParameter("@Result", SqlDbType.Int),
                    };
                parameters[0].Value = searchdateB;
                parameters[1].Value = searchdateE.AddDays(1);
                parameters[2].Direction = ParameterDirection.Output;

                DBHelperDigGameDB.RunProcedure("_Dig_UserCoreDB_AccountInterzone", parameters, "ds", 500);
                int result = Convert.ToInt32(parameters[2].Value);
                string sqlStr = "select AccountInterzone,UserCount from T_AccountInterzone";
                ds = DBHelperDigGameDB.Query(sqlStr);
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
                    if (e.Row.DataItem != null)
                    {
                        int loginNum = GetAccountUserCount();
                        int userCount = 0;
                        if (Int32.TryParse(e.Row.Cells[1].Text, out userCount))
                        {
                            if (userCount != 0 && loginNum != 0)
                            {
                                decimal baiFenBi_Float = Convert.ToDecimal(userCount) / loginNum;

                                string baiFenBi_String = (Math.Round(baiFenBi_Float, 4) * 100).ToString().Replace("00","") + "%";
                                e.Row.Cells[2].Text = baiFenBi_String;
                            }
                        }
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


        /// <summary>
        /// 得到充值人数
        /// </summary>
        public Int32 GetAccountUserCount()
        {
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
            string sqlStr = @"SELECT Count(distinct F_UserId) as UserCountAll
            FROM T_UserAccountDepositLog with(nolock) where F_AfterMoney>0";
            DateTime searchdateB01 = DateTime.Now;
            DateTime searchdateE01 = DateTime.Now;
            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB01 = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE01 = Convert.ToDateTime(tboxTimeE.Text);
            }
            sqlStr += " and F_TradeTime>='" + searchdateB01.ToString("yyyy/MM/dd 00:00:00") + "' and F_TradeTime<='" + searchdateE01.ToString("yyyy/MM/dd 23:59:59") + "' ";

            try
            {
                int pcount = 0;

                SqlParameter[] parameters = {
                    new SqlParameter("@BigZoneID", SqlDbType.Int),
                    new SqlParameter("@ZoneID", SqlDbType.Int),
                    new SqlParameter("@DBType", SqlDbType.Int),
                    new SqlParameter("@Query",SqlDbType.NVarChar),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PCount", SqlDbType.Int),
                    };
                parameters[0].Value = BigZoneID;
                parameters[1].Value = "-1";
                parameters[2].Value = 5;//UserCoreDB
                parameters[3].Value = sqlStr.Replace("'", "''");
                parameters[4].Value = 1;
                parameters[5].Value = 1000;
                parameters[6].Direction = ParameterDirection.Output;

                ds = DBHelperDigGameDB.RunProcedure("_Query_SQLCustom", parameters, "ds", 180);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["UserCountAll"]);
                }
                return 0;

            }
            catch (System.Exception ex)
            {
                return 0;
            }

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
