using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GiftConfig
{
    public partial class GiftList : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGameCoreDB = new DbHelperSQLP();
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
                bind();
                BindDdl1();
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
        /// <summary>
        /// 页面索引发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            var _sender = sender as LinkButton;
            if (_sender != null)
            {
                //获取要删除数据ID
                string ID = _sender.Attributes["_InfoID"];
                if (string.IsNullOrEmpty(ID))
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                    return;
                }
                string sql = string.Format("SELECT [F_ID],[F_PreDutyMan],[F_TUseData] FROM [dbo].[T_Tasks] WHERE F_Type=20000214 AND F_ID={0}", ID);
                ds = DBHelperGSSDB.Query(sql);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_OrderIsDelete + "');</Script>");
                    return;
                }
                string taskID = ds.Tables[0].Rows[0]["F_PreDutyMan"].ToString();
                if (string.IsNullOrEmpty(taskID))
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_OrderIsOverTime + "');</Script>");
                    return;
                }
                sql = string.Format("SELECT F_State FROM T_GiftAward_List WHERE F_TaskID={0}", taskID);
                ds = DBHelperGameCoreDB.Query(sql);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_OrderIsDelete + "');</Script>");
                    return;
                }
                string state = ds.Tables[0].Rows[0]["F_State"].ToString();
                if (Convert.ToInt32(state) == 1)
                {
                    sql = string.Format("DELETE FROM T_GiftAward_Gift WHERE F_TaskID={0}", taskID);
                    DBHelperGameCoreDB.ExecuteSql(sql);
                    sql = string.Format("DELETE FROM T_GiftAward_List WHERE F_TaskID={0}", taskID);
                    DBHelperGameCoreDB.ExecuteSql(sql);
                    sql = string.Format("DELETE FROM T_GiftAward_User WHERE F_TaskID={0}", taskID);
                    DBHelperGameCoreDB.ExecuteSql(sql);
                }
                else
                {
                    //todo
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_OrderIsOverTime + "');</Script>");
                    return;
                }
                sql = string.Format("DELETE FROM [dbo].[T_Tasks] WHERE F_Type=20000214 AND F_ID={0}", ID);
                int res = DBHelperGSSDB.ExecuteSql(sql);
                if (res > 0)
                {
                    bind();
                    sql = string.Format("INSERT INTO [dbo].[T_OpLog] ([F_Module], [F_OPID], [F_TaskID], [F_User], [F_CreateTime]) VALUES (N'发奖工单', N'Delete', {0},N'{1}', GETDATE())", ID, Session["LoginUser"].ToString());
                    DBHelperDigGameDB.ExecuteSql(sql);
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Success + "');</Script>");
                }
                else
                {
                    Response.Write("<Script Language=JavaScript>alert('" + App_GlobalResources.Language.Tip_Failure + "');</Script>");
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            //if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            //{
            //    Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
            //    return;
            //}

            //LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            //if (DropDownListArea2.SelectedIndex > 0)
            //{
            //    LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            //}
            //DateTime searchdateB = DateTime.Now;
            //DateTime searchdateE = DateTime.Now;
            //if (tboxTimeB.Text.Length > 0)
            //{
            //    searchdateB = Convert.ToDateTime(tboxTimeB.Text);
            //}
            //if (tboxTimeE.Text.Length > 0)
            //{
            //    searchdateE = Convert.ToDateTime(tboxTimeE.Text);
            //}
            //LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat) + " ";

            string sqlwhere = " where 1=1 ";
            //if (!string.IsNullOrEmpty(tbEmailType.Text.Trim()))
            //{
            //    sqlwhere += @" and Mail_TYPE=" + tbEmailType.Text.Trim();
            //}
            //if (!string.IsNullOrEmpty(tbRoleID.Text.Trim()))
            //{
            //    sqlwhere += @" and Receiver_ID=" + tbRoleID.Text.Trim();
            //}
            //sqlwhere += " and Send_Time>\"" + Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-dd") + "\"";
            //sqlwhere += " and Send_Time<\"" + Convert.ToDateTime(tboxTimeE.Text).AddDays(1).ToString("yyyy-MM-dd") + "\"";
            string bigZone = DropDownListArea1.SelectedIndex > 0 ? DropDownListArea1.SelectedValue.Split(',')[1] + "" : "0";
            string battleZone = string.Empty;
            if (WssPublish == "0")
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "1";
            }
            else
            {
                battleZone = DropDownListArea2.SelectedIndex > 0 ? DropDownListArea2.SelectedValue.Split(',')[1] + "" : "5";
            }

            string sql = "";
            sql = "SELECT * FROM OPENQUERY ([LKSV_7_gspara_db_" + bigZone + "_" + battleZone + "],'SELECT F_ProductID,F_PicID,F_Pos,F_ItemType,F_ItemType_TextId,F_PackageName,F_PackageMoneyType,F_OldKRWMoney,F_OldUSDMoney,F_CurKRWMoney,F_CurUSDMoney,F_ItemFlag,F_LimitNum,F_LimitTime,F_LimitStell,F_TimeStart,F_TimeEnd,F_GiftID_0,F_GiftNUM_0,F_GiftID_1,F_GiftNUM_1,F_GiftID_2,F_GiftNUM_2,F_GiftID_3,F_GiftNUM_3,F_GiftID_4,F_GiftNUM_4,F_Mail_Content,F_ItemInfo FROM gameshop_package ORDER BY F_Pos')";

            try
            {
                ds = DBHelperDigGameDB.Query(sql);
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
        public void GetGameCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='GameCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperGameCoreDB.connectionString = conn;
        }
        #endregion

        #region 转到触发方法
        protected void Go_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = int.Parse(((TextBox)GridView1.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
            bind();   //重新绑定GridView
        }
        #endregion

        #region 分页触发方法
        protected void GvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();  //重新绑定GridView
        }
        #endregion

        #region 查看历史页面跳转
        protected void btnGiftAward_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHis.aspx");
        }

        protected void btnFullServicesMail_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisFullServicesMail.aspx");
        }

        protected void btnDeleteFullServicesMail_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisDeleteFullServicesMail.aspx");
        }
        protected void btnDeleteGiftAward_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisDeleteGiftAward.aspx");
        }
        protected void btnLockUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisLockUser.aspx");
        }
        protected void btnUnLockUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisUnLockUser.aspx");
        }
        protected void btnLockRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisLockRole.aspx");
        }
        protected void btnUnLockRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisUnLockRole.aspx");
        }
        protected void btnDisRoleChatAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisDisRoleChatAdd.aspx");
        }
        protected void btnDisRoleChatDel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisDisRoleChatDel.aspx");
        }
        protected void btnRoleRecovery_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisRoleRecovery.aspx");
        }
        protected void btnGameNoticeHis_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModelPage/GssTool/GssViewHisGameNotice.aspx");
        }
        #endregion
        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListArea2.Items.Clear();
            BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);
        }
        public void BindDdl1()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = 1000) AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();
                newdr["F_Name"] = App_GlobalResources.Language.LblAllBigZone;
                newdr["F_ValueGame"] = "";
                ds.Tables[0].Rows.InsertAt(newdr, 0);
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
                DataRow newdr = ds.Tables[0].NewRow();
                newdr["F_Name"] = App_GlobalResources.Language.LblAllZone;

                newdr["F_ValueGame"] = "";
                ds.Tables[0].Rows.InsertAt(newdr, 0);
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

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {

        }

        #region 转换文字说明
        protected string GetLimitStell(string value)
        {
            switch(value)
            {
                case "1":
                    return "日";
                case "2":
                    return "周";
                case "3":
                    return "月";
                case "4":
                    return "永久";
                default:
                    return value;
            }
        }
        protected string GetLimitTime(string value)
        {
            if (value == "1")
                return "是";
            else
                return "否";
        }
        protected string GetItemFlag(string value)
        {
            switch (value)
            {
                case "0":
                    return "正常";
                case "1":
                    return "推荐";
                default:
                    return value;
            }
        }
        protected string GetItemType(string value)
        {
            switch (value)
            {
                case "1":
                    return "装备";
                case "2":
                    return "材料";
                case "3": 
                    return "宠物";
                case "4": 
                    return "坐骑";
                case "5": 
                    return "特殊";
                default:
                    return value;
            }
        }
        protected string GetItemTypeText(string value)
        {
            switch (value)
            {
                case "2201":
                    return "装备";
                case "2202":
                    return "材料";
                case "2203":
                    return "宠物";
                case "2204":
                    return "坐骑";
                case "2205":
                    return "特殊";
                default:
                    return value;
            }
        }
        protected string GetPackageMoneyType(string value)
        {
            switch (value)
            {
                case "0":
                    return "人民币礼包";
                case "1":
                    return "游戏币礼包";
                default:
                    return value;
            }
        }
        #endregion
    }
}
