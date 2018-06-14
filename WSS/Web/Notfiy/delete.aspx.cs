using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSS.Web.Notfiy
{
    public partial class delete : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            			if (!Page.IsPostBack)
			{
				WSS.BLL.Notfiy bll=new WSS.BLL.Notfiy();
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_ID=(Convert.ToInt32(Request.Params["id"]));
					bll.Delete(F_ID);
					Response.Redirect("list.aspx");
				}
			}

        }
    }
}