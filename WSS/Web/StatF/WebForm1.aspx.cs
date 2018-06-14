using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WSS.DBUtility;

namespace WSS.Web.StatF
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT F_ID, F_Year, F_Month, F_Day, F_BigZone, F_ZoneID, F_LoginNGSID, F_ACU, F_PCU, F_PCUTime FROM T_GameOnlineBaseDig_Day WHERE (F_Year = 2011) AND (F_Month = 12) or 1=1";
            string ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
            DbHelperSQLP sp = new DbHelperSQLP();
            sp.connectionString = ConnStr;
            DataSet ds = sp.Query(sql);

            SetExcelFromData(ds, "afds");
        }

        public void SetExcelFromData(System.Data.DataSet ds, string FileName)
        {
            string data = "";
            //data = ds.DataSetName + "\n"; 

            foreach (DataTable tb in ds.Tables)
            {
                data += tb.TableName + "\n";

                //写出列名 
                foreach (DataColumn column in tb.Columns)
                {
                    data += column.ColumnName + ",";
                }
                data += "\n";

                //写出数据 
                foreach (DataRow row in tb.Rows)
                {
                    foreach (DataColumn column in tb.Columns)
                    {
                        data += row[column].ToString() + ",";
                    }
                    data += "\n";
                }
                data += "\n";
            }


            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Context.Server.UrlEncode(FileName) + ".csv");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.Write(data);
            Response.End();
        }

    }
}
