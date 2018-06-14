#define Release
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using WSS.DBUtility;
using System.Configuration;
using System.Web.Configuration;
namespace WebWSS
{
    public class Admin_Page : BasePage
    {
        public Admin_Page()
        {
            base.Load += new EventHandler(Admin_Page_Load);

        }

        /// <summary>
        /// 覆盖系统默认的错误页
        /// </summary>
        protected override void OnError(EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            Exception exception = ctx.Server.GetLastError();
            string errorInfo =
                "<br>Offending URL: " + ctx.Request.Url.ToString() +
                "<br>Source: " + exception.Source +
                "<br>Message: " + exception.Message +
                "<br>Stack trace: " + exception.StackTrace;

            ctx.Response.Write(errorInfo);
            ctx.Server.ClearError();
            base.OnError(e);
        }


        void Admin_Page_Load(object sender, EventArgs e)
        {
#if Release
            if (base.Context.Session["FID"] == null)
            {
               Context.Response.Redirect("/admin_Login.aspx");
            }
#endif
            string url = HttpContext.Current.Request.Url.ToString().ToLower();
            
            if (url.IndexOf("/shop/")>=0 && Session["F_Power"].ToString().IndexOf(",106101,") == -1)
            {
                Response.Write("没有 商城管理 相关权限,如有疑问,请联系管理员");
                Response.End();
            }
            if (url.IndexOf("/cdkey/") >= 0 && Session["F_Power"].ToString().IndexOf(",106101,") == -1)
            {
                Response.Write("没有 CDKEY 相关权限,如有疑问,请联系管理员(设置商城管理权限即可)");
                Response.End();
            }
            if (url.IndexOf("/stats/") >= 0 && Session["F_Power"].ToString().IndexOf(",106100,") == -1)
            {
                Response.Write("没有 统计系统 相关权限,如有疑问,请联系管理员");
                Response.End();
            }
            if (!IsPostBack) 
            {
                InitZoneData();
            }

        }

        public void QuitSys()
        {
            Context.Session["FID"] = null;
            Context.Response.Redirect("/admin_Login.aspx");
        }
       
    }
    public class BasePage : System.Web.UI.Page 
    {
        protected string DateTimeFormat = System.Configuration.ConfigurationManager.AppSettings["DateTimeFormat"];
        protected string SmallDateTimeFormat = System.Configuration.ConfigurationManager.AppSettings["SmallDateTimeFormat"] ?? "yyyy-MM-dd";
        protected static string ConnStrGSSDB = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringGSSDB"];
        protected static string DigDbConnString=System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDigGameDB"];
        protected string ConnStrDigGameDB = DigDbConnString;
        protected static string T_GameConfigSession = "T_GameConfigSession";
        protected static string PageLanguage = System.Configuration.ConfigurationManager.AppSettings["PageLanguage"] ?? "zh-CN";//页面显示的语言
        protected static string cdkSign = "-";
        protected static string WssPublish = System.Configuration.ConfigurationManager.AppSettings["WssPublish"];
        /// <summary>
        /// 从配置文件中匹配当前设置职业名称和ID关系
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, string> MapVacationType()
        {
            string types = System.Configuration.ConfigurationManager.AppSettings["VocationType" + PageLanguage];
            if (string.IsNullOrEmpty(types))
            {
                types = "1=猎魔者|2=魔导师|3=龙战士|4=召唤师";
            }
            Dictionary<string, string> map = new Dictionary<string, string>();
            foreach (string item in types.Split('|'))
            {
                string[] job = item.Split('=');
                map.Add(job[0], job.Length == 1 ? string.Empty : job[1]);
            }
            return map;
        }
        protected string GetVacationTypeName(string id)
        {
            Dictionary<string, string> vc = MapVacationType();
            if (vc.Count > 0 && vc.ContainsKey(id)) 
            {
               return vc[id];
            }
            return id;
        }
        /// <summary>
        /// 初始化战区列表到session中
        /// </summary>
        protected void InitZoneData()
        {
            AddGameZoneCache();
        }
        protected static Dictionary<string, string> AddGameZoneCache() 
        {
            object session = HttpContext.Current.Session[T_GameConfigSession];
            if (session != null) 
            {
                 
                Dictionary<string,string> d= (Dictionary<string, string>)session;
                if (d.Count > 0)
                {
                    return d;
                }
            }
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;
            DataSet ds = null;
            string sql = @"SELECT  F_Name,F_ValueGame  FROM T_GameConfig 
where F_ValueGame is not null
and F_ParentID in(
select F_ID from T_GameConfig where F_ParentID=1000  and F_IsUsed=1
)  and F_IsUsed=1";
            ds = spg.Query(sql);
            DropDownList dp = new DropDownList();
            dp.DataTextField = "F_Name";
            dp.DataValueField = "F_ValueGame";
            dp.DataSource = ds;
            ds.Dispose();
            dp.DataBind();
            ListItemCollection items = dp.Items;
            dp.Dispose();

            Dictionary<string, string> cookie = new Dictionary<string, string>();
            foreach (ListItem item in items)
            {
                if (!string.IsNullOrEmpty(item.Value) && !cookie.ContainsKey(item.Value))
                    cookie.Add(item.Value, item.Text);
            }
            HttpContext.Current.Session[T_GameConfigSession] = cookie;
            return cookie;
        }
        protected Dictionary<string, string> GetZoneDataDict() 
        {
            if (Session[T_GameConfigSession] == null)
            {
                InitZoneData();
            }
            Dictionary<string, string> cookie =  Session[T_GameConfigSession] as Dictionary<string, string>;
            return cookie;
        }
        
