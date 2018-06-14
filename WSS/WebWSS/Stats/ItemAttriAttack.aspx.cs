using System;
using System.Data;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;

namespace WebWSS.Stats
{
    public partial class ItemAttriAttack : Admin_Page
    {
        string ConnStrDigGameDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        DbHelperSQLP DBHelperDigGameDB = new DbHelperSQLP();
        DbHelperSQLP DBHelperGSSDB = new DbHelperSQLP();

        DataSet dsMS;
        DataSet dsMy;
        DataSet dsTemp;
        protected void Page_Load(object sender, EventArgs e)
        {

            DBHelperDigGameDB.connectionString = ConnStrDigGameDB;
            DBHelperGSSDB.connectionString = ConnStrGSSDB;
            ControlMonthSelect1.SelectDateChanged += new EventHandler(ControlDateSelect_SelectDateChanged);
            if (!IsPostBack)
            {
                tboxTimeB.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                BindDdl1();
                BindDdl2(DropDownListArea1.SelectedValue.Split(',')[0]);
                bind();
                
            }
            ControlOutFile1.ControlOut = GridView1;
        }
        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlMonthSelect1.SelectDateB.ToString("yyyy-MM-dd");
            tboxTimeE.Text = ControlMonthSelect1.SelectDateE.ToString("yyyy-MM-dd");
            bind();
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
            LabelTime.Text = searchdateB.ToString(SmallDateTimeFormat)   +  " "+ App_GlobalResources.Language.LblTo+" "  + searchdateE.ToString(SmallDateTimeFormat)  ;

            string sql = "";
            sql = @"SELECT F_SuitName,F_StarLevel,F_EquipType,sum(F_ItemCount) as F_Num FROM T_EquipLevelVacation where F_SuitName<>'精炼' and F_Date>='" + searchdateB.ToString(SmallDateTimeFormat) + "' and F_Date<='" + searchdateE.ToString(SmallDateTimeFormat) + "' ";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                sql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sql += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }
            sql += @" group by  F_SuitName,F_StarLevel,F_EquipType";

            try
            {
                dsMS = DBHelperDigGameDB.Query(sql);

                string sqlMy = "select 1";//where OPID=50087
                string bigzoneid = DropDownListArea1.SelectedValue.Split(',')[1];
                string zoneid = DropDownListArea2.SelectedValue.Split(',')[1];

                SqlParameter[] parameters = {
					new SqlParameter("@BigZoneID", SqlDbType.Int),
					new SqlParameter("@ZoneID", SqlDbType.Int),
                    new SqlParameter("@DBType", SqlDbType.Int),
					new SqlParameter("@Query",SqlDbType.NVarChar),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PCount", SqlDbType.Int),
					};
                parameters[0].Value = bigzoneid;
                parameters[1].Value = zoneid;
                parameters[2].Value = 0;//GSLOG_DB
                parameters[3].Value = sqlMy.Replace("'", "''");
                parameters[4].Value = 1;
                parameters[5].Value = 1000;
                parameters[6].Direction = ParameterDirection.Output;

                dsMy = DBHelperDigGameDB.RunProcedure("_Query_SQLCustom", parameters, "dsMy");



                List<ItemTemp> datalist = new List<ItemTemp>();
                datalist.Add(new ItemTemp("第一套", "1星"));
                datalist.Add(new ItemTemp("第一套", "2星"));
                datalist.Add(new ItemTemp("第一套", "3星"));

                datalist.Add(new ItemTemp("第二套", "2星"));
                datalist.Add(new ItemTemp("第二套", "3星"));
                datalist.Add(new ItemTemp("第二套", "4星"));

                datalist.Add(new ItemTemp("第三套", "3星"));
                datalist.Add(new ItemTemp("第三套", "4星"));
                datalist.Add(new ItemTemp("第三套", "5星"));

                datalist.Add(new ItemTemp("第四套", "4星"));
                datalist.Add(new ItemTemp("第四套", "5星"));
                datalist.Add(new ItemTemp("第四套", "6星"));

                datalist.Add(new ItemTemp("第五套", "5星"));
                datalist.Add(new ItemTemp("第五套", "6星"));
                datalist.Add(new ItemTemp("第五套", "7星"));

                datalist.Add(new ItemTemp("第六套", "6星"));
                datalist.Add(new ItemTemp("第六套", "7星"));
                datalist.Add(new ItemTemp("第六套", "8星"));

                datalist.Add(new ItemTemp("第七套", "6星"));
                datalist.Add(new ItemTemp("第七套", "7星"));
                datalist.Add(new ItemTemp("第七套", "8星"));

                GridView1.DataSource = datalist;
                GridView1.DataBind();

                if (ControlChart1.Visible == true)
                {
                    ControlChart1.SetChart(GridView1, lblTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, true, 0, 0);
                }
                lblerro.Visible = false;
            }
            catch (System.Exception ex)
            {
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
            }

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

