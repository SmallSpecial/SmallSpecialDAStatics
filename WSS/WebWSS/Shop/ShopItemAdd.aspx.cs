using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Data.SqlClient;


namespace WebWSS.Shop
{
    public partial class ShopItemAdd : Admin_Page
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

            if (Request.QueryString["bigzone"] != null && Request.QueryString["zone"] != null)
            {
                if (!IsPostBack)
                {
                    BindDdl1();
                    DropDownListArea1.SelectedValue = Request.QueryString["bigzone"];
                    DropDownListArea1_SelectedIndexChanged(null, null);
                    DropDownListArea2.SelectedValue = Request.QueryString["zone"];
                }
                SetFormValue();
            }

        }

        /// <summary>
        /// 设置FORM值
        /// </summary>
        private void SetFormValue()
        {
            if (Request.Form["tboxValue"] != null && Request.Form["tboxValue"].Length > 0)
            {
                string[] values = Request.Form["tboxValue"].Split('|');
                tboxExcelID.Text = values[0];
                tboxItemName.Text = values[1];
                ddlShopType.SelectedValue = values[2];
                ddlItemType.SelectedValue = values[3];
                lblTempPrice.Text = values[4];

                if (Common.Validate.IsInt(tboxNum.Text)&&Common.Validate.IsInt(lblTempPrice.Text))
                {
                    tboxOldPrice.Text = (Convert.ToInt32(lblTempPrice.Text) * Convert.ToInt32(tboxNum.Text)).ToString();
                }
                
            }
            if (Request.Form["tboxGift1ID"] != null && Request.Form["tboxGift1ID"].Length > 0)
            {
                string[] values = Request.Form["tboxGift1ID"].Split('|');
                lblGift1.Text = values[0];
            }
            if (Request.Form["tboxGift2ID"] != null && Request.Form["tboxGift2ID"].Length > 0)
            {
                string[] values = Request.Form["tboxGift2ID"].Split('|');
                lblGift2.Text = values[0];
            }
            if (Request.Form["tboxGift3ID"] != null && Request.Form["tboxGift3ID"].Length > 0)
            {
                string[] values = Request.Form["tboxGift3ID"].Split('|');
                lblGift3.Text = values[0];
            }
            if (Request.Form["tboxGift4ID"] != null && Request.Form["tboxGift4ID"].Length > 0)
            {
                string[] values = Request.Form["tboxGift4ID"].Split('|');
                lblGift4.Text = values[0];
            }
            if (Request.Form["tboxGift5ID"] != null && Request.Form["tboxGift5ID"].Length > 0)
            {
                string[] values = Request.Form["tboxGift5ID"].Split('|');
                lblGift5.Text = values[0];
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
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShopItemList.aspx");
        }

        /// <summary>
        /// 提交请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCommit_Click(object sender, EventArgs e)
        {
            try
            {
                string t_EXCELID = tboxExcelID.Text.Trim();
                string t_SHOPTYPE = ddlShopType.SelectedValue;
                string t_ITEMTYPE = ddlItemType.SelectedValue;
                string t_ITEMNUMBER = tboxNum.Text.Trim();
                string t_OLDPRICE = tboxOldPrice.Text.Trim();
                string t_NEWPRICE = tboxNowPrice.Text.Trim();
                string t_VIPRICE = tboxVIPPrice.Text.Trim();
                string t_POSITION = tboxPos.Text.Trim();
                string t_ISNEW = cbxIsNew.Checked ? "1" : "0";
                string t_NEWPOS = tboxNewPos.Text.Trim();
                string t_ISHOTSALE = cbxIsHot.Checked ? "1" : "0";
                string t_HOTPOS = tboxHotPos.Text.Trim();
                string t_ISPROMOTIONS = cbxIsOffSale.Checked ? "1" : "0";
                string t_PROMOTIONPOS = tboxOffSalePos.Text.Trim();
                string t_USEGIFTS = cbxGift.Checked ? "1" : "0";
                string t_ISTIMESALE = cbxIsLimitTimeS.Checked ? "1" : "0";
                string t_LimitTimePos = tboxLimitTimePos.Text.Trim();
                string t_TimeStart = tboxLTBegin.Text.Trim();
                string t_TimeEnd = tboxLTEnd.Text.Trim();
                string t_ISHIDENITEM = cbxIsHidden.Checked ? "1" : "0";
                string t_GiftID_0 = lblGift1.Text.Trim();
                string t_GiftNUM_0 = tboxGift1Num.Text.Trim();
                string t_GiftID_1 = lblGift2.Text.Trim();
                string t_GiftNUM_1 = tboxGift2Num.Text.Trim();
                string t_GiftID_2 = lblGift3.Text.Trim();
                string t_GiftNUM_2 = tboxGift3Num.Text.Trim();
                string t_GiftID_3 = lblGift4.Text.Trim();
                string t_GiftNUM_3 = tboxGift4Num.Text.Trim();
                string t_GiftID_4 = lblGift5.Text.Trim();
                string t_GiftNUM_4 = tboxGift5Num.Text.Trim();
                string t_ItemInfo = tboxNote.Text.Trim() ;

                lblTips.Text = "";
                spanTips.Visible = true;
                if (!Common.Validate.IsInt(t_EXCELID))
                {
                    lblTips.Text = "请先选择您要添加的商品!!!";
                    return;
                }
                if (!Common.Validate.IsInt(t_ITEMNUMBER))
                {
                    lblTips.Text = "商品数量填写不正确!!!";
                    return;
                }
                if (!Common.Validate.IsInt(t_OLDPRICE))
                {
                    lblTips.Text = "商品原价填写不正确!!!";
                    return;
                }
                if (!Common.Validate.IsInt(t_NEWPRICE))
                {
                    lblTips.Text = "商品现价填写不正确!!!";
                    return;
                }
                if (!Common.Validate.IsInt(t_VIPRICE))
                {
                    lblTips.Text = "商品VIP价填写不正确!!!";
                    return;
                }
                int oldprice = Convert.ToInt32(t_OLDPRICE);
                int nowprice = Convert.ToInt32(t_NEWPRICE);
                int vipprice = Convert.ToInt32(t_VIPRICE);
                int tempprice = Convert.ToInt32(lblTempPrice.Text.Trim());
                if (oldprice < nowprice || oldprice < vipprice || nowprice < vipprice)
                {
                    lblTips.Text = "商品价格应满足 原价>=现价>=VIP价 !!!";
                    return;
                }
                //if (oldprice * 0.1 > vipprice)
                //{
                //    lblTips.Text = "商品价格应满足 最低价不小于1折 !!!";
                //    return;
                //}

                //if (tempprice * 2 < oldprice || tempprice * 0.1 > oldprice)
                //{
                //    lblTips.Text = "商品价格应满足 参考价*0.1<=原价<=参考价*2 !!!";
                //    return;
                //}
                //if (tempprice * 0.1 > nowprice)
                //{
                //    lblTips.Text = "商品价格应满足 参考价*0.1<=现价 !!!";
                //    return;
                //}
                //if (tempprice * 0.1 > vipprice)
                //{
                //    lblTips.Text = "商品价格应满足 参考价*0.1<=VIP价 !!!";
                //    return;
                //}
                if (!Common.Validate.IsInt(t_POSITION))
                {
                    lblTips.Text = "商品顺序填写不正确!!!";
                    return;
                }
                if (t_ISNEW == "1" && !Common.Validate.IsInt(t_NEWPOS))
                {
                    lblTips.Text = "新品顺序填写不正确!!!";
                    return;
                }
                if (t_ISHOTSALE == "1" && !Common.Validate.IsInt(t_HOTPOS))
                {
                    lblTips.Text = "热卖顺序填写不正确!!!";
                    return;
                }
                if (t_PROMOTIONPOS == "1" && !Common.Validate.IsInt(t_PROMOTIONPOS))
                {
                    lblTips.Text = "促销顺序填写不正确!!!";
                    return;
                }
                if (t_ISTIMESALE == "1")
                {
                    if (!Common.Validate.IsInt(t_LimitTimePos))
                    {
                        lblTips.Text = "限时特卖顺序填写不正确!!!";
                        return;
                    }
                    else if (!Common.Validate.IsDateTime(t_TimeStart))
                    {
                        lblTips.Text = "限时特卖开始时间填写不正确!!!";
                        return;
                    }
                    else if (!Common.Validate.IsDateTime(t_TimeEnd))
                    {
                        lblTips.Text = "限时特卖结束时间填写不正确!!!";
                        return;
                    }
                    else if (Convert.ToDateTime(t_TimeStart) > Convert.ToDateTime(t_TimeEnd))
                    {
                        lblTips.Text = "限时特卖开始时间不能大于结束时间!!!";
                        return;
                    }
                }
                if (t_USEGIFTS == "1")
                {
                    if (!Common.Validate.IsInt(t_GiftNUM_0))
                    {
                        lblTips.Text = "赠品1数量填写不正确!!!";
                        return;
                    }
                    else if (!Common.Validate.IsInt(t_GiftNUM_1))
                    {
                        lblTips.Text = "赠品2数量填写不正确!!!";
                        return;
                    }
                    else if (!Common.Validate.IsInt(t_GiftNUM_2))
                    {
                        lblTips.Text = "赠品3数量填写不正确!!!";
                        return;
                    }
                    else if (!Common.Validate.IsInt(t_GiftNUM_3))
                    {
                        lblTips.Text = "赠品4数量填写不正确!!!";
                        return;
                    }
                    else if (!Common.Validate.IsInt(t_GiftNUM_4))
                    {
                        lblTips.Text = "赠品5数量填写不正确!!!";
                        return;
                    }
                }
                else
                {
                    t_GiftID_0 = "-1";
                    t_GiftNUM_0 = "0";
                    t_GiftID_1 = "-1";
                    t_GiftNUM_1 = "0";
                    t_GiftID_2 = "-1";
                    t_GiftNUM_2 = "0";
                    t_GiftID_3 = "-1";
                    t_GiftNUM_3 = "0";
                    t_GiftID_4 = "-1";
                    t_GiftNUM_4 = "0";
                }
                if (t_ItemInfo.Length > 31)
                {
                    lblTips.Text = "商品描述只允许31字以内!!!";
                    return;
                }

                Decimal zhuPinPrice = Convert.ToDecimal(GetExcelPrice(t_EXCELID) * Convert.ToInt32(t_ITEMNUMBER));
                Decimal zengPinPrice = (GetExcelPrice(t_GiftID_0) * Convert.ToInt32(t_GiftNUM_0)) + (GetExcelPrice(t_GiftID_1) * Convert.ToInt32(t_GiftNUM_1)) + (GetExcelPrice(t_GiftID_2) * Convert.ToInt32(t_GiftNUM_2)) + (GetExcelPrice(t_GiftID_3) * Convert.ToInt32(t_GiftNUM_3)) + (GetExcelPrice(t_GiftID_4) * Convert.ToInt32(t_GiftNUM_4));
                if (zengPinPrice > zhuPinPrice * 5)
                {
                    lblTips.Text = "赠品总价值不能大于商品的5倍!";
                    return;
                }

                if (Convert.ToDecimal(t_NEWPRICE) < (zhuPinPrice * Convert.ToDecimal(0.2)))
                {
                    lblTips.Text = "商品的现价不能低于实际价格的2折!";
                    return;
                }
                spanTips.Visible = false;

                //开始新增
                System.Text.StringBuilder sqlStr = new System.Text.StringBuilder();
                sqlStr.Append("insert into OPENQUERY ([LKSV] ,'select * from gameshopgoodstable where F_id=0 ' )  (F_EXCELID,F_SHOPTYPE,F_ITEMTYPE,F_ITEMNUMBER,F_OLDPRICE,F_NEWPRICE,F_VIPRICE,F_POSITION,F_ISNEW,F_NEWPOS,F_ISHOTSALE,F_HOTPOS,F_ISPROMOTIONS,F_PROMOTIONPOS,F_USEGIFTS,F_ISTIMESALE,F_LimitTimePos,F_TimeStart,F_TimeEnd,F_ISHIDENITEM,F_GiftID_0,F_GiftNUM_0,F_GiftID_1,F_GiftNUM_1,F_GiftID_2,F_GiftNUM_2,F_GiftID_3,F_GiftNUM_3,F_GiftID_4,F_GiftNUM_4,F_ItemInfo) values(");
               // sqlStr.Append("480000,0,4,1,8,8,5,-1,0,-1,0,-1,0,-1,0,0,-1,'2013-03-06 00:00:00','2013-03-06 00:00:00',0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'-1')");

                sqlStr.Append(t_EXCELID+",");
                sqlStr.Append(t_SHOPTYPE+",");
                sqlStr.Append(t_ITEMTYPE+",");
                sqlStr.Append(t_ITEMNUMBER+",");
                sqlStr.Append(t_OLDPRICE+",");
                sqlStr.Append(t_NEWPRICE+",");
                sqlStr.Append(t_VIPRICE+",");
                sqlStr.Append(t_POSITION+",");
                sqlStr.Append(t_ISNEW+",");
                sqlStr.Append(t_NEWPOS+",");
                sqlStr.Append(t_ISHOTSALE+",");
                sqlStr.Append(t_HOTPOS+",");
                sqlStr.Append(t_ISPROMOTIONS+",");
                sqlStr.Append(t_PROMOTIONPOS+",");
                sqlStr.Append(t_USEGIFTS+",");
                sqlStr.Append(t_ISTIMESALE+",");
                sqlStr.Append(t_LimitTimePos+",");
                sqlStr.Append("'"+t_TimeStart+"',");
                sqlStr.Append("'" + t_TimeEnd + "',");
                sqlStr.Append(t_ISHIDENITEM+",");
                sqlStr.Append(t_GiftID_0+",");
                sqlStr.Append(t_GiftNUM_0+",");
                sqlStr.Append(t_GiftID_1+",");
                sqlStr.Append(t_GiftNUM_1+",");
                sqlStr.Append(t_GiftID_2+",");
                sqlStr.Append(t_GiftNUM_2+",");
                sqlStr.Append(t_GiftID_3+",");
                sqlStr.Append(t_GiftNUM_3+",");
                sqlStr.Append(t_GiftID_4+",");
                sqlStr.Append(t_GiftNUM_4+",");
                sqlStr.Append("'" + TransEn(t_ItemInfo) + "')");
                // sqlStr.Append("update OPENQUERY ([LKSV_6_gspara_db_1_1] ,'select * from gameshopgoodstable   where F_id=5 ' ) set F_SHOPTYPE=1");

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
                parameters[3].Value = sqlStr.ToString();
                parameters[4].Direction = ParameterDirection.Output;


                DBHelperDigGameDB.RunProcedure("_EXEC_SQLCustom", parameters, "ds",180);
                rcount = (int)parameters[4].Value;

                if (rcount > 0)
                {
                    Common.MsgBox.ShowAndRedirect(this, "商品添加成功!", "ShopItemList.aspx");
                }
                else
                {
                    Common.MsgBox.Show(this, "商品添加失败!");
                }

            }
            catch (System.Exception ex)
            {
                Common.MsgBox.Show(this, "商品添加失败"+ ex.Message);
            }
        }


        /// <summary>
        /// 查询商品价格
        /// </summary>
        public decimal GetExcelPrice(string excelID)
        {

            string sqlwhere = " where 1=1 ";

            sqlwhere += @" and F_EXCELID =" + excelID.Replace("'", "") + "";

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
                parameters[6].Value = 1;
                parameters[7].Value = 1000;
                parameters[8].Value = 0;//0普通分页 1连续ID分页
                parameters[9].Direction = ParameterDirection.Output;


                ds = DBHelperDigGameDB.RunProcedure("_Query_GSLOGDB", parameters, "ds", 180);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    return Convert.ToDecimal(ds.Tables[0].Rows[0]["F_ITEMPRICE"]);
                else
                    return 0;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShopItemList.aspx");
        }

        protected void cbxGift_CheckedChanged(object sender, EventArgs e)
        {
            pnlGift.Visible = cbxGift.Checked;
        }

        protected void cbxIsNew_CheckedChanged(object sender, EventArgs e)
        {
            tboxNewPos.Enabled = cbxIsNew.Checked;
        }

        protected void cbxIsHot_CheckedChanged(object sender, EventArgs e)
        {
            tboxHotPos.Enabled = cbxIsHot.Checked;
        }

        protected void cbxIsOffSale_CheckedChanged(object sender, EventArgs e)
        {
            tboxOffSalePos.Enabled = cbxIsOffSale.Checked;
        }

        protected void cbxIsLimitTimeS_CheckedChanged(object sender, EventArgs e)
        {
            tboxLimitTimePos.Enabled = cbxIsLimitTimeS.Checked;
            tboxLTBegin.Enabled = cbxIsLimitTimeS.Checked;
            tboxLTEnd.Enabled = cbxIsLimitTimeS.Checked;
        }

        protected void tboxNum_TextChanged(object sender, EventArgs e)
        {
            if (Common.Validate.IsInt(tboxNum.Text) && Common.Validate.IsInt(lblTempPrice.Text))
            {
                tboxOldPrice.Text = (Convert.ToInt32(lblTempPrice.Text) * Convert.ToInt32(tboxNum.Text)).ToString();
            }
        }


    }
}
