using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
	public partial class EmailLog : Admin_Page
    {
        #region 属性
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

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

            if (!IsPostBack)
            {
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                //绑定GridView
                bind();
                BindBigZone();
            }
            ControlOutFile1.ControlOut = GridView1;
            ControlOutFile1.VisibleExcel = false;
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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            System.Text.EncodingInfo[] ss = System.Text.Encoding.GetEncodings();
            ControlDateSelect1.SetSelectDate(tboxTimeB.Text);
            bind();
        }
        /// <summary>
        /// 大区Index变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListArea2.Items.Clear();
            BindBattleZone(DropDownListArea1.SelectedValue.Split(',')[0]);
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlDateSelect1.SelectDate.ToString("yyyy-MM-dd");
            bind();
        }
        protected void ControlChartSelect_SelectChanged(object sender, EventArgs e)
        {
            if (ControlChartSelect1.State == 0)
            {
                GridView1.Visible = true;
                ControlChart1.Visible = false;
            }
            else
            {
                GridView1.Visible = false;
                ControlChart1.Visible = true;
                //ControlChart1.SetChart(GridView2, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
            }
        }
        #endregion

        #region 方法
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

            LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            if (DropDownListArea2.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            }
            DateTime searchdateB = DateTime.Now;
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
            if (!string.IsNullOrEmpty(tbEmailType.Text.Trim()))
            {
                sqlwhere += @" and Mail_TYPE=" + tbEmailType.Text.Trim();
            }
            if (!string.IsNullOrEmpty(tbRoleID.Text.Trim()))
            {
                sqlwhere += @" and Receiver_ID=" + tbRoleID.Text.Trim();
            }
            sqlwhere += " and Send_Time>\"" + Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-dd") + "\"";
            sqlwhere += " and Send_Time<\"" + Convert.ToDateTime(tboxTimeE.Text).AddDays(1).ToString("yyyy-MM-dd") + "\"";
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
            string sqlCount = "";
            
            sql = "SELECT * FROM OPENQUERY ([LKSV_3_gsdata_db_" + bigZone + "_" + battleZone + "],'SELECT * FROM (SELECT Mail_Content,Mail_TYPE,Receiver_ID,Send_Time,Invalid_Time,State_Modi_Time,Mail_State,Money,Money_Sliver,TongBao,TongBei,Item1,Item2,Item3,Item4,Item5,0 AS IsDelete FROM maillist_table WHERE Mail_TYPE <> 120 UNION ALL SELECT Mail_Content,Mail_TYPE,Receiver_ID,Send_Time,Invalid_Time,State_Modi_Time,Mail_State,Money,Money_Sliver,TongBao,TongBei,Item1,Item2,Item3,Item4,Item5,0 AS IsDelete FROM invalid_mail_table WHERE Mail_TYPE <> 120) A "+sqlwhere+" ORDER BY ITEM1 DESC limit " + (Convert.ToInt32(lblcurPage.Text) - 1) * 20 + ",20')";

            sqlCount = "SELECT * FROM OPENQUERY ([LKSV_GSS_3_gsdata_db_" + bigZone + "_" + battleZone + "],'SELECT count(*) FROM (SELECT Mail_Content,Mail_TYPE,Receiver_ID,Send_Time,Invalid_Time,State_Modi_Time,Mail_State,Money,Money_Sliver,TongBao,TongBei,Item1,Item2,Item3,Item4,Item5,0 AS IsDelete FROM maillist_table WHERE Mail_TYPE <> 120 UNION ALL SELECT Mail_Content,Mail_TYPE,Receiver_ID,Send_Time,Invalid_Time,State_Modi_Time,Mail_State,Money,Money_Sliver,TongBao,TongBei,Item1,Item2,Item3,Item4,Item5,0 AS IsDelete FROM invalid_mail_table WHERE Mail_TYPE <> 120) A "+sqlwhere+"')";

            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                object obj = DBHelperDigGameDB.GetSingle(sqlCount);
                int objCount = Convert.ToInt32(obj);

                ds.Tables[0].Columns.Add("ItemBak1", typeof(string));
                ds.Tables[0].Columns.Add("ItemBakID1", typeof(string));
                ds.Tables[0].Columns.Add("ItemBak2", typeof(string));
                ds.Tables[0].Columns.Add("ItemBakID2", typeof(string));
                ds.Tables[0].Columns.Add("ItemBak3", typeof(string));
                ds.Tables[0].Columns.Add("ItemBakID3", typeof(string));
                ds.Tables[0].Columns.Add("ItemBak4", typeof(string));
                ds.Tables[0].Columns.Add("ItemBakID4", typeof(string));
                ds.Tables[0].Columns.Add("ItemBak5", typeof(string));
                ds.Tables[0].Columns.Add("ItemBakID5", typeof(string));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int item1 = 0;
                    int itemBak1 = 0;
                    byte[] btItem1 = (byte[])ds.Tables[0].Rows[i]["Item1"];
                    if (btItem1.Length >= 8)
                    {
                        item1 = BitConverter.ToInt32(btItem1, 0);
                        itemBak1 = BitConverter.ToInt32(btItem1, 4);
                    }
                    ds.Tables[0].Rows[i]["ItemBak1"] = item1;
                    ds.Tables[0].Rows[i]["ItemBakID1"] = itemBak1;

                    int item2 = 0;
                    int itemBak2 = 0;
                    byte[] btItem2 = (byte[])ds.Tables[0].Rows[i]["Item2"];
                    if (btItem2.Length >= 8)
                    {
                        item2 = BitConverter.ToInt32(btItem2, 0);
                        itemBak2 = BitConverter.ToInt32(btItem2, 4);
                    }
                    ds.Tables[0].Rows[i]["ItemBak2"] = item2;
                    ds.Tables[0].Rows[i]["ItemBakID2"] = itemBak2;

                    int item3 = 0;
                    int itemBak3 = 0;
                    byte[] btItem3 = (byte[])ds.Tables[0].Rows[i]["Item3"];
                    if (btItem3.Length >= 8)
                    {
                        item3 = BitConverter.ToInt32(btItem3, 0);
                        itemBak3 = BitConverter.ToInt32(btItem3, 4);
                    }
                    ds.Tables[0].Rows[i]["ItemBak3"] = item3;
                    ds.Tables[0].Rows[i]["ItemBakID3"] = itemBak3;

                    int item4 = 0;
                    int itemBak4 = 0;
                    byte[] btItem4 = (byte[])ds.Tables[0].Rows[i]["Item4"];
                    if (btItem4.Length >= 8)
                    {
                        item4 = BitConverter.ToInt32(btItem4, 0);
                        itemBak4 = BitConverter.ToInt32(btItem4, 4);
                    }
                    ds.Tables[0].Rows[i]["ItemBak4"] = item4;
                    ds.Tables[0].Rows[i]["ItemBakID4"] = itemBak4;

                    int item5 = 0;
                    int itemBak5 = 0;
                    byte[] btItem5 = (byte[])ds.Tables[0].Rows[i]["Item5"];
                    if (btItem5.Length >= 8)
                    {
                        item5 = BitConverter.ToInt32(btItem5, 0);
                        itemBak5 = BitConverter.ToInt32(btItem5, 4);
                    }
                    ds.Tables[0].Rows[i]["ItemBak5"] = item5;
                    ds.Tables[0].Rows[i]["ItemBakID5"] = itemBak5;
                }

                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    lblerro.Visible = true;
                    myView.AddNew();

                    lblPageCount.Text = (objCount % GridView1.PageSize == 0 ? objCount / GridView1.PageSize : objCount / GridView1.PageSize + 1).ToString();
                }
                else
                {
                    lblerro.Visible = false;
                    lblPageCount.Text = (objCount % GridView1.PageSize == 0 ? objCount / GridView1.PageSize : objCount / GridView1.PageSize + 1).ToString();
                    cmdFirstPage.Enabled = true;
                    cmdPreview.Enabled = true;
                    cmdNext.Enabled = true;
                    cmdLastPage.Enabled = true;
                    if (lblcurPage.Text == "1")
                    {
                        cmdFirstPage.Enabled = false;
                        cmdPreview.Enabled = false;
                    }
                    else if (lblcurPage.Text == lblPageCount.Text)
                    {
                        cmdLastPage.Enabled = false;
                    }
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
        /// 绑定大区
        /// </summary>
        public void BindBigZone()
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
        /// <summary>
        /// 绑定战区
        /// </summary>
        /// <param name="id"></param>
        public void BindBattleZone(string id)
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
        #endregion

        
       
        protected void ExportExcel(object sender, EventArgs e)
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
                Common.MsgBox.Show(this, App_GlobalResources.Language.Tip_TimeError);
                return;
            }

            LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            if (DropDownListArea2.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            }
            DateTime searchdateB = DateTime.Now;
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
            if (!string.IsNullOrEmpty(tbEmailType.Text.Trim()))
            {
                sqlwhere += @" and Mail_TYPE=" + tbEmailType.Text.Trim();
            }
            if (!string.IsNullOrEmpty(tbRoleID.Text.Trim()))
            {
                sqlwhere += @" and Receiver_ID=" + tbRoleID.Text.Trim();
            }
            sqlwhere += " and Send_Time>\"" + Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-dd") + "\"";
            sqlwhere += " and Send_Time<\"" + Convert.ToDateTime(tboxTimeE.Text).AddDays(1).ToString("yyyy-MM-dd") + "\"";
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
            sql = "SELECT * FROM OPENQUERY ([LKSV_3_gsdata_db_" + bigZone + "_" + battleZone + "],'SELECT Mail_Content 邮件内容,Mail_TYPE 邮件类型,Receiver_ID 接收人角色ID,Send_Time 发送时间,Invalid_Time 失效时间,State_Modi_Time 操作时间,Mail_State 邮件状态,Money 金币,Money_Sliver 银币,TongBao 红钻,TongBei 蓝钻,Item1,Item2,Item3,Item4,Item5,IsDelete 是否删除 FROM (SELECT Mail_Content,Mail_TYPE,Receiver_ID,Send_Time,Invalid_Time,State_Modi_Time,Mail_State,Money,Money_Sliver,TongBao,TongBei,Item1,Item2,Item3,Item4,Item5,0 AS IsDelete FROM maillist_table WHERE Mail_TYPE <> 120 UNION ALL SELECT Mail_Content,Mail_TYPE,Receiver_ID,Send_Time,Invalid_Time,State_Modi_Time,Mail_State,Money,Money_Sliver,TongBao,TongBei,Item1,Item2,Item3,Item4,Item5,0 AS IsDelete FROM invalid_mail_table WHERE Mail_TYPE <> 120) A " + sqlwhere + " ORDER BY ITEM1 DESC limit " + (Convert.ToInt32(lblcurPage.Text) - 1) * 20 + ",20')";

            ds = DBHelperDigGameDB.Query(sql);

            ds.Tables[0].Columns.Add("道具1", typeof(string));
            ds.Tables[0].Columns.Add("数量1", typeof(string));
            ds.Tables[0].Columns.Add("道具2", typeof(string));
            ds.Tables[0].Columns.Add("数量2", typeof(string));
            ds.Tables[0].Columns.Add("道具3", typeof(string));
            ds.Tables[0].Columns.Add("数量3", typeof(string));
            ds.Tables[0].Columns.Add("道具4", typeof(string));
            ds.Tables[0].Columns.Add("数量4", typeof(string));
            ds.Tables[0].Columns.Add("道具5", typeof(string));
            ds.Tables[0].Columns.Add("数量5", typeof(string));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int item1 = 0;
                int itemBak1 = 0;
                byte[] btItem1 = (byte[])ds.Tables[0].Rows[i]["Item1"];
                if (btItem1.Length >= 8)
                {
                    item1 = BitConverter.ToInt32(btItem1, 0);
                    itemBak1 = BitConverter.ToInt32(btItem1, 4);
                }
                ds.Tables[0].Rows[i]["道具1"] = item1;
                ds.Tables[0].Rows[i]["数量1"] = itemBak1;

                int item2 = 0;
                int itemBak2 = 0;
                byte[] btItem2 = (byte[])ds.Tables[0].Rows[i]["Item2"];
                if (btItem2.Length >= 8)
                {
                    item2 = BitConverter.ToInt32(btItem2, 0);
                    itemBak2 = BitConverter.ToInt32(btItem2, 4);
                }
                ds.Tables[0].Rows[i]["道具2"] = item2;
                ds.Tables[0].Rows[i]["数量2"] = itemBak2;

                int item3 = 0;
                int itemBak3 = 0;
                byte[] btItem3 = (byte[])ds.Tables[0].Rows[i]["Item3"];
                if (btItem3.Length >= 8)
                {
                    item3 = BitConverter.ToInt32(btItem3, 0);
                    itemBak3 = BitConverter.ToInt32(btItem3, 4);
                }
                ds.Tables[0].Rows[i]["道具3"] = item3;
                ds.Tables[0].Rows[i]["数量3"] = itemBak3;

                int item4 = 0;
                int itemBak4 = 0;
                byte[] btItem4 = (byte[])ds.Tables[0].Rows[i]["Item4"];
                if (btItem4.Length >= 8)
                {
                    item4 = BitConverter.ToInt32(btItem4, 0);
                    itemBak4 = BitConverter.ToInt32(btItem4, 4);
                }
                ds.Tables[0].Rows[i]["道具4"] = item4;
                ds.Tables[0].Rows[i]["数量4"] = itemBak4;

                int item5 = 0;
                int itemBak5 = 0;
                byte[] btItem5 = (byte[])ds.Tables[0].Rows[i]["Item5"];
                if (btItem5.Length >= 8)
                {
                    item5 = BitConverter.ToInt32(btItem5, 0);
                    itemBak5 = BitConverter.ToInt32(btItem5, 4);
                }
                ds.Tables[0].Rows[i]["道具5"] = item5;
                ds.Tables[0].Rows[i]["数量5"] = itemBak5;
            }
            ds.Tables[0].Columns.Remove("Item1");
            ds.Tables[0].Columns.Remove("Item2");
            ds.Tables[0].Columns.Remove("Item3");
            ds.Tables[0].Columns.Remove("Item4");
            ds.Tables[0].Columns.Remove("Item5");
            
            ExportExcelHelper.ExportDataSet(ds);
        }
        
        #region 分页方法
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnF_Click(object sender, EventArgs e)
        {
            lblcurPage.Text = "1";
            bind();
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnP_Click(object sender, EventArgs e)
        {
            if (lblcurPage.Text != "1")
            {
                lblcurPage.Text = (Convert.ToInt32(lblcurPage.Text) - 1).ToString();
                bind();
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnN_Click(object sender, EventArgs e)
        {
            if (lblcurPage.Text != lblPageCount.Text)
            {
                lblcurPage.Text = (Convert.ToInt32(lblcurPage.Text) + 1).ToString();
                bind();
            }
        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnE_Click(object sender, EventArgs e)
        {
            lblcurPage.Text = lblPageCount.Text;
            bind();
        }
        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Go_Click(object sender, EventArgs e)
        {
            try
            {
                int pindex = Convert.ToInt32(lblcurPage.Text);
                if (pindex > 0 && pindex <= Convert.ToInt32(lblPageCount.Text))
                {
                    lblcurPage.Text = Convert.ToInt32(lblcurPage.Text).ToString();
                    bind();
                }
            }
            catch (System.Exception ex)
            {
            }
        }
        #endregion

        #region 文字转换
        public string GetItem(string item)
        {
            return Convert.ToInt32(item, 2) + "";
        }
        public string GetMailState(string mailState, string isDelete)
        {
            if (isDelete == "0")
            {
                string state = Convert.ToString(Convert.ToInt32(mailState), 2);
                state = state.Substring(state.Length - 1, 1);
                switch (state)
                {
                    case "0":
                        return "未领取";
                    case "1":
                        return "已领取";
                    default:
                        return state;
                }
            }
            else
            {
                return "";
            }
        }
        public string GetMailType(string value)
        {
            try
            {
                switch (value)
                {

                    case "2":
                        return "系统自动邮件";
                    case "3":
                        return "游戏运营邮件";
                    case "4":
                        return "系统预告";
                    case "5":
                        return "系统退信";
                    case "6":
                        return "来自的系统奖励来自数据库";
                    case "7":
                        return "来自的系统奖励来自配置";
                    case "8":
                        return "微博发奖";
                    case "9":
                        return "来自查档工具给的";
                    case "10":
                        return "来自查档工具恢复的";
                    case "11":
                        return "脚本接口给的";
                    case "12":
                        return "元宝交易行";
                    case "13":
                        return "元宝交易行返还的";
                    case "16":
                        return "玩家之间邮件";
                    case "17":
                        return "玩家之间邮件[聊天系统私密]";
                    case "18":
                        return "家族内部邮件";
                    case "19":
                        return "军团邮件";
                    case "51":
                        return "砸宝箱给的";
                    case "52":
                        return "提取元宝奖励";
                    case "53":
                        return "异常处理道具";
                    case "54":
                        return "玩家通过商城赠送";
                    case "55":
                        return "徒弟出师返还";
                    case "56":
                        return "祝福道具";
                    case "58":
                        return "GM发送的邮件";
                    case "59":
                        return "老玩家回归邮件";
                    case "60":
                        return "参与世界Boss发送";
                    case "61":
                        return "手游每日登录奖励";
                    case "62":
                        return "副本";
                    case "63":
                        return "成就";
                    case "64":
                        return "日常(传凯的)";
                    case "65":
                        return "每日必做(彦凯那个)";
                    case "66":
                        return "神位";
                    case "67":
                        return "联盟";
                    case "68":
                        return "副本翻牌";
                    case "69":
                        return "奥泰空间";
                    case "70":
                        return "十连抽奖";
                    case "71":
                        return "+-LongY[Zck] 拍卖行[模板相关]";
                    case "72":
                        return "+-LongY[Zck] 通用BOSS";
                    case "73":
                        return "公会福利";
                    case "74":
                        return "魔法熔炉奖励";
                    case "75":
                        return "魔法熔炉累积奖励";
                    case "76":
                        return "宠物保存属性返还道具";
                    case "77":
                        return "竞标未成功，返还公会资金";
                    case "78":
                        return "CDK";
                    case "79":
                        return "商城";
                    case "80":
                        return "+-LongY[Zck] 排行榜奖励：称号";
                    case "81":
                        return " 领地战胜利给称号";
                    case "82":
                        return " 领地产出资源";
                    case "83":
                        return " 直购";
                    default:
                        return value;
                }
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }
        public string GetIsDelete(string value)
        {
            switch (value)
            {
                case "0":
                    return "未删除";
                case "1":
                    return "已删除";
                default:
                    return value;
            }
        }
        #endregion
    }
}
