using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.Reimburse
{
    public partial class Reimburse : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
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
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerach_Click(object sender, EventArgs e)
        {
            try
            {
                string txt = tbContent.Text.Trim();
                string sql = string.Empty;
                if (rbtRoleID.Checked)
                {
                    sql = string.Format("SELECT F_RoleID,F_ZoneID FROM(SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_0 WHERE F_RoleID={0} UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_1 WHERE F_RoleID={1} UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_2 WHERE F_RoleID={2} UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_3 WHERE F_RoleID={3} UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_4 WHERE F_RoleID={4})TEPM", txt, txt, txt, txt, txt);
                }
                else
                {
                    sql = string.Format("SELECT F_RoleID,F_ZoneID FROM(SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_0 WHERE F_RoleName=N'{0}' UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_1 WHERE F_RoleName=N'{1}' UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_2 WHERE F_RoleName=N'{2}' UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_3 WHERE F_RoleName=N'{3}' UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_4 WHERE F_RoleName=N'{4}')TEPM", txt, txt, txt, txt, txt);
                }
                ds = DBHelperDigGameDB.Query(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string roleID = ds.Tables[0].Rows[0]["F_RoleID"].ToString();
                    string zoneID = ds.Tables[0].Rows[0]["F_ZoneID"].ToString();
                    sql = @"SELECT * FROM OPENQUERY ([LKSV_6_ZoneRoleDataDB_0_" + zoneID + "],'SELECT F_Money,F_BindMoney,F_Gold,F_BindGold FROM t_role_base WHERE F_ID=" + roleID + "')";
                    ds = DBHelperDigGameDB.Query(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        F_Gold.Text = ds.Tables[0].Rows[0]["F_Gold"] + "";
                        F_BindGold.Text = ds.Tables[0].Rows[0]["F_BindGold"] + "";
                        F_Money.Text = ds.Tables[0].Rows[0]["F_Money"] + "";
                        F_BindMoney.Text = ds.Tables[0].Rows[0]["F_BindMoney"] + "";
                    }
                    lblinfo.Text = "";
                }
                else
                {
                    lblinfo.Text = App_GlobalResources.Language.Tip_SearchInfo;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblinfo.Text = ex.Message;
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbContent.Text = "";
            F_Gold.Text = "";
            F_BindGold.Text = "";
            F_Money.Text = "";
            F_BindMoney.Text = "";
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbContent.Text.Trim()))
            {
                lblinfo.Text = App_GlobalResources.Language.Tip_RoleIDOrRoleName;
                return;
            }
            string transactionID = string.IsNullOrEmpty(TransactionID.Text.Trim()) ? "0" : TransactionID.Text.Trim();
            string gold = string.IsNullOrEmpty(Gold.Text.Trim()) ? "0" : Gold.Text.Trim();
            string bindGold = string.IsNullOrEmpty(BindGold.Text.Trim()) ? "0" : BindGold.Text.Trim();
            string money = string.IsNullOrEmpty(Money.Text.Trim()) ? "0" : Money.Text.Trim();
            string bindMoney = string.IsNullOrEmpty(BindMoney.Text.Trim()) ? "0" : BindMoney.Text.Trim();
            string item1 = string.IsNullOrEmpty(Item1.Text.Trim()) ? "0" : Item1.Text.Trim();
            string item1Num = string.IsNullOrEmpty(Item1Num.Text.Trim()) ? "0" : Item1Num.Text.Trim();
            string item2 = string.IsNullOrEmpty(Item2.Text.Trim()) ? "0" : Item2.Text.Trim();
            string item2Num = string.IsNullOrEmpty(Item2Num.Text.Trim()) ? "0" : Item2Num.Text.Trim();
            string item3 = string.IsNullOrEmpty(Item3.Text.Trim()) ? "0" : Item3.Text.Trim();
            string item3Num = string.IsNullOrEmpty(Item3Num.Text.Trim()) ? "0" : Item3Num.Text.Trim();
            string item4 = string.IsNullOrEmpty(Item4.Text.Trim()) ? "0" : Item4.Text.Trim();
            string item4Num = string.IsNullOrEmpty(Item4Num.Text.Trim()) ? "0" : Item4Num.Text.Trim();
            string item5 = string.IsNullOrEmpty(Item5.Text.Trim()) ? "0" : Item5.Text.Trim();
            string item5Num = string.IsNullOrEmpty(Item5Num.Text.Trim()) ? "0" : Item5Num.Text.Trim();
            int isUseMail = ckbSendEmail.Checked ? 1 : 0;
            string mailTitle = "0";
            string txtMailSendName = "0";
            string txtMailContent = "0";
            if (isUseMail == 1)
            {
                mailTitle = this.mailTitle.Text.Trim();
                txtMailSendName = this.txtMailSendName.Text.Trim();
                txtMailContent = this.txtMailContent.Text.Trim();
            }
            try
            {
                string txt = tbContent.Text.Trim();
                string sql = string.Empty;
                if (rbtRoleID.Checked)
                {
                    sql = string.Format("SELECT F_RoleID,F_ZoneID FROM(SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_0 WHERE F_RoleID={0} UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_1 WHERE F_RoleID={1} UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_2 WHERE F_RoleID={2} UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_3 WHERE F_RoleID={3} UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_4 WHERE F_RoleID={4})TEPM", txt, txt, txt, txt, txt);
                }
                else
                {
                    sql = string.Format("SELECT F_RoleID,F_ZoneID FROM(SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_0 WHERE F_RoleName=N'{0}' UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_1 WHERE F_RoleName=N'{1}' UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_2 WHERE F_RoleName=N'{2}' UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_3 WHERE F_RoleName=N'{3}' UNION ALL SELECT F_RoleID,F_ZoneID FROM LKSV_2_GameCoreDB_0_1.Gamecoredb.dbo.T_RoleBaseData_4 WHERE F_RoleName=N'{4}')TEPM", txt, txt, txt, txt, txt);
                }
                ds = DBHelperDigGameDB.Query(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string roleID = ds.Tables[0].Rows[0]["F_RoleID"].ToString();
                    string zoneID = ds.Tables[0].Rows[0]["F_ZoneID"].ToString();
                    sql = string.Format("INSERT INTO OPENQUERY(LKSV_3_gsdata_db_0_" + zoneID + ",'SELECT GlobalID,transactionID,CostGold,CostBindGold,CostMoney,CostBindMoney,ItemID1,ItemNum1,ItemID2,ItemNum2,ItemID3,ItemNum3,ItemID4,ItemNum4,ItemID5,ItemNum5,F_IS_Use_Mail,F_Mail_Title,F_Mail_Content,F_Sender_Name,State,AddTime FROM COSTPLAYERGOLDTABLE WHERE 1=0') VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},N'{17}',N'{18}',N'{19}',{20},'{21}')", roleID, transactionID, gold, bindGold, money, bindMoney, item1, item1Num, item2, item2Num, item3, item3Num, item4, item4Num, item5, item5Num, isUseMail, mailTitle, txtMailContent, txtMailSendName, 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    int res = DBHelperDigGameDB.ExecuteSql(sql);
                    if (res > 0)
                    {
                        sql = string.Format("INSERT INTO [dbo].[T_CostPlayerGoldLog] ([GlobalID], [CostGold], [CostBindGold], [CostMoney], [CostBindMoney], [ItemID1], [ItemNum1], [ItemID2], [ItemNum2], [ItemID3], [ItemNum3], [ItemID4], [ItemNum4], [ItemID5], [ItemNum5], [F_IS_Use_Mail], [F_Mail_Title], [F_Mail_Content], [F_Sender_Name], [AddTime], [AddUser]) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},N'{16}',N'{17}',N'{18}',GETDATE(),'{19}')", roleID, gold, bindGold, money, bindMoney, item1, item1Num, item2, item2Num, item3, item3Num, item4, item4Num, item5, item5Num, isUseMail, mailTitle, txtMailContent, txtMailSendName, "登录人");
                        DBHelperDigGameDB.ExecuteSql(sql);
                        lblinfo.Text = App_GlobalResources.Language.Tip_Success;

                    }
                    else
                    {
                        lblinfo.Text = App_GlobalResources.Language.Tip_Failure;
                    }
                }
                else
                {
                    lblinfo.Text = App_GlobalResources.Language.Tip_SearchInfo;
                    return;
                }
            }
            catch(Exception ex)
            {
                lblinfo.Text = ex.Message;
            }
        }
        #endregion
    }
}
