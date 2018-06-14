using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSS.DBUtility;
using System.Data;
using GSSServerLibrary;
using GSSCSFrameWork;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using GSSModel.Request;
using GSSModel;
namespace GSSServer
{
    public class ServerHandle
    {
        #region 公共变量
        /// <summary>
        /// 通讯实例
        /// </summary>
        private static TcpSvr _tcpsvr;

        /// <summary>
        /// GSS系统数据库连接字符串
        /// </summary>
        private string gssdbconn;

        /// <summary>
        /// 数据库处理
        /// </summary>
        DBHandle dbhandle;

        /// <summary>
        /// 编码转换
        /// </summary>
        Coder _coder = new Coder(Coder.EncodingMothord.UTF8);

        public static TcpSvr TcpSvr
        {
            get { return _tcpsvr; }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TcpSvr">通讯实例</param>
        public ServerHandle(TcpSvr TcpSvr)
        {
            _tcpsvr = TcpSvr;

            string sqlstr = "SELECT * FROM GSSCONFIG WHERE ID=1";
            DataSet ds = DbHelperSQLite.Query(sqlstr);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                string dbip = ds.Tables[0].Rows[0]["GSSDBIP"].ToString();
                string dbname = ds.Tables[0].Rows[0]["GSSDBNAME"].ToString();
                string dbuid = ds.Tables[0].Rows[0]["GSSDBUID"].ToString();
                string dbpsw = ds.Tables[0].Rows[0]["GSSDBPSW"].ToString();
                gssdbconn = @"Data Source=" + dbip + ";Initial Catalog=" + dbname + ";Persist Security Info=True;User ID=" + dbuid + ";Password=" + dbpsw + "";
            }

            dbhandle = new DBHandle();
        }
        /// <summary>
        /// 处理接收到的请求
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="recevStr"></param>
        public void DoRequest(Session Client, MsgStruts msg)
        {
            if (Client == null || msg == null || Client.TypeOfExit != Session.ExitType.NoExit)
            {
                ShareData.Log.Info(msg.command + "未返回数据");
                return;
            }
           
            string msgStr;
            DataSet ds;
            string backStr;
            string[] msgs;
            MsgStruts callBack = new MsgStruts() {  msgsendstate = msgSendState.single, msgtype= msgType.SendText };
            ClientData fromClientData;
            try
            {
                string.Format("command->" + msg.command).Logger();
                switch (msg.command)
                {
                    case msgCommand.GetLogin:
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        msgs = msgStr.Split('|');
                        if (msgs.Length != 3)
                        {
                            backStr = "false";
                        }
                        else
                        {
                            backStr = dbhandle.Login(msgs[0], msgs[1], msgs[2]);
                        }
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GetCache:
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        ds = dbhandle.GetCache();
                        msg.Data = DataSerialize.GetDataSetSurrogateZipBYtes(ds);
                       // ShareData.Log.Warn(msg.Data);
                        _tcpsvr.Send(Client, msg);
                        // _tcpsvr.SendDataContainerBuffer(Client, msg);//cache data  to the client will change to some package
                        System.Threading.Thread.Sleep(800);
                        ((Session)TcpSvr.SessionTable[Client.ID]).UserID = msgStr;

                         SendAlertNum(Client);
                        break;
                    case msgCommand.GetAlertNum:
                        System.Threading.Thread.Sleep(800);
                        SendAlertNum();
                        break;
                    case msgCommand.GetAllTasks:
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        msgStr.Logger();
                        ds = dbhandle.GetTask(msgStr, msg.MsgParam.p1, msg.MsgParam.p2, Convert.ToInt16(msg.MsgParam.p3), Convert.ToInt16(msg.MsgParam.p4));
                        WebServiceLib.DataSetRowsLog(ds, "Service query cmd:" + msgCommand.GetAllTasks);
                        msg.MsgParam.p6 = dbhandle.GetTaskCount(msgStr).ToString();
                        msg.Data = DataSerialize.GetDataSetSurrogateZipBYtes(ds);
                        _tcpsvr.Send(Client, msg);

                        //dbhandle.SysLog(((Session)TcpSvr.SessionTable[Client.ID]).UserID, "工单列表", msgStr);
                        break;
                    case msgCommand.GetTaskLog:
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        ds = dbhandle.GetTaskLog(msgStr);
                        msg.Data = DataSerialize.GetDataSetSurrogateZipBYtes(ds);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GetGameUsersC:
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        "GetGameUsersC".Logger();
                        msgStr.Logger();
                        ds = WebServiceLib.GetGameUsersC(msgStr);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            string.Format("Service Query game user number:[{0}]", ds.Tables[0].Rows.Count).Logger();
                        }
                        else {
                            string.Format("Service Query game user number:[{0}]", 0).Logger();
                        }
                        msg.Data = DataSerialize.GetDataSetSurrogateZipBYtes(ds);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GetGameRolesC:
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        "GetGameRoleC".Logger();
                        msgStr.Logger();
                        ds = WebServiceLib.GetGameRoleC(msgStr);
                        WebServiceLib.DataSetRowsLog(ds, typeof(ServerHandle).Name + "." + msgCommand.GetGameRolesC);
                        if (ds != null) 
                        {
                            msg.Data = DataSerialize.GetDataSetSurrogateZipBYtes(ds);
                        }
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GetGameRolesCR:
                        msgCommand.GetGameRolesCR.ToString().Logger();
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        ("Query where:" + msgStr).Logger();
                        ds = WebServiceLib.GetGameRoleCRALL(msgStr);
                        WebServiceLib.DataSetRowsLog(ds, msgCommand.GetGameRolesCR + " Totla");
                        msg.Data = DataSerialize.GetDataSetSurrogateZipBYtes(ds);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.AddTask:
                        GSSModel.Tasks task = (GSSModel.Tasks)DataSerialize.GetObjectFromByte(msg.Data);
                        int id = dbhandle.AddTask(task);
                        if (task.F_Type.ToString() == "2203")
                        {
                            backStr = WebServiceLib.DisChatAdd(task);
                        }
                        else if (task.F_Type.ToString() == "2213")
                        {
                            backStr = WebServiceLib.DisChatDel(task);
                        }
                        msg.Data = _coder.GetEncodingBytes(id.ToString());
                        _tcpsvr.Send(Client, msg);
                        System.Threading.Thread.Sleep(800);
                        SendAlertNum();
                        break;
                    case msgCommand.EditTask:
                        task = (GSSModel.Tasks)DataSerialize.GetObjectFromByte(msg.Data);
                        id = dbhandle.EditTask(task);
                        msg.Data = _coder.GetEncodingBytes(id.ToString());
                        _tcpsvr.Send(Client, msg);
                        System.Threading.Thread.Sleep(800);
                        SendAlertNum();
                        break;
                    case msgCommand.EditTaskNoReturn:
                        task = (GSSModel.Tasks)DataSerialize.GetObjectFromByte(msg.Data);
                        id = dbhandle.EditTask(task);
                        msg.Data = _coder.GetEncodingBytes(id.ToString());
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.EditTaskLog:
                        task = (GSSModel.Tasks)DataSerialize.GetObjectFromByte(msg.Data);
                        id = dbhandle.EditTaskLog(task);
                        msg.Data = _coder.GetEncodingBytes(id.ToString());
                        _tcpsvr.Send(Client, msg);
                        System.Threading.Thread.Sleep(800);
                        SendAlertNum();
                        break;
                    case msgCommand.ExcSql:
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        string[] formsql = msgStr.Split('|');//分为两段,执行SQL语句的窗口ID和语句
                        int rowcount = dbhandle.ExeSql(formsql[1]);
                        backStr = formsql[0] + "|" + rowcount;
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.ExcPro:
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        string[] pformsql = msgStr.Split('|');////格式 窗口ID+存储过程+参数
                        string proname = pformsql[1];
                        SqlParameter[] param = DataSerialize.GetSqlParameterFromString(pformsql[2]);
                        break;
                    case msgCommand.GameLockUR://格式 窗口ID+封停用户还是角色+工单编号+封停时间
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        msgs = GetParam(msgStr);
                        string formid = msgs[0];
                        string doStr = WebServiceLib.URlock(msgs[1], msgs[2], msgs[3]);
                        backStr = formid + "|" + doStr;
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GameNoLockUR://格式 窗口ID+封停用户还是角色+工单编号
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        msgs = GetParam(msgStr);
                        formid = msgs[0];
                        doStr = WebServiceLib.URNolock(msgs[1], msgs[2]);
                        backStr = formid + "|" + doStr;
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GameUserUse://格式 窗口ID+工单编号
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        msgs = GetParam(msgStr);
                        formid = msgs[0];
                        doStr = WebServiceLib.GameUserUse(msgs[1], msgs[2]);
                        backStr = formid + "|" + doStr;
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GameUserNoUse://格式 窗口ID+工单编号
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        msgs = GetParam(msgStr);
                        formid = msgs[0];
                        doStr = WebServiceLib.GameUserNoUse(msgs[1]);
                        backStr = formid + "|" + doStr;
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GameResetChildInfo://格式 窗口ID+工单编号
                        msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                        msgs = GetParam(msgStr);
                        formid = msgs[0];
                        doStr = WebServiceLib.GameResetChildInfo(msgs[1]);
                        backStr = formid + "|" + doStr;
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GameNoticeStart:
                        backStr = WebServiceLib.GameNoticeStart(msg.MsgParam.p0);
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GameNoticeStop:
                        backStr = WebServiceLib.GameNoticeStop(msg.MsgParam.p0);
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.DeleteFullServiceEmail:
                        backStr = WebServiceLib.DeleteFullServiceEmail(msg.MsgParam.p0);
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.GameGiftAwardDo:
                        backStr = WebServiceLib.GameGiftAwardDo(msg.MsgParam.p0);
                        msg.Data = _coder.GetEncodingBytes(backStr);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.QuerySynGSLog:
                        msg.Data = WebServiceLib.QuerySynGSLog(msg.MsgParam.p0, msg.MsgParam.p1);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.QueryLiveGSLog:
                        msg.Data = WebServiceLib.QueryLiveGSLog(msg.MsgParam.p0, msg.MsgParam.p1);
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.DownloadTemplateFile://下载模本文件
                        object obj = DataSerialize.GetObjectFromByte(msg.Data);
                        GSSModel.TemplateFile tem = obj as GSSModel.TemplateFile;
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//二进制序列化类
                        if (tem != null)
                        {
                            msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Template\\";
                            string file = path + tem.SystemLang + "\\" + tem.TemplateName;
                            if (!File.Exists(file))
                            {
                                msg.msgsendstate = msgSendState.None;
                                bf.Serialize(ms, string.Format(LanguageResource.Language.Tip_ServiceLackTemplateFormat, tem.TemplateName));//将消息类转换为内存流
                                msg.Data = ms.ToArray();
                            }
                            else
                            {
                                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                                byte[] bb = new byte[1024];
                                List<byte> stream = new List<byte>();
                                int len = fs.Read(bb, 0, bb.Length);
                                stream.AddRange(bb);
                                while (len > 0)
                                {
                                    len = fs.Read(bb, 0, bb.Length);
                                    stream.AddRange(bb);
                                }
                                msg.Data = stream.ToArray();
                                fs.Close();
                                msg.msgsendstate = msgSendState.single;
                            }
                        }
                        else
                        {
                            msg.msgsendstate = msgSendState.None;
                            bf.Serialize(ms, LanguageResource.Language.Tip_ClientRequestLack);
                        }
                        _tcpsvr.Send(Client, msg);
                        break;
                    case msgCommand.AddLoginAward:
                        object data = DataSerialize.GetObjectFromByte(msg.Data);
                        GSSModel.Request.ClientData client = data as GSSModel.Request.ClientData;
                        GSSModel.Request.LoginAwardTask at = client.Data as GSSModel.Request.LoginAwardTask;
                        msg.Data = null;
                        client.Data = null;
                        string message = "";
                        if (at.Task.F_Type.ToString() == "20000215")
                        {
                            message = dbhandle.AddLoginAward(at);
                        }
                        else if (at.Task.F_Type.ToString() == "20000217")
                        {
                            message = dbhandle.AddFullServiceEmail(at);
                        }
                        if (message != true.ToString())
                        {
                            ShareData.Log.Error(message);
                        }
                        else {
                            client.Success = true;
                        }
                        msg.Data =DataSerialize.GetByteFromObject(client);;
                        msg.msgsendstate = msgSendState.single;
                        _tcpsvr.Send(Client, msg);
                        SendAlertNum(Client);
                        break;
                    case msgCommand.AddFullServiceEmail:
                        object objData = DataSerialize.GetObjectFromByte(msg.Data);
                        GSSModel.Request.ClientData clientData = objData as GSSModel.Request.ClientData;
                        GSSModel.Request.LoginAwardTask atl = clientData.Data as GSSModel.Request.LoginAwardTask;
                        msg.Data = null;
                        clientData.Data = null;
                        string strMessage = dbhandle.AddFullServiceEmail(atl);
                        if (strMessage != true.ToString())
                        {
                            ShareData.Log.Error(strMessage);
                        }
                        else {
                            clientData.Success = true;
                        }
                        msg.Data =DataSerialize.GetByteFromObject(clientData);;
                        msg.msgsendstate = msgSendState.single;
                        _tcpsvr.Send(Client, msg);
                        SendAlertNum(Client);
                        break;
                    case msgCommand.GameRoleRecovery:
                        object d = DataSerialize.GetObjectFromByte(msg.Data);
                        ClientData cd = d as ClientData;
                        object[] arr = cd.Data as object[];
                        GSSModel.Tasks tsk = arr[0] as GSSModel.Tasks;
                        id = dbhandle.AddTask(tsk);
                        if (id < 1)
                        {//创建工单失败
                            msg.Data = _coder.GetEncodingBytes(cd.FormID + "|" + "-1|" + LanguageResource.Language.Tip_CreateWorkOrderFailure);
                            msg.msgsendstate = msgSendState.single;
                            _tcpsvr.Send(Client, msg);
                            return;
                        }
                        RoleData role = arr[1] as RoleData;
                        string code = WebServiceLib.RecoveryRoleWithRollBack(role);
                        if (code != true.ToString())
                        {
                            System.Resources.ResourceManager rm = LanguageResource.Language.ResourceManager;
                            string info = rm.GetString("Tip_RecoveryRoleStatue_" + code);
                            if (string.IsNullOrEmpty(info))
                            {
                                info = code;
                            }
                            msg.Data = _coder.GetEncodingBytes(cd.FormID + "|" + id + "|" + info);
                            msg.msgsendstate = msgSendState.single;
                            _tcpsvr.Send(Client, msg);
                            return;
                        }
                        else
                        {
                            msg.Data = _coder.GetEncodingBytes(cd.FormID + "|" + id + "|" + true.ToString());
                            msg.msgsendstate = msgSendState.single;
                            _tcpsvr.Send(Client, msg);
                        }
                        SendAlertNum(Client);
                        break;
                    case msgCommand.SendEmailToRoles:
                        d = DataSerialize.GetObjectFromByte(msg.Data);
                        GSSModel.Request.ClientData clientdata = d as GSSModel.Request.ClientData;
                        GSSModel.SendEmailToRole email = clientdata.Data as GSSModel.SendEmailToRole;
                        //通过调用接口进行数据传输
                        clientdata.Message = WebServiceLib.SetRolesEmail(email);
                        clientdata.Data = null;
                        MsgStruts reback = new MsgStruts() { command = msgCommand.SendEmailToRoles, msgsendstate = msgSendState.single };
                        if (string.IsNullOrEmpty(clientdata.Message))
                             clientdata.Success = true;
                        reback.Data = DataSerialize.GetByteFromObject(clientdata);
                        _tcpsvr.Send(Client, reback);
                        SendAlertNum(Client);
                        break;
                    case msgCommand.ActiveFallGoods:
                        d = DataSerialize.GetObjectFromByte(msg.Data);
                        GSSModel.Request.ClientData request = d as GSSModel.Request.ClientData;
                        GSSModel.Request.ActiveFallGoodsData fall = request.Data as GSSModel.Request.ActiveFallGoodsData;
                        request.Data = null;
                        request.Message= WebServiceLib.AddActiveFallConfig(fall);
                        request.Success = true;
                        callBack.command=msgCommand.ActiveFallGoods;
                        callBack.Data=DataSerialize.GetByteFromObject(request);
                        _tcpsvr.Send(Client, callBack);
                        SendAlertNum(Client);
                        break;
                    case msgCommand.CreateTaskContainerLogic://这是对于工单数据流程优化新增的传递方式
                        string.Format(" class:[{0}], command:[{1}]", typeof(ServerHandle).Name, msgCommand.CreateTaskContainerLogic);
                        d = DataSerialize.GetObjectFromByte(msg.Data);
                        msg.Data = null;
                        fromClientData = d as GSSModel.Request.ClientData;
                        try
                        {
                            SwitchDo(fromClientData);
                        }
                        catch (Exception ex)
                        {
                            ex.ToString().ErrorLogger();
                            fromClientData.Success = false;
                            fromClientData.Message = ex.Message;
                        }
                        msg.Data = DataSerialize.GetByteFromObject(fromClientData);
                            _tcpsvr.Send(Client, msg);
                        SendAlertNum(Client);
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                ex.Message.ErrorLogger();
                msgStr = _coder.GetEncodingString(msg.Data, msg.Data.Length);
                ShareData.Log.Info(msgStr);
                ShareData.Log.Warn(ex);
                msg.Data = _coder.GetEncodingBytes("0");
                _tcpsvr.Send(Client, msg); 

            }

        }

