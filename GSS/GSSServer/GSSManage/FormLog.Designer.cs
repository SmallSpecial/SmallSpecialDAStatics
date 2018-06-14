namespace GSSServer.GSSManage
{
    partial class FormLog
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
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fUserIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fUserNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fNoteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBoxSearchText = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fIDDataGridViewTextBoxColumn,
            this.fUserIDDataGridViewTextBoxColumn,
            this.fUserNameDataGridViewTextBoxColumn,
            this.fNoteDataGridViewTextBoxColumn,
            this.fDataDataGridViewTextBoxColumn,
            this.fDateTimeDataGridViewTextBoxColumn});
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(770, 270);
            this.dataGridView1.TabIndex = 4;
            // 
            // fIDDataGridViewTextBoxColumn
            // 
            this.fIDDataGridViewTextBoxColumn.DataPropertyName = "F_ID";
            this.fIDDataGridViewTextBoxColumn.HeaderText = "编号";
            this.fIDDataGridViewTextBoxColumn.Name = "fIDDataGridViewTextBoxColumn";
            this.fIDDataGridViewTextBoxColumn.ReadOnly = true;
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
            // fNoteDataGridViewTextBoxColumn
            // 
            this.fNoteDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fNoteDataGridViewTextBoxColumn.DataPropertyName = "F_Note";
            this.fNoteDataGridViewTextBoxColumn.HeaderText = "操作记录";
            this.fNoteDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.fNoteDataGridViewTextBoxColumn.Name = "fNoteDataGridViewTextBoxColumn";
            this.fNoteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fDataDataGridViewTextBoxColumn
            // 
            this.fDataDataGridViewTextBoxColumn.DataPropertyName = "F_Data";
            this.fDataDataGridViewTextBoxColumn.HeaderText = "相关数据";
            this.fDataDataGridViewTextBoxColumn.Name = "fDataDataGridViewTextBoxColumn";
            this.fDataDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fDateTimeDataGridViewTextBoxColumn
            // 
            this.fDateTimeDataGridViewTextBoxColumn.DataPropertyName = "F_DateTime";
            this.fDateTimeDataGridViewTextBoxColumn.HeaderText = "日志时间";
            this.fDateTimeDataGridViewTextBoxColumn.Name = "fDateTimeDataGridViewTextBoxColumn";
            this.fDateTimeDataGridViewTextBoxColumn.ReadOnly = true;
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(12, 330);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 64);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "注意事项";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = global::GSSServer.Properties.Resources.Refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(88, 22);
            this.toolStripButtonRefresh.Text = "刷新数据";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonEdit,
            this.toolStripButtonDelete,
            this.toolStripButtonRefresh,
            this.toolStripComboBoxType,
            this.toolStripTextBoxSearchText,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(806, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripComboBoxType
            // 
            this.toolStripComboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxType.Items.AddRange(new object[] {
            "用户名",
            "操作记录",
            "相关数据"});
            this.toolStripComboBoxType.Name = "toolStripComboBoxType";
            this.toolStripComboBoxType.Size = new System.Drawing.Size(81, 25);
            // 
            // toolStripTextBoxSearchText
            // 
            this.toolStripTextBoxSearchText.Name = "toolStripTextBoxSearchText";
            this.toolStripTextBoxSearchText.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.toolStripTextBoxSearchText.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::GSSServer.Properties.Resources.Serch;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripButton1.Text = "查找";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(782, 296);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据列表";
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 406);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormLog";
            this.ShowInTaskbar = false;
            this.Text = "系统日志";
            this.Load += new System.EventHandler(this.FormLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fUserIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fUserNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fNoteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDateTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSearchText;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxType;
    }
}