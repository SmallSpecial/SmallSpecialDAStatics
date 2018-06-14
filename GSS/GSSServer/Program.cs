using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using GSS.DBUtility;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
namespace GSSServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormServMain());
        }
    }
    public  class AppConfig 
    {
        /// <summary>
        /// 服务端向客户端返回消息的文本语言配置项
        /// </summary>
        public static string AppTipLanaguageConfig 
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceTipLanguageValue"];
            }
        }
        /// <summary>
        /// 由于阿里云数据库连接地址的连接特异性，允许不校验IP
        /// </summary>
        public static bool VerifyIp 
        {
            get 
            {
                return ConfigurationManager.AppSettings["VerifyIp"] != "false";
            }
        }
        public void SetTipLanguage()
        {
            SqlDataReader read = DbHelperSQL.ExecuteReader("select top 1 F_value from T_GameConfig where F_Name=@F_Name", new SqlParameter[]{
                new SqlParameter("@F_Name",SqlDbType.NVarChar){Value=AppTipLanaguageConfig}
            });
            string lang = string.Empty;
            if (read.Read())
            {
                lang = read["F_value"] as string;
            }
            if (string.IsNullOrEmpty(lang))
            {
                lang = "zh-cn";
            }
            CultureInfo cul = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = cul;
            LanguageResource.Language.Culture = cul;

            //add hexw 2017-9-6 引用全局字典时候语言本地话设置
            LanguageItems.BaseLanguageItem.Culture = cul;
        }
        private static string dateFormat;
        public static string DateFormat 
        {
            get 
            {
                if (string.IsNullOrEmpty(dateFormat))
                {
                    dateFormat = ConfigurationManager.AppSettings["DateFormat"];
                    dateFormat = string.IsNullOrEmpty(dateFormat) ? "yyyy-MM-dd" : dateFormat;
                }
                return dateFormat;
            }
        }
        private static string timeFormat;
        public static string TimeFormat
        {
            get
            {
                if (string.IsNullOrEmpty(timeFormat))
                {
                    timeFormat = ConfigurationManager.AppSettings["TimeFormat"];
                    timeFormat = string.IsNullOrEmpty(timeFormat) ? "HH:mm:ss" : timeFormat;
                }
                return timeFormat;
            }
        }
        static int pipeLinePortSpaceNumber;
        public static int PipeLinePortSpaceNumber
        {
            get 
            {
                if (pipeLinePortSpaceNumber == 0)
                {
                    string cfg = ConfigurationManager.AppSettings["PipeLinePortSpaceNumber"];
                    pipeLinePortSpaceNumber = int.Parse(cfg);
                }
                return pipeLinePortSpaceNumber;
            }
        }
    }
}
