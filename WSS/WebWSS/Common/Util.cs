using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace WebWSS.Common
{
    public class Util
    {
        //字符集转换   
        public static string TranI2G(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
                gb2312 = System.Text.Encoding.GetEncoding("gbk");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }
        //字符集转换   
        public static string TranG2I(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                iso8859 = System.Text.Encoding.GetEncoding("gbk");
                gb2312 = System.Text.Encoding.GetEncoding("iso8859-1");
                byte[] iso;
                iso = iso8859.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }

        }

        //public static void OutExcel(System.Data.DataTable dt, string[] colmsThran, string fileName)
        //{
        //    //System.Collections.ArrayList list = new System.Collections.ArrayList(colmsThran);
        //    //OutExcel(dt, list, fileName);
        //    //Dictionary<string,string> dic=new Dictionary<string,string>();
        //    //dic.Add()

        //}

        //导出EXCEL
        public static void OutExcel(System.Data.DataTable dt, Dictionary<string, string> colmsThran,string fileName)
        {

            System.Text.StringBuilder data = new System.Text.StringBuilder();

            //写出列名 
            foreach (DataColumn column in dt.Columns)
            {
                string colName = "";
                if (colmsThran!=null&&colmsThran.TryGetValue(column.ColumnName, out colName))
                {
                    data.Append(colName);
                }
                else
                {
                    data.AppendFormat("{0},", column.ColumnName);
                }
            }

            data.Append("\n");

            //写出数据 
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    data.AppendFormat("{0},", row[column]);
                }
                data.Append("\n");
            }
            data.Append("\n");

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(fileName) + ".csv");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.Write(data);
            HttpContext.Current.Response.End();
        }

        public static DataTable GridViewToDataSet(GridView gv)
        {
            DataTable table = new DataTable();
            int rowIndex = 0;
            if (!gv.ShowHeader || gv.Columns.Count == 0)
            {
                return table;
            }
            List<string> cols = new List<string>();
            GridViewRow headerRow = gv.HeaderRow;
            int columnCount = headerRow.Cells.Count;
            foreach (TableCell cell in headerRow.Cells)
            {
                cols.Add(GetCellText(cell));
            }
            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DataRow r = table.NewRow();

                }
            }
            // getcell
            return null;

        }
        public static string GetCellText(TableCell cell)
        {
            string text = cell.Text;
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            return "-";
        }


        public static DataTable GetGridDataTable(GridView grid)
        {
            DataTable dt = new DataTable();
            DataColumn dc;//创建列 
            DataRow dr;       //创建行 
            //构造列 
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                dc = new DataColumn();
                dc.ColumnName = grid.Columns[i].HeaderText;
                dt.Columns.Add(dc);
            }
            //构造行 
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                dr = dt.NewRow();
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    dr[j] = grid.Rows[i].Cells[j].Text;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

    }
}
