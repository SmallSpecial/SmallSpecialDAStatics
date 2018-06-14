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
    public partial class GssViewHisGameNotice : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();
        DataSet ds;
        #endregion

        #region 事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            GetGameCoreDBString();
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

                    DataRowView data = e.Row.DataItem as DataRowView;
                    //运行公告
                    LinkButton lbtStart = e.Row.FindControl("lbtStart") as LinkButton;
                    //停止公告
                    LinkButton lbtStop = e.Row.FindControl("lbtStop") as LinkButton;

                    bool IsTrue = String.Compare(data["F_TToolUsed"].ToString(), "True", true) == 0;
                    
                    if (IsTrue)
                    {
                        lbtStart.Visible = false;
                    }
                    else
                    {
                        lbtStop.Visible = false;
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
        /// 页面索引发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            string sql = "";
            sql = "SELECT TOP 200 * FROM [T_Tasks] WHERE [F_Type]=20000213 AND CONVERT(VARCHAR(100),F_CreatTime,23)>='2017-11-16' ORDER BY F_ID DESC";
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
        public void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
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

        #region 运行/停止公告
        /// <summary>
        /// 运行公告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtStart_Click(object sender, EventArgs e)
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
                string sql = string.Format("SELECT * FROM T_Tasks WHERE F_Type=20000213 AND F_ID={0}",ID);
                ds = DBHelperGSSDB.Query(sql);
                if(ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count==0)
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_OrderNotExisit + "');</Script>");
                    return;
                }
                
                string strTaskID = ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString();
                string strContentInfo = ds.Tables[0].Rows[0]["F_URInfo"].ToString();
                string strBattleZone = ds.Tables[0].Rows[0]["F_TUseData"].ToString();
                string sendBattleZone = string.Empty;
                
                int rowcount = 0;

                try
                {
                    sql = @"select 1 FROM T_GameNotice with(nolock) WHERE (F_TaskID = " + strTaskID + ") and F_TaskState = 1";
                    rowcount = DBHelperGameCoreDB.ExecuteSql(sql);
                    if (rowcount > 0)
                    {
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_RunNoticeIsRun + "');</Script>");
                        return;
                    }
                    sql = @"delete FROM T_GameNotice WHERE (F_TaskID = " + strTaskID + ")";
                    DBHelperGameCoreDB.ExecuteSql(sql);
                    int res = 0;
                    string[] arrayNoticeInfo = strContentInfo.Split('|');
                    string[] arrayBattleZone = strBattleZone.Split(';');
                    for (int i = 0; i < arrayBattleZone.Length; i++)
                    {
                        sql = @"INSERT INTO T_GameNotice (F_ReciveZone, F_ReciveLine, F_ReciveObject, F_MSGLocation, F_Message, F_RunTimeBegin, F_RunTimeEnd, F_RunInterval, F_TaskState,F_TaskID, F_NoticeTimes)
VALUES     (" + arrayBattleZone[i] + ",-1,N'" + arrayNoticeInfo[1] + "', " + arrayNoticeInfo[2] + ", N'" + arrayNoticeInfo[0] + "', '" + arrayNoticeInfo[3] + "', '" + arrayNoticeInfo[4] + "', " + arrayNoticeInfo[5] + ", 1, " + strTaskID + ", 0)";
                        res += DBHelperGameCoreDB.ExecuteSql(sql);
                        sendBattleZone += arrayBattleZone[i] + ";";
                    }
                    if (res == arrayBattleZone.Length)
                    {
                        sql = string.Format("UPDATE T_Tasks SET F_TToolUsed=1 WHERE F_ID={0}", ID);
                        DBHelperGSSDB.ExecuteSql(sql);
                        bind();
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                    }
                    else
                    {
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + ";发送成功的战区：" + sendBattleZone + "');</Script>");
                    }
                }
                catch (System.Exception ex)
                {
                    lblerro.Text = "运行公告失败；发送成功的战区：" + sendBattleZone + ";ErrorInfo:" + ex.Message;
                }
            }
        }
        /// <summary>
        /// 停止公告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtStop_Click(object sender, EventArgs e)
        {
            try
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
                    string sql = string.Format("SELECT * FROM T_Tasks WHERE F_Type=20000213 AND F_ID={0}", ID);
                    ds = DBHelperGSSDB.Query(sql);
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_RunNoticeIsRun + "');</Script>");
                        return;
                    }
                    string strTaskID = ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString();
                    sql = @"UPDATE T_GameNotice SET F_TaskState = 0 WHERE (F_TaskID = " + strTaskID + ")";
                    int rowcount = DBHelperGameCoreDB.ExecuteSql(sql);
                    if(rowcount>0)
                    {
                        sql = string.Format("UPDATE T_Tasks SET F_TToolUsed=0 WHERE F_ID={0}", ID);
                        DBHelperGSSDB.ExecuteSql(sql);
                        bind();
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                    }
                    else
                    {
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                    }
                }
            }
            catch (System.Exception ex)
            {
                lblerro.Text = "停止公告失败;ErrorInfo:" + ex.Message;
            }
        }
        #endregion

        protected string GetContent(string str)
        {
            switch (str)
            {
                case "confirmRunNotice":
                    return App_GlobalResources.Language.Tip_ConfirmRunNotice;
                case "confirmStopNotice":
                    return App_GlobalResources.Language.Tip_ConfirmStopNotice;
                default:
                    return str;
            }

        }
    }
}
