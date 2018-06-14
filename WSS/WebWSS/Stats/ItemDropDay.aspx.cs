using System;
using System.Data;
using System.Web.UI.WebControls;
using WSS.DBUtility;

namespace WebWSS.Stats
{
    public partial class ItemDropDay : Admin_Page
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
            ControlMonthSelect1.SelectDateChanged += new EventHandler(ControlDateSelect_SelectDateChanged);
            if (!IsPostBack)
            {
                if (Request["isnow"] != null && Request["isnow"] == "1")
                {
                    tboxTimeB.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                    tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                    LabelTitle.Text = " 当天掉落统计(装备)[定时更新]";
                    tboxTimeB.Enabled = false;
                    tboxTimeE.Enabled = false;
                }
                else
                {
                    tboxTimeB.Text = string.Format("{0:yyyy-MM-01}",DateTime.Now) ;
                    tboxTimeE.Text = DateTime.Now.ToString(SmallDateTimeFormat)  ;
                    LabelTitle.Text = " 历史掉落统计(装备)";
                }

                BindDdl1();
                bind();
                bind2(GridView2, "5星");
                bind2(GridView3, "6星");
                bind2(GridView4, "7星");
                bind2(GridView5, "8星");
                bind2(GridView6, "9星");

                bind2(GridView7, "3星");
                bind2(GridView8, "4星");
            }
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
            sql = @"
WITH T_EquipStarVacation_CTE (F_Vocation, F_StarLevel, F_ItemCount)
AS
(
SELECT      F_Vocation, F_StarLevel, F_ItemCount
FROM         T_EquipStarVacation  where cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='" + searchdateB.ToString(SmallDateTimeFormat) + @"' and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime)<='" + searchdateE.ToString(SmallDateTimeFormat) + @"' ";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                sql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sql += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }

