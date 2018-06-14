using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Ext.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Text;
using System.Configuration;
using WSS.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace Web.Admin
{
    public partial class DeskTop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["FID"] != null)
            //{
            if (!IsPostBack)
            {



                Ext.Notification.Show(new Notification.Config
                        {
                            Title = "提示",
                            Width = Convert.ToInt32(ConfigurationManager.AppSettings["NotifWidth"]),
                            Height = Convert.ToInt32(ConfigurationManager.AppSettings["NotifHeight"]),
                            Icon = Icon.Accept,
                            AutoHide = Convert.ToBoolean(ConfigurationManager.AppSettings["AutoHide"]),
                            HideDelay = Convert.ToInt32(ConfigurationManager.AppSettings["HideDelay"]),
                            Shadow = true,
                            Html = "<br >桌面图标暂时均链接到全部统计<br >点击一项即可<embed src = 'notify.wav' width = '0' height = '0' id = 'music' autostart = 'true'></embed>",
                            //AutoLoad = new LoadConfig(ConfigurationManager.AppSettings["AutoLoad"] + "?id=" + dr["F_ID"].ToString() + "", LoadMode.IFrame, false)
                        });


            }
            //    }
            //    if (!Ext.IsAjaxRequest)
            //    {
            //    }
            //}
            //else
            //{
            //    Response.Redirect("../login.aspx");
            //}
        }





        protected void Logout_Click(object sender, AjaxEventArgs e)
        {
            Ext.Msg.Confirm(ConfigurationManager.AppSettings["MsgTitle"], "您确定要退出系统吗", new MessageBox.ButtonsConfig
            {
                Yes = new MessageBox.ButtonConfig
                {
                    Handler = "Coolite.AjaxMethods.SignOut();",
                    Text = "确 定"
                },
                No = new MessageBox.ButtonConfig
                {
                    Text = "取 消"
                }
            }).Show();
        }
        [AjaxMethod]
        public void SignOut()
        {
            Response.Redirect("../login.aspx");
        }
        protected void ReFreshCache_Click(object sender, AjaxEventArgs e)
        {

        }
    }
}
