using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
namespace WSS.Web.Menus
{
    public partial class Show : Page
    {        
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					int F_MenuID=(Convert.ToInt32(strid));
					ShowInfo(F_MenuID);
				}
			}
		}
		
	private void ShowInfo(int F_MenuID)
	{
		WSS.BLL.Menus bll=new WSS.BLL.Menus();
		WSS.Model.Menus model=bll.GetModel(F_MenuID);
		this.lblF_MenuID.Text=model.F_MenuID.ToString();
		this.lblF_Name.Text=model.F_Name;
		this.lblF_ParentID.Text=model.F_ParentID.ToString();
		this.lblF_IsUsed.Text=model.F_IsUsed?"ÊÇ":"·ñ";

	}


    }
}
