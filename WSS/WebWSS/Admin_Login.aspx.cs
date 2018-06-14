using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS
{
    public partial class Admin_Login :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.UserName.Text = Request.Cookies["username"] == null ? "" : Request.Cookies["username"].Value.ToString();
                Session["FID"] = null;
            }
        }

        protected void ButtonSub_Click(object sender, EventArgs e)
        {
            string username = UserName.Text.Trim().Replace("'", "");
            string password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Password.Text.Trim().Replace("'", ""), "MD5").ToLower();

            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(Password.Text.Trim()))
                {
                    lblMessage.Text = App_GlobalResources.Language.Tip_LoginAccountExistsEmpty;
                    return;
                }
                string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
                DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
                DBHelperGSSDB.connectionString = ConnStrGSSDB;

                string sql = @"SELECT T_Users.F_UserID as F_UserID,T_Users.F_RealName as F_RealName,T_Roles.F_Power as F_Power FROM T_Roles WITH(NOLOCK) INNER JOIN T_Users  WITH(NOLOCK)  ON T_Roles.F_RoleID = T_Users.F_RoleID  
WHERE (T_Users.F_UserName = N'" + username + "') AND (T_Users.F_PassWord = N'" + password + "') AND (T_Users.F_IsUsed = 1) AND (T_Roles.F_IsUsed = 1) AND ( (T_Roles.F_Power like N'%,106100,%') or (T_Roles.F_Power like N'%,106101,%') )";
                System.Data.DataSet ds = DBHelperGSSDB.Query(sql);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    lblMessage.Text = App_GlobalResources.Language.Tip_LoginAcountError;
                }
                else
                {
                    Response.Cookies["username"].Value = username;
                    Session["FID"] = ds.Tables[0].Rows[0]["F_UserID"];
                    Session["LoginUser"] = username;
                    Session["LoginName"] = ds.Tables[0].Rows[0]["F_RealName"];
                    Session["F_Power"] = ds.Tables[0].Rows[0]["F_Power"];

                    Response.Redirect("Admin_Manage.aspx");
                }
            }
            catch (System.Exception ex)
            {
                lblMessage.Text = App_GlobalResources.Language.Tip_ConfigError + ex.Message + "！";
            }
        }
        [WebMethod]
        public static string SwitchLanguage(string language)
        {
            //更新webconfig
           return UpdateSingleAppset("PageLanguage", language);
        }
        [WebMethod]
        public static string GetLanguage() 
        {
            return PageLanguage;
        }
    }
}
