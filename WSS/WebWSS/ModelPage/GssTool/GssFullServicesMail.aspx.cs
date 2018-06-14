using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GssTool
{
    public partial class GssFullServicesMail : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
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
            if (!IsPostBack)
            {
                tbSTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tbETime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                BindBigZone();
                BindBattleZone();
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)
            {
                ckbBattleZone.Items[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)
            {
                ckbBattleZone.Items[i].Selected = false;
            }
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            lblinfo.Text = "";
            string sendBattleZone = string.Empty;

            #region 全服邮件基本信息
            int taskID = Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm"));
            string strTitle = tbTitle.Text.Trim();
            if (string.IsNullOrEmpty(strTitle))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Title + "');</Script>");
                return;
            }
            string strSendUser = tbSnedUser.Text.Trim();
            if (string.IsNullOrEmpty(strSendUser))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteSendUser + "');</Script>");
                return;
            }
            string strMinLevel = tbMinLevel.Text.Trim();
            if (string.IsNullOrEmpty(strMinLevel))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteMinLevel + "');</Script>");
                return;
            }
            string strMaxLevel = tbMaxLevel.Text.Trim();
            if (string.IsNullOrEmpty(strMaxLevel))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteMaxLevel + "');</Script>");
                return;
            }
            string strStime = tbSTime.Text;
            string strETime = tbETime.Text;
            string strBigZone = "0";
            string strBattleZone = string.Empty;
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)
            {
                if (ckbBattleZone.Items[i].Selected)
                {
                    strBattleZone += ckbBattleZone.Items[i].Value + ";";
                }
            }
            if (strBattleZone.Length > 0)
            {
                strBattleZone = strBattleZone.Substring(0, strBattleZone.Length - 1);
            }
            if (string.IsNullOrEmpty(strBattleZone))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SelectBattleZone + "');</Script>");
                return;
            }
            string[] arrayBattleZone = strBattleZone.Split(';');
            string strBindGold = string.IsNullOrEmpty(tbBindGold.Text.Trim()) ? "0" : tbBindGold.Text.Trim();
            string strGold = string.IsNullOrEmpty(tbGold.Text.Trim()) ? "0" : tbGold.Text.Trim();
            string strItemID1 = string.IsNullOrEmpty(tbItemID1.Text.Trim()) ? "0" : tbItemID1.Text.Trim();
            string strItemID2 = string.IsNullOrEmpty(tbItemID2.Text.Trim()) ? "0" : tbItemID2.Text.Trim();
            string strItemID3 = string.IsNullOrEmpty(tbItemID3.Text.Trim()) ? "0" : tbItemID3.Text.Trim();
            string strItemID4 = string.IsNullOrEmpty(tbItemID4.Text.Trim()) ? "0" : tbItemID4.Text.Trim();
            string strItemID5 = string.IsNullOrEmpty(tbItemID5.Text.Trim()) ? "0" : tbItemID5.Text.Trim();
            string strItemNum1 = string.IsNullOrEmpty(tbItemNum1.Text.Trim()) ? "0" : tbItemNum1.Text.Trim();
            string strItemNum2 = string.IsNullOrEmpty(tbItemNum2.Text.Trim()) ? "0" : tbItemNum2.Text.Trim();
            string strItemNum3 = string.IsNullOrEmpty(tbItemNum3.Text.Trim()) ? "0" : tbItemNum3.Text.Trim();
            string strItemNum4 = string.IsNullOrEmpty(tbItemNum4.Text.Trim()) ? "0" : tbItemNum4.Text.Trim();
            string strItemNum5 = string.IsNullOrEmpty(tbItemNum5.Text.Trim()) ? "0" : tbItemNum5.Text.Trim();
            string strMailContent = tbMailContent.Text.Trim();
            if (string.IsNullOrEmpty(strMailContent))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteMailContent + "');</Script>");
                return;
            }
            string strBak = tbBak.Text.Trim();
            int type = 20000217;//全服邮件
            string giftStr = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}", strItemID1, strItemNum1, strItemID2, strItemNum2, strItemID3, strItemNum3, strItemID4, strItemNum4, strItemID5, strItemNum5, strBindGold, strGold);
            #endregion
            try
            {
                #region 写入Mysql-gsdata-sys_loss_award_table
                for (int m = 0; m < arrayBattleZone.Count(); m++)
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@bigZone", SqlDbType.Int) { Value = strBigZone });
                    param.Add(new SqlParameter("@ZoneID", SqlDbType.Int) { Value = Convert.ToInt32(arrayBattleZone[m]) });
                    param.Add(new SqlParameter("@logicJson", SqlDbType.NVarChar) { Value = strBattleZone + "&" + giftStr });
                    param.Add(new SqlParameter("@taskId", SqlDbType.Int) { Value = taskID });

                    DataSet ds = DBHelperGSSDB.RunProcedure("SP_AddAwardToMysql", param.ToArray(), "tableName");
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SyncDBFailure + ";已经发送成功的战区：" + sendBattleZone + "');</Script>");
                        return;
                    }
                    string sql = string.Format("INSERT INTO sys_loss_award_table (DBID,LevelMin,LevelMax,ItemID1,ItemNum1,ItemID2,ItemNum2,ItemID3,ItemNum3,ItemID4,ItemNum4,ItemID5,ItemNum5,BeginTime,InvalidTime,ItemContent,F_Mail_Title,F_Mail_Content,F_Sender_Name,F_BIND_GOLD,F_GOLD) VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},'{13}','{14}',N'{15}',N'{16}',N'{17}',N'{18}',{19},{20})", taskID, strMinLevel, strMaxLevel, strItemID1, strItemNum1, strItemID2, strItemNum2, strItemID3, strItemNum3, strItemID4, strItemNum4, strItemID5, strItemNum5, strStime, strETime, strBak, strTitle, strMailContent, strSendUser, strBindGold, strGold);

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        string link = item["name"] as string;
                        string conn = item["provider_string"] as string;
                        if (string.IsNullOrEmpty(conn))
                        {
                            continue;
                        }
                        string filter = FilterMySqlDBConnString(conn);
                        dbHelperMySQL.connectionString = filter;
                        int res = dbHelperMySQL.ExecuteMySql(sql);
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

                #region 写入[GSSDB].dbo.T_Tasks
                //F_ID-taskID
                //F_Title-标题
                //F_GPeopleName-发起人
                //F_Note-邮件内容
                //F_COther-邮件备注

                //F_URInfo-红蓝钻及道具信息
                //F_TUseData-战区
                //F_CreatTime-开始时间
                //F_EditTime-结束时间
                //F_PreDutyMan-关联taskID
                //F_OAlwaysPlace-最低级和最高级
                string strSql = string.Format("INSERT INTO [T_Tasks] ([F_Title], [F_Note], [F_From], [F_VipLevel], [F_LimitType], [F_LimitTime], [F_Type], [F_State], [F_GameName], [F_GameBigZone], [F_GameZone], [F_GUserID], [F_GUserName], [F_GRoleID], [F_GRoleName], [F_Telphone], [F_GPeopleName], [F_DutyMan], [F_PreDutyMan], [F_CreatMan], [F_CreatTime], [F_EditMan], [F_EditTime], [F_URInfo], [F_Rowtype], [F_CUserName], [F_CPSWProtect], [F_CPersonID], [F_COther], [F_OLastLoginTime], [F_OCanRestor], [F_OAlwaysPlace], [F_TToolUsed], [F_TUseData]) VALUES (N'{0}', N'{1}', 0 , 0 , 0, NULL, '{2}', '100100100', '1000', N'{3}', NULL, NULL, NULL, NULL, NULL, N'', N'{4}', NULL, {5}, '0', '{6}', '0','{7}', N'{8}', '0', '0', '0', '0', N'{9}', N'', NULL, N'" + strMinLevel + "&" + strMaxLevel + "', '1', N'{10}');", strTitle, strMailContent, type, strBigZone, strSendUser, taskID, strStime, strETime, giftStr, strBak, sendBattleZone);
                DBHelperGSSDB.ExecuteSql(strSql);

                #endregion
            }
            catch (Exception ex)
            {
                string strSql = string.Format("INSERT INTO [T_Tasks] ([F_Title], [F_Note], [F_From], [F_VipLevel], [F_LimitType], [F_LimitTime], [F_Type], [F_State], [F_GameName], [F_GameBigZone], [F_GameZone], [F_GUserID], [F_GUserName], [F_GRoleID], [F_GRoleName], [F_Telphone], [F_GPeopleName], [F_DutyMan], [F_PreDutyMan], [F_CreatMan], [F_CreatTime], [F_EditMan], [F_EditTime], [F_URInfo], [F_Rowtype], [F_CUserName], [F_CPSWProtect], [F_CPersonID], [F_COther], [F_OLastLoginTime], [F_OCanRestor], [F_OAlwaysPlace], [F_TToolUsed], [F_TUseData]) VALUES (N'{0}', N'{1}', 0 , 0 , 0, NULL, '{2}', '100100100', '1000', N'{3}', NULL, NULL, NULL, NULL, NULL, N'', N'{4}', NULL, {5}, '0', '{6}', '0','{7}', N'{8}', '0', '0', '0', '0', N'{9}', N'', NULL, N'" + strMinLevel + "&" + strMaxLevel + "', '1', N'{10}');", strTitle, strMailContent, type, strBigZone, strSendUser, taskID, strStime, strETime, giftStr, strBak, sendBattleZone);
                //DBHelperGSSDB.ExecuteSql(strSql);

                lblinfo.Text = "已经发送成功的战区：" + sendBattleZone + ";ErrorMessage:" + ex.Message;
            }
        }
        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            tbTitle.Text = "";
            tbSnedUser.Text = "";
            tbMinLevel.Text = "";
            tbMaxLevel.Text = "";
            tbSTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tbETime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
            tbMailContent.Text = "";
            tbBak.Text = "";
            tbBindGold.Text = "";
            tbGold.Text = "";
            tbItemID1.Text = "";
            tbItemID2.Text = "";
            tbItemID3.Text = "";
            tbItemID4.Text = "";
            tbItemID5.Text = "";
            tbItemNum1.Text = "";
            tbItemNum2.Text = "";
            tbItemNum3.Text = "";
            tbItemNum4.Text = "";
            tbItemNum5.Text = "";
            
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)
            {
                ckbBattleZone.Items[i].Selected = false;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定大区
        /// </summary>
        public void BindBigZone()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;
            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = 1000) AND (F_IsUsed = 1)";
            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();
                newdr["F_Name"] = App_GlobalResources.Language.LblAllBigZone;
                newdr["F_ValueGame"] = "";
                ds.Tables[0].Rows.InsertAt(newdr, 0);
                this.ddlBigZone.DataSource = ds;
                this.ddlBigZone.DataTextField = "F_Name";
                this.ddlBigZone.DataValueField = "F_ValueGame";
                this.ddlBigZone.DataBind();
            }
            catch (System.Exception ex)
            {
                ddlBigZone.Items.Clear();
                ddlBigZone.Items.Add(new ListItem(App_GlobalResources.Language.LblAllBigZone, ""));
            }
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
    }
}