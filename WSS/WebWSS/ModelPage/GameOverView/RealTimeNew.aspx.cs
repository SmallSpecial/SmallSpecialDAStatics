using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GameOverView
{
    public partial class RealTimeNew : Admin_Page
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
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
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
            if (!Common.Validate.IsDateTime(tboxTimeB.Text))
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
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }
            //用户信息
            string sqlUserInfo = string.Format("SELECT LEFT(CONVERT(VARCHAR(100),F_CreatTime,120), 13) DateTime,COUNT(A.F_UserID) UserNum FROM T_User A LEFT JOIN T_UserExInfo B ON A.F_UserID=B.F_UserID WHERE CONVERT(VARCHAR(100),F_CreatTime,23)='{0}' {1} GROUP BY LEFT(CONVERT(VARCHAR(100),F_CreatTime,120), 13) ORDER BY LEFT(CONVERT(VARCHAR(100),F_CreatTime,120), 13)", searchdateB.ToString("yyyy-MM-dd"),sqlWhere);
            //角色信息
            string sqlRoleInfo = string.Format("SELECT LEFT(CONVERT(VARCHAR(100),F_CreateTime,120), 13) DateTime,COUNT(DISTINCT F_UserID) RoleUserNum,COUNT(DISTINCT F_DeviceID) RoleDeviceNum,COUNT(F_RoleID) RoleNum FROM(SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_0 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1} UNION ALL SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_1 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1} UNION ALL SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_2 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1} UNION ALL SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_3 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1} UNION ALL SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_4 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1})TEMP GROUP BY LEFT(CONVERT(VARCHAR(100),F_CreateTime,120), 13) ORDER BY LEFT(CONVERT(VARCHAR(100),F_CreateTime,120), 13)",searchdateB.ToString("yyyy-MM-dd"),sqlWhere);

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("DateTime", System.Type.GetType("System.String"));
                dt.Columns.Add("UserNum", System.Type.GetType("System.String"));
                dt.Columns.Add("RoleUserNum", System.Type.GetType("System.String"));
                dt.Columns.Add("RoleDeviceNum", System.Type.GetType("System.String"));
                dt.Columns.Add("RoleNum", System.Type.GetType("System.String"));

                DataSet dsUserInfo = new DataSet();
                DataSet dsRoleInfo = new DataSet();

                dsUserInfo = DBHelperUserCoreDB.Query(sqlUserInfo);
                dsRoleInfo = DBHelperGameCoreDB.Query(sqlRoleInfo);
                
                for (int i = 0; i < 24; i++)
                {
                    int flagUser = 0;
                    int flagRole = 0;
                    DataRow newRow = dt.NewRow();
                    newRow["DateTime"] = searchdateB.AddHours(i).ToString("yyyy-MM-dd HH");
                    //用户信息
                    for (int j = 0; j < dsUserInfo.Tables[0].Rows.Count; j++)
                    {
                        if (searchdateB.AddHours(i).ToString("yyyy-MM-dd HH") == dsUserInfo.Tables[0].Rows[j]["DateTime"].ToString())
                        {
                            flagUser = 1;
                            newRow["UserNum"] = dsUserInfo.Tables[0].Rows[j]["UserNum"].ToString();
                        }
                    }
                    if (flagUser == 0)
                    {
                        newRow["UserNum"] = "0";
                    }
                    //角色信息
                    for (int m = 0; m < dsRoleInfo.Tables[0].Rows.Count; m++)
                    {
                        if (searchdateB.AddHours(i).ToString("yyyy-MM-dd HH") == dsRoleInfo.Tables[0].Rows[m]["DateTime"].ToString())
                        {
                            flagRole = 1;
                            newRow["RoleUserNum"] = dsRoleInfo.Tables[0].Rows[m]["RoleUserNum"].ToString();
                            newRow["RoleDeviceNum"] = dsRoleInfo.Tables[0].Rows[m]["RoleDeviceNum"].ToString();
                            newRow["RoleNum"] = dsRoleInfo.Tables[0].Rows[m]["RoleNum"].ToString();
                        }
                    }
                    if (flagRole == 0)
                    {
                        newRow["RoleUserNum"] = "0";
                        newRow["RoleDeviceNum"] = "0";
                        newRow["RoleNum"] = "0";
                    }
                    dt.Rows.Add(newRow);
                }

                DataView myView = dt.DefaultView;
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
            if (!Common.Validate.IsDateTime(tboxTimeB.Text))
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
            if (!string.IsNullOrEmpty(ddlChannel.SelectedValue.Trim()))
            {
                sqlWhere += string.Format(" AND F_ChannelID=N'{0}'", ddlChannel.SelectedValue);
            }
            //用户信息
            string sqlUserInfo = string.Format("SELECT LEFT(CONVERT(VARCHAR(100),F_CreatTime,120), 13) DateTime,COUNT(A.F_UserID) UserNum FROM T_User A LEFT JOIN T_UserExInfo B ON A.F_UserID=B.F_UserID WHERE CONVERT(VARCHAR(100),F_CreatTime,23)='{0}' {1} GROUP BY LEFT(CONVERT(VARCHAR(100),F_CreatTime,120), 13) ORDER BY LEFT(CONVERT(VARCHAR(100),F_CreatTime,120), 13)", searchdateB.ToString("yyyy-MM-dd"), sqlWhere);
            //角色信息
            string sqlRoleInfo = string.Format("SELECT LEFT(CONVERT(VARCHAR(100),F_CreateTime,120), 13) DateTime,COUNT(DISTINCT F_UserID) RoleUserNum,COUNT(DISTINCT F_DeviceID) RoleDeviceNum,COUNT(F_RoleID) RoleNum FROM(SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_0 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1} UNION ALL SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_1 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1} UNION ALL SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_2 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1} UNION ALL SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_3 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1} UNION ALL SELECT F_UserID,F_RoleID,F_DeviceID,F_CreateTime FROM T_RoleBaseData_4 WHERE CONVERT(VARCHAR(100),F_CreateTime,23)='{0}' {1})TEMP GROUP BY LEFT(CONVERT(VARCHAR(100),F_CreateTime,120), 13) ORDER BY LEFT(CONVERT(VARCHAR(100),F_CreateTime,120), 13)", searchdateB.ToString("yyyy-MM-dd"), sqlWhere);


            DataTable dt = new DataTable();
            dt.Columns.Add("时间", System.Type.GetType("System.String"));
            dt.Columns.Add("注册（激活）数", System.Type.GetType("System.String"));
            dt.Columns.Add("创建角色账号数", System.Type.GetType("System.String"));
            dt.Columns.Add("创建角色设备数", System.Type.GetType("System.String"));
            dt.Columns.Add("创建角色个数", System.Type.GetType("System.String"));

            DataSet dsUserInfo = new DataSet();
            DataSet dsRoleInfo = new DataSet();

            dsUserInfo = DBHelperUserCoreDB.Query(sqlUserInfo);
            dsRoleInfo = DBHelperGameCoreDB.Query(sqlRoleInfo);

            for (int i = 0; i < 24; i++)
            {
                int flagUser = 0;
                int flagRole = 0;
                DataRow newRow = dt.NewRow();
                newRow["时间"] = searchdateB.AddHours(i).ToString("yyyy-MM-dd HH");
                //用户信息
                for (int j = 0; j < dsUserInfo.Tables[0].Rows.Count; j++)
                {
                    if (searchdateB.AddHours(i).ToString("yyyy-MM-dd HH") == dsUserInfo.Tables[0].Rows[j]["DateTime"].ToString())
                    {
                        flagUser = 1;
                        newRow["注册（激活）数"] = dsUserInfo.Tables[0].Rows[j]["UserNum"].ToString();
                    }
                }
                if (flagUser == 0)
                {
                    newRow["注册（激活）数"] = "0";
                }
                //角色信息
                for (int m = 0; m < dsRoleInfo.Tables[0].Rows.Count; m++)
                {
                    if (searchdateB.AddHours(i).ToString("yyyy-MM-dd HH") == dsRoleInfo.Tables[0].Rows[m]["DateTime"].ToString())
                    {
                        flagRole = 1;
                        newRow["创建角色账号数"] = dsRoleInfo.Tables[0].Rows[m]["RoleUserNum"].ToString();
                        newRow["创建角色设备数"] = dsRoleInfo.Tables[0].Rows[m]["RoleDeviceNum"].ToString();
                        newRow["创建角色个数"] = dsRoleInfo.Tables[0].Rows[m]["RoleNum"].ToString();
                    }
                }
                if (flagRole == 0)
                {
                    newRow["创建角色账号数"] = "0";
                    newRow["创建角色设备数"] = "0";
                    newRow["创建角色个数"] = "0";
                }
                dt.Rows.Add(newRow);
            }
            DataSet newDataSet = new DataSet();
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
