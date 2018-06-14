using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSS.Web.Menus
{
    public partial class delete : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            			if (!Page.IsPostBack)
			{
				WSS.BLL.Menus bll=new WSS.BLL.Menus();
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_MenuID=(Convert.ToInt32(Request.Params["id"]));
					bll.Delete(F_MenuID);
					Response.Redirect("list.aspx");
				}
			}

        }
    }
}