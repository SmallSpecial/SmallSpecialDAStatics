using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Coolite.Ext.Web;


namespace WebZoneConfig
{
    public partial class Login : System.Web.UI.Page
    {

        log4net.ILog Log = log4net.LogManager.GetLogger("Log4NetLog");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = Resources.Global.StringSysTitle;

                //if (Session["LoginName"] != null)
                //{
                //    Response.Redirect("GameTool/GameConfigTools.aspx");
                //}
            }

        }
        protected void btnLogin_Click(object sender, AjaxEventArgs e)
        {
            Session["VALIDATECODEKEY"] = string.Empty;//后台管理工具移除验证码
            txtCard.Text = string.Empty;
            if (Session["VALIDATECODEKEY"] != null)
            {
                LoginMethod();
            }
            else
            {
                Ext.Msg.Alert("错误提示", "验证码过期或者加载不成功...<br />点击<span style=\"color:#f00;\">确定</span>刷新本页面...", new MessageBox.ButtonsConfig
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

                string username = txtUsername.Text;
                string password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim().Replace("'", ""), "MD5").ToLower();

                string WebSUrl = System.Configuration.ConfigurationManager.AppSettings["GSSWebServiceOutURL"];
                try
                {

                    GSSWebServiceOut.WebServiceGSS gss = new GSSWebServiceOut.WebServiceGSS();
                    gss.Url = WebSUrl;
                    gss.Credentials = System.Net.CredentialCache.DefaultCredentials;

                    //   Log.Info("goto WebServiceGSS:" + WebSUrl);

                    string back = gss.GameZoneToolLogin(username, password);
                    if (back == "true")
                    {
                        Session["LoginName"] = username;
                        Response.Redirect("GameTool/GameConfigTools.aspx");
                    }
                    else if (back.IndexOf("非法") >= 0)
                    {
                        Ext.Msg.Alert("错误提示", "非法请求,请联系管理员").Show();
                    }
                    else
                    {
                        Ext.Msg.Alert("错误提示", "帐号或密码不正确或无此权限,请重新输入").Show();
                    }
                }
                catch (System.Exception ex)
                {
                    Ext.Msg.Alert("错误提示", "WebService" + ex.Message).Show();
                }

            }
            else
            {
                Ext.Msg.Alert("错误提示", "您输入的验证码不正确,请重新输入...", new MessageBox.ButtonsConfig
                {
                    Ok = new MessageBox.ButtonConfig
                    {
                        Handler = "javascript:reimg();",
                        Text = "确定"
                    }
                }).Show();
            }
        }
    }
}
