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
namespace WSS.Web.PubNotice
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
		WSS.BLL.PubNotice bll=new WSS.BLL.PubNotice();
		WSS.Model.PubNotice model=bll.GetModel(F_ID);
		this.lblF_ID.Text=model.F_ID.ToString();
		this.txtF_Title.Text=model.F_Title;
		this.txtF_Note.Text=model.F_Note;
		this.txtF_DateTime.Text=model.F_DateTime;

	}

		public void btnSave_Click(object sender, EventArgs e)
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
			string F_Title=this.txtF_Title.Text;
			string F_Note=this.txtF_Note.Text;
			string F_DateTime=this.txtF_DateTime.Text;


			WSS.Model.PubNotice model=new WSS.Model.PubNotice();
			model.F_ID=F_ID;
			model.F_Title=F_Title;
			model.F_Note=F_Note;
			model.F_DateTime=F_DateTime;

			WSS.BLL.PubNotice bll=new WSS.BLL.PubNotice();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
