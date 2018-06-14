namespace GSSClient
{
    partial class FormLoginEx
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoginEx));
            this.cboxUserName = new System.Windows.Forms.ComboBox();
            this.tboxPassWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new GSSUI.AControl.AButton.AButton();
            this.buttonLogIn = new GSSUI.AControl.AButton.AButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLanguageSwitch = new GSSUI.AControl.AButton.AButton();
            this.tGSSIP = new System.Windows.Forms.TextBox();
            this.button3 = new GSSUI.AControl.AButton.AButton();
            this.button4 = new GSSUI.AControl.AButton.AButton();
            this.tPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboxUserName
            // 
            this.cboxUserName.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboxUserName.ForeColor = System.Drawing.SystemColors.Desktop;
            this.cboxUserName.FormattingEnabled = true;
            this.cboxUserName.Location = new System.Drawing.Point(77, 191);
            this.cboxUserName.Name = "cboxUserName";
            this.cboxUserName.Size = new System.Drawing.Size(160, 20);
            this.cboxUserName.TabIndex = 0;
            // 
            // tboxPassWord
            // 
            this.tboxPassWord.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tboxPassWord.ForeColor = System.Drawing.SystemColors.Desktop;
            this.tboxPassWord.Location = new System.Drawing.Point(77, 227);
            this.tboxPassWord.Name = "tboxPassWord";
            this.tboxPassWord.Size = new System.Drawing.Size(160, 21);
            this.tboxPassWord.TabIndex = 1;
            this.tboxPassWord.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(34, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2223335;
            this.label2.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(34, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2223336;
            this.label1.Text = "账号";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("button2.BackImg")));
            this.button2.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlText;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Location = new System.Drawing.Point(250, 225);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = global::GSSClient.LanguageResource.Language.LblReset;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.BackColor = System.Drawing.Color.Transparent;
            this.buttonLogIn.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("buttonLogIn.BackImg")));
            this.buttonLogIn.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.buttonLogIn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlText;
            this.buttonLogIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.buttonLogIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogIn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonLogIn.Location = new System.Drawing.Point(250, 189);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(63, 23);
            this.buttonLogIn.TabIndex = 2;
            this.buttonLogIn.Text = global::GSSClient.LanguageResource.Language.LblLogin;
            this.buttonLogIn.UseVisualStyleBackColor = false;
            this.buttonLogIn.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::GSSClient.Properties.Resources.loginbanner;
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2223337;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnLanguageSwitch);
            this.groupBox1.Controls.Add(this.tGSSIP);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.tPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(26, 284);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 119);
            this.groupBox1.TabIndex = 2223334;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gss网络设置";
            // 
            // btnLanguageSwitch
            // 
            this.btnLanguageSwitch.BackColor = System.Drawing.Color.Transparent;
            this.btnLanguageSwitch.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnLanguageSwitch.BackImg")));
            this.btnLanguageSwitch.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnLanguageSwitch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLanguageSwitch.Location = new System.Drawing.Point(6, 90);
            this.btnLanguageSwitch.Name = "btnLanguageSwitch";
            this.btnLanguageSwitch.Size = new System.Drawing.Size(56, 23);
            this.btnLanguageSwitch.TabIndex = 5;
            this.btnLanguageSwitch.Text = "语言";
            this.btnLanguageSwitch.UseVisualStyleBackColor = true;
            this.btnLanguageSwitch.Click += new System.EventHandler(this.btnLanguageSwitch_Click);
            // 
            // tGSSIP
            // 
            this.tGSSIP.ForeColor = System.Drawing.SystemColors.Desktop;
            this.tGSSIP.Location = new System.Drawing.Point(89, 28);
            this.tGSSIP.MaxLength = 15;
            this.tGSSIP.Name = "tGSSIP";
            this.tGSSIP.Size = new System.Drawing.Size(183, 21);
            this.tGSSIP.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("button3.BackImg")));
            this.button3.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button3.Location = new System.Drawing.Point(169, 90);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "收 起 ▲";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("button4.BackImg")));
            this.button4.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button4.Location = new System.Drawing.Point(89, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(71, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = global::GSSClient.LanguageResource.Language.LblSave;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tPort
            // 
            this.tPort.ForeColor = System.Drawing.SystemColors.Desktop;
            this.tPort.Location = new System.Drawing.Point(89, 55);
            this.tPort.Name = "tPort";
            this.tPort.Size = new System.Drawing.Size(183, 21);
            this.tPort.TabIndex = 1;
            this.tPort.Text = "5233";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "服务端口号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "服务器ｉｐ";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("宋体", 12F);
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(12, 264);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(80, 16);
            this.lblVersion.TabIndex = 2223338;
            this.lblVersion.Text = "V1.0.0.70";
            // 
            // FormLoginEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 289);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboxUserName);
            this.Controls.Add(this.tboxPassWord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.pictureBox1);
            this.FormSystemBtnSet = GSSUI.AForm.ABaseForm.FormSystemBtn.btn_miniAndbtn_close;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(352, 443);
            this.MinimumSize = new System.Drawing.Size(352, 289);
            this.Name = "FormLoginEx";
            this.Opacity = 0.999D;
            this.Text = "GSS游戏服务系统";
            this.UseFadeStyle = true;
            this.Load += new System.EventHandler(this.FormLoginEx_Load);
            this.Shown += new System.EventHandler(this.FormLoginEx_Shown);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.buttonLogIn, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tboxPassWord, 0);
            this.Controls.SetChildIndex(this.cboxUserName, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.lblVersion, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboxUserName;
        private System.Windows.Forms.TextBox tboxPassWord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private GSSUI.AControl.AButton.AButton button2;
        private GSSUI.AControl.AButton.AButton buttonLogIn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private GSSUI.AControl.AButton.AButton button3;
        private GSSUI.AControl.AButton.AButton button4;
        private System.Windows.Forms.TextBox tPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tGSSIP;
        private System.Windows.Forms.Label lblVersion;
        private GSSUI.AControl.AButton.AButton btnLanguageSwitch;

    }
}