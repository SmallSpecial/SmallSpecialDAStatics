using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.Shop
{
    public partial class ShopBaseItemList : Admin_Page
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
                BindDdl1();
                DropDownListArea1_SelectedIndexChanged(null, null);
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            string sqlwhere = " where 1=1 ";

            if (ddlShopType.SelectedIndex > 0)
            {
                sqlwhere += @" and F_SHOPTYPE =" + ddlShopType.SelectedValue + "";
            }
            if (ddlItemType.SelectedIndex > 0)
            {
                sqlwhere += @" and F_ItemType =" + ddlItemType.SelectedValue + "";
            }

            if (tboxEXCELID.Text.Trim().Length > 0 && Common.Validate.IsDouble(tboxEXCELID.Text))
            {

                sqlwhere += @" and F_EXCELID =" + tboxEXCELID.Text.Replace("'", "") + "";
            }

            if (tboxPrice.Text.Trim().Length > 0 && Common.Validate.IsDouble(tboxPrice.Text))
            {
                string tPrice = tboxPrice.Text.Replace("'", "");

                sqlwhere += @" and F_ITEMPRICE=" + tPrice + "";
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
                parameters[2].Value = 7;
                parameters[3].Value = "gameshop_itembase";
                parameters[4].Value = null;
                parameters[5].Value = sqlwhere;
                parameters[6].Value = lblPageIndex.Text;
                parameters[7].Value = GridView1.PageSize;
                parameters[8].Value = lblPageType.Text;//0普通分页 1连续ID分页
                parameters[9].Direction = ParameterDirection.Output;


                ds = DBHelperDigGameDB.RunProcedure("_Query_GSLOGDB", parameters, "ds",180);
                DataView myView = ds.Tables[0].DefaultView;
                pcount = Convert.ToInt32(parameters[9].Value);

                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                    //myView.AddNew();

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

        //得到道具名称
        public string GetGameItemName(string value)
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

        //得到文本名称
        public string GetTextName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT top 1 F_Name  FROM T_BaseGameName WHERE (F_ExcelID = " + value + ") ";
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
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

                    if (lblCount.Text == "0" || Convert.ToInt32(lblPageIndex.Text) > Convert.ToInt32(lblPageCount.Text))
                    {
                        Button btn = FindControl("btnEdit") as Button;
                        btn.Visible = false;
                        btn = e.Row.FindControl("btnDelete") as Button;
                        btn.Visible = false;
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
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
            bind();
        }

        protected void DropDownListArea2_SelectedIndexChanged(object sender, EventArgs e)
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

        //添加
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            TextBox tboxF_EXCELID = GridView1.FooterRow.FindControl("tboxF_EXCELID") as TextBox;
            TextBox tboxF_ITEMPRICE = GridView1.FooterRow.FindControl("tboxF_ITEMPRICE") as TextBox;
            DropDownList ddlF_SHOPTYPE = GridView1.FooterRow.FindControl("ddlF_SHOPTYPE") as DropDownList;
            DropDownList ddlF_ITEMTYPE = GridView1.FooterRow.FindControl("ddlF_ITEMTYPE") as DropDownList;

            string t_ExcelID = tboxF_EXCELID.Text.Trim();
            string t_ShopType = ddlF_SHOPTYPE.SelectedValue;
            string t_ItemType = ddlF_ITEMTYPE.SelectedValue;
            string t_TempPrice = tboxF_ITEMPRICE.Text.Trim();

            if (!Common.Validate.IsInt(t_ExcelID))
            {
                Common.MsgBox.Show(this, "EXCEL编号应该为数字!");
                return;
            }
            else if (!Common.Validate.IsInt(t_TempPrice))
            {
                Common.MsgBox.Show(this, "商品价格应该为数字!");
                return;
            }

            string sqlStr = "insert into OPENQUERY([LKSV] ,'select * from gameshop_itembase limit 0,1') (F_EXCELID,F_SHOPTYPE,F_ITEMTYPE,F_ITEMPRICE) values (" + t_ExcelID + "," + t_ShopType + "," + t_ItemType + "," + t_TempPrice + ") ";
            int rcount = ExecSql(sqlStr);
            if (rcount > 0)
            {
                Common.MsgBox.Show(this, "新增道具成功!");
                tboxEXCELID.Text = "";
                ddlF_SHOPTYPE.SelectedIndex = 0;
                ddlF_ITEMTYPE.SelectedIndex = 0;
                tboxF_ITEMPRICE.Text = "";
                bind();
            }

        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //GridView1.ShowFooter = false;
            //bind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //int id = Convert.ToInt32(GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].ToString());
            //if (e.CommandName == "PEdit")
            //{
            //    Response.Redirect("ShopItemEdit.aspx?id=" + id);
            //}
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            bind();

            DropDownList ddlF_SHOPTYPE = GridView1.Rows[GridView1.EditIndex].FindControl("ddlF_SHOPTYPE") as DropDownList;
            DropDownList ddlF_ITEMTYPE = GridView1.Rows[GridView1.EditIndex].FindControl("ddlF_ITEMTYPE") as DropDownList;
            Label lblF_SHOPTYPE = GridView1.Rows[GridView1.EditIndex].FindControl("lblF_SHOPTYPE") as Label;
            Label lblF_ITEMTYPE = GridView1.Rows[GridView1.EditIndex].FindControl("lblF_ITEMTYPE") as Label;

            ddlF_SHOPTYPE.SelectedValue = lblF_SHOPTYPE.Text;
            ddlF_ITEMTYPE.SelectedValue = lblF_ITEMTYPE.Text;
        }

        //更新
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string F_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            TextBox tboxF_EXCELID = GridView1.Rows[e.RowIndex].FindControl("tboxF_EXCELID") as TextBox;
            TextBox tboxF_ITEMPRICE = GridView1.Rows[e.RowIndex].FindControl("tboxF_ITEMPRICE") as TextBox;
            DropDownList ddlF_SHOPTYPE = GridView1.Rows[e.RowIndex].FindControl("ddlF_SHOPTYPE") as DropDownList;
            DropDownList ddlF_ITEMTYPE = GridView1.Rows[e.RowIndex].FindControl("ddlF_ITEMTYPE") as DropDownList;

            string t_ExcelID = tboxF_EXCELID.Text.Trim();
            string t_ShopType = ddlF_SHOPTYPE.SelectedValue;
            string t_ItemType = ddlF_ITEMTYPE.SelectedValue;
            string t_TempPrice = tboxF_ITEMPRICE.Text.Trim();

            if (!Common.Validate.IsInt(t_ExcelID))
            {
                Common.MsgBox.Show(this, "商品EXCEL编号应该为数字!");
                return;
            }
            if (!Common.Validate.IsInt(t_TempPrice))
            {
                Common.MsgBox.Show(this, "商品价格应该为数字!");
                return;
            }

            try
            {
                string sqlStr = "update OPENQUERY([LKSV] ,'select * from gameshop_itembase where f_id=" + F_ID + "')  set F_EXCELID=" + t_ExcelID + ", F_SHOPTYPE=" + t_ShopType + ",F_ITEMTYPE=" + t_ItemType + ",F_ITEMPRICE=" + t_TempPrice + " ";
                int rcount = ExecSql(sqlStr);
                if (rcount > 0)
                {
                    GridView1.EditIndex = -1;
                    bind();
                }
            }
            catch
            {
                Common.MsgBox.Show(this, "数据保存失败! (数据可能未更改)");
            }
        }

        //删除
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sqlStr = "DELETE FROM OPENQUERY([LKSV] ,'select * from gameshop_itembase where F_id=" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value) + "') ";
            ExecSql(sqlStr);
            bind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            bind();
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        private int ExecSql(string sql)
        {
            string bigzoneid = DropDownListArea1.SelectedValue.Split(',')[1];
            string zoneid = DropDownListArea2.SelectedValue.Split(',')[1];
            int rcount = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@BigZoneID", SqlDbType.Int),
					new SqlParameter("@ZoneID", SqlDbType.Int),
                    new SqlParameter("@DBType", SqlDbType.Int),
					new SqlParameter("@Query",SqlDbType.NVarChar),
					new SqlParameter("@RCount", SqlDbType.Int),
					};
            parameters[0].Value = bigzoneid;
            parameters[1].Value = zoneid;
            parameters[2].Value = 7;
            parameters[3].Value = sql;
            parameters[4].Direction = ParameterDirection.Output;
            DBHelperDigGameDB.RunProcedure("_EXEC_SQLCustom", parameters, "ds",180);
            rcount = (int)parameters[4].Value;
            return rcount;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string bigzoneid = DropDownListArea1.SelectedValue.Split(',')[1];
            string zoneid = DropDownListArea2.SelectedValue.Split(',')[1];

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
            parameters[2].Value = 7;
            parameters[3].Value = "gameshop_itembase";
            parameters[4].Value = null;
            parameters[5].Value = "";
            parameters[6].Value = 1;
            parameters[7].Value = 10000;
            parameters[8].Value = 0;//0普通分页 1连续ID分页
            parameters[9].Direction = ParameterDirection.Output;


            ds = DBHelperDigGameDB.RunProcedure("_Query_GSLOGDB", parameters, "ds",180);

            System.Collections.Generic.Dictionary<string, string> cols = new System.Collections.Generic.Dictionary<string, string>();

            string filenName = string.Format("基础道具_{0}_{1}", DropDownListArea1.SelectedItem.Text.Trim(), DropDownListArea2.SelectedItem.Text.Trim());
            Common.Util.OutExcel(ds.Tables[0], cols, filenName);
        }

    }
}
