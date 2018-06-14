using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class RechargeLog : Admin_Page
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
                tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat);
                BindDdl1();
                BindDdl2("100001");
                bind();
                BindDepositResult();
            }
            ControlOutFile1.ControlOut = GridView1;
            ControlOutFile1.VisibleExcel = false;
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

            LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            if (DropDownListArea2.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            }
            if (DropDownListArea3.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea3.SelectedItem.Text;
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
            if (!string.IsNullOrEmpty(tbRoleId.Text.Trim()))
            {
                sqlwhere += @" and F_GlobalID=" + tbRoleId.Text.Trim();
            }
            if(!string.IsNullOrEmpty(txtTransactionID.Text.Trim()))
            {
                sqlwhere += @" and F_TransactionID='" + txtTransactionID.Text.Trim() + "'";
            }
            if(!string.IsNullOrEmpty(txtProductID.Text.Trim()))
            {
                sqlwhere += @" and F_ProductID=" + txtProductID.Text.Trim();
            }
            if(!string.IsNullOrEmpty(ddlDepositResult.SelectedValue+""))
            {
                sqlwhere += @" and F_DepositResult=" + ddlDepositResult.SelectedValue;
            }
            if (!string.IsNullOrEmpty(Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-dd")))
            {
                sqlwhere += @" and CONVERT(VARCHAR(100),F_InsertTime,23)>='" + Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-dd") + "'";
            }
            if (!string.IsNullOrEmpty(Convert.ToDateTime(tboxTimeE.Text).ToString("yyyy-MM-dd")))
            {
                sqlwhere += " and CONVERT(VARCHAR(100),F_InsertTime,23)<='" + Convert.ToDateTime(tboxTimeE.Text).ToString("yyyy-MM-dd") + "'";
            }
            //sqlwhere += @" and F_Date='" + searchdateB.ToString(SmallDateTimeFormat) + "'";
            //if (DropDownListArea1.SelectedIndex > 0)
            //{
            //    sqlwhere += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            //}
            //if (DropDownListArea2.SelectedIndex > 0)
            //{
            //    sqlwhere += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            //}
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
            sql = @"SELECT F_ID,F_GlobalID,F_UserID,F_TransactionID,F_WebRequestResult,F_ReturnCode,F_ReturnStr,F_ProductID,F_InsertTime,F_StoreName,F_Price,F_DepositResult 
FROM [LKSV_5_UserCoreDB_0_1].[UserCoreDB].[dbo].[T_Deposit_Verify_Result_Log] " + sqlwhere + " order by F_InsertTime desc";


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

                //GridView2.DataSource = myView;
                //GridView2.DataBind();

                //if (ControlChart1.Visible == true)
                //{
                //    ControlChart1.SetChart(GridView2, LabelTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, false, 0, 0);
                //}
            }
            catch (System.Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
                //lblerro.Text = sql;
            }

        }
        protected void ExportExcel(object sender,EventArgs e)
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
            if (DropDownListArea3.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea3.SelectedItem.Text;
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
            if (!string.IsNullOrEmpty(tbRoleId.Text.Trim()))
            {
                sqlwhere += @" and F_GlobalID=" + tbRoleId.Text.Trim();
            }
            if(!string.IsNullOrEmpty(txtTransactionID.Text.Trim()))
            {
                sqlwhere += @" and F_TransactionID='" + txtTransactionID.Text.Trim() + "'";
            }
            if(!string.IsNullOrEmpty(txtProductID.Text.Trim()))
            {
                sqlwhere += @" and F_ProductID=" + txtProductID.Text.Trim();
            }
            if(!string.IsNullOrEmpty(ddlDepositResult.SelectedValue+""))
            {
                sqlwhere += @" and F_DepositResult=" + ddlDepositResult.SelectedValue;
            }
            if (!string.IsNullOrEmpty(Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-dd")))
            {
                sqlwhere += @" and CONVERT(VARCHAR(100),F_InsertTime,23)>='" + Convert.ToDateTime(tboxTimeB.Text).ToString("yyyy-MM-dd") + "'";
            }
            if (!string.IsNullOrEmpty(Convert.ToDateTime(tboxTimeE.Text).ToString("yyyy-MM-dd")))
            {
                sqlwhere += " and CONVERT(VARCHAR(100),F_InsertTime,23)<='" + Convert.ToDateTime(tboxTimeE.Text).ToString("yyyy-MM-dd") + "'";
            }
            //sqlwhere += @" and F_Date='" + searchdateB.ToString(SmallDateTimeFormat) + "'";
            //if (DropDownListArea1.SelectedIndex > 0)
            //{
            //    sqlwhere += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            //}
            //if (DropDownListArea2.SelectedIndex > 0)
            //{
            //    sqlwhere += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            //}
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
            sql = @"SELECT F_GlobalID 角色编号,F_UserID 用户编号,F_TransactionID 订单号,F_ProductID 商品编号,F_Price 充值钱数,F_InsertTime 充值时间,F_StoreName 商店名,F_DepositResult 充值状态 
FROM [LKSV_5_UserCoreDB_0_1].[UserCoreDB].[dbo].[T_Deposit_Verify_Result_Log] ";


                ds = DBHelperDigGameDB.Query(sql);
                ExportExcelHelper.ExportDataSet(ds);
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



        public string TransDe(string value)
        {
            if (lblDecoding.Visible)
            {
                return CodingTran.Tran(lblDeType.Text, value);
            }
            else
            {
                return value;
            }
        }
        //得到角色名称
        public string GetRoleName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT F_RoleName FROM T_BaseRoleCreate with(nolock) where F_RoleID=" + value + "";
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
        public void BindDdl3(string id)
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = " + id + ") AND (F_IsUsed = 1)";

            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();

                newdr["F_Name"] = App_GlobalResources.Language.LblAllZoneLine;

                newdr["F_ValueGame"] = "";
                ds.Tables[0].Rows.InsertAt(newdr, 0);
                this.DropDownListArea3.DataSource = ds;
                this.DropDownListArea3.DataTextField = "F_Name";
                this.DropDownListArea3.DataValueField = "F_ValueGame";
                this.DropDownListArea3.DataBind();

            }
            catch (System.Exception ex)
            {
                DropDownListArea3.Items.Clear();
                DropDownListArea3.Items.Add(new ListItem(App_GlobalResources.Language.LblAllZoneLine, ""));
            }
        }
        public void BindDepositResult()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = "SELECT DISTINCT F_DepositResult FROM [LKSV_5_UserCoreDB_0_1].[UserCoreDB].[dbo].[T_Deposit_Verify_Result_Log]";

            try
            {
                ds = spg.Query(sql);
                DataRow newdr = ds.Tables[0].NewRow();

                ds.Tables[0].Rows.InsertAt(newdr, 0);
                this.ddlDepositResult.DataSource = ds;
                this.ddlDepositResult.DataTextField = "F_DepositResult";
                this.ddlDepositResult.DataValueField = "F_DepositResult";
                this.ddlDepositResult.Items.Insert(0, new ListItem("全部", ""));
                this.ddlDepositResult.DataBind();

            }
            catch (System.Exception ex)
            {
                ddlDepositResult.Items.Clear();
                ddlDepositResult.Items.Add(new ListItem("全部", ""));
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            System.Text.EncodingInfo[] ss = System.Text.Encoding.GetEncodings();
            ControlDateSelect1.SetSelectDate(tboxTimeB.Text);
            bind();
        }

        protected void DropDownListArea1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListArea2.Items.Clear();
            DropDownListArea3.Items.Clear();
            BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);
            BindDdl3(DropDownListArea2.SelectedValue.Split(',')[0]);

        }

        protected void DropDownListArea2_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListArea3.Items.Clear();
            BindDdl3(DropDownListArea2.SelectedValue.Split(',')[0]);

        }

        protected string GetCampName(string campID)
        {
            if (campID == "1")
            {
                return "联盟阵营";
            }
            else if (campID == "2")
            {
                return "混沌军团";
            }
            else
            {
                return "";
            }
        }
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
    }
}
