using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GSSCSFrameWork;
using System.Media;
using GSS.DBUtility;
using System.IO;
using GSSUI;
using GSSUI.AForm;

namespace GSSClient
{
    public partial class Form2 : GSSUI.AForm.FormMainSkin
    {
 #region 私有变量
        /// <summary>
        /// 客户端通讯实例
        /// </summary>
        public TcpCli clientNet = null;
        /// <summary>
        /// 客户端处理实例
        /// </summary>
        public ClientHandles clihandle = null;
        #endregion

        /// <summary>
        /// 窗体任务栏闪动提示
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        /// <summary>
        /// 构造函数
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }

        #region 窗体事件

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //窗体位置居中
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            //权限
            if (ShareData.UserPower.IndexOf(",toolStripButtonT0,") < 0)
            {
                toolStripButtonT0.Checked = false;
                toolStripButtonT10.Checked = true;
                tabControl1.SelectedIndex = 1;
            }
            //网络初始化
            NetInit();
            //窗体初始化
            ControlInit();
            PlayNotic(0);
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        private void FormTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                
                timerMain.Stop();
                FormClose form = new FormClose(this.Location, this.Size);

                DialogResult IsOK = form.ShowDialog();
                this.Invalidate(true);
                if (IsOK == DialogResult.Yes)
                {
                    clientNet.Close();
                    // Win32.AnimateWindow(this.Handle, 1500, Win32.AW_BLEND|Win32.AW_HIDE);
                    //日志记录
                    ShareData.Log.Info("退出GSS系统");
                    Application.Exit();
                }
                else
                {
                   
                    e.Cancel = true;
                    timerMain.Start();
                    
                }
            }

        }

        /// <summary>
        /// 窗体位置变更事件
        /// </summary>
        private void FormTask_LocationChanged(object sender, EventArgs e)
        {
            //停止窗体停靠
            mStopAnthor();
        }

        #endregion

        #region 初始化相关

        /// <summary>
        /// 窗体控件初始化
        /// </summary>
        private void ControlInit()
        {
            timerMain.Start();
            toolStripStatusLabelCurrentUser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripStatusLabelTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripStatusLabelTime.Text = string.Format("{0:yyyy"+LanguageResource.Language.LblUnitYear+"MM"+LanguageResource.Language.LblUnitMonth+"dd"+LanguageResource.Language.LblUnitDay+" dddd HH:mm:ss }", DateTime.Now);
            ShareData.FormhidMain = this.Handle.ToInt32();

            ToolSBtn_AllClickEventBind();
            ButtonTypeInit();
        }

        /// <summary>
        /// 工单建立按键事件绑定及初始化
        /// </summary>
        private void ButtonTypeInit()
        {
            this.buttonType1.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType2.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType3.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType4.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType5.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType6.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType7.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType8.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType9.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType10.Click += new System.EventHandler(this.buttonType1_Click);

            this.buttonType21.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType22.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType23.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType24.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType25.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType26.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType27.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType28.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType29.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType30.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType31.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType32.Click += new System.EventHandler(this.buttonType1_Click);

            this.buttonType51.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType52.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType53.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType54.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType55.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType56.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType57.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType58.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType59.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType60.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType61.Click += new System.EventHandler(this.buttonType1_Click);
            this.buttonType62.Click += new System.EventHandler(this.buttonType1_Click);
        }

        /// <summary>
        /// 绑定所有菜单按钮单击事件
        /// </summary>
        private void ToolSBtn_AllClickEventBind()
        {
            //权限
            foreach (ToolStripItem control in this.toolStripMain.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
                {
                    System.Windows.Forms.ToolStripButton toolbtnActiv = (control as System.Windows.Forms.ToolStripButton);
                    if (ShareData.UserPower.IndexOf("," + toolbtnActiv.Name + ",") < 0)
                    {
                        toolbtnActiv.Enabled = false;

                    }

                    toolbtnActiv.Click += new System.EventHandler(this.toolStripButtonT0_Click);
                    toolbtnActiv.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonT0_Paint);
                }
            }

            //我的工单菜单
            this.toolStripButtonTT0.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT1.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT2.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT3.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT4.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT5.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT6.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT7.Click += new System.EventHandler(this.toolStripButtonTT0_Click);
            this.toolStripButtonTT8.Click += new System.EventHandler(this.toolStripButtonTT0_Click);

            this.toolStripButtonTT0.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT5.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT6.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT7.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);
            this.toolStripButtonTT8.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripButtonTT0_Paint);

        }

        /// <summary>
        /// 缓存数据控件初始化
        /// </summary>
        private void SetCacheControls()
        {
            try
            {
                BindDicComb(combTtasktypeP, "2000");
                BindDicComb(combTtasktype, "200000");
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Warn("系统实始化失败!" + ex.Message);
            }



            toolStripStatusLabelCurrentUser.Text = LanguageResource.Language.LblNowUser+":" + ClientCache.GetUserNameT(ShareData.UserID) + " ";
        }

        /// <summary>
        /// 绑定COMBOX字典控件
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="parentid"></param>
        private void BindDicComb(ComboBox cb, string parentid)
        {
            DataSet ds = ClientCache.GetCacheDS();
            if (ds != null)
            {
                DataTable dtdic = ds.Tables["T_Dictionary"].Clone();
                DataRow dra = dtdic.NewRow();
                dra["F_Value"] = LanguageResource.Language.LblallType;
                dra["F_DicID"] = "0";
                dtdic.Rows.Add(dra);
                DataRow[] drdic = ds.Tables["T_Dictionary"].Select("F_ParentID=" + parentid + "");
                foreach (DataRow dr in drdic)
                {
                    dtdic.ImportRow(dr);
                }
                cb.DataSource = dtdic;
                cb.DisplayMember = "F_Value";
                cb.ValueMember = "F_DicID";
                cb.SelectedIndex = 0;
            }
        }

        #endregion

        #region 网络相关

        /// <summary>
        /// 网络通讯初始化
        /// </summary>
        private void NetInit()
        {
            //for (int i = 0; i < 20000;i++ )
            //{

            try
            {
                clientNet = new TcpCli(new Coder(Coder.EncodingMothord.Default));
                clientNet.Resovlver = new DatagramResolver("]$}");
                clientNet.ReceivedDatagram += new NetEvent(this.RecvData);
                clientNet.DisConnectedServer += new NetEvent(this.ClientClose);
                clientNet.ConnectedServer += new NetEvent(this.ClientConn);
                clientNet.CannotConnectedServer += new NetEvent(this.CannotConnectedServer);
                clientNet.Connect(ShareData.LocalIp, ShareData.LocalPort);

                //实例化处理层
                clihandle = new ClientHandles(clientNet);
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Warn(ex);
                MsgBox.Show(LanguageResource.Language.Tip_ConnectionServiceError+"\r\n"+LanguageResource.Language.Tip_PleaseInspectNetOrService+"!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               
            }


            //}
        }

        /// <summary>
        /// 网络通讯-无法连接服务器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CannotConnectedServer(object sender, NetEventArgs e)
        {
            string info = LanguageResource.Language.Tip_Tip + ":" + LanguageResource.Language.Tip_ConnectionServiceError;
            toolStripStatusLabel1.Text = info;
            toolStripStatusLabel3.Visible = true;

            //日志记录
            ShareData.Log.Warn(info);
        }
        /// <summary>
        /// 网络通讯-客户端连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientConn(object sender, NetEventArgs e)
        {
            toolStripStatusLabel3.Visible = false;
            string info = string.Format(LanguageResource.Language.Tip_Client + ":{0}" + LanguageResource.Language.Tip_ConnectionService + ":{1}", e.Client, e.Client.ClientSocket.RemoteEndPoint.ToString());
            toolStripStatusLabel1.Text = info;
            clientNet.ClientSession.UserID = ShareData.UserID;

            clihandle.GetCahce(ShareData.UserID);

            //  button1_Click(null, null);

            //权限
            if (ShareData.UserPower.IndexOf(",toolStripButtonT0,") < 0)
            {
                GetTaskList("");
            }

            //日志记录
            ShareData.Log.Info(info);
        }
        /// <summary>
        /// 网络通讯-客户端连接关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientClose(object sender, NetEventArgs e)
        {
            toolStripStatusLabel3.Visible = true;
            string info;

            if (e.Client.TypeOfExit == Session.ExitType.ExceptionExit)
            {
                info = string.Format(LanguageResource.Language.Tip_Client+"Session:{0}状态:异外关闭.",
                 e.Client.ID);
            }
            else
            {
                info = string.Format(LanguageResource.Language.Tip_Client + "Session:{0}状态:正常关闭.",
                 e.Client.ID);
            }
            //日志记录
            ShareData.Log.Warn(info);

            FlashWindow(this.Handle, true);//闪烁

            toolStripStatusLabel1.Text = info;

        }
        /// <summary>
        /// 网络通讯-客户端接收数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RecvData(object sender, NetEventArgs e)
        {

            string info = string.Format(LanguageResource.Language.LblReceiveData + ":{0}bytes "+LanguageResource.Language.LblFrom+":{1}.", e.Client.MsgStrut.Data.Length, e.Client);
            toolStripStatusLabel1.Text = info;


            if (e.Client.MsgStrut == null)
            {
                return;
            }
            try
            {
                switch (e.Client.MsgStrut.command)
                {
                    case msgCommand.GetCache:
                        ClientCache.SetCache(e.Client.MsgStrut.Data);
                        SetCacheControls();
                        break;
                    case msgCommand.GetAlertNum:
                        ClientCache.SetTaskCache(e.Client.MsgStrut.Data, "TaskAlertNum");
                        //工单数量变化提醒
                        PlayNotic(1);
                        // FlashWindow(this.Handle, true);//闪烁
                        break;
                    case msgCommand.GetAllTasks:
                        DataSet dsTask = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        if (e.Client.MsgStrut.MsgParam.p0 == "toolStripButtonT11")
                        {
                            SetTaskValueGN(dsTask.Tables[0]);
                        }
                        else if (e.Client.MsgStrut.MsgParam.p0 == "toolStripButtonT12")
                        {
                            SetTaskValueGA(dsTask.Tables[0]);
                        }
                        else
                        {
                            SetTaskValue(dsTask.Tables[0]);
                        }

                        ClientCache.SetTaskCache(e.Client.MsgStrut.Data, "TaskList");
                        break;
                    case msgCommand.GetTaskLog:
                        DataSet dsTaskLog = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        SetTaskLogValue(dsTaskLog.Tables[0]);
                        ClientCache.SetTaskCache(e.Client.MsgStrut.Data, "TaskLog");
                        break;
                    case msgCommand.GetGameUsersC:
                        DataSet dsGameUsersC = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        if (dsGameUsersC == null)
                        {
                            MsgBox.Show("游戏数据拉取错误!", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        SetGameUserCValue(dsGameUsersC.Tables[0]);
                        if (DGVGameUser.SelectedRows.Count > 0)
                        {
                            string bigzonename = DGVGameUser.SelectedRows[0].Cells[2].Value.ToString();
                            string id = DGVGameUser.SelectedRows[0].Cells[0].Value.ToString();
                            clihandle.GetGameRolesC(bigzonename + "|" + id);
                        }
                        break;
                    case msgCommand.GetGameRolesC:
                        DataSet dsGameRoleC = DataSerialize.GetDatasetFromByte(e.Client.MsgStrut.Data);
                        SetGameRoleCValue(dsGameRoleC.Tables[0]);
                        break;
                    case msgCommand.AddTask:
                        string Tid = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        SetFormsMSG(ShareData.FormhidAdd, 601, Convert.ToInt32(Tid));
                        break;
                    case msgCommand.EditTask:
                        Tid = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        SetFormsMSG(ShareData.FormhidEdit, 601, Convert.ToInt32(Tid));
                        break;
                    case msgCommand.EditTaskNoReturn:
                        Tid = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        // SetFormsMSG(_taskEdithandid, 601, Convert.ToInt32(Tid));
                        break;
                    case msgCommand.ExcSql:
                        string msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        string[] formsql = msgStr.Split('|');//分为两段,执行SQL语句的窗口ID和语句
                        int formid = Convert.ToInt32(formsql[0]);
                        break;
                    case msgCommand.GameLockUR:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        string[] msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        int backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.GameNoLockUR:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.GameUserUse:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.GameUserNoUse:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    case msgCommand.GameResetChildInfo:
                        msgStr = clientNet.ServerCoder.GetEncodingString(e.Client.MsgStrut.Data, e.Client.MsgStrut.Data.Length);
                        msgs = msgStr.Split('|');//分为两段,FORM编号+返回结果(字符串:true或错误结果)
                        formid = Convert.ToInt32(msgs[0]);
                        backid = ShareData.Msg.Add(msgs[1]);
                        SetFormsMSG(formid, 601, backid);
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Warn("接收数据出错!位置" + e.Client.MsgStrut.command.ToString() + "信息:" + ex.Message);
            }


        }

        /// <summary>
        /// 重连服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            //重连服务器
            NetInit();
        }
        #endregion

        #region 委托相关

        //窗口消息的委托
        private delegate void Setfrommsg(int handid, int msgid, int recv);
        private void SetFormsMSG(int handid, int msgid, int recv)
        {
            if (this.InvokeRequired)
            {
                Setfrommsg d = new Setfrommsg(SetFormsMSG);
                object hid = handid;
                object arg = msgid;
                object rec = recv;
                this.Invoke(d, hid, arg, rec);
            }
            else
            {
                FormsMsg.PostMessage(handid, msgid, recv, 0);
            }
        }

        //工单列表控件的委托
        private delegate void SetDGValue(DataTable dt);
        private void SetTaskValue(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetDGValue d = new SetDGValue(SetTaskValue);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                DGVTask.AutoGenerateColumns = false;
                DGVTask.DataSource = dt;
                int index = ShareData.DGVSelectIndex;
                if (index < DGVTask.Rows.Count && index != 0)
                {
                    DGVTask.Rows[index].Selected = true;
                    DGVTask.Rows[0].Selected = false;
                }
                //SharData.DGVSelectIndex = 0;
                // dataGridView3.Columns[0].HeaderText = "(" + dt.Rows.Count + ")";
                //dataGridView3.Sort(dataGridViewZ.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            }
        }

        private delegate void SetDGValueGN(DataTable dt);
        private void SetTaskValueGN(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetDGValueGN d = new SetDGValueGN(SetTaskValueGN);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                DGVTaskGN.AutoGenerateColumns = false;
                DGVTaskGN.DataSource = dt;
                int index = ShareData.DGVSelectIndex;
                if (index < DGVTaskGN.Rows.Count && index != 0)
                {
                    DGVTaskGN.Rows[0].Selected = false;
                    DGVTaskGN.Rows[index].Selected = true;

                    string id = DGVTaskGN.Rows[index].Cells[2].Value.ToString();
                    string sqlwhere = "and F_ID=" + id + " order by F_LogID asc";
                    clihandle.GetTaskLog(sqlwhere);

                }
            }
        }

        private delegate void SetDGValueGA(DataTable dt);
        private void SetTaskValueGA(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetDGValueGA d = new SetDGValueGA(SetTaskValueGA);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                DGVTaskGA.AutoGenerateColumns = false;
                DGVTaskGA.DataSource = dt;
                int index = ShareData.DGVSelectIndex;
                if (index < DGVTaskGA.Rows.Count && index != 0)
                {
                    DGVTaskGA.Rows[0].Selected = false;
                    DGVTaskGA.Rows[index].Selected = true;

                    string id = DGVTaskGA.Rows[index].Cells[2].Value.ToString();
                    string sqlwhere = "and F_ID=" + id + " order by F_LogID asc";
                    clihandle.GetTaskLog(sqlwhere);

                }
            }
        }

        //游戏玩家列表控件的委托
        private delegate void SetGameDGValue(DataTable dt);
        private void SetGameUserCValue(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetGameDGValue d = new SetGameDGValue(SetGameUserCValue);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                DGVGameUser.AutoGenerateColumns = false;
                DGVGameUser.DataSource = dt;
            }
        }

        //游戏角色列表控件的
        private void SetGameRoleCValue(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetGameDGValue d = new SetGameDGValue(SetGameRoleCValue);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                DGVGameRole.AutoGenerateColumns = false;
                DGVGameRole.DataSource = dt;
            }
        }



        //工单历史控件的委托
        private delegate void SetRichtxtValue(DataTable dt);
        /// <summary>
        /// 设置工单历史
        /// </summary>
        /// <param name="dt"></param>
        private void SetTaskLogValue(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                SetRichtxtValue d = new SetRichtxtValue(SetTaskLogValue);
                object arg = dt;
                this.Invoke(d, arg);
            }
            else
            {
                if (richtbTasklog.Visible)
                {
                    richtbTasklog.Clear();
                    try
                    {
                        StringBuilder changStr = new StringBuilder();
                        changStr.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ");

                        foreach (DataRow dr in dt.Rows)
                        {
                            changStr.Append(@"\pard{\b[" + LanguageResource.Language.LblTime + "]:}" + dr["F_EditTime"] + @"\par ");
                            changStr.Append(@"{\b ["+LanguageResource.Language.LblUser+"]:}" + dr["F_EditMan"] + @"\par ");

                            string dowhatStr =  LanguageResource.Language.LblDataChange;
                            if (dr["F_State"].ToString().Trim().Length > 0)
                            {
                                dowhatStr = @"\cf1\b" + LanguageResource.Language.LblWorkOrderStatueChange + @"\b0\cf0 ";
                            }
                            else if (dr["F_TToolUsed"].ToString().Trim() == "True")
                            {
                                dowhatStr = @"\cf3\b" + LanguageResource.Language.LblToolUse + @"\b0\cf0 ";
                            }

                            changStr.Append(@"{\b ["+LanguageResource.Language.LblOperate+"]:}" + dowhatStr + @"\par ");
                            changStr.Append(@"{\b ["+LanguageResource.Language.LblData+@"]:}\par\pard\li57\ri57 ");


                            //用户名F_State
                            string[] colms = { LanguageResource.Language.LblWorkOrderSatue + "|F_State", LanguageResource.Language.LblCurrentPersonInCharge + "|F_DutyMan", LanguageResource.Language.LblPreviousPersonInCharge + "|F_PreDutyMan", LanguageResource.Language.LblTitle + "|F_Title", LanguageResource.Language.LblSource + "|F_From", LanguageResource.Language.LblVipLevel + "|F_VipLevel", LanguageResource.Language.LblLimitTimeNoun + "|F_LimitTime", LanguageResource.Language.LblType + "|F_Type", LanguageResource.Language.LblGame + "|F_GameName", LanguageResource.Language.LblBigZone + "|F_GameBigZone", LanguageResource.Language.LblZone + "|F_GameZone", LanguageResource.Language.LblAccount + "|F_GUserName", LanguageResource.Language.LblRole + "|F_GRoleName", LanguageResource.Language.LblContacter + "||F_GPeopleName", LanguageResource.Language.LblTel + "|F_Telphone", LanguageResource.Language.LblSureAccount + "|F_CUserName", LanguageResource.Language.LblSureSecurity + "|F_CPSWProtect", LanguageResource.Language.LblSureOther + "|F_COther", LanguageResource.Language.LblLastLoginTime + "|F_OLastLoginTime", LanguageResource.Language.LblCanResetData + "|F_OCanRestor", LanguageResource.Language.LblOftenGameLocation + "|F_OAlwaysPlace", LanguageResource.Language.LblRemark + "|F_Note", LanguageResource.Language.LblToolUse + "|F_TUseData" };
                            foreach (string colm in colms)
                            {
                                if (dr[colm.Split('|')[1]].ToString().Trim().Replace("-", "").Length > 0)
                                {

                                    changStr.Append(@"[" + colm.Split('|')[0] + @"]{\cf2  " + dr[colm.Split('|')[1]].ToString().Trim().Replace("\n", @"\par") + @" }\par ");

                                }

                            }

                            changStr.Append(@"\highlight0-----------------------------------\highlight0\par ");



                        }
                        changStr.Append(@"}");
                        richtbTasklog.Rtf = changStr.ToString();

                    }
                    catch (System.Exception ex)
                    {
                        ex.ToString().ErrorLogger();
                        richtbTasklog.AppendText("提示:工单历史提取数据出错!" + ex.Message);
                        //日志记录
                        ShareData.Log.Warn("工单历史数据显示出错!" + ex.Message);
                    }

                    richtbTasklog.Invalidate();
                    richtbTasklog.SelectionStart = richtbTasklog.TextLength;
                    richtbTasklog.ScrollToCaret();

                }
                else if (richtbTasklogGN.Visible)
                {
                    richtbTasklogGN.Clear();
                    try
                    {
                        StringBuilder changStr = new StringBuilder();
                        //changStr.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ");
                        string colorlistStr = "";
                        string changStrColor = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            changStr.Append(@"\pard{\b[" + LanguageResource.Language.LblTime + "]:}" + dr["F_EditTime"] + @"\par ");
                            changStr.Append(@"{\b ["+LanguageResource.Language.LblUser+"]:}" + dr["F_EditMan"] + @"\par ");

                            string dowhatStr =  LanguageResource.Language.LblDataChange;
                            if (dr["F_State"].ToString().Trim().Length > 0)
                            {
                                dowhatStr = @"\cf1\b" + LanguageResource.Language.LblWorkOrderStatueChange + @"\b0\cf0 ";
                            }
                            else if (dr["F_TToolUsed"].ToString().Trim() == "True")
                            {
                                dowhatStr = @"\cf3\b" + LanguageResource.Language.LblToolUse + @"\b0\cf0 ";
                            }

                            changStr.Append(@"{\b ["+LanguageResource.Language.LblOperate+"]:}" + dowhatStr + @"\par ");
                            changStr.Append(@"{\b ["+LanguageResource.Language.LblData+@"]:}\par\pard\li57\ri57 ");


                            //用户名F_State
                            string[] colms = { LanguageResource.Language.LblWorkOrderSatue+"|F_State", LanguageResource.Language.LblCurrentPersonInCharge+"|F_DutyMan", LanguageResource.Language.LblPreviousPersonInCharge+"|F_PreDutyMan",LanguageResource.Language.LblTitle+"|F_Title", LanguageResource.Language.LblSource+"|F_From",LanguageResource.Language.LblVipLevel+"|F_VipLevel", LanguageResource.Language.LblLimitTimeNoun+"|F_LimitTime", LanguageResource.Language.LblType+"|F_Type", LanguageResource.Language.LblGame+"|F_GameName",  LanguageResource.Language.LblBigZone + "|F_GameBigZone",  LanguageResource.Language.LblZone+ "|F_GameZone",  LanguageResource.Language.LblAccount+ "|F_GUserName",  LanguageResource.Language.LblRole+ "|F_GRoleName", LanguageResource.Language.LblContacter+ "|F_GPeopleName",  LanguageResource.Language.LblTel+ "|F_Telphone",  LanguageResource.Language.LblSureAccount+ "|F_CUserName",  LanguageResource.Language.LblSureSecurity+ "|F_CPSWProtect", LanguageResource.Language.LblToolRecord+ "|F_COther",  LanguageResource.Language.LblLastLoginTime+ "|F_OLastLoginTime",  LanguageResource.Language.LblOftenGameLocation+ "|F_OAlwaysPlace",  LanguageResource.Language.LblRemark+ "|F_Note" };
                            foreach (string colm in colms)
                            {
                                if (dr[colm.Split('|')[1]].ToString().Trim().Replace("-", "").Length > 0)
                                {

                                    changStr.Append(@"[" + colm.Split('|')[0] + @"]{\cf2  " + dr[colm.Split('|')[1]].ToString().Trim().Replace("\n", @"\par") + @" }\par ");

                                }

                            }
                            string F_userdata = dr["F_TUseData"].ToString();
                            if (F_userdata.Trim().Length > 0)
                            {
                                string[] uds = F_userdata.Split('|');
                                string gnerea = "";

                                foreach (string ud in uds)
                                {
                                    string[] u = ud.Split(',');
                                    if (u.Length == 3)
                                    {
                                        gnerea += GetNodeValue(u[0], u[1], u[2]) + @"|\par";
                                    }
                                    else if (ud.Trim().Length > 0)
                                    {
                                        gnerea += ud + @"|\par";
                                    }
                                }
                                changStr.Append("["+LanguageResource.Language.LblNoticeRange+@"]\par{\cf2" + gnerea + @"}");
                            }


                            string Gnotice = dr["F_URInfo"].ToString().Trim().Replace("\n", @"\par\highlight0##################################\highlight0\par");
                            if (Gnotice.Trim().Length > 0)
                            {
                                //转换颜色
                                string codeBeginStr = "";
                                Gnotice = Gnotice.Replace("&199", "}");
                                System.Collections.ArrayList colorlist = new System.Collections.ArrayList();

                                for (int i = 0; i < Gnotice.Length - 10; i++)
                                {
                                    if (Gnotice.Substring(i, 2) == "&2")
                                    {
                                        codeBeginStr = Gnotice.Substring(i + 2, 9);
                                        if (WinUtil.IsNumber(codeBeginStr))
                                        {
                                            colorlist.Add(codeBeginStr);
                                        }
                                    }
                                }
                                int colorindex = 4;
                                foreach (object colora in colorlist)
                                {
                                    colorlistStr += "\\red" + colora.ToString().Substring(0, 3) + "\\green" + colora.ToString().Substring(3, 3) + "\\blue" + colora.ToString().Substring(6, 3) + ";";
                                    Gnotice = Gnotice.Replace("&2" + colora.ToString(), @"{\cf" + colorindex.ToString());
                                    colorindex++;
                                }
                                changStr.Append(LanguageResource.Language.LblNoticeText + @"]\par" + Gnotice + @" \par");
                            }
                            if (dr["F_OCanRestor"].ToString().Trim().Length > 0)
                            {
                                string istooluse = dr["F_OCanRestor"].ToString() == "True" ? LanguageResource.Language.LblStartRunning : LanguageResource.Language.LblEndRunning;
                                changStr.Append(@"\b[" + LanguageResource.Language.LblRunStatue + "]" + istooluse + @" \b0\par");
                            }

                            changStr.Append(@"\highlight0-----------------------------------\highlight0\par ");



                        }
                        changStr.Append(@"}");
                        changStrColor = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;" + colorlistStr + @"}\viewkind4\uc1\pard\lang2052\f0\fs18 " + changStr.ToString();
                        richtbTasklogGN.Rtf = changStrColor;

                    }
                    catch (System.Exception ex)
                    {
                        richtbTasklogGN.AppendText("提示:工单历史提取数据出错!" + ex.Message);
                        //日志记录
                        ShareData.Log.Warn("工单历史数据显示出错!" + ex.Message);
                    }

                    richtbTasklogGN.SelectionStart = richtbTasklogGN.TextLength;
                    richtbTasklogGN.ScrollToCaret();

                }
                else if (richtbTasklogGA.Visible)
                {
                    richtbTasklogGA.Clear();
                    try
                    {
                        StringBuilder changStr = new StringBuilder();
                        //changStr.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;}\viewkind4\uc1\pard\lang2052\f0\fs18 ");
                        string colorlistStr = "";
                        string changStrColor = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            changStr.Append(@"\pard{\b[" + LanguageResource.Language.LblTime + "]:}" + dr["F_EditTime"] + @"\par ");
                            changStr.Append(@"{\b ["+LanguageResource.Language.LblUser+"]:}" + dr["F_EditMan"] + @"\par ");

                            string dowhatStr =  LanguageResource.Language.LblDataChange;
                            if (dr["F_State"].ToString().Trim().Length > 0)
                            {
                                dowhatStr = @"\cf1\b" + LanguageResource.Language.LblWorkOrderStatueChange + @"\b0\cf0 ";
                            }
                            else if (dr["F_TToolUsed"].ToString().Trim() == "True")
                            {
                                dowhatStr = @"\cf3\b" + LanguageResource.Language.LblToolUse + @"\b0\cf0 ";
                            }

                            changStr.Append(@"{\b ["+LanguageResource.Language.LblOperate+"]:}" + dowhatStr + @"\par ");
                            changStr.Append(@"{\b ["+LanguageResource.Language.LblData+@"]:}\par\pard\li57\ri57 ");


                            //用户名F_State
                            string[] colms = { LanguageResource.Language.LblWorkOrderSatue+"|F_State", LanguageResource.Language.LblCurrentPersonInCharge+"|F_DutyMan", LanguageResource.Language.LblPreviousPersonInCharge+"|F_PreDutyMan",LanguageResource.Language.LblTitle+"|F_Title", LanguageResource.Language.LblSource+"|F_From",LanguageResource.Language.LblVipLevel+"|F_VipLevel", LanguageResource.Language.LblLimitTimeNoun+"|F_LimitTime", LanguageResource.Language.LblType+"|F_Type", LanguageResource.Language.LblGame+"|F_GameName",  LanguageResource.Language.LblBigZone + "|F_GameBigZone",  LanguageResource.Language.LblZone+ "|F_GameZone",  LanguageResource.Language.LblAccount+ "|F_GUserName",  LanguageResource.Language.LblRole+ "|F_GRoleName", LanguageResource.Language.LblContacter+ "|F_GPeopleName",  LanguageResource.Language.LblTel+ "|F_Telphone",  LanguageResource.Language.LblSureAccount+ "|F_CUserName",  LanguageResource.Language.LblSureSecurity+ "|F_CPSWProtect", LanguageResource.Language.LblToolRecord+ "|F_COther",  LanguageResource.Language.LblLastLoginTime+ "|F_OLastLoginTime",  LanguageResource.Language.LblOftenGameLocation+ "|F_OAlwaysPlace",  LanguageResource.Language.LblRemark+ "|F_Note" };
                            foreach (string colm in colms)
                            {
                                if (dr[colm.Split('|')[1]].ToString().Trim().Replace("-", "").Length > 0)
                                {

                                    changStr.Append(@"[" + colm.Split('|')[0] + @"]{\cf2  " + dr[colm.Split('|')[1]].ToString().Trim().Replace("\n", @"\par") + @" }\par ");

                                }

                            }
                            string F_userdata = dr["F_TUseData"].ToString();
                            if (F_userdata.Trim().Length > 0)
                            {
                                string[] uds = F_userdata.Split('|');
                                string gnerea = "";
                                gnerea += LanguageResource.Language.LblAwardNo + ":" + uds[0] + @"\par";
                                gnerea += "奖品名称:" + (uds[1] == "0" ? LanguageResource.Language.LblProp : LanguageResource.Language.LblPackage) + @"\par";
                                gnerea += "奖品类型:" + uds[2] + @"\par";
                                gnerea += "奖品数量:" + uds[3] + @"\par";

                                changStr.Append(@"\b[奖品内容]\b0\par{\cf0" + gnerea + @"}");
                            }


                            string URInfo = dr["F_URInfo"].ToString().Trim();

                            DataSet ds =DataSerialize.GetDatasetFromByte((byte[])DataSerialize.GetObjectFromString(URInfo)) ;
                            if (ds!=null)
                            {
                                changStr.Append(@"\b[中奖用户列表] \b0("+ds.Tables[0].Rows.Count+@")\par");
                                foreach(DataRow dru in ds.Tables[0].Rows)
                                {
                                    changStr.Append(@"用户编号:" + dru[0] + LanguageResource.Language.LblRoleNo + dru[1] + @" \par");
                                }
                            }
                           

                            changStr.Append(@"\highlight0-----------------------------------\highlight0\par ");

                        }
                        changStr.Append(@"}");
                        changStrColor = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}{\colortbl ;\red255\green0\blue0;\red30\green20\blue160;\red80\green200\blue60;" + colorlistStr + @"}\viewkind4\uc1\pard\lang2052\f0\fs18 " + changStr.ToString();
                        richtbTasklogGA.Rtf = changStrColor;

                    }
                    catch (System.Exception ex)
                    {
                        richtbTasklogGA.AppendText("提示:工单历史提取数据出错!" + ex.Message);
                        //日志记录
                        ShareData.Log.Warn("工单历史数据显示出错!" + ex.Message);
                    }

                    richtbTasklogGA.SelectionStart = richtbTasklogGA.TextLength;
                    richtbTasklogGA.ScrollToCaret();

                }

            }

        }




        #endregion

        #region 私有方法

        /// <summary>
        /// 得到IMAGE对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public System.Drawing.Image GetImage(string path)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
                System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
                fs.Close();
                return result;
            }
            catch (System.Exception ex)
            {
                //日志记录
                ShareData.Log.Warn(ex);
                return new System.Drawing.Bitmap(1, 1);
            }
        }

        /// <summary>
        /// 给关键字上色
        /// </summary>
        public int getbrush(string p, string s)
        {
            int cnt = 0;
            int M = p.Length;
            int N = s.Length;
            char[] ss = s.ToCharArray(), pp = p.ToCharArray();
            if (M > N) return 0;
            for (int i = 0; i < N - M + 1; i++)
            {
                int j;
                for (j = 0; j < M; j++)
                {
                    if (ss[i + j] != pp[j]) break;
                }
                if (j == p.Length)
                {
                    this.richtbTasklog.Select(i, p.Length);
                    this.richtbTasklog.SelectionColor = Color.SteelBlue;
                    cnt++;
                }
            }
            return cnt;
        }

        /// <summary>
        /// 停靠方法
        /// </summary>
        internal AnchorStyles StopAanhor = AnchorStyles.None;
        private bool IsAnchorBack = true;
        private void mStopAnthor()
        {
            if (this.Top <= 0)
            {
                StopAanhor = AnchorStyles.Top;
            }
            else if (this.Left <= 0)
            {
                StopAanhor = AnchorStyles.Left;
            }
            else if (this.Left >= Screen.PrimaryScreen.Bounds.Width - this.Width)
            {
                StopAanhor = AnchorStyles.Right;
            }
            else
            {
                StopAanhor = AnchorStyles.None;
            }
        }

        /// <summary>
        /// 窗体依靠
        /// </summary>
        private void SetAnchor()
        {
            bool istopmost = GSSUI.SharData.TopMost;
            if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Minimized)
            {
                return;
            }
            if (this.Bounds.Contains(Cursor.Position))
            {
                switch (this.StopAanhor)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, 0);
                        break;
                    case AnchorStyles.None:
                        if (this.TopMost && !IsAnchorBack)
                        {
                            IsAnchorBack = true;
                            GSSUI.SharData.GetUIInfo();
                            this.TopMost = GSSUI.SharData.TopMost;
                        }
                        break;
                    //case AnchorStyles.Left:
                    //    this.Location = new Point(0, this.Location.Y);
                    //    break;
                    //case AnchorStyles.Right:
                    //    this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, this.Location.Y);
                    //    break;
                }
            }
            else
            {
                switch (this.StopAanhor)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, (this.Height - 32) * (-1));
                        if (!this.TopMost)
                        {
                            this.TopMost = true;
                            GSSUI.SharData.TopMost = true;
                            IsAnchorBack = false;
                        }

                        break;
                    //case AnchorStyles.Left:
                    //    this.Location = new Point((-1) * (this.Width - 12), this.Location.Y);
                    //    break;
                    //case AnchorStyles.Right:
                    //    this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 12, this.Location.Y);
                    //    break;
                }
            }
        }

        /// <summary>
        /// 播放提醒声音
        /// </summary>
        /// <param name="playid"></param>
        private void PlayNotic(int playid)
        {
            string sFileName;
            switch (playid)
            {
                case 0://功能切换
                    sFileName = "GSSData\\Sounds\\Start.wav";
                    break;
                case 1://收到消息
                    sFileName = "GSSData\\Sounds\\Msg.wav";
                    break;
                case 2://功能切换
                    sFileName = "GSSData\\Sounds\\Chimes.wav";
                    break;
                default:
                    sFileName = "GSSData\\Sounds\\Msg.wav";
                    break;
            }
            PlaySound.play(sFileName);
            //SoundPlayer sp = new SoundPlayer(Application.StartupPath+"\\System.wav");
            //sp.Play();
        }

        #endregion

        #region 工单主页相关

        /// <summary>
        /// 弹出工单建立窗口
        /// </summary>
        private void buttonType1_Click(object sender, EventArgs e)
        {
            //工单类型相关
            string dicname = (sender as System.Windows.Forms.Button).Text;
            string parentname = (sender as System.Windows.Forms.Button).Parent.Text;
            string parentid = ClientCache.GetDicID(parentname);
            int tasktype = Convert.ToInt32(ClientCache.GetDicID(dicname, parentid));

            if (parentid.Trim().Length == 0)
            {
                MsgBox.Show("初始化未成功,请重新登录！", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //不需要游戏信息的工单
            string NoUserInfo = ",20000201,20000202,20000203,20000204,20000205,20000206,20000207,";
            if (NoUserInfo.IndexOf("," + tasktype + ",") >= 0)
            {
                FormTaskAddNoUR formn = new FormTaskAddNoUR(clihandle, tasktype);
                ShareData.FormhidAdd = formn.Handle.ToInt32();
                formn.ShowDialog();
                return;
            }

            //需要游戏信息的工单
            DataGridViewRow drGuser = null;
            DataGridViewRow drGrole = null;

            if (DGVGameUser.SelectedRows.Count == 0)
            {
                MsgBox.Show("此类型工单需要游戏帐号信息,请查询后再试！", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            drGuser = DGVGameUser.SelectedRows[0];
            if (DGVGameRole.SelectedRows.Count > 0)
            {
                drGrole = DGVGameRole.SelectedRows[0];
            }


            FormTaskAdd form = new FormTaskAdd(clihandle, drGuser, drGrole, tasktype);
            ShareData.FormhidAdd = form.Handle.ToInt32();
            form.ShowDialog();
        }

        /// <summary>
        /// 客服主页,查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string telphoneStr = txtbCtelephone.Text.Trim();
            string gameuserStr = txtbCgameuser.Text.Trim();
            string gameroleStr = txtbCgamerole.Text.Trim();
            string personidStr = txtbCpersonid.Text.Trim();

            if ((telphoneStr + gameuserStr + gameroleStr + personidStr).Length == 0)
            {
                MsgBox.Show(Text = LanguageResource.Language.Tip_PleaseInputQueryWhere, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gameuserStr.Length < 3)
            {
                MsgBox.Show("帐号至少需要输入三位！", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MsgParam msgparam = new MsgParam();
            string whereStr = " ";
            if (gameuserStr.Length > 0)
            {
                whereStr += " and F_UserName like '" + gameuserStr + "%'";
            }
            if (gameroleStr.Length > 0)
            {
                msgparam.p0 = gameroleStr;
            }
            if (personidStr.Length > 0 || telphoneStr.Length > 0)
            {
                whereStr += " and F_UserID in (select F_UserID from T_UserExInfo with(nolock) where 1=1 ";
                if (personidStr.Length > 0)
                {
                    whereStr += " and F_PersonID='" + gameuserStr + "'";
                }
                if (telphoneStr.Length > 0)
                {
                    whereStr += " and F_TelPhone='" + gameuserStr + "'";
                }
                whereStr += ")";
            }
            whereStr += " order by F_UserID asc";
            msgparam.p1 = whereStr;
            DGVGameUser.DataSource = null;
            DGVGameRole.DataSource = null;
            clihandle.GetGameUsersC(whereStr);
            buttonGameSearch.Text = "查 询 5";
            buttonGameSearch.Enabled = false;
            buttonGameRest.Enabled = false;
            Application.DoEvents();
            timerGameSearch.Start();
        }

        /// <summary>
        /// 客服主页,查询条件清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button36_Click(object sender, EventArgs e)
        {
            txtbCtelephone.ResetText();
            txtbCgameuser.ResetText();
            txtbCgamerole.ResetText();
            txtbCpersonid.ResetText();
        }

        /// <summary>
        /// 游戏用户列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVGameUser_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVGameUser.CurrentRow != null && DGVGameUser.Rows.Count > 1)
            {
                // DGVGameRole.Rows.Clear();
                if (DGVGameUser.SelectedRows.Count != 0 && DGVGameUser.SelectedRows[0].Cells[0].Value.ToString().Length > 0)
                {
                    string bigzonename = DGVGameUser.SelectedRows[0].Cells[2].Value.ToString();
                    string id = DGVGameUser.SelectedRows[0].Cells[0].Value.ToString();
                    clihandle.GetGameRolesC(bigzonename + "|" + id);
                }
            }
        }

        /// <summary>
        /// 游戏用户列表单击事件
        /// </summary>
        private void DGVGameUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!DGVGameUser.Rows[e.RowIndex].Selected)
                {
                    DGVGameUser.ClearSelection();
                    DGVGameUser.Rows[e.RowIndex].Selected = true;
                }
                if (DGVGameUser.SelectedRows.Count == 1)
                {
                    DGVGameUser.CurrentCell = DGVGameUser.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                contextMenuStripGUser.Show(MousePosition.X, MousePosition.Y);
            }
        }

        /// <summary>
        /// 游戏角色列表单击事件
        /// </summary>
        private void DGVGameRole_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!DGVGameRole.Rows[e.RowIndex].Selected)
                {
                    DGVGameRole.ClearSelection();
                    DGVGameRole.Rows[e.RowIndex].Selected = true;
                }
                if (DGVGameRole.SelectedRows.Count == 1)
                {
                    DGVGameRole.CurrentCell = DGVGameRole.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                contextMenuStripGRole.Show(MousePosition.X, MousePosition.Y);
            }
        }

        /// <summary>
        /// 角色列表格式化事件
        /// </summary>
        private void DGVGameRole_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }


        #endregion

        #region 工单相关

        /// <summary>
        /// 获取工单列表
        /// </summary>
        /// <param name="value"></param>
        private void GetTaskList(string sqlvalue)
        {
            foreach (ToolStripItem control in this.toolStripMain.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton" && (control as System.Windows.Forms.ToolStripButton).Checked)
                {
                    System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)control;
                    if (toolbtnActiv.Name == "toolStripButtonT0")
                    {
                        tabControl1.SelectedIndex = 0;
                    }
                    else if (toolbtnActiv.Name == "toolStripButtonT11")
                    {
                        tabControl1.SelectedIndex = 2;

                        string whereStr = " and F_Type=20000213";
                        whereStr += sqlvalue + "  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";

                        richtbTasklogGN.Clear();
                        richtbTasklogGN.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;
                        richtbTasklogGN.ForeColor = Color.DimGray;
                        ShareData.TaskListRequstWhere = whereStr;
                        try
                        {
                            clihandle.GetAllTasks("toolStripButtonT11", whereStr);
                        }
                        catch (System.Exception ex)
                        {
                            //日志记录
                            ShareData.Log.Error(ex);
                        }

                    }
                    else if (toolbtnActiv.Name == "toolStripButtonT12")
                    {
                        tabControl1.SelectedIndex = 3;

                        string whereStr = " and F_Type=20000214";
                        whereStr += sqlvalue + "  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";

                        richtbTasklogGA.Clear();
                        richtbTasklogGA.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;
                        richtbTasklogGA.ForeColor = Color.DimGray;
                        ShareData.TaskListRequstWhere = whereStr;
                        try
                        {
                            clihandle.GetAllTasks("toolStripButtonT12", whereStr);
                        }
                        catch (System.Exception ex)
                        {
                            //日志记录
                            ShareData.Log.Error(ex);
                        }

                    }
                    else
                    {
                        tabControl1.SelectedIndex = 1;
                        string whereStr = " and F_Type<>20000213  and F_Type<>20000214 ";
                        switch (toolbtnActiv.Name)
                        {
                            case "toolStripButtonT1":
                                whereStr += sqlvalue + " and F_State=" + SystemConfig.DefaultWorkOrderStatue + "  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT2":
                                whereStr += sqlvalue + " and F_State=100100101  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT3":
                                whereStr += sqlvalue + " and F_State=100100102  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT4":
                                whereStr += sqlvalue + " and F_State=100100103  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT5":
                                whereStr += sqlvalue + " and F_State=100100104  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT6":
                                whereStr += sqlvalue + " and F_State=100100105  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT7":
                                whereStr += sqlvalue + " and F_State=100100106  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT8":
                                whereStr += sqlvalue + " and F_State=100100107  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT9":
                                whereStr += sqlvalue + " and F_State=100100108  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                                break;
                            case "toolStripButtonT10":
                                whereStr += sqlvalue + GetMyTaskSqlStr();
                                break;
                            default:
                                break;
                        }
                        richtbTasklog.Clear();
                        richtbTasklog.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;
                        richtbTasklog.ForeColor = Color.DimGray;
                        ShareData.TaskListRequstWhere = whereStr;
                        try
                        {
                            clihandle.GetAllTasks(toolbtnActiv.Name, whereStr);
                        }
                        catch (System.Exception ex)
                        {
                            //日志记录
                            ShareData.Log.Error(ex);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 得到我的工单的SQL
        /// </summary>
        private string GetMyTaskSqlStr()
        {
            string sqlStr = "";
            if (ShareData.UserID.Trim().Length == 0)
            {
                return " and 1<>1";
            }
            foreach (ToolStripItem control in this.toolStripMyTask.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton" && (control as System.Windows.Forms.ToolStripButton).Checked)
                {
                    System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)control;
                    tabControl1.SelectedIndex = 1;
                    switch (toolbtnActiv.Name)
                    {
                        case "toolStripButtonTT0":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        case "toolStripButtonTT1":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100101  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        case "toolStripButtonTT2":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100102  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        case "toolStripButtonTT3":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100103  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        case "toolStripButtonTT4":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100106  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        case "toolStripButtonTT5":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100104  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        case "toolStripButtonTT6":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100105  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        case "toolStripButtonTT7":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100107  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        case "toolStripButtonTT8":
                            sqlStr = " and F_DutyMan=" + ShareData.UserID + " and F_State=100100108  order by F_VipLevel desc,F_LimitTime asc,F_ID ASC";
                            break;
                        default:
                            break;
                    }
                }
            }
            return sqlStr;
        }

        /// <summary>
        /// 工单查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button35_Click(object sender, EventArgs e)
        {
            string sqlwhere = "";
            if (WinUtil.IsNumber(txtbTtaskid.Text))
            {
                sqlwhere += " and F_ID=" + txtbTtaskid.Text.Trim();
            }
            if (txtbTguserid.Text.Trim().Length > 0)
            {
                sqlwhere += " and F_GUserName='" + txtbTguserid.Text.Trim() + "'";
            }
            if (txtbTgroleid.Text.Trim().Length > 0)
            {
                sqlwhere += " and F_GRoleName='" + txtbTgroleid.Text.Trim() + "'";
            }
            if (txtbTtelephone.Text.Trim().Length > 0)
            {
                sqlwhere += " and F_Telphone='" + txtbTtelephone.Text.Trim() + "'";
            }
            if (combTtasktype.SelectedIndex > 0)
            {
                sqlwhere += " and F_Type=" + combTtasktype.SelectedValue + "";
            }

            GetTaskList(sqlwhere);

        }

        /// <summary>
        /// 工单列表选中变更事件
        /// </summary>
        private void DGVTask_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVTask.CurrentRow != null && DGVTask.Rows.Count > 0 && DGVTask.SelectedRows.Count > 0)
            {
                richtbTasklog.Clear();
                if (DGVTask.SelectedRows.Count != 0 && DGVTask.SelectedRows[0].Cells[2].Value.ToString().Length > 0)
                {

                    //string id = DGVTask.SelectedRows[0].Cells[2].Value.ToString();
                    string id = DGVTask.CurrentRow.Cells[2].Value.ToString();
                    string sqlwhere = "and F_ID=" + id + " order by F_LogID asc";
                    clihandle.GetTaskLog(sqlwhere);

                }
            }
        }

        /// <summary>
        /// 工单列表格式化事件
        /// </summary>
        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (DGVTask.Columns[e.ColumnIndex].Name.Equals("ColumnVipPic") && DGVTask.Rows[e.RowIndex].Cells["ColumnVipLevel"].Value != null)
            {
                //VIP图标
                string vipStr = DGVTask.Rows[e.RowIndex].Cells["ColumnVipLevel"].Value.ToString();
                if (vipStr.Trim().Length > 0)
                {
                    vipStr = Application.StartupPath + "\\GSSData\\Images\\" + vipStr + ".png";

                    e.Value = GetImage(vipStr);
                }
                else
                {
                    e.Value = new System.Drawing.Bitmap(1, 1);
                }
            }
            else if (DGVTask.Columns[e.ColumnIndex].Name.Equals("ColumnLimitT") && DGVTask.Rows[e.RowIndex].Cells["ColumnLimitTime"].Value != null)
            {
                //工单限时
                string timStr = DGVTask.Rows[e.RowIndex].Cells["ColumnLimitTime"].Value.ToString();
                if (!WinUtil.IsDateTime(timStr))
                {
                    return;
                }
                else if (Convert.ToDateTime(timStr).ToShortDateString() == Convert.ToDateTime("1900-1-1").ToShortDateString())
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                else if (DGVTask.Rows[e.RowIndex].Cells["ColumnF_State"].Value.ToString() == LanguageResource.Language.Tip_WorkOrderCompleted || DGVTask.Rows[e.RowIndex].Cells["ColumnF_State"].Value.ToString() == LanguageResource.Language.BtnWorkOrderClose)
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                DateTime limtime = Convert.ToDateTime(timStr);
                TimeSpan ts = limtime.Subtract(DateTime.Now);
                string limitStr = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
                e.Value = limitStr;

                double hours = ts.TotalHours;

                if (hours < 0)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Red;
                    e.CellStyle.ApplyStyle(cellstyle);
                }
                else if (hours < 4)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Orange;
                    e.CellStyle.ApplyStyle(cellstyle);
                }

            }


        }

        /// <summary>
        /// 工单列表:重置查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button37_Click(object sender, EventArgs e)
        {
            txtbTtaskid.ResetText();
            txtbTguserid.ResetText();
            txtbTgroleid.ResetText();
            txtbTtelephone.ResetText();
            combTtasktype.SelectedIndex = 0;
            combTtasktypeP.SelectedIndex = 0;
        }


        /// <summary>
        /// 工单列表控件单元格双击:弹出工单处理窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTask_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVTask.SelectedRows.Count == 0 || e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow drtask = DGVTask.SelectedRows[0];
            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            FormTaskEdit formtaskedit = new FormTaskEdit(clihandle, taskid);
            ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
            ShareData.DGVSelectIndex = drtask.Index;
            DialogResult dresult = formtaskedit.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                DGVTask.Rows.RemoveAt(DGVTask.SelectedRows[0].Index);
            }

        }

        /// <summary>
        /// 工单列表单击事件
        /// </summary>
        private void DGVTask_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!DGVTask.Rows[e.RowIndex].Selected)
                {
                    DGVTask.ClearSelection();
                    DGVTask.Rows[e.RowIndex].Selected = true;
                }
                if (DGVTask.SelectedRows.Count == 1)
                {
                    DGVTask.CurrentCell = DGVTask.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                contextMenuStripTask.Show(MousePosition.X, MousePosition.Y);
            }
        }

        /// <summary>
        /// 选中父工单类型
        /// </summary>
        private void combTtasktypeP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combTtasktypeP.SelectedIndex > 0)
            {
                BindDicComb(combTtasktype, combTtasktypeP.SelectedValue.ToString());
            }
            else
            {
                combTtasktype.SelectedValue = 0;
            }

        }

        #endregion

        #region 菜单相关

        /// <summary>
        /// 菜单选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonT0_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem control in this.toolStripMain.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
                {
                    (control as System.Windows.Forms.ToolStripButton).Checked = false;
                }
            }
            ToolStripMenuItemGNStart.Visible = false;
            ToolStripMenuItemGNStop.Visible = false;
            ToolStripMenuItemTReceive.Visible = false;
            ToolStripMenuItemTaskDeal.Visible = true;

            if ((sender as ToolStripItem).Name == "toolStripButtonT10")
            {
                groupBoxMyTask.Visible = true;
                groupBoxTsearch.Location = new Point(6, 59);
                groupBoxTtasklist.Location = new Point(6, 119);
                groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 119);
                groupBoxTtasklist.Height = tabPage2.Size.Height - 125;
                groupBoxTtasklog.Height = tabPage2.Size.Height - 125;
            }
            else if ((sender as ToolStripItem).Name == "toolStripButtonT11")
            {

                ToolStripMenuItemGNStart.Visible = true;
                ToolStripMenuItemGNStop.Visible = true;

            }
            else
            {
                groupBoxMyTask.Visible = false;
                groupBoxTsearch.Location = new Point(6, 6);
                groupBoxTtasklist.Location = new Point(6, 71);
                groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 71);
                groupBoxTtasklist.Height = tabPage2.Size.Height - 77;
                groupBoxTtasklog.Height = tabPage2.Size.Height - 77;

                if ((sender as ToolStripItem).Name == "toolStripButtonT1")
                {
                    ToolStripMenuItemTReceive.Visible = true;
                }
            }


            System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)sender;
            toolbtnActiv.Checked = true;
            GetTaskList("");
        }


        /// <summary>
        /// 我的工单菜单选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTT0_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem control in this.toolStripMyTask.Items)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
                {
                    (control as System.Windows.Forms.ToolStripButton).Checked = false;
                }

            }

            System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)sender;
            toolbtnActiv.Checked = true;
            GetTaskList("");
        }

        /// <summary>
        /// 绘图:主菜单画新工单提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonT0_Paint(object sender, PaintEventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
            {
                System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)sender;
                if (toolbtnActiv.Name != "toolStripButtonT0")
                {
                    DrawMainTaskAlert(e.Graphics, toolbtnActiv);
                }

            }
        }

        /// <summary>
        /// 绘图:主菜单工单提醒图标
        /// </summary>
        private void DrawMainTaskAlert(Graphics g, ToolStripButton toolbtnActiv)
        {
            string drawStr = "0";
            float x = toolbtnActiv.Width - 34;
            float y = 5.0F;
            string filterStr = "";
            switch (toolbtnActiv.Name)
            {
                case "toolStripButtonT10":
                    filterStr = "F_DutyMan=" + ShareData.UserID + "";
                    break;
                case "toolStripButtonT1":
                    filterStr = "F_State=" + SystemConfig.DefaultWorkOrderStatue + " and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT2":
                    filterStr = "F_State=100100101 and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT3":
                    filterStr = "F_State=100100102 and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT4":
                    filterStr = "F_State=100100103 and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT5":
                    filterStr = "F_State=100100104 and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT6":
                    filterStr = "F_State=100100105 and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT7":
                    filterStr = "F_State=100100106 and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT8":
                    filterStr = "F_State=100100107 and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT9":
                    filterStr = "F_State=100100108 and F_Type<>20000213 and F_Type<>20000214 ";
                    break;
                case "toolStripButtonT11":
                    filterStr = "F_Type=20000213";
                    break;
                case "toolStripButtonT12":
                    filterStr = "F_Type=20000214";
                    break;
                default:
                    filterStr = "1<>1";
                    break;
            }
            DataSet ds = ClientCache.GetTaskCache("TaskAlertNum");
            if (ds != null)
            {
                DataRow[] drs = ds.Tables[0].Select(filterStr);
                drawStr = drs.Length.ToString();
            }

            DrawAlertImgNumM(g, drawStr, x, y);

        }

        /// <summary>
        /// 绘图:底图+字 (大图)
        /// </summary>
        private void DrawAlertImgNumM(Graphics g, string drawStr, float x, float y)
        {
            if (drawStr == "0")
            {
                return;
            }
            //定义变量
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality; //高像素偏移质量
            Font drawFont = new Font("DotumChe", 10F, System.Drawing.FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            if (DateTime.Now.Millisecond % 5 == 0)
            {
                drawBrush = new SolidBrush(System.Drawing.SystemColors.ScrollBar);
            }
            Image backimg = GetImage(Application.StartupPath + "\\GSSData\\Images\\numerbg.png");
            if (drawStr.Length >= 3)
            {
                g.DrawImage(backimg, x, y, 34, 15.8F);
            }
            else
            {
                g.DrawImage(backimg, x, y, 30, 15.8F);
            }


            float sx = x + 10;
            float sy = y + 1.9F;

            if (drawStr.Length == 2)
            {
                sx -= 3.3F;
            }
            else if (drawStr.Length == 3)
            {
                sx -= 5.0F;
            }
            else if (drawStr.Length > 3)
            {
                sx -= 5.0F;
                drawStr = "+++";
            }

            PointF drawPoint = new PointF(sx, sy);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.NoClip;

            g.DrawString(drawStr, drawFont, drawBrush, drawPoint, drawFormat);
        }


        /// <summary>
        /// 绘图:我的菜单画新工单提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTT0_Paint(object sender, PaintEventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
            {
                System.Windows.Forms.ToolStripButton toolbtnActiv = (System.Windows.Forms.ToolStripButton)sender;
                DrawMyTaskAlert(e.Graphics, toolbtnActiv);
            }
        }


        /// <summary>
        /// 绘图:我的工单提醒图标
        /// </summary>
        private void DrawMyTaskAlert(Graphics g, ToolStripButton toolbtnActiv)
        {
            string drawStr = "0";
            float x = toolbtnActiv.Width - 27;
            float y = 0;
            string filterStr = "F_Type<>20000213 and F_Type<>20000214 and";
            switch (toolbtnActiv.Name)
            {
                case "toolStripButtonTT0":
                    filterStr = "F_DutyMan=" + ShareData.UserID + "";
                    break;
                case "toolStripButtonTT1":
                    filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100101";
                    break;
                case "toolStripButtonTT2":
                    filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100102";
                    break;
                case "toolStripButtonTT3":
                    filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100103";
                    break;
                case "toolStripButtonTT4":
                    filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100106";
                    break;
                case "toolStripButtonTT5":
                    filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100104";
                    break;
                case "toolStripButtonTT6":
                    filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100105";
                    break;
                case "toolStripButtonTT7":
                    filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100107";
                    break;
                case "toolStripButtonTT8":
                    filterStr = "F_DutyMan=" + ShareData.UserID + " and F_State=100100108";
                    break;
                default:
                    filterStr = "1<>1";
                    break;
            }
            DataSet ds = ClientCache.GetTaskCache("TaskAlertNum");
            if (ds != null)
            {
                DataRow[] drs = ds.Tables[0].Select(filterStr);
                drawStr = drs.Length.ToString();
            }

            DrawAlertImgNum(g, drawStr, x, y);

        }

        /// <summary>
        /// 绘图:底图+字
        /// </summary>
        private void DrawAlertImgNum(Graphics g, string drawStr, float x, float y)
        {
            if (drawStr == "0")
            {
                return;
            }
            //定义变量
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality; //高像素偏移质量
            Font drawFont = new Font("DotumChe", 8, System.Drawing.FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            //if (DateTime.Now.Second % 2 == 0)
            //{
            //    //drawBrush = new SolidBrush(System.Drawing.SystemColors.ScrollBar);
            //    drawFont = new Font("DotumChe", 8F, System.Drawing.FontStyle.Bold);
            //}
            Image backimg = GetImage(Application.StartupPath + "\\GSSData\\Images\\numerbg.png");
            g.DrawImage(backimg, x, y, 28, 14.3F);

            float sx = x + 10;
            float sy = y + 1.9F;

            if (drawStr.Length == 2)
            {
                sx -= 2;
            }
            else if (drawStr.Length == 3)
            {
                sx -= 5;
            }
            else if (drawStr.Length > 3)
            {
                sx -= 5;
                drawStr = "+++";
            }

            PointF drawPoint = new PointF(sx, sy);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.NoClip;

            g.DrawString(drawStr, drawFont, drawBrush, drawPoint, drawFormat);
        }


        #endregion

        #region 右键快捷菜单事件

        private void 查询帐号相关工单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            string whereStr = " and F_GUserID='" + DGVGameUser.SelectedRows[0].Cells[0].Value + "' order by  F_ID DESC";
            richtbTasklog.ResetText();
            richtbTasklog.ForeColor = Color.DimGray;
            richtbTasklog.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;

            groupBoxMyTask.Visible = false;
            groupBoxTsearch.Location = new Point(6, 6);
            groupBoxTtasklist.Location = new Point(6, 71);
            groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 71);
            groupBoxTtasklist.Height = tabPage2.Size.Height - 77;
            groupBoxTtasklog.Height = tabPage2.Size.Height - 77;
            ToolStripMenuItemTReceive.Visible = false;

            clihandle.GetAllTasks("", whereStr);
        }

        private void 查询角色相关工单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            string whereStr = " and F_GRoleID='" + DGVGameRole.SelectedRows[0].Cells[0].Value + "' order by  F_ID DESC";
            richtbTasklog.ResetText();
            richtbTasklog.Text = Text = LanguageResource.Language.Tip_LeftLiDisplayHistoryWorkOrder;
            richtbTasklog.ForeColor = Color.DimGray;

            groupBoxMyTask.Visible = false;
            groupBoxTsearch.Location = new Point(6, 6);
            groupBoxTtasklist.Location = new Point(6, 71);
            groupBoxTtasklog.Location = new Point(groupBoxTtasklog.Location.X, 71);
            groupBoxTtasklist.Height = tabPage2.Size.Height - 77;
            groupBoxTtasklog.Height = tabPage2.Size.Height - 77;
            ToolStripMenuItemTReceive.Visible = false;

            clihandle.GetAllTasks("", whereStr);
        }

        private void 处理工单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVTask.Visible)
            {
                if (DGVTask.SelectedRows.Count == 0)
                {
                    return;
                }
                DataGridViewRow drtask = DGVTask.SelectedRows[0];
                int taskid = Convert.ToInt32(drtask.Cells[2].Value);
                FormTaskEdit formtaskedit = new FormTaskEdit(clihandle, taskid);
                ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
                ShareData.DGVSelectIndex = drtask.Index;
                DialogResult dresult = formtaskedit.ShowDialog();
                if (dresult == DialogResult.OK)
                {
                    DGVTask.Rows.RemoveAt(DGVTask.SelectedRows[0].Index);
                }
            }
            else if (DGVTaskGN.Visible)
            {
                if (DGVTaskGN.SelectedRows.Count == 0)
                {
                    return;
                }
                DataGridViewRow drtask = DGVTaskGN.SelectedRows[0];
                int taskid = Convert.ToInt32(drtask.Cells[2].Value);
                FormTaskEditGN formtaskedit = new FormTaskEditGN(clihandle, taskid);
                ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
                ShareData.DGVSelectIndex = drtask.Index;
                DialogResult dresult = formtaskedit.ShowDialog();
                if (dresult == DialogResult.OK)
                {
                    DGVTaskGN.Rows.RemoveAt(DGVTaskGN.SelectedRows[0].Index);
                }
            }

        }
        /// <summary>
        /// 接收工单
        /// </summary>
        private void ToolStripMenuItemTReceive_Click(object sender, EventArgs e)
        {
            DialogResult IsOK = MsgBox.Show(LanguageResource.Language.Tip_AcceptSelectWorkOrder, LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (IsOK != DialogResult.Yes)
            {
                return;
            }
           GSSBLL.Tasks bll = ClientRemoting.Tasks();

            foreach (DataGridViewRow drtask in DGVTask.SelectedRows)
            {
                int taskid = Convert.ToInt32(drtask.Cells[2].Value);
                GSSModel.Tasks model = new GSSModel.Tasks();
                model.F_ID = taskid;
                model.F_State = 100100101;
                model.F_DutyMan = int.Parse(ShareData.UserID);
                model.F_EditMan = int.Parse(ShareData.UserID);
                model.F_EditTime = DateTime.Now;


                //string back = clihandle.EditTaskSyn(model);
                int back = bll.Edit(model);
                if (back != 0)
                {
                    DGVTask.Rows.RemoveAt(DGVTask.SelectedRows[0].Index);
                }
            }
            System.Threading.Thread.Sleep(1000);
            clihandle.GetAlertNum();
            //ClientCache.SetTaskCache(DataSerialize.GetDataSetSurrogateZipBYtes(bll.GetAlertNum()) , "TaskAlertNum");
        }

        private void 停止公告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult IsOK = MsgBox.Show(LanguageResource.Language.Tip_SureStopSelectNotcie, LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (IsOK != DialogResult.Yes)
            {
                return;
            }

            DataGridViewRow drtask = DGVTaskGN.SelectedRows[0];
            ShareData.DGVSelectIndex = drtask.Index;

            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_ID = taskid;
            model.F_EditMan = int.Parse(ShareData.UserID);
            model.F_EditTime = DateTime.Now;
            model.F_TToolUsed = true;


            string backStr = clihandle.GameNoticeStopSyn(taskid.ToString());


            if (backStr == "true")
            {
                MsgBox.Show(LanguageResource.Language.LblSuccStopPlacard, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                model.F_COther = LanguageResource.Language.LblSuccStopPlacard;
                model.F_OCanRestor = false;

            }
            else
            {
                MsgBox.Show(LanguageResource.Language.LblFailureStopPlacard+" ！" + backStr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                model.F_COther = LanguageResource.Language.LblFailureStopPlacard + " ！" + backStr;
            }

            backStr = clihandle.EditTaskSyn(model);
            GetTaskList("");
        }

        private void 运行公告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult IsOK = MsgBox.Show(LanguageResource.Language.Tip_SureRunSelectNotcie, LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (IsOK != DialogResult.Yes)
            {
                return;
            }
            DataGridViewRow drtask = DGVTaskGN.SelectedRows[0];
            ShareData.DGVSelectIndex = drtask.Index;

            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            GSSModel.Tasks model = new GSSModel.Tasks();
            model.F_ID = taskid;
            model.F_EditMan = int.Parse(ShareData.UserID);
            model.F_EditTime = DateTime.Now;
            model.F_TToolUsed = true;


            string backStr = clihandle.GameNoticeStartSyn(taskid.ToString());


            if (backStr == "true")
            {
                MsgBox.Show(LanguageResource.Language.Tip_SuccRunPlacard, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                model.F_COther = LanguageResource.Language.Tip_SuccRunPlacard;
                model.F_OCanRestor = true;

            }
            else
            {
                MsgBox.Show( LanguageResource.Language.Tip_RunNoticeFailure + backStr, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                model.F_COther =  LanguageResource.Language.Tip_RunNoticeFailure + backStr;
            }

            backStr = clihandle.EditTaskSyn(model);
            GetTaskList("");
        }

        #endregion

        /// <summary>
        /// 定时操作器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timerGameSearch_Tick(object sender, EventArgs e)
        {

        }

        private void aButton1_Click(object sender, EventArgs e)
        {
            FormTaskAddGameNotice form = new FormTaskAddGameNotice(clihandle, 20000213);
            form.Show();
        }

        private void aButton2_Click(object sender, EventArgs e)
        {
            FormTaskAddGameNotice1 form = new FormTaskAddGameNotice1(clihandle, 20000213);
            form.Show();
        }

        private void aButton3_Click(object sender, EventArgs e)
        {
            FormTaskAddGameNotice2 form = new FormTaskAddGameNotice2(clihandle, 20000213);
            form.ShowDialog();
        }

        /// <summary>
        /// 工单变更事件 喊话
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTaskGN_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVTaskGN.CurrentRow != null && DGVTaskGN.Rows.Count > 0 && DGVTaskGN.SelectedRows.Count > 0)
            {
                richtbTasklogGN.Clear();
                if (DGVTaskGN.SelectedRows.Count != 0 && DGVTaskGN.SelectedRows[0].Cells[2].Value.ToString().Length > 0)
                {

                    string id = DGVTaskGN.SelectedRows[0].Cells[2].Value.ToString();
                    //string id = DGVTaskGN.CurrentRow.Cells[2].Value.ToString();
                    string sqlwhere = "and F_ID=" + id + " order by F_LogID asc";
                    clihandle.GetTaskLog(sqlwhere);

                }
            }
        }

        /// <summary>
        /// 工单列表控件单元格双击:弹出工单处理窗口 喊话
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTaskGN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVTaskGN.SelectedRows.Count == 0 || e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow drtask = DGVTaskGN.SelectedRows[0];
            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            FormTaskEditGN formtaskedit = new FormTaskEditGN(clihandle, taskid);
            ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
            ShareData.DGVSelectIndex = drtask.Index;
            DialogResult dresult = formtaskedit.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                DGVTaskGN.Rows.RemoveAt(DGVTaskGN.SelectedRows[0].Index);
            }

        }

        /// <summary>
        /// 工单列表单击事件 喊话
        /// </summary>
        private void DGVTaskGN_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        /// <summary>
        /// 工单列表格式化事件
        /// </summary>
        private void DGVTaskGNGN_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (DGVTaskGN.Columns[e.ColumnIndex].Name.Equals("ColumnVipPicGN") && DGVTaskGN.Rows[e.RowIndex].Cells["ColumnVipLevelGN"].Value != null)
            {
                //VIP图标
                string vipStr = DGVTaskGN.Rows[e.RowIndex].Cells["ColumnVipLevelGN"].Value.ToString();
                if (vipStr.Trim().Length > 0)
                {
                    vipStr = Application.StartupPath + "\\GSSData\\Images\\" + vipStr + ".png";

                    e.Value = GetImage(vipStr);
                }
                else
                {
                    e.Value = new System.Drawing.Bitmap(1, 1);
                }
            }
            else if (DGVTaskGN.Columns[e.ColumnIndex].Name.Equals("ColumnLimitTGN") && DGVTaskGN.Rows[e.RowIndex].Cells["ColumnLimitTimeGN"].Value != null)
            {
                //工单限时
                string timStr = DGVTaskGN.Rows[e.RowIndex].Cells["ColumnLimitTimeGN"].Value.ToString();
                if (!WinUtil.IsDateTime(timStr))
                {
                    return;
                }
                else if (Convert.ToDateTime(timStr).ToShortDateString() == Convert.ToDateTime("1900-1-1").ToShortDateString())
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                else if (DGVTaskGN.Rows[e.RowIndex].Cells["ColumnF_StateGN"].Value.ToString() == LanguageResource.Language.Tip_WorkOrderCompleted || DGVTaskGN.Rows[e.RowIndex].Cells["ColumnF_StateGN"].Value.ToString() == LanguageResource.Language.BtnWorkOrderClose)
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                DateTime limtime = Convert.ToDateTime(timStr);
                TimeSpan ts = limtime.Subtract(DateTime.Now);
                string limitStr = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
                e.Value = limitStr;

                double hours = ts.TotalHours;

                if (hours < 0)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Red;
                    e.CellStyle.ApplyStyle(cellstyle);
                }
                else if (hours < 4)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Orange;
                    e.CellStyle.ApplyStyle(cellstyle);
                }

            }
            else if (DGVTaskGN.Columns[e.ColumnIndex].Name.Equals("ColumnGNF_OCanRestor") && DGVTaskGN.Rows[e.RowIndex].Cells["ColumnGNF_OCanRestor"].Value != null)
            {
                //公告状态
                string state = DGVTaskGN.Rows[e.RowIndex].Cells["ColumnGNF_OCanRestor"].Value.ToString();
                if (state == "True")
                {
                    e.Value = LanguageResource.Language.LblRunning;
                    return;
                }
                else
                {
                    e.Value = LanguageResource.Language.LblEndRunning;
                    return;
                }

            }


        }

        /// <summary>
        /// 工单变更事件 发奖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTaskGA_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVTaskGA.CurrentRow != null && DGVTaskGA.Rows.Count > 0 && DGVTaskGA.SelectedRows.Count > 0)
            {
                richtbTasklogGA.Clear();
                if (DGVTaskGA.SelectedRows.Count != 0 && DGVTaskGA.SelectedRows[0].Cells[2].Value.ToString().Length > 0)
                {

                    string id = DGVTaskGA.SelectedRows[0].Cells[2].Value.ToString();
                    //string id = DGVTaskGA.CurrentRow.Cells[2].Value.ToString();
                    string sqlwhere = "and F_ID=" + id + " order by F_LogID asc";
                    clihandle.GetTaskLog(sqlwhere);

                }
            }
        }

        /// <summary>
        /// 工单列表控件单元格双击:弹出工单处理窗口 发奖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVTaskGA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVTaskGA.SelectedRows.Count == 0 || e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow drtask = DGVTaskGA.SelectedRows[0];
            int taskid = Convert.ToInt32(drtask.Cells[2].Value);
            FormTaskEditGA formtaskedit = new FormTaskEditGA(clihandle, taskid);
            ShareData.FormhidEdit = formtaskedit.Handle.ToInt32();
            ShareData.DGVSelectIndex = drtask.Index;
            DialogResult dresult = formtaskedit.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                DGVTaskGA.Rows.RemoveAt(DGVTaskGA.SelectedRows[0].Index);
            }

        }

        /// <summary>
        /// 工单列表单击事件 发奖
        /// </summary>
        private void DGVTaskGA_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        /// <summary>
        /// 工单列表格式化事件 发奖
        /// </summary>
        private void DGVTaskGA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (DGVTaskGA.Columns[e.ColumnIndex].Name.Equals("ColumnVipPicGA") && DGVTaskGA.Rows[e.RowIndex].Cells["ColumnVipLevelGA"].Value != null)
            {
                //VIP图标
                string vipStr = DGVTaskGA.Rows[e.RowIndex].Cells["ColumnVipLevelGA"].Value.ToString();
                if (vipStr.Trim().Length > 0)
                {
                    vipStr = Application.StartupPath + "\\GSSData\\Images\\" + vipStr + ".png";

                    e.Value = GetImage(vipStr);
                }
                else
                {
                    e.Value = new System.Drawing.Bitmap(1, 1);
                }
            }
            else if (DGVTaskGA.Columns[e.ColumnIndex].Name.Equals("ColumnLimitTGA") && DGVTaskGA.Rows[e.RowIndex].Cells["ColumnLimitTimeGA"].Value != null)
            {
                //工单限时
                string timStr = DGVTaskGA.Rows[e.RowIndex].Cells["ColumnLimitTimeGA"].Value.ToString();
                if (!WinUtil.IsDateTime(timStr))
                {
                    return;
                }
                else if (Convert.ToDateTime(timStr).ToShortDateString() == Convert.ToDateTime("1900-1-1").ToShortDateString())
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                else if (DGVTaskGA.Rows[e.RowIndex].Cells["ColumnF_StateGA"].Value.ToString() == LanguageResource.Language.Tip_WorkOrderCompleted || DGVTaskGA.Rows[e.RowIndex].Cells["ColumnF_StateGA"].Value.ToString() == LanguageResource.Language.BtnWorkOrderClose)
                {
                    e.Value = "00:00:00:00";
                    return;
                }
                DateTime limtime = Convert.ToDateTime(timStr);
                TimeSpan ts = limtime.Subtract(DateTime.Now);
                string limitStr = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().ToString().Replace("-", "").PadLeft(2, '0'), ts.Hours.ToString().Replace("-", "").PadLeft(2, '0'), ts.Minutes.ToString().Replace("-", "").PadLeft(2, '0'), ts.Seconds.ToString().Replace("-", "").PadLeft(2, '0'));
                e.Value = limitStr;

                double hours = ts.TotalHours;

                if (hours < 0)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Red;
                    e.CellStyle.ApplyStyle(cellstyle);
                }
                else if (hours < 4)
                {
                    DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
                    cellstyle.BackColor = Color.Orange;
                    e.CellStyle.ApplyStyle(cellstyle);
                }

            }
            else if (DGVTaskGA.Columns[e.ColumnIndex].Name.Equals("ColumnGAF_OCanRestor") && DGVTaskGA.Rows[e.RowIndex].Cells["ColumnGAF_OCanRestor"].Value != null)
            {
                //公告状态
                string state = DGVTaskGA.Rows[e.RowIndex].Cells["ColumnGAF_OCanRestor"].Value.ToString();
                if (state == "True")
                {
                    e.Value = LanguageResource.Language.LblRunning;
                    return;
                }
                else
                {
                    e.Value = LanguageResource.Language.LblEndRunning;
                    return;
                }

            }


        }

        /// <summary>
        /// 得到单节值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetNodeValue(string a, string b, string c)
        {

            DataSet ds = ClientCache.GetCacheDS();
            DataRow[] dra = null;
            DataRow[] drb = null;
            DataRow[] drc = null;
            if (ds == null)
            {
                return LanguageResource.Language.Tip_GetError;
            }
            string backstr = "";
            if (a == "-1")
            {
                backstr += LanguageResource.Language.LblAllBigZone;
            }
            else
            {
                dra = ds.Tables["T_GameConfig"].Select("F_ValueGame='" + a + "'");
                if (dra.Length > 0)
                {
                    backstr += dra[0]["F_Name"].ToString();
                }
                else
                {
                    backstr += LanguageResource.Language.LblUnknowBigZone;
                }
            }
            if (b == "-1")
            {
                backstr += "/" + LanguageResource.Language.LblAllZone;
            }
            else
            {
                drb = ds.Tables["T_GameConfig"].Select("F_ValueGame='" + b + "' and F_ParentID=" + dra[0]["F_ID"].ToString() + "");
                if (drb.Length > 0)
                {
                    backstr += "/" + drb[0]["F_Name"].ToString();
                }
                else
                {
                    backstr += LanguageResource.Language.LblUnknowZone;
                }
            }
            if (c == "-1")
            {
                backstr += "/" + LanguageResource.Language.LblAllZoneLine;
            }
            else
            {
                drc = ds.Tables["T_GameConfig"].Select("F_ValueGame='" + c + "' and F_ParentID=" + drb[0]["F_ID"].ToString() + "");
                if (drc.Length > 0)
                {
                    backstr += "/" + drc[0]["F_Name"].ToString();
                }
                else
                {
                    backstr += LanguageResource.Language.LblUnknowZoneLine;
                }
            }

            return backstr;
        }

        private void aButton1_Click_1(object sender, EventArgs e)
        {
            FormTaskAddGameGiftAward form = new FormTaskAddGameGiftAward(clihandle, 20000214);
            form.ShowDialog();
        }




    }
}
