namespace GSSServer.GSSManage
{
    partial class FormDepartmentAdd
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
            System.Windows.Forms.Label f_DepartIDLabel;
            System.Windows.Forms.Label f_ParentIDLabel;
            System.Windows.Forms.Label f_DepartNameLabel;
            System.Windows.Forms.Label f_NoteLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDepartmentAdd));
            this.f_DepartIDTextBox = new System.Windows.Forms.TextBox();
            this.f_ParentIDComboBox = new System.Windows.Forms.ComboBox();
            this.f_DepartNameTextBox = new System.Windows.Forms.TextBox();
            this.buttonESC = new System.Windows.Forms.Button();
            this.buttonSure = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.f_NoterichTextBox = new System.Windows.Forms.RichTextBox();
            f_DepartIDLabel = new System.Windows.Forms.Label();
            f_ParentIDLabel = new System.Windows.Forms.Label();
            f_DepartNameLabel = new System.Windows.Forms.Label();
            f_NoteLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // f_DepartIDLabel
            // 
            f_DepartIDLabel.AutoSize = true;
            f_DepartIDLabel.Location = new System.Drawing.Point(10, 37);
            f_DepartIDLabel.Name = "f_DepartIDLabel";
            f_DepartIDLabel.Size = new System.Drawing.Size(59, 12);
            f_DepartIDLabel.TabIndex = 1;
            f_DepartIDLabel.Text = "部门编号:";
            // 
            // f_ParentIDLabel
            // 
            f_ParentIDLabel.AutoSize = true;
            f_ParentIDLabel.Location = new System.Drawing.Point(10, 64);
            f_ParentIDLabel.Name = "f_ParentIDLabel";
            f_ParentIDLabel.Size = new System.Drawing.Size(59, 12);
            f_ParentIDLabel.TabIndex = 3;
            f_ParentIDLabel.Text = "上级部门:";
            // 
            // f_DepartNameLabel
            // 
            f_DepartNameLabel.AutoSize = true;
            f_DepartNameLabel.Location = new System.Drawing.Point(10, 90);
            f_DepartNameLabel.Name = "f_DepartNameLabel";
            f_DepartNameLabel.Size = new System.Drawing.Size(59, 12);
            f_DepartNameLabel.TabIndex = 5;
            f_DepartNameLabel.Text = "部门名称:";
            // 
            // f_NoteLabel
            // 
            f_NoteLabel.AutoSize = true;
            f_NoteLabel.Location = new System.Drawing.Point(240, 17);
            f_NoteLabel.Name = "f_NoteLabel";
            f_NoteLabel.Size = new System.Drawing.Size(59, 12);
            f_NoteLabel.TabIndex = 7;
            f_NoteLabel.Text = "部门说明:";
            // 
            // f_DepartIDTextBox
            // 
            this.f_DepartIDTextBox.Enabled = false;
            this.f_DepartIDTextBox.Location = new System.Drawing.Point(83, 34);
            this.f_DepartIDTextBox.Name = "f_DepartIDTextBox";
            this.f_DepartIDTextBox.Size = new System.Drawing.Size(145, 21);
            this.f_DepartIDTextBox.TabIndex = 2;
            this.f_DepartIDTextBox.Text = "0";
            // 
            // f_ParentIDComboBox
            // 
            this.f_ParentIDComboBox.FormattingEnabled = true;
            this.f_ParentIDComboBox.Location = new System.Drawing.Point(83, 61);
            this.f_ParentIDComboBox.Name = "f_ParentIDComboBox";
            this.f_ParentIDComboBox.Size = new System.Drawing.Size(145, 20);
            this.f_ParentIDComboBox.TabIndex = 4;
            // 
            // f_DepartNameTextBox
            // 
            this.f_DepartNameTextBox.Location = new System.Drawing.Point(83, 87);
            this.f_DepartNameTextBox.Name = "f_DepartNameTextBox";
            this.f_DepartNameTextBox.Size = new System.Drawing.Size(145, 21);
            this.f_DepartNameTextBox.TabIndex = 6;
            // 
            // buttonESC
            // 
            this.buttonESC.Location = new System.Drawing.Point(137, 114);
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.Size = new System.Drawing.Size(75, 23);
            this.buttonESC.TabIndex = 34;
            this.buttonESC.Text = "取 消";
            this.buttonESC.UseVisualStyleBackColor = true;
            this.buttonESC.Click += new System.EventHandler(this.buttonESC_Click);
            // 
            // buttonSure
            // 
            this.buttonSure.Location = new System.Drawing.Point(32, 114);
            this.buttonSure.Name = "buttonSure";
            this.buttonSure.Size = new System.Drawing.Size(75, 23);
            this.buttonSure.TabIndex = 0;
            this.buttonSure.Text = "确 定";
            this.buttonSure.UseVisualStyleBackColor = true;
            this.buttonSure.Click += new System.EventHandler(this.buttonSure_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.f_NoterichTextBox);
            this.groupBox1.Controls.Add(f_DepartIDLabel);
            this.groupBox1.Controls.Add(this.buttonESC);
            this.groupBox1.Controls.Add(this.buttonSure);
            this.groupBox1.Controls.Add(f_NoteLabel);
            this.groupBox1.Controls.Add(this.f_DepartNameTextBox);
            this.groupBox1.Controls.Add(this.f_DepartIDTextBox);
            this.groupBox1.Controls.Add(f_DepartNameLabel);
            this.groupBox1.Controls.Add(f_ParentIDLabel);
            this.groupBox1.Controls.Add(this.f_ParentIDComboBox);
            this.groupBox1.Location = new System.Drawing.Point(7, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 165);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(7, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "注:顶级单位不需要选择上级部门";
            // 
            // f_NoterichTextBox
            // 
            this.f_NoterichTextBox.Location = new System.Drawing.Point(242, 32);
            this.f_NoterichTextBox.Name = "f_NoterichTextBox";
            this.f_NoterichTextBox.Size = new System.Drawing.Size(185, 113);
            this.f_NoterichTextBox.TabIndex = 35;
            this.f_NoterichTextBox.Text = "";
            // 
            // FormDepartmentAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 170);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormDepartmentAdd";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "部门添加";
            this.Load += new System.EventHandler(this.FormDepartmentAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox f_DepartIDTextBox;
        private System.Windows.Forms.ComboBox f_ParentIDComboBox;
        private System.Windows.Forms.TextBox f_DepartNameTextBox;
        private System.Windows.Forms.Button buttonESC;
        private System.Windows.Forms.Button buttonSure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox f_NoterichTextBox;
        private System.Windows.Forms.Label label1;
    }
}