namespace GSSClient
{
    partial class FormToolGuserLock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToolGuserLock));
            this.label1 = new System.Windows.Forms.Label();
            this.cboxTime = new System.Windows.Forms.ComboBox();
            this.btnDosure = new GSSUI.AControl.AButton.AButton();
            this.btnDoesc = new GSSUI.AControl.AButton.AButton();
            this.lblUR = new System.Windows.Forms.Label();
            this.rtboxNote = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择封停时间:";
            // 
            // cboxTime
            // 
            this.cboxTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxTime.FormattingEnabled = true;
            this.cboxTime.Location = new System.Drawing.Point(112, 72);
            this.cboxTime.Name = "cboxTime";
            this.cboxTime.Size = new System.Drawing.Size(156, 20);
            this.cboxTime.TabIndex = 4;
            // 
            // btnDosure
            // 
            this.btnDosure.BackColor = System.Drawing.Color.Transparent;
            this.btnDosure.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnDosure.BackImg")));
            this.btnDosure.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDosure.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDosure.Location = new System.Drawing.Point(23, 193);
            this.btnDosure.Name = "btnDosure";
            this.btnDosure.Size = new System.Drawing.Size(95, 23);
            this.btnDosure.TabIndex = 2;
            this.btnDosure.Text =  LanguageResource.Language.BtnSure;
            this.btnDosure.UseVisualStyleBackColor = true;
            this.btnDosure.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDoesc
            // 
            this.btnDoesc.BackColor = System.Drawing.Color.Transparent;
            this.btnDoesc.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnDoesc.BackImg")));
            this.btnDoesc.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDoesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDoesc.Location = new System.Drawing.Point(154, 193);
            this.btnDoesc.Name = "btnDoesc";
            this.btnDoesc.Size = new System.Drawing.Size(95, 23);
            this.btnDoesc.TabIndex = 3;
            this.btnDoesc.Text =  LanguageResource.Language.BtnCancel;
            this.btnDoesc.UseVisualStyleBackColor = true;
            this.btnDoesc.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblUR
            // 
            this.lblUR.AutoSize = true;
            this.lblUR.BackColor = System.Drawing.Color.Transparent;
            this.lblUR.Location = new System.Drawing.Point(12, 53);
            this.lblUR.Name = "lblUR";
            this.lblUR.Size = new System.Drawing.Size(83, 12);
            this.lblUR.TabIndex = 4;
            this.lblUR.Text = "封停帐号\\角色";
            // 
            // rtboxNote
            // 
            this.rtboxNote.Location = new System.Drawing.Point(12, 116);
            this.rtboxNote.Name = "rtboxNote";
            this.rtboxNote.Size = new System.Drawing.Size(256, 71);
            this.rtboxNote.TabIndex = 5;
            this.rtboxNote.Text = global::GSSClient.Properties.Resources.d;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "工具使用备注:";
            // 
            // FormToolGuserLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 248);
            this.Controls.Add(this.cboxTime);
            this.Controls.Add(this.rtboxNote);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDosure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDoesc);
            this.Controls.Add(this.lblUR);
            this.FormSystemBtnSet = GSSUI.AForm.ABaseForm.FormSystemBtn.btn_miniAndbtn_close;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormToolGuserLock";
            this.Text = "封停工具";
            this.Load += new System.EventHandler(this.FormToolGuserLock_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormToolGuserLock_FormClosing);
            this.Controls.SetChildIndex(this.lblUR, 0);
            this.Controls.SetChildIndex(this.btnDoesc, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnDosure, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.rtboxNote, 0);
            this.Controls.SetChildIndex(this.cboxTime, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboxTime;
        private GSSUI.AControl.AButton.AButton btnDosure;
        private GSSUI.AControl.AButton.AButton btnDoesc;
        private System.Windows.Forms.Label lblUR;
        private GSSUI.AControl.ARichTextBox.ARichTextBox rtboxNote;
        private System.Windows.Forms.Label label2;
    }
}