using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using log4net;
using GSSUI;
using System.Configuration;
using System.Linq;
using GSS.DBUtility;
using System.Reflection;
namespace GSSClient
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
            InitAppCurtule.DefaultLanguage();
            bool bCreatedNew;
            GSSUI.SharData.GetUIInfo(); 
         
            //System.Globalization.CultureInfo UICulture = new System.Globalization.CultureInfo("zh-CHT");
            //Thread.CurrentThread.CurrentUICulture = UICulture;

            Mutex m = new Mutex(false, "GSSClient2013", out bCreatedNew);
            bool canRunManyApp = false;
            ShareData.Log = log4net.LogManager.GetLogger("GSSLog");
            GSSModel.Response.GameConfig fs = ClientCache.GetGameConfigByName("OnlyRunSingleGssClient").FirstOrDefault();//gss客户端只能单实例启动
            if (fs != null && fs.GameValue == "false")
            {
                canRunManyApp = true;
            }
            if (!bCreatedNew && !canRunManyApp)
            {
                MsgBox.Show(LanguageResource.Language.Tip_OnlyRunSignleApp, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               
                ShareData.Log.Info("启动系统");
                //登录窗口,用户验证
                " will go to login".Logger();
                FormLoginEx form = new FormLoginEx();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    "login success ,will go to run the FormTask".Logger();
                   // form.Close();
                    //记录日志
                    ShareData.Log.Info("通过验证,成功登录系统");
                    "call ShareData.Log.Info".Logger();
                    Application.EnableVisualStyles();
                    "Application.EnableVisualStyle,will call Application.Run".Logger();
                    Application.Run(new FormTask());
                }
            }
                

        }
    }
    public class InitAppCurtule 
    {
        /// <summary>
        /// language items
        /// </summary>
        static string LanguageItems = System.Configuration.ConfigurationManager.AppSettings["SwitchLanguage"];
        public static string Culture { get; set; }
        public static void SetCurtule(string culture) 
        {
            Culture = culture;
            System.Globalization.CultureInfo cul=new System.Globalization.CultureInfo(Culture);
            Thread th = System.Threading.Thread.CurrentThread;
            th.CurrentCulture = cul;
            th.CurrentUICulture = cul;
            LanguageResource.Language.Culture = cul;
        }
        public static void DefaultLanguage() 
        {
            string culture = System.Configuration.ConfigurationManager.AppSettings["AppLanguage"];
            SetCurtule(culture);
        }
        public static string SwitchLanguage() 
        {
            string[] items = LanguageItems.Split('|');
            string cul = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            int cur = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].ToLower() == cul.ToLower())
                {
                    cur=i;
                    break;
                }
            }
            cur++;
            if (cur == items.Length) 
            {
                cur = 0;
            }
            string path = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(path);
            config.AppSettings.Settings["AppLanguage"].Value = items[cur];
            config.Save();
            SetCurtule(items[cur]);
            return items[cur];
        }
    }
    public class SystemConfig 
    {
        public static string BigZoneName 
        {
            get 
            {
                return ConfigurationManager.AppSettings["BigZoneName"];
            }
        }
        public static string[] GetAwardUserColumn 
        {//发奖工单导入的用户Excel数据列
            get 
            {
                string app= ConfigurationManager.AppSettings["AwardUserInfoColumns"];
                string[]items= app.Split('|').Where(s=>!string.IsNullOrEmpty(s)).ToArray();
                return items;
            }
        }
        public static string DateTimeFormat 
        {
            get 
            {
                return ConfigurationManager.AppSettings["DateTimeFormat"];
            }
        }
        public static string DateHourMinute 
        {
            get 
            {
                return ConfigurationManager.AppSettings["DateHourMinute"];
            }
        }
        public static int BigZoneParentId = 1000;
        public static int BattleZoneParentId = 100001;
        public static int AppID
        {
            get 
            {
                 int def=0;
                 string appid = ConfigurationManager.AppSettings["AppID"];
                 int.TryParse(appid, out def);
                return def;
            }
        }
        public static int GameID
        {
            get 
            {
                 int def=0;
                 string gid = ConfigurationManager.AppSettings["GameID"];
                 int.TryParse(gid, out def);
                return def;
            }
        }
        public static int DefaultWorkOrderStatue 
        {//默认工单状态【等待处理】
            get
            {
                int def = 0;
                string appid = ConfigurationManager.AppSettings["DefaultWorkOrderStatue"];
                int.TryParse(appid, out def);
                return def;
            }
        }
        public static Dictionary<int, string> WillCallServicesWorkOrder = new Dictionary<int, string>();
        /// <summary>
        /// 在socket加载数据时遇到缓冲区8192限制没解决时采用延时等待
        /// </summary>
        public static bool GetCacheWaitSleep 
        {
            get 
            {
                string sec = ConfigurationManager.AppSettings["SocektDataReceiveWaitSleep"];
                if (!string.IsNullOrEmpty(sec)&&sec=="true")
                {
                    return true;
                }
                return false;
            }
        }
        private static string sheetName;
        /// <summary>
        /// 默认数据读取的Excel页
        /// </summary>
        public static string DefaultSheetName
        {
            get 
            {
                if(string.IsNullOrEmpty(sheetName))
                    sheetName=ConfigurationManager.AppSettings["DefaultSheetName"];
                return sheetName;
            }
        }
        public static int BetweenFormChatMsgId
        {
            get { return 601; }
        }
        /// <summary>
        /// gss client与客户端存在差异时自动更新客户端
        /// </summary>
        public static bool AutoUpdateGssClient 
        {
            get 
            {
                string auto = ConfigurationManager.AppSettings["AutoUpdateGssClientGameconfig"];
                return auto == "true";
            }
        }
        static string software;
        public static string AppSoftwareName
        {
            get
            {
                if (string.IsNullOrEmpty(software))
                {
                    AssemblyName name = Assembly.GetExecutingAssembly().GetName();
                    software = name.Name;
                }
                return software;
            }
        }
        /// <summary>
        /// 同步版本缓存数据
        /// </summary>
        public static bool SyncVersionCache
        {
            get 
            {
                return ConfigurationManager.AppSettings["SyncVersionCache"] == "true";
            }
        }
        static bool? gridViewCellNamePrint;
        public static bool OpenGridCellNamePrint 
        {
            get 
            {
                if(!gridViewCellNamePrint.HasValue)
                     gridViewCellNamePrint= ConfigurationManager.AppSettings["OpenGridCellNamePrint"] == "true";
                return gridViewCellNamePrint.Value;
            }
        }
    }
}
