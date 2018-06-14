using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.CDKey
{
    public partial class CDKeyImport : Admin_Page
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
                if (ds != null)
                {
                    myView = ds.Tables[0].DefaultView;
                }

                if (ds == null || myView.Count == 0)
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
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        private bool ExecSql(string sql)
        {
            int rcount = 0;
            SqlParameter[] parameters = {
                    new SqlParameter("@BigZoneID", SqlDbType.Int),
                    new SqlParameter("@ZoneID", SqlDbType.Int),
                    new SqlParameter("@DBType", SqlDbType.Int),
                    new SqlParameter("@Query",SqlDbType.NVarChar),
                    new SqlParameter("@RCount", SqlDbType.Int),
                    };
            parameters[0].Value = BigZoneID;
            parameters[1].Value = -1;
            parameters[2].Value = 8;
            parameters[3].Value = sql;
            parameters[4].Direction = ParameterDirection.Output;
            DBHelperDigGameDB.RunProcedure("_EXEC_SQLCustom", parameters, "ds", 180);
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
                ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();

                string sqlMaster;
                sqlMaster = " SELECT F_CDKey,F_KeyType  FROM [" + fname + "] where len(F_KeyType)>0";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();
                DataColumn col = new DataColumn("F_State");
                ds.Tables[0].Columns.Add(col);
                //ViewState["dsImport"] = ds;
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
        public bool AddItem(DataRow dr)
        {
            string m_CDKey = dr["F_CDKey"].ToString().Trim().Replace(cdkSign, "");
            string m_KeyType = dr["F_KeyType"].ToString().Trim();

            string strTips = "";
            if (!Common.Validate.IsInt(m_KeyType))
            {
                strTips = App_GlobalResources.Language.Tip_CardTypeIsError + "!!!";

            }
            if (strTips.Trim().Length > 0)
            {
                throw new Exception("CDKEY:" + m_CDKey + App_GlobalResources.Language.CDKey_ErrorOccurred + "  " + strTips);
            }

            //开始新增
            string sqlStr = "insert into OPENQUERY([LKSV] ,'select top 1 * from T_CDKeyList') (F_CDKey, F_IsUsed, F_KeyType, F_UserID, F_RoleID, F_UsedTime) values ('" + m_CDKey + "',0," + m_KeyType + ",null,null,null) ";
            return ExecSql(sqlStr);

        }


        //发奖用户数据
        protected string CopyAwardUserData(DataTable dt)
        {
            DateTime beginTime = DateTime.Now;

            //声明数据库连接
            SqlConnection conn = new SqlConnection(ConnStrDigGameDB);
            conn.Open();
            //声明SqlBulkCopy ,using释放非托管资源
            using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
            {
                //一次批量的插入的数据量
                sqlBC.BatchSize = 1000;
                //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                sqlBC.BulkCopyTimeout = 600;

                //设置要批量写入的表
                sqlBC.DestinationTableName = "[LKSV_8_MainDB_1_-1].MainDB.dbo.T_CDKeyList";

                //自定义的datatable和数据库的字段进行对应
                sqlBC.ColumnMappings.Add("F_CDKey", "F_CDKey");
                sqlBC.ColumnMappings.Add("F_KeyType", "F_KeyType");

                //批量写入
                sqlBC.WriteToServer(dt);
            }
            conn.Dispose();

            DateTime endTime = DateTime.Now;
            TimeSpan useTime = endTime - beginTime;//使用时间
            return App_GlobalResources.Language.Tip_CDKExecuteTime + "：" + useTime.TotalSeconds.ToString() + App_GlobalResources.Language.LblUnit_Sec;
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
                ds = null;
                ImportXlsToData(filename);//将XLS文件的数据导入数据库                
                //if (filename != string.Empty && System.IO.File.Exists(filename))
                //{
                //    System.IO.File.Delete(filename);//删除上传的XLS文件
                //}
                ViewState["filename"] = filename;
               // LblMessage.Text = App_GlobalResources.Language.Tip_DataImportSuccess+"！(数量:" + ((DataSet)ViewState["dsImport"]).Tables[0].DefaultView.Count + ")";
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
                ImportXlsToData(ViewState["filename"].ToString());//将XLS文件的数据导入数据库    

                if (ds == null)
                {
                    LblMessagePub.Text = App_GlobalResources.Language.Tip_PleaseImportFile;
                    return;
                }

               // DataSet ds = (DataSet)ViewState["dsImport"];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    LblMessagePub.Text = App_GlobalResources.Language.Tip_NoDataNeedImport;
                    return;
                }


                LblMessagePub.Text = "";



                if (cbReset.Checked)
                {
                    string sql = "DELETE FROM OPENQUERY([LKSV] ,'select * from T_CDKeyList')";
                    if (!ExecSql(sql))
                    {
                        LblMessagePub.Text =App_GlobalResources.Language.Tip_ResetDataError_StopPublish+ "<br>";
                        return;
                    }
                }

                int count = ds.Tables[0].Rows.Count;
                int countLose = 0;
                for (int i = 0; i < count; i++)
                {
                    bool result = AddItem(ds.Tables[0].Rows[i]);
                    if (!result)
                    {
                        ds.Tables[0].Rows[i]["F_State"] = App_GlobalResources.Language.Tip_Failure;
                    }
                    else
                    {
                        ds.Tables[0].Rows[i]["F_State"] = App_GlobalResources.Language.Tip_Success;
                    }
                    //ds.Tables[0].Rows.RemoveAt(0);
                }

               // LblMessagePub.Text +=CopyAwardUserData(ds.Tables[0]);

                LblMessagePub.Text +=App_GlobalResources.Language.Tip_DataPublishSuccess +"!";


                LblMessagePub.Text += "... " + string.Format(App_GlobalResources.Language.Tip_SuccessNumberFormat, count - countLose) + "  " +
                                         string.Format(App_GlobalResources.Language.Tip_FailuerNumberFormat, countLose) + "</br>";

               // ViewState["dsImport"] = ds;
                bind();

            }
            catch (System.Exception ex)
            {
                LblMessagePub.Text = ex.Message;
            }
        }



    }
}
