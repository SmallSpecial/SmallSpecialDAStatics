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
    public partial class RechargeRegion : Admin_Page
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
            string sql = string.Format(@"SELECT CASE WHEN F_Price>=0 AND F_Price<=9 THEN 1
						                             WHEN F_Price>=10 AND F_Price<=29 THEN 2
						                             WHEN F_Price>=30 AND F_Price<=49 THEN 3
						                             WHEN F_Price>=50 AND F_Price<=99 THEN 4
						                             WHEN F_Price>=100 AND F_Price<=199 THEN 5
						                             WHEN F_Price>=200 AND F_Price<=299 THEN 6
						                             WHEN F_Price>=300 AND F_Price<=499 THEN 7
						                             WHEN F_Price>=500 AND F_Price<=999 THEN 8
						                             WHEN F_Price>=1000 AND F_Price<=1999 THEN 9 
						                             WHEN F_Price>=2000 AND F_Price<=4999 THEN 10
						                             WHEN F_Price>=5000 AND F_Price<=9999 THEN 11
						                             WHEN F_Price>=10000 AND F_Price<=29999 THEN 12
						                             WHEN F_Price>=30000 AND F_Price<=49999 THEN 13
						                             WHEN F_Price>=50000 AND F_Price<=99999 THEN 14
						                             WHEN F_Price>=100000 THEN 15
						                             ELSE '' END AS serialNum,
				                        ISNULL(COUNT(F_UserID),0) RechargeNumOfPeople,SUM(F_Price) RechargeAmount,SUM(F_Price)/COUNT(F_UserID) ARPU
                                        FROM (
                                            SELECT F_UserID,SUM(F_Price) AS F_Price
                                            FROM T_Deposit_Verify_Result_Log
                                            WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' {2}
                                            GROUP BY F_UserID
                                        )TEMP
                                        GROUP BY 
                                        CASE WHEN F_Price>=0 AND F_Price<=9 THEN 1
						                     WHEN F_Price>=10 AND F_Price<=29 THEN 2
						                     WHEN F_Price>=30 AND F_Price<=49 THEN 3
						                     WHEN F_Price>=50 AND F_Price<=99 THEN 4
						                     WHEN F_Price>=100 AND F_Price<=199 THEN 5
						                     WHEN F_Price>=200 AND F_Price<=299 THEN 6
						                     WHEN F_Price>=300 AND F_Price<=499 THEN 7
						                     WHEN F_Price>=500 AND F_Price<=999 THEN 8
						                     WHEN F_Price>=1000 AND F_Price<=1999 THEN 9 
						                     WHEN F_Price>=2000 AND F_Price<=4999 THEN 10
						                     WHEN F_Price>=5000 AND F_Price<=9999 THEN 11
						                     WHEN F_Price>=10000 AND F_Price<=29999 THEN 12
						                     WHEN F_Price>=30000 AND F_Price<=49999 THEN 13
						                     WHEN F_Price>=50000 AND F_Price<=99999 THEN 14
						                     WHEN F_Price>=100000 THEN 15
						                     ELSE '' END
                                       ORDER BY serialNum", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                if(ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count==0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("serialNum", System.Type.GetType("System.String"));
                    dt.Columns.Add("RechargeNumOfPeople", System.Type.GetType("System.String"));
                    dt.Columns.Add("RechargeAmount", System.Type.GetType("System.String"));
                    dt.Columns.Add("ARPU", System.Type.GetType("System.String"));
                    for(int i=0;i<15;i++)
                    {
                        DataRow newRow=dt.NewRow();
                        newRow["serialNum"] = i+1;
                        newRow["RechargeNumOfPeople"] = "0";
                        newRow["RechargeAmount"] = "0";
                        newRow["ARPU"] = "0";
                        dt.Rows.Add(newRow); 
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = ds.Tables[0].DefaultView;
                    GridView1.DataBind();
                }
                
                
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
            string sql = string.Format(@"SELECT CASE WHEN F_Price>=0 AND F_Price<=9 THEN 1
						                             WHEN F_Price>=10 AND F_Price<=29 THEN 2
						                             WHEN F_Price>=30 AND F_Price<=49 THEN 3
						                             WHEN F_Price>=50 AND F_Price<=99 THEN 4
						                             WHEN F_Price>=100 AND F_Price<=199 THEN 5
						                             WHEN F_Price>=200 AND F_Price<=299 THEN 6
						                             WHEN F_Price>=300 AND F_Price<=499 THEN 7
						                             WHEN F_Price>=500 AND F_Price<=999 THEN 8
						                             WHEN F_Price>=1000 AND F_Price<=1999 THEN 9 
						                             WHEN F_Price>=2000 AND F_Price<=4999 THEN 10
						                             WHEN F_Price>=5000 AND F_Price<=9999 THEN 11
						                             WHEN F_Price>=10000 AND F_Price<=29999 THEN 12
						                             WHEN F_Price>=30000 AND F_Price<=49999 THEN 13
						                             WHEN F_Price>=50000 AND F_Price<=99999 THEN 14
						                             WHEN F_Price>=100000 THEN 15
						                             ELSE '' END AS serialNum,
				                        ISNULL(COUNT(F_UserID),0) RechargeNumOfPeople,SUM(F_Price) RechargeAmount,SUM(F_Price)/COUNT(F_UserID) ARPU
                                        FROM (
                                            SELECT F_UserID,SUM(F_Price) AS F_Price
                                            FROM T_Deposit_Verify_Result_Log
                                            WHERE F_DepositResult=1 AND CONVERT(VARCHAR(100),F_InsertTime,23)>='{0}' AND CONVERT(VARCHAR(100),F_InsertTime,23)<='{1}' {2}
                                            GROUP BY F_UserID
                                        )TEMP
                                        GROUP BY 
                                        CASE WHEN F_Price>=0 AND F_Price<=9 THEN 1
						                     WHEN F_Price>=10 AND F_Price<=29 THEN 2
						                     WHEN F_Price>=30 AND F_Price<=49 THEN 3
						                     WHEN F_Price>=50 AND F_Price<=99 THEN 4
						                     WHEN F_Price>=100 AND F_Price<=199 THEN 5
						                     WHEN F_Price>=200 AND F_Price<=299 THEN 6
						                     WHEN F_Price>=300 AND F_Price<=499 THEN 7
						                     WHEN F_Price>=500 AND F_Price<=999 THEN 8
						                     WHEN F_Price>=1000 AND F_Price<=1999 THEN 9 
						                     WHEN F_Price>=2000 AND F_Price<=4999 THEN 10
						                     WHEN F_Price>=5000 AND F_Price<=9999 THEN 11
						                     WHEN F_Price>=10000 AND F_Price<=29999 THEN 12
						                     WHEN F_Price>=30000 AND F_Price<=49999 THEN 13
						                     WHEN F_Price>=50000 AND F_Price<=99999 THEN 14
						                     WHEN F_Price>=100000 THEN 15
						                     ELSE '' END
                                       ORDER BY serialNum", searchdateB.ToString("yyyy-MM-dd"), searchdateE.ToString("yyyy-MM-dd"), sqlWhere);
        
            ds = DBHelperUserCoreDB.Query(sql);

            DataSet newDataSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", System.Type.GetType("System.String"));
            dt.Columns.Add("价格段", System.Type.GetType("System.String"));
            dt.Columns.Add("充值人数", System.Type.GetType("System.String"));
            dt.Columns.Add("充值金额", System.Type.GetType("System.String"));
            dt.Columns.Add("ARPU", System.Type.GetType("System.String"));
            for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
            {
                DataRow newRow = dt.NewRow();
                newRow["序号"] = ds.Tables[0].Rows[i]["serialNum"];
                newRow["价格段"] = GetPrice(ds.Tables[0].Rows[i]["serialNum"].ToString());
                newRow["充值人数"] = ds.Tables[0].Rows[i]["RechargeNumOfPeople"];
                newRow["充值金额"] = ds.Tables[0].Rows[i]["RechargeAmount"];
                newRow["ARPU"] = ds.Tables[0].Rows[i]["ARPU"];
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
        protected string GetPrice(string value)
        {
            switch(value)
            {
                case "1":
                    return "0~9";
                case "2":
                    return "10~29";
                case "3":
                    return "30~49";
                case "4":
                    return "50~99";
                case "5":
                    return "100~199";
                case "6":
                    return "200~299";
                case "7":
                    return "300~499";
                case "8":
                    return "500~999";
                case "9":
                    return "1000~1999";
                case "10":
                    return "2000~4999";
                case "11":
                    return "5000~9999";
                case "12":
                    return "10000~29999";
                case "13":
                    return "30000~49999";
                case "14":
                    return "50000~99999";
                case "15":
                    return "100000";
                default:
                    return value;
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
