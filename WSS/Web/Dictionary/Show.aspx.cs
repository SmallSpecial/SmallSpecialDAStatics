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
namespace WSS.Web.Dictionary
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
					string F_DicID= strid;
					ShowInfo(F_DicID);
				}
			}
		}
		
	private void ShowInfo(string F_DicID)
	{
		WSS.BLL.Dictionary bll=new WSS.BLL.Dictionary();
		WSS.Model.Dictionary model=bll.GetModel(F_DicID);
		this.lblF_DicID.Text=model.F_DicID;
		this.lblF_ParentID.Text=model.F_ParentID;
		this.lblF_Value.Text=model.F_Value;
		this.lblF_IsUsed.Text=model.F_IsUsed?"ÊÇ":"·ñ";

	}


    }
}
