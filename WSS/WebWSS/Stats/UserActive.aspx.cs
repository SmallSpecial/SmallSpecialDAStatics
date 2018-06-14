using System;
using System.Data;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class UserActive : Admin_Page
    {
        string ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortOrder"] = "F_ID";
                ViewState["OrderDire"] = "ASC";
                bind();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            string sql = @"SELECT F_ID,F_Year, F_Month, F_Day, F_UserNum,  F_NewUserNum, F_NewActivationNum, F_NewLoginNum, F_ActiveUserNum, F_NewActiveUserNum, F_LoseUserNum,F_NewLoseUserNum, F_ReturnUserNum, F_NewReturnUserNum FROM T_UserBaseDig_Day WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='2011-1-1'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='2012-10-10'";

            try
            {
                DbHelperSQLP sp = new DbHelperSQLP();
                sp.connectionString = ConnStr;
                ds = sp.Query(sql);
                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                }
                else
                {
                    lblerro.Visible = false;
                }
                string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
                myView.Sort = sort;
                GridView1.DataSource = myView;
                GridView1.DataKeyNames = new string[] { "F_ID" };
                GridView1.DataBind();
            }
            catch (System.Exception ex)
            {
                lblerro.Visible = true;
            }

        }
        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。此事件通常用于在某个行被绑定到数据时修改该行的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-color:#cccccc";
            }
        }
        /// <summary>
        /// 在单击某个用于对列进行排序的超链接时发生，但在 GridView 控件执行排序操作之前。此事件通常用于取消排序操作或执行自定义的排序例程。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
