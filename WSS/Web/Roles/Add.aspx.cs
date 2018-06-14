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
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
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
			bool F_IsUsed=this.chkF_IsUsed.Checked;
			string F_Power=this.txtF_Power.Text;

			WSS.Model.Roles model=new WSS.Model.Roles();
			model.F_IsUsed=F_IsUsed;
			model.F_Power=F_Power;

			WSS.BLL.Roles bll=new WSS.BLL.Roles();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
