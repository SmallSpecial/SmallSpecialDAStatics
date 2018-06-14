using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GameInCome
{
    public partial class RechargeRankList : Admin_Page
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
            if (!IsPostBack)
            {
                //初始化开始时间
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                //初始化结束时间
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                //绑定数据
                Bind();
                //绑定渠道
                BindChannel();
            }
            ControlOutFile1.ControlOut = GridView1;
            ControlOutFile1.VisibleExcel = false;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            System.Text.EncodingInfo[] ss = System.Text.Encoding.GetEncodings();
            ControlDateSelect1.SetSelectDate(tboxTimeB.Text);
            Bind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Bind();
        }
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
        protected void ControlChartSelect_SelectChanged(object sender, EventArgs e)
        {
            if (ControlChartSelect1.State == 0)
            {
                GridView1.Visible = true;
                ControlChart1.Visible = false;
            }
            else
            {
                GridView1.Visible = false;
                ControlChart1.Visible = true;
                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + "所有大区" + " " + DateTime.Now.ToString("yyyy-MM-dd"), ControlChartSelect1.State, false, 0, 0);
            }
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            Bind();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void Bind()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now;
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
            string sql = string.Format("SELECT F_ZoneID,F_UserID,F_GlobalID,COUNT(F_GlobalID) RechargeNum,SUM(F_Price) RechargeAmount,MAX(F_InsertTime) LastRechargeTime,MAX(F_PlayerLevel) LastLevel FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' {2} GROUP BY F_ZoneID,F_UserID,F_GlobalID ORDER BY F_ZoneID", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
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
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ExportExcel(object sender, EventArgs e)
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            string sqlWhere = "";
            DateTime searchdateB = DateTime.Now.AddDays(-6);
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

            string sql = string.Format("SELECT F_ZoneID 区服,F_UserID 用户编号,F_GlobalID 角色编号,COUNT(F_GlobalID) 充值次数,SUM(F_Price) 充值金额,MAX(F_InsertTime) 最后一次充值时间,MAX(F_PlayerLevel) 最后一次充值等级 FROM T_Deposit_Verify_Result_Log WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' {2} GROUP BY F_ZoneID,F_UserID,F_GlobalID ORDER BY F_ZoneID", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);

            ds = DBHelperUserCoreDB.Query(sql);

            DataSet newDataSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("区服", System.Type.GetType("System.String"));
            dt.Columns.Add("用户编号", System.Type.GetType("System.String"));
            dt.Columns.Add("角色编号", System.Type.GetType("System.String"));
            dt.Columns.Add("角色名称", System.Type.GetType("System.String"));
            dt.Columns.Add("充值次数", System.Type.GetType("System.String"));
            dt.Columns.Add("充值金额", System.Type.GetType("System.String"));
            dt.Columns.Add("最后一次充值时间", System.Type.GetType("System.String"));
            dt.Columns.Add("最后一次充值等级", System.Type.GetType("System.String"));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["区服"] = ds.Tables[0].Rows[i]["区服"];
                newRow["用户编号"] = ds.Tables[0].Rows[i]["用户编号"];
                newRow["角色编号"] = ds.Tables[0].Rows[i]["角色编号"];
                newRow["角色名称"] = GetRoleName(ds.Tables[0].Rows[i]["角色编号"].ToString());
                newRow["充值次数"] = ds.Tables[0].Rows[i]["充值次数"];
                newRow["充值金额"] = ds.Tables[0].Rows[i]["充值金额"];
                newRow["最后一次充值时间"] = ds.Tables[0].Rows[i]["最后一次充值时间"];
                newRow["最后一次充值等级"] = ds.Tables[0].Rows[i]["最后一次充值等级"];
                dt.Rows.Add(newRow);
            }
            newDataSet.Tables.Add(dt);
            ExportExcelHelper.ExportDataSet(newDataSet);
        }
        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
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

        #region 转到触发方法
        protected void Go_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = int.Parse(((TextBox)GridView1.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            Bind();   //重新绑定GridView
        }
        #endregion

        #region 分页触发方法
        protected void GvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Bind();  //重新绑定GridView
        }
        #endregion
    }
}
