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
namespace WSS.Web.Roles
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_RoleID=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(F_RoleID);
				}
			}
		}
			
	private void ShowInfo(int F_RoleID)
	{
		WSS.BLL.Roles bll=new WSS.BLL.Roles();
		WSS.Model.Roles model=bll.GetModel(F_RoleID);
		this.lblF_RoleID.Text=model.F_RoleID.ToString();
		this.chkF_IsUsed.Checked=model.F_IsUsed;
		this.txtF_Power.Text=model.F_Power;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtF_Power.Text.Trim().Length==0)
			{
				strErr+="权限不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int F_RoleID=int.Parse(this.lblF_RoleID.Text);
			bool F_IsUsed=this.chkF_IsUsed.Checked;
			string F_Power=this.txtF_Power.Text;


			WSS.Model.Roles model=new WSS.Model.Roles();
			model.F_RoleID=F_RoleID;
			model.F_IsUsed=F_IsUsed;
			model.F_Power=F_Power;

			WSS.BLL.Roles bll=new WSS.BLL.Roles();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
