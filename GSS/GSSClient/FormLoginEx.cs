using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GSS.DBUtility;
using GSSCSFrameWork;
using System.Net;
using GSSUI;
using log4net;
using GSSBLL;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.IO;
namespace GSSClient
{
    public partial class FormLoginEx : GSSUI.AForm.FormMain
    {
        /// <summary>
        /// 客户端处理类
        /// </summary>
        private ClientHandles _clihandle;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormLoginEx()
        {

            InitializeComponent();
            //客户端处理类
            _clihandle = new ClientHandles(null);
            InitElement();
        }
        void InitElement()
        {
            cboxUserName.KeyDown += new KeyEventHandler(TextBox_EnterClick);
            tboxPassWord.KeyDown += new KeyEventHandler(TextBox_EnterClick);
        }
        void InitLanguageText()
        {
            this.Text = LanguageResource.Language.LblSystemName;
            this.label2.Text = LanguageResource.Language.LblPassword;
            this.label1.Text = LanguageResource.Language.LblAccount;
            this.button2.Text = LanguageResource.Language.LblReset;
            this.buttonLogIn.Text = LanguageResource.Language.LblLogin;
            this.groupBox1.Text = LanguageResource.Language.LblGssNetSet;
            this.button3.Text = LanguageResource.Language.LblPackUp + " ▲";
            this.button4.Text = LanguageResource.Language.LblSave;
            this.label3.Text = LanguageResource.Language.LblServicePort;
            this.label4.Text = LanguageResource.Language.LblServiceIP;
            btnLanguageSwitch.Text = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
          //  lblVersion.Text = SystemConfig.AppSoftwareName + " " + lblVersion.Text;
        }
        #region 事件

        /// <summary>
        /// 系统登录事件
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!WinUtil.isNull(cboxUserName.Text) || !WinUtil.isNull(tboxPassWord.Text))
            {
                MsgBox.Show(LanguageResource.Language.Tip_PleaseInputAccountOrPassword, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string ClinetIP = "127.0.0.1";
            try
            {
                //本地IP
                IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

                // string ip = iphost.AddressList[0].ToString();

                foreach (IPAddress ip in ipHostEntry.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ClinetIP = ip.ToString();
                        break;
                    }
                }

                //取得输入的帐号\密码
                string psw = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tboxPassWord.Text.Trim(), "MD5").ToLower();
                psw.Logger();
                string userid = _clihandle.GetLoginS(cboxUserName.Text, psw, ClinetIP);
                userid.Logger();
                if (SystemConfig.AutoUpdateGssClient)
                {
                    "AutoUpdateGssClient".Logger();
                    SureVersion();
                }

                if (GSS.DBUtility.WinUtil.IsNumber(userid))
                {
                    ShareData.UserID = userid.Trim();
                    "Will loding cache".Logger();
                    _clihandle.GetCahceSyn(ShareData.UserID, LoginComplateQueryCacheCallBack);
                }
                else if (userid.Length > 6)
                {
                    MsgBox.Show(userid, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MsgBox.Show(LanguageResource.Language.Tip_ErrorAccountOrPassword, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //记录日志
                    ShareData.Log.Info("登录系统,验证失败");
                }
                "login and cache finsh".Logger();
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                MsgBox.Show(LanguageResource.Language.Tip_ConnectionServiceError + "\r\n" + LanguageResource.Language.Tip_PleaseInspectNetOrService + "!\r\n(" + LanguageResource.Language.Tip_DoubleLogoForChangeNetConfig + ")", LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //记录日志
                ShareData.Log.Warn("登录系统时网络错误,请检查网络或服务器!\r\n服务端:" + ShareData.LocalIp + ":" + ShareData.LocalPort + "本地IP:" + ClinetIP + "", ex);
            }
        }

        /// <summary>
        /// 清空登录帐号密码
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            cboxUserName.Text = "";
            tboxPassWord.Text = "";
        }


        /// <summary>
        /// 鼠标双击LOGO事件
        /// </summary>
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetConfigView();
        }

