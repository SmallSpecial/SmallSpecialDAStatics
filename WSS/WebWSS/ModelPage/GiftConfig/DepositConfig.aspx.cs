using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GiftConfig
{
    public partial class DepositConfig : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();
        DbHelperSQLP dbHelperMySQL = new DbHelperSQLP();

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
                //绑定战区
                BindBattleZone();
                this.F_Type.SelectedIndex = 0;
                F_SubType_Init();
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int taskID = Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm"));
            string sendBattleZone = string.Empty;

            #region 获取礼包配置基本信息

            #region 战区
            string strBigZone = "0";//大区默认值

            string strBattleZone = string.Empty;
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)//获取选中的战区
            {
                if (ckbBattleZone.Items[i].Selected)
                {
                    strBattleZone += ckbBattleZone.Items[i].Value + ";";
                }
            }
            if (strBattleZone.Length > 0)//判断是否选择战区
            {
                strBattleZone = strBattleZone.Substring(0, strBattleZone.Length - 1);
            }
            if (string.IsNullOrEmpty(strBattleZone))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SelectBattleZone + "');</Script>");
                return;
            }
            string[] arrayBattleZone = strBattleZone.Split(';');
            #endregion

            string strProductID = ProductID.Text.Trim();
            if(string.IsNullOrEmpty(strProductID))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写ProductID！');</Script>");
                return;
            }
            string strType = F_Type.SelectedValue;
            string strSubType = F_SubType.SelectedValue;

            string strOldKRWCostMoney = string.IsNullOrEmpty(F_OldKRWCostMoney.Text.Trim()) ? "0" : F_OldKRWCostMoney.Text.Trim();
            string strOldUSDCostMoney = string.IsNullOrEmpty(F_OldUSDCostMoney.Text.Trim()) ? "0" : F_OldUSDCostMoney.Text.Trim();
            string strCurKRWCostMoney = string.IsNullOrEmpty(F_CurKRWCostMoney.Text.Trim()) ? "0" : F_CurKRWCostMoney.Text.Trim();
            string strCurUSDCostMoney = string.IsNullOrEmpty(F_CurUSDCostMoney.Text.Trim()) ? "0" : F_CurUSDCostMoney.Text.Trim();

            string strGetMoney = string.IsNullOrEmpty(F_GetMoney.Text.Trim()) ? "0" : F_GetMoney.Text.Trim();
            string strPara1 = string.IsNullOrEmpty(F_Para1.Text.Trim()) ? "0" : F_Para1.Text.Trim();
            string strPara2 = string.IsNullOrEmpty(F_Para2.Text.Trim()) ? "0" : F_Para2.Text.Trim();
            string strPara3 = string.IsNullOrEmpty(F_Para3.Text.Trim()) ? "0" : F_Para3.Text.Trim();
            string strExp = string.IsNullOrEmpty(F_Exp.Text.Trim()) ? "0" : F_Exp.Text.Trim();

            string strBeginGiveBindGoldTime = string.IsNullOrEmpty(F_BeginGiveBindGoldTime.Text.Trim()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : F_BeginGiveBindGoldTime.Text.Trim();
            string strEndGiveBindGoldTime = string.IsNullOrEmpty(F_EndGiveBindGoldTime.Text.Trim()) ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") : F_EndGiveBindGoldTime.Text.Trim();

            string strFirstGiveBindGold = string.IsNullOrEmpty(F_FirstGiveBindGold.Text.Trim()) ? "0" : F_FirstGiveBindGold.Text.Trim();
            string strGiveBindGold = string.IsNullOrEmpty(F_GiveBindGold.Text.Trim()) ? "0" : F_GiveBindGold.Text.Trim();
                
            string strTitle = this.tbTitle.Text.Trim();
            if (string.IsNullOrEmpty(strTitle))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写邮件标题！');</Script>");
                return;
            }
            string strSendUser = this.tbSnedUser.Text.Trim();
            if (string.IsNullOrEmpty(strSendUser))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写发件人！');</Script>");
                return;
            }
            string strMailContent = this.tbMailContent.Text.Trim();
            if (string.IsNullOrEmpty(strMailContent))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写邮件内容！');</Script>");
                return;
            }
            #endregion

            try
            {
                #region 写入gameshop_package
                for (int m = 0; m < arrayBattleZone.Count(); m++)
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@bigZone", SqlDbType.Int) { Value = strBigZone });
                    param.Add(new SqlParameter("@ZoneID", SqlDbType.Int) { Value = Convert.ToInt32(arrayBattleZone[m]) });
                    param.Add(new SqlParameter("@logicJson", SqlDbType.NVarChar) { Value = strBattleZone});
                    param.Add(new SqlParameter("@taskId", SqlDbType.Int) { Value = taskID });

                    DataSet ds = DBHelperGSSDB.RunProcedure("SP_AddAwardToMysql", param.ToArray(), "tableName");
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SyncDBFailure + ";已经发送成功的战区：" + sendBattleZone + "');</Script>");
                        return;
                    }

                    string link = ds.Tables[0].Rows[0]["name"] as string;
                    string conn = ds.Tables[0].Rows[0]["provider_string"] as string;
                    string filter = FilterMySqlDBConnString(conn);
                    dbHelperMySQL.connectionString = filter;

                    string sqlSelect = string.Format("SELECT * FROM deposit_table WHERE F_ProductID={0}", strProductID);
                    ds = dbHelperMySQL.QueryForMysql(sqlSelect);
                    
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        string sqlInsert = string.Format("INSERT INTO deposit_table (F_ProductID, F_Type, F_SubType, F_OldKRWCostMoney, F_OldUSDCostMoney, F_CurKRWCostMoney, F_CurUSDCostMoney, F_GetMoney, F_Para1, F_Para2, F_Para3, F_Exp, F_BeginGiveBindGoldTime, F_EndGiveBindGoldTime, F_FirstGiveBindGold, F_GiveBindGold, F_Mail_Title, F_Mail_Content, F_Sender_Name) VALUES ({0}, {1}, {2},{3},{4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, '{12}', '{13}', {14}, {15}, N'{16}', N'{17}',N'{18}')",strProductID,strType,strSubType,strOldKRWCostMoney,strOldUSDCostMoney,strCurKRWCostMoney,strCurUSDCostMoney,strGetMoney,strPara1,strPara2,strPara3,strExp,strBeginGiveBindGoldTime,strEndGiveBindGoldTime,strFirstGiveBindGold,strGiveBindGold,strTitle,strMailContent,strSendUser);

                        int res = dbHelperMySQL.ExecuteMySql(sqlInsert);
                        if (res > 0)
                        {
                        }
                        else
                        {
                            Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "已经发送成功的战区：" + sendBattleZone + "');</Script>");
                            return;
                        }
                    }
                    else
                    {
                        string sqlUpdate = string.Format("UPDATE deposit_table SET F_ProductID={0}, F_Type={1}, F_SubType={2}, F_OldKRWCostMoney={3}, F_OldUSDCostMoney={4}, F_CurKRWCostMoney={5}, F_CurUSDCostMoney={6}, F_GetMoney={7}, F_Para1={8}, F_Para2={9}, F_Para3={10}, F_Exp={11}, F_BeginGiveBindGoldTime='{12}', F_EndGiveBindGoldTime='{13}', F_FirstGiveBindGold={14}, F_GiveBindGold={15}, F_Mail_Title=N'{16}', F_Mail_Content=N'{17}', F_Sender_Name=N'{18}' WHERE (F_ProductID={19})", strProductID, strType, strSubType, strOldKRWCostMoney, strOldUSDCostMoney, strCurKRWCostMoney, strCurUSDCostMoney, strGetMoney, strPara1, strPara2, strPara3, strExp, strBeginGiveBindGoldTime, strEndGiveBindGoldTime, strFirstGiveBindGold, strGiveBindGold, strTitle, strMailContent, strSendUser, strProductID);

                        int res = dbHelperMySQL.ExecuteMySql(sqlUpdate);
                        if (res > 0)
                        {
                        }
                        else
                        {
                            Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "已经发送成功的战区：" + sendBattleZone + "');</Script>");
                            return;
                        }
                    }
                    sendBattleZone += arrayBattleZone[m] + ";";
                }
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                #endregion

                #region 写入T_GiftConfigLog表
                string sql = string.Format("INSERT INTO T_DepositConfigLog (F_ProductID, F_Type, F_SubType, F_OldKRWCostMoney, F_OldUSDCostMoney, F_CurKRWCostMoney, F_CurUSDCostMoney, F_GetMoney, F_Para1, F_Para2, F_Para3, F_Exp, F_BeginGiveBindGoldTime, F_EndGiveBindGoldTime, F_FirstGiveBindGold, F_GiveBindGold, F_Mail_Title, F_Mail_Content, F_Sender_Name,F_BattleZone,F_OPTime) VALUES ({0}, {1}, {2},{3},{4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, '{12}', '{13}', {14}, {15}, N'{16}', N'{17}',N'{18}',N'{19}',GETDATE())", strProductID, strType, strSubType, strOldKRWCostMoney, strOldUSDCostMoney, strCurKRWCostMoney, strCurUSDCostMoney, strGetMoney, strPara1, strPara2, strPara3, strExp, strBeginGiveBindGoldTime, strEndGiveBindGoldTime, strFirstGiveBindGold, strGiveBindGold, strTitle, strMailContent, strSendUser, sendBattleZone);
                DBHelperDigGameDB.ExecuteSql(sql);
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //战区
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)
            {
                ckbBattleZone.Items[i].Selected = false;
            }
            this.ProductID.Text = "";
            this.F_Type.SelectedIndex = 0;
            this.F_SubType.SelectedIndex = 0;
            this.F_OldKRWCostMoney.Text = "";
            this.F_OldUSDCostMoney.Text = "";
            this.F_CurKRWCostMoney.Text = "";
            this.F_CurUSDCostMoney.Text = "";
            this.F_GetMoney.Text = "";
            this.F_Para1.Text = "";
            this.F_Para2.Text = "";
            this.F_Para3.Text = "";
            this.F_Exp.Text = "";
            this.F_BeginGiveBindGoldTime.Text = "";
            this.F_EndGiveBindGoldTime.Text = "";
            this.F_FirstGiveBindGold.Text = "";
            this.F_GiveBindGold.Text = "";
            this.tbTitle.Text = "";
            this.tbSnedUser.Text = "";
            this.tbMailContent.Text = "";
        }
        #endregion

        #region 方法
        private void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        /// <summary>
        /// 绑定战区
        /// </summary>
        public void BindBattleZone()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value,F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = 100001) AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();

                this.ckbBattleZone.DataSource = ds;
                this.ckbBattleZone.DataTextField = "F_Name";
                this.ckbBattleZone.DataValueField = "F_ValueGame";
                this.ckbBattleZone.DataBind();
            }
            catch (System.Exception ex)
            {
            }
        }
        public static string FilterMySqlDBConnString(string dbConstring)
        {
            string[] filter = new string[] { "driver", "option", "stmt" };
            string[] mysqlConfigs = dbConstring.Split(';');
            List<string> connConfig = new List<string>();
            foreach (string item in mysqlConfigs)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                if (!filter.Contains(item.Split('=')[0].ToLower()))
                {
                    connConfig.Add(item);
                }
            }
            return string.Join(";", connConfig.ToArray());
        }
        #endregion

        protected void F_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(F_Type.SelectedValue=="0")
            {
                F_SubType.Items.Clear();
                F_SubType.Items.Add(new ListItem("6元", "0"));
                F_SubType.Items.Add(new ListItem("30元", "1"));
                F_SubType.Items.Add(new ListItem("68元", "2"));
                F_SubType.Items.Add(new ListItem("128元", "3"));
                F_SubType.Items.Add(new ListItem("328元", "4"));
                F_SubType.Items.Add(new ListItem("648元", "5"));

                GetMoney.Visible = true;
            }
            else if(F_Type.SelectedValue=="1")
            {
                F_SubType.Items.Clear();
                F_SubType.Items.Add(new ListItem("月卡", "0"));
                F_SubType.Items.Add(new ListItem("周卡", "1"));

                GetMoney.Visible = true;
                Para1.Text = "每日领取获得绑定钻石数量";
                Para2.Visible = false;
                F_Para2.Visible = false;
                Para3.Visible = false;
                F_Para3.Visible = false;

            }
            else if(F_Type.SelectedValue=="2")
            {
                F_SubType.Items.Clear();
                F_SubType.Items.Add(new ListItem("", ""));

                GetMoney.Visible = false;
                Para1.Visible = false;
                F_Para1.Visible = false;
                Para2.Visible = false;
                F_Para2.Visible = false;
                Para3.Visible = false;
                F_Para3.Visible = false;
            }
        }
        protected void F_SubType_Init()
        {
            F_SubType.Items.Clear();
            F_SubType.Items.Add(new ListItem("6元", "0"));
            F_SubType.Items.Add(new ListItem("30元", "1"));
            F_SubType.Items.Add(new ListItem("68元", "2"));
            F_SubType.Items.Add(new ListItem("128元", "3"));
            F_SubType.Items.Add(new ListItem("328元", "4"));
            F_SubType.Items.Add(new ListItem("648元", "5"));
        }
    }
}