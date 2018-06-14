namespace RemoteRestart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonSure = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPSW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.checkBoxSysTurn = new System.Windows.Forms.CheckBox();
            this.checkBoxReboot = new System.Windows.Forms.CheckBox();
            this.checkBoxShutdown = new System.Windows.Forms.CheckBox();
            this.textBoxIPNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonIPY = new System.Windows.Forms.RadioButton();
            this.radioButtonIPN = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.labelIPOnline = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSure
            // 
            this.buttonSure.Location = new System.Drawing.Point(29, 213);
            this.buttonSure.Name = "buttonSure";
            this.buttonSure.Size = new System.Drawing.Size(174, 37);
            this.buttonSure.TabIndex = 0;
            this.buttonSure.Text = "执 行 操 作";
            this.buttonSure.UseVisualStyleBackColor = true;
            this.buttonSure.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "管理员帐号:";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(89, 14);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(145, 21);
            this.textBoxUser.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "管理员密码:";
            // 
            // textBoxPSW
            // 
            this.textBoxPSW.Location = new System.Drawing.Point(89, 46);
            this.textBoxPSW.Name = "textBoxPSW";
            this.textBoxPSW.Size = new System.Drawing.Size(145, 21);
            this.textBoxPSW.TabIndex = 2;
            this.textBoxPSW.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "IP网段:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(89, 76);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(145, 21);
            this.textBoxIP.TabIndex = 2;
            this.textBoxIP.Text = "192.168.7.";
            // 
            // checkBoxSysTurn
            // 
            this.checkBoxSysTurn.AutoSize = true;
            this.checkBoxSysTurn.Checked = true;
            this.checkBoxSysTurn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSysTurn.Location = new System.Drawing.Point(117, 159);
            this.checkBoxSysTurn.Name = "checkBoxSysTurn";
            this.checkBoxSysTurn.Size = new System.Drawing.Size(96, 16);
            this.checkBoxSysTurn.TabIndex = 4;
            this.checkBoxSysTurn.Text = "更改启动顺序";
            this.checkBoxSysTurn.UseVisualStyleBackColor = true;
            // 
            // checkBoxReboot
            // 
            this.checkBoxReboot.AutoSize = true;
            this.checkBoxReboot.Checked = true;
            this.checkBoxReboot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReboot.Location = new System.Drawing.Point(24, 159);
            this.checkBoxReboot.Name = "checkBoxReboot";
            this.checkBoxReboot.Size = new System.Drawing.Size(72, 16);
            this.checkBoxReboot.TabIndex = 4;
            this.checkBoxReboot.Text = "远程重启";
            this.checkBoxReboot.UseVisualStyleBackColor = true;
            this.checkBoxReboot.CheckedChanged += new System.EventHandler(this.checkBoxReboot_CheckedChanged);
            // 
            // checkBoxShutdown
            // 
            this.checkBoxShutdown.AutoSize = true;
            this.checkBoxShutdown.Location = new System.Drawing.Point(24, 181);
            this.checkBoxShutdown.Name = "checkBoxShutdown";
            this.checkBoxShutdown.Size = new System.Drawing.Size(72, 16);
            this.checkBoxShutdown.TabIndex = 5;
            this.checkBoxShutdown.Text = "远程关机";
            this.checkBoxShutdown.UseVisualStyleBackColor = true;
            this.checkBoxShutdown.CheckedChanged += new System.EventHandler(this.checkBoxShutdown_CheckedChanged);
            // 
            // textBoxIPNo
            // 
            this.textBoxIPNo.Location = new System.Drawing.Point(89, 103);
            this.textBoxIPNo.Name = "textBoxIPNo";
            this.textBoxIPNo.Size = new System.Drawing.Size(145, 21);
            this.textBoxIPNo.TabIndex = 7;
            this.textBoxIPNo.Text = "1,2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "特殊IP:";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.Color.ForestGreen;
            this.richTextBoxLog.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.richTextBoxLog.Location = new System.Drawing.Point(240, 14);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(186, 245);
            this.richTextBoxLog.TabIndex = 8;
            this.richTextBoxLog.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label5.Location = new System.Drawing.Point(9, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(215, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "IP网段示例:192.168.7.  排除IP:1,2,3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSure);
            this.groupBox1.Controls.Add(this.radioButtonIPY);
            this.groupBox1.Controls.Add(this.radioButtonIPN);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.labelIPOnline);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.richTextBoxLog);
            this.groupBox1.Controls.Add(this.textBoxUser);
            this.groupBox1.Controls.Add(this.textBoxIPNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBoxShutdown);
            this.groupBox1.Controls.Add(this.textBoxPSW);
            this.groupBox1.Controls.Add(this.checkBoxReboot);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkBoxSysTurn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxIP);
            this.groupBox1.Location = new System.Drawing.Point(7, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 280);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // radioButtonIPY
            // 
            this.radioButtonIPY.AutoSize = true;
            this.radioButtonIPY.Location = new System.Drawing.Point(129, 130);
            this.radioButtonIPY.Name = "radioButtonIPY";
            this.radioButtonIPY.Size = new System.Drawing.Size(95, 16);
            this.radioButtonIPY.TabIndex = 13;
            this.radioButtonIPY.Text = "只运行特殊IP";
            this.radioButtonIPY.UseVisualStyleBackColor = true;
            // 
            // radioButtonIPN
            // 
            this.radioButtonIPN.AutoSize = true;
            this.radioButtonIPN.Checked = true;
            this.radioButtonIPN.Location = new System.Drawing.Point(29, 130);
            this.radioButtonIPN.Name = "radioButtonIPN";
            this.radioButtonIPN.Size = new System.Drawing.Size(83, 16);
            this.radioButtonIPN.TabIndex = 13;
            this.radioButtonIPN.TabStop = true;
            this.radioButtonIPN.Text = "排除特殊IP";
            this.radioButtonIPN.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.ForestGreen;
            this.label6.Location = new System.Drawing.Point(360, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "在线IP";
            // 
            // labelIPOnline
            // 
            this.labelIPOnline.AutoSize = true;
            this.labelIPOnline.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelIPOnline.Location = new System.Drawing.Point(402, 262);
            this.labelIPOnline.Name = "labelIPOnline";
            this.labelIPOnline.Size = new System.Drawing.Size(11, 12);
            this.labelIPOnline.TabIndex = 11;
            this.labelIPOnline.Text = "0";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 289);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "局域网远程控制";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPSW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.CheckBox checkBoxSysTurn;
        private System.Windows.Forms.CheckBox checkBoxReboot;
        private System.Windows.Forms.CheckBox checkBoxShutdown;
        private System.Windows.Forms.TextBox textBoxIPNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelIPOnline;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButtonIPY;
        private System.Windows.Forms.RadioButton radioButtonIPN;
    }
}

