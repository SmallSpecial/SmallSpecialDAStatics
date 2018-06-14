using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebWSS
{
    public partial class NewMenu : Admin_Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["LoginUser"] != null)
            {
                lblUser.Text = HttpContext.Current.Session["LoginUser"].ToString();
            }
        }

        protected void lbquitsys_Click(object sender, EventArgs e)
        {
            QuitSys();
        }

        protected string GetMac()
        {
            try
            {
                ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection queryCollection = query.Get();
                foreach (ManagementObject mo in queryCollection)
                {
                    if (mo["IPEnabled"].ToString() == "True")
                        return mo["MacAddress"].ToString();
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}