using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSSCSFrameWork;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using GSS.DBUtility;
namespace GSSClient
{
    public class ClientHandles
    {
        private TcpCli _tcpcli = null;
        Coder _coder = new Coder(Coder.EncodingMothord.UTF8);
        MsgStruts SocketResponseData { get; set; }
        public ClientHandles(TcpCli tcpcli)
        {
            _tcpcli = tcpcli;
        }
        void AsyncCallback(IAsyncResult iar)
        {
            Socket soc = iar.AsyncState as Socket;
            SocketError error;
            int flag = soc.EndReceive(iar, out error);
            if (error != SocketError.Success)
            {
                return  ;
            }
            byte[] _recvDataBuffer = new byte[10240];
            soc.BeginReceive(_recvDataBuffer, 0, _recvDataBuffer.Length, SocketFlags.None, AsyncCallback, _recvDataBuffer);
            System.IO.MemoryStream msb = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bfb = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            msb.Write(_recvDataBuffer, 0, _recvDataBuffer.Length);
            msb.Position = 0;
            SocketResponseData = (MsgStruts)bfb.Deserialize(msb);
        }
        /// <summary>
        /// 得到网络配置值并验证该用户名密码(同步方式)
        /// </summary>
        public  string  GetLoginS(string uid, string upsw, string uip)
        {
            //网络配置值初始化
            string[] servinfo = DataConfig.GetServerInfo();
            string StrIp = servinfo[0];
            string StrPort = servinfo[1];
            int port = int.Parse(StrPort);
            ShareData.LocalIp = StrIp;
            ShareData.LocalPort = port;

            TcpCli client = new TcpCli(ShareData.LocalIp, ShareData.LocalPort);
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetLogin;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string msgStr = uid + "|" + upsw + "|" + uip;
            msg.Data = _coder.GetEncodingBytes(msgStr);
            client.ReceiverDataComplate = AsyncCallback;
            MsgStruts msgb = client.SendAndBack(msg);
            string loginStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return loginStr;
        }

