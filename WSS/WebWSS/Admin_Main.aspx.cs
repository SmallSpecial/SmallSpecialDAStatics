using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using WSS.DBUtility;
using InfoSoftGlobal;
using System.Text;

namespace WebWSS
{
    public partial class Admin_Main : Admin_Page
    {
        string ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        DataSet ds;
        StringBuilder xmlData = new StringBuilder();
        string chartitle = "";
        DbHelperSQLP sp = new DbHelperSQLP();

        protected void Page_Load(object sender, EventArgs e)
        {
            sp.connectionString = ConnStr;
            if (!IsPostBack)
            {
                SetInfo();
                SetChart();
            }
        }
        private void SetInfo()
        {
            try
            {
                DataSet dst = null;
                DataRow drt = null;
                string sql = @"with RVcte(F_Date,F_VocationType, F_VocationNum)
as
( SELECT convert(varchar(10),F_Date,120),F_VocationType, Sum(F_VocationNum) as F_VocationNum FROM T_RoleVocation with(nolock) where 1=1 and F_Date='" + DateTime.Now.ToString("yyyy-MM-dd") + "' group by F_Date,F_VocationType )  select distinct(a.F_Date) as 日期,";
                sql += @"(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=5) as 虎贲,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=2) as 浪人,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=3) as 龙胆,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=4) as 巧工,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=7) as 斗仙,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=0) as 花灵,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=1) as 天师,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=6) as 行者,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date) as 总计
 FROM RVcte a order by a.F_Date desc";
                dst = sp.Query(sql);
                if (dst != null && dst.Tables[0].Rows.Count > 0)
                {
                    drt = dst.Tables[0].Rows[0];
                    lblInfo0.Text = "<b>角色总量</b> 总计:" + drt["总计"].ToString() + " 虎贲:" + drt["虎贲"].ToString() + " 浪人:" + drt["浪人"].ToString() + " 龙胆:" + drt["龙胆"].ToString() + " 巧工:" + drt["巧工"].ToString() + " 斗仙:" + drt["斗仙"].ToString() + " 花灵:" + drt["花灵"].ToString() + " 天师:" + drt["天师"].ToString() + " 行者:" + drt["行者"].ToString();
                    lblInfo0.Text += " &nbsp;| <a href='Stats/RoleVocation.aspx' target='main'>详情</a> ";
                }


                sql = @"with RVcte(F_Date,F_VocationType, F_VocationNum)
as
( SELECT convert(varchar(10),F_Date,120),F_VocationType, Sum(F_VocationNum) as F_VocationNum FROM T_RoleVocationGrow with(nolock) where 1=1 and F_Date='" + DateTime.Now.ToString("yyyy-MM-dd") + "' group by F_Date,F_VocationType )  select distinct(a.F_Date) as 日期,";
                sql += @"(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=5) as 虎贲,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=2) as 浪人,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=3) as 龙胆,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=4) as 巧工,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=7) as 斗仙,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=0) as 花灵,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=1) as 天师,
(select isnull(Sum(F_VocationNum),0) from RVcte where F_Date=a.F_Date and F_VocationType/6=6) as 行者

 FROM RVcte a order by a.F_Date desc";
                dst = sp.Query(sql);
                if (dst != null)
                {

                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        drt = dst.Tables[0].Rows[0];
                        lblInfo1.Text = "<b>角色增量</b>  虎贲:" + drt["虎贲"].ToString() + " 浪人:" + drt["浪人"].ToString() + " 龙胆:" + drt["龙胆"].ToString() + " 巧工:" + drt["巧工"].ToString() + " 斗仙:" + drt["斗仙"].ToString() + " 花灵:" + drt["花灵"].ToString() + " 天师:" + drt["天师"].ToString() + " 行者:" + drt["行者"].ToString();
                        lblInfo1.Text += " &nbsp;| <a href='Stats/RoleVocationGrow.aspx' target='main'>详情</a> ";



                        string xmlr = "<chart caption='当天角色总体增量图 " + DateTime.Now.ToString("yyyy-MM-dd") + "' xAxisName='职业' yAxisName='数量' showValues= '0' numberPrefix='' labelDisplay='NONE' rotateLabels='0' slantLabels='0'  aboutMenuItemLabel='关于 北京小小传奇网络信息有限' aboutMenuItemLink='#' exportEnabled='0' exportAction='Download' exportShowMenuItem = '0'  exportfilename='SLYCharts' exportAtClient='0' exportHandler='Export_Handler/FCExporter.aspx' exportDialogMessage='数据转换中,请稍候...' animation='1'  numVDivLines='10' bgColor='#f7fbfe' borderColor='#b9b9b9' showBorder='1'>";


                        xmlr += "<set label='虎贲' value='" + drt["虎贲"].ToString() + "' />";
                        xmlr += "<set label='浪人' value='" + drt["浪人"].ToString() + "' />";
                        xmlr += "<set label='龙胆' value='" + drt["龙胆"].ToString() + "' />";
                        xmlr += "<set label='巧工' value='" + drt["巧工"].ToString() + "' />";
                        xmlr += "<set label='斗仙' value='" + drt["斗仙"].ToString() + "' />";
                        xmlr += "<set label='花灵' value='" + drt["花灵"].ToString() + "' />";
                        xmlr += "<set label='天师' value='" + drt["天师"].ToString() + "' />";
                        xmlr += "<set label='行者' value='" + drt["行者"].ToString() + "' />";

                        xmlr += "</chart>";
                        //FusionCharts.SetRenderer("javascript");
                        Literal2.Text = FusionCharts.RenderChart("img/Charts/Column3D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlr, "mychart2", "350", "350", false, true);
                    }
                }


            }
            catch (System.Exception ex)
            {
                lblInfo1.Text = ex.Message;
            }

        }
        private void SetChart()
        {
            string sql = @"SELECT top 5000 convert(nvarchar(2), F_CreateTime,108) as F_CreateTime,  F_GGSNAME, F_GNGSID, F_GNGSNAME, F_GZONEID, F_GZONENAME, F_PlayerNumOnline, F_MaxPlayerNumHistory, F_DateTimeMaxPlayerNum from T_RoleOnLineFlow with(nolock) where 1=1  and F_Date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  order by F_CreateTime desc";

            ds = sp.Query(sql);

            if (ds == null)
            {
                return;
            }
            chartitle = "当天角色在线趋势图 " + DateTime.Now.ToString("yyyy-MM-dd") + "";


            xmlData.Append("<chart caption='" + chartitle + "' xAxisName='时间' yAxisName='数量' showValues= '0' numberPrefix='' labelDisplay='NONE' rotateLabels='0' slantLabels='0'  aboutMenuItemLabel='关于 北京小小传奇网络信息有限' aboutMenuItemLink='#' exportEnabled='0' exportAction='Download' exportShowMenuItem = '0'  exportfilename='SLYCharts' exportAtClient='0' exportHandler='Export_Handler/FCExporter.aspx' exportDialogMessage='数据转换中,请稍候...' animation='1'  numVDivLines='22'  bgColor='#eef7fd'>");



            List<string> lines = new List<string>();
            List<string> times = new List<string>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string line = dr["F_GNGSNAME"].ToString().Trim();
                // string time = dr["F_CreateTime"].ToString();
                if (!lines.Contains(line) && !string.IsNullOrEmpty(line))
                {
                    lines.Add(line);
                }
                //if (!times.Contains(time) && !string.IsNullOrEmpty(time))
                //{
                //    times.Add(time);
                //}
            }
            for (int i = 0; i < 24; i++)
            {
                times.Add(i.ToString());
            }

