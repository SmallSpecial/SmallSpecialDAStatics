using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSS.Web.Department
{
    public partial class delete : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            			if (!Page.IsPostBack)
			{
				WSS.BLL.Department bll=new WSS.BLL.Department();
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_DepartID=(Convert.ToInt32(Request.Params["id"]));
					bll.Delete(F_DepartID);
					Response.Redirect("list.aspx");
				}
			}

        }
    }
}