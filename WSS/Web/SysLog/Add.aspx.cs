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
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtF_UserID.Text))
			{
				strErr+="�û�ID��ʽ����\\n";	
			}
			if(this.txtF_UserName.Text.Trim().Length==0)
			{
				strErr+="�û�������Ϊ�գ�\\n";	
			}
			if(this.txtF_Note.Text.Trim().Length==0)
			{
				strErr+="��ע����Ϊ�գ�\\n";	
			}
			if(this.txtF_URL.Text.Trim().Length==0)
			{
				strErr+="��ҳ��ַ����Ϊ�գ�\\n";	
			}
			if(this.txtF_DateTime.Text.Trim().Length==0)
			{
				strErr+="ʱ�䲻��Ϊ�գ�\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int F_UserID=int.Parse(this.txtF_UserID.Text);
			string F_UserName=this.txtF_UserName.Text;
			string F_Note=this.txtF_Note.Text;
			string F_URL=this.txtF_URL.Text;
			string F_DateTime=this.txtF_DateTime.Text;

			WSS.Model.SysLog model=new WSS.Model.SysLog();
			model.F_UserID=F_UserID;
			model.F_UserName=F_UserName;
			model.F_Note=F_Note;
			model.F_URL=F_URL;
			model.F_DateTime=F_DateTime;

			WSS.BLL.SysLog bll=new WSS.BLL.SysLog();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"����ɹ���","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
