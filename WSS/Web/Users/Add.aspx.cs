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
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtF_UserName.Text.Trim().Length==0)
			{
				strErr+="�û�������Ϊ�գ�\\n";	
			}
			if(this.txtF_PassWord.Text.Trim().Length==0)
			{
				strErr+="���벻��Ϊ�գ�\\n";	
			}
			if(!PageValidate.IsNumber(txtF_DepartID.Text))
			{
				strErr+="�������Ÿ�ʽ����\\n";	
			}
			if(!PageValidate.IsNumber(txtF_RoleID.Text))
			{
				strErr+="��ɫ����ʽ����\\n";	
			}
			if(!PageValidate.IsDateTime(txtF_Birthday.Text))
			{
				strErr+="���ո�ʽ����\\n";	
			}
			if(this.txtF_Email.Text.Trim().Length==0)
			{
				strErr+="���䲻��Ϊ�գ�\\n";	
			}
			if(this.txtF_MobilePhone.Text.Trim().Length==0)
			{
				strErr+="�ƶ��绰����Ϊ�գ�\\n";	
			}
			if(!PageValidate.IsDateTime(txtF_RegTime.Text))
			{
				strErr+="ע��ʱ���ʽ����\\n";	
			}
			if(!PageValidate.IsDateTime(txtF_LastInTime.Text))
			{
				strErr+="����¼ʱ���ʽ����\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
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
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"����ɹ���","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
