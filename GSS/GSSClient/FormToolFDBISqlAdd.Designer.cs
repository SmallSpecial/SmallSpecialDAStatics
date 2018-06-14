namespace GSSClient
{
    partial class FormToolFDBISqlAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToolFDBISqlAdd));
            this.btnDosure = new GSSUI.AControl.AButton.AButton();
            this.btnDoesc = new GSSUI.AControl.AButton.AButton();
            this.label7 = new System.Windows.Forms.Label();
            this.tboxTitle = new System.Windows.Forms.TextBox();
            this.tboxSql = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tboxNote = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.rbtnPublic = new System.Windows.Forms.RadioButton();
            this.rbtnSelf = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnDosure
            // 
            this.btnDosure.BackColor = System.Drawing.Color.Transparent;
            this.btnDosure.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnDosure.BackImg")));
            this.btnDosure.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDosure.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDosure.Location = new System.Drawing.Point(56, 289);
            this.btnDosure.Name = "btnDosure";
            this.btnDosure.Size = new System.Drawing.Size(100, 26);
            this.btnDosure.TabIndex = 5;
            this.btnDosure.Text =  LanguageResource.Language.BtnSure;
            this.btnDosure.UseVisualStyleBackColor = false;
            this.btnDosure.Click += new System.EventHandler(this.btnDosure_Click);
            // 
            // btnDoesc
            // 
            this.btnDoesc.BackColor = System.Drawing.Color.Transparent;
            this.btnDoesc.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnDoesc.BackImg")));
            this.btnDoesc.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDoesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDoesc.Location = new System.Drawing.Point(207, 289);
            this.btnDoesc.Name = "btnDoesc";
            this.btnDoesc.Size = new System.Drawing.Size(100, 26);
            this.btnDoesc.TabIndex = 6;
            this.btnDoesc.Text =  LanguageResource.Language.BtnCancel;
            this.btnDoesc.UseVisualStyleBackColor = false;
            this.btnDoesc.Click += new System.EventHandler(this.btnDoesc_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(18, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 10;
            this.label7.Text =LanguageResource.Language.LblTitle+":";
            // 
            // tboxTitle
            // 
            this.tboxTitle.Location = new System.Drawing.Point(56, 65);
            this.tboxTitle.MaxLength = 50;
            this.tboxTitle.Name = "tboxTitle";
            this.tboxTitle.Size = new System.Drawing.Size(289, 21);
            this.tboxTitle.TabIndex = 11;
            // 
            // tboxSql
            // 
            this.tboxSql.BackColor = System.Drawing.SystemColors.Window;
            this.tboxSql.Location = new System.Drawing.Point(56, 95);
            this.tboxSql.MaxLength = 500;
            this.tboxSql.Name = "tboxSql";
            this.tboxSql.Size = new System.Drawing.Size(289, 90);
            this.tboxSql.TabIndex = 12;
            this.tboxSql.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(18, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "命令:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(15, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "属性:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(15, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "说明:";
            // 
            // tboxNote
            // 
            this.tboxNote.BackColor = System.Drawing.SystemColors.Window;
            this.tboxNote.Location = new System.Drawing.Point(56, 191);
            this.tboxNote.MaxLength = 500;
            this.tboxNote.Name = "tboxNote";
            this.tboxNote.Size = new System.Drawing.Size(289, 66);
            this.tboxNote.TabIndex = 16;
            this.tboxNote.Text = "";
            // 
            // rbtnPublic
            // 
            this.rbtnPublic.AutoSize = true;
            this.rbtnPublic.BackColor = System.Drawing.Color.Transparent;
            this.rbtnPublic.Checked = true;
            this.rbtnPublic.Location = new System.Drawing.Point(71, 263);
            this.rbtnPublic.Name = "rbtnPublic";
            this.rbtnPublic.Size = new System.Drawing.Size(47, 16);
            this.rbtnPublic.TabIndex = 17;
            this.rbtnPublic.TabStop = true;
            this.rbtnPublic.Text = "公开";
            this.rbtnPublic.UseVisualStyleBackColor = false;
            // 
            // rbtnSelf
            // 
            this.rbtnSelf.AutoSize = true;
            this.rbtnSelf.BackColor = System.Drawing.Color.Transparent;
            this.rbtnSelf.Location = new System.Drawing.Point(148, 263);
            this.rbtnSelf.Name = "rbtnSelf";
            this.rbtnSelf.Size = new System.Drawing.Size(47, 16);
            this.rbtnSelf.TabIndex = 18;
            this.rbtnSelf.Text = "私人";
            this.rbtnSelf.UseVisualStyleBackColor = false;
            // 
            // FormToolFDBISqlAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 354);
            this.Controls.Add(this.rbtnSelf);
            this.Controls.Add(this.rbtnPublic);
            this.Controls.Add(this.tboxNote);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tboxSql);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tboxTitle);
            this.Controls.Add(this.btnDoesc);
            this.Controls.Add(this.btnDosure);
            this.FormSystemBtnSet = GSSUI.AForm.ABaseForm.FormSystemBtn.btn_miniAndbtn_close;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormToolFDBISqlAdd";
            this.Text = "查询模板添加";
            this.Load += new System.EventHandler(this.FormToolFDBISqlAdd_Load);
            this.Controls.SetChildIndex(this.btnDosure, 0);
            this.Controls.SetChildIndex(this.btnDoesc, 0);
            this.Controls.SetChildIndex(this.tboxTitle, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.tboxSql, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.tboxNote, 0);
            this.Controls.SetChildIndex(this.rbtnPublic, 0);
            this.Controls.SetChildIndex(this.rbtnSelf, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GSSUI.AControl.AButton.AButton btnDosure;
        private GSSUI.AControl.AButton.AButton btnDoesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tboxTitle;
        private GSSUI.AControl.ARichTextBox.ARichTextBox tboxSql;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private GSSUI.AControl.ARichTextBox.ARichTextBox tboxNote;
        private System.Windows.Forms.RadioButton rbtnPublic;
        private System.Windows.Forms.RadioButton rbtnSelf;
    }
}