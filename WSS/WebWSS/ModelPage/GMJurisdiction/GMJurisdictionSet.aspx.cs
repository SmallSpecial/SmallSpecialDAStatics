using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GMJurisdiction
{
    public partial class GMJurisdictionSet : Admin_Page
    {
        #region 属性
        string gmPassWord = System.Configuration.ConfigurationManager.AppSettings["GMPassWord"];
        string gmIP = System.Configuration.ConfigurationManager.AppSettings["GMIP"];
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string DBMysqlConnection = System.Configuration.ConfigurationManager.ConnectionStrings["DBMysqlConnection"].ToString(); 
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperUserCoreDB = new DbHelperSQLP();
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
            dbHelperMySQL.connectionString = DBMysqlConnection;
            GetGameCoreDBString();
            GetUserCoreDBString();
            if (!IsPostBack)
            {
            }
        }
        #endregion
        private void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
        }
        protected void btnGM_Click(object sender, EventArgs e)
        {
            //角色ID
            string roleID = tbGMContent.Text.Trim();
            //判断角色ID
            if(string.IsNullOrEmpty(roleID))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteRoleID + "');</Script>");
                return;
            }
            //获取用户ID
            string sql = string.Format("SELECT [F_UserID] FROM [T_RoleCreate] WHERE [F_RoleID]={0}", roleID);
            ds = DBHelperGameCoreDB.Query(sql);
            if(ds==null||ds.Tables[0].Rows.Count==0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoUserInfo+ "');</Script>");
                return;
            }
            sql=string.Format("SELECT [F_UserID],[F_UserName] FROM [T_User] WHERE [F_UserID]={0}",ds.Tables[0].Rows[0][0]);
            ds = DBHelperUserCoreDB.Query(sql);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_UserInfoError + "');</Script>");
                return;
            }
            string userID = ds.Tables[0].Rows[0][0].ToString();
            string userName = ds.Tables[0].Rows[0][1].ToString();
            sql = string.Format("SELECT [F_UserID],[F_GMName],[F_GMPassWord],[F_IP],[F_RightLevel] FROM [T_GameManager] WHERE [F_UserID]={0}", userID);
            ds = DBHelperGameCoreDB.Query(sql);
            if(ds!=null&&ds.Tables[0].Rows.Count>0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_GmIsExist + "');</Script>");
                return;
            }
            sql = string.Format("INSERT INTO [T_GameManager] ([F_UserID], [F_GMName], [F_GMPassWord], [F_IP], [F_RightLevel]) VALUES ({0}, '{1}', '{2}', '{3}', 9)", userID, userName, gmPassWord, gmIP);
            int res = DBHelperGameCoreDB.ExecuteSql(sql);
            if(res>0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
            }
            else
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
            }
        }

        protected void btnWhiteList_Click(object sender, EventArgs e)
        {
            string roleID = tbWhiteListContent.Text.Trim();
            if(string.IsNullOrEmpty(roleID))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WriteRoleID + "');</Script>");
                return;
            }
            string sql = string.Format("SELECT [F_UserID] FROM [T_RoleCreate] WHERE [F_RoleID]={0}", roleID);
            ds = DBHelperGameCoreDB.Query(sql);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_NoUserInfo + "');</Script>");
                return;
            }
            sql = string.Format("SELECT [F_UserID],[F_UserName] FROM [T_User] WHERE [F_UserID]={0}", ds.Tables[0].Rows[0][0]);
            ds = DBHelperUserCoreDB.Query(sql);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_UserInfoError + "');</Script>");
                return;
            }
            string userID = ds.Tables[0].Rows[0][0].ToString();
            string userName = ds.Tables[0].Rows[0][1].ToString();
            sql = string.Format("SELECT id,uuid FROM test_users WHERE uuid=N'{0}'", userName);
            ds = dbHelperMySQL.QueryForMysql(sql);
            if(ds!=null&&ds.Tables[0].Rows.Count>0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_WhiteListIsExist + "');</Script>");
                return;
            }
            sql = string.Format("INSERT INTO test_users (`uuid`, `created_at`, `updated_at`) VALUES ('{0}', NOW(), NOW())", userName);
            int res = dbHelperMySQL.ExecuteMySql(sql);
            if(res>0)
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
            }
            else
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
            }
        }
    }
}