        /// <summary>
        /// 客户端登录
        /// </summary>
        public void GetLogin(string uid,string upsw,string uip)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetLogin;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string msgStr = uid + "|" + upsw + "|" + uip;
            msg.Data = _coder.GetEncodingBytes(msgStr); 
            _tcpcli.Send(msg);
        }
        /// <summary>
        /// 客户端退出
        /// </summary>
        public void Login11(string uid, string upsw, string uip)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetLogin;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string msgStr = uid + "|" + upsw + "|" + uip;
            msg.Data = _coder.GetEncodingBytes(msgStr); 
            _tcpcli.Send(msg);
        }
        /// <summary>
        /// 得到客户端缓存
        /// </summary>
        public void GetCahce(string userid)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetCache;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(userid);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 得到数字提醒,全部在线都发送
        /// </summary>
        public void GetAlertNum()
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetAlertNum;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            _tcpcli.Send(msg);
        }
        /// <summary>
        /// 得到客户端缓存(同步)
        /// </summary>
        public bool GetCahceSyn(string userid)
        {
            TcpCli client = new TcpCli(ShareData.LocalIp, ShareData.LocalPort);
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetCache;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(userid);
            MsgStruts msgb = client.SendAndBack(msg);
            DataSet ds = DataSerialize.GetDatasetFromByte(msgb.Data);
            return ClientCache.SetCache(msgb.Data);
        }
        /// <summary>
        /// 得到工单列表
        /// </summary>
        /// <param name="whereStr"></param>
        public void GetAllTasks(string param,string whereStr)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetAllTasks;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = param;
            msg.Data = _coder.GetEncodingBytes(whereStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 得到工单列表(同步)
        /// </summary>
        /// <param name="whereStr"></param>
        public DataSet GetAllTasksSyn(string whereStr)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetAllTasks;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(whereStr);
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            DataSet ds = DataSerialize.GetDatasetFromByte(msgb.Data);
            ClientCache.SetTaskCache(msgb.Data, "TaskList");
            return ds;
        }

        /// <summary>
        /// 得到工单列表
        /// </summary>
        /// <param name="whereStr"></param>
        public void GetAllTasks(string param, string whereStr, string fieldName, string orderStr, int PageSize, int PageIndex)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetAllTasks;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = param;
            msg.MsgParam.p1 = fieldName;
            msg.MsgParam.p2 = orderStr;
            msg.MsgParam.p3 = PageSize.ToString();
            msg.MsgParam.p4 = PageIndex.ToString();
            msg.Data = _coder.GetEncodingBytes(whereStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 得到工单列表(同步)
        /// </summary>
        /// <param name="whereStr"></param>
        public DataSet GetAllTasksSyn(string param, string whereStr, string OrderFieldName, string OrderType, int PageSize, int PageIndex)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetAllTasks;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = param;
            msg.MsgParam.p1 = OrderFieldName;
            msg.MsgParam.p2 = OrderType;
            msg.MsgParam.p3 = PageSize.ToString();
            msg.MsgParam.p4 = PageIndex.ToString();
            msg.Data = _coder.GetEncodingBytes(whereStr);
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            DataSet ds = DataSerialize.GetDatasetFromByte(msgb.Data);
            ClientCache.SetTaskCache(msgb.Data, "TaskList");
            return ds;
        }


        /// <summary>
        /// 得到工单历史
        /// </summary>
        /// <param name="whereStr"></param>
        public void GetTaskLog(string whereStr)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetTaskLog;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(whereStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 得到工单历史(同步)
        /// </summary>
        /// <param name="whereStr"></param>
        public DataSet GetTaskLogSyn(string whereStr)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetTaskLog;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(whereStr);
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            if (msgb == null || msgb.command != msgCommand.GetTaskLog)
            {
                return null;
            }
            DataSet ds = DataSerialize.GetDatasetFromByte(msgb.Data);
            ClientCache.SetTaskCache(msgb.Data, "TaskLog");
            return ds;
        }

        /// <summary>
        /// 得到游戏帐号
        /// </summary>
        /// <param name="whereStr"></param>
        public void GetGameUsersC(string whereStr)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetGameUsersC;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(whereStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 得到游戏帐号
        /// </summary>
        /// <param name="whereStr"></param>
        public void GetGameUsersC(MsgParam msgparam)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetGameUsersC;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodBytes(msgparam);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 得到游戏角色
        /// </summary>
        /// <param name="whereStr"></param>
        public void GetGameRolesC(string whereStr)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetGameRolesC;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(whereStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 得到游戏角色
        /// </summary>
        /// <param name="whereStr"></param>
        public void GetGameRolesCR(string whereStr)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GetGameRolesCR;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(whereStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 增加工单
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(GSSModel.Tasks task)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.AddTask;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetByteFromObject(task);
            _tcpcli.Send(msg);
        }


        /// <summary>
        /// 增加工单
        /// </summary>
        /// <param name="task"></param>
        public string AddTaskSyn(GSSModel.Tasks task)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.AddTask;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetByteFromObject(task);

            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            string backStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return backStr;
        }

        /// <summary>
        /// 增加工单(发奖)
        /// </summary>
        /// <param name="task"></param>
        public string AddTaskSynGA(GSSModel.Tasks task,DataSet ds)
        {
            task.F_URInfo =DataSerialize.GetStringFromObject(DataSerialize.GetDataSetSurrogateZipBYtes(ds)) ;

            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.AddTask;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = "AddTaskGA";
            msg.Data = DataSerialize.GetByteFromObject(task);
            

            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            string backStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return backStr;
        }


        /// <summary>
        /// 编辑工单
        /// </summary>
        public void EditTask(GSSModel.Tasks task)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.EditTask;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetByteFromObject(task);
            _tcpcli.Send(msg);
        }
        /// <summary>
        /// 编辑工单历史
        /// </summary>
        public void EditTaskLog(GSSModel.Tasks task)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.EditTaskLog;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetByteFromObject(task);
            _tcpcli.Send(msg);
        }
        /// <summary>
        /// 编辑工单历史
        /// </summary>
        public string EditTaskLogSyn(GSSModel.Tasks task)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.EditTaskLog;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetByteFromObject(task);

            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            string backStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return backStr;
        }
        /// <summary>
        /// 编辑工单(同步)
        /// </summary>
        /// <param name="task"></param>
        public string EditTaskSyn(GSSModel.Tasks task)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.EditTask;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetByteFromObject(task);

            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            string backStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return backStr;
        }
        /// <summary>
        /// 编辑工单(服务端不回复信息)
        /// </summary>
        public void EditTaskNoReturn(GSSModel.Tasks task)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.EditTaskNoReturn;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetByteFromObject(task);
            _tcpcli.Send(msg);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sqlStr"></param>
        public void ExcSql(string sqlStr)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.ExcSql;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = _coder.GetEncodingBytes(sqlStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="proname"></param>
        /// <param name="parameters"></param>
        public void ExcPro(string formid, string proname, SqlParameter parameters)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.ExcPro;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string param = DataSerialize.GetStringFromSqlParameter(parameters);

            string sqlStr = formid + "|" + proname + "|" + param;//格式 窗口ID+存储过程+参数
            msg.Data = _coder.GetEncodingBytes(sqlStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        ///  游戏工具:封停
        /// </summary>
        /// <param name="UserorRole">是帐户还是角色,帐号:1 角色:2</param>
        /// <param name="URname">用户或角色名称</param>
        /// <param name="locktimeid">封住类型编号</param>
        public void GameLockUR(int formid,int UserorRole,string TaskID,string locktimeid)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GameLockUR;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string lockStr = formid.ToString() + "|" + UserorRole.ToString() + "|" + TaskID + "|" + locktimeid;
            msg.Data = _coder.GetEncodingBytes(lockStr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        ///  游戏工具:解封
        /// </summary>
        /// <param name="UserorRole">是帐户还是角色,帐号:1 角色:2</param>
        public void GameNoLockUR(int formid, int UserorRole, string TaskID)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GameNoLockUR;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string lockStr = formid.ToString() + "|" + UserorRole.ToString() + "|" + TaskID + "";
            msg.Data = _coder.GetEncodingBytes(lockStr);
            _tcpcli.Send(msg);
        }
        /// <summary>
        ///  游戏工具:帐号借用
        /// </summary>
        public void GameUserUse(int formid, string TaskID,string newpsw)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GameUserUse;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string sendtr = formid.ToString() + "|" + TaskID + "|" + newpsw + "";
            msg.Data = _coder.GetEncodingBytes(sendtr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        ///  游戏工具:帐号归还
        /// </summary>
        public void GameUserNoUse(int formid, string TaskID)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GameUserNoUse;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string sendtr = formid.ToString() + "|" + TaskID + "";
            msg.Data = _coder.GetEncodingBytes(sendtr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        ///  游戏工具:清空防沉迷
        /// </summary>
        public void GameResetChildInfo(int formid, string TaskID)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GameResetChildInfo;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            string sendtr = formid.ToString() + "|" + TaskID + "";
            msg.Data = _coder.GetEncodingBytes(sendtr);
            _tcpcli.Send(msg);
        }

        /// <summary>
        /// 游戏工具:开始公告(同步)
        /// </summary>
        /// <param name="task"></param>
        public string GameNoticeStartSyn(string taskid)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GameNoticeStart;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = taskid;
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            string backStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return backStr;
        }
        /// <summary>
        /// 游戏工具:停止公告(同步)
        /// </summary>
        /// <param name="task"></param>
        public string GameNoticeStopSyn(string taskid)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GameNoticeStop;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = taskid;
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            string backStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return backStr;
        }
        /// <summary>
        /// 删除全服邮件
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public string DeleteFullServiceEmail(string taskid)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.DeleteFullServiceEmail;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = taskid;
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            string backStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return backStr;
        }
        /// <summary>
        /// 游戏工具:发奖
        /// </summary>
        /// <param name="task"></param>
        public string GameGiftAwardDo(string taskid)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.GameGiftAwardDo;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = taskid;
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            string backStr = _coder.GetEncodingString(msgb.Data, msgb.Data.Length);
            return backStr;
        }
        /// <summary>
        /// 游戏工具:离线查询GS日志
        /// </summary>
        public DataSet QuerySynLog(string zoneid, string querysql)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.QuerySynGSLog;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = zoneid;
            msg.MsgParam.p1 = querysql;
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            if (msgb == null)
            {
                return null;
            }
            DataSet ds = DataSerialize.GetDatasetFromByte(msgb.Data);
            ClientCache.SetTaskCache(msgb.Data, "QuerySynLog");
            return ds;
        }
        /// <summary>
        /// 游戏工具:实时查询GS日志
        /// </summary>
        public DataSet QueryLiveGSLog(string zoneid, string querysql)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.QueryLiveGSLog;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.MsgParam.p0 = zoneid;
            msg.MsgParam.p1 = querysql;
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            if (msgb == null)
            {
                return null;
            }
            DataSet ds = DataSerialize.GetDatasetFromByte(msgb.Data);
            ClientCache.SetTaskCache(msgb.Data, "QueryLiveGSLog");
            return ds;
        }
        /// <summary>
        /// 增加工单
        /// </summary>
        /// <param name="task"></param>
        public MsgStruts DownloadTemplateFile(GSSModel.TemplateFile task)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.DownloadTemplateFile;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            msg.Data = DataSerialize.GetByteFromObject(task);
            MsgStruts msgb = _tcpcli.SendAndBack(msg);
            return msgb;
        }
        public void AddLoginAward(int formId, GSSModel.Request.LoginAwardTask awardTask)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.AddLoginAward;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            GSSModel.Request.ClientData data = new GSSModel.Request.ClientData() { FormID = formId, Data = awardTask };
            msg.Data = DataSerialize.GetByteFromObject(data);
            _tcpcli.Send(msg);
        }
        public void AddFullServiceEmail(int formId, GSSModel.Request.LoginAwardTask awardTask)
        {
            MsgStruts msg = new MsgStruts();
            msg.command = msgCommand.AddFullServiceEmail;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            GSSModel.Request.ClientData data = new GSSModel.Request.ClientData() { FormID = formId, Data = awardTask };
            msg.Data = DataSerialize.GetByteFromObject(data);
            _tcpcli.Send(msg);
        }
        public void BindCommandWithSend(msgCommand cmd, int formid, GSSModel.Tasks task,object sendData) 
        {
            MsgStruts msg = new MsgStruts();
            msg.command = cmd;
            msg.msgtype = msgType.SendText;
            msg.msgsendstate = msgSendState.single;
            GSSModel.Request.ClientData client = new GSSModel.Request.ClientData();
            client.FormID = formid;
            client.Data = new object[] { task,sendData };
            //json 序列化
            msg.Data = DataSerialize.GetByteFromObject(client);
            _tcpcli.Send(msg);
        }
        /// <summary>
        /// 得到客户端缓存(同步)
        /// </summary>
        public void GetCahceSyn(string userid, CallBackEvent callback)
        {
            try
            {
                TcpCli client = new TcpCli(ShareData.LocalIp, ShareData.LocalPort);
                MsgStruts msg = new MsgStruts();
                msg.command = msgCommand.GetCache;
                msg.msgtype = msgType.SendText;
                msg.msgsendstate = msgSendState.single;
                msg.Data = _coder.GetEncodingBytes(userid);
                if (SystemConfig.GetCacheWaitSleep)
                {//socket缓冲区8192问题没有解决时使用延时加载形式去加载缓存数据
                    msg = client.SendAndBack(msg);
                    callback(msg);
                }
                else client.AsyncSendWithReceiverData(msg, callback);
            }
            catch (Exception ex)
            {
                ex.ToString().ErrorLogger();
                ShareData.Log.Info(ex);
                callback(null);
            }
            //return ClientCache.SetCache(msgb.Data);
        }
        public void SendEmail(int formId, GSSModel.Request.ClientData clientparam) 
        {

            MsgStruts msg = new MsgStruts() { msgtype = msgType.SendText, command = msgCommand.SendEmailToRoles,msgsendstate=msgSendState.single };
            msg.Data = _coder.GetEncodBytes(clientparam);
            _tcpcli.Send(msg);

        }
        public void SendTextToService(GSSModel.Request.ClientData data,msgCommand command)
        {
            MsgStruts str = new MsgStruts()
            {
                msgsendstate = msgSendState.single,
                msgtype = msgType.SendText,
                command = command
            };
            str.Data = DataSerialize.GetByteFromObject(data);
            _tcpcli.Send(str);
        }
        public void SendTaskContainerLogicData(GSSModel.Request.ClientData data) 
        {
            MsgStruts str = new MsgStruts() 
            {
                msgsendstate=msgSendState.single,
                msgtype=msgType.SendText,
                command=msgCommand.CreateTaskContainerLogic
            };
            str.Data = DataSerialize.GetByteFromObject(data);
            string.Format("command:[{0}] Client send service bytes:[{1}]", str.command,str.Data.Length);
            _tcpcli.Send(str);
        }
    }

    class CreateTreeViewFromDataTable
    {
        // 这个 Dictionary 对象用来标识每一个 List<TreeNode> 对象
        // 每一个 List<TreeNode> 用来存储同一个父节点下的所有的树节点
        private static Dictionary<int, List<TreeNode>> dic;


        public static void BuildTree(DataTable dt, TreeView treeView, Boolean expandAll,
            string displayName, string nodeId, string parentId)
        {
            // 清除当前 TreeView 所有已存在的数据
            treeView.Nodes.Clear();

            dic = new Dictionary<int, List<TreeNode>>();

            TreeNode node = null;

            foreach (DataRow row in dt.Rows)
            {
                // 重新存储每一个节点数据
                node = new TreeNode(row[displayName].ToString());
                node.Tag = row[nodeId];

                // 对于 TreeView 的根节点，DataTable 中对应的 parentId 属性值为 ""
                // 所以当 parentId 为 "" 时，他就是根节点
                // 否则的话，他就是一般的树节点
                if (row[parentId] != DBNull.Value)
                {
                    int _parentId = Convert.ToInt32(row[parentId]);

                    // 如果在 Dictionary 总存在一个键值为 _parentId 的 List<TreeNode> 对象
                    // 那么我们可以把当前的树节点放入这个 List<TreeNode> 下面，作为他的子节点
                    if (dic.ContainsKey(_parentId))
                    {
                        dic[_parentId].Add(node);
                    }
                    else
                    {
                        // 否则
                        // 我们需要新建一个记录在 Dictionary<int, List<TreeNode>>
                        dic.Add(_parentId, new List<TreeNode>());

                        // 然后，将树节点放入这个新的记录
                        dic[_parentId].Add(node);
                    }
                }
                else
                {
                    // 将节点加入 TreeView 的根节点下面
                    treeView.Nodes.Add(node);
                }
            }

            // 在填充满所有的树节点和他的子节点之后
            // 我们就可以在根节点下面构建整个树形结构
            SearchChildNodes(treeView.Nodes[0]);

            if (expandAll)
            {
                // 展开 TreeView
                treeView.ExpandAll();
            }
        }
        private static void SearchChildNodes(TreeNode parentNode)
        {
            if (!dic.ContainsKey(Convert.ToInt32(parentNode.Tag)))
            {
                // 如果在指定的 parentId 下没有一个集合
                // 那么我们可以直接返回
                return;
            }

            // 将指定的集合添加到 TreeView 中并且作为他的 parentId 节点的子节点
            parentNode.Nodes.AddRange(dic[Convert.ToInt32(parentNode.Tag)].ToArray());

            // 继续遍历查找所有的子节点，是否有节点还有其子节点
            foreach (TreeNode _parentNode in dic[Convert.ToInt32(parentNode.Tag)].ToArray())
            {
                SearchChildNodes(_parentNode);
            }
        }
    }
}
