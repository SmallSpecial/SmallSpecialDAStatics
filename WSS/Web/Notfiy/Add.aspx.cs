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
namespace WSS.Web.Notfiy
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtF_Title.Text.Trim().Length==0)
			{
				strErr+="标题不能为空！\\n";	
			}
			if(this.txtF_Note.Text.Trim().Length==0)
			{
				strErr+="内容不能为空！\\n";	
			}
			if(this.txtF_URL.Text.Trim().Length==0)
			{
				strErr+="指向的网页地址不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtF_DateTime.Text))
			{
				strErr+="创建时间格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtF_SeeTime.Text))
			{
				strErr+="查看时间格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtF_UserID.Text))
			{
				strErr+="用户编号格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtF_Type.Text))
			{
				strErr+="即时窗口类型格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string F_Title=this.txtF_Title.Text;
			string F_Note=this.txtF_Note.Text;
			string F_URL=this.txtF_URL.Text;
			DateTime F_DateTime=DateTime.Parse(this.txtF_DateTime.Text);
			DateTime F_SeeTime=DateTime.Parse(this.txtF_SeeTime.Text);
			bool F_IsSeed=this.chkF_IsSeed.Checked;
			int F_UserID=int.Parse(this.txtF_UserID.Text);
			int F_Type=int.Parse(this.txtF_Type.Text);

			WSS.Model.Notfiy model=new WSS.Model.Notfiy();
			model.F_Title=F_Title;
			model.F_Note=F_Note;
			model.F_URL=F_URL;
			model.F_DateTime=F_DateTime;
			model.F_SeeTime=F_SeeTime;
			model.F_IsSeed=F_IsSeed;
			model.F_UserID=F_UserID;
			model.F_Type=F_Type;

			WSS.BLL.Notfiy bll=new WSS.BLL.Notfiy();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
