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
    public partial class GssGiftAward : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();

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
                BindBigZone();
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            #region 发奖工单基本信息
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
            string strMailContent = tbMailContent.Text.Trim();
            if (string.IsNullOrEmpty(strMailContent))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteMailContent + "');</Script>");
                return;
            }
            if(strMailContent.Length>200)
            {
                Response.Write("<Script Language=JavaScript>alert('邮件内容不要超过200个字符！');</Script>");
                return;
            }
            string strBigZone = "0";
            string strBattleZone = "-1";
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
            string strContent = tbContent.Text.Trim();
            if (string.IsNullOrEmpty(strContent))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteUserInfo + "');</Script>");
                return;
            }
            int type = 20000214;//发奖工单
            #endregion

            #region 发奖用户信息
            DataTable dt = new DataTable("table_a");
            DataColumn dc = null;
            dc = dt.Columns.Add("RoleName", System.Type.GetType("System.String"));
            dc = dt.Columns.Add("BattleZoneNo", System.Type.GetType("System.String"));
            string giftUser=string.Empty;
            string[] strUserInfo = strContent.Split(Environment.NewLine.ToCharArray());
            foreach (string userInfo in strUserInfo)
            {
                if (!string.IsNullOrEmpty(userInfo))
                {
                    DataRow newRow;
                    newRow = dt.NewRow();
                    newRow["RoleName"] = userInfo;
                    newRow["BattleZoneNo"] = strBattleZone;
                    dt.Rows.Add(newRow);
                    giftUser+=userInfo+";";
                }
            }
            giftUser=giftUser.Substring(0,giftUser.Length-1);
            ds = new DataSet();
            ds.Tables.Add(dt);
            if(ds==null)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteUserInfo + "');</Script>");
                return;
            }
            #endregion

            try
            {
                #region 写入[GameCoreDB].dbo.T_GiftAward_List
                string sql = @"INSERT INTO T_GiftAward_List (F_AwardName, F_Note,F_State,F_CreateTime, F_ExecType,F_JobTime,F_TaskID) VALUES (N'GSS发奖 工单:" + taskID + " (自动)', N'作业10分钟后自动执行发奖',1, '" + DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss") + "' ,1,'" + DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss") + "'," + taskID + ") ;SELECT @@IDENTITY";
                object res = DBHelperGameCoreDB.GetSingle(sql);
                if(res==null)
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_GiftAwardDefeat + "');</Script>");
                    return;
                }
                #endregion

                #region 写入[GameCoreDB].dbo.T_GiftAward_Gift
                sql = @"INSERT INTO T_GiftAward_Gift ([F_AwardID],[F_ItemID1],[F_ItemNum1],[F_ItemID2],[F_ItemNum2],[F_ItemID3],[F_ItemNum3],[F_ItemID4],[F_ItemNum4],[F_ItemID5],[F_ItemNum5],[F_TaskID],F_MailTitle,F_MailSendName,F_MailContent,[F_Gold],[F_BindGold]) VALUES (" + res.ToString() + ", " + strItemID1 + "," + strItemNum1 + "," + strItemID2 + "," + strItemNum2 + "," + strItemID3 + "," + strItemNum3 + "," + strItemID4 + "," + strItemNum4 + "," + strItemID5 + "," + strItemNum5 + ", " + taskID + ",N'" + strTitle + "',N'" + strSendUser + "',N'" + strMailContent + "'," + strGold+ "," + strBindGold + ")";
                DBHelperGameCoreDB.ExecuteSql(sql);
                #endregion

                #region 写入[GameCoreDB].dbo.T_GiftAward_User
                //加入用户
                string strSql = string.Empty;
                if (rbtRoleID.Checked)
                {
                    strSql = "SELECT F_UserID,F_RoleID,-1 F_ZoneID FROM dbo.T_RoleCreate WHERE F_RoleID IN";
                }
                else
                {
                    strSql = "SELECT F_UserID,F_RoleID,-1 F_ZoneID FROM dbo.T_RoleCreate WHERE F_RoleName IN";
                }
                DataTable dtuser = ds.Tables[0];
                string startTemp = "(";
                string endTemp = ")";
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {
                    startTemp += "N'" + dtuser.Rows[i][0] + "',";
                }
                startTemp = startTemp.Substring(0, startTemp.Length - 1);
                startTemp += endTemp;
                strSql += startTemp;
                DataSet dsRole = DBHelperGameCoreDB.Query(strSql);
                DataTable dtRole = dsRole.Tables[0];

                dtRole.Columns.Add("F_AwardID", typeof(int), res.ToString());
                dtRole.Columns.Add("F_TaskID", typeof(int), taskID.ToString());
                dtRole.Columns[0].ColumnName = "F_UserID";
                dtRole.Columns[1].ColumnName = "F_RoleID";
                dtRole.Columns[2].ColumnName = "F_ZoneID";
                dtRole.Columns[3].DefaultValue = Convert.ToInt32(res);
                dtRole.Columns[4].DefaultValue = Convert.ToInt32(taskID);
                //写入[GameCoreDB].dbo.T_GiftAward_User	
                CopyAwardUserData(dtRole);
                #endregion

                #region 创建发奖作业
                sql = @"DECLARE	@Result INT EXEC [dbo].[_WSS_GiftAward_List_Job] @F_AwardID = " + res.ToString() + ",@Result = @Result OUTPUT SELECT @Result";
                object result = DBHelperGameCoreDB.GetSingle(sql);
                if (result == null || result.ToString() != "0")
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_CreateExecute + result + "" + "');</Script>");
                    return;
                }
                #endregion

                #region 写入[GSSDB].dbo.T_Tasks
                //F_ID-taskID
                //F_Title-标题
                //F_GPeopleName-发起人
                //F_Note-邮件内容
                //F_URInfo-红蓝钻及道具信息
                //F_TUseData-发奖用户信息
                //F_PreDutyMan-关联taskID
                string giftStr=string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}",strItemID1,strItemNum1,strItemID2,strItemNum2,strItemID3,strItemNum3,strItemID4,strItemNum4,strItemID5,strItemNum5,strBindGold,strGold);

                sql = string.Format("INSERT INTO [T_Tasks] ([F_Title], [F_Note], [F_From], [F_VipLevel], [F_LimitType], [F_LimitTime], [F_Type], [F_State], [F_GameName], [F_GameBigZone], [F_GameZone], [F_GUserID], [F_GUserName], [F_GRoleID], [F_GRoleName], [F_Telphone], [F_GPeopleName], [F_DutyMan], [F_PreDutyMan], [F_CreatMan], [F_CreatTime], [F_EditMan], [F_EditTime], [F_URInfo], [F_Rowtype], [F_CUserName], [F_CPSWProtect], [F_CPersonID], [F_COther], [F_OLastLoginTime], [F_OCanRestor], [F_OAlwaysPlace], [F_TToolUsed], [F_TUseData]) VALUES (N'{0}', N'{1}', 0 , 0 , 0, NULL, '{2}', '100100100', '1000', N'{3}', N'{4}', NULL, NULL, NULL, NULL, N'', N'{5}', NULL, "+taskID+", '0', GETDATE(), '0', GETDATE(), N'{6}', '0', '0', '0', '0', N'', N'', NULL, N'', '1', N'{7}');", strTitle, strMailContent,type, strBigZone, strBattleZone, strSendUser, giftStr, giftUser);
                DBHelperGSSDB.ExecuteSql(sql);
                #endregion

                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
            }
            catch (Exception ex)
            {
                throw ex;
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
            tbMailContent.Text = "";
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
            tbContent.Text = "";
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
        private void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        protected void CopyAwardUserData(DataTable dt)
        {
            DateTime beginTime = DateTime.Now;

            //声明数据库连接
            SqlConnection conn = new SqlConnection(DBHelperGameCoreDB.connectionString);
            conn.Open();
            //声明SqlBulkCopy ,using释放非托管资源
            using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
            {
                //一次批量的插入的数据量
                sqlBC.BatchSize = 1000;
                //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                sqlBC.BulkCopyTimeout = 60;

                //设置要批量写入的表
                sqlBC.DestinationTableName = "dbo.T_GiftAward_User";

                //自定义的datatable和数据库的字段进行对应
                sqlBC.ColumnMappings.Add("F_AwardID", "F_AwardID");
                sqlBC.ColumnMappings.Add("F_UserID", "F_UserID");
                sqlBC.ColumnMappings.Add("F_RoleID", "F_RoleID");
                sqlBC.ColumnMappings.Add("F_ZoneID", "F_ZoneID");
                sqlBC.ColumnMappings.Add("F_TaskID", "F_TaskID");

                //批量写入
                sqlBC.WriteToServer(dt);
            }
            conn.Dispose();
        }
        #endregion
    }
}