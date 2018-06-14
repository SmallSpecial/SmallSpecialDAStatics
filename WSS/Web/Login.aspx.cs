using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Coolite.Ext.Web;
using WSS.BLL;
using WSS.DBUtility;

namespace Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = " WSS统计系统";
                this.txtUsername.Text = Request.Cookies["username"]==null?"":Request.Cookies["username"].Value.ToString();         
            }
        }
        protected void btnLogin_Click(object sender, AjaxEventArgs e)
        {
            if (Session["VALIDATECODEKEY"] != null)
            {
                LoginMethod();
            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "验证码过期或者加载不成功...<br />点击<span style=\"color:#f00;\">确定</span>刷新本页面...", new MessageBox.ButtonsConfig
                {
                    Ok = new MessageBox.ButtonConfig
                    {
                        Handler = string.Format("window.location.href='{0}';", Request.Url.ToString()),
                        Text = "确定"
                    }
                }).Show();
            }
        }

        private void LoginMethod()
        {
            if (txtCard.Text == Session["VALIDATECODEKEY"].ToString())
            {

                string username = txtUsername.Text.Trim().Replace("'", "");
                string password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim().Replace("'", ""), "MD5").ToLower();

                try
                {
                    string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
                    DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
                    DBHelperGSSDB.connectionString = ConnStrGSSDB;

                    string sql = @"SELECT T_Users.F_UserID as F_UserID,T_Users.F_RealName as F_RealName FROM T_Roles WITH(NOLOCK) INNER JOIN T_Users  WITH(NOLOCK)  ON T_Roles.F_RoleID = T_Users.F_RoleID  
WHERE (T_Users.F_UserName = N'" + username + "') AND (T_Users.F_PassWord = N'" + password + "') AND (T_Users.F_IsUsed = 1) AND (T_Roles.F_IsUsed = 1) AND (T_Roles.F_Power like N'%,106100,%')";
                    System.Data.DataSet ds = DBHelperGSSDB.Query(sql);
                    if (ds == null || ds.Tables[0].Rows.Count == 0)
                    {
                        Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgError"], "您输入的帐号或密码不正确,请重新输入...").Show();
                    }
                    else
                    {
                        Response.Cookies["username"].Value = username;
                        Session["FID"] = ds.Tables[0].Rows[0]["F_UserID"];
                        Session["LoginUser"] = username;
                        Session["LoginName"] = ds.Tables[0].Rows[0]["F_RealName"];
                        //Session["RoleName"] = list[0].F_RoleID;
                        // AllOther.AddSysLog(Session["FID"].ToString(), Session["LoginName"].ToString(), "登录系统成功");
                        Response.Redirect("AdminF/index.aspx");
    
                    }
                }
                catch (System.Exception ex)
                {
                    Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgError"], "配置出现错误,请联系管理员").Show();
                }

            }
            else
            {
                Ext.Msg.Alert(ConfigurationManager.AppSettings["MsgAlert"], "您输入的验证码不正确,请重新输入...").Show();
            }
        }
    }
}
