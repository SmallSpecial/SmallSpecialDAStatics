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
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int F_DepartID=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(F_DepartID);
				}
			}
		}
			
	private void ShowInfo(int F_DepartID)
	{
		WSS.BLL.Department bll=new WSS.BLL.Department();
		WSS.Model.Department model=bll.GetModel(F_DepartID);
		this.lblF_DepartID.Text=model.F_DepartID.ToString();
		this.txtF_ParentID.Text=model.F_ParentID.ToString();
		this.txtF_DepartName.Text=model.F_DepartName;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtF_ParentID.Text))
			{
				strErr+="父编号格式错误！\\n";	
			}
			if(this.txtF_DepartName.Text.Trim().Length==0)
			{
				strErr+="部门名不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int F_DepartID=int.Parse(this.lblF_DepartID.Text);
			int F_ParentID=int.Parse(this.txtF_ParentID.Text);
			string F_DepartName=this.txtF_DepartName.Text;


			WSS.Model.Department model=new WSS.Model.Department();
			model.F_DepartID=F_DepartID;
			model.F_ParentID=F_ParentID;
			model.F_DepartName=F_DepartName;

			WSS.BLL.Department bll=new WSS.BLL.Department();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
