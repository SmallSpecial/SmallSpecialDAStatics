using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Shop
{
    public partial class ShopItemImport : Admin_Page, ICallbackEventHandler
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
            }
            InitPage();
        }
        private void InitPage()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, F_ValueGame FROM T_GameConfig WHERE (F_ParentID = 1000) AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Label lbl = new Label();
                        lbl.Text = "&nbsp;";
                        divZoneList.Controls.Add(lbl);

                        CheckBox cb = new CheckBox();
                        cb.ID = string.Format("cbZone{0}_{1}", dr["F_ID"], dr["F_ValueGame"]);
                        cb.Text = dr["F_Name"].ToString();
                        cb.Font.Bold = true;
                        cb.AutoPostBack = true;
                        cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
                        divZoneList.Controls.Add(cb);

                        string sqlz = @"SELECT F_ID, F_Name, F_Value,F_ValueGame FROM T_GameConfig WHERE (F_ParentID = " + dr["F_ID"].ToString() + ") AND (F_IsUsed = 1)";
                        ds = spg.Query(sqlz);

                        CheckBoxList cbl = new CheckBoxList();
                        cbl.ID = string.Format("cblZone{0}", cb.ID);
                        cbl.RepeatColumns = 5;
                        cbl.RepeatDirection = RepeatDirection.Horizontal;
                        cbl.DataSource = ds.Tables[0];
                        cbl.DataTextField = "F_Name";
                        cbl.DataValueField = "F_ValueGame";
                        cbl.DataBind();
                        cbl.AutoPostBack = true;
                        cbl.SelectedIndexChanged += new EventHandler(cbl_SelectedIndexChanged);
                        divZoneList.Controls.Add(cbl);

                    }
                }
                else
                {
                    LblMessagePub.Text = "未获取到战区列表";
                }

            }
            catch (System.Exception ex)
            {
                LblMessagePub.Text = ex.Message;
            }
        }

        void cbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList cbl = (CheckBoxList)sender;
            CheckBox cb = (CheckBox)divZoneList.FindControl(cbl.ID.Replace("cblZone", ""));
            cb.Checked = cbl.SelectedIndex != -1;
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            CheckBoxList cbl = (CheckBoxList)divZoneList.FindControl(string.Format("cblZone{0}", cb.ID));
            foreach (ListItem item in cbl.Items)
            {
                item.Selected = cb.Checked;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            try
            {
                DataView myView = new DataView();
                if (ViewState["dsImport"] != null)
                {
                    myView = ((DataSet)ViewState["dsImport"]).Tables[0].DefaultView;
                }

                if (ViewState["dsImport"] == null || myView.Count == 0)
                {
                    GridView1.Visible = false;
                    lblerro.Visible = true;
                }
                else
                {
                    GridView1.Visible = true;
                    lblerro.Visible = false;

                    GridView1.DataSource = myView;
                    GridView1.DataBind();
                }

            }
            catch (System.Exception ex)
            {
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
                lblinfo.Text = ex.Message;
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

                string sql = @"SELECT top 1 F_Name  FROM T_BaseGameName WHERE (F_ExcelID = " + value + ") ";
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }


        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        private bool ExecSql(string sql, string bigzoneid, string zoneid)
        {
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
            return true;
        }


        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData(string fileName)
        {
            try
            {
                if (fileName == string.Empty)
                {
                    throw new ArgumentNullException(App_GlobalResources.Language.Tip_UploadExcelFailure);
                }

                string fpath = Server.MapPath("~/UpFiles/").ToLower() + "";
                string fname = fileName.ToLower().Replace(fpath, "");

                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source='";
                oleDBConnString += fpath;
                oleDBConnString += "';Extended Properties='text;HDR=Yes;FMT=Delimited'";

                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                DataTable m_tableName = new DataTable();
                DataSet ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();

                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + fname + "] where len(F_EXCELID)>0";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();
                DataColumn col = new DataColumn("F_State");
                ds.Tables[0].Columns.Add(col);
                ViewState["dsImport"] = ds;
                bind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 上传Excel文件
        /// </summary>
        /// <param name="inputfile">上传的控件名</param>
        /// <returns></returns>
        private string UpLoadXls(System.Web.UI.HtmlControls.HtmlInputFile inputfile)
        {
            string orifilename = string.Empty;
            string uploadfilepath = string.Empty;
            string modifyfilename = string.Empty;
            string fileExtend = "";//文件扩展名
            int fileSize = 0;//文件大小
            try
            {
                if (inputfile.Value != string.Empty)
                {
                    //得到文件的大小
                    fileSize = inputfile.PostedFile.ContentLength;
                    if (fileSize == 0)
                    {
                        throw new Exception(App_GlobalResources.Language.Tip_UploadExcelIsEmpty);
                    }
                    //得到扩展名
                    fileExtend = inputfile.Value.Substring(inputfile.Value.LastIndexOf(".") + 1);
                    if (fileExtend.ToLower() != "xls" && fileExtend.ToLower() != "csv" && fileExtend.ToLower() != "xlsx")
                    {
                        throw new Exception(App_GlobalResources.Language.Tip_LimitImportOnlyExcel);
                    }
                    //路径
                    uploadfilepath = Server.MapPath("~/UpFiles");
                    //新文件名
                    modifyfilename = System.Guid.NewGuid().ToString();
                    modifyfilename += "." + inputfile.Value.Substring(inputfile.Value.LastIndexOf(".") + 1);
                    //判断是否有该目录
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadfilepath);
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    orifilename = uploadfilepath + "\\" + modifyfilename;
                    //如果存在,删除文件
                    if (File.Exists(orifilename))
                    {
                        File.Delete(orifilename);
                    }
                    // 上传文件
                    inputfile.PostedFile.SaveAs(orifilename);
                }
                else
                {
                    throw new Exception(App_GlobalResources.Language.Tip_PleaseSelectImportExcel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orifilename;
        }


        /// <summary>
        /// 插入数据到数据库
        /// </summary>
        public bool AddItem(DataRow dr, string bigzoneid, string zoneid)
        {
            try
            {

                string t_EXCELID = dr["F_ExcelID"].ToString().Trim();
                string t_SHOPTYPE = dr["F_SHOPTYPE"].ToString().Trim();
                string t_ITEMTYPE = dr["F_ITEMTYPE"].ToString().Trim();
                string t_ITEMNUMBER = dr["F_ITEMNUMBER"].ToString().Trim();
                string t_OLDPRICE = dr["F_OldPrice"].ToString().Trim();
                string t_NEWPRICE = dr["F_NEWPRICE"].ToString().Trim();
                string t_VIPRICE = dr["F_VIPRICE"].ToString().Trim();
                string t_POSITION = dr["F_POSITION"].ToString().Trim();
                string t_ISNEW = dr["F_ISNEW"].ToString().Trim();
                string t_NEWPOS = dr["F_NewPos"].ToString().Trim();
                string t_ISHOTSALE = dr["F_ISHOTSALE"].ToString().Trim();
                string t_HOTPOS = dr["F_HotPos"].ToString().Trim();
                string t_ISPROMOTIONS = dr["F_ISPROMOTIONS"].ToString().Trim();
                string t_PROMOTIONPOS = dr["F_PROMOTIONPOS"].ToString().Trim();
                string t_USEGIFTS = dr["F_USEGIFTS"].ToString().Trim();
                string t_ISTIMESALE = dr["F_ISTIMESALE"].ToString().Trim();
                string t_LimitTimePos = dr["F_LimitTimePos"].ToString().Trim();
                string t_TimeStart = dr["F_TimeStart"].ToString().Trim();
                string t_TimeEnd = dr["F_TimeEnd"].ToString().Trim();
                string t_ISHIDENITEM = dr["F_ISHIDENITEM"].ToString().Trim();
                string t_GiftID_0 = dr["F_GiftID_0"].ToString().Trim();
                string t_GiftNUM_0 = dr["F_GiftNUM_0"].ToString().Trim();
                string t_GiftID_1 = dr["F_GiftID_1"].ToString().Trim();
                string t_GiftNUM_1 = dr["F_GiftNUM_1"].ToString().Trim();
                string t_GiftID_2 = dr["F_GiftID_2"].ToString().Trim();
                string t_GiftNUM_2 = dr["F_GiftNUM_2"].ToString().Trim();
                string t_GiftID_3 = dr["F_GiftID_3"].ToString().Trim();
                string t_GiftNUM_3 = dr["F_GiftNUM_3"].ToString().Trim();
                string t_GiftID_4 = dr["F_GiftID_4"].ToString().Trim();
                string t_GiftNUM_4 = dr["F_GiftNUM_4"].ToString().Trim();
                string t_ItemInfo = dr["F_ItemInfo"].ToString().Trim();

                string strTips = "";
                if (!Common.Validate.IsInt(t_EXCELID))
                {
                    strTips = "EXCEL编号不正确!!!";

                }
                else if (!Common.Validate.IsInt(t_ITEMNUMBER))
                {
                    strTips = "商品数量填写不正确!!!";

                }
                else if (!Common.Validate.IsInt(t_OLDPRICE))
                {
                    strTips = "商品原价填写不正确!!!";

                }
                else if (!Common.Validate.IsInt(t_NEWPRICE))
                {
                    strTips = "商品现价填写不正确!!!";

                }
                else if (!Common.Validate.IsInt(t_VIPRICE))
                {
                    strTips = "商品VIP价填写不正确!!!";

                }
                else if (Common.Validate.IsInt(t_OLDPRICE) && Common.Validate.IsInt(t_NEWPRICE) && Common.Validate.IsInt(t_VIPRICE))
                {
                    int oldprice = Convert.ToInt32(t_OLDPRICE);
                    int nowprice = Convert.ToInt32(t_NEWPRICE);
                    int vipprice = Convert.ToInt32(t_VIPRICE);
                    int tempprice = vipprice;
                    if (oldprice < nowprice || oldprice < vipprice || nowprice < vipprice)
                    {
                        strTips = "商品价格应满足 原价>现价>VIP价 !!!";

                    }

                    //if (tempprice * 2 < oldprice || tempprice * 0.1 > oldprice)
                    //{
                    //    strTips = "商品价格应满足 参考价*0.1<=原价<=参考价*2 !!!";

                    //}
                    //if (tempprice * 0.1 > nowprice)
                    //{
                    //    strTips = "商品价格应满足 参考价*0.1<=现价 !!!";

                    //}
                    //if (tempprice * 0.1 > vipprice)
                    //{
                    //    strTips = "商品价格应满足 参考价*0.1<=VIP价 !!!";

                    //}
                }

                else if (!Common.Validate.IsInt(t_POSITION))
                {
                    strTips = "商品顺序填写不正确!!!";

                }
                else if (t_ISNEW == "1" && !Common.Validate.IsInt(t_NEWPOS))
                {
                    strTips = "新品顺序填写不正确!!!";

                }
                else if (t_ISHOTSALE == "1" && !Common.Validate.IsInt(t_HOTPOS))
                {
                    strTips = "热卖顺序填写不正确!!!";

                }
                else if (t_PROMOTIONPOS == "1" && !Common.Validate.IsInt(t_PROMOTIONPOS))
                {
                    strTips = "促销顺序填写不正确!!!";

                }
                else if (t_ISTIMESALE == "1")
                {
                    if (!Common.Validate.IsInt(t_LimitTimePos))
                    {
                        strTips = "限时特卖顺序填写不正确!!!";

                    }
                    else if (!Common.Validate.IsDateTime(t_TimeStart))
                    {
                        strTips = "限时特卖开始时间填写不正确!!!";

                    }
                    else if (!Common.Validate.IsDateTime(t_TimeEnd))
                    {
                        strTips = "限时特卖结束时间填写不正确!!!";

                    }
                    else if (Convert.ToDateTime(t_TimeStart) > Convert.ToDateTime(t_TimeEnd))
                    {
                        strTips = "限时特卖开始时间不能大于结束时间!!!";

                    }
                }
                if (t_USEGIFTS == "1")
                {
                    if (!Common.Validate.IsInt(t_GiftNUM_0))
                    {
                        strTips = "赠品1数量填写不正确!!!";

                    }
                    else if (!Common.Validate.IsInt(t_GiftNUM_1))
                    {
                        strTips = "赠品2数量填写不正确!!!";

                    }
                    else if (!Common.Validate.IsInt(t_GiftNUM_2))
                    {
                        strTips = "赠品3数量填写不正确!!!";

                    }
                    else if (!Common.Validate.IsInt(t_GiftNUM_3))
                    {
                        strTips = "赠品4数量填写不正确!!!";

                    }
                    else if (!Common.Validate.IsInt(t_GiftNUM_4))
                    {
                        strTips = "赠品5数量填写不正确!!!";

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
                if (TransDe(t_ItemInfo).Length > 31)
                {
                    strTips = "商品描述只允许31字以内!!!";

                }
                if (strTips.Trim().Length > 0)
                {
                    throw new Exception("EXCEL编号:" + t_EXCELID + App_GlobalResources.Language.CDKey_ErrorOccurred+"  " + strTips);
                }

                //开始新增
                System.Text.StringBuilder sqlStr = new System.Text.StringBuilder();
                sqlStr.Append("insert into OPENQUERY ([LKSV] ,'select * from gameshopgoodstable where F_id=0 ' )  (F_EXCELID,F_SHOPTYPE,F_ITEMTYPE,F_ITEMNUMBER,F_OLDPRICE,F_NEWPRICE,F_VIPRICE,F_POSITION,F_ISNEW,F_NEWPOS,F_ISHOTSALE,F_HOTPOS,F_ISPROMOTIONS,F_PROMOTIONPOS,F_USEGIFTS,F_ISTIMESALE,F_LimitTimePos,F_TimeStart,F_TimeEnd,F_ISHIDENITEM,F_GiftID_0,F_GiftNUM_0,F_GiftID_1,F_GiftNUM_1,F_GiftID_2,F_GiftNUM_2,F_GiftID_3,F_GiftNUM_3,F_GiftID_4,F_GiftNUM_4,F_ItemInfo) values(");
                sqlStr.Append(t_EXCELID + ",");
                sqlStr.Append(t_SHOPTYPE + ",");
                sqlStr.Append(t_ITEMTYPE + ",");
                sqlStr.Append(t_ITEMNUMBER + ",");
                sqlStr.Append(t_OLDPRICE + ",");
                sqlStr.Append(t_NEWPRICE + ",");
                sqlStr.Append(t_VIPRICE + ",");
                sqlStr.Append(t_POSITION + ",");
                sqlStr.Append(t_ISNEW + ",");
                sqlStr.Append(t_NEWPOS + ",");
                sqlStr.Append(t_ISHOTSALE + ",");
                sqlStr.Append(t_HOTPOS + ",");
                sqlStr.Append(t_ISPROMOTIONS + ",");
                sqlStr.Append(t_PROMOTIONPOS + ",");
                sqlStr.Append(t_USEGIFTS + ",");
                sqlStr.Append(t_ISTIMESALE + ",");
                sqlStr.Append(t_LimitTimePos + ",");
                sqlStr.Append("'" + t_TimeStart + "',");
                sqlStr.Append("'" + t_TimeEnd + "',");
                sqlStr.Append(t_ISHIDENITEM + ",");
                sqlStr.Append(t_GiftID_0 + ",");
                sqlStr.Append(t_GiftNUM_0 + ",");
                sqlStr.Append(t_GiftID_1 + ",");
                sqlStr.Append(t_GiftNUM_1 + ",");
                sqlStr.Append(t_GiftID_2 + ",");
                sqlStr.Append(t_GiftNUM_2 + ",");
                sqlStr.Append(t_GiftID_3 + ",");
                sqlStr.Append(t_GiftNUM_3 + ",");
                sqlStr.Append(t_GiftID_4 + ",");
                sqlStr.Append(t_GiftNUM_4 + ",");
                sqlStr.Append("'" + t_ItemInfo + "')");

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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }

        }

        protected string GetTimeSpan(DateTime startTime, DateTime endTime)
        {
            string timeSpan = string.Empty;
            TimeSpan ts = endTime - startTime;
            if (ts.Days > 0)
                timeSpan += ts.Days.ToString() + App_GlobalResources.Language.LblUnit_Day;
            if (ts.Hours > 0)
                timeSpan += ts.Hours.ToString() + App_GlobalResources.Language.LblUnit_Hour;
            if (ts.Minutes > 0)
                timeSpan += ts.Minutes.ToString() + App_GlobalResources.Language.LblUnit_Minute;
            if (ts.Seconds > 0)
                timeSpan += ts.Seconds.ToString() + App_GlobalResources.Language.LblUnit_Sec;
            return timeSpan;
        }

        protected void BtnImport_Click(object sender, EventArgs e)
        {
            string filename = string.Empty;
            try
            {
                filename = UpLoadXls(FileExcel);//上传XLS文件
                ImportXlsToData(filename);//将XLS文件的数据导入数据库                
                if (filename != string.Empty && System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);//删除上传的XLS文件
                }

                LblMessage.Text = App_GlobalResources.Language.Tip_DataImportSuccess + "！(" + App_GlobalResources.Language.LblNumber + ":" + ((DataSet)ViewState["dsImport"]).Tables[0].DefaultView.Count + ")";
                LblMessagePub.Text = "";
            }
            catch (Exception ex)
            {
                LblMessage.Text = ex.Message;
            }
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["dsImport"] == null)
                {
                    LblMessagePub.Text = App_GlobalResources.Language.Tip_PleaseImportFile;
                    return;
                }

                DataSet ds = (DataSet)ViewState["dsImport"];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    LblMessagePub.Text = App_GlobalResources.Language.Tip_NoDataNeedImport;
                    return;
                }

                bool ischeck = false;
                foreach (System.Web.UI.Control cbl in divZoneList.Controls)
                {
                    if (cbl.ID != null && cbl.ID.IndexOf("cblZone") != -1)
                    {
                        CheckBoxList cblTemp = (CheckBoxList)cbl;
                        if (cblTemp.SelectedIndex != -1)
                        {
                            ischeck = true;
                            break;
                        }
                    }
                }
                if (!ischeck)
                {
                    LblMessagePub.Text = "请选择需要导入数据的战区服务器";
                    return;
                }


                LblMessagePub.Text = "";
                foreach (System.Web.UI.Control cbl in divZoneList.Controls)
                {
                    if (cbl.ID != null && cbl.ID.IndexOf("cblZone") != -1)
                    {
                        CheckBoxList cblTemp = (CheckBoxList)cbl;
                        if (cblTemp.SelectedIndex != -1)
                        {
                            for (int x = 0; x < cblTemp.Items.Count; x++)
                            {
                                if (cblTemp.Items[x].Selected)
                                {
                                    string bigzone = cblTemp.ID.Substring(cblTemp.ID.LastIndexOf("_") + 1);
                                    string zone = cblTemp.Items[x].Value;

                                    if (cbReset.Checked)
                                    {
                                        string sql = "DELETE FROM OPENQUERY([LKSV] ,'select * from gameshopgoodstable ')";
                                        if (!ExecSql(sql, bigzone, zone))
                                        {
                                            LblMessagePub.Text = App_GlobalResources.Language.Tip_ResetDataError_StopPublish + "<br>";
                                            break;
                                        }
                                    }

                                    int count = ds.Tables[0].Rows.Count;
                                    for (int i = 0; i < count; i++)
                                    {
                                        bool result = AddItem(ds.Tables[0].Rows[i], bigzone, zone);
                                        if (!result)
                                        {
                                            ds.Tables[0].Rows[i]["F_State"] = App_GlobalResources.Language.Tip_Failure;
                                        }
                                        //ds.Tables[0].Rows.RemoveAt(0);
                                    }
                                    LblMessagePub.Text += string.Format("{0}"+App_GlobalResources.Language.Tip_DataPublishSuccess +"!", cblTemp.Items[x].Text);

                                    int countLose = ds.Tables[0].Select("F_State='" + App_GlobalResources.Language.Tip_Failure + "'").Length;
                                    for (int i = 0; i < countLose; i++)
                                    {
                                        ds.Tables[0].Rows[0]["F_State"] = App_GlobalResources.Language.Tip_Failure;
                                    }

                                    LblMessagePub.Text += "... " + string.Format(App_GlobalResources.Language.Tip_SuccessNumberFormat, count - countLose) + "  " +
                                          string.Format(App_GlobalResources.Language.Tip_FailuerNumberFormat, countLose) + "</br>";
                                    if (countLose > 0)
                                    {
                                        LblMessagePub.Text += App_GlobalResources.Language.Tip_OccurredErrorDataStopPublish + "<br>";
                                        break;
                                    }
                                }
                            }
                        }
                        if (LblMessagePub.Text.IndexOf("发布终止") != -1)
                        {
                            break;
                        }
                    }
                }

                //int count = ds.Tables[0].Rows.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    ds.Tables[0].Rows.RemoveAt(0);
                //}


                //LblMessagePub.Text = "数据发布成功!";

                //int countLose = ds.Tables[0].Rows.Count;
                //for (int i = 0; i < countLose; i++)
                //{
                //    ds.Tables[0].Rows[0]["F_State"] = "失败";
                //}


                ViewState["dsImport"] = ds;
                bind();

            }
            catch (System.Exception ex)
            {
                LblMessagePub.Text = ex.Message;
            }
        }
        private string AspEventArgs;

        public void RaiseCallbackEvent(string EventArgs)
        {
            AspEventArgs = EventArgs;
        }
        private static int i = 0;
        public string GetCallbackResult()
        {
            //int i = Convert.ToInt32(AspEventArgs);
            i++;
            return i.ToString() + ",150";
        }


    }
}
