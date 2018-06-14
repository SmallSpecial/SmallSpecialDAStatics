namespace GSSServer.GSSManage
{
    partial class FormUsers
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
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fUserIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fUserNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fPassWordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDepartIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fRoleIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fRealNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fSexDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fBirthdayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fEmailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fMobilePhoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fTelphoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fNoteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fRegTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fLastInTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fIsUsedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Image = global::GSSServer.Properties.Resources.DEL;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButtonDelete.Size = new System.Drawing.Size(54, 22);
            this.toolStripButtonDelete.Text = "删除";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = global::GSSServer.Properties.Resources.OK;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButtonAdd.Size = new System.Drawing.Size(54, 22);
            this.toolStripButtonAdd.Text = "新增";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fUserIDDataGridViewTextBoxColumn,
            this.fUserNameDataGridViewTextBoxColumn,
            this.fPassWordDataGridViewTextBoxColumn,
            this.fDepartIDDataGridViewTextBoxColumn,
            this.fRoleIDDataGridViewTextBoxColumn,
            this.fRealNameDataGridViewTextBoxColumn,
            this.fSexDataGridViewCheckBoxColumn,
            this.fBirthdayDataGridViewTextBoxColumn,
            this.fEmailDataGridViewTextBoxColumn,
            this.fMobilePhoneDataGridViewTextBoxColumn,
            this.fTelphoneDataGridViewTextBoxColumn,
            this.fNoteDataGridViewTextBoxColumn,
            this.fRegTimeDataGridViewTextBoxColumn,
            this.fLastInTimeDataGridViewTextBoxColumn,
            this.fIsUsedDataGridViewCheckBoxColumn});
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(716, 373);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // fUserIDDataGridViewTextBoxColumn
            // 
            this.fUserIDDataGridViewTextBoxColumn.DataPropertyName = "F_UserID";
            this.fUserIDDataGridViewTextBoxColumn.HeaderText = "用户编号";
            this.fUserIDDataGridViewTextBoxColumn.Name = "fUserIDDataGridViewTextBoxColumn";
            this.fUserIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fUserNameDataGridViewTextBoxColumn
            // 
            this.fUserNameDataGridViewTextBoxColumn.DataPropertyName = "F_UserName";
            this.fUserNameDataGridViewTextBoxColumn.HeaderText = "用户名";
            this.fUserNameDataGridViewTextBoxColumn.Name = "fUserNameDataGridViewTextBoxColumn";
            this.fUserNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fPassWordDataGridViewTextBoxColumn
            // 
            this.fPassWordDataGridViewTextBoxColumn.DataPropertyName = "F_PassWord";
            this.fPassWordDataGridViewTextBoxColumn.HeaderText = "密码";
            this.fPassWordDataGridViewTextBoxColumn.Name = "fPassWordDataGridViewTextBoxColumn";
            this.fPassWordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fDepartIDDataGridViewTextBoxColumn
            // 
            this.fDepartIDDataGridViewTextBoxColumn.DataPropertyName = "F_DepartID";
            this.fDepartIDDataGridViewTextBoxColumn.HeaderText = "部门";
            this.fDepartIDDataGridViewTextBoxColumn.Name = "fDepartIDDataGridViewTextBoxColumn";
            this.fDepartIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fRoleIDDataGridViewTextBoxColumn
            // 
            this.fRoleIDDataGridViewTextBoxColumn.DataPropertyName = "F_RoleID";
            this.fRoleIDDataGridViewTextBoxColumn.HeaderText = "角色";
            this.fRoleIDDataGridViewTextBoxColumn.Name = "fRoleIDDataGridViewTextBoxColumn";
            this.fRoleIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fRealNameDataGridViewTextBoxColumn
            // 
            this.fRealNameDataGridViewTextBoxColumn.DataPropertyName = "F_RealName";
            this.fRealNameDataGridViewTextBoxColumn.HeaderText = "真实姓名";
            this.fRealNameDataGridViewTextBoxColumn.Name = "fRealNameDataGridViewTextBoxColumn";
            this.fRealNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fSexDataGridViewCheckBoxColumn
            // 
            this.fSexDataGridViewCheckBoxColumn.DataPropertyName = "F_Sex";
            this.fSexDataGridViewCheckBoxColumn.HeaderText = "性别";
            this.fSexDataGridViewCheckBoxColumn.Name = "fSexDataGridViewCheckBoxColumn";
            this.fSexDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // fBirthdayDataGridViewTextBoxColumn
            // 
            this.fBirthdayDataGridViewTextBoxColumn.DataPropertyName = "F_Birthday";
            this.fBirthdayDataGridViewTextBoxColumn.HeaderText = "生日";
            this.fBirthdayDataGridViewTextBoxColumn.Name = "fBirthdayDataGridViewTextBoxColumn";
            this.fBirthdayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fEmailDataGridViewTextBoxColumn
            // 
            this.fEmailDataGridViewTextBoxColumn.DataPropertyName = "F_Email";
            this.fEmailDataGridViewTextBoxColumn.HeaderText = "邮箱";
            this.fEmailDataGridViewTextBoxColumn.Name = "fEmailDataGridViewTextBoxColumn";
            this.fEmailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fMobilePhoneDataGridViewTextBoxColumn
            // 
            this.fMobilePhoneDataGridViewTextBoxColumn.DataPropertyName = "F_MobilePhone";
            this.fMobilePhoneDataGridViewTextBoxColumn.HeaderText = "移动电话";
            this.fMobilePhoneDataGridViewTextBoxColumn.Name = "fMobilePhoneDataGridViewTextBoxColumn";
            this.fMobilePhoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fTelphoneDataGridViewTextBoxColumn
            // 
            this.fTelphoneDataGridViewTextBoxColumn.DataPropertyName = "F_Telphone";
            this.fTelphoneDataGridViewTextBoxColumn.HeaderText = "固定电话";
            this.fTelphoneDataGridViewTextBoxColumn.Name = "fTelphoneDataGridViewTextBoxColumn";
            this.fTelphoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fNoteDataGridViewTextBoxColumn
            // 
            this.fNoteDataGridViewTextBoxColumn.DataPropertyName = "F_Note";
            this.fNoteDataGridViewTextBoxColumn.HeaderText = "备注";
            this.fNoteDataGridViewTextBoxColumn.Name = "fNoteDataGridViewTextBoxColumn";
            this.fNoteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fRegTimeDataGridViewTextBoxColumn
            // 
            this.fRegTimeDataGridViewTextBoxColumn.DataPropertyName = "F_RegTime";
            this.fRegTimeDataGridViewTextBoxColumn.HeaderText = "注册时间";
            this.fRegTimeDataGridViewTextBoxColumn.Name = "fRegTimeDataGridViewTextBoxColumn";
            this.fRegTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fLastInTimeDataGridViewTextBoxColumn
            // 
            this.fLastInTimeDataGridViewTextBoxColumn.DataPropertyName = "F_LastInTime";
            this.fLastInTimeDataGridViewTextBoxColumn.HeaderText = "更新时间";
            this.fLastInTimeDataGridViewTextBoxColumn.Name = "fLastInTimeDataGridViewTextBoxColumn";
            this.fLastInTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fIsUsedDataGridViewCheckBoxColumn
            // 
            this.fIsUsedDataGridViewCheckBoxColumn.DataPropertyName = "F_IsUsed";
            this.fIsUsedDataGridViewCheckBoxColumn.HeaderText = "是否启用";
            this.fIsUsedDataGridViewCheckBoxColumn.Name = "fIsUsedDataGridViewCheckBoxColumn";
            this.fIsUsedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = global::GSSServer.Properties.Resources.Refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(78, 22);
            this.toolStripButtonRefresh.Text = "刷新数据";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(728, 399);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列表";
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.Image = global::GSSServer.Properties.Resources.People;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButtonEdit.Size = new System.Drawing.Size(84, 22);
            this.toolStripButtonEdit.Text = "查看/修改";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonEdit,
            this.toolStripButtonDelete,
            this.toolStripButtonRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(752, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FormUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 439);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormUsers";
            this.ShowInTaskbar = false;
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.FormUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStrip toolStrip1;

        private System.Windows.Forms.DataGridViewTextBoxColumn fUserIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fUserNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fPassWordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDepartIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fRoleIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fRealNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn fSexDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fBirthdayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fEmailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fMobilePhoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fTelphoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fNoteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fRegTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fLastInTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn fIsUsedDataGridViewCheckBoxColumn;
    }
}