using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using WSS.DBUtility;
using InfoSoftGlobal;
using System.Text;

namespace WebWSS
{
    public partial class Admin_Main_Data : Admin_Page
    {
        string ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        DbHelperSQLP sp = new DbHelperSQLP();

        protected void Page_Load(object sender, EventArgs e)
        {
            sp.connectionString = ConnStr;
            if (!IsPostBack)
            {
                SetInfo();
            }
        }
        private void SetInfo()
        {
            try
            {
                DataSet dst = null;
                string  sql = @"select * from T_BaseJobConfig with(nolock) where F_ID<>5";
                dst = sp.Query(sql);
                if (dst != null)
                {

                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        lblInfo2.Text = "";
                        foreach (DataRow dr in dst.Tables[0].Rows)
                        {
                            lblInfo2.Text += "统计名称:" + dr["F_JobItem"].ToString() + " 运行间隔:" + dr["F_Interval"].ToString() + "分钟 运行时间:" + dr["F_RunTime"].ToString() + "<br />";
                        }

                    }
                }

            }
            catch (System.Exception ex)
            {
                lblInfo2.Text = ex.Message;
            }


            try
            {
                string sql = @"select * from (SELECT top 100 F_ID, F_RunID, F_Msg, F_DateTime FROM T_BaseJobMsg where 1=1 order by F_ID desc) a order by F_ID asc";

                DataSet ds = sp.Query(sql);
                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                    myView.AddNew();
                }
                else
                {
                    lblerro.Visible = false;
                }

                GridView1.DataSource = myView;
                GridView1.DataBind();
            }
            catch (System.Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }

        }
    }
}
