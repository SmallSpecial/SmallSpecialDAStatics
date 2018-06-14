using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.CDKey
{
    public partial class CDKeyBatchList : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        DataTable dtctype;
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            InitKeyType();
            if (!IsPostBack)
            {
                BindDdlCtype();
                bind(0);
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind(int type)
        {

            string sqlwhere = " where 1=1 ";

            if (ddlKeyType.SelectedIndex > 0)
            {
                sqlwhere += @" and F_KeyType = " + ddlKeyType.SelectedValue + "";
            }
            if (Common.Validate.IsInt(tboxExcelID.Text))
            {
                sqlwhere += @" and F_ExcelID=" + tboxExcelID.Text + " ";
            }
            if (tboxNote.Text.Trim().Length > 0)
            {
                string content = tboxNote.Text.Replace("'", "");

                sqlwhere += @" and F_Note like '%" + content + "%'";
            }


            try
            {
                int pcount = 0;
                int psize = 20;

                if (type == 0)
                {
                    psize = GridView1.PageSize;
                }
                else
                {
                    psize = 200000;
                }


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
                parameters[0].Value = BigZoneID;
                parameters[1].Value = -1;
                parameters[2].Value = 8;
                parameters[3].Value = "T_CDKeyBatch with(nolock)";
                parameters[4].Value = sqlwhere;
                parameters[5].Value = " order by F_CreateTime desc";
                parameters[6].Value = lblPageIndex.Text;
                parameters[7].Value = psize;
                parameters[8].Direction = ParameterDirection.Output;


                ds = DBHelperDigGameDB.RunProcedure("_Query_SQLSERVER", parameters, "ds", 180);

                if (type != 0)
                {
                    Common.Util.OutExcel(ds.Tables[0], null, "CDKeyBatch");
                }
                else
                {
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
            }
            catch (System.Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
                lblinfo.Text = ex.Message;
            }

        }

     

        /// <summary>
        /// 得到类型名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetTypeName(DropDownList ddl, string value)
        {
            ListItem item = ddl.Items.FindByValue(value);
            return item == null ? value : item.Text;
        }

        //得到文本名称
        public string GetTextName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT ( cast(F_ExcelID as varchar(9)) +' '+F_Name+' '+F_Type+' '+F_TypeP ) as F_Name  FROM T_BaseGameName with(nolock) WHERE (F_ExcelID = " + value + ") ";
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }
        //得到角色名称
        public string GetRoleName(string bigzone, string roleid)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT  F_RoleName FROM T_BaseRoleCreate with(nolock) WHERE (F_BigZoneID =" + bigzone + ") AND (F_RoleID = " + roleid + ")  ";
                return string.Format("({0}) {1}", roleid, spg.GetSingle(sql));
            }
            catch (System.Exception ex)
            {
                return roleid;
            }
        }

        private void InitKeyType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CID", Type.GetType("System.String"));
            dt.Columns.Add("CValue", Type.GetType("System.String"));
            string[] ctypes = GetCDKeyCategory();// new string[] { "1|官方新手礼包", "2|YY贵族特权卡", "3|精英公会卡", "4|17173专属卡", "5|QQ特权卡", "6|新浪特权卡", "7|QQ会员特权卡", "8|YY皇室特权卡", "9|公会元宝卡(特殊 200张)", "10|17173爱心礼包", "11|QQ每天10次卡" };
            foreach (String ctype in ctypes)
            {
                DataRow dr = dt.NewRow();
                dr["CID"] = ctype.Split('|')[0];
                dr["CValue"] = ctype.Split('|')[1];
                dt.Rows.Add(dr);
            }
            dtctype = dt;
        }



        private void BindDdlCtype()
        {
            try
            {
                DataRow newdr = dtctype.NewRow();
                newdr["CID"] = App_GlobalResources.Language.LblAllSelect;
                newdr["CValue"] = App_GlobalResources.Language.LblAllSelect;
                dtctype.Rows.InsertAt(newdr, 0);
                this.ddlKeyType.DataSource = dtctype;
                this.ddlKeyType.DataTextField = "CValue";
                this.ddlKeyType.DataValueField = "CID";
                this.ddlKeyType.DataBind();

            }
            catch (System.Exception ex)
            {
                ddlKeyType.Items.Clear();
                ddlKeyType.Items.Add(new ListItem(App_GlobalResources.Language.LblAllSelect, ""));
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
            bind(0);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind(0);
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = "1";
            bind(0);
        }





        protected void lbtnF_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = "1";
            bind(0);
        }

        protected void lbtnP_Click(object sender, EventArgs e)
        {
            if (lblPageIndex.Text != "1")
            {
                lblPageIndex.Text = (Convert.ToInt32(lblPageIndex.Text) - 1).ToString();
                bind(0);
            }
        }

        protected void lbtnN_Click(object sender, EventArgs e)
        {
            if (lblPageIndex.Text != lblPageCount.Text)
            {
                lblPageIndex.Text = (Convert.ToInt32(lblPageIndex.Text) + 1).ToString();
                bind(0);
            }
        }

        protected void lbtnE_Click(object sender, EventArgs e)
        {
            lblPageIndex.Text = lblPageCount.Text;
            bind(0);
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pindex = Convert.ToInt32(tboxPageIndex.Text);
                if (pindex > 0 && pindex <= Convert.ToInt32(lblPageCount.Text))
                {
                    lblPageIndex.Text = Convert.ToInt32(tboxPageIndex.Text).ToString();
                    bind(0);
                }

            }
            catch (System.Exception ex)
            {

            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            bind(1);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "List")
            {
                string id =e.CommandArgument.ToString();
                Response.Redirect("CDKeyList.aspx?batchid=" + id);
            }
        }


    }
}