        /// <summary>
        /// 从session中格式化显示战区名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string FormatZoneName(string id)
        {
            Dictionary<string, string> config = (Dictionary<string, string>)Session[T_GameConfigSession];
            if (config == null)
            {
                InitZoneData();
            }
            if (id == "-1")
            {
                return App_GlobalResources.Language.LblAllZone;
            }
            if (!config.ContainsKey(id)) { return string.Empty; }
            return config[id];
        }
        /// <summary>
        /// 进行字符集转换【此处代码存储多处编写，抽离共用】
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string TranI2G(string value)
        {
            try
            {
                System.Text.Encoding iso8859, gb2312;
                // iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");//此处将出现中文乱码
                gb2312 = System.Text.Encoding.GetEncoding("gb2312");
                byte[] iso;
                iso = gb2312.GetBytes(value);
                return gb2312.GetString(iso);
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }
        static void SetLanguage() 
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(PageLanguage);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(PageLanguage);
        }
        protected override void InitializeCulture()
        {
            SetLanguage();
        }
        public string GetResource(string key) 
        {
           return  App_GlobalResources.Language.ResourceManager.GetString(key,new System.Globalization.CultureInfo(PageLanguage));
        }
        /// <summary>
        /// 得到游戏大区名字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetZoneName(object valuea, object valueb)
        {
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;
            //韩文不能带入到SQL语句中，否则查询的结果形式 ? ?:大区 ??:Korea外网       
            string sql = @"SELECT isnull(a.F_Name,'')+':'+isNull((SELECT TOP 1 b.F_Name FROM T_GameConfig b WHERE (b.F_ParentID = a.F_ID) AND (b.F_IsUsed = 1) 
and (b.F_ValueGame='" + valueb + "')),'') FROM T_GameConfig a WHERE (a.F_ParentID = 1000) AND (a.F_IsUsed = 1) and (a.F_ValueGame='" + valuea + "')";

            try
            {
                string zoneInfo = spg.GetSingle(sql).ToString();
                string[] sp = zoneInfo.Split(':');
                return App_GlobalResources.Language.LblBigZone + ":" + sp[0] + " " + App_GlobalResources.Language.LblZone + ":" + sp[1];
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 系统服务的大区对象ID
        /// </summary>
        public int BigZoneID 
        {
            get 
            {
                return bigZoneId;
            }
        }
        protected static int bigZoneId
        {
            get
            {
                string id = ConfigurationManager.AppSettings["BigZoneID"];
                int ID = 0;
                int.TryParse(id, out ID);
                return ID;
            }
        }
        public string[] GetCDKeyCategory() 
        {
            string cdkey = ConfigurationManager.AppSettings["CDKeyType"];
            string[] items = cdkey.Split(',');
            List<string> list = new List<string>();
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }
        public static string UpdateSingleAppset(string key,string value) 
        {
            try
            {
                PageLanguage = value;
                SetLanguage();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string GetHtmlLang() 
        {
           return PageLanguage.Split('-')[0];
        }
        public string GetNowTimeFormat(string format)
        {
            DateTime now = DateTime.Now;
            return now.ToString(format);
        }
        public bool DisableCDKeyCreate()
        {
            return CreateCdkSwitchIsOn();
        }
        public static Dictionary<string, string> GetWssConfig()
        {
            string cmd = @"select F_name,F_ValueGame from  dbo.T_GameConfig where  F_ParentID =(select F_ID from  dbo.T_GameConfig where F_Name='WssConfig')";
            DbHelperSQLP spg = new DbHelperSQLP();
            spg.connectionString = ConnStrGSSDB;
            DataSet ds= spg.Query(cmd);
           
            if (ds == null ||ds.Tables.Count== 0)
            {
                return new Dictionary<string, string>();
            }
            Dictionary<string, string> configs = new Dictionary<string, string>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                string name = (string)item[0];
                string value =(string) item[1];
                configs.Add(name, value);
            }
            return configs;
        }
        /// <summary>
        /// 是否开启wss创建cdk功能
        /// </summary>
        /// <returns></returns>
        public static bool CreateCdkSwitchIsOn()
        {
            Dictionary<string, string> config = GetWssConfig();
            if (config.ContainsKey("CdkCreateSwichOn"))
            {
                return config["CdkCreateSwichOn"] == "true";
            }
            return false;
        }
        /// <summary>
        /// 获取角色名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetRoleName(string value)
        {
            try
            {
                DbHelperSQLP spg = new DbHelperSQLP();
                spg.connectionString = ConnStrDigGameDB;

                string sql = @"SELECT F_RoleName FROM [LKSV_2_GameCoreDB_0_1].Gamecoredb.dbo.T_RoleCreate with(nolock) where F_RoleID=" + value + "";
                return spg.GetSingle(sql).ToString();
            }
            catch (System.Exception ex)
            {
                return value;
            }
        }
    }
}
