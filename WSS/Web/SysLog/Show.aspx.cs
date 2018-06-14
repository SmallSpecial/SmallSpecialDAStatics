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
namespace WSS.Web.SysLog
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
					int F_ID=(Convert.ToInt32(strid));
					ShowInfo(F_ID);
				}
			}
		}
		
	private void ShowInfo(int F_ID)
	{
		WSS.BLL.SysLog bll=new WSS.BLL.SysLog();
		WSS.Model.SysLog model=bll.GetModel(F_ID);
		this.lblF_ID.Text=model.F_ID.ToString();
		this.lblF_UserID.Text=model.F_UserID.ToString();
		this.lblF_UserName.Text=model.F_UserName;
		this.lblF_Note.Text=model.F_Note;
		this.lblF_URL.Text=model.F_URL;
		this.lblF_DateTime.Text=model.F_DateTime;

	}


    }
}
