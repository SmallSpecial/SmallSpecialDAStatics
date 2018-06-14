namespace RTXNotifyServer
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tboxRPSW = new System.Windows.Forms.TextBox();
            this.tboxRUser = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tboxRPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tboxRIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonQuit = new System.Windows.Forms.ToolStripButton();
            this.labelStatus = new System.Windows.Forms.Label();
            this.tboxInfo = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开窗体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxConnStr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxTFSDetailURL = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tboxMailIP = new System.Windows.Forms.TextBox();
            this.checkBoxIM = new System.Windows.Forms.CheckBox();
            this.checkBoxSys = new System.Windows.Forms.CheckBox();
            this.radioButtonGroup = new System.Windows.Forms.RadioButton();
            this.radioButtonOne = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSysAlertTime = new System.Windows.Forms.Label();
            this.textBoxTFSPsw = new System.Windows.Forms.TextBox();
            this.tboxSysAlertTime = new System.Windows.Forms.TextBox();
            this.textBoxTFSUser = new System.Windows.Forms.TextBox();
            this.tboxMailPort = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(204, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "密码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 29;
            this.label7.Text = "机器人帐号：";
            // 
            // tboxRPSW
            // 
            this.tboxRPSW.Enabled = false;
            this.tboxRPSW.Location = new System.Drawing.Point(251, 54);
            this.tboxRPSW.Name = "tboxRPSW";
            this.tboxRPSW.Size = new System.Drawing.Size(79, 21);
            this.tboxRPSW.TabIndex = 25;
            this.tboxRPSW.Text = "111111";
            this.tboxRPSW.UseSystemPasswordChar = true;
            // 
            // tboxRUser
            // 
            this.tboxRUser.Enabled = false;
            this.tboxRUser.Location = new System.Drawing.Point(95, 54);
            this.tboxRUser.Name = "tboxRUser";
            this.tboxRUser.Size = new System.Drawing.Size(103, 21);
            this.tboxRUser.TabIndex = 23;
            this.tboxRUser.Text = "机器人";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tboxRPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tboxRIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tboxRPSW);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tboxRUser);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(7, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 87);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RTX配置项";
            // 
            // tboxRPort
            // 
            this.tboxRPort.Enabled = false;
            this.tboxRPort.Location = new System.Drawing.Point(251, 20);
            this.tboxRPort.Name = "tboxRPort";
            this.tboxRPort.Size = new System.Drawing.Size(79, 21);
            this.tboxRPort.TabIndex = 31;
            this.tboxRPort.Text = "8006";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "端口：";
            // 
            // tboxRIP
            // 
            this.tboxRIP.Enabled = false;
            this.tboxRIP.Location = new System.Drawing.Point(95, 20);
            this.tboxRIP.Name = "tboxRIP";
            this.tboxRIP.Size = new System.Drawing.Size(103, 21);
            this.tboxRIP.TabIndex = 30;
            this.tboxRIP.Text = "192.168.7.77";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "RTX服务器IP：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStart,
            this.toolStripButtonStop,
            this.toolStripButtonSet,
            this.toolStripButtonQuit});
            this.toolStrip1.Location = new System.Drawing.Point(7, 102);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(336, 34);
            this.toolStrip1.TabIndex = 110;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.AutoToolTip = false;
            this.toolStripButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStart.Image")));
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(87, 31);
            this.toolStripButtonStart.Text = "开始服务";
            this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.AutoToolTip = false;
            this.toolStripButtonStop.Enabled = false;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(87, 31);
            this.toolStripButtonStop.Text = "停止服务";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // toolStripButtonSet
            // 
            this.toolStripButtonSet.AutoToolTip = false;
            this.toolStripButtonSet.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSet.Image")));
            this.toolStripButtonSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSet.Name = "toolStripButtonSet";
            this.toolStripButtonSet.Size = new System.Drawing.Size(81, 31);
            this.toolStripButtonSet.Text = "RTX配置";
            this.toolStripButtonSet.Click += new System.EventHandler(this.toolStripButtonSet_Click);
            // 
            // toolStripButtonQuit
            // 
            this.toolStripButtonQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonQuit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonQuit.Image")));
            this.toolStripButtonQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonQuit.Name = "toolStripButtonQuit";
            this.toolStripButtonQuit.Size = new System.Drawing.Size(34, 31);
            this.toolStripButtonQuit.Text = "退出服务";
            this.toolStripButtonQuit.Click += new System.EventHandler(this.toolStripButtonQuit_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(7, 266);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(137, 12);
            this.labelStatus.TabIndex = 111;
            this.labelStatus.Text = "已经运行0天0小时0分0秒";
            // 
            // tboxInfo
            // 
            this.tboxInfo.BackColor = System.Drawing.Color.ForestGreen;
            this.tboxInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tboxInfo.Location = new System.Drawing.Point(7, 139);
            this.tboxInfo.Name = "tboxInfo";
            this.tboxInfo.ReadOnly = true;
            this.tboxInfo.Size = new System.Drawing.Size(336, 124);
            this.tboxInfo.TabIndex = 112;
            this.tboxInfo.Text = "如果提示信息暂停使用了,请查看日志文件\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(253, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 111;
            this.label3.Text = "RTXRobot V1.0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "RTX消息服务Robot";
            this.notifyIcon1.BalloonTipTitle = "消息提示";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "RTX消息服务Robot";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开窗体ToolStripMenuItem,
            this.退出服务ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // 打开窗体ToolStripMenuItem
            // 
            this.打开窗体ToolStripMenuItem.Name = "打开窗体ToolStripMenuItem";
            this.打开窗体ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.打开窗体ToolStripMenuItem.Text = "管理界面";
            this.打开窗体ToolStripMenuItem.Click += new System.EventHandler(this.打开窗体ToolStripMenuItem_Click);
            // 
            // 退出服务ToolStripMenuItem
            // 
            this.退出服务ToolStripMenuItem.Name = "退出服务ToolStripMenuItem";
            this.退出服务ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.退出服务ToolStripMenuItem.Text = "退出服务";
            this.退出服务ToolStripMenuItem.Click += new System.EventHandler(this.退出服务ToolStripMenuItem_Click);
            // 
            // textBoxConnStr
            // 
            this.textBoxConnStr.Enabled = false;
            this.textBoxConnStr.Location = new System.Drawing.Point(72, 91);
            this.textBoxConnStr.Name = "textBoxConnStr";
            this.textBoxConnStr.Size = new System.Drawing.Size(257, 21);
            this.textBoxConnStr.TabIndex = 31;
            this.textBoxConnStr.Text = "server=192.168.7.77\\code;database=GameCoreDB;uid=sa;pwd=asdf1234!";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "TFS转RTX：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.tboxMailPort);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBoxTFSDetailURL);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tboxMailIP);
            this.groupBox2.Controls.Add(this.checkBoxIM);
            this.groupBox2.Controls.Add(this.checkBoxSys);
            this.groupBox2.Controls.Add(this.radioButtonGroup);
            this.groupBox2.Controls.Add(this.radioButtonOne);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lblSysAlertTime);
            this.groupBox2.Controls.Add(this.textBoxConnStr);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxTFSPsw);
            this.groupBox2.Controls.Add(this.tboxSysAlertTime);
            this.groupBox2.Controls.Add(this.textBoxTFSUser);
            this.groupBox2.Location = new System.Drawing.Point(7, 282);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 200);
            this.groupBox2.TabIndex = 113;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TFS配置项";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label12.Location = new System.Drawing.Point(73, 149);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(137, 12);
            this.label12.TabIndex = 40;
            this.label12.Text = "定时提醒使用,{0}表示id";
            // 
            // textBoxTFSDetailURL
            // 
            this.textBoxTFSDetailURL.Enabled = false;
            this.textBoxTFSDetailURL.Location = new System.Drawing.Point(2, 164);
            this.textBoxTFSDetailURL.Name = "textBoxTFSDetailURL";
            this.textBoxTFSDetailURL.Size = new System.Drawing.Size(326, 21);
            this.textBoxTFSDetailURL.TabIndex = 39;
            this.textBoxTFSDetailURL.Text = "http://192.168.7.2/sites/TFSDF/ShenLongYou/_layouts/tswa/UI/Pages/WorkItems/WorkI" +
                "temEdit.aspx?id={0}";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 121);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 38;
            this.label11.Text = "TFS用户：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 37;
            this.label5.Text = "TFS明细页：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 36;
            this.label9.Text = "邮件服务：";
            // 
            // tboxMailIP
            // 
            this.tboxMailIP.Enabled = false;
            this.tboxMailIP.Location = new System.Drawing.Point(72, 20);
            this.tboxMailIP.Name = "tboxMailIP";
            this.tboxMailIP.Size = new System.Drawing.Size(173, 21);
            this.tboxMailIP.TabIndex = 35;
            this.tboxMailIP.Text = "192.168.7.77";
            // 
            // checkBoxIM
            // 
            this.checkBoxIM.AutoSize = true;
            this.checkBoxIM.Checked = true;
            this.checkBoxIM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIM.Enabled = false;
            this.checkBoxIM.Location = new System.Drawing.Point(226, 48);
            this.checkBoxIM.Name = "checkBoxIM";
            this.checkBoxIM.Size = new System.Drawing.Size(84, 16);
            this.checkBoxIM.TabIndex = 34;
            this.checkBoxIM.Text = "IM消息提醒";
            this.checkBoxIM.UseVisualStyleBackColor = true;
            // 
            // checkBoxSys
            // 
            this.checkBoxSys.AutoSize = true;
            this.checkBoxSys.Checked = true;
            this.checkBoxSys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSys.Enabled = false;
            this.checkBoxSys.Location = new System.Drawing.Point(72, 69);
            this.checkBoxSys.Name = "checkBoxSys";
            this.checkBoxSys.Size = new System.Drawing.Size(72, 16);
            this.checkBoxSys.TabIndex = 34;
            this.checkBoxSys.Text = "系统消息";
            this.checkBoxSys.UseVisualStyleBackColor = true;
            // 
            // radioButtonGroup
            // 
            this.radioButtonGroup.AutoSize = true;
            this.radioButtonGroup.Checked = true;
            this.radioButtonGroup.Enabled = false;
            this.radioButtonGroup.Location = new System.Drawing.Point(72, 46);
            this.radioButtonGroup.Name = "radioButtonGroup";
            this.radioButtonGroup.Size = new System.Drawing.Size(59, 16);
            this.radioButtonGroup.TabIndex = 33;
            this.radioButtonGroup.TabStop = true;
            this.radioButtonGroup.Text = "群提醒";
            this.radioButtonGroup.UseVisualStyleBackColor = true;
            // 
            // radioButtonOne
            // 
            this.radioButtonOne.AutoSize = true;
            this.radioButtonOne.Enabled = false;
            this.radioButtonOne.Location = new System.Drawing.Point(145, 47);
            this.radioButtonOne.Name = "radioButtonOne";
            this.radioButtonOne.Size = new System.Drawing.Size(59, 16);
            this.radioButtonOne.TabIndex = 33;
            this.radioButtonOne.Text = "单提醒";
            this.radioButtonOne.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 32;
            this.label6.Text = "密码：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 32;
            this.label10.Text = "发送选项：";
            // 
            // lblSysAlertTime
            // 
            this.lblSysAlertTime.AutoSize = true;
            this.lblSysAlertTime.Enabled = false;
            this.lblSysAlertTime.Location = new System.Drawing.Point(143, 70);
            this.lblSysAlertTime.Name = "lblSysAlertTime";
            this.lblSysAlertTime.Size = new System.Drawing.Size(89, 12);
            this.lblSysAlertTime.TabIndex = 32;
            this.lblSysAlertTime.Text = "显示时间(秒)：";
            // 
            // textBoxTFSPsw
            // 
            this.textBoxTFSPsw.Enabled = false;
            this.textBoxTFSPsw.Location = new System.Drawing.Point(226, 118);
            this.textBoxTFSPsw.Name = "textBoxTFSPsw";
            this.textBoxTFSPsw.Size = new System.Drawing.Size(103, 21);
            this.textBoxTFSPsw.TabIndex = 23;
            this.textBoxTFSPsw.Text = "0000";
            this.textBoxTFSPsw.UseSystemPasswordChar = true;
            // 
            // tboxSysAlertTime
            // 
            this.tboxSysAlertTime.Enabled = false;
            this.tboxSysAlertTime.Location = new System.Drawing.Point(238, 64);
            this.tboxSysAlertTime.Name = "tboxSysAlertTime";
            this.tboxSysAlertTime.Size = new System.Drawing.Size(91, 21);
            this.tboxSysAlertTime.TabIndex = 23;
            this.tboxSysAlertTime.Text = "3.0";
            // 
            // textBoxTFSUser
            // 
            this.textBoxTFSUser.Enabled = false;
            this.textBoxTFSUser.Location = new System.Drawing.Point(72, 118);
            this.textBoxTFSUser.Name = "textBoxTFSUser";
            this.textBoxTFSUser.Size = new System.Drawing.Size(103, 21);
            this.textBoxTFSUser.TabIndex = 23;
            this.textBoxTFSUser.Text = "huangchao";
            // 
            // tboxMailPort
            // 
            this.tboxMailPort.Enabled = false;
            this.tboxMailPort.Location = new System.Drawing.Point(292, 20);
            this.tboxMailPort.Name = "tboxMailPort";
            this.tboxMailPort.Size = new System.Drawing.Size(36, 21);
            this.tboxMailPort.TabIndex = 41;
            this.tboxMailPort.Text = "25";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(251, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 42;
            this.label13.Text = "端口：";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(348, 493);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tboxInfo);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTX消息服务Robot -北京神龙游";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tboxRPSW;
        private System.Windows.Forms.TextBox tboxRUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tboxRPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tboxRIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonSet;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.RichTextBox tboxInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton toolStripButtonQuit;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开窗体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出服务ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxConnStr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSysAlertTime;
        private System.Windows.Forms.TextBox textBoxTFSPsw;
        private System.Windows.Forms.TextBox textBoxTFSUser;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radioButtonGroup;
        private System.Windows.Forms.RadioButton radioButtonOne;
        private System.Windows.Forms.CheckBox checkBoxIM;
        private System.Windows.Forms.CheckBox checkBoxSys;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tboxMailIP;
        private System.Windows.Forms.TextBox tboxSysAlertTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxTFSDetailURL;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tboxMailPort;
    }
}

