using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSS.Web.Users
{
    public partial class delete : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            			if (!Page.IsPostBack)
			{
				WSS.BLL.Users bll=new WSS.BLL.Users();
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_UserID=(Convert.ToInt32(Request.Params["id"]));
					bll.Delete(F_UserID);
					Response.Redirect("list.aspx");
				}
			}

        }
    }
}