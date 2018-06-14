using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSS.Web.Dictionary
{
    public partial class delete : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            			if (!Page.IsPostBack)
			{
				WSS.BLL.Dictionary bll=new WSS.BLL.Dictionary();
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string F_DicID= Request.Params["id"];
					bll.Delete(F_DicID);
					Response.Redirect("list.aspx");
				}
			}

        }
    }
}