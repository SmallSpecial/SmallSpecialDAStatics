using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using GSSUpdateLib;
using System.IO;
using System.Threading;

namespace GSSUpdate
{
    public partial class FormMain : Form
    {
        NetFileTransfer nft = null;
        public Thread ThStartServer = null;
        public FormMain(string[] args)
        {
            InitializeComponent();
            if (args.Length == 2)
            {
                nft = NetFileTransfer(args[0], args[1]);
            }
            else
            {
                this.Close();
                Application.Exit();
            }

            ThStartServer = new Thread(new ThreadStart(DoBackRun));
            ThStartServer.IsBackground = true;
            ThStartServer.Start();
            //  nft=NetFileTransfer("192.168.7.77", "5234");
        }

        public void DoBackRun()
        {
            Thread.Sleep(500);
            btnStart_Click(null, null);
        }

        /// <summary>
        /// 获取文件，并保存
        /// </summary>
        /// <param name="RemoteFilePath">远程文件路径</param>
        /// <param name="LocalSavePath">本地保存路径</param>
        /// <returns>状态</returns>
        public bool GetFile(string RemoteFilePath, string LocalSavePath)
        {
            if (nft == null) return true;
            try
            {
                byte[] filebytes = nft.GetFileBytes(RemoteFilePath);
                if (filebytes != null)
                {
                    if (RemoteFilePath.IndexOf("GSSUpdate.exe") != -1 || RemoteFilePath.IndexOf("GSSUpdateLib.dll") != -1)
                    {
                        return true;
                    }
                    FileStream fs = new FileStream(LocalSavePath, FileMode.Create, FileAccess.Write, FileShare.Write);
                    fs.Write(filebytes, 0, filebytes.Length);
                    fs.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// NetFileTransfer
        /// </summary>
        public static NetFileTransfer NetFileTransfer(string ip, string port)
        {
            try
            {
                IChannel channel = new TcpClientChannel();
                string classname = "NetFileTransfer";
                string serverurl = string.Format("tcp://{0}:{1}/{2}", ip, port, classname);

                ChannelServices.RegisterChannel(channel, false);
                NetFileTransfer obj = (NetFileTransfer)Activator.GetObject(typeof(NetFileTransfer), serverurl);
                // ChannelServices.UnregisterChannel(channel);
                return obj;
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // GetFile(@"GSSServer.exe",  "GSSServer.exe");

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            pbDownFile.ResetText();
            lvUpdateList.Items.Clear();
            btnStart.Enabled = false;

            if (nft != null)
            {
                FileInfo[] files = nft.GetFileList("GSSClient");
                foreach (FileInfo f in files)
                {
                    string[] fileArray = new string[4];
                    fileArray[0] = f.Name;
                    fileArray[1] = f.LastWriteTime.ToString();
                    fileArray[2] = "准备";
                    fileArray[3] = f.FullName;
                    lvUpdateList.Items.Add(new ListViewItem(fileArray));
                    System.Threading.Thread.Sleep(50);
                }
                pbDownFile.Maximum = files.Length;
                pbDownFile.Step = 1;
                pbDownFile.Minimum = 0;
                pbDownFile.Value = 0;
            }

            System.Threading.Thread.Sleep(1500);
            for (int i = 0; i < this.lvUpdateList.Items.Count; i++)
            {
                string UpdateFile = lvUpdateList.Items[i].Text.Trim();
                string updateFileUrl = lvUpdateList.Items[i].SubItems[3].Text.Trim();
                string fileurl = updateFileUrl.Substring(updateFileUrl.IndexOf("GSSClient") + 10);
                FileInfo f = new FileInfo(fileurl);
                f.Directory.Create();
                if (GetFile(updateFileUrl, fileurl))
                {
                    this.lvUpdateList.Items[i].SubItems[2].Text = "完成";
                }
                else
                    this.lvUpdateList.Items[i].SubItems[2].Text = "失败";
                pbDownFile.PerformStep();
                System.Threading.Thread.Sleep(100);
            }
            btnCancel.Enabled = false;
            btnStart.Enabled = true;
            btnFinish.Enabled = true;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.ExitThread();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (btnStart.Enabled)
            //{
            //    btnStart_Click(null, null);
            //}
        }
    }
}
