using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GssTool
{
    public partial class GssViewHisFullServicesMail : Admin_Page
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
                bind();
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
        /// <summary>
        /// 页面索引发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            var _sender = sender as LinkButton;
            if (_sender != null)
            {
                //获取要删除数据ID
                string ID = _sender.Attributes["_InfoID"];
                if (string.IsNullOrEmpty(ID))
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                    return;
                }
                string sql = string.Format("SELECT [F_ID],[F_PreDutyMan],[F_TUseData] FROM [dbo].[T_Tasks] WHERE F_Type=20000217 AND F_ID={0}", ID);
                ds = DBHelperGSSDB.Query(sql);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_OrderIsDelete + "');</Script>");
                    return;
                }
                string DBID = ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString();
                string battleZone = ds.Tables[0].Rows[0]["F_TUseData"].ToString();
                string[] arrayBattleZone = battleZone.Split(';');
                for (int i = 0; i < arrayBattleZone.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrayBattleZone[i]))
                    {
                        sql = string.Format("DELETE FROM OPENQUERY([LKSV_7_gspara_db_0_{0}],'SELECT * FROM sys_loss_award_table WHERE DBID={1}')", arrayBattleZone[i], DBID);
                        DBHelperGSSDB.ExecuteSql(sql);
                    }
                }
                sql = string.Format("DELETE FROM [dbo].[T_Tasks] WHERE F_Type=20000217 AND F_ID={0}", ID);
                int res = DBHelperGSSDB.ExecuteSql(sql);
                if (res > 0)
                {
                    bind();
                    sql = string.Format("INSERT INTO [dbo].[T_OpLog] ([F_Module], [F_OPID], [F_TaskID], [F_User], [F_CreateTime]) VALUES (N'全服邮件', N'Delete', {0},N'{1}', GETDATE())", ID, Session["LoginUser"].ToString());
                    DBHelperDigGameDB.ExecuteSql(sql);
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            string sql = "";
            sql = "SELECT TOP 200 * FROM [T_Tasks] WHERE [F_Type]=20000217 AND CONVERT(VARCHAR(100),F_CreatTime,23)>='2017-11-10' ORDER BY F_ID DESC";
            try
            {
                ds = DBHelperGSSDB.Query(sql);
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

        #region 查看历史页面跳转
        protected void btnGiftAward_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHis.aspx");
        }

        protected void btnFullServicesMail_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisFullServicesMail.aspx");
        }

        protected void btnDeleteFullServicesMail_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisDeleteFullServicesMail.aspx");
        }
        protected void btnDeleteGiftAward_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisDeleteGiftAward.aspx");
        }
        protected void btnLockUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisLockUser.aspx");
        }
        protected void btnUnLockUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisUnLockUser.aspx");
        }
        protected void btnLockRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisLockRole.aspx");
        }
        protected void btnUnLockRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisUnLockRole.aspx");
        }
        protected void btnDisRoleChatAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisDisRoleChatAdd.aspx");
        }
        protected void btnDisRoleChatDel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisDisRoleChatDel.aspx");
        }
        protected void btnRoleRecovery_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisRoleRecovery.aspx");
        }
        protected void btnGameNoticeHis_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisGameNotice.aspx");
        }
        #endregion
    }
}
