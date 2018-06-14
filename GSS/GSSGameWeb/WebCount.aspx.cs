using System;
namespace GSSGameWeb
{
    public partial class WebCount : System.Web.UI.Page
    {
        public string NowURL = "WebCount.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.StrIsNullOrEmpty(GSSRequest.GetUrlReferrer()))//上一页面是否空
                return;
            else if (!CheckRealm(Utils.GetCountStatRealm(), GSSRequest.GetUrlReferrer()))//是否需要统计的域名
                return;
            //else if(Utils.StrIsNullOrEmpty(GSSRequest.GetString("s")))
            //    return;
            NowURL = this.Request.Url.ToString();
            if (NowURL.IndexOf("&w=") != -1)
                NowURL = NowURL.Remove(NowURL.IndexOf("&w="));
            //参数检测
            string game = GSSRequest.GetString("game");
            string type = GSSRequest.GetString("type");
            string swidth = GSSRequest.GetString("w").Trim();
            string sheight = GSSRequest.GetString("h").Trim();
            if (!CheckGame(game))
                return;
            else if (!CheckType(type))
                return;



            if (swidth.Length != 0)
            {
                if (!SessionAllow(game + "-" + type))//是否可以计数
                    return;
                GSSGameWeb.WebServiceGSS.CountDetail model = new GSSGameWeb.WebServiceGSS.CountDetail();
                System.Collections.Specialized.NameValueCollection ServerVariables = System.Web.HttpContext.Current.Request.ServerVariables;
                //IP
                model.IP = ServerVariables["REMOTE_ADDR"].ToString();
                //来路
                string referer = GSSRequest.GetUrlReferrer();
                if (referer.Length > 150)
                {
                    referer = referer.Substring(0, 150);
                }
                model.Page = referer;

                //客户端软件使用情况
                string useragent = ServerVariables["HTTP_USER_AGENT"].ToString();

                //浏览器
                model.IESoft = "Other";
                string[,] arvsoft = new string[,] { { "NetCaptor", "NetCaptor" }, { "MSIE 11", "MSIE 11.x" }, { "MSIE 10", "MSIE 10.x" }, { "MSIE 9", "MSIE 9.x" }, { "MSIE 8", "MSIE 8.x" }, { "MSIE 7", "MSIE 7.x" }, { "MSIE 6", "MSIE 6.x" }, { "MSIE 5", "MSIE 5.x" }, { "MSIE 4", "MSIE 4.x" }, { "Netscape", "Netscape" }, { "Opera", "Opera" }, { "Chrome", "Chrome" } };
                for (int i = 0; i < arvsoft.Length; i++)
                {
                    if (useragent.IndexOf(arvsoft[i, 0].ToString()) > 0)
                    {
                        model.IESoft = arvsoft[i, 1];
                        break;
                    }
                }

                //操作系统
                model.OS = "Other";
                string[,] arvos = new string[,] { { "Windows NT 5.0", "Win2k" }, { "Windows NT 5.1", "WinXP" }, { "Windows NT 5.2", "Win2k3" }, { "Windows NT 6.0", "WinVista" }, { "Windows NT 6.1", "Win7" }, { "Windows NT 6.2", "Win8" }, { "Windows NT", "WinNT" }, { "Windows 9", "Win9x" }, { "unix", "Unix's" }, { "linux", "Unix's" }, { "SunOS", "Unix's" }, { "BSD", "Unix's" }, { "Mac", "Mac" } };
                for (int i = 0; i < arvos.Length; i++)
                {
                    if (useragent.IndexOf(arvos[i, 0].ToString()) > 0)
                    {
                        model.OS = arvos[i, 1];
                        break;
                    }
                }
                model.CLR = Request.Browser.ClrVersion.ToString();
                model.Language = Request.UserLanguages[0];
                model.WinBit = Request.Browser.Win32 ? "WIN32" : (Request.Browser.Win16 ? "WIN16" : "OTHER");

                model.Year = DateTime.Now.Year;
                model.Month = DateTime.Now.Month;
                model.Day = DateTime.Now.Day;
                model.Hour = DateTime.Now.Hour;
                model.Game = int.Parse(game);
                model.Type = int.Parse(type);
                model.Time = DateTime.Now;
                model.Screenwidth = int.Parse(swidth);
                model.Screenheight = int.Parse(sheight);

                WebServiceGSS.WebServiceGSS ws = new WebServiceGSS.WebServiceGSS();
                ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
                ws.GSSCountDetailAdd(model);
            }

        }

        #region 私有方法
        /// <summary>
        /// 是否计数
        /// </summary>
        /// <returns></returns>
        private bool SessionAllow(string cookietype)
        {
            string sessioname = cookietype;
            if (Session[sessioname] != null)
            {
                TimeSpan ts = DateTime.Now -
                Convert.ToDateTime(Session[sessioname]);
                if (ts.TotalSeconds > 180)
                {
                    Session[sessioname] = DateTime.Now; return true;
                }
                else
                    return false;
            }
            else
            {
                Session[sessioname] = DateTime.Now; return true;
            }
        }
        /// <summary>
        /// 检测是否需要统计的游戏类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckGame(string value)
        {
            foreach (int i in Enum.GetValues(typeof(CountGame)))
            {
                if (i.ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检测是否需要统计的类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckType(string value)
        {
            foreach (int i in Enum.GetValues(typeof(CountType)))
            {
                if (i.ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckRealm(string a, string b)
        {
            try
            {
                Uri u = new Uri(b);
                if (("," + a + ",").IndexOf(u.Host) > 0)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 统计类型
        /// </summary>
        private enum CountType
        {
            /// <summary>
            /// 安装成功
            /// </summary>
            INSTALLSUCCESS = 0,
            /// <summary>
            /// 卸载成功
            /// </summary>
            UNINSTALLSUCCESS = 1,
            /// <summary>
            /// 安装失败
            /// </summary>
            INSTALLFAIL = 2,
            /// <summary>
            /// 卸载失败
            /// </summary>
            UNINSTALLFAIL = 3,
            /// <summary>
            /// 下载成功
            /// </summary>
            DOWNLOADSUCCESS = 4,
            /// <summary>
            /// 下载失败
            /// </summary>
            DOWNLOADFAIL = 5
        }

        /// <summary>
        /// 统计游戏
        /// </summary>
        private enum CountGame
        {
            /// <summary>
            /// 寻龙记
            /// </summary>
            XUNLONGJI = 0
        }
        #endregion
    }

}
