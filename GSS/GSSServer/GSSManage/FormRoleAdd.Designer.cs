namespace GSSServer.GSSManage
{
    partial class FormRoleAdd
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
            System.Windows.Forms.Label f_RoleIDLabel;
            System.Windows.Forms.Label f_RoleNameLabel;
            System.Windows.Forms.Label f_PowerLabel;
            this.f_RoleIDTextBox = new System.Windows.Forms.TextBox();
            this.f_RoleNameTextBox = new System.Windows.Forms.TextBox();
            this.f_IsUsedCheckBox = new System.Windows.Forms.CheckBox();
            this.buttonESC = new System.Windows.Forms.Button();
            this.buttonSure = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TreeView1 = new GSSUI.AControl.AThirTreeView.AThirTreeViewCtr();
            f_RoleIDLabel = new System.Windows.Forms.Label();
            f_RoleNameLabel = new System.Windows.Forms.Label();
            f_PowerLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // f_RoleIDLabel
            // 
            f_RoleIDLabel.AutoSize = true;
            f_RoleIDLabel.Location = new System.Drawing.Point(7, 29);
            f_RoleIDLabel.Name = "f_RoleIDLabel";
            f_RoleIDLabel.Size = new System.Drawing.Size(59, 12);
            f_RoleIDLabel.TabIndex = 1;
            f_RoleIDLabel.Text = "角色编号:";
            // 
            // f_RoleNameLabel
            // 
            f_RoleNameLabel.AutoSize = true;
            f_RoleNameLabel.Location = new System.Drawing.Point(7, 62);
            f_RoleNameLabel.Name = "f_RoleNameLabel";
            f_RoleNameLabel.Size = new System.Drawing.Size(59, 12);
            f_RoleNameLabel.TabIndex = 3;
            f_RoleNameLabel.Text = "角色名称:";
            // 
            // f_PowerLabel
            // 
            f_PowerLabel.AutoSize = true;
            f_PowerLabel.Location = new System.Drawing.Point(222, 97);
            f_PowerLabel.Name = "f_PowerLabel";
            f_PowerLabel.Size = new System.Drawing.Size(65, 12);
            f_PowerLabel.TabIndex = 7;
            f_PowerLabel.Text = "角色权限>>";
            // 
            // f_RoleIDTextBox
            // 
            this.f_RoleIDTextBox.Enabled = false;
            this.f_RoleIDTextBox.Location = new System.Drawing.Point(72, 26);
            this.f_RoleIDTextBox.Name = "f_RoleIDTextBox";
            this.f_RoleIDTextBox.Size = new System.Drawing.Size(215, 21);
            this.f_RoleIDTextBox.TabIndex = 2;
            this.f_RoleIDTextBox.Text = "0";
            // 
            // f_RoleNameTextBox
            // 
            this.f_RoleNameTextBox.Location = new System.Drawing.Point(72, 59);
            this.f_RoleNameTextBox.Name = "f_RoleNameTextBox";
            this.f_RoleNameTextBox.Size = new System.Drawing.Size(215, 21);
            this.f_RoleNameTextBox.TabIndex = 4;
            // 
            // f_IsUsedCheckBox
            // 
            this.f_IsUsedCheckBox.AutoSize = true;
            this.f_IsUsedCheckBox.Checked = true;
            this.f_IsUsedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.f_IsUsedCheckBox.Location = new System.Drawing.Point(9, 97);
            this.f_IsUsedCheckBox.Name = "f_IsUsedCheckBox";
            this.f_IsUsedCheckBox.Size = new System.Drawing.Size(72, 16);
            this.f_IsUsedCheckBox.TabIndex = 6;
            this.f_IsUsedCheckBox.Text = "是否启用";
            this.f_IsUsedCheckBox.UseVisualStyleBackColor = true;
            // 
            // buttonESC
            // 
            this.buttonESC.Location = new System.Drawing.Point(148, 128);
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.Size = new System.Drawing.Size(75, 23);
            this.buttonESC.TabIndex = 34;
            this.buttonESC.Text = "取 消";
            this.buttonESC.UseVisualStyleBackColor = true;
            this.buttonESC.Click += new System.EventHandler(this.buttonESC_Click);
            // 
            // buttonSure
            // 
            this.buttonSure.Location = new System.Drawing.Point(25, 128);
            this.buttonSure.Name = "buttonSure";
            this.buttonSure.Size = new System.Drawing.Size(75, 23);
            this.buttonSure.TabIndex = 0;
            this.buttonSure.Text = "确 定";
            this.buttonSure.UseVisualStyleBackColor = true;
            this.buttonSure.Click += new System.EventHandler(this.buttonSure_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TreeView1);
            this.groupBox1.Controls.Add(f_RoleIDLabel);
            this.groupBox1.Controls.Add(this.buttonESC);
            this.groupBox1.Controls.Add(f_PowerLabel);
            this.groupBox1.Controls.Add(this.buttonSure);
            this.groupBox1.Controls.Add(this.f_IsUsedCheckBox);
            this.groupBox1.Controls.Add(this.f_RoleNameTextBox);
            this.groupBox1.Controls.Add(this.f_RoleIDTextBox);
            this.groupBox1.Controls.Add(f_RoleNameLabel);
            this.groupBox1.Location = new System.Drawing.Point(9, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 357);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // TreeView1
            // 
            this.TreeView1.CheckBoxes = true;
            this.TreeView1.ItemHeight = 16;
            this.TreeView1.Location = new System.Drawing.Point(293, 26);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(233, 322);
            this.TreeView1.TabIndex = 36;
            // 
            // FormRoleAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 368);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormRoleAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "角色添加";
            this.Load += new System.EventHandler(this.FormRoleAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox f_RoleIDTextBox;
        private System.Windows.Forms.TextBox f_RoleNameTextBox;
        private System.Windows.Forms.CheckBox f_IsUsedCheckBox;
        private System.Windows.Forms.Button buttonESC;
        private System.Windows.Forms.Button buttonSure;
        private System.Windows.Forms.GroupBox groupBox1;
        private GSSUI.AControl.AThirTreeView.AThirTreeViewCtr TreeView1;
    }
}