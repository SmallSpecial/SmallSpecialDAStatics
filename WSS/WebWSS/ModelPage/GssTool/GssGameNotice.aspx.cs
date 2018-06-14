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
    public partial class GssGameNotice : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        DbHelperSQLP dbHelperMySQL = new DbHelperSQLP();

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
                tbSTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tbETime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                
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
            #region 喊话工单基本信息
            lblinfo.Text = "";
            int taskID = Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm"));
            //标题
            string strTitle = tbTitle.Text.Trim();
            if (string.IsNullOrEmpty(strTitle))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Title + "');</Script>");
                return;
            }
            //发起人
            string strSendUser = tbSnedUser.Text.Trim();
            if (string.IsNullOrEmpty(strSendUser))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteSendUser + "');</Script>");
                return;
            }
            //战区
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
            //公告内容
            string strMailContent = tbMailContent.Text;
            if(string.IsNullOrEmpty(strMailContent))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_CreateCodeInfo + "');</Script>");
                return;
            }
            //备注
            string strBak = tbBak.Text.Trim();
            #endregion

            int rowcount = 0;
            string sendBattleZone = string.Empty;
            try
            {
                string[] arrayNoticeInfo = strMailContent.Split('|');
                for (int i = 0; i < arrayBattleZone.Length; i++)
                {
                    string sql = @"INSERT INTO T_GameNotice (F_ReciveZone, F_ReciveLine, F_ReciveObject, F_MSGLocation, F_Message, F_RunTimeBegin, F_RunTimeEnd, F_RunInterval, F_TaskState,F_TaskID, F_NoticeTimes)
VALUES     (" + arrayBattleZone[i] + ",-1,N'" + arrayNoticeInfo[1] + "', " + arrayNoticeInfo[2] + ", N'" + arrayNoticeInfo[0] + "', '" + arrayNoticeInfo[3] + "', '" + arrayNoticeInfo[4] + "', " + arrayNoticeInfo[5] + ", 1, " + taskID + ", 0)";
                    rowcount+=DBHelperGameCoreDB.ExecuteSql(sql);
                    sendBattleZone += arrayBattleZone[i]+";";
                }
                if(rowcount==arrayBattleZone.Length)
                {
                    string sql = string.Format("INSERT INTO T_Tasks ([F_Title], [F_Note], [F_From], [F_VipLevel], [F_LimitType], [F_LimitTime], [F_Type], [F_State], [F_GameName], [F_GameBigZone], [F_GameZone], [F_GUserID], [F_GUserName], [F_GRoleID], [F_GRoleName], [F_Telphone], [F_GPeopleName], [F_DutyMan], [F_PreDutyMan], [F_CreatMan], [F_CreatTime], [F_EditMan], [F_EditTime], [F_URInfo], [F_Rowtype], [F_CUserName], [F_CPSWProtect], [F_CPersonID], [F_COther], [F_OLastLoginTime], [F_OCanRestor], [F_OAlwaysPlace], [F_TToolUsed], [F_TUseData]) VALUES (N'{0}', N'{1}', '100103100', '10010501', '10010404', NULL, '20000213', '100100100', '1000', NULL, NULL, NULL, NULL, NULL, NULL, N'', N'{2}', NULL, {3}, N'{4}', GETDATE(), N'{5}', GETDATE(), N'{6}', '0', NULL, NULL, NULL, N'运行公告操作成功!', NULL, '1', NULL, '1', N'{7}')", strTitle, strBak, strSendUser, taskID, 0, 0, strMailContent, strBattleZone);
                    DBHelperGSSDB.ExecuteSql(sql);

                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + ";发送成功的战区："+sendBattleZone+"');</Script>");
                }
            }
            catch (System.Exception ex)
            {
                lblinfo.Text = "失败；发送成功的战区：" + sendBattleZone + ";ErrorInfo:" + ex.Message;
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
            tbContentInfo.Text = "";
            tbSTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tbETime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
            tbDay.Text = "0";
            tbHourse.Text="0";
            tbMinutes.Text="0";
            tbsecond.Text = "30";
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)
            {
                ckbBattleZone.Items[i].Selected = false;
            }
            tbMailContent.Text = "";
            tbBak.Text = "";
        }
        /// <summary>
        /// 生成代码到公告内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateCode_Click(object sender, EventArgs e)
        {
            //公告代码示例：公告内容|0,60|5|2017-11-16 14:24|2017-11-16 14:39|30
            string strCodeContent = string.Empty;

            if (tbContentInfo.Text.Trim().Replace("\n", "").Replace("|", "").Trim().Length == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('"+App_GlobalResources.Language.Tip_WriteNoticeInfo+"');</Script>");
                return;
            }
            //消息内容
            string strContentInfo = string.Empty;
            string[] strArrayContentInfo = tbContentInfo.Text.Replace("\n", "").Replace("|", "").Trim().Split(Environment.NewLine.ToCharArray());
            for (int i = 0; i < strArrayContentInfo.Length; i++)
            {
                if (!string.IsNullOrEmpty(strArrayContentInfo[i]))
                {
                    strContentInfo += strArrayContentInfo[i];
                }
            }
            if(strContentInfo.Length>300)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoticeLengthLimit + "');</Script>");
                return;
            }
            //最低等级
            string strMin = "0";
            //最高等级
            string strMax = "60";
            string strLevel = strMin + "," + strMax;
            //消息位置
            string strMSGLocation = "5";
            //开始时间
            string strStime = string.IsNullOrEmpty(tbSTime.Text) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : tbSTime.Text;
            //结束时间
            string strETime = string.IsNullOrEmpty(tbETime.Text) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : tbETime.Text;
            //天，默认0
            string strDay = string.IsNullOrEmpty(tbDay.Text.Trim()) ? "0" : tbDay.Text.Trim();
            //小时，默认0
            string strHourse = string.IsNullOrEmpty(tbHourse.Text.Trim()) ? "0" : tbHourse.Text.Trim();
            //分钟，默认0
            string strMinutes = string.IsNullOrEmpty(tbMinutes.Text.Trim()) ? "0" : tbMinutes.Text.Trim();
            //秒，默认30
            string strSecond = string.IsNullOrEmpty(tbsecond.Text.Trim()) ? "30" : tbsecond.Text.Trim();
            //时间间隔
            int secsum = Convert.ToInt32(strDay) * 24 * 60 * 60 + Convert.ToInt32(strHourse) * 60 * 60 + Convert.ToInt32(strMinutes) * 60 + Convert.ToInt32(strSecond);
            //公告内容代码
            strCodeContent = strContentInfo + "|" + strLevel + "|" + strMSGLocation + "|" + strStime + "|" + strETime + "|" + secsum;

            //公告内容
            tbMailContent.Text = strCodeContent;
        }
        #endregion

        #region 方法
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
        public void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        #endregion
    }
}