namespace GSSServer.GSSManage
{
    partial class FormDictionaryAdd
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
            System.Windows.Forms.Label f_DicIDLabel;
            System.Windows.Forms.Label f_ParentIDLabel;
            System.Windows.Forms.Label f_ValueLabel;
            System.Windows.Forms.Label f_SortLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDictionaryAdd));
            this.f_DicIDTextBox = new System.Windows.Forms.TextBox();
            this.f_ValueTextBox = new System.Windows.Forms.TextBox();
            this.f_IsUsedCheckBox = new System.Windows.Forms.CheckBox();
            this.f_SortTextBox = new System.Windows.Forms.TextBox();
            this.buttonESC = new System.Windows.Forms.Button();
            this.buttonSure = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.f_ParentIDTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            f_DicIDLabel = new System.Windows.Forms.Label();
            f_ParentIDLabel = new System.Windows.Forms.Label();
            f_ValueLabel = new System.Windows.Forms.Label();
            f_SortLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // f_DicIDLabel
            // 
            f_DicIDLabel.AutoSize = true;
            f_DicIDLabel.Location = new System.Drawing.Point(17, 24);
            f_DicIDLabel.Name = "f_DicIDLabel";
            f_DicIDLabel.Size = new System.Drawing.Size(59, 12);
            f_DicIDLabel.TabIndex = 1;
            f_DicIDLabel.Text = "字典编号:";
            // 
            // f_ParentIDLabel
            // 
            f_ParentIDLabel.AutoSize = true;
            f_ParentIDLabel.Location = new System.Drawing.Point(17, 51);
            f_ParentIDLabel.Name = "f_ParentIDLabel";
            f_ParentIDLabel.Size = new System.Drawing.Size(59, 12);
            f_ParentIDLabel.TabIndex = 3;
            f_ParentIDLabel.Text = "上级字典:";
            // 
            // f_ValueLabel
            // 
            f_ValueLabel.AutoSize = true;
            f_ValueLabel.Location = new System.Drawing.Point(17, 78);
            f_ValueLabel.Name = "f_ValueLabel";
            f_ValueLabel.Size = new System.Drawing.Size(47, 12);
            f_ValueLabel.TabIndex = 5;
            f_ValueLabel.Text = "字典值:";
            // 
            // f_SortLabel
            // 
            f_SortLabel.AutoSize = true;
            f_SortLabel.Location = new System.Drawing.Point(17, 105);
            f_SortLabel.Name = "f_SortLabel";
            f_SortLabel.Size = new System.Drawing.Size(35, 12);
            f_SortLabel.TabIndex = 9;
            f_SortLabel.Text = "排序:";
            // 
            // f_DicIDTextBox
            // 
            this.f_DicIDTextBox.Location = new System.Drawing.Point(100, 21);
            this.f_DicIDTextBox.Name = "f_DicIDTextBox";
            this.f_DicIDTextBox.Size = new System.Drawing.Size(167, 21);
            this.f_DicIDTextBox.TabIndex = 2;
            // 
            // f_ValueTextBox
            // 
            this.f_ValueTextBox.Location = new System.Drawing.Point(100, 75);
            this.f_ValueTextBox.Name = "f_ValueTextBox";
            this.f_ValueTextBox.Size = new System.Drawing.Size(167, 21);
            this.f_ValueTextBox.TabIndex = 6;
            // 
            // f_IsUsedCheckBox
            // 
            this.f_IsUsedCheckBox.Checked = true;
            this.f_IsUsedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.f_IsUsedCheckBox.Location = new System.Drawing.Point(20, 125);
            this.f_IsUsedCheckBox.Name = "f_IsUsedCheckBox";
            this.f_IsUsedCheckBox.Size = new System.Drawing.Size(104, 20);
            this.f_IsUsedCheckBox.TabIndex = 8;
            this.f_IsUsedCheckBox.Text = "是否启用";
            this.f_IsUsedCheckBox.UseVisualStyleBackColor = true;
            // 
            // f_SortTextBox
            // 
            this.f_SortTextBox.Location = new System.Drawing.Point(100, 102);
            this.f_SortTextBox.Name = "f_SortTextBox";
            this.f_SortTextBox.Size = new System.Drawing.Size(167, 21);
            this.f_SortTextBox.TabIndex = 10;
            this.f_SortTextBox.Text = "0";
            // 
            // buttonESC
            // 
            this.buttonESC.Location = new System.Drawing.Point(170, 148);
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.Size = new System.Drawing.Size(75, 23);
            this.buttonESC.TabIndex = 34;
            this.buttonESC.Text = "取 消";
            this.buttonESC.UseVisualStyleBackColor = true;
            this.buttonESC.Click += new System.EventHandler(this.buttonESC_Click);
            // 
            // buttonSure
            // 
            this.buttonSure.Location = new System.Drawing.Point(35, 148);
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
            this.groupBox1.Controls.Add(f_DicIDLabel);
            this.groupBox1.Controls.Add(this.buttonESC);
            this.groupBox1.Controls.Add(this.f_SortTextBox);
            this.groupBox1.Controls.Add(this.buttonSure);
            this.groupBox1.Controls.Add(f_SortLabel);
            this.groupBox1.Controls.Add(this.f_IsUsedCheckBox);
            this.groupBox1.Controls.Add(this.f_DicIDTextBox);
            this.groupBox1.Controls.Add(this.f_ValueTextBox);
            this.groupBox1.Controls.Add(f_ParentIDLabel);
            this.groupBox1.Controls.Add(f_ValueLabel);
            this.groupBox1.Controls.Add(this.f_ParentIDTextBox);
            this.groupBox1.Location = new System.Drawing.Point(8, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 191);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // f_ParentIDTextBox
            // 
            this.f_ParentIDTextBox.Location = new System.Drawing.Point(100, 48);
            this.f_ParentIDTextBox.Name = "f_ParentIDTextBox";
            this.f_ParentIDTextBox.Size = new System.Drawing.Size(167, 21);
            this.f_ParentIDTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(134, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "注:系统使用后请慎重修改";
            // 
            // FormDictionaryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormDictionaryAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字典添加";
            this.Load += new System.EventHandler(this.FormDictionaryAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox f_DicIDTextBox;
        private System.Windows.Forms.TextBox f_ValueTextBox;
        private System.Windows.Forms.CheckBox f_IsUsedCheckBox;
        private System.Windows.Forms.TextBox f_SortTextBox;
        private System.Windows.Forms.Button buttonESC;
        private System.Windows.Forms.Button buttonSure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox f_ParentIDTextBox;
        private System.Windows.Forms.Label label1;

    }
}