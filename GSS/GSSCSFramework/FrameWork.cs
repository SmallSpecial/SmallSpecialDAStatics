using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using GSS.DBUtility;
namespace GSSCSFrameWork
{
    /// <summary>
    /// 网络通讯事件模型委托
    /// </summary>
    public delegate void NetEvent(object sender, NetEventArgs e);

    /// <summary>
    /// 提供TCP连接服务的服务器类
    /// </summary>
    public class TcpSvr
    {
        #region 定义字段

        /// <summary>
        /// 默认的服务器最大连接客户端端数据
        /// </summary>
        public const int DefaultMaxClient = 1024;

        /// <summary>
        /// 接收数据缓冲区大小64K
        /// </summary>
        public const int DefaultBufferSize = 64000 * 1024;

        /// <summary>
        /// 最大数据报文大小
        /// </summary>
        public const int MaxDatagramSize = 64000 * 1024;

        /// <summary>
        /// 报文解析器
        /// </summary>
        private DatagramResolver _resolver;

        /// <summary>
        /// 通讯格式编码解码器
        /// </summary>
        private Coder _coder;

        /// <summary>
        /// 服务器程序使用的IP
        /// </summary>
        private IPAddress _svrip;

        /// <summary>
        /// 服务器程序使用的端口
        /// </summary>
        private ushort _port;

        /// <summary>
        /// 服务器程序允许的最大客户端连接数
        /// </summary>
        private ushort _maxClient;

        /// <summary>
        /// 服务器的运行状态
        /// </summary>
        private bool _isRun;

        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        private byte[] _recvDataBuffer;
        /// <summary>
        /// 服务器使用的异步Socket类,
        /// </summary>
        private Socket _svrSock;

        /// <summary>
        /// 保存所有客户端会话的哈希表
        /// </summary>
        private Hashtable _sessionTable;
        public System.Collections.Generic.Dictionary<int, object> dic = new System.Collections.Generic.Dictionary<int, object>();

        /// <summary>
        /// 当前的连接的客户端数
        /// </summary>
        private ushort _clientCount;

        #endregion

        #region 事件定义

        /// <summary>
        /// 客户端建立连接事件
        /// </summary>
        public event NetEvent ClientConn;

        /// <summary>
        /// 客户端关闭事件
        /// </summary>
        public event NetEvent ClientClose;

        /// <summary>
        /// 服务器已经满事件
        /// </summary>
        public event NetEvent ServerFull;

