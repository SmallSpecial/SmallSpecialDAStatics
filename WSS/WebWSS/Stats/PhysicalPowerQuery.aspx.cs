using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class PhysicalPowerQuery : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            //加入当月日期按钮控件
            for (int i = 0; i <= 32; i++)
            {
                Button btn = new Button();
                btn.ID = "btndateselect" + i;
                btn.Text = i.ToString().PadLeft(2, '0').Replace("00", "<<").Replace("32", ">>");
                btn.Visible = false;
                btn.Click += new EventHandler(btn_Click);
                DateSelect.Controls.Add(btn);
            }
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;

            if (!IsPostBack)
            {
                tboxTimeB.Text = DateTime.Now.AddDays(-1).ToString(SmallDateTimeFormat);
                tboxTimeE.Text = DateTime.Now.AddDays(-1).ToString(SmallDateTimeFormat);
                if (Request.QueryString["cid"] != null)
                {
                    tboxCID.Text = Request.QueryString["cid"].ToString();
                }
                BindDdl1();
                DropDownListArea1_SelectedIndexChanged(null, null);
                bind();
            }
            ControlOutFile1.ControlOut = GridView1;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }
            //if (Convert.ToDateTime(tboxTimeB.Text) > DateTime.Now.AddDays(-1))
            //{
            //    tboxTimeB.Text = DateTime.Now.AddDays(-1).ToString(SmallDateTimeFormat)  ;
            //}

            //设置日期按钮状态
            string dates = Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-01");
            DateTime dtb = Convert.ToDateTime(dates);
            DateTime dte = dtb.AddMonths(1).AddDays(-1);
            for (int i = 0; i <= 32; i++)
            {
                Control ctl = DateSelect.FindControl("btndateselect" + i);
                if (ctl != null && ctl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctl;
                    if (dtb.AddDays(i - 1) <= DateTime.Now && (i <= dte.Day || i == 32))
                    {
                        btn.Visible = true;
                        if (btn.Text == Convert.ToDateTime(tboxTimeB.Text).ToString("dd"))
                        {
                            btn.CssClass = "buttonblueo";
                            btn.Enabled = false;
                        }
                        else
                        {
                            btn.CssClass = "buttonblue";
                            btn.Enabled = true;
                        }
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }
            }
            LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            if (DropDownListArea2.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            }



            DateTime searchdateB = DateTime.Now.AddDays(-1);
            DateTime searchdateE = DateTime.Now;



            if (tboxTimeB.Text.Length > 0)
            {
                searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            }
            if (tboxTimeE.Text.Length > 0)
            {
                searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            }
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat) + " ";

            string sqlwhere = " where 1=1 ";

            if (tboxUID.Text.Trim().Length > 0)
            {

                sqlwhere += @" and UID =" + tboxUID.Text.Replace("'", "") + "";
            }
            if (tboxCID.Text.Trim().Length > 0)
            {

                sqlwhere += @" and CID =" + tboxCID.Text.Replace("'", "") + "";
            }

            if (ddlGoldType.SelectedIndex > 0)
            {
                sqlwhere += @" and OPID = " + ddlGoldType.SelectedValue + "";
            }
            if (ddlPara1Type.SelectedIndex > 0)
            {
                sqlwhere += @" and PARA_1 = " + ddlPara1Type.SelectedValue + "";
            }

            if (tboxBAK.Text.Trim().Length > 0)
            {

                sqlwhere += @" and op_bak like ''%" + tboxBAK.Text.Replace("'", "") + "%''";
            }


            try
            {
                string bigzoneid = DropDownListArea1.SelectedValue.Split(',')[1];
                string zoneid = DropDownListArea2.SelectedValue.Split(',')[1];
                int pcount = 0;

                SqlParameter[] parameters = {
					new SqlParameter("@BigZoneID", SqlDbType.Int),
					new SqlParameter("@ZoneID", SqlDbType.Int),
                    new SqlParameter("@DBType", SqlDbType.Int),
                    new SqlParameter("@QueryTable",SqlDbType.NVarChar,50),
					new SqlParameter("@QueryDate", SqlDbType.DateTime),
					new SqlParameter("@Query",SqlDbType.NVarChar,100),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageType", SqlDbType.Int),
					new SqlParameter("@PCount", SqlDbType.Int),
					};
                parameters[0].Value = bigzoneid;
                parameters[1].Value = zoneid;
                parameters[2].Value = 0;
                parameters[3].Value = "_money_log";
                parameters[4].Value = searchdateB.ToString(SmallDateTimeFormat);
                parameters[5].Value = sqlwhere;
                parameters[6].Value = lblPageIndex.Text;
                parameters[7].Value = GridView1.PageSize;
                parameters[8].Value = lblPageType.Text;//0普通分页 1连续ID分页
                parameters[9].Direction = ParameterDirection.Output;


                ds = DBHelperDigGameDB.RunProcedure("_Query_GSLOGDB", parameters, "ds", 180);
                DataView myView = ds.Tables[0].DefaultView;
                pcount = (int)parameters[9].Value;

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
                lblinfo.Text = ex.Message;
            }

        }

        public string TransEn(string value)
        {
            if (lblEncoding.Visible)
            {
                return TranG2I(value);
            }
            else
            {
                return value;
            }
        }

        public string TransDe(string value)
        {
            if (lblDecoding.Visible)
            {
                return TranI2G(value);
            }
            else
            {
                return value;
            }
        }


        //字符集转换   
        public string TranI2G(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
                gb2312 = System.Text.Encoding.GetEncoding("gbk");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        //字符集转换   
        public string TranG2I(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("gbk");
                gb2312 = System.Text.Encoding.GetEncoding("iso8859-1");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }


        //得到道具名称
        public string GetItemName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT ( F_Vocation + '  ' + F_EquipType +' '+ F_StarLevel + ' ' + ' ' + F_Level + ' ' + F_SuitName) as F_Name  FROM T_BaseGameItem WHERE (F_ExcelID = " + value + ") ";
                return value + "." + spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
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

        /// <summary>
        /// 操作备注格式化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetOPBakFormate(string type, string value)
        {
            try
            {
                //string[] items = value.Split('\t');
                string[] items = value.Split(' ');
                if (items.Length == 1)
                {
                    items = value.Split('\t');
                }
                switch (type)
                {
                    case "0":
                        return String.Format("角色元宝:{0} 操作数量:{1} 提取率:{2}", items[2], items[3], items[4]);
                    case "2":
                        return String.Format("角色元宝:{0} 道具:{1} 数量:{2}", items[2], GetItemName(items[3]), items[4]);
                    case "3":
                        return String.Format("角色绑定元宝:{0} 脚本参数:{1}", items[2], items[3]);
                    case "5":
                        return String.Format("收/发角色ID:{0} 邮件低32位:{1} 邮件高32位:{2}", items[2], items[3], items[4]);
                    case "6":
                        return String.Format("角色元宝:{0} 角色ID:{1} ", items[2], items[3]);
                    case "8":
                        return String.Format("帮会ID:{0}", items[2]);
                    case "9":
                        return String.Format("投保等级:{0}", items[2]);
                    case "10":
                        return String.Format("线路号:{0} 镖车等级:{1} ", items[2], items[3]);
                    case "11":
                        return String.Format("活动ID:{0} 支付方式:{1} ", items[2], items[3]);
                    case "13":
                        return String.Format("订单低32位:{0} 订单高32位:{1} 价格:{2}", items[2], items[3], items[4]);
                    case "15":
                        return String.Format("订单低32位:{0} 订单高32位:{1} 交易金额:{2}", items[2], items[3], items[4]);
                    default:
                        return value;

                }


            }
            catch (System.Exception ex)
            {
                return value;
            }
        }

        public string GetNPCName(string uid, string cid)
        {
            if (cid == "0")
            {
                return GetTextName(uid);
            }
            else
            {
                return uid;
            }
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

        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string date = Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-") + btn.Text;
                if (date.IndexOf("<<") >= 0)
                {
                    date = Convert.ToDateTime(date.Replace("<<", "01")).AddDays(-1).ToString("yyyy-MM-dd");
                }
                if (date.IndexOf(">>") >= 0)
                {
                    date = Convert.ToDateTime(date.Replace(">>", "01")).AddMonths(1).ToString("yyyy-MM-dd");
                }
                tboxTimeB.Text = date;
                lblPageIndex.Text = "1";
                bind();
            }
            catch (System.Exception ex)
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
            }

        }




        public void BindDdl1()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = 1000) AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);
                //DataRow newdr = ds.Tables[0].NewRow();
                //newdr["F_Name"] = App_GlobalResources.Language.LblAllBigZone;
                //newdr["F_ValueGame"] = "";
                //ds.Tables[0].Rows.InsertAt(newdr, 0);
                this.DropDownListArea1.DataSource = ds;
                this.DropDownListArea1.DataTextField = "F_Name";
                this.DropDownListArea1.DataValueField = "F_ValueGame";
                this.DropDownListArea1.DataBind();

            }
            catch (System.Exception ex)
            {
                DropDownListArea1.Items.Clear();
                DropDownListArea1.Items.Add(new ListItem(App_GlobalResources.Language.LblAllBigZone, ""));
            }
        }
        public void BindDdl2(string id)
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = " + id + ") AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);
                //DataRow newdr = ds.Tables[0].NewRow();
                //newdr["F_Name"] = App_GlobalResources.Language.LblAllZone;

                //newdr["F_ValueGame"] = "";
                //ds.Tables[0].Rows.InsertAt(newdr, 0);
                this.DropDownListArea2.DataSource = ds;
                this.DropDownListArea2.DataTextField = "F_Name";
                this.DropDownListArea2.DataValueField = "F_ValueGame";
                this.DropDownListArea2.DataBind();

            }
            catch (System.Exception ex)
            {
                DropDownListArea2.Items.Clear();
                DropDownListArea2.Items.Add(new ListItem(App_GlobalResources.Language.LblAllZone, ""));
            }
        }
        public void BindDdl3(string id)
        {
            //DbHelperSQLP spg = new DbHelperSQLP();
            //spg.connectionString = ConnStrGSSDB;

            //string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = " + id + ") AND (F_IsUsed = 1)";

            //try
            //{
            //    ds = spg.Query(sql);
            //    //DataRow newdr = ds.Tables[0].NewRow();

            //    //newdr["F_Name"] = "所有战线";

            //    //newdr["F_ValueGame"] = "";
            //    //ds.Tables[0].Rows.InsertAt(newdr, 0);
            //    this.DropDownListArea3.DataSource = ds;
            //    this.DropDownListArea3.DataTextField = "F_Name";
            //    this.DropDownListArea3.DataValueField = "F_ValueGame";
            //    this.DropDownListArea3.DataBind();

            //}
            //catch (System.Exception ex)
            //{
            //    DropDownListArea3.Items.Clear();
            //    DropDownListArea3.Items.Add(new ListItem("所有战线", ""));
            //}
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

        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListArea2.Items.Clear();
            BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);
            BindDdl3(DropDownListArea2.SelectedValue.Split(',')[0]);

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