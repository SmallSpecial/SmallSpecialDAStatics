using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
namespace WebWSS.StartNotice
{
    public partial class StartNoticeList : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];


        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();
        DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;

            GetGameCoreDBString();

            if (!IsPostBack)
            {
                bind(0);
            }
        }

        #region GridView1 窗体事件


        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind(int type = 0)
        {
            // string sqlwhere = " where 1=1 ";
            try
            {

                #region 调用通用存储过程
                //string sqlwhere = " where 1=1 ";
                //int pcount = 0;
                //int psize = 20;

                //if (type == 0)
                //{
                //    psize = GridView1.PageSize;
                //}
                //if (!string.IsNullOrEmpty(txtRoleName.Text))
                //{
                //    QureyCDKeyByRole(txtRoleName.Text, psize, int.Parse(lblPageIndex.Text));
                //    return;
                //}
                //SqlParameter[] parameters = {
                //    new SqlParameter("@BigZoneID", SqlDbType.Int),
                //    new SqlParameter("@ZoneID", SqlDbType.Int),
                //    new SqlParameter("@DBType", SqlDbType.Int),
                //    new SqlParameter("@QueryTable",SqlDbType.NVarChar,50),
                //    new SqlParameter("@Query",SqlDbType.NVarChar,100),
                //    new SqlParameter("@OrderStr",SqlDbType.NVarChar,100),
                //    new SqlParameter("@PageIndex", SqlDbType.Int),
                //    new SqlParameter("@PageSize", SqlDbType.Int),
                //    new SqlParameter("@PCount", SqlDbType.Int),
                //    };
                //parameters[0].Value = BigZoneID;
                //parameters[1].Value = -1;
                //parameters[2].Value = 2;
                //parameters[3].Value = "T_STARTNOTICE with(nolock)";
                //parameters[4].Value = sqlwhere;
                //parameters[5].Value = " order by F_ID desc";
                //parameters[6].Value = lblPageIndex.Text;
                //parameters[7].Value = psize;
                //parameters[8].Direction = ParameterDirection.Output;
                // ds = DBHelperDigGameDB.RunProcedure("_Query_SQLSERVER", parameters, "ds", 180);
                //pcount = (int)parameters[8].Value;
                //ds = DBHelperDigGameDB.RunProcedure("_Query_SQLSERVER", parameters, "ds", 180);
                //pcount = (int)parameters[8].Value; 
                #endregion

                string sql = @"
SELECT * FROM  (  SELECT ROW_NUMBER() OVER( ORDER  BY F_ID  DESC ) AS ROWNUM, [F_ID]
      ,[F_TITLE]
      ,[F_NOTICEINFO]
      ,CONVERT(NVARCHAR(19),F_STARTTIME, 20) AS [F_STARTTIME]
      ,CONVERT(NVARCHAR(19),F_ENDTIME, 20) AS [F_ENDTIME]
      ,CONVERT(NVARCHAR(19),F_CREATETIME, 20) AS [F_CREATETIME] 
FROM T_STARTNOTICE  with(nolock) ) A 
  WHERE   ROWNUM BETWEEN @pa AND @pb ;
select count(1) as cou from  T_STARTNOTICE  with(nolock) ;

";
                SqlParameter[] parameters = {
                    new SqlParameter("@pa", SqlDbType.Int),
                    new SqlParameter("@pb", SqlDbType.Int),
                    };
                int index = 1;
                if (Int32.TryParse(lblPageIndex.Text, out index)) { }

                parameters[0].Value = (index - 1) * GridView1.PageSize + 1;
                parameters[1].Value = (index) * GridView1.PageSize;
                ds = DBHelperGameCoreDB.Query(sql, parameters);

                //总行数
                int pcount = 0;
                pcount = Convert.ToInt32(ds.Tables[1].Rows[0]["cou"]);

                BindGridView(ds, GridView1, pcount, type);
            }
            catch (System.Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }

        }
        #endregion

        /// <summary>
        /// 行编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;

            bind();
        }

        //行更新
        /// <summary>
        /// 行更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            TextBox tb = (TextBox)(GridView1.Rows[e.RowIndex].FindControl("txtbox"));

            string res = tb.Text;

            string sqlstr = "update T_STARTNOTICE set F_TITLE= N'"
           + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() + "',F_NOTICEINFO=N'"
           + res + "',F_STARTTIME='"
           + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim() + "',F_ENDTIME='"
           + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim()
           + "' where F_ID='"
           + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            ExecuteAndAlter(sqlstr);

            //退出编辑状态
            GridView1.EditIndex = -1;

            GridView1.DataBind();
            bind();
        }

        /// <summary>
        /// 执行sql并弹窗
        /// </summary>
        /// <param name="sql"></param>
        private void ExecuteAndAlter(string sql)
        {
            int result = DBHelperGameCoreDB.ExecuteSql(sql);
            if (result > 0)
            {
                //this.Response.Write("<script language=javascript>ALTER('操作成功')</script>");
            }
            else
            {

            }
        }

        //行删除事件
        /// <summary>
        /// 行删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sqlstr = "delete from T_STARTNOTICE where f_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";

            ExecuteAndAlter(sqlstr);

            GridView1.DataBind();
            bind();
        }

        /// <summary>
        /// 行取消编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
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
                    if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit))
                    {
                        HiddenField hfTasksToRole = (HiddenField)e.Row.FindControl("hfTasksToRole");
                        TextBox tb = (TextBox)e.Row.FindControl("txtbox");
                        if (hfTasksToRole != null && tb != null)
                        {
                            tb.Text = hfTasksToRole.Value;
                        }
                    }
                    else
                    {
                        e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                        e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");

                        LinkButton btn = ((LinkButton)e.Row.Cells[7].Controls[0]);
                        if (btn != null)
                        {
                            btn.Attributes.Add("onclick", "javascript:return confirm('confirmed to delete?')");
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
            bind(0);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind(0);
        }

        #endregion 窗体事件


        #region 按钮事件
        /// <summary>
        /// 新增一条公告,标题内容空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string datetime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string sqlstr = @"insert into T_STARTNOTICE 
([F_TITLE]
,[F_NOTICEINFO]
,[F_STARTTIME]
,[F_ENDTIME]
,[F_CREATETIME]
)
values ('','','" + datetime + "','" + datetime + "','" + datetime + "')";
            ExecuteAndAlter(sqlstr);

            GridView1.DataBind();
            bind();
            // + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() + "',F_CREATETIME='"             + DateTime.Now.ToShortTimeString()


        }


        /// <summary>
        /// 查询按钮 隐藏了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = "1";
            bind(0);
        }

        #endregion


        #region 方法

        /// <summary>
        /// 绑定刷新页面
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="grid"></param>
        /// <param name="pcount"></param>
        /// <param name="type"></param>
        private void BindGridView(DataSet ds, GridView grid, int pcount, int type)
        {

            DataView myView = ds.Tables[0].DefaultView;
            if (myView.Count == 0)
            {
                lblerro.Visible = true;
                myView.AddNew();

                lblCount.Text = pcount.ToString();
                lblPageCount.Text = (pcount % GridView1.PageSize == 0 ? pcount / GridView1.PageSize : pcount / GridView1.PageSize + 1).ToString();
            }
            else
            {
                lblerro.Visible = false;
                lblPageCount.Text = (pcount % GridView1.PageSize == 0 ? pcount / GridView1.PageSize : pcount / GridView1.PageSize + 1).ToString();
                lblCount.Text = pcount.ToString();

                lbtnF.Enabled = true;
                lbtnP.Enabled = true;
                lbtnN.Enabled = true;
                lbtnE.Enabled = true;
                if (lblPageIndex.Text == "1")
                {
                    lbtnF.Enabled = false;
                    lbtnP.Enabled = false;
                }
                else if (lblPageIndex.Text == lblPageCount.Text)
                {
                    lbtnN.Enabled = false;
                    lbtnE.Enabled = false;
                }
                tboxPageIndex.Text = lblPageIndex.Text;
            }

            grid.DataSource = myView;
            grid.DataBind();
        }

        #region  初始化gamecoredb连接字符串
        /// <summary>
        /// 初始化gamecoredb连接字符串
        /// </summary>
        private void GetGameCoreDBString()
        {
            //查询GameCoreDB所在服务器ip    F_DBType= 2  and F_BigZoneID=0 and F_BattleZoneID=-1 and F_DBName='GameCoreDB'
            string sql = @" select top 1 provider_string  from sys.servers where product='GameCoreDB' ";

            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            //数据库DigGameDB表 sys.servers 获取 GameCoreDB 所在ip
            DBHelperGameCoreDB.connectionString = conn;
        }
        #endregion

        #endregion


        #region 分页按钮事件

        protected void lbtnF_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = "1";
            bind(0);
        }

        protected void lbtnP_Click(object sender, EventArgs e)
        {
            if (lblPageIndex.Text != "1")
            {
                lblPageIndex.Text = (Convert.ToInt32(lblPageIndex.Text) - 1).ToString();
                bind(0);
            }
        }

        protected void lbtnN_Click(object sender, EventArgs e)
        {
            if (lblPageIndex.Text != lblPageCount.Text)
            {
                lblPageIndex.Text = (Convert.ToInt32(lblPageIndex.Text) + 1).ToString();
                bind(0);
            }
        }

        protected void lbtnE_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = lblPageCount.Text;
            bind(0);
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pindex = Convert.ToInt32(tboxPageIndex.Text);
                if (pindex > 0 && pindex <= Convert.ToInt32(lblPageCount.Text))
                {
                    lblPageIndex.Text = Convert.ToInt32(tboxPageIndex.Text).ToString();
                    bind(0);
                }

            }
            catch (System.Exception ex)
            {

            }
        }

        #endregion 分页按钮事件

    }
}