        /// <summary>
        /// 服务器接收到数据事件
        /// </summary>
        public event NetEvent RecvData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        /// <param name="maxClient">服务器能容纳客户端的最大能力</param>
        /// <param name="encodingMothord">通讯的编码方式</param>
        public TcpSvr(IPAddress svrip, ushort port, ushort maxClient, Coder coder)
        {
            _svrip = svrip;
            _port = port;
            _maxClient = maxClient;
            _coder = coder;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        /// <param name="maxClient">服务器能容纳客户端的最大能力</param>
        /// <param name="encodingMothord">通讯的编码方式</param>
        public TcpSvr(ushort port, ushort maxClient, Coder coder)
        {
            _port = port;
            _maxClient = maxClient;
            _coder = coder;
        }


        /// <summary>
        /// 构造函数(默认使用Default编码方式)
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        /// <param name="maxClient">服务器能容纳客户端的最大能力</param>
        public TcpSvr(ushort port, ushort maxClient)
        {
            _port = port;
            _maxClient = maxClient;
            _coder = new Coder(Coder.EncodingMothord.UTF8);
        }


        /// <summary>
        /// 构造函数(默认使用Default编码方式和DefaultMaxClient(100)个客户端的容量)
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        public TcpSvr(ushort port)
            : this(port, DefaultMaxClient)
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 服务器的Socket对象
        /// </summary>
        public Socket ServerSocket
        {
            get
            {
                return _svrSock;
            }
        }

        /// <summary>
        /// 数据报文分析器
        /// </summary>
        public DatagramResolver Resovlver
        {
            get
            {
                return _resolver;
            }
            set
            {
                _resolver = value;
            }
        }

        /// <summary>
        /// 客户端会话数组,保存所有的客户端,不允许对该数组的内容进行修改
        /// </summary>
        public Hashtable SessionTable
        {
            get
            {
                return _sessionTable;
            }
        }

        /// <summary>
        /// 服务器可以容纳客户端的最大能力
        /// </summary>
        public int Capacity
        {
            get
            {
                return _maxClient;
            }
        }

        /// <summary>
        /// 当前的客户端连接数
        /// </summary>
        public int SessionCount
        {
            get
            {
                return _clientCount;
            }
        }

        /// <summary>
        /// 服务器运行状态
        /// </summary>
        public bool IsRun
        {
            get
            {
                return _isRun;
            }

        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 启动服务器程序,开始监听客户端请求
        /// </summary>
        public virtual void Start()
        {
            if (_isRun)
            {
                throw (new ApplicationException("服务已经在运行."));
            }
            _sessionTable = new Hashtable(1024);
            _recvDataBuffer = new byte[DefaultBufferSize];
            _svrSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //IPEndPoint iep = new IPEndPoint(IPAddress.Any, _port);
            IPEndPoint iep = new IPEndPoint(_svrip, _port);
            iep.ToString().Logger();
            _svrSock.Bind(iep);
            _svrSock.Listen(1024);
            _svrSock.BeginAccept(new AsyncCallback(AcceptConn), _svrSock);

            _isRun = true;
        }

        /// <summary>
        /// 停止服务器程序,所有与客户端的连接将关闭
        /// </summary>
        public virtual void Stop()
        {
            if (!_isRun)
            {
                throw (new ApplicationException("服务已经停止"));
            }
            _isRun = false;
            if (_svrSock.Connected)
            {
                _svrSock.Shutdown(SocketShutdown.Both);
            }

            CloseAllClient();
            _svrSock.Close();
            _sessionTable = null;
            dic = null;
        }


        /// <summary>
        /// 关闭所有的客户端会话,与所有的客户端连接会断开
        /// </summary>
        public virtual void CloseAllClient()
        {
            foreach (Session client in _sessionTable.Values)
            {
                client.Close();
            }

            _clientCount = 0;
            _sessionTable.Clear();
            dic.Clear();
        }

        /// <summary>
        /// 关闭一个与客户端之间的会话
        /// </summary>
        /// <param name="closeClient">需要关闭的客户端会话对象</param>
        public virtual void CloseSession(Session closeClient)
        {
            Debug.Assert(closeClient != null);

            if (closeClient != null)
            {

                closeClient.Datagram = null;
                _sessionTable.Remove(closeClient.ID);
                dic.Remove(closeClient.ID.ID);
                _clientCount--;

                //客户端强制关闭链接
                if (ClientClose != null)
                {
                    ClientClose(this, new NetEventArgs(closeClient));
                }

                closeClient.Close();
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="recvDataClient">接收数据的客户端会话</param>
        /// <param name="datagram">数据报文</param>
        public virtual void Send(Session recvDataClient, MsgStruts msg)
        {
            if (msg == null)
            {
                return;
            }

            //获得报文的编码字节


            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//二进制序列化类
            bf.Serialize(ms, msg);//将消息类转换为内存流
            ms.Position = 0;
            //    byte[] data = ms.GetBuffer();

            byte[] data = new byte[ms.Length];
            data = ms.ToArray();
            string.Format("send byte:[{0}]", data.Length).Logger();
            ms.Flush();

            recvDataClient.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), recvDataClient.ClientSocket);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="recvDataClient">接收数据的客户端会话</param>
        /// <param name="datagram">数据报文</param>
        public virtual void Send(Session recvDataClient, string datagram)
        {
            //获得数据编码
            byte[] data = _coder.GetEncodingBytes(datagram);

            recvDataClient.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), recvDataClient.ClientSocket);
        }

        #endregion

        #region 受保护方法
        /// <summary>
        /// 关闭一个客户端Socket,首先需要关闭Session
        /// </summary>
        /// <param name="client">目标Socket对象</param>
        /// <param name="exitType">客户端退出的类型</param>
        protected virtual void CloseClient(Socket client, Session.ExitType exitType)
        {
            Debug.Assert(client != null);

            //查找该客户端是否存在,如果不存在,抛出异常
            Session closeClient = FindSession(client);

            closeClient.TypeOfExit = exitType;

            if (closeClient != null)
            {
                CloseSession(closeClient);
            }
            else
            {
                throw (new ApplicationException("需要关闭的Socket对象不存在"));
            }
        }

        /// <summary>
        /// 客户端连接处理函数
        /// </summary>
        /// <param name="iar">欲建立服务器连接的Socket对象</param>
        protected virtual void AcceptConn(IAsyncResult iar)
        {
            //如果服务器停止了服务,就不能再接收新的客户端
            if (!_isRun)
            {
                return;
            }

            //接受一个客户端的连接请求
            Socket oldserver = (Socket)iar.AsyncState;
            Socket client = oldserver.EndAccept(iar);

            //检查是否达到最大的允许的客户端数目
            if (_clientCount == _maxClient)
            {
                if (ServerFull != null)
                {
                    ServerFull(this, new NetEventArgs(new Session(client)));
                }
            }
            else
            {
                //新建一个客户端连接
                Session newSession = new Session(client);
                _sessionTable.Add(newSession.ID, newSession);
                dic.Add(newSession.ID.ID, newSession);


                _clientCount++;

                newSession.RecvDataBuffer = _recvDataBuffer;
                //开始接受来自该客户端的数据
                client.BeginReceive(newSession.RecvDataBuffer, 0, newSession.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(ReceiveData), newSession);

                //新的客户段连接,发出通知
                if (ClientConn != null)
                {
                    ClientConn(this, new NetEventArgs(newSession));
                }
            }

            //继续接受客户端
            _svrSock.BeginAccept(new AsyncCallback(AcceptConn), _svrSock);
        }

        /// <summary>
        /// 通过Socket对象查找Session对象
        /// </summary>
        /// <param name="client"></param>
        /// <returns>找到的Session对象,如果为null,说明并不存在该会话</returns>
        private Session FindSession(Socket client)
        {
            SessionId id = new SessionId((int)client.Handle);
            return (Session)_sessionTable[id];
        }

        /// <summary>
        /// 接受数据完成处理函数，异步的特性就体现在这个函数中，
        /// 收到数据后，会自动解析为字符串报文(消息结构体)
        /// </summary>
        /// <param name="iar">目标客户端Socket</param>
        protected virtual void ReceiveData(IAsyncResult iar)
        {
            string.Format("class :{0},function:ReceiveData", typeof(TcpSvr).Name).Logger();
            Session sendDataSession = (Session)iar.AsyncState;
            Socket client = sendDataSession.ClientSocket;

            try
            {
                //如果两次开始了异步的接收,所以当客户端退出的时候
                //会两次执行EndReceive
                int recv = client.EndReceive(iar);
                "1->1".Logger();
                if (recv == 0)
                {
                    CloseClient(client, Session.ExitType.NormalExit);
                    return;
                }




                //发布收到数据的事件
                if (RecvData != null)
                {
                    "2->1".Logger();
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    ms.Write(sendDataSession.RecvDataBuffer, 0, sendDataSession.RecvDataBuffer.Length);
                    ms.Position = 0;
                    MsgStruts msg = (MsgStruts)bf.Deserialize(ms);
                    ms.Close();

                    //Session sendDataSession = FindSession(client);

                    Debug.Assert(sendDataSession != null);

                    if (msg.msgsendstate != msgSendState.single)
                    {
                        "2->2".Logger();
                        if (sendDataSession.MsgStrut != null && msg.msgsendstate == msgSendState.start)
                        {
                            sendDataSession.MsgStrut = msg;
                        }
                        else
                        {
                            "2->3".Logger();
                            int aInt = sendDataSession.MsgStrut.Data.Length;
                            int bInt = msg.Data.Length;
                            byte[] bytes = new byte[aInt + bInt];
                            sendDataSession.MsgStrut.Data.CopyTo(bytes, 0);
                            msg.Data.CopyTo(bytes, aInt);
                            sendDataSession.MsgStrut.Data = bytes;
                        }

                        if (msg.msgsendstate == msgSendState.end)
                        {
                            "2->4".Logger();
                            ICloneable copySession = (ICloneable)sendDataSession;
                            Session clientSession = (Session)copySession.Clone();
                            clientSession.MsgStrut = sendDataSession.MsgStrut;
                            RecvData(this, new NetEventArgs(clientSession));
                        }

                        if (sendDataSession.MsgStrut.Data.Length > MaxDatagramSize)
                        {
                            "2->5".Logger();
                            sendDataSession.MsgStrut = null;
                        }
                    }
                    else
                    {
                        "2->6".Logger();
                        ICloneable copySession = (ICloneable)sendDataSession;
                        Session clientSession = (Session)copySession.Clone();
                        //clientSession.ClassName = this.GetClassFullName(ref receivedData);
                        clientSession.MsgStrut = msg;
                        //sendDataSession.RecvDataBuffer = null;
                        RecvData(this, new NetEventArgs(clientSession));
                    }
                }

                //继续接收来自来客户端的数据
                sendDataSession.RecvDataBuffer = new byte[DefaultBufferSize];
                client.BeginReceive(sendDataSession.RecvDataBuffer, 0, sendDataSession.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(ReceiveData), sendDataSession);
            }
            catch (SocketException ex)
            {
                ex.Message.ErrorLogger();
                if (10054 == ex.ErrorCode || 10053 == ex.ErrorCode)
                {
                    //客户端强制关闭
                    CloseClient(client, Session.ExitType.ExceptionExit);
                }

            }
            catch (ObjectDisposedException ex)
            {
                ex.Message.ErrorLogger();
                if (ex != null)
                {
                    ex = null;
                    //DoNothing;
                    CloseClient(client, Session.ExitType.ExceptionExit);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ErrorLogger();
                if (ex != null)
                {
                    ex = null;
                    //DoNothing;
                    CloseClient(client, Session.ExitType.ExceptionExit);
                }
            }
        }

        /// <summary>
        /// 接受数据完成处理函数，异步的特性就体现在这个函数中，
        /// 收到数据后，会自动解析为字符串报文
        /// </summary>
        /// <param name="iar">目标客户端Socket</param>
        protected virtual void ReceiveDataStr(IAsyncResult iar)
        {
            Session sendDataSession = (Session)iar.AsyncState;
            Socket client = sendDataSession.ClientSocket;

            try
            {
                //如果两次开始了异步的接收,所以当客户端退出的时候
                //会两次执行EndReceive
                int recv = client.EndReceive(iar);

                if (recv == 0)
                {
                    CloseClient(client, Session.ExitType.NormalExit);
                    return;
                }

                string receivedData = _coder.GetEncodingString(sendDataSession.RecvDataBuffer, recv);
                //string receivedData = _coder.GetEncodingString(_recvDataBuffer, recv);

                //发布收到数据的事件
                if (RecvData != null)
                {
                    //Session sendDataSession = FindSession(client);

                    Debug.Assert(sendDataSession != null);

                    if (_resolver != null)
                    {
                        if (sendDataSession.Datagram != null && sendDataSession.Datagram.Length != 0)
                        {
                            receivedData = sendDataSession.Datagram + receivedData;
                        }

                        string[] recvDatagrams = _resolver.Resolve(ref receivedData);

                        foreach (string newDatagram in recvDatagrams)
                        {
                            ICloneable copySession = (ICloneable)sendDataSession;
                            Session clientSession = (Session)copySession.Clone();

                            string strDatagram = newDatagram;
                            //clientSession.ClassName = this.GetClassFullName(ref strDatagram);
                            clientSession.Datagram = strDatagram;

                            //发布一个报文消息
                            RecvData(this, new NetEventArgs(clientSession));
                        }

                        //剩余的代码片断,下次接收的时候使用
                        sendDataSession.Datagram = receivedData;

                        if (sendDataSession.Datagram.Length > MaxDatagramSize)
                        {
                            sendDataSession.Datagram = null;
                        }
                    }
                    else
                    {
                        ICloneable copySession = (ICloneable)sendDataSession;
                        Session clientSession = (Session)copySession.Clone();
                        //clientSession.ClassName = this.GetClassFullName(ref receivedData);
                        clientSession.Datagram = receivedData;

                        RecvData(this, new NetEventArgs(clientSession));
                    }
                }

                //继续接收来自来客户端的数据
                client.BeginReceive(sendDataSession.RecvDataBuffer, 0, sendDataSession.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(ReceiveData), sendDataSession);
            }
            catch (SocketException ex)
            {
                ex.ToString().ErrorLogger();
                if (10054 == ex.ErrorCode)
                {
                    //客户端强制关闭
                    CloseClient(client, Session.ExitType.ExceptionExit);
                }

            }
            catch (ObjectDisposedException ex)
            {
                ex.ToString().ErrorLogger();
                if (ex != null)
                {
                    ex = null;
                    //DoNothing;
                }
            }
        }

        /// <summary>
        /// 发送数据完成处理函数
        /// </summary>
        /// <param name="iar">目标客户端Socket</param>
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            try
            {
                Socket client = (Socket)iar.AsyncState;
                string.Format("RemoteEndPoint->"+ client.RemoteEndPoint).Logger();
                int sent = client.EndSend(iar);
                Debug.Assert(sent != 0);
            }
            catch (System.Exception ex)
            {
                ex.Message.ErrorLogger();
            }

        }
        /// <summary>
        /// 协议解析方法
        /// </summary>
        /// <param name="Term"></param>
        /// <returns></returns>
        private string GetClassFullName(ref string Term)
        {
            //{[object name][channel][request id][|param1|param2|param3|...|]}

            string ClassFullName = Term.Substring(2, Term.IndexOf(']') - 2);
            Term = "{" + Term.Substring(Term.IndexOf(']') + 1);

            return ClassFullName;
        }

        #endregion
        /// <summary>
        /// while the send data is more than the package ,network or routing will auto change to some small package[ in head ,the sizeof(int)  of length byte is representative the nature data]
        /// </summary>
        /// <param name="recvDataClient"></param>
        /// <param name="msg"></param>
        public void SendDataContainerBuffer(Session recvDataClient, MsgStruts msg)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//二进制序列化类
            bf.Serialize(ms, msg);//将消息类转换为内存流
            ms.Position = 0;
            //    byte[] data = ms.GetBuffer();

            byte[] data = new byte[ms.Length];
            data = ms.ToArray();
            ms.Flush();
            int m = data.Length;
            byte[] arry = new byte[sizeof(int)];
            arry[0] = (byte)(m & 0xFF);
            arry[1] = (byte)((m & 0xFF00) >> 8);
            arry[2] = (byte)((m & 0xFF0000) >> 16);
            arry[3] = (byte)((m >> 24) & 0xFF);
            List<byte> buffer = new List<byte>();
            buffer.AddRange(arry);
            buffer.AddRange(data);// the send data (send data= length+ data)
            //socket  缓存区大小默认值为8192字节（8kb）
            for (int offset = 0; offset < buffer.Count;)
            {
               int sned= recvDataClient.ClientSocket.Send(buffer.ToArray(), offset, buffer.Count, SocketFlags.None);
               offset += sned;
            }
            //recvDataClient.ClientSocket.BeginSend(buffer.ToArray(), 0, buffer.Count, SocketFlags.None,
            // new AsyncCallback(SendDataEnd), recvDataClient.ClientSocket);
        }
    }
    public class CacheData 
    {
        public static Dictionary<string, object> ClientCacheData = new Dictionary<string, object>();
    }

