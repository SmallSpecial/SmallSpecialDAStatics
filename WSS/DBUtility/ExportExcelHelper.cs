using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace WSS.DBUtility
{
    public static class ExportExcelHelper
    {
        /// <summary>
        /// 将查询出的DataSet导出为Excel。
        /// </summary>
        /// <param name="dataSet">要导出的DataSet数据集，可以包含多个DataTable数据，使用DataSetName作为导出的文件名</param>
        public static void ExportDataSet(DataSet dataSet)
        {
            if (dataSet == null) throw new ArgumentException("dataSet");
            if (dataSet.Tables.Count == 0) return;

            MemoryStream stream = SpreadsheetReader.Create();
            SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, true);
            //首先清空原有的Sheet
            var allSheet = doc.WorkbookPart.Workbook.Descendants<Sheet>();
            if (allSheet.Count() > 0)
            {
                doc.WorkbookPart.Workbook.Sheets.RemoveAllChildren();
            }
            //构造Excel
            foreach (DataTable dt in dataSet.Tables)
            {
                WorksheetPart sheetPart = SpreadsheetWriter.InsertWorksheet(doc, dt.TableName);
                WorksheetWriter sheetWriter = new WorksheetWriter(doc, sheetPart);
                //打印列名
                int colIndex = 0;//当前第几列，从0开始
                foreach (DataColumn col in dt.Columns)
                {
                    string reference = GetExcelColumnHeader(colIndex) + "1";
                    sheetWriter.PasteText(reference, col.ColumnName);
                    colIndex++;
                }
                sheetWriter.PasteDataTable(dt, "A2");
                SpreadsheetWriter.Save(doc);
            }
            //Write to response stream
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            context.Response.Clear();
            string UserAgent = context.Request.ServerVariables["http_user_agent"].ToLower();
            string FileName = dataSet.DataSetName;
            if (UserAgent.IndexOf("firefox") == -1)
            {//非火狐浏览器                
                FileName = context.Server.UrlEncode(FileName);
            }
            context.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName + ".xlsx"));
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            stream.WriteTo(context.Response.OutputStream);
            context.Response.End();
        }
        /// <summary>
        /// 计算当前列的列名，列数从0开始
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="currIndex">从0开始</param>
        /// <returns></returns>
        private static string GetExcelColumnHeader(int currIndex)
        {
            char[] arrOfAlphabet = new char[] { 
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 
                'H', 'I', 'J', 'K', 'L', 'M', 'N', 
                'O', 'P', 'Q', 'R', 'S', 'T', 
                'U', 'V', 'W', 'X', 'Y', 'Z' 
            };
            //按十进制数转为26进制数的方法获取列名
            System.Text.StringBuilder s = new StringBuilder();
            int intQuotient = currIndex / 26;
            int intRemainder = currIndex % 26;

            while (intQuotient > 0)
            {
                s.Insert(0, arrOfAlphabet[intRemainder]);
                if (intQuotient == 1)
                {
                    intQuotient = intQuotient / 26;
                    intRemainder = intQuotient % 26;
                }
                else
                {
                    intRemainder = (intQuotient - 1) % 26;
                    intQuotient = intQuotient / 26;
                }
            };
            if (intQuotient == 0)
            {
                int index = s.Length > 0 ? s.Length - 1 : 0;
                s.Insert(index, arrOfAlphabet[intRemainder]);
            }
            return s.ToString();
        }
    }
}
