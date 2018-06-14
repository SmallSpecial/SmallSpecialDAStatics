using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GiftConfig
{
    public partial class GiftConfigLog : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        
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
        #endregion

        #region 方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            string sql = "SELECT TOP 200 * FROM T_GiftConfigLog ORDER BY F_OPTime DESC";

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

        #region 转换文字说明
        protected string GetLimitStell(string value)
        {
            switch (value)
            {
                case "1":
                    return "日";
                case "2":
                    return "周";
                case "3":
                    return "月";
                case "4":
                    return "永久";
                default:
                    return value;
            }
        }
        protected string GetLimitTime(string value)
        {
            if (value == "1")
                return "是";
            else
                return "否";
        }
        protected string GetItemFlag(string value)
        {
            switch (value)
            {
                case "0":
                    return "正常";
                case "1":
                    return "推荐";
                default:
                    return value;
            }
        }
        protected string GetItemType(string value)
        {
            switch (value)
            {
                case "1":
                    return "装备";
                case "2":
                    return "材料";
                case "3":
                    return "宠物";
                case "4":
                    return "坐骑";
                case "5":
                    return "特殊";
                default:
                    return value;
            }
        }
        protected string GetItemTypeText(string value)
        {
            switch (value)
            {
                case "2201":
                    return "装备";
                case "2202":
                    return "材料";
                case "2203":
                    return "宠物";
                case "2204":
                    return "坐骑";
                case "2205":
                    return "特殊";
                default:
                    return value;
            }
        }
        protected string GetPackageMoneyType(string value)
        {
            switch (value)
            {
                case "0":
                    return "人民币礼包";
                case "1":
                    return "游戏币礼包";
                default:
                    return value;
            }
        }
        #endregion
    }
}
