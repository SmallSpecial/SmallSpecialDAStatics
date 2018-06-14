using System;
using System.Collections.Generic;
using System.Web;

namespace WebWSS.Common
{
    public class MsgBox
    {
        public static void Show(System.Web.UI.Page page, string value)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script>alert(\"" + value + "\")</script>");
        }

        public static void ShowAndRedirect(System.Web.UI.Page page, string value,string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script>alert(\"" + value + "\");window.location.href=\""+url+"\";</script>");
        }
    }
}
