using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WSS.DBUtility;

namespace WSS.Web.AdminF.Stats
{
    public partial class UserLoginLose : System.Web.UI.Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortOrder"] = "F_ID";
                ViewState["OrderDire"] = "ASC";
                BindDdl1();
                if (Request["Area1"] != null && Request["Area1"] != string.Empty)
                {
                    DropDownListArea1.SelectedValue = Request["Area1"];
                    DropDownListArea1_SelectedIndexChanged(null, null);
                    if (Request["Area2"] != null && Request["Area2"] != string.Empty)
                    {
                        DropDownListArea3.Visible = true;
                        DropDownListArea2.SelectedValue = Request["Area2"];
                        DropDownListArea2_SelectedIndexChanged(null, null);
                        if (Request["Area3"] != null && Request["Area3"] != string.Empty)
                        {
                            DropDownListArea3.SelectedValue = Request["Area3"];
                        }
                    }
                    else
                    {
                        DropDownListArea2.Visible = true;
                        DropDownListArea2.AutoPostBack = false;
                    }
                }
                else
                {
                    DropDownListArea1.Visible = true;
                    DropDownListArea1.AutoPostBack = false;
                }

                if (Request["IsNow"] != null && Request["IsNow"] != string.Empty)
                {
                    LabelSTime.Visible = false;
                    DropDownListYear.Visible = false;
                    DropDownListMonth.Visible = false;
                    DropDownListDay.Visible = false;
                }

                bind();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {

            //if ()
            //{
            //}

            LabelArea.Text = DropDownListArea1.SelectedItem.Text;
            if (DropDownListArea2.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea2.SelectedItem.Text;
            }
            if (DropDownListArea3.SelectedIndex > 0)
            {
                LabelArea.Text += "|" + DropDownListArea3.SelectedItem.Text;
            }

            if (LabelSTime.Visible)
            {
                LabelTime.Text = DropDownListYear.SelectedItem.Text + "年" + DropDownListMonth.SelectedItem.Text + "月";
                if (DropDownListDay.SelectedIndex > 0)
                {
                    LabelTime.Text += DropDownListDay.SelectedItem.Text + "日";
                }
            }
            else
            {
                LabelTime.Text = DateTime.Now.ToString("yyyy-MM--dd hh:mm");
            }






            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStrDigGameDB;




            string sql = @"SELECT F_ID,F_Year, F_Month, F_Day, F_UserNum,  F_NewUserNum, F_NewActivationNum, F_NewLoginNum, F_ActiveUserNum, F_NewActiveUserNum, F_LoseUserNum,F_NewLoseUserNum, F_ReturnUserNum, F_NewReturnUserNum FROM T_UserBaseDig_Day WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='2011-1-1'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='2012-10-10'";

            try
            {
                ds = sp.Query(sql);
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
                string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
                myView.Sort = sort;
                GridView1.DataSource = myView;
                GridView1.DataKeyNames = new string[] { "F_ID" };
                GridView1.DataBind();
            }
            catch (System.Exception ex)
            {
                lblerro.Visible = true;
            }



            sql = @"SELECT top 1 F_ID,F_Year, F_Month, F_Day, F_UserNum,  F_NewUserNum, F_NewActivationNum, F_NewLoginNum, F_ActiveUserNum, F_NewActiveUserNum, F_LoseUserNum,F_NewLoseUserNum, F_ReturnUserNum, F_NewReturnUserNum FROM T_UserBaseDig_Day WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='2011-1-1'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='2012-10-10'";

            try
            {
                ds = sp.Query(sql);
                DataView myView = ds.Tables[0].DefaultView;
                if (myView.Count == 0)
                {
                    lblerro2.Visible = true;
                    myView.AddNew();
                }
                else
                {
                    lblerro2.Visible = false;
                }
                string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
                myView.Sort = sort;
                GridView2.DataSource = myView;
                GridView2.DataKeyNames = new string[] { "F_ID" };
                GridView2.DataBind();
            }
            catch (System.Exception ex)
            {
                lblerro2.Visible = true;
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
                newdr["F_Name"] = "所有大区";
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
                DropDownListArea1.Items.Add(new ListItem("所有大区", ""));
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
                if (DropDownListArea1.SelectedIndex > 0)
                {
                    newdr["F_Name"] = DropDownListArea1.SelectedItem.Text;
                }
                else
                {
                    newdr["F_Name"] = "所有战区";
                }

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
                DropDownListArea2.Items.Add(new ListItem("所有战区", ""));
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
                if (DropDownListArea2.SelectedIndex > 0)
                {
                    newdr["F_Name"] = DropDownListArea2.SelectedItem.Text;
                }
                else
                {
                    newdr["F_Name"] = "所有战线";
                }
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
                DropDownListArea3.Items.Add(new ListItem("所有战线", ""));
            }
        }


        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。此事件通常用于在某个行被绑定到数据时修改该行的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-color:#cccccc";
            }
        }
        /// <summary>
        /// 在单击某个用于对列进行排序的超链接时发生，但在 GridView 控件执行排序操作之前。此事件通常用于取消排序操作或执行自定义的排序例程。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        protected void DropDownListNavi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListNavi.SelectedIndex>0)
            {
                Response.Redirect("UserOnLine.aspx?isnow=1");
            }
        }

    }
}
