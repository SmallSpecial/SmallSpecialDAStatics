using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSServer
{
    class ShareData
    {
        private static string _GSSClientVersion = "1.0.0.50";
        /// <summary>
        /// 客户端版本号
        /// </summary>
        public static string GSSClientVersion
        {
            get { return _GSSClientVersion; }
            set { _GSSClientVersion = value; }
        }

        private static string _userid = "";
        /// <summary>
        /// 当前登录用户编号
        /// </summary>
        public static string UserID
        {
            get
            {
                return (_userid);
            }
            set
            {
                _userid = value;
            }
        }
        private static log4net.ILog _log;
        /// <summary>
        /// 日志记录
        /// </summary>
        public static log4net.ILog Log
        {
            get
            {
                return (_log);
            }
            set
            {
                _log = value;
            }
        }
        private static string _localip;
        /// <summary>
        /// 当前IP
        /// </summary>
        public static string LocalIp
        {
            get
            {
                return (_localip);
            }
            set
            {
                _localip = value;
            }
        }

        private static int _localport;
        /// <summary>
        /// 当前端口
        /// </summary>
        public static int LocalPort
        {
            get
            {
                return (_localport);
            }
            set
            {
                _localport = value;
            }
        }
    }
}
