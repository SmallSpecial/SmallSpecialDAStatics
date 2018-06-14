using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using InfoSoftGlobal;
using System.Text;
using System.Data;

namespace WebWSS.Common
{
    public partial class ControlChart : System.Web.UI.UserControl
    {

        private static GridView _gridview = null;

        private static int Height = 0;


        /// <summary>
        /// 需要显示的GV
        /// </summary>
        public GridView GridViewChart
        {
            get { return _gridview; }
            set { _gridview = value; }
        }

        public void SetChart(GridView grid, String title)
        {
            SetChart(grid, title, 1);
        }
        public void SetChart(GridView grid, String title, int type)
        {
            SetChart(grid, title, type, false);
        }
        public void SetChart(GridView grid, String title, int type, bool isdesc)
        {
            SetChart(grid, title, type, isdesc, 0, 1);
        }

        public void SetChart(GridView grid, String title, int type, bool isdesc, int bc, int ec)
        {
            StringBuilder xmlData = new StringBuilder();


            xmlData.Append("<chart caption='" + title + "' xAxisName='' yAxisName='' showValues= '0' numberPrefix='' labelDisplay='AUTO' rotateLabels='0' slantLabels='1'  aboutMenuItemLabel='关于 北京小小传奇网络信息有限公司' aboutMenuItemLink='#' exportEnabled='0' exportAction='Download' exportShowMenuItem = '0'  exportfilename='SLYCharts' exportAtClient='0' exportHandler='Export_Handler/FCExporter.aspx' exportDialogMessage='数据转换中,请稍候...' animation='1'  numVDivLines='22' bgColor='#f7fbfe' borderColor='#b9b9b9' showBorder='1'>");

            int serCount = grid.Columns.Count - 1 - ec - bc;

            if (serCount == 1)
            {


                if (isdesc)
                {
                    for (int i = grid.Rows.Count - 1; i >= 0; i--)
                    {
                        xmlData.Append("<set label='" + grid.Rows[i].Cells[bc].Text + "' value='" + grid.Rows[i].Cells[bc+1].Text + "' />");
                    }
                }
                else
                {
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        xmlData.Append("<set label='" + grid.Rows[i].Cells[bc].Text + "' value='" + grid.Rows[i].Cells[bc + 1].Text + "' />");
                    }
                }

                xmlData.Append("</chart>");

                //FusionCharts.SetRenderer("javascript");
                //FusionCharts.SetRenderer("flash");

                switch (type)
                {
                    case 1:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/Line.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                    case 2:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/Column3D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                    case 3:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/Pie3D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                    case 4:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/Area2D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                    default:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/Line.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                }
            }
            else
            {
                //添加横向数值
                xmlData.Append("<categories>");

                if (isdesc)
                {
                    for (int i = grid.Rows.Count - 1; i >= 0; i--)
                    {
                        xmlData.Append("<category label='" + grid.Rows[i].Cells[bc].Text + "' />");
                    }
                }
                else
                {
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        xmlData.Append("<category label='" + grid.Rows[i].Cells[bc].Text + "' />");
                    }
                }

                xmlData.Append("</categories>");


                //添加系列
                for (int i = 1 + bc; i < grid.Columns.Count - ec; i++)
                {
                    if (!grid.Columns[i].Visible)
                    {
                        continue;
                    }

                    xmlData.Append("<dataset seriesName='" + grid.Columns[i].HeaderText + "'>");

                    if (isdesc)
                    {
                        for (int y = grid.Rows.Count - 1; y >= 0; y--)
                        {
                            xmlData.Append("<set value='" + grid.Rows[y].Cells[i].Text + "' />");
                        }
                    }
                    else
                    {
                        for (int y = 0; y < grid.Rows.Count; y++)
                        {
                            xmlData.Append("<set value='" + grid.Rows[y].Cells[i].Text + "' />");
                        }
                    }

                    xmlData.Append("</dataset>");

                }

                xmlData.Append("</chart>");

                //FusionCharts.SetRenderer("javascript");
                //FusionCharts.SetRenderer("flash");

                switch (type)
                {
                    case 1:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/MSLine.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                    case 2:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/MSColumn3D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                    case 3:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/Pie3D.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                    case 4:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/MSArea.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                    default:
                        LiteralChart.Text = FusionCharts.RenderChart("/img/Charts/MSLine.swf?ChartNoDataText=提示:没有相关数据,无法生成图表!", "", xmlData.ToString(), "mychart", "95%", "80%", false, true);
                        break;
                }
            }


        }

    }
}