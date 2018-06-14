using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSS.Web.Roles
{
    public partial class delete : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            			if (!Page.IsPostBack)
			{
				WSS.BLL.Roles bll=new WSS.BLL.Roles();
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_RoleID=(Convert.ToInt32(Request.Params["id"]));
					bll.Delete(F_RoleID);
					Response.Redirect("list.aspx");
				}
			}

        }
    }
}