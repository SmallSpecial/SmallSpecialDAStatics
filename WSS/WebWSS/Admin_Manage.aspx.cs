using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebWSS
{
    public partial class Admin_Manage : Admin_Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbquitsys_Click(object sender, EventArgs e)
        {
            QuitSys();
        }
    }
}