    /// <summary>
    /// 提供Tcp网络连接服务的客户端类
    /// </summary>
    public class TcpCli
    {
        #region 字段

        /// <summary>
        /// 客户端与服务器之间的会话类
        /// </summary>
        private Session _session;

        /// <summary>
        /// 客户端是否已经连接服务器
        /// </summary>
        private bool _isConnected = false;

        /// <summary>
        /// 接收数据缓冲区大小640K
        /// </summary>
        public const int DefaultBufferSize = 64000 * 1024;
        int PackReceiveBuffLength = 1024;
        /// <summary>
        /// 报文解析器
        /// </summary>
        private DatagramResolver _resolver;

        /// <summary>
        /// 通讯格式编码解码器
        /// </summary>
        private Coder _coder;
        /// <summary>
        /// IP
        /// </summary>
        private string _localip;
        /// <summary>
        /// 端口号
        /// </summary>
        private int _localport;
        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        private byte[] _recvDataBuffer = new byte[DefaultBufferSize];
        #endregion

        #region 事件定义

        /// <summary>
        /// 无法连接到服务器事件
        /// </summary>
        public event NetEvent CannotConnectedServer;

        /// <summary>
        /// 已经连接服务器事件
        /// </summary>
        public event NetEvent ConnectedServer;

        /// <summary>
        /// 接收到数据报文事件
        /// </summary>
        public event NetEvent ReceivedDatagram;

