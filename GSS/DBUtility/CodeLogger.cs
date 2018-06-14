using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GSS.DBUtility
{
    public static class DefineLogger
    {
        public static void LoggerWithDir(this string text, string relateveDir)
        {
            CodeLogger.Logger(text, relateveDir);
        }
    }
    static class CodeLogger
    {
        public static void Logger(this string info,string relateveDir, bool debugeLog = false)
        {
            bool NoLogger = !debugeLog && System.Configuration.ConfigurationManager.AppSettings["CloseIoLogger"] == "true";
            if (NoLogger)
            {
                return;
            }
            string dir = GetAssemblyDir();
            if (!string.IsNullOrEmpty(relateveDir))
            {
                dir += "\\" + relateveDir;
            }
            dir+="\\" + TodayOfWeekInYear();
            try
            {
                string file = DateTime.Now.ToString("yyyyMMddHH") + ".log";
                WrtiteFile(dir, file, info);
            }
            catch (Exception ex)
            { }
        }
        static void WrtiteFile(string path, string fileName, string text)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + "\\" + fileName;
            FileStream file;
            if (File.Exists(filename))
            {
                file = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            }
            else
            {
                file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            sb.AppendLine(text);
            //待写入的内容过大是否能分段写入
            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
            int len = bytes.Length;
            file.Write(bytes, 0, bytes.Length);
            file.Flush();
            file.Close();
        }
        static string GetAssemblyDir()
        {
            AssemblyPath ass = new AssemblyPath();
            return ass.GetAssemblyDir(3);
        }
        /// <summary>
        /// 今天是当年的第几周
        /// </summary>
        /// <returns></returns>
        static int TodayOfWeekInYear()
        {
            //获取当前周
            DateTime now = DateTime.Now;
            // System.Globalization in  mscorlib.dll
            System.Globalization.GregorianCalendar calend = new System.Globalization.GregorianCalendar();//日历函数
            int week = calend.GetWeekOfYear(now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return week;
        }
    }
}
