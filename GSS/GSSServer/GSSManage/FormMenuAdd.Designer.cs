namespace GSSServer.GSSManage
{
    partial class FormMenuAdd
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
            System.Windows.Forms.Label f_MenuIDLabel;
            System.Windows.Forms.Label f_NameLabel;
            System.Windows.Forms.Label f_FormNameLabel;
            System.Windows.Forms.Label f_ParentIDLabel;
            System.Windows.Forms.Label f_SortLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenuAdd));
            this.f_MenuIDTextBox = new System.Windows.Forms.TextBox();
            this.f_NameTextBox = new System.Windows.Forms.TextBox();
            this.f_FormNameTextBox = new System.Windows.Forms.TextBox();
            this.f_ParentIDTextBox = new System.Windows.Forms.TextBox();
            this.f_IsUsedCheckBox = new System.Windows.Forms.CheckBox();
            this.f_SortTextBox = new System.Windows.Forms.TextBox();
            this.buttonESC = new System.Windows.Forms.Button();
            this.buttonSure = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            f_MenuIDLabel = new System.Windows.Forms.Label();
            f_NameLabel = new System.Windows.Forms.Label();
            f_FormNameLabel = new System.Windows.Forms.Label();
            f_ParentIDLabel = new System.Windows.Forms.Label();
            f_SortLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // f_MenuIDLabel
            // 
            f_MenuIDLabel.AutoSize = true;
            f_MenuIDLabel.Location = new System.Drawing.Point(22, 26);
            f_MenuIDLabel.Name = "f_MenuIDLabel";
            f_MenuIDLabel.Size = new System.Drawing.Size(59, 12);
            f_MenuIDLabel.TabIndex = 1;
            f_MenuIDLabel.Text = "菜单编号:";
            // 
            // f_NameLabel
            // 
            f_NameLabel.AutoSize = true;
            f_NameLabel.Location = new System.Drawing.Point(22, 53);
            f_NameLabel.Name = "f_NameLabel";
            f_NameLabel.Size = new System.Drawing.Size(59, 12);
            f_NameLabel.TabIndex = 3;
            f_NameLabel.Text = "菜单名称:";
            // 
            // f_FormNameLabel
            // 
            f_FormNameLabel.AutoSize = true;
            f_FormNameLabel.Location = new System.Drawing.Point(22, 80);
            f_FormNameLabel.Name = "f_FormNameLabel";
            f_FormNameLabel.Size = new System.Drawing.Size(59, 12);
            f_FormNameLabel.TabIndex = 5;
            f_FormNameLabel.Text = "窗体名称:";
            // 
            // f_ParentIDLabel
            // 
            f_ParentIDLabel.AutoSize = true;
            f_ParentIDLabel.Location = new System.Drawing.Point(22, 107);
            f_ParentIDLabel.Name = "f_ParentIDLabel";
            f_ParentIDLabel.Size = new System.Drawing.Size(59, 12);
            f_ParentIDLabel.TabIndex = 7;
            f_ParentIDLabel.Text = "上级编号:";
            // 
            // f_SortLabel
            // 
            f_SortLabel.AutoSize = true;
            f_SortLabel.Location = new System.Drawing.Point(22, 134);
            f_SortLabel.Name = "f_SortLabel";
            f_SortLabel.Size = new System.Drawing.Size(35, 12);
            f_SortLabel.TabIndex = 11;
            f_SortLabel.Text = "排序:";
            // 
            // f_MenuIDTextBox
            // 
            this.f_MenuIDTextBox.Location = new System.Drawing.Point(105, 23);
            this.f_MenuIDTextBox.Name = "f_MenuIDTextBox";
            this.f_MenuIDTextBox.Size = new System.Drawing.Size(169, 21);
            this.f_MenuIDTextBox.TabIndex = 2;
            // 
            // f_NameTextBox
            // 
            this.f_NameTextBox.Location = new System.Drawing.Point(105, 50);
            this.f_NameTextBox.Name = "f_NameTextBox";
            this.f_NameTextBox.Size = new System.Drawing.Size(169, 21);
            this.f_NameTextBox.TabIndex = 4;
            // 
            // f_FormNameTextBox
            // 
            this.f_FormNameTextBox.Location = new System.Drawing.Point(105, 77);
            this.f_FormNameTextBox.Name = "f_FormNameTextBox";
            this.f_FormNameTextBox.Size = new System.Drawing.Size(169, 21);
            this.f_FormNameTextBox.TabIndex = 6;
            // 
            // f_ParentIDTextBox
            // 
            this.f_ParentIDTextBox.Location = new System.Drawing.Point(105, 104);
            this.f_ParentIDTextBox.Name = "f_ParentIDTextBox";
            this.f_ParentIDTextBox.Size = new System.Drawing.Size(169, 21);
            this.f_ParentIDTextBox.TabIndex = 8;
            // 
            // f_IsUsedCheckBox
            // 
            this.f_IsUsedCheckBox.Checked = true;
            this.f_IsUsedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.f_IsUsedCheckBox.Location = new System.Drawing.Point(190, 129);
            this.f_IsUsedCheckBox.Name = "f_IsUsedCheckBox";
            this.f_IsUsedCheckBox.Size = new System.Drawing.Size(84, 24);
            this.f_IsUsedCheckBox.TabIndex = 10;
            this.f_IsUsedCheckBox.Text = "是否启用";
            this.f_IsUsedCheckBox.UseVisualStyleBackColor = true;
            // 
            // f_SortTextBox
            // 
            this.f_SortTextBox.Location = new System.Drawing.Point(105, 131);
            this.f_SortTextBox.Name = "f_SortTextBox";
            this.f_SortTextBox.Size = new System.Drawing.Size(70, 21);
            this.f_SortTextBox.TabIndex = 12;
            this.f_SortTextBox.Text = "0";
            // 
            // buttonESC
            // 
            this.buttonESC.Location = new System.Drawing.Point(179, 164);
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.Size = new System.Drawing.Size(75, 23);
            this.buttonESC.TabIndex = 34;
            this.buttonESC.Text = "取 消";
            this.buttonESC.UseVisualStyleBackColor = true;
            this.buttonESC.Click += new System.EventHandler(this.buttonESC_Click);
            // 
            // buttonSure
            // 
            this.buttonSure.Location = new System.Drawing.Point(40, 164);
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
            this.groupBox1.Controls.Add(f_MenuIDLabel);
            this.groupBox1.Controls.Add(this.buttonESC);
            this.groupBox1.Controls.Add(this.f_SortTextBox);
            this.groupBox1.Controls.Add(this.buttonSure);
            this.groupBox1.Controls.Add(f_SortLabel);
            this.groupBox1.Controls.Add(this.f_IsUsedCheckBox);
            this.groupBox1.Controls.Add(this.f_MenuIDTextBox);
            this.groupBox1.Controls.Add(this.f_ParentIDTextBox);
            this.groupBox1.Controls.Add(f_NameLabel);
            this.groupBox1.Controls.Add(f_ParentIDLabel);
            this.groupBox1.Controls.Add(this.f_NameTextBox);
            this.groupBox1.Controls.Add(this.f_FormNameTextBox);
            this.groupBox1.Controls.Add(f_FormNameLabel);
            this.groupBox1.Location = new System.Drawing.Point(8, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 216);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(146, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "注:系统使用后请慎重修改";
            // 
            // FormMenuAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 223);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMenuAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "菜单添加";
            this.Load += new System.EventHandler(this.FormMenuAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox f_MenuIDTextBox;
        private System.Windows.Forms.TextBox f_NameTextBox;
        private System.Windows.Forms.TextBox f_FormNameTextBox;
        private System.Windows.Forms.TextBox f_ParentIDTextBox;
        private System.Windows.Forms.CheckBox f_IsUsedCheckBox;
        private System.Windows.Forms.TextBox f_SortTextBox;
        private System.Windows.Forms.Button buttonESC;
        private System.Windows.Forms.Button buttonSure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}