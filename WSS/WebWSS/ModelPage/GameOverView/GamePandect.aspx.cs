using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using Newtonsoft.Json;
using System.Web.Services;

namespace WebWSS.ModelPage.GameOverView
{
    public partial class GamePandect : Admin_Page
    {
        #region 属性
        //获取DigGameDB连接字符串
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        //获取GssDB连接字符串
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        //DigGameDB
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        //GSSDB
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        //UserCoreDB
        DbHelperSQLP DBHelperUserCoreDB = new DbHelperSQLP();
        //GameCoreDB
        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();

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
            //获取DigGameDB连接字符串
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            //获取GssDB连接字符串
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            //获取UserCoreDB连接字符串
            GetUserCoreDBString();
            //获取GameCoreDB连接字符串
            GetGameCoreDBString();
            if (!IsPostBack)
            {
                //初始化开始时间
                tboxTimeB.Text = DateTime.Now.AddDays(-7).ToString(SmallDateTimeFormat);
                //初始化结束时间
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                //绑定渠道
                BindChannel();
                //绑定注册用户
                BindRegisterPlayerNum();
                //绑定创建用户
                BindCreatePlayerNum();
                //绑定充值人数
                BindRechargePeopleOfNum();
                //绑定充值金额
                BindRechargeAmount();
                //总览
                GetPandect();
            }
            //禁用图表
            //ControlOutFile1.ControlOut = GridView1;
            //禁用导出Excel/Word
            ControlOutFile1.VisibleExcel = false;
            ControlOutFile1.VisibleDoc = false;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            //绑定注册用户
            BindRegisterPlayerNum();
            //绑定创建用户
            BindCreatePlayerNum();
            //绑定充值人数
            BindRechargePeopleOfNum();
            //绑定充值金额
            BindRechargeAmount();
            //总览
            GetPandect();
        }

