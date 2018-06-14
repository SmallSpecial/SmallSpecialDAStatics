using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.ServiceState
{
    public partial class QueryServiceState : Admin_Page
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
                BindServices();
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
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
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
            sql = "SELECT * FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_BattleZone";
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
        public void BindServices()
        {
            try
            {
                string sql = "SELECT * FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_BattleZone";
                ds = DBHelperDigGameDB.Query(sql);
                this.ddlServices.DataSource = ds;
                this.ddlServices.DataTextField = "F_ZoneName";
                this.ddlServices.DataValueField = "F_ZoneID";
                this.ddlServices.DataBind();
                this.ddlServices.Items.Insert(0, new ListItem("==SELECT==", ""));

                this.ddlServices1.DataSource = ds;
                this.ddlServices1.DataTextField = "F_ZoneName";
                this.ddlServices1.DataValueField = "F_ZoneID";
                this.ddlServices1.DataBind();
                this.ddlServices1.Items.Insert(0, new ListItem("==SELECT==", ""));
            }
            catch (System.Exception ex)
            {
                //lblinfo.Text = ex.Message;
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

        protected void btnconfirm_Click(object sender, EventArgs e)
        {
            string services = ddlServices.SelectedValue;
            if(string.IsNullOrEmpty(services))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SelectServices + "！');</Script>");
                return;
            }
            string servicesState = ddlServicesState.SelectedValue;
            if (string.IsNullOrEmpty(servicesState))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SetState + "');</Script>");
                return;
            }
            string sql = string.Format("UPDATE OPENQUERY(LKSV_2_GameCoreDB_0_1,'SELECT * FROM T_BattleZone WHERE F_ZoneID={0}') SET F_ZoneAttrib={1}", services, servicesState);
            int res = DBHelperDigGameDB.ExecuteSql(sql);
            if(res>0)
            {
                bind();
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
            }
            else
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string services = ddlServices1.SelectedValue;
            if (string.IsNullOrEmpty(services))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SelectServices + "！');</Script>");
                return;
            }
            string servicesState = DropDownList2.SelectedValue;
            if (string.IsNullOrEmpty(servicesState))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SetState + "');</Script>");
                return;
            }
            string sql = string.Format("UPDATE OPENQUERY(LKSV_2_GameCoreDB_0_1,'SELECT * FROM T_BattleZone WHERE F_ZoneID={0}') SET F_ZoneState={1}", services, servicesState);
            int res = DBHelperDigGameDB.ExecuteSql(sql);
            if (res > 0)
            {
                bind();
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
            }
            else
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
            }
        }
        protected string GetZoneState(string val)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (val)
                {
                    case "1":
                        return "일반";
                    case "64":
                        return "점검";
                    case "128":
                        return "숨김";
                    default:
                        return val;
                }
            }
            else
            {
                switch (val)
                {
                    case "1":
                        return "正常";
                    case "64":
                        return "维护";
                    case "128":
                        return "隐藏";
                    default:
                        return val;
                }
            }
        }
        protected string GetZoneAttrib(string val)
        {
            if (PageLanguage == "ko-kr")
            {
                switch (val)
                {
                    case "0":
                        return "일반";
                    case "1":
                        return "신규";
                    case "3":
                        return "추천";
                    default:
                        return val;
                }
            }
            else
            {
                switch (val)
                {
                    case "0":
                        return "正常";
                    case "1":
                        return "新服";
                    case "3":
                        return "推荐";
                    default:
                        return val;
                }
            }
            
        }
    }
}
