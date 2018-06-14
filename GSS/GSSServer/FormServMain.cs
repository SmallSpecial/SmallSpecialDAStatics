using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Net;
using GSS.DBUtility;
using GSSCSFrameWork;
using System.Collections;

namespace GSSServer
{
    public partial class FormServMain : Form
    {
        #region 定义字段
        //服务器端通讯实例
        private TcpSvr svr = null;
        //保存连接对象
        private Hashtable m_ChannelSock = new Hashtable();
        //请求的处理
        ServerHandle svrhandle;
        #endregion


        #region 构造函数,初始化
        public FormServMain()
        {
            InitializeComponent();
            ShareData.Log = log4net.LogManager.GetLogger("GSSLog");
            //日志记录
            ShareData.Log.Info("启动系统");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            formInit();
        }
        #endregion

        #region 窗口按钮事件
        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            //日志记录
            ShareData.Log.Info("退出系统");
            Application.Exit();
        }
        /// <summary>
        /// 服务器配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            FormServConfig form2 = new FormServConfig();
            form2.ShowDialog();
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ServStop();
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false) 
            {
                timer1.Enabled = true;
                formInit();
            }
           
            ServStart();
        }
        /// <summary>
        /// 设置开机自启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SetAutoRun();
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                string balltipstr = notifyIcon1.Text;
                notifyIcon1.ShowBalloonTip(5, "GSS", balltipstr, ToolTipIcon.Info);
                e.Cancel = true;
            }

        }
        /// <summary>
        /// 窗体最小化,尺寸变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                string balltipstr = notifyIcon1.Text;
                notifyIcon1.ShowBalloonTip(5, "GSS", balltipstr, ToolTipIcon.Info);
            }
        }
        /// <summary>
        /// NOTIFY双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }
        #endregion

        #region Notify菜单事件

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //日志记录
            ShareData.Log.Info("退出系统");
            Application.Exit();
        }

        private void 关于GSSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxGSS aboutbox = new AboutBoxGSS();
            aboutbox.ShowDialog();
        }

        private void 开启服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServStart();
        }

        private void 停止服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServStop();
        }

        #endregion

        #region 通讯事件
        /// <summary>
        /// 客户端建立连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientConn(object sender, NetEventArgs e)
        {
            string Sstate = string.Format("{0}/{1}", svr.SessionCount, svr.Capacity);
            SetLblStateValue(Sstate);

        }
        /// <summary>
        /// 客户端已满
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ServerFull(object sender, NetEventArgs e)
        {
            //Must do it
            e.Client.Close();
            //日志记录
            ShareData.Log.Warn("服务端达到设定的最大连接数:"+svr.Capacity);
            pictureBox1.Image = global::GSSServer.Properties.Resources.GSSserver_over;
            notifyIcon1.Icon = ((System.Drawing.Icon)(global::GSSServer.Properties.Resources.GSSserver_Nover));
        }
        /// <summary>
        /// 关闭客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientClose(object sender, NetEventArgs e)
        {

            if (e.Client.Channel != "")
            {
                try
                {
                    this.m_ChannelSock.Remove(e.Client.Channel);
                }
                catch(Exception ex) 
                {
                    ex.ToString().ErrorLogger();
                }
            }

            string Sstate = string.Format("{0}/{1}", svr.SessionCount, svr.Capacity);
            SetLblStateValue(Sstate);

            pictureBox1.Image = global::GSSServer.Properties.Resources.GSSserver_start;
            notifyIcon1.Icon = ((System.Drawing.Icon)(global::GSSServer.Properties.Resources.GSSserver_Nstart));
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RecvData(object sender, NetEventArgs e)
        {
            //处理接收到的数据
            try
            {
                svrhandle.DoRequest(e.Client, e.Client.MsgStrut);
            }
            catch (System.Exception ex)
            {
                ex.Message.ErrorLogger();
                //日志记录
                ShareData.Log.Error("GSS处理请求ERRO", ex);
            }

        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 方法:服务启动
        /// </summary>
        private void ServStart()
        {
            toolStripStatusLabel2.Text = "服务启动中......";
            buttonStart.Enabled = false;
            button2.Enabled = true;
            button2.Focus();
            Application.DoEvents();
            Thread.Sleep(800);
            //通讯,开始监听
            try
            {
                svr.Start();
               ServerRemoting.Start();
            }
            catch (System.Exception ex)
            {
                ex.Message.ErrorLogger();
                buttonStart.Enabled = true;
                button2.Enabled = false;
                MessageBox.Show("信息:" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //日志记录
                ShareData.Log.Warn("开启GSS服务错误",ex);
                return;
            }

            string Sstate = string.Format("{0}/{1}", svr.SessionCount, svr.Capacity);
            lblServState.Text = Sstate;

            pictureBox1.Image = global::GSSServer.Properties.Resources.GSSserver_start;
            notifyIcon1.Icon = ((System.Drawing.Icon)(global::GSSServer.Properties.Resources.GSSserver_Nstart));
            notifyIcon1.ShowBalloonTip(5, "GSS", "服务已经启动", ToolTipIcon.Info);
            notifyIcon1.Text = "服务已经启动";
            toolStripStatusLabel2.Text = "服务已经启动";
            AppConfig app = new AppConfig();
            app.SetTipLanguage();
            //日志记录
            ShareData.Log.Info("开启GSS服务");
        }
        /// <summary>
        /// 方法:服务停止
        /// </summary>
        private void ServStop()
        {
            toolStripStatusLabel2.Text = "服务停止中......";
            buttonStart.Enabled = true;
            button2.Enabled = false;
            buttonStart.Focus();
            Application.DoEvents();
            Thread.Sleep(600);
            //通讯停止监听,关闭连接
            svr.Stop();
            ServerRemoting.Stop();
            string Sstate = string.Format("{0}/{1}", svr.SessionCount, svr.Capacity);
            lblServState.Text = Sstate;

            pictureBox1.Image = global::GSSServer.Properties.Resources.GSSserver_stop;
            notifyIcon1.Icon = ((System.Drawing.Icon)(global::GSSServer.Properties.Resources.GSSserver_Nstop));
            notifyIcon1.ShowBalloonTip(5, "GSS", "服务已经停止", ToolTipIcon.Info);
            notifyIcon1.Text = "服务已经停止";
            toolStripStatusLabel2.Text = "服务已经停止";

            //日志记录
            ShareData.Log.Info("停止GSS服务");
        }
        /// <summary>
        /// 设定开机自启动
        /// </summary>
        private void SetAutoRun()
        {
            try
            {
                RegistryKey ms_run = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (this.checkBox1.Checked)
                {
                    string runstr = ms_run.GetValue("ShenLongYouSoft").ToString();
                    ms_run.SetValue("ShenLongYouSoft", Application.ExecutablePath.ToString());
                    if (runstr.Trim().Length == 0)
                    {
                        MessageBox.Show("开机自启动-设置成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    ms_run.SetValue("ShenLongYouSoft", "");
                    MessageBox.Show("开机自启动-禁用成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Warn(ex);
                MessageBox.Show("设置失败！请确认本程序能够访问注册表", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 窗体初始化,包括配置数据文件
        /// </summary>
        private void formInit()
        {
            try
            {
                string gssipStr = "";
                string gssportStr = "";
                //配置SQLITE文件
                PubConstant.SqliteConnStr = Application.StartupPath;
                string sqlstr = "SELECT * FROM GSSCONFIG WHERE ID=1";
                DataSet ds = DbHelperSQLite.Query(sqlstr);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    gssipStr = ds.Tables[0].Rows[0]["GSSIP"].ToString().Trim();
                    gssportStr = ds.Tables[0].Rows[0]["GSSPORT"].ToString().Trim();
                }
                string.Format("ip:[{0}],port:[{1}]", gssipStr, gssportStr).Logger();
                if (gssipStr.Length > 0 && gssportStr.Length > 0)
                {
                    tGSSip.Text = gssipStr;
                    tGSSport.Text = gssportStr;
                    ShareData.LocalIp = gssipStr;
                    ShareData.LocalPort = Convert.ToInt16(gssportStr);


                    //-------服务端通讯相关---------
                    IPAddress svripaddr = IPAddress.Parse(gssipStr);
                    ushort uPort = ushort.Parse(gssportStr);
                    svr = new TcpSvr(svripaddr, uPort, 1024, new Coder(Coder.EncodingMothord.Default));
                    svr.Resovlver = new DatagramResolver("]$}");
                    //处理客户端连接数已满事件
                    svr.ServerFull += new NetEvent(this.ServerFull);
                    //处理新客户端连接事件
                    svr.ClientConn += new NetEvent(this.ClientConn);
                    //处理客户端关闭事件
                    svr.ClientClose += new NetEvent(this.ClientClose);
                    //处理接收到数据事件
                    svr.RecvData += new NetEvent(this.RecvData);
                    string Sstate = string.Format("{0}/{1}", svr.SessionCount, svr.Capacity);
                    lblServState.Text = Sstate;
                    //服务对客户端请求的处理实例
                    svrhandle = new ServerHandle(svr);

                    //日志记录
                    ShareData.Log.Info("GSS系统网络初始化" + gssipStr + ":" + gssportStr);

                    //开机自启动
                    string runstr = "";
                    try
                    { 
                        RegistryKey ms_run = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        if (ms_run.GetValue("ShenLongYouSoft") != null)
                        {
                            runstr = ms_run.GetValue("ShenLongYouSoft").ToString();
                        }
                        timer1.Enabled = true;
                    }
                    catch (System.Exception ex)
                    {
                        ex.ToString().ErrorLogger();
                        ShareData.Log.Error("注册表查询失败", ex);
                    }
                   
                    
                    if (runstr.Trim().Length > 0)
                    {
                        checkBox1.Checked = true;
                        ServStart();
                    }

                }
                else
                {
                    FormServConfig form2 = new FormServConfig();
                    form2.ShowDialog();
                    Application.Restart();
                }

            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                MessageBox.Show("GSS初始化失败！\r\n" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //日志记录
                ShareData.Log.Error("GSS初始化失败",ex);
                timer1.Enabled = false;
            }


        }


        //工单列表控件的委托
        private delegate void SetLblValue(string stateStr);
        private void SetLblStateValue(string stateStr)//加载到DV控件
        {
            if (this.InvokeRequired)
            {
                SetLblValue d = new SetLblValue(SetLblStateValue);
                object arg = stateStr;
                this.Invoke(d, arg);
            }
            else
            {
                lblServState.Text = stateStr;
            }
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            ManageMDIParent form = new ManageMDIParent();
            // FormManage form = new FormManage();
            form.Show();
        }

        bool issynzoneline = true;
        bool isnewtaskalter = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled == false) {
                return;
            }
            //labelTime.Text = string.Format("{0: yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            labelTime.Text = string.Format("{0:时间:yyyy年MM月dd日 dddd HH:mm:ss }", DateTime.Now);
            if (!buttonStart.Enabled)
            {
                string startStr = ".";
                string i = (DateTime.Now.Millisecond % 2).ToString();
                switch (i)
                {
                    case "0":
                        startStr = @"";
                        break;
                    case "1":
                        startStr = @".";
                        break;
                }
                lblIsStart.Text = startStr;
            }
            if (DateTime.Now.Minute==0&&issynzoneline)
            {
                //string back = WebServiceLib.SynGameZoneLine();
                //ShareData.Log.Info("同步游戏中的战区战线"+back);
                issynzoneline=false;
            }
            else
            {
                issynzoneline=true;
            }

            //查询新增工单
            if (DateTime.Now.Second % 10 == 0 && isnewtaskalter)
            {
                svrhandle.CheckNewTask();
                isnewtaskalter = false;
            }
            else if (DateTime.Now.Minute % 5 == 0)
            {
                isnewtaskalter = true;
            }
        }


    }
}