        /// <summary>
        /// 发送提醒数量用的DATASET
        /// </summary>
        /// <param name="client"></param>
        private void SendAlertNum(Session Client)
        {
            //Thread Alert_thread = null;
            //Alert_thread = new Thread(new ThreadStart(SendAlert));
            //Alert_thread.Start();
            SendAlert(Client);

        }
        private void SendAlert(Session Client)
        {
            Session client = (Session)_tcpsvr.SessionTable[Client.ID];
            DataSet ds = dbhandle.GetAlertNum();
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetAlertNum;
            msg.msgtype = msgType.SendDataset;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetDataSetSurrogateZipBYtes(ds);

            _tcpsvr.Send(client, msg);
        }

        //定时查询新订单,用于WEB提交的工单
        public void CheckNewTask()
        {
            int i = dbhandle.GetNewTaskCount();
            if (i > 0)
            {
                SendAlertNum();
            }
        }

        /// <summary>
        /// 发送提醒数量用的DATASET
        /// </summary>
        /// <param name="client"></param>
        public void SendAlertNum()
        {
            Thread Alert_thread = null;
            Alert_thread = new Thread(new ThreadStart(SendAlert));
            Alert_thread.Start();
           // Alert_thread.Join(1000);
            //  SendAlert();
            //if (Alert_thread == null)
            //    Alert_thread = new Thread(new ThreadStart(SendAlertNum));

            //if (Alert_thread.ThreadState == ThreadState.Stopped)
            //{
            //    Alert_thread = null;
            //    Alert_thread = new Thread(new ThreadStart(SendAlertNum));
            //}

            //if (!Alert_thread.IsAlive)
            //    Alert_thread.Start();
        }
        public void SendAlert()
        {
            try
            {
                lock (_tcpsvr.dic.Values)
                {
                    List<object> clients = new List<object>(_tcpsvr.dic.Values);
                    foreach (object client in clients)
                    {
                        Session Client = (Session)client;
                        if (Client != null && Client.TypeOfExit == Session.ExitType.NoExit)
                        {
                            try
                            {

                                DataSet ds = dbhandle.GetAlertNum();
                                MsgStruts msg = new MsgStruts();
                                msg.command = msgCommand.GetAlertNum;
                                msg.msgtype = msgType.SendDataset;
                                msg.msgsendstate = msgSendState.single;
                                msg.Data = DataSerialize.GetDataSetSurrogateZipBYtes(ds);
                                _tcpsvr.Send(Client, msg);
                                System.Threading.Thread.Sleep(100);
                            }
                            catch (System.Exception ex)
                            {
                                ex.ToString().ErrorLogger();
                                //日志记录
                                ShareData.Log.Warn(ex);
                                // _tcpsvr.CloseSession(Client);
                            }

                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Error("GSS发送提醒数字错误:", ex);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recevStr"></param>
        protected string[] GetParam(string recevStr)
        {
            string[] param = new string[5];
            recevStr = recevStr.Replace(_tcpsvr.Resovlver.EndTag, "");

            int j = 0;//参数索引值
            for (int i = 0; i < recevStr.Length; i++)
            {
                if (recevStr[i] != '|')
                {
                    param[j] += recevStr[i].ToString().Trim();
                }
                else
                {
                    j++;
                }
            }
            return param;
        }

        private void GameLockUR()
        {

        }
        void SendEmailToRoles(Session tcpclient,object data) 
        {
        //    MsgStruts msg = data as MsgStruts;
           // string json=_coder.GetEncodingString(msg.Data,msg.Data.Length);
            GSSModel.Request.ClientData client = data as GSSModel.Request.ClientData;
            GSSModel.SendEmailToRole email = client.Data as GSSModel.SendEmailToRole;
            MsgStruts reback=new MsgStruts(){command=msgCommand.SendEmailToRoles};
            client.Message="reback";
            client.Data = null;
            reback.Data = _coder.GetEncodBytes(client);
            _tcpsvr.Send(tcpclient, reback);
        }
        ClientData SwitchDo(GSSModel.Request.ClientData clientData) 
        {//在创建工单的同时将逻辑数据提交到服务端处理
            TaskContainerLogicData tl = clientData.Data as TaskContainerLogicData;
            Tasks task = tl.WorkOrder;
            int id = dbhandle.AddTask(task);
            clientData.TaskID = id;
            string.Format("Sync create task and run :{0}", clientData.Data.GetType().Name);
            if (tl.LogicData.GetType().Name != typeof(RunTask).Name)
            {
                Unlock un = tl.LogicData as Unlock;//bug 此处不能直接将object转换为json
                string json = un.MapObject<Unlock,UnlockLogic>().ConvertJson();
                // if(clientData.Data.GetType().Name==typeof())
                int code = dbhandle.InsertLogicJsonAfterTask(id, json);
              
                clientData.Data = null;
                if (code != StatueCode.Ok)
                {//未创建成功 
                    clientData.Data = code;
                    clientData.Message = "Error";

                }
                else
                {
                    string ret = WebServiceLib.URNolock((un.RoleId > 0 ? "2" : "1"), id.ToString());
                    clientData.Success = ret == "true";
                    string.Format("taskId=[{0}]->Unlock result:[{1}]",id, ret).Logger();
                    clientData.Message = ret;
                }
                return clientData;
            }
            msgCommand cmd;
            RunTask rt = tl.LogicData as RunTask;
            clientData.Data = null;
            string.Format("create work order and run-> {0}", rt.Command).Logger();
            string msg = string.Empty;
            cmd = (msgCommand)Enum.Parse(typeof(msgCommand), rt.Command);
            switch (cmd)
            {
                case msgCommand.GameNoticeStart://运行公告
                    msg=WebServiceLib.GameNoticeStart(id.ToString());
                    break;
                default:
                    break;
            }
            clientData.Message = msg;
            if (!string.IsNullOrEmpty(msg))
            {
                clientData.Success = true;
            }
            return clientData;
        }
    }

}
