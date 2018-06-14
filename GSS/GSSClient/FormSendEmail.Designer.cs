namespace GSSClient
{
    partial class FormSendEmail
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
            this.gbEquip = new System.Windows.Forms.GroupBox();
            this.panelEquip = new System.Windows.Forms.Panel();
            this.txtEquipSizeOf = new System.Windows.Forms.TextBox();
            this.lblEquipID = new System.Windows.Forms.Label();
            this.lblEquipSizeOf = new System.Windows.Forms.Label();
            this.txtEquipID = new System.Windows.Forms.TextBox();
            this.cbHavaEquip = new System.Windows.Forms.CheckBox();
            this.gbReceiver = new System.Windows.Forms.GroupBox();
            this.gridReceiveRole = new System.Windows.Forms.DataGridView();
            this.lblDownTemplae = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.gbZoneWithBelongBig = new System.Windows.Forms.GroupBox();
            this.cmbZone = new System.Windows.Forms.ComboBox();
            this.lblZone = new System.Windows.Forms.Label();
            this.lblBigZone = new System.Windows.Forms.Label();
            this.cmbBigZone = new System.Windows.Forms.ComboBox();
            this.gbEmail = new System.Windows.Forms.GroupBox();
            this.gbValidTime = new System.Windows.Forms.GroupBox();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEmailBody = new System.Windows.Forms.Label();
            this.lblEmailHead = new System.Windows.Forms.Label();
            this.rtbEmailHead = new System.Windows.Forms.RichTextBox();
            this.rtbEmailBody = new System.Windows.Forms.RichTextBox();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.btnLodingRoles = new System.Windows.Forms.Button();
            this.btnClearRole = new System.Windows.Forms.Button();
            this.gbEquip.SuspendLayout();
            this.panelEquip.SuspendLayout();
            this.gbReceiver.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReceiveRole)).BeginInit();
            this.gbZoneWithBelongBig.SuspendLayout();
            this.gbEmail.SuspendLayout();
            this.gbValidTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEquip
            // 
            this.gbEquip.BackColor = System.Drawing.Color.White;
            this.gbEquip.Controls.Add(this.panelEquip);
            this.gbEquip.Controls.Add(this.cbHavaEquip);
            this.gbEquip.Location = new System.Drawing.Point(12, 97);
            this.gbEquip.Name = "gbEquip";
            this.gbEquip.Size = new System.Drawing.Size(401, 47);
            this.gbEquip.TabIndex = 2223335;
            this.gbEquip.TabStop = false;
            this.gbEquip.Text = "Equip";
            // 
            // panelEquip
            // 
            this.panelEquip.BackColor = System.Drawing.Color.White;
            this.panelEquip.Controls.Add(this.txtEquipSizeOf);
            this.panelEquip.Controls.Add(this.lblEquipID);
            this.panelEquip.Controls.Add(this.lblEquipSizeOf);
            this.panelEquip.Controls.Add(this.txtEquipID);
            this.panelEquip.Location = new System.Drawing.Point(105, 10);
            this.panelEquip.Name = "panelEquip";
            this.panelEquip.Size = new System.Drawing.Size(297, 34);
            this.panelEquip.TabIndex = 2223336;
            this.panelEquip.Visible = false;
            // 
            // txtEquipSizeOf
            // 
            this.txtEquipSizeOf.Location = new System.Drawing.Point(229, 6);
            this.txtEquipSizeOf.Name = "txtEquipSizeOf";
            this.txtEquipSizeOf.Size = new System.Drawing.Size(58, 21);
            this.txtEquipSizeOf.TabIndex = 4;
            // 
            // lblEquipID
            // 
            this.lblEquipID.AutoSize = true;
            this.lblEquipID.Location = new System.Drawing.Point(18, 10);
            this.lblEquipID.Name = "lblEquipID";
            this.lblEquipID.Size = new System.Drawing.Size(53, 12);
            this.lblEquipID.TabIndex = 1;
            this.lblEquipID.Text = "Equip ID";
            // 
            // lblEquipSizeOf
            // 
            this.lblEquipSizeOf.AutoSize = true;
            this.lblEquipSizeOf.Location = new System.Drawing.Point(169, 12);
            this.lblEquipSizeOf.Name = "lblEquipSizeOf";
            this.lblEquipSizeOf.Size = new System.Drawing.Size(41, 12);
            this.lblEquipSizeOf.TabIndex = 3;
            this.lblEquipSizeOf.Text = "SizeOf";
            // 
            // txtEquipID
            // 
            this.txtEquipID.Location = new System.Drawing.Point(78, 4);
            this.txtEquipID.Name = "txtEquipID";
            this.txtEquipID.Size = new System.Drawing.Size(58, 21);
            this.txtEquipID.TabIndex = 2;
            // 
            // cbHavaEquip
            // 
            this.cbHavaEquip.AutoSize = true;
            this.cbHavaEquip.Location = new System.Drawing.Point(7, 21);
            this.cbHavaEquip.Name = "cbHavaEquip";
            this.cbHavaEquip.Size = new System.Drawing.Size(84, 16);
            this.cbHavaEquip.TabIndex = 0;
            this.cbHavaEquip.Tag = "panelEquip";
            this.cbHavaEquip.Text = "Hava Equip";
            this.cbHavaEquip.UseVisualStyleBackColor = true;
            this.cbHavaEquip.Click += new System.EventHandler(this.CheckBox_Check);
            // 
            // gbReceiver
            // 
            this.gbReceiver.BackColor = System.Drawing.Color.White;
            this.gbReceiver.Controls.Add(this.gridReceiveRole);
            this.gbReceiver.Controls.Add(this.lblDownTemplae);
            this.gbReceiver.Controls.Add(this.txtFile);
            this.gbReceiver.Controls.Add(this.btnSelectFile);
            this.gbReceiver.Location = new System.Drawing.Point(13, 151);
            this.gbReceiver.Name = "gbReceiver";
            this.gbReceiver.Size = new System.Drawing.Size(400, 249);
            this.gbReceiver.TabIndex = 2223336;
            this.gbReceiver.TabStop = false;
            this.gbReceiver.Text = "Email Receiver";
            // 
            // gridReceiveRole
            // 
            this.gridReceiveRole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridReceiveRole.Location = new System.Drawing.Point(6, 56);
            this.gridReceiveRole.Name = "gridReceiveRole";
            this.gridReceiveRole.RowTemplate.Height = 23;
            this.gridReceiveRole.Size = new System.Drawing.Size(385, 185);
            this.gridReceiveRole.TabIndex = 81;
            // 
            // lblDownTemplae
            // 
            this.lblDownTemplae.AutoSize = true;
            this.lblDownTemplae.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblDownTemplae.Location = new System.Drawing.Point(11, 41);
            this.lblDownTemplae.Name = "lblDownTemplae";
            this.lblDownTemplae.Size = new System.Drawing.Size(113, 12);
            this.lblDownTemplae.TabIndex = 80;
            this.lblDownTemplae.Text = "down load template";
            this.lblDownTemplae.Click += new System.EventHandler(this.lblDownTemplae_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(8, 15);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(277, 21);
            this.txtFile.TabIndex = 2;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(299, 15);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "file";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            // 
            // gbZoneWithBelongBig
            // 
            this.gbZoneWithBelongBig.BackColor = System.Drawing.Color.White;
            this.gbZoneWithBelongBig.Controls.Add(this.cmbZone);
            this.gbZoneWithBelongBig.Controls.Add(this.lblZone);
            this.gbZoneWithBelongBig.Controls.Add(this.lblBigZone);
            this.gbZoneWithBelongBig.Controls.Add(this.cmbBigZone);
            this.gbZoneWithBelongBig.Location = new System.Drawing.Point(13, 42);
            this.gbZoneWithBelongBig.Name = "gbZoneWithBelongBig";
            this.gbZoneWithBelongBig.Size = new System.Drawing.Size(400, 49);
            this.gbZoneWithBelongBig.TabIndex = 2223337;
            this.gbZoneWithBelongBig.TabStop = false;
            this.gbZoneWithBelongBig.Text = "gpZone";
            // 
            // cmbZone
            // 
            this.cmbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZone.FormattingEnabled = true;
            this.cmbZone.Location = new System.Drawing.Point(240, 18);
            this.cmbZone.Name = "cmbZone";
            this.cmbZone.Size = new System.Drawing.Size(121, 20);
            this.cmbZone.TabIndex = 4;
            // 
            // lblZone
            // 
            this.lblZone.AutoSize = true;
            this.lblZone.Location = new System.Drawing.Point(205, 18);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(29, 12);
            this.lblZone.TabIndex = 3;
            this.lblZone.Text = "Zone";
            // 
            // lblBigZone
            // 
            this.lblBigZone.AutoSize = true;
            this.lblBigZone.Location = new System.Drawing.Point(6, 18);
            this.lblBigZone.Name = "lblBigZone";
            this.lblBigZone.Size = new System.Drawing.Size(53, 12);
            this.lblBigZone.TabIndex = 2;
            this.lblBigZone.Text = "Big Zone";
            // 
            // cmbBigZone
            // 
            this.cmbBigZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBigZone.FormattingEnabled = true;
            this.cmbBigZone.Location = new System.Drawing.Point(65, 18);
            this.cmbBigZone.Name = "cmbBigZone";
            this.cmbBigZone.Size = new System.Drawing.Size(121, 20);
            this.cmbBigZone.TabIndex = 0;
            this.cmbBigZone.Tag = "cmbZone";
            // 
            // gbEmail
            // 
            this.gbEmail.BackColor = System.Drawing.Color.White;
            this.gbEmail.Controls.Add(this.gbValidTime);
            this.gbEmail.Controls.Add(this.lblEmailBody);
            this.gbEmail.Controls.Add(this.lblEmailHead);
            this.gbEmail.Controls.Add(this.rtbEmailHead);
            this.gbEmail.Controls.Add(this.rtbEmailBody);
            this.gbEmail.Location = new System.Drawing.Point(420, 42);
            this.gbEmail.Name = "gbEmail";
            this.gbEmail.Size = new System.Drawing.Size(269, 358);
            this.gbEmail.TabIndex = 2223338;
            this.gbEmail.TabStop = false;
            this.gbEmail.Text = "gbEmail";
            // 
            // gbValidTime
            // 
            this.gbValidTime.Controls.Add(this.dtpEndTime);
            this.gbValidTime.Controls.Add(this.dtpStartTime);
            this.gbValidTime.Controls.Add(this.lblEndTime);
            this.gbValidTime.Controls.Add(this.lblStartTime);
            this.gbValidTime.Location = new System.Drawing.Point(8, 277);
            this.gbValidTime.Name = "gbValidTime";
            this.gbValidTime.Size = new System.Drawing.Size(255, 81);
            this.gbValidTime.TabIndex = 4;
            this.gbValidTime.TabStop = false;
            this.gbValidTime.Text = "Invalid Time";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(95, 47);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(147, 21);
            this.dtpEndTime.TabIndex = 7;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Location = new System.Drawing.Point(95, 17);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(147, 21);
            this.dtpStartTime.TabIndex = 6;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(6, 53);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(53, 12);
            this.lblEndTime.TabIndex = 5;
            this.lblEndTime.Text = "End Time";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(6, 17);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(65, 12);
            this.lblStartTime.TabIndex = 4;
            this.lblStartTime.Text = "Start Time";
            // 
            // lblEmailBody
            // 
            this.lblEmailBody.AutoSize = true;
            this.lblEmailBody.Location = new System.Drawing.Point(6, 80);
            this.lblEmailBody.Name = "lblEmailBody";
            this.lblEmailBody.Size = new System.Drawing.Size(65, 12);
            this.lblEmailBody.TabIndex = 3;
            this.lblEmailBody.Text = "Email Body";
            // 
            // lblEmailHead
            // 
            this.lblEmailHead.AutoSize = true;
            this.lblEmailHead.Location = new System.Drawing.Point(6, 18);
            this.lblEmailHead.Name = "lblEmailHead";
            this.lblEmailHead.Size = new System.Drawing.Size(65, 12);
            this.lblEmailHead.TabIndex = 2;
            this.lblEmailHead.Text = "Email Head";
            // 
            // rtbEmailHead
            // 
            this.rtbEmailHead.Location = new System.Drawing.Point(6, 33);
            this.rtbEmailHead.Name = "rtbEmailHead";
            this.rtbEmailHead.Size = new System.Drawing.Size(257, 40);
            this.rtbEmailHead.TabIndex = 1;
            this.rtbEmailHead.Text = "";
            // 
            // rtbEmailBody
            // 
            this.rtbEmailBody.Location = new System.Drawing.Point(6, 95);
            this.rtbEmailBody.Name = "rtbEmailBody";
            this.rtbEmailBody.Size = new System.Drawing.Size(244, 176);
            this.rtbEmailBody.TabIndex = 0;
            this.rtbEmailBody.Text = "";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(523, 406);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(75, 23);
            this.btnSendEmail.TabIndex = 2223339;
            this.btnSendEmail.Text = "Send Email";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // btnLodingRoles
            // 
            this.btnLodingRoles.Location = new System.Drawing.Point(260, 406);
            this.btnLodingRoles.Name = "btnLodingRoles";
            this.btnLodingRoles.Size = new System.Drawing.Size(144, 23);
            this.btnLodingRoles.TabIndex = 2223340;
            this.btnLodingRoles.Text = "loding roles";
            this.btnLodingRoles.UseVisualStyleBackColor = true;
            // 
            // btnClearRole
            // 
            this.btnClearRole.Location = new System.Drawing.Point(19, 406);
            this.btnClearRole.Name = "btnClearRole";
            this.btnClearRole.Size = new System.Drawing.Size(75, 23);
            this.btnClearRole.TabIndex = 2223341;
            this.btnClearRole.Text = "clear role";
            this.btnClearRole.UseVisualStyleBackColor = true;
            this.btnClearRole.Click += new System.EventHandler(this.btnClearRole_Click);
            // 
            // FormSendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 438);
            this.Controls.Add(this.btnClearRole);
            this.Controls.Add(this.btnLodingRoles);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.gbEmail);
            this.Controls.Add(this.gbZoneWithBelongBig);
            this.Controls.Add(this.gbReceiver);
            this.Controls.Add(this.gbEquip);
            this.Name = "FormSendEmail";
            this.Text = "FormSendEmail";
            this.Controls.SetChildIndex(this.gbEquip, 0);
            this.Controls.SetChildIndex(this.gbReceiver, 0);
            this.Controls.SetChildIndex(this.gbZoneWithBelongBig, 0);
            this.Controls.SetChildIndex(this.gbEmail, 0);
            this.Controls.SetChildIndex(this.btnSendEmail, 0);
            this.Controls.SetChildIndex(this.btnLodingRoles, 0);
            this.Controls.SetChildIndex(this.btnClearRole, 0);
            this.gbEquip.ResumeLayout(false);
            this.gbEquip.PerformLayout();
            this.panelEquip.ResumeLayout(false);
            this.panelEquip.PerformLayout();
            this.gbReceiver.ResumeLayout(false);
            this.gbReceiver.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReceiveRole)).EndInit();
            this.gbZoneWithBelongBig.ResumeLayout(false);
            this.gbZoneWithBelongBig.PerformLayout();
            this.gbEmail.ResumeLayout(false);
            this.gbEmail.PerformLayout();
            this.gbValidTime.ResumeLayout(false);
            this.gbValidTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEquip;
        private System.Windows.Forms.TextBox txtEquipID;
        private System.Windows.Forms.Label lblEquipID;
        private System.Windows.Forms.CheckBox cbHavaEquip;
        private System.Windows.Forms.TextBox txtEquipSizeOf;
        private System.Windows.Forms.Label lblEquipSizeOf;
        private System.Windows.Forms.Panel panelEquip;
        private System.Windows.Forms.GroupBox gbReceiver;
        private System.Windows.Forms.GroupBox gbZoneWithBelongBig;
        private System.Windows.Forms.Label lblZone;
        private System.Windows.Forms.Label lblBigZone;
        private System.Windows.Forms.ComboBox cmbBigZone;
        private System.Windows.Forms.ComboBox cmbZone;
        private System.Windows.Forms.GroupBox gbEmail;
        private System.Windows.Forms.Label lblEmailBody;
        private System.Windows.Forms.Label lblEmailHead;
        private System.Windows.Forms.RichTextBox rtbEmailHead;
        private System.Windows.Forms.RichTextBox rtbEmailBody;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnLodingRoles;
        private System.Windows.Forms.Button btnClearRole;
        private System.Windows.Forms.Label lblDownTemplae;
        private System.Windows.Forms.DataGridView gridReceiveRole;
        private System.Windows.Forms.GroupBox gbValidTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label lblEndTime;
    }
}