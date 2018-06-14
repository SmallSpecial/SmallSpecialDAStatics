using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GSS.DBUtility;
using RTXSAPILib;

namespace RTXNotifyServer
{
    public partial class FormMain : Form
    {
        RTXSAPIRootObj RootObj;  //声明一个根对象
        RTXSAPIObj AObj;//声明一个应用对象
        RTXSData Rdata;//SData
        DateTime StartTime;//开启服务时间
        DataSet dsUserRtx;
        string jbtxno = "";

        private LumiSoft.Net.SMTP.Server.SMTP_Server SMTP_Server;
        const string _sqlitstr = "Data Source=Config.dat;Version=3;Password=wd~@#gdt*(*[$*&^%;";
        const string _notfsuserreceiver = "田博辉;刘洋";//TFS无法找到RTX帐号时提醒的人员

        Dictionary<string, string> QADic = new Dictionary<string, string>();

        List<string> alreadyRece = new List<string>();
        public FormMain()
        {
            InitializeComponent();
            try
            {
                // Control.CheckForIllegalCrossThreadCalls = false;
                DbHelperSQLite.connectionString = _sqlitstr;
                string sqlstr = "SELECT * FROM CONFIG WHERE ID=1";
                DataSet ds = DbHelperSQLite.Query(sqlstr);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    tboxRIP.Text = ds.Tables[0].Rows[0]["RIP"].ToString();
                    tboxRPort.Text = ds.Tables[0].Rows[0]["RPORT"].ToString();
                    tboxRUser.Text = ds.Tables[0].Rows[0]["RUSER"].ToString();
                    tboxRPSW.Text = ds.Tables[0].Rows[0]["RPSW"].ToString();
                    textBoxConnStr.Text = ds.Tables[0].Rows[0]["TCONN"].ToString();
                    textBoxTFSUser.Text = ds.Tables[0].Rows[0]["TUSER"].ToString();
                    textBoxTFSPsw.Text = ds.Tables[0].Rows[0]["TPSW"].ToString();
                    tboxMailIP.Text = ds.Tables[0].Rows[0]["TMAIL"].ToString();
                    tboxMailPort.Text = ds.Tables[0].Rows[0]["TMAILPORT"].ToString();
                    radioButtonGroup.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["TQUN"]);
                    radioButtonOne.Checked = !radioButtonGroup.Checked;
                    checkBoxSys.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["TSYS"]);
                    tboxSysAlertTime.Text = ds.Tables[0].Rows[0]["TSYSTIME"].ToString();
                    checkBoxIM.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["TIM"]);
                    jbtxno = ds.Tables[0].Rows[0]["JBTXN"].ToString();
                    textBoxTFSDetailURL.Text = ds.Tables[0].Rows[0]["TFSDETAILURL"].ToString();
                    WriteLog("参数设置读取成功!");
                }
                else
                {
                    WriteLog("参数设置读取失败!");
                }

                RootObj = new RTXSAPIRootObj();     //创建根对象
                AObj = RootObj.CreateAPIObj();
                AObj.OnRecvMessage += new _IRTXSAPIObjEvents_OnRecvMessageEventHandler(RecvMessageEvent);


                this.SMTP_Server = new LumiSoft.Net.SMTP.Server.SMTP_Server(this.components);
                // 
                // SMTP_Server
                // 
                SMTP_Server.Enabled = false;
                this.SMTP_Server.CommandIdleTimeOut = 60000;
                this.SMTP_Server.LogCommands = false;
                this.SMTP_Server.MaxBadCommands = 8;
                this.SMTP_Server.MaxMessageSize = 1000000;
                this.SMTP_Server.MaxRecipients = 100;
                this.SMTP_Server.SessionIdleTimeOut = 80000;
                this.SMTP_Server.AuthUser += new LumiSoft.Net.SMTP.Server.AuthUserEventHandler(this.Server_AuthenticateUser);
                this.SMTP_Server.SessionLog += new LumiSoft.Net.LogEventHandler(this.SMTP_Server_SessionLog);
                this.SMTP_Server.ValidateMailFrom += new LumiSoft.Net.SMTP.Server.ValidateMailFromHandler(this.smtp_Server_ValidateSender);
                this.SMTP_Server.SysError += new LumiSoft.Net.ErrorEventHandler(this.OnServer_SysError);
                this.SMTP_Server.StoreMessage += new LumiSoft.Net.SMTP.Server.NewMailEventHandler(this.smtp_Server_NewMailEvent);
                this.SMTP_Server.ValidateMailTo += new LumiSoft.Net.SMTP.Server.ValidateMailToHandler(this.smtp_Server_ValidateRecipient);
                this.SMTP_Server.ValidateMailboxSize += new LumiSoft.Net.SMTP.Server.ValidateMailboxSize(this.SMTP_Server_ValidateMailBoxSize);
                this.SMTP_Server.ValidateIPAddress += new LumiSoft.Net.ValidateIPHandler(this.SMTP_Server_ValidateIPAddress);

                //------- SMTP Settings ---------------------------------------------//
                SMTP_Server.IpAddress = tboxMailIP.Text;
                SMTP_Server.Port = Convert.ToInt32(tboxMailPort.Text);
                SMTP_Server.Threads = 200;
                SMTP_Server.SessionIdleTimeOut = 300 * 1000; // 毫秒
                SMTP_Server.CommandIdleTimeOut = 120 * 1000; // 毫秒
                SMTP_Server.MaxMessageSize = 10 * 1000000;  // 字节
                SMTP_Server.MaxRecipients = 100;
                SMTP_Server.MaxBadCommands = 8;
                SMTP_Server.LogCommands = false;
                WriteLog("系统初始化成功!");
            }
            catch (System.Exception ex)
            {
                WriteLog("系统初始化失败!" + ex.Message);
            }
        }

        /// <summary>
        /// 接收到RTX消息
        /// </summary>
        private void RecvMessageEvent(RTXSAPILib.IRTXSAPIMessage ms)
        {
            try
            {
                Rdata = RootObj.CreateRTXSData();
                Rdata.XML = ms.Content;
                if (Rdata.XML != null)
                {
                    string s1 = "Txt&gt;";
                    string s2 = "&lt;/Txt";
                    string content = FindMatchStr(s1, s2, Rdata.XML);
                    string groupName = Rdata.GetString("Title");
                    long mode = Rdata.GetLong("Mode");
                    string matchName = FindMatchStr("@", " ", Rdata.XML);
                    if (matchName.Length > 0)
                    {
                        string sendMsg = "时间:" + DateTime.Now + "\n";
                        if (mode == 4 && groupName != string.Empty && content != groupName)
                        {
                            sendMsg += "群名称:" + groupName + "\n";
                        }
                        sendMsg += "内容:" + content;
                        RootObj.SendNotify(matchName, ms.Sender + "@了你", Convert.ToInt32(Convert.ToDouble(tboxSysAlertTime.Text) * 1000), sendMsg);
                    }
                }
                if (ms.Content.IndexOf("DisGroupId") != -1)
                {
                    long groupID = Rdata.GetLong("DisGroupId");
                    if (groupID == 10001)
                    {
                        string s1 = "Txt&gt;";
                        string s2 = "&lt;/Txt";
                        string content = FindMatchStr(s1, s2, Rdata.XML);
                        string receives = ms.Sender + ";" + ms.Receivers;
                        string guid = Rdata.GetString("Key");
                        string messageID = FindMatchStr("<Item Key=\"im_message_id\" Type=\"Buffer\">", "</Item>", Rdata.XML);
                        if (QADic.ContainsKey(content.Trim()))
                        {
                            if (alreadyRece.Contains(messageID))
                            {
                                return;
                            }
                            if (alreadyRece.Count >= 100)
                            {
                                alreadyRece.Clear();
                            }
                            alreadyRece.Add(messageID);
                            RootObj.SendIM(tboxRUser.Text, tboxRPSW.Text, receives, QADic[content], guid);
                        }
                        if (content.IndexOf("@小秘书 学习 问题:") != -1)
                        {
                            string question = FindMatchStr("@小秘书 学习 问题:", "回答:", content).Trim();
                            string answer = FindMatchStr("回答:", s2, Rdata.XML);
                            if (question == "")
                            {
                                return;
                            }
                            else
                            {
                                if (QADic.Count <= 100)
                                {
                                    if (QADic.ContainsKey(question))
                                    {
                                        if (answer == QADic[question])
                                        {
                                            return;
                                        }
                                        RootObj.SendIM(tboxRUser.Text, tboxRPSW.Text, receives, "问题:" + question + "已经更新", guid);
                                    }
                                    QADic[question] = answer;
                                }
                                else
                                {
                                    //学习的问题超过100个，人家脑子不大，不给你们记忆了嘛！！
                                    RootObj.SendIM(tboxRUser.Text, tboxRPSW.Text, receives, "学习的问题超过100个，人家脑子不够用，不给你们记忆了嘛！！", guid);
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private string FindMatchStr(string formerStr, string laterStr, string s)
        {
            string match = "(?<=(" + formerStr + "))[.\\s\\S]*?(?=(" + laterStr + "))";
            Regex rg = new Regex(match, RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(s).Value;
        }

        private void SetJbtxNo(string userid, bool isadd, string receiver, string guid)
        {
            string sql = "UPDATE CONFIG SET JBTXN=replace(JBTXN,'," + userid + ",','')||'," + userid + ",' WHERE ID=1";
            if (isadd)
            {
                sql = "UPDATE CONFIG SET JBTXN=replace(JBTXN,'," + userid + ",','') WHERE ID=1";
            }
            DbHelperSQLite.connectionString = _sqlitstr;
            int row = DbHelperSQLite.ExecuteSql(sql);
            if (row >= 1)
            {
                RootObj.SendIM(tboxRUser.Text, tboxRPSW.Text, userid, "/dp" + userid + (isadd ? "开启" : "关闭") + "提醒-成功", guid);
            }
            else
            {
                RootObj.SendIM(tboxRUser.Text, tboxRPSW.Text, userid, "/dp" + userid + (isadd ? "开启" : "关闭") + "加入提醒-失败", guid);
            }
        }


        /// <summary>
        /// 开启服务
        /// </summary>
        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            if (toolStripButtonSet.Text == "RTX配置保存")
            {
                MessageBox.Show("保存RTX配置后再启动", "提示消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {

                //注册
                RootObj.ServerIP = tboxRIP.Text;
                RootObj.ServerPort = Convert.ToInt16(tboxRPort.Text);
                AObj.ServerIP = RootObj.ServerIP;
                AObj.ServerPort = RootObj.ServerPort;
                AObj.AppGUID = "{" + System.Guid.NewGuid().ToString() + "}";
                AObj.AppName = "RTXRobot";
                AObj.AppAction = RTXSAPILib.RTXSAPI_APP_ACTION.AA_COPY;
                AObj.FilterAppName = "ALL";

                //过滤消息
                AObj.FilterRequestType = "Tencent.RTX.IM";// 过滤消息类型,必填参数
                AObj.FilterResponseType = "none";// 无回复消息类型,必填参数
                AObj.FilterSender = "anyone";// 过滤所有发送人的该类消息,必填参数
                AObj.FilterReceiver = "anyone";// 过滤所有接收人的该类消息,必填参数
                AObj.FilterReceiverState = "anystate";// 关注所有状态,必填参数
                AObj.FilterKey = "";// 过滤关键字为空表示过滤所有消息
                AObj.RegisterApp();

                AObj.StartApp("", 4);
                WriteLog("启动RTX机器人服务");
                SMTP_Server.IpAddress = tboxMailIP.Text;
                SMTP_Server.Port = Convert.ToInt32(tboxMailPort.Text);
                WriteLog("" + SMTP_Server.IpAddress + ":" + SMTP_Server.Port);
                OnStart(null);

                StartTime = DateTime.Now;
                toolStripButtonStart.Enabled = false;
                toolStripButtonStop.Enabled = true;
                timer1.Enabled = true;
                notifyIcon1.ShowBalloonTip(5, "信息提示", "RTXRobot服务已经启动", ToolTipIcon.Info);
                notifyIcon1.Text = "RTXRobot服务已经启动";
                WriteLog(string.Format("启动消息服务\nRTX服务器: {0}:{1}\n机器人ID: {2}", AObj.ServerIP, AObj.ServerPort, tboxRUser.Text));
                //SendTaskLateMsg();
                //ParseURLContent();
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                AObj.StopApp();
                AObj.UnRegisterApp();
                OnStop();
                toolStripButtonStart.Enabled = true;
                toolStripButtonStop.Enabled = false;
                notifyIcon1.ShowBalloonTip(5, "信息提示", "RTXRobot服务已经停止", ToolTipIcon.Info);
                notifyIcon1.Text = "RTXRobot服务已经停止";
                WriteLog(string.Format("停止消息服务\nRTX服务器: {0}:{1}\n{2}", AObj.ServerIP, AObj.ServerPort, labelStatus.Text));
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 启动邮件服务
        /// </summary>
        protected void OnStart(string[] args)
        {
            try
            {
                SMTP_Server.Enabled = true;
                WriteLog("启动邮件服务");
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 关闭邮件服务.
        /// </summary>
        protected void OnStop()
        {
            SMTP_Server.Enabled = false;
        }


        /// <summary>
        /// RTX机器人配置
        /// </summary>
        private void toolStripButtonSet_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonStart.Enabled)
            {
                MessageBox.Show("停止服务后再进行配置", "提示消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (toolStripButtonSet.Text == "RTX配置保存")
            {

                try
                {
                    string RIP = tboxRIP.Text;
                    string RPORT = tboxRPort.Text;
                    string RUSER = tboxRUser.Text;
                    string RPSW = tboxRPSW.Text;
                    string TCONN = textBoxConnStr.Text;
                    string TUSER = textBoxTFSUser.Text;
                    string TPSW = textBoxTFSPsw.Text;
                    string TMAIL = tboxMailIP.Text;
                    string TQUN = radioButtonGroup.Checked.ToString();
                    string TSYS = checkBoxSys.Checked.ToString();
                    string TSYSTIME = tboxSysAlertTime.Text;
                    string TIM = checkBoxIM.Checked.ToString();
                    string TMAILPORT = tboxMailPort.Text;
                    string TFSDETAILURL = textBoxTFSDetailURL.Text;

                    if (!WinUtil.IsDouble(TSYSTIME))
                    {
                        WriteLog("系统消息显示时间(秒)输入格式不正确");
                        return;
                    }

                    string sql = "UPDATE CONFIG SET RIP='" + RIP + "',RPORT='" + RPORT + "',RUSER='" + RUSER + "',RPSW='" + RPSW + "',TCONN='" + TCONN + "',TUSER='" + TUSER + "',TPSW='" + TPSW + "',TMAIL='" + TMAIL + "',TQUN='" + TQUN + "',TSYS='" + TSYS + "',TSYSTIME='" + TSYSTIME + "',TIM='" + TIM + "',TMAILPORT='" + TMAILPORT + "',TFSDETAILURL='" + TFSDETAILURL + "' WHERE ID=1";
                    DbHelperSQLite.connectionString = _sqlitstr;
                    int row = DbHelperSQLite.ExecuteSql(sql);
                    if (row >= 1)
                    {

                        WriteLog("RTX配置保存成功!");
                        toolStripButtonSet.Text = "RTX配置";
                        tboxRIP.Enabled = false;
                        tboxRPort.Enabled = false;
                        tboxRUser.Enabled = false;
                        tboxRPSW.Enabled = false;
                        textBoxConnStr.Enabled = false;
                        textBoxTFSUser.Enabled = false;
                        textBoxTFSPsw.Enabled = false;
                        tboxMailIP.Enabled = false;
                        radioButtonGroup.Enabled = false;
                        radioButtonOne.Enabled = false;
                        checkBoxSys.Enabled = false;
                        checkBoxIM.Enabled = false;
                        tboxSysAlertTime.Enabled = false;
                        lblSysAlertTime.Enabled = false;

                        tboxMailPort.Enabled = false;
                        textBoxTFSDetailURL.Enabled = false;
                    }
                    else
                    {
                        WriteLog("RTX配置保存失败!");
                    }

                }
                catch (System.Exception ex)
                {
                    WriteLog("RTX配置保存失败!" + ex.Message);
                }
            }
            else
            {
                toolStripButtonSet.Text = "RTX配置保存";
                tboxRIP.Enabled = true;
                tboxRPort.Enabled = true;
                tboxRUser.Enabled = true;
                tboxRPSW.Enabled = true;
                textBoxConnStr.Enabled = true;
                textBoxTFSUser.Enabled = true;
                textBoxTFSPsw.Enabled = true;
                tboxMailIP.Enabled = true;
                radioButtonGroup.Enabled = true;
                radioButtonOne.Enabled = true;
                checkBoxSys.Enabled = true;
                checkBoxIM.Enabled = true;
                tboxSysAlertTime.Enabled = true;
                lblSysAlertTime.Enabled = true;

                tboxMailPort.Enabled = true;
                textBoxTFSDetailURL.Enabled = true;
            }
        }

        bool CheckExtWork = true;//是否提醒加班人群注意提交加班单
        bool SendLateTaskAlter = true;//是否提醒工单
        /// <summary>
        /// 计时器事件
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (StartTime != null)
                {
                    TimeSpan ts = DateTime.Now - StartTime;
                    labelStatus.Text = string.Format("已经运行{0}天{1}小时{2}分钟{3:D2}秒", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                }
                if (!SendLateTaskAlter && (DateTime.Now.Hour < 9 && DateTime.Now.Hour > 4 || DateTime.Now.Hour > 12 && DateTime.Now.Hour < 15 || DateTime.Now.Hour > 17 && DateTime.Now.Hour < 19))
                {
                    SendLateTaskAlter = true;
                }
                if (SendLateTaskAlter &&
                        (
                            Convert.ToDateTime(DateTime.Now.ToShortTimeString()) > Convert.ToDateTime("16:00") && Convert.ToDateTime(DateTime.Now.ToShortTimeString()) < Convert.ToDateTime("16:15") ||
                            Convert.ToDateTime(DateTime.Now.ToShortTimeString()) > Convert.ToDateTime("9:45") && Convert.ToDateTime(DateTime.Now.ToShortTimeString()) < Convert.ToDateTime("9:55") ||
                            Convert.ToDateTime(DateTime.Now.ToShortTimeString()) > Convert.ToDateTime("20:25") && Convert.ToDateTime(DateTime.Now.ToShortTimeString()) < Convert.ToDateTime("20:45")
                        )
                    )
                {
                    SendLateTaskAlter = false;
                    SendTaskLateMsg();
                }
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        private void toolStripButtonQuit_Click(object sender, EventArgs e)
        {
            toolStripButtonStop_Click(null, null);
            Application.Exit();
        }
        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                string balltipstr = notifyIcon1.Text;
                notifyIcon1.ShowBalloonTip(5, "消息提示", balltipstr, ToolTipIcon.Info);
                e.Cancel = true;
            }
        }

        private void 打开窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }

        private void 退出服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonStop_Click(null, null);
            Application.Exit();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }


        private void SMTP_Server_ValidateIPAddress(object sender, LumiSoft.Net.ValidateIP_EventArgs e)
        {
            e.Validated = true;
        }
        private void Server_AuthenticateUser(object sender, LumiSoft.Net.SMTP.Server.AuthUser_EventArgs e)
        {
            e.Validated = true;
        }
        private void smtp_Server_ValidateSender(object sender, LumiSoft.Net.SMTP.Server.ValidateSender_EventArgs e)
        {
            e.Validated = true;
        }
        private void smtp_Server_ValidateRecipient(object sender, LumiSoft.Net.SMTP.Server.ValidateRecipient_EventArgs e)
        {
            e.Validated = true;
        }
        private void SMTP_Server_ValidateMailBoxSize(object sender, LumiSoft.Net.SMTP.Server.ValidateMailboxSize_EventArgs e)
        {
            e.IsValid = true;
        }
        private void smtp_Server_NewMailEvent(object sender, LumiSoft.Net.SMTP.Server.NewMail_EventArgs e)
        {
            try
            {

                ProcessAndStoreMessage(e.MailFrom, e.MailTo, e.MessageStream);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
        internal void ProcessAndStoreMessage(string sender, string[] recipient, MemoryStream msgStream)
        {
            try
            {
                WriteLog("收到一封邮件");
                string html = System.Text.Encoding.GetEncoding("utf-8").GetString(msgStream.ToArray());
                MatchCollection matches = Regex.Matches(html, @"(Content-Transfer-Encoding: base64)\s+(.+\s{0,2})+");
                if (matches.Count == 0)
                {
                    WriteLog("EML解析错误" + html);
                    return;
                }
                foreach (Match s in matches)
                {
                    html = s.Value;
                    html = Regex.Replace(html, @"(Content-Transfer-Encoding: base64)\s+", "");
                    break;
                }
                html = System.Text.Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(html));
                ParseTFSMailString(html);
            }
            catch (System.Exception ex)
            {
                Log.Info(ex);
            }

        }
        private void SMTP_Server_SessionLog(object sender, LumiSoft.Net.Log_EventArgs e)
        {
            //  WriteLog(e.LogText);
        }
        private void OnServer_SysError(object sender, LumiSoft.Net.Error_EventArgs e)
        {
            if (e.Exception.Message.IndexOf("中断") == -1)
            {
                toolStripButtonStop_Click(null, null);
                WriteLog(e.Exception.Message);
            }
        }
        private void SendTaskLateMsg()
        {

            try
            {
                string sqlXXCQ = @"SELECT [ID]
                                    , [Title]
                                    , [Name]
                                    , [State]
                                    ,[Work Item Type]
                                    ,datediff(HOUR,[Tfs_Warehouse].[dbo].[DimWorkItem].[Microsoft_VSTS_Scheduling_StartDate],CONVERT(varchar,GETDATE(),112)) as [Delta Start Time]
                                    ,datediff(HOUR,[Tfs_Warehouse].[dbo].[DimWorkItem].[Microsoft_VSTS_Scheduling_FinishDate],CONVERT(varchar,GETDATE(),112)) as [Delta Finish Time]

                                    FROM [Tfs_XXCQ].[dbo].[WorkItemsLatest],[Tfs_Warehouse].[dbo].[DimWorkItem],[Tfs_Warehouse].[dbo].[DimPerson]
                                    where 
                                    (
                                        [Tfs_XXCQ].[dbo].[WorkItemsLatest].[Work Item Type] != '用户情景'
                                        and [Tfs_XXCQ].[dbo].[WorkItemsLatest].[Work Item Type] != 'Bug'
                                        and [Tfs_XXCQ].[dbo].[WorkItemsLatest].[Rev] = [Tfs_Warehouse].[dbo].[DimWorkItem].[System_Rev] 
                                        and [Tfs_XXCQ].[dbo].[WorkItemsLatest].[ID] = [Tfs_Warehouse].[dbo].[DimWorkItem].[System_Id]
                                        and [Tfs_Warehouse].[dbo].[DimWorkItem].[System_AssignedTo__PersonSK] = [Tfs_Warehouse].[dbo].[DimPerson].[PersonSK]
                                        and
                                        (
                                            ([Tfs_XXCQ].[dbo].[WorkItemsLatest].[State] = '新建' 
                                            and [Tfs_Warehouse].[dbo].[DimWorkItem].[Microsoft_VSTS_Scheduling_StartDate] < CONVERT(varchar,DATEADD(d,1,GETDATE()),112))
                                            or
                                            ([Tfs_XXCQ].[dbo].[WorkItemsLatest].[State] = '活动'
                                            and[Tfs_Warehouse].[dbo].[DimWorkItem].[Microsoft_VSTS_Scheduling_FinishDate] < CONVERT(varchar,DATEADD(d,1,GETDATE()),112))
                                        )
                                    )";
                WriteLog("开始读取将到期的或未按时进行中的任务列表");
                DbHelperSQLP db = new DbHelperSQLP();
                db.connectionString = textBoxConnStr.Text;
                db.ExecuteSql(sqlXXCQ);
                DataSet dstask = db.Query(sqlXXCQ);
                int SysAlertTime = Convert.ToInt32(Convert.ToDouble(tboxSysAlertTime.Text) * 1000);
                foreach (DataRow dr in dstask.Tables[0].Rows)
                {

                    string itemid = dr["ID"].ToString();
                    string strUrl = textBoxTFSDetailURL.Text.Replace("{0}", itemid);
                    string name = dr["Name"].ToString();
                    string taskName = dr["Title"].ToString();
                    string state = dr["State"].ToString();
                    string taskType = dr["Work Item Type"].ToString();
                    string DeltaStartTime = dr["Delta Start Time"].ToString();
                    string DeltaFinishTime = dr["Delta Finish Time"].ToString();

                    string rtxMsg = "任务名称:" + taskName + "\r\n" + "任务状态:" + state + "\r\n" + "[点击进入详细任务显示|" + strUrl + "]\r\n";
                    WriteLog("发送延迟提醒：" + "itemid:" + itemid + "name:" + name + "strurl:::" + strUrl);
                    //给这个人发送延迟提醒
                    RootObj.SendNotify(name, "★★★即将延期任务提醒★★★", SysAlertTime, rtxMsg);


                    //给部门负责人发送延迟提醒
                    string masterName = "";
                    switch (taskType)
                    {
                        case "策划任务":
                            masterName = "李小伟";
                            break;
                        case "美术任务":
                            masterName = "于涛";
                            break;
                        case "平台任务":
                            masterName = "卢珊";
                            break;
                        case "程序任务":
                            masterName = "田博辉;王志杰;刘英伟";
                            break;
                        default:
                            break;
                    }
                    if (masterName.IndexOf(name) == -1)
                    {
                        RootObj.SendNotify(masterName, "★★★部门内即将延期任务提醒★★★", SysAlertTime, rtxMsg);
                    }

                    //给田老大发送超过48小时没开始或者没完成的任务

                    if (state == "新建")
                    {
                        if (Convert.ToInt32(DeltaStartTime) >= 48)
                        {
                            RootObj.SendNotify("田博辉", "★★★超过48小时未开始任务★★★", SysAlertTime, rtxMsg);
                        }
                    }
                    if (state == "活动")
                    {
                        if (Convert.ToInt32(DeltaFinishTime) >= 48)
                        {
                            RootObj.SendNotify("田博辉", "★★★超过48小时未完成任务★★★", SysAlertTime, rtxMsg);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.Message);
            }

        }

        public static log4net.ILog Log = log4net.LogManager.GetLogger("GSSLog");

        //写日志委托
        private delegate void WriteLogDelegate(string msg);

        /// <summary>
        /// 写日志
        /// </summary>
        private void WriteLog(string msg)
        {
            if (this.InvokeRequired)
            {
                WriteLogDelegate d = new WriteLogDelegate(WriteLog);
                object arg = msg;
                this.Invoke(d, arg);
            }
            else
            {
                WriteLog1(msg);
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        private void WriteLog1(string msg)
        {
            try
            {
                Log.Info(msg);
                lock (tboxInfo)
                {
                    if (tboxInfo.Text.Length > 5000)
                    {
                        tboxInfo.Text = "";
                    }
                    tboxInfo.Text += string.Format("--------------{0}---------------\n", DateTime.Now);
                    tboxInfo.Text += string.Format("{0}\n", msg);
                    tboxInfo.SelectionStart = tboxInfo.TextLength;
                    tboxInfo.ScrollToCaret();
                }
                Log.Info("+++++++++++++++++++++++");
            }
            catch (System.Exception ex)
            {
                Log.Info(msg, ex);
            }
        }

        private void textBoxConnStr_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void tboxMailIP_TextChanged(object sender, EventArgs e)
        {

        }


        private void ParseTFSMailString(string content)
        {
            const string assignedToStart = "指派给:";
            const string assignedToEnd = "状态:";
            const string stateStart = "状态:";
            const string stateEnd = "原因:";
            const string taskNameStart = "工作项已更改:";
            const string taskNameEnd = "(";
            const string taskURLStart = "(";
            const string taskURLEnd = ")";
            const string changedByStart = "字段: 更改者新值:";
            const string RTXSendStart = "字段: 隐藏RTX要通知的人新值:";

            //查找要指派的人
            int IndexofA = content.IndexOf(assignedToStart);
            int IndexofB = content.IndexOf(assignedToEnd);
            string assignedTo = content.Substring(IndexofA + assignedToStart.Length + 1, IndexofB - IndexofA - assignedToStart.Length - 1).Trim();

            //查找当前状态
            IndexofA = content.IndexOf(stateStart);
            IndexofB = content.IndexOf(stateEnd);
            string state = content.Substring(IndexofA + stateStart.Length + 1, IndexofB - IndexofA - stateStart.Length - 1).Trim();

            //任务名称
            IndexofA = content.IndexOf(taskNameStart);
            IndexofB = content.IndexOf(taskNameEnd);
            string taskName = content.Substring(IndexofA + taskNameStart.Length + 1, IndexofB - IndexofA - taskNameStart.Length - 1).Trim();

            //任务地址
            IndexofA = content.IndexOf(taskURLStart);
            IndexofB = content.IndexOf(taskURLEnd);
            string taskURL = content.Substring(IndexofA + taskURLStart.Length, IndexofB - IndexofA - taskURLStart.Length).Trim();

            string changer = "";
            if (state != "新建")
            {
                //更改者
                IndexofA = content.IndexOf(changedByStart);
                string temp = content.Substring(IndexofA + changedByStart.Length + 1, content.Length - IndexofA - changedByStart.Length - 1).Trim();
                IndexofB = temp.IndexOf("\r\n");
                changer = content.Substring(IndexofA + changedByStart.Length + 1, IndexofB).Trim();
            }

            string rtxSend = "";
            if (state == "已关闭")
            {
                //更改者
                IndexofA = content.IndexOf(RTXSendStart);
                string temp = content.Substring(IndexofA + RTXSendStart.Length + 1, content.Length - IndexofA - RTXSendStart.Length - 1).Trim();
                IndexofB = temp.IndexOf("\r\n");
                rtxSend = content.Substring(IndexofA + RTXSendStart.Length + 1, IndexofB).Trim();
            }


            //如果是指派人和更改人相同就不发提醒了
            if (assignedTo == changer)
            {
                return;
            }
            int SysAlertTime = Convert.ToInt32(Convert.ToDouble(tboxSysAlertTime.Text) * 1000);
            if (assignedTo == "")
            {
                return;
            }
            string changeRTX = "";
            if (changer != "")
            {
                changeRTX = "更改者:" + changer;
            }
            string rtxMsg = "任务名称:" + taskName + "\r\n" + stateStart + state + "\r\n" + "[点击进入详细任务显示|" + taskURL + "]\r\n" + changeRTX;
            RootObj.SendNotify(assignedTo, state + "任务", SysAlertTime, rtxMsg);

            if (rtxSend != "")
            {
                RootObj.SendNotify(rtxSend, state + "任务", SysAlertTime, rtxMsg);
            }
        }

        private void ParseURLContent()
        {
            try
            {
                string URL = "http://tfsserver:8080/tfs/web/wi.aspx?pcguid=00278750-36d4-469d-ab76-65e9e1bfc66e&id=67";
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(URL);
                webrequest.UseDefaultCredentials = true;
                string html = "";
                using (WebResponse webreponse = (HttpWebResponse)webrequest.GetResponse())
                {
                    Stream stream = webreponse.GetResponseStream();
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        html = sr.ReadToEnd();
                    }

                }
                if (html.Length > 0)
                {
                    WriteLog("HTML读取正确");
                }
                else
                {
                    WriteLog("HTML读取ERRO");
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
    }
}