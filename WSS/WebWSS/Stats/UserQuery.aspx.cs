﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.Stats
{
    public partial class UserQuery : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;

            if (!IsPostBack)
            {
               // bind();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {

            string sqlwhere = " where 1=1 ";


            if (Common.Validate.IsInt(tboxUserID.Text))
            {
                sqlwhere += @" and F_UserID=" + tboxUserID.Text + "";
            }

            if (tboxUerName.Text.Trim().Length > 0)
            {
                string content = tboxUerName.Text.Replace("'", "");

                sqlwhere += @" and F_UserName like '" + content + "%'";
            }


            try
            {
                int pcount = 0;
                int psize = 20;

                psize = GridView1.PageSize;


                SqlParameter[] parameters = {
					new SqlParameter("@BigZoneID", SqlDbType.Int),
					new SqlParameter("@ZoneID", SqlDbType.Int),
                    new SqlParameter("@DBType", SqlDbType.Int),
                    new SqlParameter("@QueryTable",SqlDbType.NVarChar,50),
					new SqlParameter("@Query",SqlDbType.NVarChar,100),
                    new SqlParameter("@OrderStr",SqlDbType.NVarChar,100),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PCount", SqlDbType.Int),
					};
                parameters[0].Value =BigZoneID;
                parameters[1].Value = -1;
                parameters[2].Value = 8;
                parameters[3].Value = "T_User with(nolock)";
                parameters[4].Value = sqlwhere;
                parameters[5].Value = " order by F_UserID asc";
                parameters[6].Value = lblPageIndex.Text;
                parameters[7].Value = psize;
                parameters[8].Direction = ParameterDirection.Output;


                ds = DBHelperDigGameDB.RunProcedure("_Query_SQLSERVER", parameters, "ds", 180);


                DataView myView = ds.Tables[0].DefaultView;
                pcount = (int)parameters[8].Value;

                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                    myView.AddNew();

                    lblCount.Text = pcount.ToString();
                    lblPageCount.Text = (pcount % GridView1.PageSize == 0 ? pcount / GridView1.PageSize : pcount / GridView1.PageSize + 1).ToString();
                }
                else
                {
                    lblerro.Visible = false;
                    lblPageCount.Text = (pcount % GridView1.PageSize == 0 ? pcount / GridView1.PageSize : pcount / GridView1.PageSize + 1).ToString();
                    lblCount.Text = pcount.ToString();

                    lbtnF.Enabled = true;
                    lbtnP.Enabled = true;
                    lbtnN.Enabled = true;
                    lbtnE.Enabled = true;
                    if (lblPageIndex.Text == "1")
                    {
                        lbtnF.Enabled = false;
                        lbtnP.Enabled = false;
                    }
                    else if (lblPageIndex.Text == lblPageCount.Text)
                    {
                        lbtnN.Enabled = false;
                        lbtnE.Enabled = false;
                    }
                    tboxPageIndex.Text = lblPageIndex.Text;
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





        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");
                }

            }
            catch (System.Exception ex)
            {

            }
        }
        /// <summary>
        /// 在单击某个用于对列进行排序的超链接时发生
        /// </summary>
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sPage = e.SortExpression;
            if (ViewState["SortOrder"].ToString() == sPage)
            {
                if (ViewState["OrderDire"].ToString() == "DESC")
                    ViewState["OrderDire"] = "ASC";
                else
                    ViewState["OrderDire"] = "DESC";
            }
            else
            {
                ViewState["SortOrder"] = e.SortExpression;
            }
            bind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = "1";
            bind();
        }





        protected void lbtnF_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = "1";
            bind();
        }

        protected void lbtnP_Click(object sender, EventArgs e)
        {
            if (lblPageIndex.Text != "1")
            {
                lblPageIndex.Text = (Convert.ToInt32(lblPageIndex.Text) - 1).ToString();
                bind();
            }
        }

        protected void lbtnN_Click(object sender, EventArgs e)
        {
            if (lblPageIndex.Text != lblPageCount.Text)
            {
                lblPageIndex.Text = (Convert.ToInt32(lblPageIndex.Text) + 1).ToString();
                bind();
            }
        }

        protected void lbtnE_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = lblPageCount.Text;
            bind();
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pindex = Convert.ToInt32(tboxPageIndex.Text);
                if (pindex > 0 && pindex <= Convert.ToInt32(lblPageCount.Text))
                {
                    lblPageIndex.Text = Convert.ToInt32(tboxPageIndex.Text).ToString();
                    bind();
                }

            }
            catch (System.Exception ex)
            {

            }
        }



    }
}
