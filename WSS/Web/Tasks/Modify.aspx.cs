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
using Maticsoft.Common;
using LTP.Accounts.Bus;
namespace WSS.Web.Tasks
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_ID=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(F_ID);
				}
			}
		}
			
	private void ShowInfo(int F_ID)
	{
		WSS.BLL.Tasks bll=new WSS.BLL.Tasks();
		WSS.Model.Tasks model=bll.GetModel(F_ID);
		this.lblF_ID.Text=model.F_ID.ToString();
		this.txtF_Title.Text=model.F_Title;
		this.txtF_Note.Text=model.F_Note;
		this.txtF_From.Text=model.F_From;
		this.txtF_Type.Text=model.F_Type;
		this.txtF_JinjiLevel.Text=model.F_JinjiLevel;
		this.txtF_GameName.Text=model.F_GameName;
		this.txtF_GameZone.Text=model.F_GameZone;
		this.txtF_GUserID.Text=model.F_GUserID;
		this.txtF_GRoleName.Text=model.F_GRoleName;
		this.txtF_Tag.Text=model.F_Tag;
		this.txtF_DateTime.Text=model.F_DateTime;
		this.txtF_State.Text=model.F_State;
		this.txtF_Telphone.Text=model.F_Telphone;
		this.txtF_DutyMan.Text=model.F_DutyMan;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtF_Title.Text.Trim().Length==0)
			{
				strErr+="F_Title不能为空！\\n";	
			}
			if(this.txtF_Note.Text.Trim().Length==0)
			{
				strErr+="F_Note不能为空！\\n";	
			}
			if(this.txtF_From.Text.Trim().Length==0)
			{
				strErr+="F_From不能为空！\\n";	
			}
			if(this.txtF_Type.Text.Trim().Length==0)
			{
				strErr+="F_Type不能为空！\\n";	
			}
			if(this.txtF_JinjiLevel.Text.Trim().Length==0)
			{
				strErr+="F_JinjiLevel不能为空！\\n";	
			}
			if(this.txtF_GameName.Text.Trim().Length==0)
			{
				strErr+="F_GameName不能为空！\\n";	
			}
			if(this.txtF_GameZone.Text.Trim().Length==0)
			{
				strErr+="F_GameZone不能为空！\\n";	
			}
			if(this.txtF_GUserID.Text.Trim().Length==0)
			{
				strErr+="F_GUserID不能为空！\\n";	
			}
			if(this.txtF_GRoleName.Text.Trim().Length==0)
			{
				strErr+="F_GRoleName不能为空！\\n";	
			}
			if(this.txtF_Tag.Text.Trim().Length==0)
			{
				strErr+="F_Tag不能为空！\\n";	
			}
			if(this.txtF_DateTime.Text.Trim().Length==0)
			{
				strErr+="F_DateTime不能为空！\\n";	
			}
			if(this.txtF_State.Text.Trim().Length==0)
			{
				strErr+="F_State不能为空！\\n";	
			}
			if(this.txtF_Telphone.Text.Trim().Length==0)
			{
				strErr+="F_Telphone不能为空！\\n";	
			}
			if(this.txtF_DutyMan.Text.Trim().Length==0)
			{
				strErr+="F_DutyMan不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int F_ID=int.Parse(this.lblF_ID.Text);
			string F_Title=this.txtF_Title.Text;
			string F_Note=this.txtF_Note.Text;
			string F_From=this.txtF_From.Text;
			string F_Type=this.txtF_Type.Text;
			string F_JinjiLevel=this.txtF_JinjiLevel.Text;
			string F_GameName=this.txtF_GameName.Text;
			string F_GameZone=this.txtF_GameZone.Text;
			string F_GUserID=this.txtF_GUserID.Text;
			string F_GRoleName=this.txtF_GRoleName.Text;
			string F_Tag=this.txtF_Tag.Text;
			string F_DateTime=this.txtF_DateTime.Text;
			string F_State=this.txtF_State.Text;
			string F_Telphone=this.txtF_Telphone.Text;
			string F_DutyMan=this.txtF_DutyMan.Text;


			WSS.Model.Tasks model=new WSS.Model.Tasks();
			model.F_ID=F_ID;
			model.F_Title=F_Title;
			model.F_Note=F_Note;
			model.F_From=F_From;
			model.F_Type=F_Type;
			model.F_JinjiLevel=F_JinjiLevel;
			model.F_GameName=F_GameName;
			model.F_GameZone=F_GameZone;
			model.F_GUserID=F_GUserID;
			model.F_GRoleName=F_GRoleName;
			model.F_Tag=F_Tag;
			model.F_DateTime=F_DateTime;
			model.F_State=F_State;
			model.F_Telphone=F_Telphone;
			model.F_DutyMan=F_DutyMan;

			WSS.BLL.Tasks bll=new WSS.BLL.Tasks();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
