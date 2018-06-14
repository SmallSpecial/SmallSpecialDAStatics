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
namespace WSS.Web.Dictionary
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string F_DicID= Request.Params["id"];
					ShowInfo(F_DicID);
				}
			}
		}
			
	private void ShowInfo(string F_DicID)
	{
		WSS.BLL.Dictionary bll=new WSS.BLL.Dictionary();
		WSS.Model.Dictionary model=bll.GetModel(F_DicID);
		this.lblF_DicID.Text=model.F_DicID;
		this.txtF_ParentID.Text=model.F_ParentID;
		this.txtF_Value.Text=model.F_Value;
		this.chkF_IsUsed.Checked=model.F_IsUsed;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtF_ParentID.Text.Trim().Length==0)
			{
				strErr+="F_ParentID不能为空！\\n";	
			}
			if(this.txtF_Value.Text.Trim().Length==0)
			{
				strErr+="F_Value不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string F_DicID=this.lblF_DicID.Text;
			string F_ParentID=this.txtF_ParentID.Text;
			string F_Value=this.txtF_Value.Text;
			bool F_IsUsed=this.chkF_IsUsed.Checked;


			WSS.Model.Dictionary model=new WSS.Model.Dictionary();
			model.F_DicID=F_DicID;
			model.F_ParentID=F_ParentID;
			model.F_Value=F_Value;
			model.F_IsUsed=F_IsUsed;

			WSS.BLL.Dictionary bll=new WSS.BLL.Dictionary();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
