using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using GSSBLL;
using GSSServerLibrary;

namespace GSSClient
{
    /// <summary>
    /// remoting方式
    /// </summary>
    public abstract class ClientRemoting
    {
        public ClientRemoting()
        {

        }
        FDBISql FDBISql_S = null;
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            ChannelServices.RegisterChannel(new TcpClientChannel(), false);
            string classname = "FDBISql";
            string serverurl = string.Format("tcp://{0}:{1}/{2}", ShareData.LocalIp, ShareData.LocalPort + 1, classname);
            FDBISql_S = (FDBISql)Activator.GetObject(typeof(FDBISql), serverurl);

        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Stop()
        {

        }

        /// <summary>
        /// FDBISql
        /// </summary>
        public static FDBISql FDBISql()
        {
            IChannel channel = new TcpClientChannel();
            string classname = "FDBISql";
            string serverurl = string.Format("tcp://{0}:{1}/{2}", ShareData.LocalIp, ShareData.LocalPort + 1, classname);

            ChannelServices.RegisterChannel(channel, false);
            FDBISql obj = (FDBISql)Activator.GetObject(typeof(FDBISql), serverurl);
            ChannelServices.UnregisterChannel(channel);
            return obj;
        }
        /// <summary>
        /// Tasks
        /// </summary>
        public static Tasks Tasks()
        {
            IChannel channel = new TcpClientChannel();
            string classname = "Tasks";
            string serverurl = string.Format("tcp://{0}:{1}/{2}", ShareData.LocalIp, ShareData.LocalPort + 1, classname);

            ChannelServices.RegisterChannel(channel, false);
            Tasks obj = (Tasks)Activator.GetObject(typeof(Tasks), serverurl);
            ChannelServices.UnregisterChannel(channel);
            return obj;
        }

        /// <summary>
        /// TasksLog
        /// </summary>
        public static TasksLog TasksLog()
        {
            IChannel channel = new TcpClientChannel();
            string classname = "TasksLog";
            string serverurl = string.Format("tcp://{0}:{1}/{2}", ShareData.LocalIp, ShareData.LocalPort + 1, classname);

            ChannelServices.RegisterChannel(channel, false);
            TasksLog obj = (TasksLog)Activator.GetObject(typeof(TasksLog), serverurl);
            ChannelServices.UnregisterChannel(channel);
            return obj;
        }


        /// <summary>
        /// ServerRemoteLib
        /// </summary>
        public static ServerRemoteLib ServerRemoteLib()
        {
            IChannel channel = new TcpClientChannel();
            string classname = "ServerRemoteLib";
            string serverurl = string.Format("tcp://{0}:{1}/{2}", ShareData.LocalIp, ShareData.LocalPort + 1, classname);

            ChannelServices.RegisterChannel(channel, false);
            ServerRemoteLib obj = (ServerRemoteLib)Activator.GetObject(typeof(ServerRemoteLib), serverurl);
            ChannelServices.UnregisterChannel(channel);
            return obj;
        }

    }
}