        #region 注册用户
        protected void gvRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRegister.PageIndex = e.NewPageIndex;
            //绑定注册用户
            BindRegisterPlayerNum();
        }
        protected void gvRegister_RowDataBound(object sender, GridViewRowEventArgs e)
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
        /// 转到触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRegister_Go_Click(object sender, EventArgs e)
        {
            gvRegister.PageIndex = int.Parse(((TextBox)gvRegister.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            //绑定注册用户
            BindRegisterPlayerNum();
        }
        #endregion

        #region 创建玩家数
        protected void gvCreate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCreate.PageIndex = e.NewPageIndex;
            //绑定创建用户
            BindCreatePlayerNum();
        }
        protected void gvCreate_RowDataBound(object sender, GridViewRowEventArgs e)
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
        /// 转到触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCreate_Go_Click(object sender, EventArgs e)
        {
            gvCreate.PageIndex = int.Parse(((TextBox)gvCreate.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            //绑定创建用户
            BindCreatePlayerNum();
        }
        #endregion

        #region 充值人数
        protected void gvRechargePeopleOfNum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRechargePeopleOfNum.PageIndex = e.NewPageIndex;
            //绑定充值人数
            BindRechargePeopleOfNum();
        }
        protected void gvRechargePeopleOfNum_RowDataBound(object sender, GridViewRowEventArgs e)
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
        /// 转到触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRechargePeopleOfNum_Go_Click(object sender, EventArgs e)
        {
            gvRechargePeopleOfNum.PageIndex = int.Parse(((TextBox)gvRechargePeopleOfNum.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            //绑定充值人数
            BindRechargePeopleOfNum();
        }
        #endregion

        #region 充值金额
        protected void gvRechargeAmount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRechargeAmount.PageIndex = e.NewPageIndex;
            //绑定充值人数
            BindRechargeAmount();
        }
        protected void gvRechargeAmount_RowDataBound(object sender, GridViewRowEventArgs e)
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
        /// 转到触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRechargeAmount_Go_Click(object sender, EventArgs e)
        {
            gvRechargeAmount.PageIndex = int.Parse(((TextBox)gvRechargeAmount.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            //绑定充值人数
            BindRechargeAmount();
        }
        #endregion

        #region 保留（日期控件和图表控件事件）
        protected void ControlChartSelect_SelectChanged(object sender, EventArgs e)
        {
            if (ControlChartSelect1.State == 0)
            {
                //GridView1.Visible = true;
                //ControlChart1.Visible = false;
            }
            else
            {
                //GridView1.Visible = false;
                //ControlChart1.Visible = true;
                //ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + "所有大区" + " " + DateTime.Now.ToString("yyyy-MM-dd"), ControlChartSelect1.State, false, 0, 0);
            }
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            //Bind();
        }
        #endregion

        #endregion

        #region 方法
        /// <summary>
        /// 总览
        /// </summary>
        public void GetPandect()
        {
            //注册玩家数
            lblRegister.Text = GetRegisterPlayerNum();
            //创建玩家数
            lblCreate.Text = GetCreatePlayerNum();
            //充值人数
            lblRechargePeopleOfNum.Text = GetRechargePeopleOfNum();
            //充值金额
            string strRechargeAmount = GetRechargeAmount();
            lblRechargeAmount.Text = string.IsNullOrEmpty(strRechargeAmount) ? "0" : strRechargeAmount;

            if (lblRegister.Text == "0")
            {
                lblRegisterLv.Text = "0%";
            }
            else
            {
                double percent = Math.Round(Convert.ToInt32(lblRechargePeopleOfNum.Text) * 1.00 / Convert.ToInt32(lblRegister.Text) * 100.0, 2);
                lblRegisterLv.Text = percent + "%";
            }

            if (lblCreate.Text == "0")
            {
                lblCreateLv.Text = "0%";
            }
            else
            {
                double percent = Math.Round(Convert.ToInt32(lblRechargePeopleOfNum.Text) * 1.00 / Convert.ToInt32(lblCreate.Text) * 100.0, 2);
                lblCreateLv.Text = percent + "%";
            }
        }
        /// <summary>
        /// 绑定注册玩家数
        /// </summary>
        public void BindRegisterPlayerNum()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_CreatTime,23) DateTime,COUNT(F_UserID) Num FROM T_User WHERE CONVERT(VARCHAR(100),F_CreatTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreatTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_CreatTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    myView.AddNew();
                }

                gvRegister.DataSource = myView;
                gvRegister.DataBind();
            }
            catch (System.Exception ex)
            {
                gvRegister.DataSource = null;
                gvRegister.DataBind();
            }
        }
        /// <summary>
        /// 绑定创建用户数
        /// </summary>
        public void BindCreatePlayerNum()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_CreateTime,23) DateTime,COUNT(DISTINCT F_UserID) Num FROM (SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_0 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_1 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_2 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_3 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_4 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}')TEMP GROUP BY CONVERT(VARCHAR(100),F_CreateTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            try
            {
                ds = DBHelperGameCoreDB.Query(sql);
                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    myView.AddNew();
                }
                gvCreate.DataSource = myView;
                gvCreate.DataBind();
            }
            catch (System.Exception ex)
            {
                gvCreate.DataSource = null;
                gvCreate.DataBind();
            }
        }
        /// <summary>
        /// 绑定充值人数
        /// </summary>
        public void BindRechargePeopleOfNum()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }
            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_InsertTime,23) DateTime,COUNT(DISTINCT F_UserID) Num FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_InsertTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    myView.AddNew();
                }
                gvRechargePeopleOfNum.DataSource = myView;
                gvRechargePeopleOfNum.DataBind();
            }
            catch (System.Exception ex)
            {
                gvRechargePeopleOfNum.DataSource = null;
                gvRechargePeopleOfNum.DataBind();
            }
        }
        /// <summary>
        /// 绑定充值金额
        /// </summary>
        public void BindRechargeAmount()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_InsertTime,23) DateTime,SUM(F_Price) Amount FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_InsertTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    myView.AddNew();
                }
                gvRechargeAmount.DataSource = myView;
                gvRechargeAmount.DataBind();
            }
            catch (System.Exception ex)
            {
                gvRechargeAmount.DataSource = null;
                gvRechargeAmount.DataBind();
            }
        }
        /// <summary>
        /// 获取注册玩家数
        /// </summary>
        public string GetRegisterPlayerNum()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return "0";
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT COUNT(F_UserID) Num FROM T_User WHERE CONVERT(VARCHAR(100),F_CreatTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreatTime,23)<='{1}' ", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch(Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取创建用户数
        /// </summary>
        public string GetCreatePlayerNum()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return "0";
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT COUNT(DISTINCT F_UserID) Num FROM (SELECT F_UserID FROM T_RoleBaseData_0 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID FROM T_RoleBaseData_1 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID FROM T_RoleBaseData_2 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID FROM T_RoleBaseData_3 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID FROM T_RoleBaseData_4 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}')TEMP ", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            try
            {
                ds = DBHelperGameCoreDB.Query(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取充值人数
        /// </summary>
        public string GetRechargePeopleOfNum()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return "";
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }
            string sql = string.Format("SELECT COUNT(DISTINCT F_UserID) Num FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' ", searchdateB.ToString("yyyy-MM-dd"),searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取充值金额
        /// </summary>
        public string GetRechargeAmount()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return "0";
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT SUM(F_Price) Amount FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}'", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取UsercoreDB连接字符串
        /// </summary>
        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
        }
        /// <summary>
        /// 获取GameCoreDB连接字符串
        /// </summary>
        public void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        /// <summary>
        /// 绑定渠道
        /// </summary>
        public void BindChannel()
        {
            string sql = "SELECT DISTINCT F_ChannelID FROM T_Deposit_Verify_Result_Log ORDER BY F_ChannelID ";
            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                this.ddlChannel.DataSource = ds;
                this.ddlChannel.DataTextField = "F_ChannelID";
                this.ddlChannel.DataValueField = "F_ChannelID";
                this.ddlChannel.DataBind();
                this.ddlChannel.Items.Insert(0, new ListItem("全部", ""));
            }
            catch (System.Exception ex)
            {
                ddlChannel.Items.Clear();
                ddlChannel.Items.Add(new ListItem("", ""));
            }
        }
        #endregion

        #region 导出Excel
        protected void btnRegisterToExcel_Click(object sender, EventArgs e)
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_CreatTime,23) 时间,COUNT(F_UserID) 玩家数 FROM T_User WHERE CONVERT(VARCHAR(100),F_CreatTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreatTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_CreatTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

           ds = DBHelperUserCoreDB.Query(sql);
           ExportExcelHelper.ExportDataSet(ds);
        }

        protected void btnCreateToExcel_Click(object sender, EventArgs e)
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_CreateTime,23) 时间,COUNT(DISTINCT F_UserID) 玩家数 FROM (SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_0 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_1 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_2 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_3 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}' UNION ALL SELECT F_UserID,F_CreateTime FROM T_RoleBaseData_4 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_CreateTime,23)<='{1}')TEMP GROUP BY CONVERT(VARCHAR(100),F_CreateTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            ds = DBHelperGameCoreDB.Query(sql);
            ExportExcelHelper.ExportDataSet(ds);
        }

        protected void btnRechargePeopleOfNumToExcel_Click(object sender, EventArgs e)
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }
            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_InsertTime,23) 时间,COUNT(DISTINCT F_UserID) 人数 FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_InsertTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            ds = DBHelperUserCoreDB.Query(sql);
            ExportExcelHelper.ExportDataSet(ds);
        }

        protected void btnRechargeAmountToExcel_Click(object sender, EventArgs e)
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-7);
            DateTime searchdateE = DateTime.Now;

            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }

            string sql = string.Format("SELECT CONVERT(VARCHAR(100),F_InsertTime,23) 时间,SUM(F_Price) 金额 FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' GROUP BY CONVERT(VARCHAR(100),F_InsertTime,23)", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            ds = DBHelperUserCoreDB.Query(sql);
            ExportExcelHelper.ExportDataSet(ds);
        }
        #endregion

        #region 图表切换
        protected void btnRegisterTableTransform_Click(object sender, EventArgs e)
        {
            divTableRegister.Visible = true;
            divMapRegister.Visible = false;
        }

        protected void btnRegisterMapTransform_Click(object sender, EventArgs e)
        {
            divTableRegister.Visible = false;
            divMapRegister.Visible = true;
        }

        protected void btnCreateTableTransform_Click(object sender, EventArgs e)
        {
            divTableCreate.Visible = true;
            divMapCreate.Visible = false;
        }

        protected void btnCreateMapTransform_Click(object sender, EventArgs e)
        {
            divTableCreate.Visible = false ;
            divMapCreate.Visible = true;
        }

        protected void btnRechargePeopleOfNumTableTransform_Click(object sender, EventArgs e)
        {
            divTableRechargePeopleOfNum.Visible = true;
            divMapRechargePeopleOfNum.Visible = false;
        }

        protected void btnRechargePeopleOfNumMapTransform_Click(object sender, EventArgs e)
        {
            divTableRechargePeopleOfNum.Visible = false;
            divMapRechargePeopleOfNum.Visible = true;
        }

        protected void btnRechargeAmountTableTransform_Click(object sender, EventArgs e)
        {
            divTableRechargeAmount.Visible = true;
            divMapRechargeAmount.Visible = false;
        }

        protected void btnRechargeAmountMapTransform_Click(object sender, EventArgs e)
        {
            divTableRechargeAmount.Visible = false;
            divMapRechargeAmount.Visible = true;
        }
        #endregion
    }
}