                ControlChart1.SetChart(GridView1, lblTitle.Text.Replace(">>", " - ") + "  " + LabelArea.Text + " " + LabelTime.Text, ControlChartSelect1.State, true, 0, 0);
            }
        }

        public void BindDdl1()
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;

            string sql = @"SELECT F_ID, F_ParentID, F_Name, F_Value, cast(F_ID as varchar(20))+','+F_ValueGame as F_ValueGame, F_IsUsed, F_Sort FROM T_GameConfig WHERE (F_ParentID = 1000) AND (F_IsUsed = 1)";

            try
            {
                dsTemp = spg.Query(sql);
                //DataRow newdr = dsTemp.Tables[0].NewRow();
                //newdr["F_Name"] = App_GlobalResources.Language.LblAllBigZone;
                //newdr["F_ValueGame"] = "";
                //dsTemp.Tables[0].Rows.InsertAt(newdr, 0);
                this.DropDownListArea1.DataSource = dsTemp;
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
                dsTemp = spg.Query(sql);
                //DataRow newdr = dsTemp.Tables[0].NewRow();
                //newdr["F_Name"] = App_GlobalResources.Language.LblAllZone;
                //newdr["F_ValueGame"] = "";
                //dsTemp.Tables[0].Rows.InsertAt(newdr, 0);
                this.DropDownListArea2.DataSource = dsTemp;
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
                //dsTemp = spg.Query(sql);
                //DataRow newdr = dsTemp.Tables[0].NewRow();
                //newdr["F_Name"] = "所有战线";
                //newdr["F_ValueGame"] = "";
                //dsTemp.Tables[0].Rows.InsertAt(newdr, 0);
                //this.DropDownListArea3.DataSource = dsTemp;
                //this.DropDownListArea3.DataTextField = "F_Name";
                //this.DropDownListArea3.DataValueField = "F_ValueGame";
                //this.DropDownListArea3.DataBind();

            }
            catch (System.Exception ex)
            {
                DropDownListArea3.Items.Clear();
                DropDownListArea3.Items.Add(new ListItem("所有战线", ""));
            }
        }

        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
        Dictionary<int, int> dic = new Dictionary<int, int>();
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");

                    if (e.Row.RowIndex % 3 == 0)
                    {
                        e.Row.Cells[0].RowSpan = 3;
                    }
                    else
                    {
                        e.Row.Cells[0].Visible = false;
                    }
                    e.Row.Cells[1].BackColor = GetItemColor(e.Row.Cells[1].Text);

                    for (int i = 2; i < 17; i++)
                    {

                        if ((i - 2) % 3 == 2)
                        {
                            e.Row.Cells[i].Text = "0.0%";
                            continue;
                        }
                        else if ((i - 2) % 3 == 0)
                        {
                            string part = GetItemPartName(i);
                            e.Row.Cells[i].Text = GetTotalNum(e.Row.Cells[0].Text, e.Row.Cells[1].Text, part);
                        }
                        else if ((i - 2) % 3 == 1)
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                        int num = Convert.ToInt32(e.Row.Cells[i].Text);
                        if (dic.ContainsKey(i))
                        {
                            dic[i] += num;
                        }
                        else
                        {
                            dic.Add(i, num);
                        }
                    }

                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {

                    e.Row.Cells[0].Text = "总";
                    e.Row.Cells[1].Text = "计:";


                    for (int i = 2; i < 17; i++)
                    {
                        if ((i - 2) % 3 == 2)
                        {
                            e.Row.Cells[i].Text = "0.0%";
                            continue;
                        }
                        e.Row.Cells[i].Text = dic[i].ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
                lblDebug.Text = ex.Message;
            }
        }
        /// <summary>
        /// 得到部位名称
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static string GetItemPartName(int i)
        {
            string part = "武器";
            switch ((i-2)/3)
            {
                case 0:
                    part = "武器";
                    break;
                case 1:
                    part = "副手";
                    break;
                case 2:
                    part = "项链";
                    break;
                case 3:
                    part = "手镯";
                    break;
                case 4:
                    part = "戒指";
                    break;
            }
            return part;
        }
        private string GetTotalNum(string itemSuit,string itemColor,string itemPart)
        {
            if (dsMS==null)
            {
                return "0";
            }
            else
            {
                string whereStr=string.Format("F_SuitName='{0}' and F_StarLevel='{1}' and F_EquipType='{2}'",itemSuit,itemColor,itemPart);
                DataRow[] drs= dsMS.Tables[0].Select(whereStr);
                if (drs.Length==0)
                {
                    return "0";
                }
                else
                return drs[0]["F_Num"].ToString();

            }
        }
        /// <summary>
        /// 得到装备品质颜色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Color GetItemColor(string value)
        {
            switch (value.Replace("星",""))
            {
                case "1":
                    return Color.Gainsboro;
                case "2":
                    return Color.White;
                case "3":
                    return Color.LawnGreen;
                case "4":
                    return Color.DodgerBlue;
                case "5":
                    return Color.Yellow;
                case "6":
                    return Color.Orange;
                case "7":
                    return Color.Red;
                case "8":
                    return Color.DarkViolet;
                default:
                    return Color.Black;
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
            ControlMonthSelect1.SetSelectDate(tboxTimeB.Text, tboxTimeE.Text);
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

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    //第一行表头
                    TableCellCollection tcHeader = e.Row.Cells;
                    tcHeader.Clear();

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[0].Attributes.Add("rowspan", "2");
                    tcHeader[0].Text = "装备系统";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[1].Attributes.Add("rowspan", "2");
                    tcHeader[1].Text = "装备品质";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[2].Attributes.Add("colspan", "3");
                    tcHeader[2].Text = "武器";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[3].Attributes.Add("colspan", "3");
                    tcHeader[3].Text = "副手";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[4].Attributes.Add("colspan", "3");
                    tcHeader[4].Text = "项链";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[5].Attributes.Add("colspan", "3");
                    tcHeader[5].Text = "手镯";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[6].Attributes.Add("colspan", "3");
                    tcHeader[6].Text = "戒指</th></tr><tr style='color:White;background-color:#006699;font-size:12px;font-weight:bold;'>";

                    //第二行表头
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[7].Text = "总产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[8].Text = "极品产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[9].Text = "所占比率";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[10].Text = "总产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[11].Text = "极品产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[12].Text = "所占比率";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[13].Text = "总产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[14].Text = "极品产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[15].Text = "所占比率";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[16].Text = "总产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[17].Text = "极品产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[18].Text = "所占比率";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[19].Text = "总产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[20].Text = "极品产出量";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[21].Text = "所占比率";
                    break;

                //case DataControlRowType.DataRow:
                //    TableCellCollection tcRow = e.Row.Cells;

                //    break;
            }
        }

    }

    public class ItemTemp
    {
        private string str1;

        public string Str1
        {
            get { return str1; }
            set { str1 = value; }
        }
        private string str2;

        public string Str2
        {
            get { return str2; }
            set { str2 = value; }
        }
        public ItemTemp(string a, string b)
        {
            str1 = a;
            str2 = b;
        }

    }
}
