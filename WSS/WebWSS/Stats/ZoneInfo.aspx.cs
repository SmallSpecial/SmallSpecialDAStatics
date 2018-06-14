using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace WebWSS.Stats
{
    public partial class ZoneInfo : System.Web.UI.Page
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
                bind();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            //            string sqlStr = @"SELECT DataContent
            //            FROM crossgsdatasave_baseinfo where NDataID=1";

            //            SqlParameter[] parameters = {
            //                    new SqlParameter("@BigZoneID", SqlDbType.Int),
            //                    new SqlParameter("@ZoneID", SqlDbType.Int),
            //                    new SqlParameter("@DBType", SqlDbType.Int),
            //                    new SqlParameter("@Query",SqlDbType.NVarChar),
            //                    new SqlParameter("@PageIndex", SqlDbType.Int),
            //                    new SqlParameter("@PageSize", SqlDbType.Int),
            //                    new SqlParameter("@PCount", SqlDbType.Int),
            //                    };
            //            parameters[0].Value = 1;
            //            parameters[1].Value = 1;
            //            parameters[2].Value = 3;//
            //            parameters[3].Value = sqlStr.Replace("'", "''");
            //            parameters[4].Value = 1;
            //            parameters[5].Value = 1000;
            //            parameters[6].Direction = ParameterDirection.Output;

            //            ds = DBHelperDigGameDB.RunProcedure("_Query_SQLCustom", parameters, "ds", 180);


            //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //            {
            //                byte[] by = (byte[])ds.Tables[0].Rows[i]["DataContent"];
            //                string temp = System.Text.Encoding.Default.GetString(by);
            //            }




            string sql = @"SELECT F_ID,F_BigZone,F_ZoneID,F_WSZ_CSD,F_XYL_CSD,F_TJM_CSD,F_FY_DataContent,F_FY_DateTime FROM T_ZoneInfo_Total with(nolock) WHERE F_Date='" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "'";

            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        byte[] by = (byte[])(ds.Tables[0].Rows[i]["F_FY_DataContent"]);
                        string temp = System.Text.Encoding.Default.GetString(by);
                    }
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


        public int GetLevel(byte[] byte_level)
        {
            if (byte_level.Length == 8)
            {
                return byte_level[4];
            }
            return 0;
        }


        public int GetDays(int level, DateTime levelDate, int bigZoneID, int zoneID)
        {
            try
            {
                int day01 = (DateTime.Now - levelDate).Days;
                string sqlStr = @"SELECT lockday FROM serverlocklevel_table where locklevel>" + level + " order by locklevel asc limit 0,1";

                SqlParameter[] parameters = {
                                new SqlParameter("@BigZoneID", SqlDbType.Int),
                                new SqlParameter("@ZoneID", SqlDbType.Int),
                                new SqlParameter("@DBType", SqlDbType.Int),
                                new SqlParameter("@Query",SqlDbType.NVarChar),
                                new SqlParameter("@PageIndex", SqlDbType.Int),
                                new SqlParameter("@PageSize", SqlDbType.Int),
                                new SqlParameter("@PCount", SqlDbType.Int),
                                };
                parameters[0].Value = bigZoneID;
                parameters[1].Value = 1;
                parameters[2].Value = 7;//
                parameters[3].Value = sqlStr.Replace("'", "''");
                parameters[4].Value = 1;
                parameters[5].Value = 1000;
                parameters[6].Direction = ParameterDirection.Output;

                ds = DBHelperDigGameDB.RunProcedure("_Query_SQLCustom", parameters, "ds", 180);
                int lockday = Convert.ToInt32(ds.Tables[0].Rows[0]["lockday"]);

                string sqlStr01 = @"SELECT lockday FROM serverlocklevel_table where locklevel=" + level + "";

                SqlParameter[] parameters01 = {
                                new SqlParameter("@BigZoneID", SqlDbType.Int),
                                new SqlParameter("@ZoneID", SqlDbType.Int),
                                new SqlParameter("@DBType", SqlDbType.Int),
                                new SqlParameter("@Query",SqlDbType.NVarChar),
                                new SqlParameter("@PageIndex", SqlDbType.Int),
                                new SqlParameter("@PageSize", SqlDbType.Int),
                                new SqlParameter("@PCount", SqlDbType.Int),
                                };
                parameters01[0].Value = bigZoneID;
                parameters01[1].Value = 1;
                parameters01[2].Value = 7;//
                parameters01[3].Value = sqlStr01.Replace("'", "''");
                parameters01[4].Value = 1;
                parameters01[5].Value = 1000;
                parameters01[6].Direction = ParameterDirection.Output;

                ds = DBHelperDigGameDB.RunProcedure("_Query_SQLCustom", parameters01, "ds", 180);
                int lockday01 = Convert.ToInt32(ds.Tables[0].Rows[0]["lockday"]);
                int returnValue = lockday - lockday01 - day01;
                if (returnValue < 0)
                    return 0;
                else
                    return returnValue;
            }
            catch (Exception)
            {
                return 0;
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

        /// <summary>
        /// 得到类型名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetTypeName(DropDownList ddl, string value)
        {
            ListItem item = ddl.Items.FindByValue(value);
            return item == null ? value : item.Text;
        }

        /// <summary>
        /// 得到大区名称
        /// </summary>
        public string GetBigZoneName(object bigzone)
        {
            try
            {
                if (bigzone == null)
                {
                    return "";
                }
                string sql = string.Format("select top 1 F_Name from T_GameConfig with(nolock) where  (F_ParentID = 1000) and F_ValueGame='{0}'", bigzone);
                return DBHelperGSSDB.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return bigzone.ToString();
            }
        }

        /// <summary>
        /// 得到战区名称
        /// </summary>
        public string GetZoneName(object bigzone, object zone)
        {
            try
            {
                if (bigzone == null || zone == null)
                {
                    return "";
                }
                string sql = string.Format("select top 1 F_Name from T_GameConfig with(nolock) where  F_ValueGame='{0}' and F_ParentID = (select top 1 F_ID from T_GameConfig with(nolock) where  (F_ParentID = 1000) and F_ValueGame={1}) ", zone, bigzone);
                return DBHelperGSSDB.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return zone.ToString();
            }
        }

        /// <summary>
        /// 昨日登陆角色数
        /// </summary>
        public string GetRoleCount(object bigzone, object zone)
        {
            try
            {
                if (bigzone == null || zone == null)
                {
                    return "";
                }
                string sql = string.Format("SELECT SUM(F_InRoleCount) as F_InRoleCount FROM T_Other_GSLog_Total where F_OPID=40052 and F_Date='" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "' and F_BigZone=" + bigzone + " and F_ZoneID=" + zone);
                object temp = DBHelperDigGameDB.GetSingle(sql);
                if (temp == null)
                    return "0";
                else
                    return temp.ToString();
            }
            catch (System.Exception ex)
            {
                return "0";
            }
        }

        /// <summary>
        /// 昨日最高在线数
        /// </summary>
        public string GetMaxRoleCount(object bigzone, object zone)
        {
            try
            {
                if (bigzone == null || zone == null)
                {
                    return "";
                }
                string sql = string.Format("SELECT SUM(F_PlayerNumOnline) as F_PlayerNumOnline FROM (SELECT Max(F_PlayerNumOnline) as F_PlayerNumOnline FROM T_RoleOnLineFlow where F_Date='" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "' and F_BigZone=" + bigzone + " and F_ZoneID=" + zone) + " group by F_GNGSID) as temp";
                object temp = DBHelperDigGameDB.GetSingle(sql);
                if (temp == null)
                    return "0";
                else
                    return temp.ToString();
            }
            catch (System.Exception ex)
            {
                return "0";
            }
        }

        /// <summary>
        /// 昨日总充值数
        /// </summary>
        public string GetMaxMoneyCount(object bigzone, object zone)
        {
            try
            {
                if (bigzone == null || zone == null)
                {
                    return "";
                }
                string sql = string.Format("SELECT SUM(F_MoneyCount) as F_TradeMoney  FROM T_Money_Recharge_Day WHERE  F_Date='" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "' and F_BigZone=" + bigzone + " and F_ZoneID=" + zone);

                ds = DBHelperDigGameDB.Query(sql);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["F_TradeMoney"].ToString().Trim()))
                    return ds.Tables[0].Rows[0]["F_TradeMoney"].ToString();
                else
                    return "0";
            }
            catch (System.Exception ex)
            {
                return "0";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string GetHuoYueCount(object bigzone, object zone, int huoYueType)
        {
            try
            {
                if (bigzone == null || zone == null)
                {
                    return "";
                }
                string sql = string.Empty;
                if (huoYueType == 1)
                    sql = string.Format("SELECT SUM(F_Item01Count) FROM T_HuoYueZhi_GSLog_Rank where F_Date='" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "' and F_BigZone=" + bigzone + " and F_ZoneID=" + zone);
                if (huoYueType == 2)
                    sql = string.Format("SELECT SUM(F_Item02Count) FROM T_HuoYueZhi_GSLog_Rank where F_Date='" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "' and F_BigZone=" + bigzone + " and F_ZoneID=" + zone);
                if (huoYueType == 3)
                    sql = string.Format("SELECT SUM(F_Item03Count) FROM T_HuoYueZhi_GSLog_Rank where F_Date='" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "' and F_BigZone=" + bigzone + " and F_ZoneID=" + zone);
                if (huoYueType == 4)
                    sql = string.Format("SELECT SUM(F_Item04Count) FROM T_HuoYueZhi_GSLog_Rank where F_Date='" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "' and F_BigZone=" + bigzone + " and F_ZoneID=" + zone);
                if (sql.Length > 0)
                    return DBHelperDigGameDB.GetSingle(sql).ToString();
                else
                    return "0";
            }
            catch (System.Exception ex)
            {
                return "0";
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
                    string loginRoleCount = (e.Row.FindControl("lblLoginRoleCount") as Label).Text;
                    if (loginRoleCount.Trim() == "0" || loginRoleCount.Trim() == "")
                        e.Row.Visible = false;
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

    }
}
