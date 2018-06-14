namespace GSSClient
{
    partial class FormToolGUserUse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToolGUserUse));
            this.rtboxNote = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.btnDosure = new GSSUI.AControl.AButton.AButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDoesc = new GSSUI.AControl.AButton.AButton();
            this.lblUR = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNewPSW = new System.Windows.Forms.TextBox();
            this.textBoxNewPSWConfirm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtboxNote
            // 
            this.rtboxNote.Location = new System.Drawing.Point(12, 148);
            this.rtboxNote.Name = "rtboxNote";
            this.rtboxNote.Size = new System.Drawing.Size(260, 63);
            this.rtboxNote.TabIndex = 12;
            this.rtboxNote.Text = "";
            // 
            // btnDosure
            // 
            this.btnDosure.BackColor = System.Drawing.Color.Transparent;
            this.btnDosure.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnDosure.BackImg")));
            this.btnDosure.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDosure.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDosure.Location = new System.Drawing.Point(30, 217);
            this.btnDosure.Name = "btnDosure";
            this.btnDosure.Size = new System.Drawing.Size(95, 23);
            this.btnDosure.TabIndex = 8;
            this.btnDosure.Text =  LanguageResource.Language.BtnSure;
            this.btnDosure.UseVisualStyleBackColor = true;
            this.btnDosure.Click += new System.EventHandler(this.btnDosure_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(10, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "工具使用备注:";
            // 
            // btnDoesc
            // 
            this.btnDoesc.BackColor = System.Drawing.Color.Transparent;
            this.btnDoesc.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnDoesc.BackImg")));
            this.btnDoesc.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDoesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDoesc.Location = new System.Drawing.Point(156, 217);
            this.btnDoesc.Name = "btnDoesc";
            this.btnDoesc.Size = new System.Drawing.Size(95, 23);
            this.btnDoesc.TabIndex = 9;
            this.btnDoesc.Text =  LanguageResource.Language.BtnCancel;
            this.btnDoesc.UseVisualStyleBackColor = true;
            this.btnDoesc.Click += new System.EventHandler(this.btnDoesc_Click);
            // 
            // lblUR
            // 
            this.lblUR.AutoSize = true;
            this.lblUR.BackColor = System.Drawing.Color.Transparent;
            this.lblUR.Location = new System.Drawing.Point(11, 56);
            this.lblUR.Name = "lblUR";
            this.lblUR.Size = new System.Drawing.Size(59, 12);
            this.lblUR.TabIndex = 11;
            this.lblUR.Text = "帐号\\角色";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(11, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "新密码";
            // 
            // textBoxNewPSW
            // 
            this.textBoxNewPSW.Location = new System.Drawing.Point(72, 75);
            this.textBoxNewPSW.Name = "textBoxNewPSW";
            this.textBoxNewPSW.Size = new System.Drawing.Size(200, 21);
            this.textBoxNewPSW.TabIndex = 15;
            this.textBoxNewPSW.UseSystemPasswordChar = true;
            // 
            // textBoxNewPSWConfirm
            // 
            this.textBoxNewPSWConfirm.Location = new System.Drawing.Point(72, 107);
            this.textBoxNewPSWConfirm.Name = "textBoxNewPSWConfirm";
            this.textBoxNewPSWConfirm.Size = new System.Drawing.Size(200, 21);
            this.textBoxNewPSWConfirm.TabIndex = 17;
            this.textBoxNewPSWConfirm.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(11, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "密码确认";
            // 
            // FormToolGUserUse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 271);
            this.Controls.Add(this.textBoxNewPSWConfirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxNewPSW);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtboxNote);
            this.Controls.Add(this.btnDosure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDoesc);
            this.Controls.Add(this.lblUR);
            this.FormSystemBtnSet = GSSUI.AForm.ABaseForm.FormSystemBtn.btn_miniAndbtn_close;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormToolGUserUse";
            this.Text = "借用帐号工具";
            this.Load += new System.EventHandler(this.FormToolGuserUnLock_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormToolGUserUse_FormClosing);
            this.Controls.SetChildIndex(this.lblUR, 0);
            this.Controls.SetChildIndex(this.btnDoesc, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnDosure, 0);
            this.Controls.SetChildIndex(this.rtboxNote, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.textBoxNewPSW, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.textBoxNewPSWConfirm, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GSSUI.AControl.ARichTextBox.ARichTextBox rtboxNote;
        private GSSUI.AControl.AButton.AButton btnDosure;
        private System.Windows.Forms.Label label2;
        private GSSUI.AControl.AButton.AButton btnDoesc;
        private System.Windows.Forms.Label lblUR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNewPSW;
        private System.Windows.Forms.TextBox textBoxNewPSWConfirm;
        private System.Windows.Forms.Label label3;
    }
}