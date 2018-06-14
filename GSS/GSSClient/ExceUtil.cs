using System.Windows.Forms;
using System.Data;
using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using GSS.DBUtility;
namespace GSSClient
{
    class ExceUtil
    {

        //导出为csv文件
        public static void ExportToCsv(System.Data.DataTable dt, string strName)
        {
            string strPath = Path.GetTempPath() + strName + ".csv";

            if (File.Exists(strPath))
            {
                File.Delete(strPath);
            }
            //先打印标头

            StringBuilder strColu = new StringBuilder();
            StringBuilder strValue = new StringBuilder();
            int i = 0;

            try
            {
                StreamWriter sw = new StreamWriter(new FileStream(strPath, FileMode.CreateNew), Encoding.GetEncoding("GB2312"));

                for (i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    strColu.Append(dt.Columns[i].ColumnName);
                    strColu.Append(",");
                }
                strColu.Remove(strColu.Length - 1, 1);//移出掉最后一个,字符

                sw.WriteLine(strColu);

                foreach (DataRow dr in dt.Rows)
                {
                    strValue.Remove(0, strValue.Length);//移出

                    for (i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        strValue.Append(dr[i].ToString());
                        strValue.Append(",");
                    }
                    strValue.Remove(strValue.Length - 1, 1);//移出掉最后一个,字符
                    sw.WriteLine(strValue);
                }

                sw.Close();
            }
            catch (Exception ex)
            {
                ex.ToString().ErrorLogger();
                MessageBox.Show(ex.Message);

            }

            System.Diagnostics.Process.Start(strPath);

        }



    }
}
