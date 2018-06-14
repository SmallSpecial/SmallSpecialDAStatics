using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Ext.Web;

namespace WSS.Web.StatF
{
    public partial class StatIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Logout_Click(object sender, AjaxEventArgs e)
        {
            Ext.Msg.Confirm("提示", "您确定要退出系统吗", new MessageBox.ButtonsConfig
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
            Session["LoginName"] = null;
            Response.Redirect("../login.aspx");
        }
    }
}
