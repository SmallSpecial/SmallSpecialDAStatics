using System;
using System.Data;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class UserOnLine : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
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
                if (Request["BackUrl"] == null)
                {
                    ButtonBack.Visible = false;
                }
                else
                {
                    LabelTitle.Text = "历史在线人数";
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


            string yearStr = "";



            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStrDigGameDB;

            //select * from (select F_BigZone=0,F_BigZoneName='ssss' union select 1,'ss') as a left outer join (SELECT max(F_BigZone) as F_BigZone, sum(F_OnlineNum) as F_OnlineNum, sum(F_OnlineIpNum) as F_OnlineIpNum FROM T_GameOnlineBaseDig_ZoneLine WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='2011-12-5'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='2011-12-5' group by F_BigZone) as b on a.F_BigZone=b.F_BigZone

            string sql1 = "";
            string sql2 = "";
            if (DropDownListArea1.SelectedIndex==0)
            {
                sql1 = @"SELECT max(F_ID) as F_ID, max(F_BigZone) as F_ZoneStr, sum(F_OnlineNum) as F_OnlineNum, sum(F_OnlineIpNum) as F_OnlineIpNum FROM T_GameOnlineBaseDig_ZoneLine WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='2011-12-5'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='2011-12-5' group by F_BigZone";
                sql2 = @"SELECT max(F_ID) as F_ID,cast(max(F_BigZone) as varchar(10))+'|'+cast(max(F_ZoneID) as varchar(10)) as F_ZoneStr, sum(F_OnlineNum) as F_OnlineNum, sum(F_OnlineIpNum) as F_OnlineIpNum FROM T_GameOnlineBaseDig_ZoneLine WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='2011-12-5'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='2011-12-5' group by F_BigZone,F_ZoneID";
            }
            else
            {
                sql1 = @"SELECT max(F_ID) as F_ID,cast(max(F_BigZone) as varchar(10))+'|'+cast(max(F_ZoneID) as varchar(10)) as F_ZoneStr, sum(F_OnlineNum) as F_OnlineNum, sum(F_OnlineIpNum) as F_OnlineIpNum FROM T_GameOnlineBaseDig_ZoneLine WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='2011-12-5'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='2011-12-5' and F_BigZone="+DropDownListArea1.SelectedValue.Split(',')[1]+" group by F_BigZone,F_ZoneID";
                sql2 = @"SELECT max(F_ID) as F_ID,cast(max(F_BigZone) as varchar(10))+'|'+cast(max(F_ZoneID) as varchar(10))+'|'+cast(max(F_LoginNGSID) as varchar(10)) as F_ZoneStr, sum(F_OnlineNum) as F_OnlineNum, sum(F_OnlineIpNum) as F_OnlineIpNum FROM T_GameOnlineBaseDig_ZoneLine WHERE  1=1 and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='2011-12-5'  and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) <='2011-12-5' and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "  group by F_BigZone,F_ZoneID,F_LoginNGSID";
            }


            try
            {
                ds = sp.Query(sql1);
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
                GridView1.DataSource = myView;
                GridView1.DataKeyNames = new string[] { "F_ZoneStr" };
                GridView1.DataBind();
            }
            catch (System.Exception ex)
            {
                lblerro2.Visible = true;
            }





            try
            {
                ds = sp.Query(sql2);
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
                GridView2.DataSource = myView;
                GridView2.DataKeyNames = new string[] { "F_ID" };
                GridView2.DataBind();
            }
            catch (System.Exception ex)
            {
                lblerro.Visible = true;
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
                if (DropDownListArea1.SelectedIndex > 0)
                {
                    newdr["F_Name"] = DropDownListArea1.SelectedItem.Text;
                }
                else
                {
                    newdr["F_Name"] = App_GlobalResources.Language.LblAllZone;
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

        //得到用区域名称
        public string GetZoneNameFull(object a)
        {
            try
            {
                string zonename = GetZoneName(a);
               string[] zones = a.ToString().Split('|');
                if (zones.Length==2)
                {
                    zonename += "(" + GetZoneName(zones[0]) + ")";
                }
                else if (zones.Length==3)
                {
                    zonename += "(" + GetZoneName(zones[0]) + ">>" + GetZoneName(zones[0]+"|"+zones[1]) + ")";
                }
                return zonename;
            }
            catch (System.Exception ex)
            {
            	return "";
            }
        }
        public string GetZoneName(object a)
        {
            try
            {
                string sql = "";
                string[] zones = a.ToString().Split('|');
                if (zones.Length == 1)
                {
                    sql = @"SELECT  F_Name FROM T_GameConfig WHERE (F_ParentID = 1000) and F_ValueGame='" + a.ToString() + "'";
                }
                else if(zones.Length==2)
                {
                    sql = @"SELECT  F_Name FROM T_GameConfig WHERE (F_ParentID = (select F_ID from T_GameConfig where F_ValueGame='" + zones[0] + "' and F_ParentID=1000 )) and F_ValueGame='" + zones[1] + "'";
                }
                else if (zones.Length==3)
                {
                    sql = @"SELECT  F_Name FROM T_GameConfig WHERE (F_ParentID = (SELECT  F_ID FROM T_GameConfig WHERE (F_ParentID = (select F_ID from T_GameConfig where F_ValueGame='"+zones[0]+"' and F_ParentID=1000 )) AND F_ValueGame='"+zones[1]+"')) and F_ValueGame='"+zones[2]+"'";
                }
                else
                {
                    return "";
                }
                return DBHelperGSSDB.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 得到比例值
        /// </summary>
        public string GetScale(object a, object b)
        {
            try
            {
                //if (Convert.ToInt32(b)==0)
                //{
                //    return "";
                //}
                double scale = Convert.ToDouble(a) / Convert.ToDouble(b) * 100;
                return string.Format("{0,25:N2}%", scale).Replace("非数字", "100");
            }
            catch (System.Exception ex)
            {
                return "";
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

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserOnLineHistory.aspx");
        }
        protected void DropDownListNavi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListNavi.SelectedIndex > 0)
            {
                Response.Redirect("UserOnLineHistory.aspx");
            }
        }
    }
}
