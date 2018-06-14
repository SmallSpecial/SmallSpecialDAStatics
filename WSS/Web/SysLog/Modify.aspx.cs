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
namespace WSS.Web.SysLog
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
		WSS.BLL.SysLog bll=new WSS.BLL.SysLog();
		WSS.Model.SysLog model=bll.GetModel(F_ID);
		this.lblF_ID.Text=model.F_ID.ToString();
		this.txtF_UserID.Text=model.F_UserID.ToString();
		this.txtF_UserName.Text=model.F_UserName;
		this.txtF_Note.Text=model.F_Note;
		this.txtF_URL.Text=model.F_URL;
		this.txtF_DateTime.Text=model.F_DateTime;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtF_UserID.Text))
			{
				strErr+="用户ID格式错误！\\n";	
			}
			if(this.txtF_UserName.Text.Trim().Length==0)
			{
				strErr+="用户名不能为空！\\n";	
			}
			if(this.txtF_Note.Text.Trim().Length==0)
			{
				strErr+="备注不能为空！\\n";	
			}
			if(this.txtF_URL.Text.Trim().Length==0)
			{
				strErr+="网页地址不能为空！\\n";	
			}
			if(this.txtF_DateTime.Text.Trim().Length==0)
			{
				strErr+="时间不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int F_ID=int.Parse(this.lblF_ID.Text);
			int F_UserID=int.Parse(this.txtF_UserID.Text);
			string F_UserName=this.txtF_UserName.Text;
			string F_Note=this.txtF_Note.Text;
			string F_URL=this.txtF_URL.Text;
			string F_DateTime=this.txtF_DateTime.Text;


			WSS.Model.SysLog model=new WSS.Model.SysLog();
			model.F_ID=F_ID;
			model.F_UserID=F_UserID;
			model.F_UserName=F_UserName;
			model.F_Note=F_Note;
			model.F_URL=F_URL;
			model.F_DateTime=F_DateTime;

			WSS.BLL.SysLog bll=new WSS.BLL.SysLog();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
