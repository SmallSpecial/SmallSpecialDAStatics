using System;
using System.Configuration;
using System.Data;
namespace GSS.DBUtility
{

    public class PubConstant
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string _connectionString = string.Empty;

                string sqlstr = "SELECT * FROM GSSCONFIG WHERE ID=1";
                DataSet ds = DbHelperSQLite.Query(sqlstr);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    string dbip = ds.Tables[0].Rows[0]["GSSDBIP"].ToString();
                    string dbname = ds.Tables[0].Rows[0]["GSSDBNAME"].ToString();
                    string dbuid = ds.Tables[0].Rows[0]["GSSDBUID"].ToString();
                    string dbpsw = ds.Tables[0].Rows[0]["GSSDBPSW"].ToString();
                    _connectionString = @"Data Source=" + dbip + ";Initial Catalog=" + dbname + ";Persist Security Info=True;User ID=" + dbuid + ";Password=" + dbpsw + "";
                }
                _connectionString.Logger();
                return _connectionString;
            }
        }


        private static string _sqlitstr = "Data Source=GSSDATA\\GSSConfig.config;Version=3;Password=wd~@#gdt*(*[$*&^%;";
        /// <summary>
        /// GSS配置文件:sqlite连接字符串
        /// </summary>
        public static string SqliteConnStr
        {
            get
            {
                return _sqlitstr;
            }
            set
            {
                _sqlitstr = "Data Source=" + value + "\\GSSData\\GSSConfig.config;Version=3;Password=wd~@#gdt*(*[$*&^%;";
            }
        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }
    }
}
