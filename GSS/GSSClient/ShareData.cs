using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Threading;

namespace GSSClient
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
        private static string _userid="";
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
        private static string _userpower="";
        /// <summary>
        /// 当前登录用户权限
        /// </summary>
        public static string UserPower
        {
            get
            {
                return (_userpower);
            }
            set
            {
                _userpower = value;
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

        private static log4net.ILog  _log;
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

        private static ArrayList _msg = new ArrayList();
        /// <summary>
        /// 用于保存当前收到的消息,如返回的操作消息！
        /// </summary>
        public static ArrayList Msg
        {
            get
            {
                return (_msg);
            }
            set
            {
                _msg = value;
            }
        }

        private static int _fromhidMain;
        /// <summary>
        /// 主窗体处理编号
        /// </summary>
        public static int FormhidMain
        {
            get
            {
                return (_fromhidMain);
            }
            set
            {
                _fromhidMain = value;
            }
        }

        private static int _fromhidAdd;
        /// <summary>
        /// 添加窗体处理编号
        /// </summary>
        public static int FormhidAdd
        {
            get
            {
                return (_fromhidAdd);
            }
            set
            {
                _fromhidAdd = value;
            }
        }

        private static int _fromhidEdit;
        /// <summary>
        /// 编辑窗体处理编号
        /// </summary>
        public static int FormhidEdit
        {
            get
            {
                return (_fromhidEdit);
            }
            set
            {
                _fromhidEdit = value;
            }
        }

        private static int _dgvselectidex=0;
        /// <summary>
        /// 工单列表选中值
        /// </summary>
        public static int DGVSelectIndex
        {
            get
            {
                return (_dgvselectidex);
            }
            set
            {
                _dgvselectidex = value;
            }
        }

        private static int _dgvselecttaskid;
        /// <summary>
        /// 工单列表选中值
        /// </summary>
        public static int DGVSelectTaskID
        {
            get
            {
                return (_dgvselecttaskid);
            }
            set
            {
                _dgvselecttaskid = value;
            }
        }

        private static string _tasklistrequestwhere;
        /// <summary>
        /// 工单列表请求条件
        /// </summary>
        public static string TaskListRequstWhere
        {
            get
            {
                return (_tasklistrequestwhere);
            }
            set
            {
                _tasklistrequestwhere = value;
            }
        }

        private static FormChat _formChat;
        /// <summary>
        /// 聊天窗体
        /// </summary>
        public static FormChat FormChat
        {
            get {return ShareData._formChat;}
            set { ShareData._formChat = value; }
        }

        //private static Thread threadGSS = new Thread(,);
    }
}