        /// <summary>
        /// 收起窗体
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            SetConfigView();
        }

        /// <summary>
        /// 保存GSS网络配置
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }
        #endregion
        #region 方法

        private void SureVersion()
        {
            try
            {
                GSSBLL.BLLShareData bll = new GSSBLL.BLLShareData();
                string dd = bll.GSSClientVersion_C();

                TcpClientChannel channel = new TcpClientChannel();
                ChannelServices.RegisterChannel(channel, false);
                string classname = "BLLShareData";
                string serverurl = string.Format("tcp://{0}:{1}/{2}", ShareData.LocalIp, ShareData.LocalPort + 1, classname);
                GSSBLL.BLLShareData bllS = (GSSBLL.BLLShareData)Activator.GetObject(typeof(BLLShareData), serverurl);
                string verS = bllS.GSSClientVersion_S();
                string verC = bll.GSSClientVersion_C();
                ChannelServices.UnregisterChannel(channel);
                if (verC != verS)
                {
                    AutoUpdate();
                }
            }
            catch (System.Exception ex)
            {
                ShareData.Log.Warn(ex);
            }
        }

        /// <summary>
        /// 设置网络配置显示与否
        /// </summary>
        private void SetConfigView()
        {
            if (this.Height == 443)
            {
                this.Height = 289;
            }
            else
            {
                this.Height = 443;
            }
            lblVersion.Location = new Point(lblVersion.Location.X, this.Height - 25);
        }
        private void LoadConfig()
        {
            try
            {
                PubConstant.SqliteConnStr = Application.ExecutablePath.ToString();
                string[] servinfo = DataConfig.GetServerInfo();
                tGSSIP.Text = servinfo[0];
                tPort.Text = servinfo[1];
            }
            catch (System.Exception ex)
            {
                MsgBox.Show(LanguageResource.Language.Tip_ConfigNoExists, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //日志记录
                ShareData.Log.Error(LanguageResource.Language.Tip_ConfigNoExists, ex);
                return;
            }
        }
        /// <summary>
        /// 保存网络配置
        /// </summary>
        private void SaveConfig()
        {
            try
            {
                string gssip = tGSSIP.Text;
                string gssport = tPort.Text.Trim();
                if (!WinUtil.isIpaddres(gssip))
                {
                    MsgBox.Show(LanguageResource.Language.Tip_ErrorServiceIPFormat);
                    return;
                }
                if (!WinUtil.IsNumber(gssport))
                {
                    MsgBox.Show(LanguageResource.Language.Tip_PortShouldNumber);
                    return;
                }
                string sql = "UPDATE GSSCONFIG SET GSSIP='" + gssip + "',GSSPORT='" + gssport + "'WHERE ID=1";
                int row = DbHelperSQLite.ExecuteSql(sql);
                if (row >= 1)
                {
                    MsgBox.Show(LanguageResource.Language.Tip_SuccessSaveNetConfig, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetConfigView();
                }
                else
                {
                    MsgBox.Show(LanguageResource.Language.Tip_ErrorSaveNetConfig, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                MsgBox.Show(LanguageResource.Language.Tip_ErrorSaveNetConfigTip, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //日志记录
                ShareData.Log.Error(ex);
            }
        }

        /// <summary>
        /// 弹出更新程序
        /// </summary>
        private void AutoUpdate()
        {
            string paramStr = string.Format("{0} {1}", tGSSIP.Text, (Convert.ToInt32(tPort.Text) + 1).ToString());
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "GSSUpdate.exe", paramStr);// pp = new System.Diagnostics.Process();
            // Application.Exit();
        }

        #endregion

        private void FormLoginEx_Load(object sender, EventArgs e)
        {
            InitLanguageText();
            LoadConfig();
            cboxUserName.Focus();
        }

        private void FormLoginEx_Shown(object sender, EventArgs e)
        {
            cboxUserName.Focus();
        }

        private void btnLanguageSwitch_Click(object sender, EventArgs e)
        {
            //当前进程语言
            InitAppCurtule.SwitchLanguage();
            FormLoginEx_Load(this, null);
        }
        private void LoginComplateQueryCacheCallBack(object cache)
        {
            "loading cache end".Logger();
            MsgStruts msgb = cache as MsgStruts;
            try
            {
                if (cache == null)
                {
                    this.DialogResult = DialogResult.No;
                    string tip = "Error,please see above tip. ";
                    tip.Logger();
                    ShareData.Log.Info(tip);
                    MsgBox.Show(LanguageResource.Language.Tip_LoseCacheRefuseComing, LanguageResource.Language.Tip_Tip, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    return;
                }
                System.Data.DataSet ds = DataSerialize.GetDatasetFromByte(msgb.Data);
                if (ds == null || ds.Tables.Count == 0)
                {
                    string temp = "Because the network query Cache error.refuse use login,in fact use login success";
                    temp.Logger();
                    this.DialogResult = DialogResult.No;
                    ShareData.Log.Info(temp);
                    MsgBox.Show(LanguageResource.Language.Tip_LoseCacheRefuseComing, LanguageResource.Language.Tip_Tip, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    return;
                }
                int tables = ds.Tables.Count;
                if (SystemConfig.SyncVersionCache)
                {
                    DataTable version = ds.Tables["T_Version"];
                    object obj= version.Rows[0]["F_DB_Version"];
                    ds.Tables["T_Version"].Dispose();
                    string ver=string.Empty;
                    if (obj != null)
                    {
                        ver = obj.ToString();
                    }
                    //转换为文件流
                    System.IO.StreamWriter sr = new System.IO.StreamWriter(System.Windows.Forms.Application.StartupPath +"/GSSDATA/Version.DAT", false);
                    sr.Write(ver);
                    sr.Close();
                }
                (" [table]" + tables).Logger();
                ClientCache.SetCache(msgb.Data);
                ShareData.UserPower = ClientCache.GetUserPower(ShareData.UserID);
                this.DialogResult = DialogResult.OK;
                "cache convert complae".Logger();
            }
            catch (Exception ex)
            {
                ex.ToString().ErrorLogger();
                MsgBox.Show(ex.Message, LanguageItems.BaseLanguageItem.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void TextBox_EnterClick(object sender, KeyEventArgs e)
        {//登录使用回车快捷操作
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            button1_Click(sender, e);
        }

    }
}
