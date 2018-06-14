namespace GSSClient
{
    partial class FormTaskAddNoUR
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskAddNoUR));
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.lblTaskType = new System.Windows.Forms.Label();
            this.lblURinfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxVIP = new System.Windows.Forms.ComboBox();
            this.cboxLimitTime = new System.Windows.Forms.ComboBox();
            this.button7 = new GSSUI.AControl.AButton.AButton();
            this.ckboxDonow = new System.Windows.Forms.CheckBox();
            this.button6 = new GSSUI.AControl.AButton.AButton();
            this.button5 = new GSSUI.AControl.AButton.AButton();
            this.button4 = new GSSUI.AControl.AButton.AButton();
            this.button3 = new GSSUI.AControl.AButton.AButton();
            this.groupBoxCheck = new System.Windows.Forms.GroupBox();
            this.tboxCOther = new System.Windows.Forms.TextBox();
            this.ckboxCOther = new System.Windows.Forms.CheckBox();
            this.ckboxCPersonID = new System.Windows.Forms.CheckBox();
            this.groupBoxKnow = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tboxGPeopleName = new System.Windows.Forms.TextBox();
            this.tboxTelphone = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rboxNote = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.btnDosure = new GSSUI.AControl.AButton.AButton();
            this.btnDoesc = new GSSUI.AControl.AButton.AButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxCheck.SuspendLayout();
            this.groupBoxKnow.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.BackColor = System.Drawing.Color.White;
            this.groupBoxInfo.Controls.Add(this.lblTaskType);
            this.groupBoxInfo.Controls.Add(this.lblURinfo);
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.label2);
            this.groupBoxInfo.Controls.Add(this.cboxVIP);
            this.groupBoxInfo.Controls.Add(this.cboxLimitTime);
            this.groupBoxInfo.Location = new System.Drawing.Point(6, 6);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(502, 59);
            this.groupBoxInfo.TabIndex = 0;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = LanguageResource.Language.LblBaseInfo;
            // 
            // lblTaskType
            // 
            this.lblTaskType.AutoSize = true;
            this.lblTaskType.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lblTaskType.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblTaskType.Location = new System.Drawing.Point(6, 25);
            this.lblTaskType.Name = "lblTaskType";
            this.lblTaskType.Size = new System.Drawing.Size(67, 14);
            this.lblTaskType.TabIndex = 6;
            this.lblTaskType.Text = LanguageResource.Language.LblWorkOrderType;
            // 
            // lblURinfo
            // 
            this.lblURinfo.AutoSize = true;
            this.lblURinfo.Location = new System.Drawing.Point(7, 36);
            this.lblURinfo.Name = "lblURinfo";
            this.lblURinfo.Size = new System.Drawing.Size(53, 12);
            this.lblURinfo.TabIndex = 5;
            this.lblURinfo.Text = LanguageResource.Language.LblBaseInfo;
            this.lblURinfo.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text =LanguageResource.Language.LblVipLevel+"";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = LanguageResource.Language.LblWorkOrderLimit;
            // 
            // cboxVIP
            // 
            this.cboxVIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxVIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxVIP.FormattingEnabled = true;
            this.cboxVIP.Items.AddRange(new object[] {
            "VIP0",
            "VIP1",
            "VIP2",
            "VIP3",
            "VIP4",
            "VIP5",
            "VIP6",
            "VIP7"});
            this.cboxVIP.Location = new System.Drawing.Point(298, 23);
            this.cboxVIP.Name = "cboxVIP";
            this.cboxVIP.Size = new System.Drawing.Size(60, 20);
            this.cboxVIP.TabIndex = 2;
            // 
            // cboxLimitTime
            // 
            this.cboxLimitTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxLimitTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxLimitTime.FormattingEnabled = true;
            this.cboxLimitTime.Location = new System.Drawing.Point(423, 23);
            this.cboxLimitTime.Name = "cboxLimitTime";
            this.cboxLimitTime.Size = new System.Drawing.Size(73, 20);
            this.cboxLimitTime.TabIndex = 1;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("button7.BackImg")));
            this.button7.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button7.Location = new System.Drawing.Point(419, 23);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(90, 26);
            this.button7.TabIndex = 12;
            this.button7.Text = LanguageResource.Language.BtnResetAntiIndulgence;
            this.button7.UseVisualStyleBackColor = true;
            // 
            // ckboxDonow
            // 
            this.ckboxDonow.AutoSize = true;
            this.ckboxDonow.Location = new System.Drawing.Point(7, 3);
            this.ckboxDonow.Name = "ckboxDonow";
            this.ckboxDonow.Size = new System.Drawing.Size(132, 16);
            this.ckboxDonow.TabIndex = 7;
            this.ckboxDonow.Text = LanguageResource.Language.BtnSubmitAndRun;
            this.ckboxDonow.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("button6.BackImg")));
            this.button6.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button6.Location = new System.Drawing.Point(323, 23);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 26);
            this.button6.TabIndex = 11;
            this.button6.Text =LanguageResource.Language.LblPlayNOTool;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("button5.BackImg")));
            this.button5.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button5.Location = new System.Drawing.Point(227, 23);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 26);
            this.button5.TabIndex = 10;
            this.button5.Text =LanguageResource.Language.BtnGagTool;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("button4.BackImg")));
            this.button4.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button4.Location = new System.Drawing.Point(131, 23);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 26);
            this.button4.TabIndex = 9;
            this.button4.Text = LanguageResource.Language.BtnCloseDownRole;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("button3.BackImg")));
            this.button3.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.button3.Location = new System.Drawing.Point(32, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 26);
            this.button3.TabIndex = 8;
            this.button3.Text = LanguageResource.Language.BtnCloseDownAccount;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBoxCheck
            // 
            this.groupBoxCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCheck.BackColor = System.Drawing.Color.White;
            this.groupBoxCheck.Controls.Add(this.tboxCOther);
            this.groupBoxCheck.Controls.Add(this.ckboxCOther);
            this.groupBoxCheck.Controls.Add(this.ckboxCPersonID);
            this.groupBoxCheck.Location = new System.Drawing.Point(6, 71);
            this.groupBoxCheck.Name = "groupBoxCheck";
            this.groupBoxCheck.Size = new System.Drawing.Size(502, 60);
            this.groupBoxCheck.TabIndex = 1;
            this.groupBoxCheck.TabStop = false;
            this.groupBoxCheck.Text = "验证项";
            // 
            // tboxCOther
            // 
            this.tboxCOther.Location = new System.Drawing.Point(181, 25);
            this.tboxCOther.MaxLength = 100;
            this.tboxCOther.Name = "tboxCOther";
            this.tboxCOther.Size = new System.Drawing.Size(259, 21);
            this.tboxCOther.TabIndex = 6;
            // 
            // ckboxCOther
            // 
            this.ckboxCOther.AutoSize = true;
            this.ckboxCOther.Location = new System.Drawing.Point(125, 27);
            this.ckboxCOther.Name = "ckboxCOther";
            this.ckboxCOther.Size = new System.Drawing.Size(48, 16);
            this.ckboxCOther.TabIndex = 5;
            this.ckboxCOther.Text = "其它";
            this.ckboxCOther.UseVisualStyleBackColor = true;
            // 
            // ckboxCPersonID
            // 
            this.ckboxCPersonID.AutoSize = true;
            this.ckboxCPersonID.Location = new System.Drawing.Point(8, 27);
            this.ckboxCPersonID.Name = "ckboxCPersonID";
            this.ckboxCPersonID.Size = new System.Drawing.Size(108, 16);
            this.ckboxCPersonID.TabIndex = 1;
            this.ckboxCPersonID.Text = "提供完整证件号";
            this.ckboxCPersonID.UseVisualStyleBackColor = true;
            // 
            // groupBoxKnow
            // 
            this.groupBoxKnow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKnow.BackColor = System.Drawing.Color.White;
            this.groupBoxKnow.Controls.Add(this.label9);
            this.groupBoxKnow.Controls.Add(this.tboxGPeopleName);
            this.groupBoxKnow.Controls.Add(this.tboxTelphone);
            this.groupBoxKnow.Controls.Add(this.label8);
            this.groupBoxKnow.Controls.Add(this.label4);
            this.groupBoxKnow.Location = new System.Drawing.Point(6, 137);
            this.groupBoxKnow.Name = "groupBoxKnow";
            this.groupBoxKnow.Size = new System.Drawing.Size(502, 53);
            this.groupBoxKnow.TabIndex = 2;
            this.groupBoxKnow.TabStop = false;
            this.groupBoxKnow.Text = "需要了解的信息项";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "联系人称呼:";
            // 
            // tboxGPeopleName
            // 
            this.tboxGPeopleName.Location = new System.Drawing.Point(84, 20);
            this.tboxGPeopleName.MaxLength = 16;
            this.tboxGPeopleName.Name = "tboxGPeopleName";
            this.tboxGPeopleName.Size = new System.Drawing.Size(89, 21);
            this.tboxGPeopleName.TabIndex = 13;
            // 
            // tboxTelphone
            // 
            this.tboxTelphone.Location = new System.Drawing.Point(244, 20);
            this.tboxTelphone.MaxLength = 16;
            this.tboxTelphone.Name = "tboxTelphone";
            this.tboxTelphone.Size = new System.Drawing.Size(117, 21);
            this.tboxTelphone.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(179, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = LanguageResource.Language.LblTel+":";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(379, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "注:其他项请写入备注";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.rboxNote);
            this.groupBox4.Location = new System.Drawing.Point(6, 196);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(501, 171);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = LanguageResource.Language.LblWorkOrderRemark;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(57, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "*";
            // 
            // rboxNote
            // 
            this.rboxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rboxNote.Location = new System.Drawing.Point(6, 18);
            this.rboxNote.MaxLength = 500;
            this.rboxNote.Name = "rboxNote";
            this.rboxNote.Size = new System.Drawing.Size(489, 145);
            this.rboxNote.TabIndex = 0;
            this.rboxNote.Text = global::GSSClient.Properties.Resources.d;
            // 
            // btnDosure
            // 
            this.btnDosure.BackColor = System.Drawing.Color.Transparent;
            this.btnDosure.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnDosure.BackImg")));
            this.btnDosure.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDosure.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDosure.Location = new System.Drawing.Point(77, 374);
            this.btnDosure.Name = "btnDosure";
            this.btnDosure.Size = new System.Drawing.Size(125, 30);
            this.btnDosure.TabIndex = 4;
            this.btnDosure.Text =  LanguageResource.Language.BtnSure;
            this.btnDosure.UseVisualStyleBackColor = false;
            this.btnDosure.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDoesc
            // 
            this.btnDoesc.BackColor = System.Drawing.Color.Transparent;
            this.btnDoesc.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnDoesc.BackImg")));
            this.btnDoesc.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDoesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDoesc.Location = new System.Drawing.Point(274, 374);
            this.btnDoesc.Name = "btnDoesc";
            this.btnDoesc.Size = new System.Drawing.Size(125, 30);
            this.btnDoesc.TabIndex = 5;
            this.btnDoesc.Text =  LanguageResource.Language.BtnCancel;
            this.btnDoesc.UseVisualStyleBackColor = false;
            this.btnDoesc.Click += new System.EventHandler(this.button2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(12, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(57, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = " ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel1.Text =  LanguageResource.Language.LblReady;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBoxInfo);
            this.panel1.Controls.Add(this.btnDoesc);
            this.panel1.Controls.Add(this.groupBoxCheck);
            this.panel1.Controls.Add(this.btnDosure);
            this.panel1.Controls.Add(this.groupBoxKnow);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Location = new System.Drawing.Point(6, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 412);
            this.panel1.TabIndex = 6;
            // 
            // FormTaskAddNoUR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 482);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormSystemBtnSet = GSSUI.AForm.ABaseForm.FormSystemBtn.btn_miniAndbtn_close;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTaskAddNoUR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工单建立 -- GSS游戏服务系统";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxCheck.ResumeLayout(false);
            this.groupBoxCheck.PerformLayout();
            this.groupBoxKnow.ResumeLayout(false);
            this.groupBoxKnow.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxCheck;
        private System.Windows.Forms.GroupBox groupBoxKnow;
        private System.Windows.Forms.GroupBox groupBox4;
        private GSSUI.AControl.AButton.AButton btnDosure;
        private GSSUI.AControl.AButton.AButton btnDoesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboxVIP;
        private System.Windows.Forms.ComboBox cboxLimitTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblURinfo;
        private System.Windows.Forms.TextBox tboxCOther;
        private System.Windows.Forms.CheckBox ckboxCOther;
        private System.Windows.Forms.CheckBox ckboxCPersonID;
        private System.Windows.Forms.TextBox tboxTelphone;
        private System.Windows.Forms.Label label8;
        private GSSUI.AControl.ARichTextBox.ARichTextBox rboxNote;
        private System.Windows.Forms.CheckBox ckboxDonow;
        private GSSUI.AControl.AButton.AButton button5;
        private GSSUI.AControl.AButton.AButton button4;
        private GSSUI.AControl.AButton.AButton button3;
        private GSSUI.AControl.AButton.AButton button7;
        private GSSUI.AControl.AButton.AButton button6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TextBox tboxGPeopleName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTaskType;
        private System.Windows.Forms.Label label10;
    }
}