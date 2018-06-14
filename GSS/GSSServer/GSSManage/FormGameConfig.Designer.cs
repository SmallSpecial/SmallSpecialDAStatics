namespace GSSServer.GSSManage
{
    partial class FormGameConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fParentIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fValue1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fValueGameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fIsUsedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fSortDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.fIDDataGridViewTextBoxColumn,
            this.fParentIDDataGridViewTextBoxColumn,
            this.fNameDataGridViewTextBoxColumn,
            this.fValueDataGridViewTextBoxColumn,
            this.fValue1DataGridViewTextBoxColumn,
            this.fValueGameDataGridViewTextBoxColumn,
            this.fIsUsedDataGridViewCheckBoxColumn,
            this.fSortDataGridViewTextBoxColumn});
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(731, 241);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 301);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(743, 78);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "注意事项";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(33, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "1.系统正式使用后请慎重修改数据\r\n";
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
            this.toolStrip1.Size = new System.Drawing.Size(767, 25);
            this.toolStrip1.TabIndex = 5;
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
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(743, 267);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据列表";
            // 
            // fIDDataGridViewTextBoxColumn
            // 
            this.fIDDataGridViewTextBoxColumn.DataPropertyName = "F_ID";
            this.fIDDataGridViewTextBoxColumn.HeaderText = "编号";
            this.fIDDataGridViewTextBoxColumn.Name = "fIDDataGridViewTextBoxColumn";
            this.fIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fParentIDDataGridViewTextBoxColumn
            // 
            this.fParentIDDataGridViewTextBoxColumn.DataPropertyName = "F_ParentID";
            this.fParentIDDataGridViewTextBoxColumn.HeaderText = "父编号";
            this.fParentIDDataGridViewTextBoxColumn.Name = "fParentIDDataGridViewTextBoxColumn";
            this.fParentIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fNameDataGridViewTextBoxColumn
            // 
            this.fNameDataGridViewTextBoxColumn.DataPropertyName = "F_Name";
            this.fNameDataGridViewTextBoxColumn.HeaderText = "名称";
            this.fNameDataGridViewTextBoxColumn.Name = "fNameDataGridViewTextBoxColumn";
            this.fNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fValueDataGridViewTextBoxColumn
            // 
            this.fValueDataGridViewTextBoxColumn.DataPropertyName = "F_Value";
            this.fValueDataGridViewTextBoxColumn.HeaderText = "值";
            this.fValueDataGridViewTextBoxColumn.Name = "fValueDataGridViewTextBoxColumn";
            this.fValueDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fValue1DataGridViewTextBoxColumn
            // 
            this.fValue1DataGridViewTextBoxColumn.DataPropertyName = "F_Value1";
            this.fValue1DataGridViewTextBoxColumn.HeaderText = "值1";
            this.fValue1DataGridViewTextBoxColumn.Name = "fValue1DataGridViewTextBoxColumn";
            this.fValue1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fValueGameDataGridViewTextBoxColumn
            // 
            this.fValueGameDataGridViewTextBoxColumn.DataPropertyName = "F_ValueGame";
            this.fValueGameDataGridViewTextBoxColumn.HeaderText = "游戏中对应值";
            this.fValueGameDataGridViewTextBoxColumn.Name = "fValueGameDataGridViewTextBoxColumn";
            this.fValueGameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fIsUsedDataGridViewCheckBoxColumn
            // 
            this.fIsUsedDataGridViewCheckBoxColumn.DataPropertyName = "F_IsUsed";
            this.fIsUsedDataGridViewCheckBoxColumn.HeaderText = "是否启用";
            this.fIsUsedDataGridViewCheckBoxColumn.Name = "fIsUsedDataGridViewCheckBoxColumn";
            this.fIsUsedDataGridViewCheckBoxColumn.ReadOnly = true;
            this.fIsUsedDataGridViewCheckBoxColumn.Width = 60;
            // 
            // fSortDataGridViewTextBoxColumn
            // 
            this.fSortDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fSortDataGridViewTextBoxColumn.DataPropertyName = "F_Sort";
            this.fSortDataGridViewTextBoxColumn.HeaderText = "排序";
            this.fSortDataGridViewTextBoxColumn.Name = "fSortDataGridViewTextBoxColumn";
            this.fSortDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormGameConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 391);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormGameConfig";
            this.ShowInTaskbar = false;
            this.Text = "游戏配置";
            this.Load += new System.EventHandler(this.FormGameConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn fIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fParentIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fValue1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fValueGameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn fIsUsedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fSortDataGridViewTextBoxColumn;
    }
}