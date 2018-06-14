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
namespace WSS.Web.Menus
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_MenuID=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(F_MenuID);
				}
			}
		}
			
	private void ShowInfo(int F_MenuID)
	{
		WSS.BLL.Menus bll=new WSS.BLL.Menus();
		WSS.Model.Menus model=bll.GetModel(F_MenuID);
		this.lblF_MenuID.Text=model.F_MenuID.ToString();
		this.txtF_Name.Text=model.F_Name;
		this.txtF_ParentID.Text=model.F_ParentID.ToString();
		this.chkF_IsUsed.Checked=model.F_IsUsed;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtF_Name.Text.Trim().Length==0)
			{
				strErr+="菜单名称不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtF_ParentID.Text))
			{
				strErr+="父编号格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int F_MenuID=int.Parse(this.lblF_MenuID.Text);
			string F_Name=this.txtF_Name.Text;
			int F_ParentID=int.Parse(this.txtF_ParentID.Text);
			bool F_IsUsed=this.chkF_IsUsed.Checked;


			WSS.Model.Menus model=new WSS.Model.Menus();
			model.F_MenuID=F_MenuID;
			model.F_Name=F_Name;
			model.F_ParentID=F_ParentID;
			model.F_IsUsed=F_IsUsed;

			WSS.BLL.Menus bll=new WSS.BLL.Menus();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