            string str0 = "<categories>";
            foreach (string time in times)
            {
                str0 += " <category label='" + time.PadLeft(2, '0') + "' />";
            }
            str0 += " </categories>";

            string str1 = "";
            foreach (string line in lines)
            {
                str1 += "<dataset seriesName='" + line + "'>";
                foreach (string time in times)
                {
                    str1 += "<set value='" + GetOnlineNum(time.PadLeft(2, '0'), line) + "' />";
                }
                str1 += "</dataset>";
            }


            xmlData.Append(str0 + str1);

            xmlData.Append("</chart>");

            //FusionCharts.SetRenderer("javascript");
            // FusionCharts.SetRenderer("flash");

            Literal1.Text = FusionCharts.RenderChart("img/Charts/MSLine.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "800", "350", false, true);
        }

        /// <summary>
        /// 得到在线人数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="linename"></param>
        /// <returns></returns>
        public string GetOnlineNum(string time, string linename)
        {
            try
            {
                string num = ds.Tables[0].Select("F_CreateTime='" + time + "' and F_GNGSNAME='" + linename + "'", "F_PlayerNumOnline DESC")[0]["F_PlayerNumOnline"].ToString();
                return string.IsNullOrEmpty(num) ? "0" : num;
            }
            catch// (System.Exception ex)
            {
                // Response.Write(ex.Message);
                return "0";
            }
        }
    }
}