        /// <summary>
        /// 连接断开事件
        /// </summary>
        public event NetEvent DisConnectedServer;

        public AsyncCallback ReceiverDataComplate { get; set; }
        #endregion

        #region 属性

        /// <summary>
        /// 返回客户端与服务器之间的会话对象
        /// </summary>
        public Session ClientSession
        {
            get
            {
                return _session;
            }
        }

        /// <summary>
        /// 返回客户端与服务器之间的连接状态
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }

        /// <summary>
        /// 数据报文分析器
        /// </summary>
        public DatagramResolver Resovlver
        {
            get
            {
                return _resolver;
            }
            set
            {
                _resolver = value;
            }
        }

        /// <summary>
        /// 编码解码器
        /// </summary>
        public Coder ServerCoder
        {
            get
            {
                return _coder;
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 默认构造函数,使用默认的编码格式
        /// </summary>
        public TcpCli()
        {
            DefaultEvent();
        }
        void DefaultEvent() 
        {
            _coder = new Coder(Coder.EncodingMothord.UTF8);
        }
        /// <summary>
        /// 构造函数,使用一个特定的编码器来初始化
        /// </summary>
        /// <param name="_coder">报文编码器</param>
        public TcpCli(Coder coder)
        {
            _coder = coder;
        }

        /// <summary>
        /// 构造函数,网络初始化
        /// </summary>
        public TcpCli(string ip, int port)
        {
            _localip = ip;
            _localport = port;
            DefaultEvent();
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip">服务器IP地址</param>
        /// <param name="port">服务器端口</param>
        public virtual void Connect(string ip, int port)
        {
            /*
            if (IsConnected)
            {
                Debug.Assert(_session != null);
                Close();
            }
            */
            _localip = ip;
            _localport = port;

            Socket newsock = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);


        }

        /// <summary>
        /// 发送数据报文
        /// </summary>
        /// <param name="datagram"></param>
        public virtual void Send(string datagram)
        {
            if (datagram.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                string msg = "没有连接服务器，不能发送数据";
                msg.ErrorLogger();
                throw (new ApplicationException(msg));
            }

            //获得报文的编码字节
            byte[] data = _coder.GetEncodingBytes(datagram);

            _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), _session.ClientSocket);
        }

