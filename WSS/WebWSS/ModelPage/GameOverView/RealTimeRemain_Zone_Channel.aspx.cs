using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.ModelPage.GameOverView
{
    public partial class RealTimeRemain_Zone_Channel : Admin_Page
    {
        #region 属性
        //获取DigGameDB连接字符串
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        //获取GssDB连接字符串
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        //DigGameDB
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        //GSSDB
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();
        //UserCoreDB
        DbHelperSQLP DBHelperUserCoreDB = new DbHelperSQLP();

        DataSet ds;
        #endregion

        #region 事件
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取DigGameDB连接字符串
            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            //获取GssDB连接字符串
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            //获取UserCoreDB连接字符串
            GetUserCoreDBString();
            if (!IsPostBack)
            {
                tboxTimeB.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                //绑定大区
                BindBigZone();
                //绑定列表数据
                bind();
                //绑定渠道
                BindChannel();
            }
            ControlOutFile1.ControlOut = GridView1;
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlMonthSelect1.SelectDateB.ToString("yyyy-MM-dd");
            tboxTimeE.Text = ControlMonthSelect1.SelectDateE.ToString("yyyy-MM-dd");
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
                ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, true, 0, 0);
            }
        }
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
            ControlMonthSelect1.SetSelectDate(tboxTimeB.Text, tboxTimeE.Text);
            bind();
        }
        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListArea2.Items.Clear();
            BindBattleZone(DropDownListArea1.SelectedValue.Split(',')[0]);
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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat) + " " + App_GlobalResources.Language.LblTo + " " + searchdateE.ToString(SmallDateTimeFormat);

            string sql = "";
            sql = @"SELECT * FROM T_RoleRemain where F_Date>='" + searchdateB.ToString(SmallDateTimeFormat) + "' and F_Date<='" + searchdateE.ToString(SmallDateTimeFormat) + "' ";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                sql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sql += @" and F_Zone=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }
            sql += " ORDER BY [F_Zone],[F_Date] DESC";

            try
            {
                ds = DBHelperDigGameDB.Query(sql);
                DataTable dt = ds.Tables[0];

                #region 增加留存数据计算
                //增加新列
                dt.Columns.Add("F_Remain2").SetOrdinal(dt.Columns.Count - 1);
                dt.Columns.Add("F_Remain3").SetOrdinal(dt.Columns.Count - 1);
                dt.Columns.Add("F_Remain4").SetOrdinal(dt.Columns.Count - 1);
                dt.Columns.Add("F_Remain5").SetOrdinal(dt.Columns.Count - 1);
                dt.Columns.Add("F_Remain6").SetOrdinal(dt.Columns.Count - 1);
                dt.Columns.Add("F_Remain7").SetOrdinal(dt.Columns.Count - 1);
                //加入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i + 1 >= dt.Rows.Count)//2日留存
                    {
                        dt.Rows[i]["F_Remain2"] = "-";
                    }
                    else
                    {
                        if (((int)dt.Rows[i + 1]["F_LoginDay1Num"] != 0) && (dt.Rows[i]["F_Zone"].ToString() == dt.Rows[i + 1]["F_Zone"].ToString()))
                        {
                            double percent = Math.Round((int)dt.Rows[i]["F_LoginDay2Num"] * 1.00 / (int)dt.Rows[i + 1]["F_LoginDay1Num"] * 100.0, 2);
                            dt.Rows[i]["F_Remain2"] = percent + "%";
                        }
                        else
                        {
                            dt.Rows[i]["F_Remain2"] = "-";
                        }
                    }

                    if (i + 2 >= dt.Rows.Count)//3日留存
                    {
                        dt.Rows[i]["F_Remain3"] = "-";
                    }
                    else
                    {
                        if ((int)dt.Rows[i + 2]["F_LoginDay1Num"] != 0 && dt.Rows[i]["F_Zone"].ToString() == dt.Rows[i + 2]["F_Zone"].ToString())
                        {
                            double percent1 = Math.Round((int)dt.Rows[i]["F_LoginDay3Num"] * 1.00 / (int)dt.Rows[i + 2]["F_LoginDay1Num"] * 100.0, 2);
                            dt.Rows[i]["F_Remain3"] = percent1 + "%";
                        }
                        else
                        {
                            dt.Rows[i]["F_Remain3"] = "-";
                        }
                    }

                    if (i + 3 >= dt.Rows.Count)//4日留存
                    {
                        dt.Rows[i]["F_Remain4"] = "-";
                    }
                    else
                    {
                        if ((int)dt.Rows[i + 3]["F_LoginDay1Num"] != 0 && dt.Rows[i]["F_Zone"].ToString() == dt.Rows[i + 3]["F_Zone"].ToString())
                        {
                            double percent1 = Math.Round((int)dt.Rows[i]["F_LoginDay4Num"] * 1.00 / (int)dt.Rows[i + 3]["F_LoginDay1Num"] * 100.0, 2);
                            dt.Rows[i]["F_Remain4"] = percent1 + "%";
                        }
                        else
                        {
                            dt.Rows[i]["F_Remain4"] = "-";
                        }
                    }

                    if (i + 4 >= dt.Rows.Count)//5日留存
                    {
                        dt.Rows[i]["F_Remain5"] = "-";
                    }
                    else
                    {
                        if ((int)dt.Rows[i + 4]["F_LoginDay1Num"] != 0 && dt.Rows[i]["F_Zone"].ToString() == dt.Rows[i + 4]["F_Zone"].ToString())
                        {
                            double percent2 = Math.Round((int)dt.Rows[i]["F_LoginDay5Num"] * 1.00 / (int)dt.Rows[i + 4]["F_LoginDay1Num"] * 100.0, 2);
                            dt.Rows[i]["F_Remain5"] = percent2 + "%";
                        }
                        else
                        {
                            dt.Rows[i]["F_Remain5"] = "-";
                        }
                    }

                    if (i + 5 >= dt.Rows.Count)//6日留存
                    {
                        dt.Rows[i]["F_Remain6"] = "-";
                    }
                    else
                    {
                        if ((int)dt.Rows[i + 5]["F_LoginDay1Num"] != 0 && dt.Rows[i]["F_Zone"].ToString() == dt.Rows[i + 5]["F_Zone"].ToString())
                        {
                            double percent2 = Math.Round((int)dt.Rows[i]["F_LoginDay6Num"] * 1.00 / (int)dt.Rows[i + 5]["F_LoginDay1Num"] * 100.0, 2);
                            dt.Rows[i]["F_Remain6"] = percent2 + "%";
                        }
                        else
                        {
                            dt.Rows[i]["F_Remain6"] = "-";
                        }
                    }

                    if (i + 6 >= dt.Rows.Count)
                    {
                        dt.Rows[i]["F_Remain7"] = "-";
                    }
                    else
                    {
                        if ((int)dt.Rows[i + 6]["F_LoginDay1Num"] != 0 && dt.Rows[i]["F_Zone"].ToString() == dt.Rows[i + 6]["F_Zone"].ToString())
                        {
                            double percent3 = Math.Round((int)dt.Rows[i]["F_LoginDay7Num"] * 1.00 / (int)dt.Rows[i + 6]["F_LoginDay1Num"] * 100.0, 2);
                            dt.Rows[i]["F_Remain7"] = percent3 + "%";
                        }
                        else
                        {
                            dt.Rows[i]["F_Remain7"] = "-";
                        }
                    }
                }
                #endregion

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

                if (ControlChart1.Visible == true)
                {
                    ControlChart1.SetChart(GridView1, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, true, 0, 0);
                }
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
        /// 获取UserCoreDB连接字符串
        /// </summary>
        private void GetUserCoreDBString()
        {
            string sql = @"SELECT TOP 1 provider_string FROM sys.servers WHERE product='UserCoreDB'";
            string conn = DBHelperDigGameDB.GetSingle(sql).ToString();
            DBHelperUserCoreDB.connectionString = conn;
        }
        /// <summary>
        /// 绑定渠道
        /// </summary>
        public void BindChannel()
        {
            string sql = "SELECT DISTINCT F_ChannelID FROM T_Deposit_Verify_Result_Log ORDER BY F_ChannelID ";
            try
            {
                ds = DBHelperUserCoreDB.Query(sql);
                this.ddlChannel.DataSource = ds;
                this.ddlChannel.DataTextField = "F_ChannelID";
                this.ddlChannel.DataValueField = "F_ChannelID";
                this.ddlChannel.DataBind();
                this.ddlChannel.Items.Insert(0, new ListItem("全部", ""));
            }
            catch (System.Exception ex)
            {
                ddlChannel.Items.Clear();
                ddlChannel.Items.Add(new ListItem("", ""));
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
    }
}
