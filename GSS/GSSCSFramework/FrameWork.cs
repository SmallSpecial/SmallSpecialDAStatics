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
    /// ����ͨѶ�¼�ģ��ί��
    /// </summary>
    public delegate void NetEvent(object sender, NetEventArgs e);

    /// <summary>
    /// �ṩTCP���ӷ���ķ�������
    /// </summary>
    public class TcpSvr
    {
        #region �����ֶ�

        /// <summary>
        /// Ĭ�ϵķ�����������ӿͻ��˶�����
        /// </summary>
        public const int DefaultMaxClient = 1024;

        /// <summary>
        /// �������ݻ�������С64K
        /// </summary>
        public const int DefaultBufferSize = 64000 * 1024;

        /// <summary>
        /// ������ݱ��Ĵ�С
        /// </summary>
        public const int MaxDatagramSize = 64000 * 1024;

        /// <summary>
        /// ���Ľ�����
        /// </summary>
        private DatagramResolver _resolver;

        /// <summary>
        /// ͨѶ��ʽ���������
        /// </summary>
        private Coder _coder;

        /// <summary>
        /// ����������ʹ�õ�IP
        /// </summary>
        private IPAddress _svrip;

        /// <summary>
        /// ����������ʹ�õĶ˿�
        /// </summary>
        private ushort _port;

        /// <summary>
        /// ������������������ͻ���������
        /// </summary>
        private ushort _maxClient;

        /// <summary>
        /// ������������״̬
        /// </summary>
        private bool _isRun;

        /// <summary>
        /// �������ݻ�����
        /// </summary>
        private byte[] _recvDataBuffer;
        /// <summary>
        /// ������ʹ�õ��첽Socket��,
        /// </summary>
        private Socket _svrSock;

        /// <summary>
        /// �������пͻ��˻Ự�Ĺ�ϣ��
        /// </summary>
        private Hashtable _sessionTable;
        public System.Collections.Generic.Dictionary<int, object> dic = new System.Collections.Generic.Dictionary<int, object>();

        /// <summary>
        /// ��ǰ�����ӵĿͻ�����
        /// </summary>
        private ushort _clientCount;

        #endregion

        #region �¼�����

        /// <summary>
        /// �ͻ��˽��������¼�
        /// </summary>
        public event NetEvent ClientConn;

        /// <summary>
        /// �ͻ��˹ر��¼�
        /// </summary>
        public event NetEvent ClientClose;

        /// <summary>
        /// �������Ѿ����¼�
        /// </summary>
        public event NetEvent ServerFull;

        /// <summary>
        /// ���������յ������¼�
        /// </summary>
        public event NetEvent RecvData;

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="port">�������˼����Ķ˿ں�</param>
        /// <param name="maxClient">�����������ɿͻ��˵��������</param>
        /// <param name="encodingMothord">ͨѶ�ı��뷽ʽ</param>
        public TcpSvr(IPAddress svrip, ushort port, ushort maxClient, Coder coder)
        {
            _svrip = svrip;
            _port = port;
            _maxClient = maxClient;
            _coder = coder;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="port">�������˼����Ķ˿ں�</param>
        /// <param name="maxClient">�����������ɿͻ��˵��������</param>
        /// <param name="encodingMothord">ͨѶ�ı��뷽ʽ</param>
        public TcpSvr(ushort port, ushort maxClient, Coder coder)
        {
            _port = port;
            _maxClient = maxClient;
            _coder = coder;
        }


        /// <summary>
        /// ���캯��(Ĭ��ʹ��Default���뷽ʽ)
        /// </summary>
        /// <param name="port">�������˼����Ķ˿ں�</param>
        /// <param name="maxClient">�����������ɿͻ��˵��������</param>
        public TcpSvr(ushort port, ushort maxClient)
        {
            _port = port;
            _maxClient = maxClient;
            _coder = new Coder(Coder.EncodingMothord.UTF8);
        }


        /// <summary>
        /// ���캯��(Ĭ��ʹ��Default���뷽ʽ��DefaultMaxClient(100)���ͻ��˵�����)
        /// </summary>
        /// <param name="port">�������˼����Ķ˿ں�</param>
        public TcpSvr(ushort port)
            : this(port, DefaultMaxClient)
        {
        }

        #endregion

        #region ����

        /// <summary>
        /// ��������Socket����
        /// </summary>
        public Socket ServerSocket
        {
            get
            {
                return _svrSock;
            }
        }

        /// <summary>
        /// ���ݱ��ķ�����
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
        /// �ͻ��˻Ự����,�������еĿͻ���,������Ը���������ݽ����޸�
        /// </summary>
        public Hashtable SessionTable
        {
            get
            {
                return _sessionTable;
            }
        }

        /// <summary>
        /// �������������ɿͻ��˵��������
        /// </summary>
        public int Capacity
        {
            get
            {
                return _maxClient;
            }
        }

        /// <summary>
        /// ��ǰ�Ŀͻ���������
        /// </summary>
        public int SessionCount
        {
            get
            {
                return _clientCount;
            }
        }

        /// <summary>
        /// ����������״̬
        /// </summary>
        public bool IsRun
        {
            get
            {
                return _isRun;
            }

        }

        #endregion

        #region ���з���

        /// <summary>
        /// ��������������,��ʼ�����ͻ�������
        /// </summary>
        public virtual void Start()
        {
            if (_isRun)
            {
                throw (new ApplicationException("�����Ѿ�������."));
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
        /// ֹͣ����������,������ͻ��˵����ӽ��ر�
        /// </summary>
        public virtual void Stop()
        {
            if (!_isRun)
            {
                throw (new ApplicationException("�����Ѿ�ֹͣ"));
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
        /// �ر����еĿͻ��˻Ự,�����еĿͻ������ӻ�Ͽ�
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
        /// �ر�һ����ͻ���֮��ĻỰ
        /// </summary>
        /// <param name="closeClient">��Ҫ�رյĿͻ��˻Ự����</param>
        public virtual void CloseSession(Session closeClient)
        {
            Debug.Assert(closeClient != null);

            if (closeClient != null)
            {

                closeClient.Datagram = null;
                _sessionTable.Remove(closeClient.ID);
                dic.Remove(closeClient.ID.ID);
                _clientCount--;

                //�ͻ���ǿ�ƹر�����
                if (ClientClose != null)
                {
                    ClientClose(this, new NetEventArgs(closeClient));
                }

                closeClient.Close();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="recvDataClient">�������ݵĿͻ��˻Ự</param>
        /// <param name="datagram">���ݱ���</param>
        public virtual void Send(Session recvDataClient, MsgStruts msg)
        {
            if (msg == null)
            {
                return;
            }

            //��ñ��ĵı����ֽ�


            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//���������л���
            bf.Serialize(ms, msg);//����Ϣ��ת��Ϊ�ڴ���
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
        /// ��������
        /// </summary>
        /// <param name="recvDataClient">�������ݵĿͻ��˻Ự</param>
        /// <param name="datagram">���ݱ���</param>
        public virtual void Send(Session recvDataClient, string datagram)
        {
            //������ݱ���
            byte[] data = _coder.GetEncodingBytes(datagram);

            recvDataClient.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), recvDataClient.ClientSocket);
        }

        #endregion

        #region �ܱ�������
        /// <summary>
        /// �ر�һ���ͻ���Socket,������Ҫ�ر�Session
        /// </summary>
        /// <param name="client">Ŀ��Socket����</param>
        /// <param name="exitType">�ͻ����˳�������</param>
        protected virtual void CloseClient(Socket client, Session.ExitType exitType)
        {
            Debug.Assert(client != null);

            //���Ҹÿͻ����Ƿ����,���������,�׳��쳣
            Session closeClient = FindSession(client);

            closeClient.TypeOfExit = exitType;

            if (closeClient != null)
            {
                CloseSession(closeClient);
            }
            else
            {
                throw (new ApplicationException("��Ҫ�رյ�Socket���󲻴���"));
            }
        }

        /// <summary>
        /// �ͻ������Ӵ�����
        /// </summary>
        /// <param name="iar">���������������ӵ�Socket����</param>
        protected virtual void AcceptConn(IAsyncResult iar)
        {
            //���������ֹͣ�˷���,�Ͳ����ٽ����µĿͻ���
            if (!_isRun)
            {
                return;
            }

            //����һ���ͻ��˵���������
            Socket oldserver = (Socket)iar.AsyncState;
            Socket client = oldserver.EndAccept(iar);

            //����Ƿ�ﵽ��������Ŀͻ�����Ŀ
            if (_clientCount == _maxClient)
            {
                if (ServerFull != null)
                {
                    ServerFull(this, new NetEventArgs(new Session(client)));
                }
            }
            else
            {
                //�½�һ���ͻ�������
                Session newSession = new Session(client);
                _sessionTable.Add(newSession.ID, newSession);
                dic.Add(newSession.ID.ID, newSession);


                _clientCount++;

                newSession.RecvDataBuffer = _recvDataBuffer;
                //��ʼ�������Ըÿͻ��˵�����
                client.BeginReceive(newSession.RecvDataBuffer, 0, newSession.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(ReceiveData), newSession);

                //�µĿͻ�������,����֪ͨ
                if (ClientConn != null)
                {
                    ClientConn(this, new NetEventArgs(newSession));
                }
            }

            //�������ܿͻ���
            _svrSock.BeginAccept(new AsyncCallback(AcceptConn), _svrSock);
        }

        /// <summary>
        /// ͨ��Socket�������Session����
        /// </summary>
        /// <param name="client"></param>
        /// <returns>�ҵ���Session����,���Ϊnull,˵���������ڸûỰ</returns>
        private Session FindSession(Socket client)
        {
            SessionId id = new SessionId((int)client.Handle);
            return (Session)_sessionTable[id];
        }

        /// <summary>
        /// ����������ɴ��������첽�����Ծ���������������У�
        /// �յ����ݺ󣬻��Զ�����Ϊ�ַ�������(��Ϣ�ṹ��)
        /// </summary>
        /// <param name="iar">Ŀ��ͻ���Socket</param>
        protected virtual void ReceiveData(IAsyncResult iar)
        {
            string.Format("class :{0},function:ReceiveData", typeof(TcpSvr).Name).Logger();
            Session sendDataSession = (Session)iar.AsyncState;
            Socket client = sendDataSession.ClientSocket;

            try
            {
                //������ο�ʼ���첽�Ľ���,���Ե��ͻ����˳���ʱ��
                //������ִ��EndReceive
                int recv = client.EndReceive(iar);
                "1->1".Logger();
                if (recv == 0)
                {
                    CloseClient(client, Session.ExitType.NormalExit);
                    return;
                }




                //�����յ����ݵ��¼�
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

                //���������������ͻ��˵�����
                sendDataSession.RecvDataBuffer = new byte[DefaultBufferSize];
                client.BeginReceive(sendDataSession.RecvDataBuffer, 0, sendDataSession.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(ReceiveData), sendDataSession);
            }
            catch (SocketException ex)
            {
                ex.Message.ErrorLogger();
                if (10054 == ex.ErrorCode || 10053 == ex.ErrorCode)
                {
                    //�ͻ���ǿ�ƹر�
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
        /// ����������ɴ��������첽�����Ծ���������������У�
        /// �յ����ݺ󣬻��Զ�����Ϊ�ַ�������
        /// </summary>
        /// <param name="iar">Ŀ��ͻ���Socket</param>
        protected virtual void ReceiveDataStr(IAsyncResult iar)
        {
            Session sendDataSession = (Session)iar.AsyncState;
            Socket client = sendDataSession.ClientSocket;

            try
            {
                //������ο�ʼ���첽�Ľ���,���Ե��ͻ����˳���ʱ��
                //������ִ��EndReceive
                int recv = client.EndReceive(iar);

                if (recv == 0)
                {
                    CloseClient(client, Session.ExitType.NormalExit);
                    return;
                }

                string receivedData = _coder.GetEncodingString(sendDataSession.RecvDataBuffer, recv);
                //string receivedData = _coder.GetEncodingString(_recvDataBuffer, recv);

                //�����յ����ݵ��¼�
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

                            //����һ��������Ϣ
                            RecvData(this, new NetEventArgs(clientSession));
                        }

                        //ʣ��Ĵ���Ƭ��,�´ν��յ�ʱ��ʹ��
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

                //���������������ͻ��˵�����
                client.BeginReceive(sendDataSession.RecvDataBuffer, 0, sendDataSession.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(ReceiveData), sendDataSession);
            }
            catch (SocketException ex)
            {
                ex.ToString().ErrorLogger();
                if (10054 == ex.ErrorCode)
                {
                    //�ͻ���ǿ�ƹر�
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
        /// ����������ɴ�����
        /// </summary>
        /// <param name="iar">Ŀ��ͻ���Socket</param>
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
        /// Э���������
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

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//���������л���
            bf.Serialize(ms, msg);//����Ϣ��ת��Ϊ�ڴ���
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
            //socket  ��������СĬ��ֵΪ8192�ֽڣ�8kb��
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
    /// �ṩTcp�������ӷ���Ŀͻ�����
    /// </summary>
    public class TcpCli
    {
        #region �ֶ�

        /// <summary>
        /// �ͻ����������֮��ĻỰ��
        /// </summary>
        private Session _session;

        /// <summary>
        /// �ͻ����Ƿ��Ѿ����ӷ�����
        /// </summary>
        private bool _isConnected = false;

        /// <summary>
        /// �������ݻ�������С640K
        /// </summary>
        public const int DefaultBufferSize = 64000 * 1024;
        int PackReceiveBuffLength = 1024;
        /// <summary>
        /// ���Ľ�����
        /// </summary>
        private DatagramResolver _resolver;

        /// <summary>
        /// ͨѶ��ʽ���������
        /// </summary>
        private Coder _coder;
        /// <summary>
        /// IP
        /// </summary>
        private string _localip;
        /// <summary>
        /// �˿ں�
        /// </summary>
        private int _localport;
        /// <summary>
        /// �������ݻ�����
        /// </summary>
        private byte[] _recvDataBuffer = new byte[DefaultBufferSize];
        #endregion

        #region �¼�����

        /// <summary>
        /// �޷����ӵ��������¼�
        /// </summary>
        public event NetEvent CannotConnectedServer;

        /// <summary>
        /// �Ѿ����ӷ������¼�
        /// </summary>
        public event NetEvent ConnectedServer;

        /// <summary>
        /// ���յ����ݱ����¼�
        /// </summary>
        public event NetEvent ReceivedDatagram;

        /// <summary>
        /// ���ӶϿ��¼�
        /// </summary>
        public event NetEvent DisConnectedServer;

        public AsyncCallback ReceiverDataComplate { get; set; }
        #endregion

        #region ����

        /// <summary>
        /// ���ؿͻ����������֮��ĻỰ����
        /// </summary>
        public Session ClientSession
        {
            get
            {
                return _session;
            }
        }

        /// <summary>
        /// ���ؿͻ����������֮�������״̬
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }

        /// <summary>
        /// ���ݱ��ķ�����
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
        /// ���������
        /// </summary>
        public Coder ServerCoder
        {
            get
            {
                return _coder;
            }
        }

        #endregion

        #region ���з���

        /// <summary>
        /// Ĭ�Ϲ��캯��,ʹ��Ĭ�ϵı����ʽ
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
        /// ���캯��,ʹ��һ���ض��ı���������ʼ��
        /// </summary>
        /// <param name="_coder">���ı�����</param>
        public TcpCli(Coder coder)
        {
            _coder = coder;
        }

        /// <summary>
        /// ���캯��,�����ʼ��
        /// </summary>
        public TcpCli(string ip, int port)
        {
            _localip = ip;
            _localport = port;
            DefaultEvent();
        }
        /// <summary>
        /// ���ӷ�����
        /// </summary>
        /// <param name="ip">������IP��ַ</param>
        /// <param name="port">�������˿�</param>
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
        /// �������ݱ���
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
                string msg = "û�����ӷ����������ܷ�������";
                msg.ErrorLogger();
                throw (new ApplicationException(msg));
            }

            //��ñ��ĵı����ֽ�
            byte[] data = _coder.GetEncodingBytes(datagram);

            _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), _session.ClientSocket);
        }

        /// <summary>
        /// �������ݱ���
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

            //��ñ��ĵı����ֽ�


            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//���������л���
            bf.Serialize(ms, msg);//����Ϣ��ת��Ϊ�ڴ���
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
        /// �ر�����
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
        /// ͬ������
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
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//���������л���
                    bf.Serialize(ms, msg);//����Ϣ��ת��Ϊ�ڴ���
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
                    System.Threading.Thread.Sleep(wait);//��ʱ���ܱ�֤���ݽ������
                }
                else
                {
                    System.Threading.Thread.Sleep(300);//��ʱ���ܱ�֤���ݽ������
                }
                //bool end=true;
                //int pos = 0;
                //_recvDataBuffer = new byte[1024];
                //socket.Receive(_recvDataBuffer, 0, _recvDataBuffer.Length, SocketFlags.None);
                //socket.BeginReceive(_recvDataBuffer, 0, _recvDataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiverDataComplate), socket);
                //while (end)
                //{
                //    int length = soc.Available;//�������ݵ��Լ�������С
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



        #region �ܱ�������

        /// <summary>
        /// ���ݷ�����ɴ�����
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
        /// ����Tcp���Ӻ������
        /// </summary>
        /// <param name="iar">�첽Socket</param>
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
                //�����޷����������¼�
                if (CannotConnectedServer != null)
                {
                    //�����µĻỰ
                    _session = new Session(socket);
                    _isConnected = false;
                    _session.Datagram = ex.Message;
                    CannotConnectedServer(this, new NetEventArgs(_session));

                }
                return;
            }


            //�����µĻỰ
            _session = new Session(socket);
            _isConnected = true;

            //�������ӽ����¼�
            if (ConnectedServer != null)
            {
                ConnectedServer(this, new NetEventArgs(_session));
            }
            _session.ClientSocket.BeginReceive(_recvDataBuffer, 0,
             DefaultBufferSize, SocketFlags.None,
             new AsyncCallback(RecvData), socket);
        }

        /// <summary>
        /// ���ݽ��մ�����(��Ϣ�ṹ��)
        /// </summary>
        /// <param name="iar">�첽Socket</param>
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


                //ͨ���¼������յ��ı���
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

                //������������
                if (_session != null && _session.ClientSocket.Connected)
                {
                    _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(RecvData), _session.ClientSocket);
                }

            }
            catch (SocketException ex)
            {
                ex.Message.ErrorLogger();
                _isConnected = false;
                //�ͻ����˳�
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

                //������������
                if (_session != null && _session.ClientSocket.Connected)
                {
                    _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(RecvData), _session.ClientSocket);
                }
            }

        }

        /// <summary>
        /// ���ݽ��մ�����(�ַ���)
        /// </summary>
        /// <param name="iar">�첽Socket</param>
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

                //ͨ���¼������յ��ı���
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

                            //����һ��������Ϣ
                            ReceivedDatagram(this, new NetEventArgs(clientSession));
                        }

                        //ʣ��Ĵ���Ƭ��,�´ν��յ�ʱ��ʹ��
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

                //������������
                _recvDataBuffer = new byte[DefaultBufferSize];
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None,
                 new AsyncCallback(RecvData), _session.ClientSocket);
            }
            catch (SocketException ex)
            {
                ex.Message.ErrorLogger();
                //�ͻ����˳�
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
        {//�첽�����ļ��� 
        
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
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//���������л���
                bf.Serialize(ms, msg);//����Ϣ��ת��Ϊ�ڴ���
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
            { //�����󷵻ص����ݳ��ȡ��Զ�����Ϣ=�������ݳ���+�������ݡ�
                int buffLen = sizeof(int);
                if (partRequestData.Length < buffLen)
                {
                    soc.BeginReceive(partRequestData, 0, partRequestData.Length, SocketFlags.None, new AsyncCallback(ReceivePackageData), soc);
                    return;
                }
                byte[] len = new byte[buffLen];
                Array.Copy(partRequestData, len, buffLen);
                ResponseDataLength = BitConverter.ToInt32(len, 0);
                //�޳����ĳ���ռ�õ��ֽ�
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
            {//��������Ӧ���صĶ�����ݰ��ѳɹ�����
                
                ResponseDataLength = 0;//���ñ��ĳ��� 
                ComplateReceiveDataCallBack();//�������ݽ������
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
            //��ȡ��ǰ��
            DateTime now = DateTime.Now;
            // System.Globalization in  mscorlib.dll
            System.Globalization.GregorianCalendar calend = new System.Globalization.GregorianCalendar();//��������
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



