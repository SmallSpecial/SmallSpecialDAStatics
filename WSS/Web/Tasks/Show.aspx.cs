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
namespace WSS.Web.Tasks
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
		WSS.BLL.Tasks bll=new WSS.BLL.Tasks();
		WSS.Model.Tasks model=bll.GetModel(F_ID);
		this.lblF_ID.Text=model.F_ID.ToString();
		this.lblF_Title.Text=model.F_Title;
		this.lblF_Note.Text=model.F_Note;
		this.lblF_From.Text=model.F_From;
		this.lblF_Type.Text=model.F_Type;
		this.lblF_JinjiLevel.Text=model.F_JinjiLevel;
		this.lblF_GameName.Text=model.F_GameName;
		this.lblF_GameZone.Text=model.F_GameZone;
		this.lblF_GUserID.Text=model.F_GUserID;
		this.lblF_GRoleName.Text=model.F_GRoleName;
		this.lblF_Tag.Text=model.F_Tag;
		this.lblF_DateTime.Text=model.F_DateTime;
		this.lblF_State.Text=model.F_State;
		this.lblF_Telphone.Text=model.F_Telphone;
		this.lblF_DutyMan.Text=model.F_DutyMan;

	}


    }
}