            sql += @")
select '" + searchdateB.ToString(SmallDateTimeFormat) +  " "+ App_GlobalResources.Language.LblTo+" "  + searchdateE.ToString(SmallDateTimeFormat) + @"' as 日期, 
case F_StarLevel when '3星' then '绿色(3星)' when '4星' then '蓝色(4星)' when '5星' then '黄色(5星)' when '6星' then '橙色(6星)' when '7星' then '红色(7星)' when '8星' then '紫色(8星)' else '未知' end +'装备' as 装备星级, 
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel and left(F_Vocation,2)='虎贲') as 虎贲,
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel and left(F_Vocation,2)='浪人') as 浪人,
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel and left(F_Vocation,2)='龙胆') as 龙胆,
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel and left(F_Vocation,2)='巧工') as 巧工,
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel and left(F_Vocation,3)='斗仙') as 斗仙,
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel and left(F_Vocation,2)='花灵') as 花灵,
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel and left(F_Vocation,2)='天师') as 天师,
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel and left(F_Vocation,2)='行者') as 行者,
(select isnull(sum(F_ItemCount),0) from T_EquipStarVacation_CTE where F_StarLevel=a.F_StarLevel ) as 总计
from (select '3星' as F_StarLevel
union all
select '4星'
union all
select '5星' as F_StarLevel
union all
select '6星'
union all
select '7星'
union all
select '8星') as a  ";


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
            }

        }

        protected void ControlDateSelect_SelectDateChanged(object sender, EventArgs e)
        {
            tboxTimeB.Text = ControlMonthSelect1.SelectDateB.ToString("yyyy-MM-dd");
            tboxTimeE.Text = ControlMonthSelect1.SelectDateE.ToString("yyyy-MM-dd");
            bind();
            bind2(GridView2, "5星");
            bind2(GridView3, "6星");
            bind2(GridView4, "7星");
            bind2(GridView5, "8星");
            bind2(GridView6, "9星");

            bind2(GridView7, "3星");
            bind2(GridView8, "4星");
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind2(GridView gv, string eqtype)
        {
            if (!Common.Validate.IsDateTime(tboxTimeB.Text) || !Common.Validate.IsDateTime(tboxTimeE.Text))
            {
 
                return;
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


            string sql = "";
            sql = @"
WITH T_EquipLevelVacation_CTE (F_SuitName, F_EquipType, F_ItemCount)
AS
(
SELECT      F_SuitName, F_EquipType, F_ItemCount
FROM         T_EquipLevelVacation  where cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime) >='" + searchdateB.ToString(SmallDateTimeFormat) + @"' and cast(cast(F_Year as varchar(4))+'-'+cast(F_Month as varchar(2))+'-'+cast(F_Day as varchar(2)) as datetime)<='" + searchdateE.ToString(SmallDateTimeFormat) + @"' ";
            if (DropDownListArea1.SelectedIndex > 0)
            {
                sql += @" and F_BigZone=" + DropDownListArea1.SelectedValue.Split(',')[1] + "";
            }
            if (DropDownListArea2.SelectedIndex > 0)
            {
                sql += @" and F_ZoneID=" + DropDownListArea2.SelectedValue.Split(',')[1] + "";
            }
            if (eqtype.Length > 0)
            {
                sql += @" and F_StarLevel='" + eqtype + "'";
            }
            if (DropDownListVocation.SelectedIndex > 0)
            {
                sql += @" and left(F_Vocation,2)='" + DropDownListVocation.SelectedValue + "'";
            }

            sql += @")
select EqType as 装备类别, 
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第一套') as 第一套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第二套') as 第二套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第三套') as 第三套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第四套') as 第四套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第五套') as 第五套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第六套') as 第六套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第七套') as 第七套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第八套') as 第八套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第九套') as 第九套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第十套') as 第十套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第十一套') as 第十一套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第十二套') as 第十二套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType and F_SuitName='第十三套') as 第十三套,
(select isnull(sum(F_ItemCount),0) from T_EquipLevelVacation_CTE where F_EquipType=a.EqType ) as 总计
from (select '头盔' as EqType
union all
select '衣服'
union all
select '护手'
union all
select '腰带'
union all
select '鞋子'
union all
select '武器'
union all
select '副手'
union all
select '坐骑'
union all
select '首饰'
union all
select '时装'
) as a  ";
            










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
                gv.DataSource = myView;
                gv.DataBind();


            }
            catch (System.Exception ex)
            {

                gv.DataSource = null;
                gv.DataBind();
                lblerro.Visible = true;
                lblerro.Text = App_GlobalResources.Language.LblError + ex.Message;
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

                newdr["F_Name"] = "所有战线";

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

        long sumi2 = 0;
        long sumi3 = 0;
        long sumi4 = 0;
        long sumi5 = 0;
        long sumi6 = 0;
        long sumi7 = 0;
        long sumi8 = 0;
        long sumi9 = 0;
        long sumi10 = 0;
        /// <sumimary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </sumimary>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");
                }
                if (e.Row.RowIndex >= 0 && !lblerro.Visible)
                {

                    sumi2 += Convert.ToInt64(e.Row.Cells[2].Text);
                    sumi3 += Convert.ToInt64(e.Row.Cells[3].Text);
                    sumi4 += Convert.ToInt64(e.Row.Cells[4].Text);
                    sumi5 += Convert.ToInt64(e.Row.Cells[5].Text);
                    sumi6 += Convert.ToInt64(e.Row.Cells[6].Text);
                    sumi7 += Convert.ToInt64(e.Row.Cells[7].Text);
                    sumi8 += Convert.ToInt64(e.Row.Cells[8].Text);
                    sumi9 += Convert.ToInt64(e.Row.Cells[9].Text);
                    sumi10 += Convert.ToInt64(e.Row.Cells[10].Text);
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = App_GlobalResources.Language.LblSum;

                    e.Row.Cells[2].Text = sumi2.ToString("#,#0");
                    e.Row.Cells[3].Text = sumi3.ToString("#,#0");
                    e.Row.Cells[4].Text = sumi4.ToString("#,#0");
                    e.Row.Cells[5].Text = sumi5.ToString("#,#0");
                    e.Row.Cells[6].Text = sumi6.ToString("#,#0");
                    e.Row.Cells[7].Text = sumi7.ToString("#,#0");
                    e.Row.Cells[8].Text = sumi8.ToString("#,#0");
                    e.Row.Cells[9].Text = sumi9.ToString("#,#0");
                    e.Row.Cells[10].Text = sumi10.ToString("#,#0");


                    sumi2 = 0;
                    sumi3 = 0;
                    sumi4 = 0;
                    sumi5 = 0;
                    sumi6 = 0;
                    sumi7 = 0;
                    sumi8 = 0;
                    sumi9 = 0;
                    sumi10 = 0;

                }

            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// 在 GridView 控件中的某个行被绑定到一个数据记录时发生。
        /// </summary>
        long sum1 = 0;
        long sum2 = 0;
        long sum3 = 0;
        long sum4 = 0;
        long sum5 = 0;
        long sum6 = 0;
        long sum7 = 0;
        long sum8 = 0;
        long sum9 = 0;
        long sum10 = 0;
        long sum11 = 0;
        long sum12 = 0;
        long sum13 = 0;
        long sum14 = 0;
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                    e.Row.Attributes.Add("onMouseOut", "SetOldColor(this)");
                }
                if (e.Row.RowIndex >= 0 && !lblerro.Visible)
                {
                    //long sumi = 0;
                    //sumi += Convert.ToInt64(e.Row.Cells[1].Text);
                    //sumi += Convert.ToInt64(e.Row.Cells[2].Text);
                    //sumi += Convert.ToInt64(e.Row.Cells[3].Text);
                    //sumi += Convert.ToInt64(e.Row.Cells[4].Text);
                    //sumi += Convert.ToInt64(e.Row.Cells[5].Text);
                    //sumi += Convert.ToInt64(e.Row.Cells[6].Text);
                    //sumi += Convert.ToInt64(e.Row.Cells[7].Text);
                    //sumi += Convert.ToInt64(e.Row.Cells[8].Text);
                    //e.Row.Cells[9].Text = sumi.ToString("#,#0");


                    sum1 += Convert.ToInt64(e.Row.Cells[1].Text);
                    sum2 += Convert.ToInt64(e.Row.Cells[2].Text);
                    sum3 += Convert.ToInt64(e.Row.Cells[3].Text);
                    sum4 += Convert.ToInt64(e.Row.Cells[4].Text);
                    sum5 += Convert.ToInt64(e.Row.Cells[5].Text);
                    sum6 += Convert.ToInt64(e.Row.Cells[6].Text);
                    sum7 += Convert.ToInt64(e.Row.Cells[7].Text);
                    sum8 += Convert.ToInt64(e.Row.Cells[8].Text);
                    sum9 += Convert.ToInt64(e.Row.Cells[9].Text);
                    sum10 += Convert.ToInt64(e.Row.Cells[10].Text);
                    sum11 += Convert.ToInt64(e.Row.Cells[11].Text);
                    sum12 += Convert.ToInt64(e.Row.Cells[12].Text);
                    sum13 += Convert.ToInt64(e.Row.Cells[13].Text);
                    sum14 += Convert.ToInt64(e.Row.Cells[14].Text);
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = App_GlobalResources.Language.LblSum;
                    e.Row.Cells[1].Text = sum1.ToString("#,#0");
                    e.Row.Cells[2].Text = sum2.ToString("#,#0");
                    e.Row.Cells[3].Text = sum3.ToString("#,#0");
                    e.Row.Cells[4].Text = sum4.ToString("#,#0");
                    e.Row.Cells[5].Text = sum5.ToString("#,#0");
                    e.Row.Cells[6].Text = sum6.ToString("#,#0");
                    e.Row.Cells[7].Text = sum7.ToString("#,#0");
                    e.Row.Cells[8].Text = sum8.ToString("#,#0");
                    e.Row.Cells[9].Text = sum9.ToString("#,#0");
                    e.Row.Cells[10].Text = sum10.ToString("#,#0");
                    e.Row.Cells[11].Text = sum11.ToString("#,#0");
                    e.Row.Cells[12].Text = sum12.ToString("#,#0");
                    e.Row.Cells[13].Text = sum13.ToString("#,#0");
                    e.Row.Cells[14].Text = sum14.ToString("#,#0");

                     sum1 = 0;
                     sum2 = 0;
                     sum3 = 0;
                     sum4 = 0;
                     sum5 = 0;
                     sum6 = 0;
                     sum7 = 0;
                     sum8 = 0;
                     sum9 = 0;
                     sum10 = 0;
                     sum11 = 0;
                     sum12 = 0;
                     sum13 = 0;
                     sum14 = 0;
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
            ControlMonthSelect1.SetSelectDate(tboxTimeB.Text, tboxTimeE.Text);
            bind();
            bind2(GridView2, "5星");
            bind2(GridView3, "6星");
            bind2(GridView4, "7星");
            bind2(GridView5, "8星");
            bind2(GridView6, "9星");

            bind2(GridView7, "3星");
            bind2(GridView8, "4星");
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
                    tcHeader[0].Text = "日期";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[1].Attributes.Add("rowspan", "2");
                    tcHeader[1].Text = "装备星级";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[2].Attributes.Add("colspan", "8");
                    tcHeader[2].Text = "职业";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[3].Attributes.Add("rowspan", "2");
                    tcHeader[3].Text = "总计</th></tr><tr style='color:White;background-color:#006699;font-size:12px;font-weight:bold;'>";

                    //第二行表头
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[4].Text = "虎贲";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[5].Attributes.Add("BackColor", "#006699");
                    tcHeader[5].Attributes.Add("ForeColor", "White");
                    tcHeader[5].Text = "浪人";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[6].Attributes.Add("BackColor", "#006699");
                    tcHeader[6].Attributes.Add("ForeColor", "White");
                    tcHeader[6].Text = "龙胆";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[7].Attributes.Add("BackColor", "#006699");
                    tcHeader[7].Attributes.Add("ForeColor", "White");
                    tcHeader[7].Text = "巧工";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[8].Attributes.Add("BackColor", "#006699");
                    tcHeader[8].Attributes.Add("ForeColor", "White");
                    tcHeader[8].Text = "斗仙";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[9].Attributes.Add("BackColor", "#006699");
                    tcHeader[9].Attributes.Add("ForeColor", "White");
                    tcHeader[9].Text = "花灵";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[10].Attributes.Add("BackColor", "#006699");
                    tcHeader[10].Attributes.Add("ForeColor", "White");
                    tcHeader[10].Text = "天师";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[11].Attributes.Add("BackColor", "#006699");
                    tcHeader[11].Attributes.Add("ForeColor", "White");
                    tcHeader[11].Text = "行者";
                    break;
            }
        }

        protected void DropDownListVocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind2(GridView2, "5星");
            bind2(GridView3, "6星");
            bind2(GridView4, "7星");
            bind2(GridView5, "8星");
            bind2(GridView6, "9星");

            bind2(GridView7, "3星");
            bind2(GridView8, "4星");
        }

    }
}
