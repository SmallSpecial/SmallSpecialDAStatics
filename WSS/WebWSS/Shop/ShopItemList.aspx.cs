using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;

namespace WebWSS.Shop
{
    public partial class ShopItemList : Admin_Page
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
                ViewState["shopType"] = 0;
                ViewState["itemType"] = -1;
                BindDdl1();
                DropDownListArea1_SelectedIndexChanged(null, null);
            }

        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            try
            {
                int shopType = Convert.ToInt32(ViewState["shopType"]);
                int itemType = Convert.ToInt32(ViewState["itemType"]);
                string sqlwhere = " where 1=1 ";
                sqlwhere += " and F_SHOPTYPE=" + shopType + "";
                if (itemType == -1)
                {
                    sqlwhere += " and F_ISPROMOTIONS=1";
                }
                else if (itemType == -2)
                {
                    sqlwhere += " and F_ISHOTSALE=1";
                }
                else
                {
                    sqlwhere += " and F_ITEMTYPE=" + itemType + "";
                }
                sqlwhere += " order by F_ISHIDENITEM ASC";

                if (itemType == -1)
                {
                    sqlwhere += ",F_PROMOTIONPOS ASC";
                }
                else if (itemType == -2)
                {
                    sqlwhere += ",F_HOTPOS asc";
                }
                else
                {
                    sqlwhere += ",F_POSITION asc";
                }

                switch (itemType)
                {
                    case -1:
                    case -2:
                        DataList1.RepeatColumns = 2;
                        lblPageSize.Text = "6";
                        break;
                    case 3:
                        DataList1.RepeatColumns = 3;
                        lblPageSize.Text = "9";
                        break;
                    default:
                        DataList1.RepeatColumns = 4;
                        lblPageSize.Text = "12";
                        break;
                }

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
                parameters[3].Value = "gameshopgoodstable";
                parameters[4].Value = null;
                parameters[5].Value = sqlwhere;
                parameters[6].Value = lblPageIndex.Text;
                parameters[7].Value = lblPageSize.Text;
                parameters[8].Value = lblPageType.Text;//0普通分页 1连续ID分页
                parameters[9].Direction = ParameterDirection.Output;


                ds = DBHelperDigGameDB.RunProcedure("_Query_GSLOGDB", parameters, "ds", 180);
                DataView myView = ds.Tables[0].DefaultView;
                pcount = (int)parameters[9].Value;

                if (myView.Count == 0)
                {
                    DataList1.Visible = false;
                    lblerro.Visible = true;
                    divPage.Visible = false;

                }
                else
                {
                    DataList1.Visible = true;
                    divPage.Visible = true;
                    lblerro.Visible = false;

                    lblPageCount.Text = (pcount % Convert.ToInt32(lblPageSize.Text) == 0 ? pcount / Convert.ToInt32(lblPageSize.Text) : pcount / Convert.ToInt32(lblPageSize.Text) + 1).ToString();
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

                    DataList1.DataSource = myView;
                    DataList1.DataBind();
                }

            }
            catch (System.Exception ex)
            {
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
                lblinfo.Text = ex.Message;
            }

        }

        public string TransEn(string value)
        {
            if (lblEncoding.Visible)
            {
                return System.Web.HttpUtility.UrlEncode(value, System.Text.Encoding.Default);
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
                return System.Web.HttpUtility.UrlDecode(value, System.Text.Encoding.Default);
            }
            else
            {
                return value;
            }
        }


        //字符集转换   
        public string Str2Asc(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.Default;
                gb2312 = System.Text.Encoding.ASCII;
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




        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListArea2.Items.Clear();
            BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);
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
        protected void DropDownListArea2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPageIndex.Text = "1";
            bind();
        }
        /// <summary>
        /// 商城类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShopType_Click(object sender, EventArgs e)
        {
            try
            {
                btnShopTypeJB.Enabled = true;
                btnShopTypeJP.Enabled = true;
                btnShopTypeJB.CssClass = "buttonbl";
                btnShopTypeJP.CssClass = "buttonbl";

                Button btn = (Button)sender;
                btn.Enabled = false;
                btn.CssClass = "buttonblo";

                switch (btn.ID)
                {
                    case "btnShopTypeJB":
                        ViewState["shopType"] = 0;
                        break;
                    case "btnShopTypeJP":
                        ViewState["shopType"] = 1;
                        break;
                    default:
                        ViewState["shopType"] = 0;
                        break;
                }
                lblPageIndex.Text = "1";
                bind();
            }
            catch (System.Exception ex)
            {
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }
        }
        /// <summary>
        /// 商品类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnItemType_Click(object sender, EventArgs e)
        {
            try
            {
                btnItemType00.Enabled = true;
                btnItemType01.Enabled = true;
                btnItemType0.Enabled = true;
                btnItemType1.Enabled = true;
                btnItemType2.Enabled = true;
                btnItemType3.Enabled = true;
                btnItemType4.Enabled = true;
                btnItemType5.Enabled = true;
                btnItemType00.CssClass = "buttonbl";
                btnItemType01.CssClass = "buttonbl";
                btnItemType0.CssClass = "buttonbl";
                btnItemType1.CssClass = "buttonbl";
                btnItemType2.CssClass = "buttonbl";
                btnItemType3.CssClass = "buttonbl";
                btnItemType4.CssClass = "buttonbl";
                btnItemType5.CssClass = "buttonbl";

                Button btn = (Button)sender;
                btn.Enabled = false;
                btn.CssClass = "buttonblo";

                switch (btn.ID)
                {
                    case "btnItemType00":
                        ViewState["itemType"] = -1;
                        break;
                    case "btnItemType01":
                        ViewState["itemType"] = -2;
                        break;
                    case "btnItemType0":
                        ViewState["itemType"] = 0;
                        break;
                    case "btnItemType1":
                        ViewState["itemType"] = 1;
                        break;
                    case "btnItemType2":
                        ViewState["itemType"] = 2;
                        break;
                    case "btnItemType3":
                        ViewState["itemType"] = 3;
                        break;
                    case "btnItemType4":
                        ViewState["itemType"] = 4;
                        break;
                    case "btnItemType5":
                        ViewState["itemType"] = 5;
                        break;
                    default:
                        ViewState["itemType"] = 0;
                        break;
                }
                lblPageIndex.Text = "1";
                bind();
            }
            catch (System.Exception ex)
            {
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }
        }

        protected void DataList1_EditCommand(object source, DataListCommandEventArgs e)
        {
            DataList1.EditItemIndex = e.Item.ItemIndex;
            bind();
            // DataList1.DataBind();
        }

        protected void DataList1_CancelCommand(object source, DataListCommandEventArgs e)
        {
            DataList1.EditItemIndex = -1;
            bind();
        }

        protected void DataList1_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex].ToString());
                System.Text.StringBuilder sqlStr = new System.Text.StringBuilder();
                sqlStr.AppendFormat("DELETE FROM OPENQUERY([LKSV] ,'select * from gameshopgoodstable where F_id={0} ')", id);
                ExecSql(sqlStr.ToString());
            }
            catch (System.Exception ex)
            {
                Common.MsgBox.Show(this, ex.Message);
            }
        }

        protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            //MyPhotoList.BLL.User bll = new MyPhotoList.BLL.User(); MyPhotoList.Model.User model = new MyPhotoList.Model.User(); model.UId = Convert.ToInt32(e.CommandArgument); TextBox txt1 = e.Item.FindControl("txtname") as TextBox; TextBox txt2 = e.Item.FindControl("txtpwd") as TextBox; if (txt1 != null) { model.UName = txt1.Text; } if (txt2 != null) { model.UPwd = txt2.Text; } if (bll.Update(model)) { DataList1.EditItemIndex = -1; DataList1.DataBind(); } else { Response.Write("更新失败"); }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            int shopType = Convert.ToInt32(ViewState["shopType"]);
            int itemType = Convert.ToInt32(ViewState["itemType"]);
            int id = Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex].ToString());
            if (e.CommandName == "PEdit")
            {
                string url = string.Format("ShopItemEdit.aspx?id={0}&bigzone={1}&zone={2}", id, DropDownListArea1.SelectedValue, DropDownListArea2.SelectedValue);
                Response.Redirect(url, true);
            }
            else if (e.CommandName == "Up")
            {
                System.Text.StringBuilder sqlStr = new System.Text.StringBuilder();
                sqlStr.AppendFormat("update OPENQUERY([LKSV] ,'select * from gameshopgoodstable where F_id={0} ') set ", id);
                if (itemType == -1)
                {
                    sqlStr.Append(" F_PROMOTIONPOS=F_PROMOTIONPOS-1");
                }
                else if (itemType == -2)
                {
                    sqlStr.Append(" F_HOTPOS=F_HOTPOS-1");
                }
                else
                {
                    sqlStr.Append(" F_POSITION=F_POSITION-1");
                }

                ExecSql(sqlStr.ToString());
            }
            else if (e.CommandName == "Down")
            {
                System.Text.StringBuilder sqlStr = new System.Text.StringBuilder();
                sqlStr.AppendFormat("update OPENQUERY([LKSV] ,'select * from gameshopgoodstable where F_id={0} ') set ", id);
                if (itemType == -1)
                {
                    sqlStr.Append(" F_PROMOTIONPOS=F_PROMOTIONPOS+1");
                }
                else if (itemType == -2)
                {
                    sqlStr.Append(" F_HOTPOS=F_HOTPOS+1");
                }
                else
                {
                    sqlStr.Append(" F_POSITION=F_POSITION+1");
                }
                ExecSql(sqlStr.ToString());
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        private void ExecSql(string sql)
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
            DBHelperDigGameDB.RunProcedure("_EXEC_SQLCustom", parameters, "ds", 180);
            rcount = (int)parameters[4].Value;
            if (rcount > 0)
            {
                bind();
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url = string.Format("ShopItemAdd.aspx?bigzone={0}&zone={1}", DropDownListArea1.SelectedValue, DropDownListArea2.SelectedValue);
            Response.Redirect(url, true);
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
            parameters[3].Value = "gameshopgoodstable";
            parameters[4].Value = null;
            parameters[5].Value = "";
            parameters[6].Value = 1;
            parameters[7].Value = 10000;
            parameters[8].Value = 0;//0普通分页 1连续ID分页
            parameters[9].Direction = ParameterDirection.Output;


            ds = DBHelperDigGameDB.RunProcedure("_Query_GSLOGDB", parameters, "ds", 180);

            System.Collections.Generic.Dictionary<string, string> cols = new System.Collections.Generic.Dictionary<string, string>();
            //cols.Add("F_ID", "编号");
            //cols.Add("F_EXCELID", "EXCEL编号");
            //cols.Add("F_SHOPTYPE", "商城类型");
            //cols.Add("F_ITEMTYPE", "");
            //cols.Add("F_ITEMNUMBER", "");
            //cols.Add("F_OLDPRICE", "");
            //cols.Add("F_NEWPRICE", "");
            //cols.Add("F_VIPRICE", "");
            //cols.Add("F_POSITION", "");
            //cols.Add("F_ISNEW", "");
            //cols.Add("F_NEWPOS", "");
            //cols.Add("F_ISHOTSALE", "");
            //cols.Add("F_HOTPOS", "");
            //cols.Add("F_ISPROMOTIONS", "");
            //cols.Add("F_PROMOTIONPOS", "");
            //cols.Add("F_USEGIFTS", "");
            //cols.Add("F_ISTIMESALE", "");
            //cols.Add("F_LimitTimePos", "");
            //cols.Add("F_TimeStart", "");
            //cols.Add("F_TimeEnd", "");
            //cols.Add("F_ISHIDENITEM", "");
            //cols.Add("F_GiftID_0", "");
            //cols.Add("F_GiftNUM_0", "");
            //cols.Add("F_GiftID_1", "");
            //cols.Add("F_GiftNUM_1", "");
            //cols.Add("F_GiftID_2", "");
            //cols.Add("F_GiftNUM_2", "");
            //cols.Add("F_GiftID_3", "");
            //cols.Add("F_GiftNUM_3", "");
            //cols.Add("F_GiftID_4", "");
            //cols.Add("F_GiftNUM_4", "");
            //cols.Add("F_ItemInfo", "");
            string filenName = string.Format("商城商品_{0}_{1}", DropDownListArea1.SelectedItem.Text.Trim(), DropDownListArea2.SelectedItem.Text.Trim());
            Common.Util.OutExcel(ds.Tables[0], cols, filenName);
        }

    }
}
