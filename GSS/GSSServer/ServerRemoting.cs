using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using GSSBLL;
using GSSUpdateLib;
using GSSServerLibrary;

namespace GSSServer
{
    /// <summary>
    /// remoting方式
    /// </summary>
    public abstract class ServerRemoting
    {
        // static TcpServerChannel channel = null;
        static TcpChannel tcpChannel = null;
        public ServerRemoting()
        {

        }
        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            IDictionary tcpProperties = new Hashtable();
            tcpProperties["name"] = "tcpBinary";
            tcpProperties["port"] = ShareData.LocalPort + AppConfig.PipeLinePortSpaceNumber;//由于在远程服务器上提供了一个测试端口为5234，与该端口冲突
            tcpProperties["rejectRemoteRequests"] = false;
            tcpProperties["bindTo"] = ShareData.LocalIp;
            BinaryClientFormatterSinkProvider tcpClientSinkProvider = new BinaryClientFormatterSinkProvider();
            BinaryServerFormatterSinkProvider tcpServerSinkProvider = new BinaryServerFormatterSinkProvider();
            tcpServerSinkProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            tcpChannel = new TcpChannel(tcpProperties, tcpClientSinkProvider, tcpServerSinkProvider);

            //ListDictionary channelProperties = new ListDictionary();
            //channelProperties.Add("port", 1234);
            //channelProperties.Add("name", "External");
            //channelProperties.Add("machineName", externalIP);
            //TcpChannel externalChannel = new TcpChannel(channelProperties, new BinaryClientFormatterSinkProvider(), new BinaryServerFormatterSinkProvider());
            //ChannelServices.RegisterChannel(externalChannel);



            // channel = new TcpServerChannel(tcpProperties,tcpClientSinkProvider, tcpServerSinkProvider);
            ChannelServices.RegisterChannel(tcpChannel, false);
            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
            RemotingConfiguration.CustomErrorsEnabled(false);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(FDBISql), "FDBISql", WellKnownObjectMode.SingleCall);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(GameConfig), "GameConfig", WellKnownObjectMode.SingleCall);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Tasks), "Tasks", WellKnownObjectMode.SingleCall);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(TasksLog), "TasksLog", WellKnownObjectMode.SingleCall);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(GSSUpdateLib.NetFileTransfer), "NetFileTransfer", WellKnownObjectMode.SingleCall);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(BLLShareData), "BLLShareData", WellKnownObjectMode.SingleCall);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ServerRemoteLib), "ServerRemoteLib", WellKnownObjectMode.SingleCall);
        }
        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            if (tcpChannel != null)
            {
                ChannelServices.UnregisterChannel(tcpChannel);
            }
        }

    }

    //public class ServerRemoteLib : MarshalByRefObject
    //{
    //    public string RoleNameChange(string gamename, string bigzonename, int roleid, string rolename, string newrolename)
    //    {
    //        return WebServiceLib.RoleNameChange(gamename, bigzonename, roleid, rolename, newrolename);
    //    }
    //    public string RoleZoneChange(string gamename, string bigzonename, int roleid, int zoneid)
    //    {
    //        return WebServiceLib.RoleZoneChange(gamename, bigzonename, roleid, zoneid);
    //    }
        
   // }


}
