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
namespace WSS.Web.Users
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_UserID=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(F_UserID);
				}
			}
		}
			
	private void ShowInfo(int F_UserID)
	{
		WSS.BLL.Users bll=new WSS.BLL.Users();
		WSS.Model.Users model=bll.GetModel(F_UserID);
		this.lblF_UserID.Text=model.F_UserID.ToString();
		this.txtF_UserName.Text=model.F_UserName;
		this.txtF_PassWord.Text=model.F_PassWord;
		this.txtF_DepartID.Text=model.F_DepartID.ToString();
		this.txtF_RoleID.Text=model.F_RoleID.ToString();
		this.chkF_Sex.Checked=model.F_Sex;
		this.txtF_Birthday.Text=model.F_Birthday.ToString();
		this.txtF_Email.Text=model.F_Email;
		this.txtF_MobilePhone.Text=model.F_MobilePhone;
		this.txtF_RegTime.Text=model.F_RegTime.ToString();
		this.txtF_LastInTime.Text=model.F_LastInTime.ToString();
		this.chkF_IsUsed.Checked=model.F_IsUsed;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtF_UserName.Text.Trim().Length==0)
			{
				strErr+="用户名不能为空！\\n";	
			}
			if(this.txtF_PassWord.Text.Trim().Length==0)
			{
				strErr+="密码不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtF_DepartID.Text))
			{
				strErr+="所属部门格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtF_RoleID.Text))
			{
				strErr+="角色名格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtF_Birthday.Text))
			{
				strErr+="生日格式错误！\\n";	
			}
			if(this.txtF_Email.Text.Trim().Length==0)
			{
				strErr+="邮箱不能为空！\\n";	
			}
			if(this.txtF_MobilePhone.Text.Trim().Length==0)
			{
				strErr+="移动电话不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtF_RegTime.Text))
			{
				strErr+="注册时间格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtF_LastInTime.Text))
			{
				strErr+="最后登录时间格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int F_UserID=int.Parse(this.lblF_UserID.Text);
			string F_UserName=this.txtF_UserName.Text;
			string F_PassWord=this.txtF_PassWord.Text;
			int F_DepartID=int.Parse(this.txtF_DepartID.Text);
			int F_RoleID=int.Parse(this.txtF_RoleID.Text);
			bool F_Sex=this.chkF_Sex.Checked;
			DateTime F_Birthday=DateTime.Parse(this.txtF_Birthday.Text);
			string F_Email=this.txtF_Email.Text;
			string F_MobilePhone=this.txtF_MobilePhone.Text;
			DateTime F_RegTime=DateTime.Parse(this.txtF_RegTime.Text);
			DateTime F_LastInTime=DateTime.Parse(this.txtF_LastInTime.Text);
			bool F_IsUsed=this.chkF_IsUsed.Checked;


			WSS.Model.Users model=new WSS.Model.Users();
			model.F_UserID=F_UserID;
			model.F_UserName=F_UserName;
			model.F_PassWord=F_PassWord;
			model.F_DepartID=F_DepartID;
			model.F_RoleID=F_RoleID;
			model.F_Sex=F_Sex;
			model.F_Birthday=F_Birthday;
			model.F_Email=F_Email;
			model.F_MobilePhone=F_MobilePhone;
			model.F_RegTime=F_RegTime;
			model.F_LastInTime=F_LastInTime;
			model.F_IsUsed=F_IsUsed;

			WSS.BLL.Users bll=new WSS.BLL.Users();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
