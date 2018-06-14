using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Coolite.Ext.Web;

namespace WebZoneConfig.GameTool
{
    public partial class GameConfigTools : System.Web.UI.Page
    {
        log4net.ILog Log = log4net.LogManager.GetLogger("Log4NetLog");
        protected WebServiceZoneConfig.ServiceZ xlj = new WebServiceZoneConfig.ServiceZ();
        public string ddd = "dddddd";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Ext.IsAjaxRequest)
            //{
            //    string jsons = xlj.GetBattleZones();

            //    StoreBattleZone.LoadData(jsons);
            //}
            //Log.Info(" goto GameConfigTools.aspx_Page_Load; the session[LoginName]: " + Session["LoginName"]);

            if (Session["LoginName"] == null || Session["LoginName"] == "")
            {
                Response.Redirect("../Login.aspx");
            }


        }
        protected void Logout_Click(object sender, AjaxEventArgs e)
        {
            Ext.Msg.Confirm("提示", "您确定要退出战区架设工具吗", new MessageBox.ButtonsConfig
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
            Session["LoginName"] = "";
            Response.Redirect("../login.aspx");
        }



    }
}
