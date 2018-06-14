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
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
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
			string F_Name=this.txtF_Name.Text;
			int F_ParentID=int.Parse(this.txtF_ParentID.Text);
			bool F_IsUsed=this.chkF_IsUsed.Checked;

			WSS.Model.Menus model=new WSS.Model.Menus();
			model.F_Name=F_Name;
			model.F_ParentID=F_ParentID;
			model.F_IsUsed=F_IsUsed;

			WSS.BLL.Menus bll=new WSS.BLL.Menus();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
