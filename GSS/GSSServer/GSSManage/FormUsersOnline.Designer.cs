namespace GSSServer.GSSManage
{
    partial class FormUsersOnline
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fUserIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fUserNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDepartIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fRoleIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fRealNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fMobilePhoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fTelphoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.fDepartIDDataGridViewTextBoxColumn,
            this.fRoleIDDataGridViewTextBoxColumn,
            this.fRealNameDataGridViewTextBoxColumn,
            this.fMobilePhoneDataGridViewTextBoxColumn,
            this.fTelphoneDataGridViewTextBoxColumn});
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(808, 388);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
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
            this.toolStrip1.Size = new System.Drawing.Size(844, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = global::GSSServer.Properties.Resources.OK;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButtonAdd.Size = new System.Drawing.Size(54, 22);
            this.toolStripButtonAdd.Text = "新增";
            this.toolStripButtonAdd.Visible = false;
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.Image = global::GSSServer.Properties.Resources.People;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButtonEdit.Size = new System.Drawing.Size(84, 22);
            this.toolStripButtonEdit.Text = "查看/修改";
            this.toolStripButtonEdit.Visible = false;
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Image = global::GSSServer.Properties.Resources.DEL;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButtonDelete.Size = new System.Drawing.Size(54, 22);
            this.toolStripButtonDelete.Text = "删除";
            this.toolStripButtonDelete.Visible = false;
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = global::GSSServer.Properties.Resources.Refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(112, 22);
            this.toolStripButtonRefresh.Text = "刷新在线用户";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(820, 414);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "在线用户";
            // 
            // fUserIDDataGridViewTextBoxColumn
            // 
            this.fUserIDDataGridViewTextBoxColumn.DataPropertyName = "F_UserID";
            this.fUserIDDataGridViewTextBoxColumn.HeaderText = "用户编号";
            this.fUserIDDataGridViewTextBoxColumn.MinimumWidth = 100;
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
            // fMobilePhoneDataGridViewTextBoxColumn
            // 
            this.fMobilePhoneDataGridViewTextBoxColumn.DataPropertyName = "F_MobilePhone";
            this.fMobilePhoneDataGridViewTextBoxColumn.HeaderText = "移动电话";
            this.fMobilePhoneDataGridViewTextBoxColumn.Name = "fMobilePhoneDataGridViewTextBoxColumn";
            this.fMobilePhoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fTelphoneDataGridViewTextBoxColumn
            // 
            this.fTelphoneDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fTelphoneDataGridViewTextBoxColumn.DataPropertyName = "F_Telphone";
            this.fTelphoneDataGridViewTextBoxColumn.HeaderText = "固定电话";
            this.fTelphoneDataGridViewTextBoxColumn.Name = "fTelphoneDataGridViewTextBoxColumn";
            this.fTelphoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormUsersOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 454);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormUsersOnline";
            this.Text = "在线用户";
            this.Load += new System.EventHandler(this.FormUsersOnline_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn fUserIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fUserNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDepartIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fRoleIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fRealNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fMobilePhoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fTelphoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}