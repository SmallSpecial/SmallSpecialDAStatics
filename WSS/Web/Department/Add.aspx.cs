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
namespace WSS.Web.Department
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

       protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtF_ParentID.Text))
			{
				strErr+="����Ÿ�ʽ����\\n";	
			}
			if(this.txtF_DepartName.Text.Trim().Length==0)
			{
				strErr+="����������Ϊ�գ�\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int F_ParentID=int.Parse(this.txtF_ParentID.Text);
			string F_DepartName=this.txtF_DepartName.Text;

			WSS.Model.Department model=new WSS.Model.Department();
			model.F_ParentID=F_ParentID;
			model.F_DepartName=F_DepartName;

			WSS.BLL.Department bll=new WSS.BLL.Department();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"����ɹ���","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
