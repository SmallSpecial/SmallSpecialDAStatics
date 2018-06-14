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
namespace WSS.Web.Department
{
    public partial class Show : Page
    {        
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					int F_DepartID=(Convert.ToInt32(strid));
					ShowInfo(F_DepartID);
				}
			}
		}
		
	private void ShowInfo(int F_DepartID)
	{
		WSS.BLL.Department bll=new WSS.BLL.Department();
		WSS.Model.Department model=bll.GetModel(F_DepartID);
		this.lblF_DepartID.Text=model.F_DepartID.ToString();
		this.lblF_ParentID.Text=model.F_ParentID.ToString();
		this.lblF_DepartName.Text=model.F_DepartName;

	}


    }
}
