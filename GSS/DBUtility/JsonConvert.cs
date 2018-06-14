using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization.Json;// rely System.ServiceModel.Web.dll
using System.IO;
namespace GSS.DBUtility
{
    /*
     limit:the property in the object must  fiexed type  
     * if not :请将未知的类型以静态方式添加到已知类型的列表，序列化成JSON的KnownTypeAttribute
     */
    public static class JsonConvert
    {
        public static string ConvertJson<T>(this T obj) where T : class
        {
            DataContractJsonSerializer jss = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            jss.WriteObject(ms, obj);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        public static T ConvertObject<T>(this string json) where T : class
        {
            DataContractJsonSerializer jss = new DataContractJsonSerializer(typeof(T));
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
            MemoryStream ms = new MemoryStream(jsonBytes);
            T obj = (T)jss.ReadObject(ms);
            return obj;
        }
    }

    public static class SampleLogger
    {
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="debugeLog">该值为false，如果在配置文件中设置CloseIoLogger=true则不写日志；该值为true默认写日志</param>
        public static void Logger(this string info,bool debugeLog=false)
        {
            bool NoLogger=!debugeLog&& System.Configuration.ConfigurationManager.AppSettings["CloseIoLogger"] == "true";
            if (NoLogger)
            {
                return;
            }
            string dir = GetAssemblyDir() + "\\" + DateTime.Now.Year+""+ TodayOfWeekInYear();
            try
            {
                string file = DateTime.Now.ToString("yyyyMMddHH") + ".log";
                WrtiteFile(dir, file, info);
            }
            catch (Exception ex)
            { }
        }
        public static void ErrorLogger(this string error,bool debugLog=false) 
        {
            bool NoLogger = !debugLog && System.Configuration.ConfigurationManager.AppSettings["CloseIoLogger"] == "true";
            if (NoLogger)
            {
                return;
            }
            string dir = GetAssemblyDir() + "\\ErrorLog\\" + DateTime.Now.Year + "" + TodayOfWeekInYear();
            try
            {
                string file = DateTime.Now.ToString("yyyyMMddHH") + ".log";
                WrtiteFile(dir, file, error);
            }
            catch (Exception ex)
            { }
        }
        public static void Logger(this string info, string relativeDir, bool debugLog=false)
        {
            bool NoLogger = !debugLog && System.Configuration.ConfigurationManager.AppSettings["CloseIoLogger"] == "true";
            if (NoLogger)
            {
                return;
            }
            string dir = GetAssemblyDir();
            if (!string.IsNullOrEmpty(relativeDir))
                dir += "\\" + relativeDir;
            dir+="\\" + TodayOfWeekInYear();
            try
            {
                string file = DateTime.Now.ToString("yyyyMMddHH") + ".log";
                WrtiteFile(dir, file, info);
            }
            catch (Exception ex)
            { }
        }
        public static void WebLogger(this string info, string relativeDir, bool debugLog = false)
        {
            string dir = System.Threading.Thread.GetDomain().BaseDirectory;//web获取路径不能使用程序集，程序集会定位路径到 C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\站点名\xx\xxx\assembly
            DirectoryInfo di = new DirectoryInfo(dir);
           string logDir=  di.Parent.FullName;

           bool NoLogger = !debugLog && System.Configuration.ConfigurationManager.AppSettings["CloseIoLogger"] == "true";
           if (NoLogger)
           {
               return;
           }
           if (!string.IsNullOrEmpty(relativeDir))
               logDir += "\\" + relativeDir;
           logDir += "\\" + TodayOfWeekInYear();
           try
           {
               string file = DateTime.Now.ToString("yyyyMMddHH") + ".log";
               WrtiteFile(logDir, file, info);
           }
           catch (Exception ex)
           { }
        }
    }
    public class AssemblyPath
    {
        public string GetAssemblyDir(int parentLayer)
        {
            System.Reflection.Assembly ass = this.GetType().Assembly;
            DirectoryInfo dir = new DirectoryInfo(ass.Location);
            string path = dir.Parent.FullName;
            while (parentLayer > 0)
            {
                dir = Directory.GetParent(path);
                path = dir.FullName;
                if (path == dir.Root.Name)
                {
                    break;
                }
                parentLayer--;
            }
            return path;
        }
    }
}