        /// <summary>
        /// 发送数据报文
        /// </summary>
        /// <param name="datagram"></param>
        public virtual void Send(MsgStruts msg)
        {
            try{
            if (msg == null)
            {
                return;
            }

            if (!_isConnected)
            {
                Debug.Assert(_session != null);
               // Close();
                Connect(_localip, _localport);
            }

            //获得报文的编码字节


            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//二进制序列化类
            bf.Serialize(ms, msg);//将消息类转换为内存流
            ms.Position = 0;
            byte[] data = new byte[ms.Length];
            data = ms.ToArray();
            ms.Flush();
            string.Format("Remote service :[{0}]", _session.ClientSocket.RemoteEndPoint).Logger();
            _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), _session.ClientSocket);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public virtual void Close()
        {
            if (!_isConnected)
            {
                return;
            }

            _session.Close();
            _session = null;
            _isConnected = false;
        }
        TcpClient InitClient() 
        {
            TcpClient tcpclnt;
            string key = typeof(TcpClient).Name;
            if (CacheData.ClientCacheData.ContainsKey(key))
            {
                tcpclnt = CacheData.ClientCacheData[key] as TcpClient;
            }
            else
            {
                tcpclnt = new TcpClient();
                tcpclnt.SendTimeout = 5000;
                tcpclnt.Connect(_localip, _localport);
                CacheData.ClientCacheData.Add(key, tcpclnt);
            }
            return tcpclnt;
        }
        /// <summary>
        /// 同步方法
        /// </summary>
        public MsgStruts SendAndBack(MsgStruts msg)
        {
            if (_localip == null)
            {
                return null;
            }
            try
            {
                TcpClient tcpclnt;
                string key = typeof(TcpClient).Name;
                if (CacheData.ClientCacheData.ContainsKey(key))
                {
                    tcpclnt = CacheData.ClientCacheData[key] as TcpClient;
                }
                else 
                {
                    tcpclnt = new TcpClient();
                    tcpclnt.SendTimeout = 5000;
                    tcpclnt.Connect(_localip, _localport);
                   // CacheData.ClientCacheData.Add(key, tcpclnt);
                }
                
                Stream stm = tcpclnt.GetStream();
                byte[] data = null;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//二进制序列化类
                    bf.Serialize(ms, msg);//将消息类转换为内存流
                    System.Threading.Thread.Sleep(300);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    data = ms.ToArray();
                    ms.Flush();
                }
               
                stm.Write(data, 0, data.Length);
                Socket socket = tcpclnt.Client;
                if (msg.command == msgCommand.GetCache)
                {
                    string sec = System.Configuration.ConfigurationManager.AppSettings["GetCacheSleepSec"];
                    sec = string.IsNullOrEmpty(sec) ? "3000" : sec;
                    int wait = int.Parse(sec);
                    System.Threading.Thread.Sleep(wait);//延时不能保证数据接收完毕
                }
                else
                {
                    System.Threading.Thread.Sleep(300);//延时不能保证数据接收完毕
                }
                //bool end=true;
                //int pos = 0;
                //_recvDataBuffer = new byte[1024];
                //socket.Receive(_recvDataBuffer, 0, _recvDataBuffer.Length, SocketFlags.None);
                //socket.BeginReceive(_recvDataBuffer, 0, _recvDataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiverDataComplate), socket);
                //while (end)
                //{
                //    int length = soc.Available;//返回数据的自己流量大小
                //    pos = soc.Receive(RecvDataBuffer, pos, RecvDataBuffer.Length - pos, SocketFlags.None);
                //    //int  pos = soc.ReceiveFrom(RecvDataBuffer, ref point);
                //    if (pos == 0 || (pos <RecvDataBuffer.Length || soc.Available == 0))
                //    {
                //        end = false;
                //    }
                //    read.AddRange(RecvDataBuffer);
                //}
                byte[] RecvDataBuffer = new byte[DefaultBufferSize];
                int k = stm.Read(RecvDataBuffer, 0, RecvDataBuffer.Length);
                System.IO.MemoryStream msb = new System.IO.MemoryStream();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bfb = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                msb.Write(RecvDataBuffer, 0, RecvDataBuffer.Length);
                msb.Position = 0;
                MsgStruts msgb =  (MsgStruts)bfb.Deserialize(msb);
                Logger.Log.Info(msgb.Data);
                //msgb.command = msg.command;
                //stm.Close();
                //tcpclnt.Close();
               
                return msgb;
            }
            catch (System.Exception ex)
            {
                MsgStruts msge = new MsgStruts();
                ex.Message.ErrorLogger();
                msge.Data = _coder.GetEncodingBytes(ex.Message);
                return msge;
            }

        }

        #endregion



        #region 受保护方法

        /// <summary>
        /// 数据发送完成处理函数
        /// </summary>
        /// <param name="iar"></param>
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            string.Format("LocalEndPoint->{0}", remote.LocalEndPoint).Logger();
            Debug.Assert(sent != 0);
        }

        /// <summary>
        /// 建立Tcp连接后处理过程
        /// </summary>
        /// <param name="iar">异步Socket</param>
        protected virtual void Connected(IAsyncResult iar)
        {
            Socket socket = (Socket)iar.AsyncState;
            try
            {
                socket.EndConnect(iar);
            }
            catch (SocketException ex)
            {
                ex.Message.ErrorLogger();
                //触发无法建立连接事件
                if (CannotConnectedServer != null)
                {
                    //创建新的会话
                    _session = new Session(socket);
                    _isConnected = false;
                    _session.Datagram = ex.Message;
                    CannotConnectedServer(this, new NetEventArgs(_session));

                }
                return;
            }


            //创建新的会话
            _session = new Session(socket);
            _isConnected = true;

            //触发连接建立事件
            if (ConnectedServer != null)
            {
                ConnectedServer(this, new NetEventArgs(_session));
            }
            _session.ClientSocket.BeginReceive(_recvDataBuffer, 0,
             DefaultBufferSize, SocketFlags.None,
             new AsyncCallback(RecvData), socket);
        }

        /// <summary>
        /// 数据接收处理函数(消息结构体)
        /// </summary>
        /// <param name="iar">异步Socket</param>
        protected virtual void RecvData(IAsyncResult iar)
        {
            string.Format("class:[{0}],function:[RecvData]", typeof(TcpCli).Name).Logger();
            Socket remote = (Socket)iar.AsyncState;

            try
            {
                int recv = remote.EndReceive(iar);

                if (recv == 0)
                {
                    _session.TypeOfExit = Session.ExitType.NormalExit;

                    _isConnected = false;
                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }

                    return;
                }

                // string receivedData = _coder.GetEncodingString(_recvDataBuffer, recv);


                //通过事件发布收到的报文
                if (ReceivedDatagram != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    ms.Write(_recvDataBuffer, 0, _recvDataBuffer.Length);
                    ms.Position = 0;
                    string.Format("the one small bag of bytes:[0]", _recvDataBuffer.Length).Logger();
                    MsgStruts msg = (MsgStruts)bf.Deserialize(ms);

                    if (msg.msgsendstate != msgSendState.single)
                    {
                        if (_session.MsgStrut != null && msg.msgsendstate == msgSendState.start)
                        {
                            _session.MsgStrut = msg;
                        }
                        else
                        {
                            int aInt = _session.MsgStrut.Data.Length;
                            int bInt = msg.Data.Length;
                            byte[] bytes = new byte[aInt + bInt];
                            _session.MsgStrut.Data.CopyTo(bytes, 0);
                            msg.Data.CopyTo(bytes, aInt);
                            _session.MsgStrut.Data = bytes;
                        }
                        if (msg.msgsendstate == msgSendState.end)
                        {
                            ICloneable copySession = (ICloneable)_session;
                            Session clientSession = (Session)copySession.Clone();
                            clientSession.MsgStrut = _session.MsgStrut;
                            ReceivedDatagram(this, new NetEventArgs(clientSession));
                        }

                    }
                    else
                    {
                        ICloneable copySession = (ICloneable)_session;
                        Session clientSession = (Session)copySession.Clone();
                        clientSession.MsgStrut = msg;

                        ReceivedDatagram(this, new NetEventArgs(clientSession));
                    }

                }//end of if(ReceivedDatagram != null)

                //继续接收数据
                if (_session != null && _session.ClientSocket.Connected)
                {
                    _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(RecvData), _session.ClientSocket);
                }

            }
            catch (SocketException ex)
            {
                ex.Message.ErrorLogger();
                _isConnected = false;
                //客户端退出
                if (10054 == ex.ErrorCode)
                {
                    _session.TypeOfExit = Session.ExitType.ExceptionExit;

                    _isConnected = false;
                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                }
                //else
                //{
                //    throw (ex);
                //}
            }
            catch (ObjectDisposedException ex)
            {
                ex.Message.ErrorLogger();
                _isConnected = false;
                if (ex != null)
                {
                    ex = null;
                    //DoNothing;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ErrorLogger();
                //System.Windows.Forms.MessageBox.Show(ex.Message, ex.GetType().ToString());
                if (ex != null)
                {
                    ex = null;
                }

                //继续接收数据
                if (_session != null && _session.ClientSocket.Connected)
                {
                    _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(RecvData), _session.ClientSocket);
                }
            }

        }

        /// <summary>
        /// 数据接收处理函数(字符串)
        /// </summary>
        /// <param name="iar">异步Socket</param>
        protected virtual void RecvDataStr(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;

            try
            {
                int recv = remote.EndReceive(iar);

                if (recv == 0)
                {
                    _session.TypeOfExit = Session.ExitType.NormalExit;

                    _isConnected = false;
                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }

                    return;
                }

                string receivedData = _coder.GetEncodingString(_recvDataBuffer, recv);

                //通过事件发布收到的报文
                if (ReceivedDatagram != null)
                {
                    if (_resolver != null)
                    {
                        if (_session.Datagram != null && _session.Datagram.Length != 0)
                        {
                            receivedData = _session.Datagram + receivedData;
                        }

                        string[] recvDatagrams = _resolver.Resolve(ref receivedData);
                        foreach (string newDatagram in recvDatagrams)
                        {
                            ICloneable copySession = (ICloneable)_session;
                            Session clientSession = (Session)copySession.Clone();
                            clientSession.Datagram = newDatagram;

                            //发布一个报文消息
                            ReceivedDatagram(this, new NetEventArgs(clientSession));
                        }

                        //剩余的代码片断,下次接收的时候使用
                        _session.Datagram = receivedData;
                    }
                    else
                    {
                        ICloneable copySession = (ICloneable)_session;
                        Session clientSession = (Session)copySession.Clone();
                        clientSession.Datagram = receivedData;

                        ReceivedDatagram(this, new NetEventArgs(clientSession));
                    }

                }//end of if(ReceivedDatagram != null)

                //继续接收数据
                _recvDataBuffer = new byte[DefaultBufferSize];
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None,
                 new AsyncCallback(RecvData), _session.ClientSocket);
            }
            catch (SocketException ex)
            {
                ex.Message.ErrorLogger();
                //客户端退出
                if (10054 == ex.ErrorCode)
                {
                    _session.TypeOfExit = Session.ExitType.ExceptionExit;

                    _isConnected = false;

                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                }
                else
                {
                    throw (ex);
                }
            }
            catch (ObjectDisposedException ex)
            {
                ex.Message.ErrorLogger();
                _isConnected = false;
                if (ex != null)
                {
                    ex = null;
                    //DoNothing;
                }
            }
        }
        public void AsyncReceiveData(IAsyncResult iar)
        {//异步加载文件流 
        
        }
        #endregion
        List<byte> ResponseData = new List<byte>();
        byte[] partRequestData;
        TcpClient tcpclnt;//Keep connection service
        int ResponseDataLength = 0;
        CallBackEvent receiveComplateEvent;
        public void AsyncSendWithReceiverData(MsgStruts msg, CallBackEvent receiveComplateCallBack) 
        {
            receiveComplateEvent = receiveComplateCallBack;
            tcpclnt = InitClient();
            Stream stm = tcpclnt.GetStream();
            byte[] data = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//二进制序列化类
                bf.Serialize(ms, msg);//将消息类转换为内存流
                ms.Position = 0;
                data = new byte[ms.Length];
                data = ms.ToArray();
                ms.Flush();
            }
            stm.Write(data, 0, data.Length);
            Socket soc = tcpclnt.Client;
            partRequestData = new byte[PackReceiveBuffLength];
            soc.BeginReceive(partRequestData, 0, partRequestData.Length, SocketFlags.None, new AsyncCallback(ReceivePackageData), soc);
        }
        private void ReceivePackageData(IAsyncResult iar)
        {
            Socket soc = iar.AsyncState  as Socket;
            byte[] aimData = null;
            if (ResponseDataLength == 0)
            { //改请求返回的数据长度【自定义消息=请求数据长度+报文内容】
                int buffLen = sizeof(int);
                if (partRequestData.Length < buffLen)
                {
                    soc.BeginReceive(partRequestData, 0, partRequestData.Length, SocketFlags.None, new AsyncCallback(ReceivePackageData), soc);
                    return;
                }
                byte[] len = new byte[buffLen];
                Array.Copy(partRequestData, len, buffLen);
                ResponseDataLength = BitConverter.ToInt32(len, 0);
                //剔除报文长度占用的字节
                aimData = new byte[partRequestData.Length - buffLen];
                Array.Copy(partRequestData, buffLen, aimData, 0, aimData.Length);
            }
            else
            {
                aimData = partRequestData;
            }
            ResponseData.AddRange(aimData);
            if (ResponseData.Count > ResponseDataLength)
            {
                byte[] bts = new byte[ResponseDataLength];
                Array.Copy(ResponseData.ToArray(), bts, bts.Length);
                ResponseData.Clear();
                ResponseData.AddRange(bts);
            }
            if (ResponseDataLength == ResponseData.Count)
            {//该请求响应返回的多个数据包已成功接收
                
                ResponseDataLength = 0;//重置报文长度 
                ComplateReceiveDataCallBack();//本次数据接收完毕
            }
            soc.BeginReceive(partRequestData, 0, partRequestData.Length, SocketFlags.None, new AsyncCallback(ReceivePackageData), soc);
        }
        void ComplateReceiveDataCallBack() 
        {
            System.IO.MemoryStream msb = new System.IO.MemoryStream(ResponseData.ToArray());
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bfb = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
           // msb.Write(ResponseData.ToArray(), 0, ResponseData.Count);
            //msb.Position = 0;
            MsgStruts msgb = new MsgStruts();
            msgb = (MsgStruts)bfb.Deserialize(msb);
            ResponseData = new List<byte>();
            receiveComplateEvent(msgb);
        }
    }
    public delegate void CallBackEvent (object param);
    public class Logger 
    {
        static Logger() 
        {
            if (Log == null)
            {
                Log = log4net.LogManager.GetLogger(Dir);
            }
        }
        public static string Dir 
        {
            get 
            {
                string dir = System.Configuration.ConfigurationManager.AppSettings["ErrorLogDir"];
                if (string.IsNullOrEmpty(dir))
                {
                    dir = typeof(Logger).Name;
                }
                return dir;
            }
        }
        public static log4net.ILog Log { get; set; }
    }
    public class WriteLogger 
    {
        public  static void Info(string log)
        {
            bool openLogFun = System.Configuration.ConfigurationManager.AppSettings["OpenCodeLog"] == "true";
            if (!openLogFun)
            {
                return;
            }
            AssemblyData ass = new AssemblyData();
            string dir = ass.GetAssemblyDir(4) + "\\" + DateTime.Now.Year + TodayOfWeekInYear();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string name = DateTime.Now.ToString("yyyyMMddHH")+".log";
            FileStream file;
            string fullName = dir + "\\" + name;
            if (File.Exists(fullName))
            {
                file = new FileStream(fullName, FileMode.Append, FileAccess.Write, FileShare.Write);
            }
            else 
            {
                file = new FileStream(fullName, FileMode.Create, FileAccess.Write, FileShare.Write);
            }
            StringBuilder text = new StringBuilder();
            text.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            text.AppendLine(log);
            byte[] bytes = Encoding.UTF8.GetBytes(text.ToString());
            int len = bytes.Length;
            file.Write(bytes, 0, bytes.Length);
            file.Flush();
            file.Close();
        }
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
    class AssemblyData 
    {
        public string GetAssemblyDir(int parentLayer)
        {
            System.Reflection.Assembly ass = this.GetType().Assembly;
            DirectoryInfo dir = new DirectoryInfo(ass.Location);
            string path = dir.Parent.FullName;
            string root = dir.Root.FullName;
            while (parentLayer > 0)
            {
                dir = Directory.GetParent(path);
                path = dir.FullName;
                if (path == root)
                {
                    return path;
                }
                parentLayer--;
            }
            return path;
        }
    }
}



