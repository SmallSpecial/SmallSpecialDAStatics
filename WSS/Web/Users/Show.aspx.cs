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
namespace WSS.Web.Users
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
					int F_UserID=(Convert.ToInt32(strid));
					ShowInfo(F_UserID);
				}
			}
		}
		
	private void ShowInfo(int F_UserID)
	{
		WSS.BLL.Users bll=new WSS.BLL.Users();
		WSS.Model.Users model=bll.GetModel(F_UserID);
		this.lblF_UserID.Text=model.F_UserID.ToString();
		this.lblF_UserName.Text=model.F_UserName;
		this.lblF_PassWord.Text=model.F_PassWord;
		this.lblF_DepartID.Text=model.F_DepartID.ToString();
		this.lblF_RoleID.Text=model.F_RoleID.ToString();
		this.lblF_Sex.Text=model.F_Sex?"ÊÇ":"·ñ";
		this.lblF_Birthday.Text=model.F_Birthday.ToString();
		this.lblF_Email.Text=model.F_Email;
		this.lblF_MobilePhone.Text=model.F_MobilePhone;
		this.lblF_RegTime.Text=model.F_RegTime.ToString();
		this.lblF_LastInTime.Text=model.F_LastInTime.ToString();
		this.lblF_IsUsed.Text=model.F_IsUsed?"ÊÇ":"·ñ";

	}


    }
}
