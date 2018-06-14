using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GiftConfig
{
    public partial class GiftConfig :Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();
        DbHelperSQLP dbHelperMySQL = new DbHelperSQLP();

        DataSet ds;
        #endregion

        #region 事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            GetGameCoreDBString();
            if (!IsPostBack)
            {
                //绑定战区
                BindBattleZone();
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int taskID = Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm"));
            string sendBattleZone = string.Empty;

            #region 获取礼包配置基本信息

            #region 战区
            string strBigZone = "0";//大区默认值

            string strBattleZone = string.Empty;
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)//获取选中的战区
            {
                if (ckbBattleZone.Items[i].Selected)
                {
                    strBattleZone += ckbBattleZone.Items[i].Value + ";";
                }
            }
            if (strBattleZone.Length > 0)//判断是否选择战区
            {
                strBattleZone = strBattleZone.Substring(0, strBattleZone.Length - 1);
            }
            if (string.IsNullOrEmpty(strBattleZone))
            {
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SelectBattleZone + "');</Script>");
                return;
            }
            string[] arrayBattleZone = strBattleZone.Split(';');
            #endregion

            string strPackageName = this.tbPackageName.Text.Trim();//礼包名称
            if(string.IsNullOrEmpty(strPackageName))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写礼包名称！');</Script>");
                return;
            }

            string strItemFlag = this.ckbItemFlag.Checked ? "1" : "0";//是否推荐0正常1推荐
            string strProductID = this.ddlProductID.SelectedValue;//ProductID

            string strOldKRWMoney = this.OldKRWMoney.Text.Trim();
            string strOldUSDMoney = this.OldUSDMoney.Text.Trim();
            string strCurKRWMoney = this.CurKRWMoney.Text.Trim();
            string strCurUSDMoney = this.CurUSDMoney.Text.Trim();
            if(string.IsNullOrEmpty(strOldKRWMoney)||string.IsNullOrEmpty(strOldUSDMoney)||string.IsNullOrEmpty(strCurKRWMoney)||string.IsNullOrEmpty(strCurUSDMoney))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写商品价格！');</Script>");
                return;
            }

            string strItemType = this.ddlItemType.SelectedValue;//分页类型
            string strItemTypeText = GetItemTypeText(this.ddlItemType.SelectedValue);//分页类型显示文本

            string strPos = this.Pos.Text.Trim();//礼包显示位置
            if (string.IsNullOrEmpty(strPos))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写礼包显示顺利！');</Script>");
                return;
            }

            string strPackageMoneyType = this.ddlPackageMoneyType.SelectedValue;//礼包类型
            string strLimitStell = this.ddlLimitStell.SelectedValue;//限制类型
            string strLimitNum = string.IsNullOrEmpty(this.LimitNum.Text.Trim()) ? "0" : this.LimitNum.Text.Trim();//限购个数
            string strLimitTime = this.ckbLimitTime.Checked ? "1" : "0";//是否限时

            string strSTime = string.IsNullOrEmpty(this.TimeStart.Text.Trim()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : this.TimeStart.Text.Trim();//限时开始时间
            string strETime = string.IsNullOrEmpty(this.TimeEnd.Text.Trim()) ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") : this.TimeEnd.Text.Trim();//限时结束时间

            string strPicID = this.ddlPicID.SelectedValue;//显示图片

            string strGiftID_0 = string.IsNullOrEmpty(this.F_GiftID_0.Text.Trim()) ? "0" : this.F_GiftID_0.Text.Trim();
            string strGiftID_1 = string.IsNullOrEmpty(this.F_GiftID_1.Text.Trim()) ? "0" : this.F_GiftID_1.Text.Trim();
            string strGiftID_2 = string.IsNullOrEmpty(this.F_GiftID_2.Text.Trim()) ? "0" : this.F_GiftID_2.Text.Trim();
            string strGiftID_3 = string.IsNullOrEmpty(this.F_GiftID_3.Text.Trim()) ? "0" : this.F_GiftID_3.Text.Trim();
            string strGiftID_4 = string.IsNullOrEmpty(this.F_GiftID_4.Text.Trim()) ? "0" : this.F_GiftID_4.Text.Trim();
            string strGiftNUM_0 = string.IsNullOrEmpty(this.F_GiftNUM_0.Text.Trim()) ? "0" : this.F_GiftNUM_0.Text.Trim();
            string strGiftNUM_1 = string.IsNullOrEmpty(this.F_GiftNUM_1.Text.Trim()) ? "0" : this.F_GiftNUM_1.Text.Trim();
            string strGiftNUM_2 = string.IsNullOrEmpty(this.F_GiftNUM_2.Text.Trim()) ? "0" : this.F_GiftNUM_2.Text.Trim();
            string strGiftNUM_3 = string.IsNullOrEmpty(this.F_GiftNUM_3.Text.Trim()) ? "0" : this.F_GiftNUM_3.Text.Trim();
            string strGiftNUM_4 = string.IsNullOrEmpty(this.F_GiftNUM_4.Text.Trim()) ? "0" : this.F_GiftNUM_4.Text.Trim();
            string strGifts = string.Format("{0};{1}|{2};{3}|{4};{5}|{6};{7}|{8};{9}|", strGiftID_0, strGiftNUM_0, strGiftID_1, strGiftNUM_1,strGiftID_2, strGiftNUM_2, strGiftID_3, strGiftNUM_3, strGiftID_4, strGiftNUM_4);

            string strTitle = this.tbTitle.Text.Trim();
            if(string.IsNullOrEmpty(strTitle))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写邮件标题！');</Script>");
                return;
            }
            string strSendUser = this.tbSnedUser.Text.Trim();
            if(string.IsNullOrEmpty(strSendUser))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写发件人！');</Script>");
                return;
            }
            string strMailContent = this.tbMailContent.Text.Trim();
            if(string.IsNullOrEmpty(strMailContent))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写邮件内容！');</Script>");
                return;
            }
            string strItemInfo = this.ItemInfo.Text.Trim();
            if(string.IsNullOrEmpty(strItemInfo))
            {
                Response.Write("<Script Language=JavaScript>alert('请填写礼包描述！');</Script>");
                return;
            }
            #endregion

            try
            {
                #region 写入gameshop_package
                for (int m = 0; m < arrayBattleZone.Count(); m++)
                {
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@bigZone", SqlDbType.Int) { Value = strBigZone });
                    param.Add(new SqlParameter("@ZoneID", SqlDbType.Int) { Value = Convert.ToInt32(arrayBattleZone[m]) });
                    param.Add(new SqlParameter("@logicJson", SqlDbType.NVarChar) { Value = strBattleZone + "&" + strGifts });
                    param.Add(new SqlParameter("@taskId", SqlDbType.Int) { Value = taskID });

                    DataSet ds = DBHelperGSSDB.RunProcedure("SP_AddAwardToMysql", param.ToArray(), "tableName");
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_SyncDBFailure + ";已经发送成功的战区：" + sendBattleZone + "');</Script>");
                        return;
                    }
                    string link = ds.Tables[0].Rows[0]["name"] as string;
                    string conn = ds.Tables[0].Rows[0]["provider_string"] as string;
                    string filter = FilterMySqlDBConnString(conn);
                    dbHelperMySQL.connectionString = filter;

                    string sqlSelect = string.Format("SELECT * FROM gameshop_package WHERE F_ProductID={0}",strProductID);
                    ds = dbHelperMySQL.QueryForMysql(sqlSelect);
                    if(ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count==0)
                    {
                        string sqlInsert = string.Format("INSERT INTO gameshop_package (F_ProductID, F_ServerSaveID, F_PicID, F_Pos, F_ItemType, F_ItemType_TextId, F_PackageName, F_PackageMoneyType, F_OldKRWMoney, F_OldUSDMoney, F_CurKRWMoney, F_CurUSDMoney, F_ItemFlag, F_LimitNum, F_LimitTime, F_LimitStell, F_TimeStart, F_TimeEnd, F_GiftID_0, F_GiftNUM_0, F_GiftID_1, F_GiftNUM_1, F_GiftID_2, F_GiftNUM_2, F_GiftID_3, F_GiftNUM_3, F_GiftID_4, F_GiftNUM_4, F_Mail_Title, F_Mail_Content, F_Sender_Name, F_ItemInfo) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, N'{6}', {7}, {8}, {9}, {10}, {11}, {12},{13},{14},{15}, '{16}', '{17}', {18},{19},{20},{21}, {22},{23},{24},{25},{26},{27}, N'{28}', N'{29}', N'{30}', N'{31}')",strProductID,strProductID,strPicID,strPos,strItemType,strItemTypeText,strPackageName,strPackageMoneyType,strOldKRWMoney,strOldUSDMoney,strCurKRWMoney,strCurUSDMoney,strItemFlag,strLimitNum,strLimitTime,strLimitStell,strSTime,strETime,strGiftID_0,strGiftNUM_0,strGiftID_1,strGiftNUM_1,strGiftID_2,strGiftNUM_2,strGiftID_3,strGiftNUM_3,strGiftID_4,strGiftNUM_4,strTitle,strMailContent,strSendUser,strItemInfo);

                        int res = dbHelperMySQL.ExecuteMySql(sqlInsert);
                        if (res > 0)
                        {
                        }
                        else
                        {
                            Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "已经发送成功的战区：" + sendBattleZone + "');</Script>");
                            return;
                        }
                    }
                    else
                    {
                        string strServerSaveID=string.Empty;
                        if(strProductID.Length==ds.Tables[0].Rows[0]["F_ServerSaveID"].ToString().Length)
                        {
                            strServerSaveID=strProductID+"00";
                        }
                        else
                        {
                            strServerSaveID = (Convert.ToInt32(ds.Tables[0].Rows[0]["F_ServerSaveID"].ToString()) + 1).ToString();
                        }
                        string sqlUpdate = string.Format("UPDATE gameshop_package SET F_ProductID={0}, F_ServerSaveID={1}, F_PicID={2}, F_Pos={3}, F_ItemType={4}, F_ItemType_TextId={5}, F_PackageName=N'{6}', F_PackageMoneyType={7}, F_OldKRWMoney={8}, F_OldUSDMoney={9}, F_CurKRWMoney={10}, F_CurUSDMoney={11}, F_ItemFlag={12}, F_LimitNum={13}, F_LimitTime={14}, F_LimitStell={15}, F_TimeStart='{16}', F_TimeEnd='{17}', F_GiftID_0={18}, F_GiftNUM_0={19}, F_GiftID_1={20}, F_GiftNUM_1={21}, F_GiftID_2={22}, F_GiftNUM_2={23}, F_GiftID_3={24}, F_GiftNUM_3={25}, F_GiftID_4={26}, F_GiftNUM_4={27}, F_Mail_Title=N'{28}', F_Mail_Content=N'{29}', F_Sender_Name=N'{30}', F_ItemInfo=N'{31}' WHERE (F_ProductID={32})", strProductID, strServerSaveID, strPicID, strPos, strItemType, strItemTypeText, strPackageName, strPackageMoneyType, strOldKRWMoney, strOldUSDMoney, strCurKRWMoney, strCurUSDMoney, strItemFlag, strLimitNum, strLimitTime, strLimitStell, strSTime, strETime, strGiftID_0, strGiftNUM_0, strGiftID_1, strGiftNUM_1, strGiftID_2, strGiftNUM_2, strGiftID_3, strGiftNUM_3, strGiftID_4, strGiftNUM_4, strTitle, strMailContent, strSendUser, strItemInfo, strProductID);

                        int res = dbHelperMySQL.ExecuteMySql(sqlUpdate);
                        if (res > 0)
                        {
                        }
                        else
                        {
                            Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "已经发送成功的战区：" + sendBattleZone + "');</Script>");
                            return;
                        }
                    }
                    sendBattleZone += arrayBattleZone[m] + ";";
                }
                Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                #endregion

                #region 写入T_GiftConfigLog表
                string sql = string.Format("INSERT INTO T_GiftConfigLog (F_ProductID, F_ServerSaveID, F_PicID, F_Pos, F_ItemType, F_ItemType_TextId, F_PackageName, F_PackageMoneyType, F_OldKRWMoney, F_OldUSDMoney, F_CurKRWMoney, F_CurUSDMoney, F_ItemFlag, F_LimitNum, F_LimitTime, F_LimitStell, F_TimeStart, F_TimeEnd, F_GiftID_0, F_GiftNUM_0, F_GiftID_1, F_GiftNUM_1, F_GiftID_2, F_GiftNUM_2, F_GiftID_3, F_GiftNUM_3, F_GiftID_4, F_GiftNUM_4, F_Mail_Title, F_Mail_Content, F_Sender_Name, F_ItemInfo,F_BattleZone,F_OPTime) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, N'{6}', {7}, {8}, {9}, {10}, {11}, {12},{13},{14},{15}, '{16}', '{17}', {18},{19},{20},{21}, {22},{23},{24},{25},{26},{27}, N'{28}', N'{29}', N'{30}', N'{31}',N'{32}',GETDATE())", strProductID, strProductID, strPicID, strPos, strItemType, strItemTypeText, strPackageName, strPackageMoneyType, strOldKRWMoney, strOldUSDMoney, strCurKRWMoney, strCurUSDMoney, strItemFlag, strLimitNum, strLimitTime, strLimitStell, strSTime, strETime, strGiftID_0, strGiftNUM_0, strGiftID_1, strGiftNUM_1, strGiftID_2, strGiftNUM_2, strGiftID_3, strGiftNUM_3, strGiftID_4, strGiftNUM_4, strTitle, strMailContent, strSendUser, strItemInfo, sendBattleZone);
                DBHelperDigGameDB.ExecuteSql(sql);
                #endregion
            }
            catch(Exception ex)
            {
                
            }
        }
        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //战区
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)
            {
                ckbBattleZone.Items[i].Selected = false;
            }
            this.tbPackageName.Text = "";
            this.ckbItemFlag.Checked = false;
            this.ddlProductID.SelectedIndex = 0;
            this.OldKRWMoney.Text = "";
            this.OldUSDMoney.Text = "";
            this.CurKRWMoney.Text = "";
            this.CurUSDMoney.Text = "";
            this.ddlItemType.SelectedIndex = 0;
            this.Pos.Text = "";
            this.ddlPackageMoneyType.SelectedIndex = 0;
            this.ddlLimitStell.SelectedIndex = 0;
            this.LimitNum.Text = "";
            this.ckbLimitTime.Checked = false;
            this.TimeStart.Text = "";
            this.TimeEnd.Text = "";
            this.ddlPicID.SelectedIndex = 0;
            this.F_GiftID_0.Text = "";
            this.F_GiftID_1.Text = "";
            this.F_GiftID_2.Text = "";
            this.F_GiftID_3.Text = "";
            this.F_GiftID_4.Text = "";
            this.F_GiftNUM_0.Text = "";
            this.F_GiftNUM_1.Text = "";
            this.F_GiftNUM_2.Text = "";
            this.F_GiftNUM_3.Text = "";
            this.F_GiftNUM_4.Text = "";
            this.tbTitle.Text = "";
            this.tbSnedUser.Text = "";
            this.tbMailContent.Text = ""; 
            this.ItemInfo.Text = "";
        }
        #endregion

        #region 方法
        private void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        /// <summary>
        /// 绑定战区
        /// </summary>
        public void BindBattleZone()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value,F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = 100001) AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();

                this.ckbBattleZone.DataSource = ds;
                this.ckbBattleZone.DataTextField = "F_Name";
                this.ckbBattleZone.DataValueField = "F_ValueGame";
                this.ckbBattleZone.DataBind();
            }
            catch (System.Exception ex)
            {
            }
        }
        /// <summary>
        /// 获取分页类型文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetItemTypeText(string value)
        {
            switch (value)
            {
                case "1":
                    return "2201";
                case "2":
                    return "2202";
                case "3":
                    return "2203";
                case "4":
                    return "2204";
                case "5":
                    return "2205";
                default:
                    return value;
            }
        }
        public static string FilterMySqlDBConnString(string dbConstring)
        {
            string[] filter = new string[] { "driver", "option", "stmt" };
            string[] mysqlConfigs = dbConstring.Split(';');
            List<string> connConfig = new List<string>();
            foreach (string item in mysqlConfigs)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                if (!filter.Contains(item.Split('=')[0].ToLower()))
                {
                    connConfig.Add(item);
                }
            }
            return string.Join(";", connConfig.ToArray());
        }
        #endregion

        protected void ckbBattleZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strBattleZone = string.Empty;
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)//获取选中的战区
            {
                if (ckbBattleZone.Items[i].Selected)
                {
                    strBattleZone += ckbBattleZone.Items[i].Value + ";";
                }
            }
            if (strBattleZone.Length == 0)//判断是否选择战区
                return;
            strBattleZone = strBattleZone.Substring(0, strBattleZone.Length - 1);
            string[] arrayBattleZone = strBattleZone.Split(';');
            string sql = string.Empty;
            for (int i = 0; i < arrayBattleZone.Length; i++)
            {
                if(string.IsNullOrEmpty(sql))
                {
                    sql += @"SELECT * FROM OPENQUERY (LKSV_7_gspara_db_0_" + arrayBattleZone[i] + ",'SELECT F_ProductID FROM deposit_table')";
                }
                else
                {
                    sql += @"UNION SELECT * FROM OPENQUERY (LKSV_7_gspara_db_0_" + arrayBattleZone[i] + ",'SELECT F_ProductID FROM deposit_table')";
                }
            }
            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                this.ddlProductID.DataSource = ds;
                this.ddlProductID.DataTextField = "F_ProductID";
                this.ddlProductID.DataValueField = "F_ProductID";
                this.ddlProductID.DataBind();

                if (!string.IsNullOrEmpty(ddlProductID.SelectedValue))
                {
                    sql = @"SELECT * FROM OPENQUERY (LKSV_7_gspara_db_0_" + arrayBattleZone[0] + ",'SELECT F_OldKRWCostMoney,F_OldUSDCostMoney,F_CurKRWCostMoney,F_CurUSDCostMoney FROM deposit_table WHERE F_ProductID="+ddlProductID.SelectedValue+"')";
                    ds = DBHelperDigGameDB.Query(sql);
                    if(ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count==0){}
                    else
                    {
                        this.OldKRWMoney.Text = ds.Tables[0].Rows[0]["F_OldKRWCostMoney"] + "";
                        this.OldUSDMoney.Text = ds.Tables[0].Rows[0]["F_OldUSDCostMoney"] + "";
                        this.CurKRWMoney.Text = ds.Tables[0].Rows[0]["F_CurKRWCostMoney"] + "";
                        this.CurUSDMoney.Text = ds.Tables[0].Rows[0]["F_CurUSDCostMoney"] + "";
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        protected void ddlProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strBattleZone = string.Empty;
            for (int i = 0; i < ckbBattleZone.Items.Count; i++)//获取选中的战区
            {
                if (ckbBattleZone.Items[i].Selected)
                {
                    strBattleZone += ckbBattleZone.Items[i].Value + ";";
                }
            }
            if (strBattleZone.Length == 0)//判断是否选择战区
                return;
            strBattleZone = strBattleZone.Substring(0, strBattleZone.Length - 1);
            string[] arrayBattleZone = strBattleZone.Split(';');

            if (!string.IsNullOrEmpty(ddlProductID.SelectedValue))
            {
                string sql = @"SELECT * FROM OPENQUERY (LKSV_7_gspara_db_0_" + arrayBattleZone[0] + ",'SELECT F_OldKRWCostMoney,F_OldUSDCostMoney,F_CurKRWCostMoney,F_CurUSDCostMoney FROM deposit_table WHERE F_ProductID=" + ddlProductID.SelectedValue + "')";
                ds = DBHelperDigGameDB.Query(sql);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) { }
                else
                {
                    this.OldKRWMoney.Text = ds.Tables[0].Rows[0]["F_OldKRWCostMoney"] + "";
                    this.OldUSDMoney.Text = ds.Tables[0].Rows[0]["F_OldUSDCostMoney"] + "";
                    this.CurKRWMoney.Text = ds.Tables[0].Rows[0]["F_CurKRWCostMoney"] + "";
                    this.CurUSDMoney.Text = ds.Tables[0].Rows[0]["F_CurUSDCostMoney"] + "";
                }
            }
        }
    }
}