namespace GSSServer.GSSManage
{
    partial class FormGameConfigAdd
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
            System.Windows.Forms.Label f_IDLabel;
            System.Windows.Forms.Label f_ParentIDLabel;
            System.Windows.Forms.Label f_NameLabel;
            System.Windows.Forms.Label f_ValueLabel;
            System.Windows.Forms.Label f_ValueGameLabel;
            System.Windows.Forms.Label f_SortLabel;
            System.Windows.Forms.Label f_Value1Label;
            this.f_IDTextBox = new System.Windows.Forms.TextBox();
            this.f_ParentIDTextBox = new System.Windows.Forms.TextBox();
            this.f_NameTextBox = new System.Windows.Forms.TextBox();
            this.f_ValueTextBox = new System.Windows.Forms.TextBox();
            this.f_IsUsedCheckBox = new System.Windows.Forms.CheckBox();
            this.f_SortTextBox = new System.Windows.Forms.TextBox();
            this.buttonESC = new System.Windows.Forms.Button();
            this.buttonSure = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.f_ValueGamerichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.f_Value1TextBox = new System.Windows.Forms.TextBox();
            f_IDLabel = new System.Windows.Forms.Label();
            f_ParentIDLabel = new System.Windows.Forms.Label();
            f_NameLabel = new System.Windows.Forms.Label();
            f_ValueLabel = new System.Windows.Forms.Label();
            f_ValueGameLabel = new System.Windows.Forms.Label();
            f_SortLabel = new System.Windows.Forms.Label();
            f_Value1Label = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // f_IDLabel
            // 
            f_IDLabel.AutoSize = true;
            f_IDLabel.Location = new System.Drawing.Point(19, 25);
            f_IDLabel.Name = "f_IDLabel";
            f_IDLabel.Size = new System.Drawing.Size(35, 12);
            f_IDLabel.TabIndex = 1;
            f_IDLabel.Text = "编号:";
            // 
            // f_ParentIDLabel
            // 
            f_ParentIDLabel.AutoSize = true;
            f_ParentIDLabel.Location = new System.Drawing.Point(19, 52);
            f_ParentIDLabel.Name = "f_ParentIDLabel";
            f_ParentIDLabel.Size = new System.Drawing.Size(47, 12);
            f_ParentIDLabel.TabIndex = 3;
            f_ParentIDLabel.Text = "上级项:";
            // 
            // f_NameLabel
            // 
            f_NameLabel.AutoSize = true;
            f_NameLabel.Location = new System.Drawing.Point(19, 79);
            f_NameLabel.Name = "f_NameLabel";
            f_NameLabel.Size = new System.Drawing.Size(47, 12);
            f_NameLabel.TabIndex = 5;
            f_NameLabel.Text = "项名称:";
            // 
            // f_ValueLabel
            // 
            f_ValueLabel.AutoSize = true;
            f_ValueLabel.Location = new System.Drawing.Point(19, 106);
            f_ValueLabel.Name = "f_ValueLabel";
            f_ValueLabel.Size = new System.Drawing.Size(47, 12);
            f_ValueLabel.TabIndex = 7;
            f_ValueLabel.Text = "项内容:";
            // 
            // f_ValueGameLabel
            // 
            f_ValueGameLabel.AutoSize = true;
            f_ValueGameLabel.Location = new System.Drawing.Point(19, 161);
            f_ValueGameLabel.Name = "f_ValueGameLabel";
            f_ValueGameLabel.Size = new System.Drawing.Size(71, 12);
            f_ValueGameLabel.TabIndex = 9;
            f_ValueGameLabel.Text = "游戏相关值:";
            // 
            // f_SortLabel
            // 
            f_SortLabel.AutoSize = true;
            f_SortLabel.Location = new System.Drawing.Point(19, 248);
            f_SortLabel.Name = "f_SortLabel";
            f_SortLabel.Size = new System.Drawing.Size(35, 12);
            f_SortLabel.TabIndex = 13;
            f_SortLabel.Text = "排序:";
            // 
            // f_IDTextBox
            // 
            this.f_IDTextBox.Location = new System.Drawing.Point(108, 22);
            this.f_IDTextBox.Name = "f_IDTextBox";
            this.f_IDTextBox.Size = new System.Drawing.Size(214, 21);
            this.f_IDTextBox.TabIndex = 2;
            // 
            // f_ParentIDTextBox
            // 
            this.f_ParentIDTextBox.Location = new System.Drawing.Point(108, 49);
            this.f_ParentIDTextBox.Name = "f_ParentIDTextBox";
            this.f_ParentIDTextBox.Size = new System.Drawing.Size(214, 21);
            this.f_ParentIDTextBox.TabIndex = 4;
            // 
            // f_NameTextBox
            // 
            this.f_NameTextBox.Location = new System.Drawing.Point(108, 76);
            this.f_NameTextBox.Name = "f_NameTextBox";
            this.f_NameTextBox.Size = new System.Drawing.Size(214, 21);
            this.f_NameTextBox.TabIndex = 6;
            // 
            // f_ValueTextBox
            // 
            this.f_ValueTextBox.Location = new System.Drawing.Point(108, 103);
            this.f_ValueTextBox.Name = "f_ValueTextBox";
            this.f_ValueTextBox.Size = new System.Drawing.Size(214, 21);
            this.f_ValueTextBox.TabIndex = 8;
            // 
            // f_IsUsedCheckBox
            // 
            this.f_IsUsedCheckBox.Checked = true;
            this.f_IsUsedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.f_IsUsedCheckBox.Location = new System.Drawing.Point(219, 248);
            this.f_IsUsedCheckBox.Name = "f_IsUsedCheckBox";
            this.f_IsUsedCheckBox.Size = new System.Drawing.Size(76, 21);
            this.f_IsUsedCheckBox.TabIndex = 12;
            this.f_IsUsedCheckBox.Text = "是否启用";
            this.f_IsUsedCheckBox.UseVisualStyleBackColor = true;
            // 
            // f_SortTextBox
            // 
            this.f_SortTextBox.Location = new System.Drawing.Point(108, 245);
            this.f_SortTextBox.Name = "f_SortTextBox";
            this.f_SortTextBox.Size = new System.Drawing.Size(96, 21);
            this.f_SortTextBox.TabIndex = 14;
            this.f_SortTextBox.Text = "0";
            // 
            // buttonESC
            // 
            this.buttonESC.Location = new System.Drawing.Point(219, 287);
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.Size = new System.Drawing.Size(75, 23);
            this.buttonESC.TabIndex = 34;
            this.buttonESC.Text = "取 消";
            this.buttonESC.UseVisualStyleBackColor = true;
            this.buttonESC.Click += new System.EventHandler(this.buttonESC_Click);
            // 
            // buttonSure
            // 
            this.buttonSure.Location = new System.Drawing.Point(56, 287);
            this.buttonSure.Name = "buttonSure";
            this.buttonSure.Size = new System.Drawing.Size(75, 23);
            this.buttonSure.TabIndex = 0;
            this.buttonSure.Text = "确 定";
            this.buttonSure.UseVisualStyleBackColor = true;
            this.buttonSure.Click += new System.EventHandler(this.buttonSure_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.f_ValueGamerichTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(f_IDLabel);
            this.groupBox1.Controls.Add(this.buttonESC);
            this.groupBox1.Controls.Add(this.f_SortTextBox);
            this.groupBox1.Controls.Add(this.buttonSure);
            this.groupBox1.Controls.Add(f_SortLabel);
            this.groupBox1.Controls.Add(this.f_IsUsedCheckBox);
            this.groupBox1.Controls.Add(this.f_IDTextBox);
            this.groupBox1.Controls.Add(f_ParentIDLabel);
            this.groupBox1.Controls.Add(f_ValueGameLabel);
            this.groupBox1.Controls.Add(this.f_ParentIDTextBox);
            this.groupBox1.Controls.Add(this.f_Value1TextBox);
            this.groupBox1.Controls.Add(this.f_ValueTextBox);
            this.groupBox1.Controls.Add(f_Value1Label);
            this.groupBox1.Controls.Add(f_NameLabel);
            this.groupBox1.Controls.Add(f_ValueLabel);
            this.groupBox1.Controls.Add(this.f_NameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(8, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 340);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // f_ValueGamerichTextBox
            // 
            this.f_ValueGamerichTextBox.Location = new System.Drawing.Point(108, 161);
            this.f_ValueGamerichTextBox.Name = "f_ValueGamerichTextBox";
            this.f_ValueGamerichTextBox.Size = new System.Drawing.Size(214, 78);
            this.f_ValueGamerichTextBox.TabIndex = 37;
            this.f_ValueGamerichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(192, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "注:系统使用后请慎重修改";
            // 
            // f_Value1Label
            // 
            f_Value1Label.AutoSize = true;
            f_Value1Label.Location = new System.Drawing.Point(19, 133);
            f_Value1Label.Name = "f_Value1Label";
            f_Value1Label.Size = new System.Drawing.Size(53, 12);
            f_Value1Label.TabIndex = 7;
            f_Value1Label.Text = "项内容1:";
            // 
            // f_Value1TextBox
            // 
            this.f_Value1TextBox.Location = new System.Drawing.Point(108, 130);
            this.f_Value1TextBox.Name = "f_Value1TextBox";
            this.f_Value1TextBox.Size = new System.Drawing.Size(214, 21);
            this.f_Value1TextBox.TabIndex = 8;
            // 
            // FormGameConfigAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 348);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormGameConfigAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "游戏配置添加";
            this.Load += new System.EventHandler(this.FormGameConfigAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox f_IDTextBox;
        private System.Windows.Forms.TextBox f_ParentIDTextBox;
        private System.Windows.Forms.TextBox f_NameTextBox;
        private System.Windows.Forms.TextBox f_ValueTextBox;
        private System.Windows.Forms.CheckBox f_IsUsedCheckBox;
        private System.Windows.Forms.TextBox f_SortTextBox;
        private System.Windows.Forms.Button buttonESC;
        private System.Windows.Forms.Button buttonSure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox f_ValueGamerichTextBox;
        private System.Windows.Forms.TextBox f_Value1TextBox;
    }
}