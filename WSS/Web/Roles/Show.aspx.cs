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
namespace WSS.Web.Roles
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
					int F_RoleID=(Convert.ToInt32(strid));
					ShowInfo(F_RoleID);
				}
			}
		}
		
	private void ShowInfo(int F_RoleID)
	{
		WSS.BLL.Roles bll=new WSS.BLL.Roles();
		WSS.Model.Roles model=bll.GetModel(F_RoleID);
		this.lblF_RoleID.Text=model.F_RoleID.ToString();
		this.lblF_IsUsed.Text=model.F_IsUsed?"ÊÇ":"·ñ";
		this.lblF_Power.Text=model.F_Power;

	}


    }
}
