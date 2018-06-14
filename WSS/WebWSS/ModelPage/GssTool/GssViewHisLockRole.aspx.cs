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
    public partial class GssViewHisLockRole : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        DataSet ds;
        #endregion

        #region 事件
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
            sql = "SELECT TOP 200 * FROM T_OpLog WHERE F_Module=N'角色封停' ORDER BY F_CreateTime DESC ";
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
