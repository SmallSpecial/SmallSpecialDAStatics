using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using InfoSoftGlobal;
using System.Text;
using WSS.DBUtility;
using System.Data;

namespace WSS.Web.StatF
{
    public partial class GameOnlineBaseDiga : System.Web.UI.Page
    {
        string ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetChart("2001-1-1");
            }

        }

        private void SetChart(string dates)
        {
            string datestr = "2001-1-1";
            if (dates.Length > 0)
            {
                datestr = dates;
            }

            DateTime date;
            try
            {
                date = Convert.ToDateTime(datestr);
            }
            catch (System.Exception ex)
            {
                date = Convert.ToDateTime("2001-1-1");
            }

            string var = date.ToShortDateString();
            string sql = @"SELECT F_ID, F_Year, F_Month, F_Day, F_Hour, F_BigZone, F_LoginNum, F_LoginIpNum, F_ExitNum, F_ExitIpNum, F_OnlineNum, F_OnlineIpNum, 
                      F_OnlineTime FROM T_GameOnlineBaseDig where F_year=" + date.Year + " and F_month=" + date.Month + " and F_day=" + date.Day + " order by F_hour asc";

            if (date == Convert.ToDateTime("2001-1-1"))
            {
                var = "运营总数据";

                sql = @"SELECT max(F_Hour) as F_Hour, sum(F_LoginNum) as F_LoginNum, sum(F_LoginIpNum) as F_LoginIpNum, sum(F_ExitNum) as F_ExitNum, sum(F_ExitIpNum) as F_ExitIpNum, sum(F_OnlineNum) as F_OnlineNum, sum(F_OnlineIpNum) as F_OnlineIpNum, sum(F_OnlineTime) as F_OnlineTime
FROM         T_GameOnlineBaseDig group by F_hour order by F_hour asc";
            }



            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStr;
            DataSet ds = sp.Query(sql);

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return;
            }

            int num = 0;
            foreach (ListItem ck in CheckBoxList1.Items)
            {
                if (ck.Selected)
                {
                    num++;
                }
            }


            FusionCharts.SetRenderer("flash");
            StringBuilder xmlData = new StringBuilder();

            if (num == 0)
            {
                return;
            }
            else if (num == 1)
            {
                xmlData.Append("<chart caption='游戏在线基本信息24小时:" + CheckBoxList1.SelectedItem.Value + "[" + var + "]' xAxisName='时间' yAxisName='数量' showValues='0' formatNumberScale='0' showBorder='1' aboutMenuItemLabel='关于 神龙游' aboutMenuItemLink='http://www.shenlongyou.cn' lineColor='#129bee' ColumnColor='#129bee' >");

                string valueStr = "F_OnlineNum";
                switch (CheckBoxList1.SelectedItem.Value)
                {
                    case "登录次数":
                        valueStr = "F_LoginNum";
                        break;
                    case "登录IP数":
                        valueStr = "F_LoginIpNum";
                        break;
                    case "退出次数":
                        valueStr = "F_ExitNum";
                        break;
                    case "退出IP数":
                        valueStr = "F_ExitIpNum";
                        break;
                    case "在线人数":
                        valueStr = "F_OnlineNum";
                        break;
                    case "在线IP数":
                        valueStr = "F_OnlineIpNum";
                        break;
                    case "在线时长":
                        valueStr = "F_OnlineTime";
                        break;
                    default:
                        valueStr = "F_OnlineNum";
                        break;
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    xmlData.Append("<set label='" + dr["F_Hour"] + "点' value='" + dr[valueStr] + "' />");
                }

                xmlData.Append("</chart>");


                switch (RadioButtonList1.SelectedValue)
                {
                    case "矩形图":
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/Column3D.swf", "", xmlData.ToString(), "myNext", "1000", "400", false, true);
                        break;
                    case "线形图":
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/Line.swf", "", xmlData.ToString(), "myNext", "1000", "400", false, true);
                        break;
                    case "饼形图":
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/Pie3D.swf", "", xmlData.ToString(), "myNext", "1000", "400", false, true);
                        break;
                    case "云图":
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/Area2D.swf", "", xmlData.ToString(), "myNext", "1000", "400", false, true);
                        break;
                    default:
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/Column3D.swf", "", xmlData.ToString(), "myNext", "1000", "400", false, true);
                        break;
                }

            }
            else
            {
                //<dataset seriesName='2005' renderAs='Area'>

                xmlData.Append("<chart caption='游戏在线基本信息24小时[" + var + "]' xAxisName='时间' yAxisName='数量' showValues= '0' numberPrefix=''  aboutMenuItemLabel='关于 神龙游' aboutMenuItemLink='http://www.shenlongyou.cn' >");

                string str0 = "<categories>";
                string str1 = "<dataset seriesName='登录次数'>";
                string str2 = "<dataset seriesName='登录IP数'>";
                string str3 = "<dataset seriesName='退出次数'>";
                string str4 = "<dataset seriesName='退出IP数'>";
                string str5 = "<dataset seriesName='在线人数'>";
                string str6 = "<dataset seriesName='在线IP数'>";
                string str7 = "<dataset seriesName='在线时长'>";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str0 += " <category label='" + dr["F_Hour"] + "点' />";
                    str1 += "<set value='" + dr["F_LoginNum"] + "' />";
                    str2 += "<set value='" + dr["F_LoginIpNum"] + "' />";
                    str3 += "<set value='" + dr["F_ExitNum"] + "' />";
                    str4 += "<set value='" + dr["F_ExitIpNum"] + "' />";
                    str5 += "<set value='" + dr["F_OnlineNum"] + "' />";
                    str6 += "<set value='" + dr["F_OnlineIpNum"] + "' />";
                    str7 += "<set value='" + dr["F_OnlineTime"] + "' />";

                }
                str0 += " </categories>";
                str1 += "</dataset>";
                str2 += "</dataset>";
                str3 += "</dataset>";
                str4 += "</dataset>";
                str5 += "</dataset>";
                str6 += "</dataset>";
                str7 += "</dataset>";


                foreach (ListItem ck in CheckBoxList1.Items)
                {
                    if (!ck.Selected)
                    {
                        switch (ck.Value)
                        {
                            case "登录次数":
                                str1 = "";
                                break;
                            case "登录IP数":
                                str2 = "";
                                break;
                            case "退出次数":
                                str3 = "";
                                break;
                            case "退出IP数":
                                str4 = "";
                                break;
                            case "在线人数":
                                str5 = "";
                                break;
                            case "在线IP数":
                                str6 = "";
                                break;
                            case "在线时长":
                                str7 = "";
                                break;
                        }
                    }
                }


                xmlData.Append(str0 + str1 + str2 + str3 + str4 + str5 + str6 + str7);

                xmlData.Append("</chart>");


                switch (RadioButtonList1.SelectedValue)
                {
                    case "矩形图":
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/MSColumn3D.swf", "", xmlData.ToString(), "myNext", "1000", "500", false, true);
                        break;
                    case "线形图":
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/MSLine.swf", "", xmlData.ToString(), "myNext", "1000", "500", false, true);
                        break;
                    case "饼形图":
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/MSCombiDY2D.swf", "", xmlData.ToString(), "myNext", "1000", "500", false, true);
                        break;
                    default:
                        Literal1.Text = FusionCharts.RenderChart("FusionCharts/MSColumn3D.swf", "", xmlData.ToString(), "myNext", "1000", "500", false, true);
                        break;
                }
            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string dd = "";
            SetChart(TextBox1.Text);
        }
    }
}
