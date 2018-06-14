using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.Stats
{
    public partial class MoneyGoldBindRank_SumRole_Tiqu : Admin_Page
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
            ControlDateSelect.SelectDateChanged += new EventHandler(ControlDateSelect_SelectDateChanged);
            if (!IsPostBack)
            {
                tboxTimeB.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                BindDdl1();
                BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);
                bind();
            }
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect.SelectDate.ToString("yyyy-MM-dd");
            bind();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            DateTime searchdateB = DateTime.Now;
            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }

            string sqlStr = string.Format(@"SELECT UID,CID,sum(PARA_2) as GoldSum,count(1) as GoldTime FROM {0:yyyy_MM_dd}_gold_log ", searchdateB);

            sqlStr += " where 1=1";

            if (Common.Validate.IsInt(tboxUserID.Text))
            {
                sqlStr += @" and UID=" + tboxUserID.Text + "";
            }

            sqlStr += @" and OPID=10056
                        and PARA_2>0
                        group by CID
                        order by GoldSum desc";

            try
            {
                int pcount = 0;
                string bigzoneid = DropDownListArea1.SelectedValue.Split(',')[1];
                string zoneid = DropDownListArea2.SelectedValue.Split(',')[1];

                SqlParameter[] parameters = {
                    new SqlParameter("@BigZoneID", SqlDbType.Int),
                    new SqlParameter("@ZoneID", SqlDbType.Int),
                    new SqlParameter("@DBType", SqlDbType.Int),
                    new SqlParameter("@Query",SqlDbType.NVarChar),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PCount", SqlDbType.Int),
                    };
                parameters[0].Value = bigzoneid;
                parameters[1].Value = zoneid;
                parameters[2].Value = 0;//gslog_db
                parameters[3].Value = sqlStr.Replace("'", "''");
                parameters[4].Value = 1;
                parameters[5].Value = 1000;
                parameters[6].Direction = ParameterDirection.Output;

                ds = DBHelperDigGameDB.RunProcedure("_Query_SQLCustom", parameters, "ds",180);


                DataView myView = ds.Tables[0].DefaultView;
                pcount = (int)parameters[6].Value;

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
               // lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }

        }

        //得到角色名称
        public string GetRoleName(string bigzone, string roleid)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT  F_RoleName FROM T_BaseRoleCreate with(nolock) WHERE (F_BigZoneID =" + bigzone + ") AND (F_RoleID = " + roleid + ")  ";
                return string.Format("({0}) {1}", roleid, spg.GetSingle(sql));
            }
            catch (System.Exception ex)
            {
                return roleid;
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
                //DataRow newdr = ds.Tables[0].NewRow();
                //newdr["F_Name"] = App_GlobalResources.Language.LblAllBigZone;
                //newdr["F_ValueGame"] = "";
                //ds.Tables[0].Rows.InsertAt(newdr, 0);
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
                //DataRow newdr = ds.Tables[0].NewRow();
                //newdr["F_Name"] = App_GlobalResources.Language.LblAllZone;

                //newdr["F_ValueGame"] = "";
                //ds.Tables[0].Rows.InsertAt(newdr, 0);
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
        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListArea2.Items.Clear();
            BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);
        }

        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
     //   System.Collections.Generic.Dictionary<int, int> dicSum = new System.Collections.Generic.Dictionary<int, int>();
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");

                    //for (int i = 1; i < 3; i++)
                    //{
                    //    int num = Convert.ToInt32(e.Row.Cells[i].Text);
                    //    if (dicSum.ContainsKey(i))
                    //    {
                    //        dicSum[i] += num;
                    //    }
                    //    else
                    //    {
                    //        dicSum.Add(i, num);
                    //    }
                    //}
                }
                //else if (e.Row.RowType == DataControlRowType.Footer)
                //{

                //    e.Row.Cells[0].Text = "总 计";

                //    for (int i = 1; i < 3; i++)
                //    {
                //        e.Row.Cells[i].Text = dicSum[i].ToString();
                //    }
                //}

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
            ControlDateSelect.SetSelectDate(tboxTimeB.Text);
            bind();
        }
    }
}
