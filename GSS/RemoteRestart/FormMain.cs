using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;

namespace RemoteRestart
{
    public partial class FormMain : Form
    {
        List<Thread> list = new List<Thread>();
        public FormMain()
        {
            InitializeComponent();
            // Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxUser.Text.Trim().Length==0||textBoxPSW.Text.Trim().Length==0)
                {
                    MessageBox.Show("请写入管理员帐号及管理员密码");
                    return;
                }
                if (MessageBox.Show("将扫描局域网内在线IP并执行选中动作\r\n确定要进行此操作吗?","提示消息",MessageBoxButtons.OKCancel,MessageBoxIcon.Information)== DialogResult.Cancel)
                {
                    return;
                }
                

                buttonSure.Enabled = false;

                richTextBoxLog.Text = "";
                labelIPOnline.Text = "0";

                list.Clear();
                buttonSure.Invalidate(true);

                GetLocalIPByPing();

                //foreach (Thread t in list)
                //{
                //    if (t.ThreadState != System.Threading.ThreadState.Stopped)
                //    {
                //        this.Refresh();
                //        t.Join(500);

                //    }
                //}

                buttonSure.Enabled = true;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("执行出错 " + ex.Message);
                buttonSure.Enabled = true;

            }

        }

        private void checkBoxReboot_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxReboot.Checked)
            {
                return;
            }
            checkBoxShutdown.Checked = !checkBoxReboot.Checked;
        }
        private void checkBoxShutdown_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxShutdown.Checked)
            {
                return;
            }
            checkBoxReboot.Checked = !checkBoxShutdown.Checked;
        }
        public ArrayList GetLocalIPByArp()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine("arp -a");
            p.StandardInput.WriteLine("exit");
            ArrayList list = new ArrayList();
            StreamReader reader = p.StandardOutput;
            string IPHead = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString().Substring(0, 3);
            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                line = line.Trim();
                if (line.StartsWith(IPHead) && (line.IndexOf("dynamic") != -1))
                {
                    string IP = line.Substring(0, 15).Trim();
                    string Mac = line.Substring(line.IndexOf("-") - 2, 0x11).Trim();
                    LocalMachine localMachine = new LocalMachine();
                    localMachine.MachineIP = IP;
                    localMachine.MachineMAC = Mac;
                    localMachine.MachineName = "";
                    list.Add(localMachine);

                    richTextBoxLog.Text += "" + IP + "  成功\r\n";
                    // SetRemote(IP);
                }
            }
            return list;
        }

        private void GetLocalIPByPing()
        {
            string gate = textBoxIP.Text;
            string ipNos = "," + textBoxIPNo.Text + ",";
            for (int i = 1; i <= 255; i++)
            {
                if (radioButtonIPN.Checked)
                {
                    if (ipNos.IndexOf("," + i + ",") >= 0)
                    {
                        continue;
                    }
                }
                else
                {
                    if (ipNos.IndexOf("," + i + ",") == -1)
                    {
                        continue;
                    }
                }


                IPAddress address = IPAddress.Parse(gate + i.ToString());
                Thread thread = new Thread(new ThreadStart(
                    delegate()
                    {
                        Ping p = new Ping();
                        PingReply result = p.Send(address);
                        if (result.Status == IPStatus.Success)
                        {
                            lock (labelIPOnline)
                            {
                                labelIPOnline.Text = (Convert.ToInt16(labelIPOnline.Text) + 1).ToString();
                            }
                            SetRemote(address.ToString());
                        }
                    }
                    ));
                lock (list)
                {
                    list.Add(thread);
                }
                thread.IsBackground = true;
                thread.Start();
            }

        }


        private void SetRemote(string remoteip)
        {
            //lock (this.richTextBoxLog)
            //{
            //    richTextBoxLog.Text += "" + remoteip + "  执行成功!\r\n";
            //    richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
            //    richTextBoxLog.ScrollToCaret();
            //}
            //return;

            ConnectionOptions options = new ConnectionOptions();
            //设定用于WMI连接操作的用户名
            options.Username = textBoxUser.Text;
            //设定用户的口令
            options.Password = textBoxPSW.Text;

            try
            {
                if (checkBoxSysTurn.Checked)
                {
                    //ManagementPath 包装了生成和分析wmi对象的路径
                    ManagementPath mngPath = new ManagementPath(@"\\" + remoteip + @"\root\cimv2:Win32_Process");
                    ManagementScope scope = new ManagementScope(mngPath, options);
                    scope.Connect();

                    //ObjectGetOptions 类是指定用于获取管理对象的选项
                    ObjectGetOptions objOption = new ObjectGetOptions();
                    //ManagementClass 是表示公共信息模型 (CIM) 管理类，通过该类的成员，可以使用特定的 WMI 类路径访问 WMI 数据
                    ManagementClass classInstance = new ManagementClass(scope, mngPath, objOption);

                    int ProcessId = 0;
                    object[] cmdline = { @"cmd /c c:\windows\boot.exe", @"c:\", null, ProcessId };

                    //调用执行命令的方法
                    classInstance.InvokeMethod("Create", cmdline);
                }


                ManagementScope Conn = new ManagementScope("\\\\" + remoteip + "\\root\\cimv2", options);
                Conn.Connect();

                //确定WMI操作的内容
                ObjectQuery oq = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher query1 = new ManagementObjectSearcher(Conn, oq);
                //获取WMI操作内容
                ManagementObjectCollection queryCollection1 = query1.Get();
                //根据使用者选择，执行相应的远程操作
                foreach (ManagementObject mo in queryCollection1)
                {
                    string[] ss = { "" };

                    if (checkBoxReboot.Checked)
                    {
                        mo.InvokeMethod("Reboot", ss);
                    }
                    else
                    {
                        mo.InvokeMethod("Shutdown", ss);
                    }
                }

                lock (this.richTextBoxLog)
                {
                    richTextBoxLog.Text += "" + remoteip + "  执行成功!\r\n";
                    richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                    richTextBoxLog.ScrollToCaret();
                }
            }
            catch (Exception ex)  //报错
            {
                lock (this.richTextBoxLog)
                {
                    richTextBoxLog.Text += "" + remoteip + "  执行失败!" + ex.Message + "\r\n";
                    richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                    richTextBoxLog.ScrollToCaret();
                }
            }
        }



    }
    class LocalMachine
    {
        public string MachineIP = "";
        public string MachineMAC = "";
        public string MachineName = "";
    }
}
