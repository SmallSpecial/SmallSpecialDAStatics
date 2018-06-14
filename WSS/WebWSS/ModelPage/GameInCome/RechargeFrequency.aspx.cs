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
    public partial class RechargeFrequency : Admin_Page
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
                tboxTimeB.Text = DateTime.Now.AddDays(-6).ToString(SmallDateTimeFormat);
                //初始化结束时间
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                //绑定数据
                Bind();
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

            string sql = string.Format("SELECT COUNT (F_UserID) RechargeNumOfPeople,SUM (CASE WHEN Num = 2 THEN 1 ELSE 0 END) RechargeNumOfPeople_2,SUM (CASE WHEN Num = 3 THEN 1 ELSE 0 END) RechargeNumOfPeople_3,CAST (CONVERT (DECIMAL (18, 2),SUM (CASE WHEN Num = 2 THEN 1 ELSE 0 END) / CAST (ISNULL(NULLIF(COUNT (F_UserID), 0), 1) AS FLOAT) * 100) AS VARCHAR (10)) + '%' AS RechargeNumOfPeople_2Lv,CAST (CONVERT (DECIMAL (18, 2),SUM (CASE WHEN Num = 3 THEN 1 ELSE 0 END) / CAST (ISNULL(NULLIF(COUNT (F_UserID), 0), 1) AS FLOAT) * 100) AS VARCHAR (10)) + '%' AS RechargeNumOfPeople_3Lv FROM (SELECT F_UserID,COUNT (F_UserID) Num FROM UserCoreDB.dbo.T_Deposit_Verify_Result_Log WHERE F_DepositResult = 1 AND CONVERT (VARCHAR (100),F_InsertTime,23) >= '{0}' AND CONVERT (VARCHAR (100),F_InsertTime,23) <= '{1}' GROUP BY F_UserID) TEMP", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"));
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

            string sql = string.Format("SELECT COUNT (F_UserID) 充值总人数,SUM (CASE WHEN Num = 2 THEN 1 ELSE 0 END) 付费两次用户,SUM (CASE WHEN Num = 3 THEN 1 ELSE 0 END) 付费三次用户,CAST (CONVERT (DECIMAL (18, 2),SUM (CASE WHEN Num = 2 THEN 1 ELSE 0 END) / CAST (ISNULL(NULLIF(COUNT (F_UserID), 0), 1) AS FLOAT) * 100) AS VARCHAR (10)) + '%' AS 两次付费率,CAST (CONVERT (DECIMAL (18, 2),SUM (CASE WHEN Num = 3 THEN 1 ELSE 0 END) / CAST (ISNULL(NULLIF(COUNT (F_UserID), 0), 1) AS FLOAT) * 100) AS VARCHAR (10)) + '%' AS 三次付费率 FROM (SELECT F_UserID,COUNT (F_UserID) Num FROM UserCoreDB.dbo.T_Deposit_Verify_Result_Log WHERE F_DepositResult = 1 AND CONVERT (VARCHAR (100),F_InsertTime,23) >= '{0}' AND CONVERT (VARCHAR (100),F_InsertTime,23) <= '{1}' GROUP BY F_UserID) TEMP", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"));

            ds = DBHelperUserCoreDB.Query(sql);
            ExportExcelHelper.ExportDataSet(ds);
        }
        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
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
