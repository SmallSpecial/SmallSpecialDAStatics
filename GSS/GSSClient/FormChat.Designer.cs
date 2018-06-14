namespace GSSClient
{
    partial class FormChat
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChat));
            this.dgvRoleList = new GSSUI.DataGridViewUI();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTip = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRlstClear = new GSSUI.AControl.AButton.AButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new GSSUI.AControl.AButton.AButton();
            this.tboxRoleName = new System.Windows.Forms.TextBox();
            this.lblLoading = new System.Windows.Forms.Label();
            this.groupBoxTtasklog = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLoadingMSGList = new System.Windows.Forms.Label();
            this.tboxMSGList = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.buttonOK = new GSSUI.AControl.AButton.AButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lboxExample = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemSend = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLoadingRole = new System.Windows.Forms.Label();
            this.lblRoleInfo = new System.Windows.Forms.Label();
            this.lblTaskID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tboxNote = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.aButton1 = new GSSUI.AControl.AButton.AButton();
            this.aButton2 = new GSSUI.AControl.AButton.AButton();
            this.aButton3 = new GSSUI.AControl.AButton.AButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lsvExample = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCloseForm = new GSSUI.AControl.AButton.AButton();
            this.ButtonClose = new GSSUI.AControl.AButton.AButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxTtasklog.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRoleList
            // 
            this.dgvRoleList.AllowUserToAddRows = false;
            this.dgvRoleList.AllowUserToDeleteRows = false;
            this.dgvRoleList.AllowUserToResizeRows = false;
            this.dgvRoleList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRoleList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoleList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRoleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoleList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column15,
            this.ColumnTip});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRoleList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRoleList.Location = new System.Drawing.Point(6, 2);
            this.dgvRoleList.MultiSelect = false;
            this.dgvRoleList.Name = "dgvRoleList";
            this.dgvRoleList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoleList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRoleList.RowHeadersVisible = false;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvRoleList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRoleList.RowTemplate.Height = 23;
            this.dgvRoleList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoleList.Size = new System.Drawing.Size(195, 303);
            this.dgvRoleList.TabIndex = 0;
            this.dgvRoleList.SelectionChanged += new System.EventHandler(this.dgvRoleList_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "F_ID";
            this.Column1.HeaderText = global::GSSClient.LanguageResource.Language.LblWorkOrderNo;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column15
            // 
            this.Column15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column15.DataPropertyName = "F_GRoleName";
            this.Column15.HeaderText = global::GSSClient.LanguageResource.Language.LblRoleName;
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // ColumnTip
            // 
            this.ColumnTip.HeaderText = "";
            this.ColumnTip.Name = "ColumnTip";
            this.ColumnTip.ReadOnly = true;
            this.ColumnTip.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnTip.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnTip.Width = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnRlstClear);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.tboxRoleName);
            this.groupBox1.Controls.Add(this.lblLoading);
            this.groupBox1.Controls.Add(this.dgvRoleList);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 384);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnRlstClear
            // 
            this.btnRlstClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRlstClear.BackColor = System.Drawing.Color.Transparent;
            this.btnRlstClear.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnRlstClear.BackImg")));
            this.btnRlstClear.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnRlstClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRlstClear.Location = new System.Drawing.Point(75, 328);
            this.btnRlstClear.Name = "btnRlstClear";
            this.btnRlstClear.Size = new System.Drawing.Size(126, 22);
            this.btnRlstClear.TabIndex = 2223348;
            this.btnRlstClear.Text = "刷新咨询列表";
            this.btnRlstClear.UseVisualStyleBackColor = true;
            this.btnRlstClear.Click += new System.EventHandler(this.btnRlstClear_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(8, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 2223347;
            this.label6.Text = "咨询列表";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "角色";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnSearch.BackImg")));
            this.btnSearch.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Location = new System.Drawing.Point(129, 356);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(50, 22);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查 找";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tboxRoleName
            // 
            this.tboxRoleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxRoleName.Location = new System.Drawing.Point(37, 357);
            this.tboxRoleName.Name = "tboxRoleName";
            this.tboxRoleName.Size = new System.Drawing.Size(86, 21);
            this.tboxRoleName.TabIndex = 1;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblLoading.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoading.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblLoading.Location = new System.Drawing.Point(48, 95);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(106, 14);
            this.lblLoading.TabIndex = 2223339;
            this.lblLoading.Text = "数据加载中...";
            // 
            // groupBoxTtasklog
            // 
            this.groupBoxTtasklog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTtasklog.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxTtasklog.Controls.Add(this.label5);
            this.groupBoxTtasklog.Controls.Add(this.lblLoadingMSGList);
            this.groupBoxTtasklog.Controls.Add(this.tboxMSGList);
            this.groupBoxTtasklog.Location = new System.Drawing.Point(216, 128);
            this.groupBoxTtasklog.Name = "groupBoxTtasklog";
            this.groupBoxTtasklog.Size = new System.Drawing.Size(525, 256);
            this.groupBoxTtasklog.TabIndex = 2223336;
            this.groupBoxTtasklog.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(8, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 2223346;
            this.label5.Text = "咨询内容";
            // 
            // lblLoadingMSGList
            // 
            this.lblLoadingMSGList.AutoSize = true;
            this.lblLoadingMSGList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoadingMSGList.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblLoadingMSGList.Location = new System.Drawing.Point(136, 109);
            this.lblLoadingMSGList.Name = "lblLoadingMSGList";
            this.lblLoadingMSGList.Size = new System.Drawing.Size(106, 14);
            this.lblLoadingMSGList.TabIndex = 2223343;
            this.lblLoadingMSGList.Text = "数据加载中...";
            // 
            // tboxMSGList
            // 
            this.tboxMSGList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMSGList.EnableAutoDragDrop = true;
            this.tboxMSGList.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tboxMSGList.Location = new System.Drawing.Point(6, 14);
            this.tboxMSGList.Name = "tboxMSGList";
            this.tboxMSGList.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tboxMSGList.Size = new System.Drawing.Size(513, 236);
            this.tboxMSGList.TabIndex = 17;
            this.tboxMSGList.Text = "";
            this.tboxMSGList.UseCutPaste = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BackColor = System.Drawing.Color.Transparent;
            this.buttonOK.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("buttonOK.BackImg")));
            this.buttonOK.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.buttonOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOK.Location = new System.Drawing.Point(647, 506);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "发 送";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lboxExample);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblLoadingRole);
            this.groupBox2.Controls.Add(this.lblRoleInfo);
            this.groupBox2.Controls.Add(this.lblTaskID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(216, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(525, 120);
            this.groupBox2.TabIndex = 2223337;
            this.groupBox2.TabStop = false;
            // 
            // lboxExample
            // 
            this.lboxExample.ContextMenuStrip = this.contextMenuStrip1;
            this.lboxExample.FormattingEnabled = true;
            this.lboxExample.ItemHeight = 12;
            this.lboxExample.Items.AddRange(new object[] {
            "您好",
            "好的",
            "还有什么需要帮助的吗？",
            "谢谢您的宝贵意见",
            "再见",
            "我们会尽快处理",
            "感谢您的支持！",
            "非常抱歉",
            "我们会尽快处理"});
            this.lboxExample.Location = new System.Drawing.Point(311, 32);
            this.lboxExample.Name = "lboxExample";
            this.lboxExample.Size = new System.Drawing.Size(195, 88);
            this.lboxExample.TabIndex = 0;
            this.lboxExample.Visible = false;
            this.lboxExample.SelectedIndexChanged += new System.EventHandler(this.lboxExample_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSend});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 26);
            // 
            // ToolStripMenuItemSend
            // 
            this.ToolStripMenuItemSend.Name = "ToolStripMenuItemSend";
            this.ToolStripMenuItemSend.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)));
            this.ToolStripMenuItemSend.Size = new System.Drawing.Size(172, 22);
            this.ToolStripMenuItemSend.Text = "Send";
            this.ToolStripMenuItemSend.Click += new System.EventHandler(this.ToolStripMenuItemSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(8, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 2223345;
            this.label4.Text = "咨询对象";
            // 
            // lblLoadingRole
            // 
            this.lblLoadingRole.AutoSize = true;
            this.lblLoadingRole.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoadingRole.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblLoadingRole.Location = new System.Drawing.Point(136, 38);
            this.lblLoadingRole.Name = "lblLoadingRole";
            this.lblLoadingRole.Size = new System.Drawing.Size(106, 14);
            this.lblLoadingRole.TabIndex = 2223340;
            this.lblLoadingRole.Text = "数据加载中...";
            // 
            // lblRoleInfo
            // 
            this.lblRoleInfo.AutoEllipsis = true;
            this.lblRoleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoleInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRoleInfo.Location = new System.Drawing.Point(3, 17);
            this.lblRoleInfo.Name = "lblRoleInfo";
            this.lblRoleInfo.Size = new System.Drawing.Size(519, 100);
            this.lblRoleInfo.TabIndex = 2223344;
            this.lblRoleInfo.Text = "    ";
            // 
            // lblTaskID
            // 
            this.lblTaskID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaskID.AutoSize = true;
            this.lblTaskID.BackColor = System.Drawing.Color.White;
            this.lblTaskID.Location = new System.Drawing.Point(446, 2);
            this.lblTaskID.Name = "lblTaskID";
            this.lblTaskID.Size = new System.Drawing.Size(29, 12);
            this.lblTaskID.TabIndex = 2223342;
            this.lblTaskID.Text = "    ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(387, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2223341;
            this.label2.Text = "咨询编号:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tboxNote);
            this.groupBox3.Location = new System.Drawing.Point(216, 390);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(525, 107);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(8, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 12);
            this.label8.TabIndex = 2223349;
            this.label8.Text = "发送内容";
            // 
            // tboxNote
            // 
            this.tboxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxNote.EnableAutoDragDrop = true;
            this.tboxNote.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tboxNote.Location = new System.Drawing.Point(6, 13);
            this.tboxNote.Name = "tboxNote";
            this.tboxNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tboxNote.Size = new System.Drawing.Size(513, 88);
            this.tboxNote.TabIndex = 0;
            this.tboxNote.Text = "";
            this.tboxNote.UseCutPaste = false;
            // 
            // aButton1
            // 
            this.aButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.aButton1.BackColor = System.Drawing.Color.Transparent;
            this.aButton1.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("aButton1.BackImg")));
            this.aButton1.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.aButton1.Enabled = false;
            this.aButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aButton1.Location = new System.Drawing.Point(139, 506);
            this.aButton1.Name = "aButton1";
            this.aButton1.Size = new System.Drawing.Size(101, 23);
            this.aButton1.TabIndex = 6;
            this.aButton1.Text = "完成聊天并存档";
            this.aButton1.UseVisualStyleBackColor = true;
            // 
            // aButton2
            // 
            this.aButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.aButton2.BackColor = System.Drawing.Color.Transparent;
            this.aButton2.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("aButton2.BackImg")));
            this.aButton2.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.aButton2.Enabled = false;
            this.aButton2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aButton2.Location = new System.Drawing.Point(3, 506);
            this.aButton2.Name = "aButton2";
            this.aButton2.Size = new System.Drawing.Size(130, 23);
            this.aButton2.TabIndex = 5;
            this.aButton2.Text = "完成聊天并生成工单";
            this.aButton2.UseVisualStyleBackColor = true;
            // 
            // aButton3
            // 
            this.aButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aButton3.BackColor = System.Drawing.Color.Transparent;
            this.aButton3.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("aButton3.BackImg")));
            this.aButton3.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.aButton3.Enabled = false;
            this.aButton3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aButton3.Location = new System.Drawing.Point(473, 506);
            this.aButton3.Name = "aButton3";
            this.aButton3.Size = new System.Drawing.Size(80, 23);
            this.aButton3.TabIndex = 4;
            this.aButton3.Text = "历史记录";
            this.aButton3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(372, 511);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 2223342;
            this.label3.Text = "Ctrl+Enter 发送";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.lsvExample);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(3, 393);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(207, 107);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // lsvExample
            // 
            this.lsvExample.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvExample.FullRowSelect = true;
            this.lsvExample.GridLines = true;
            this.lsvExample.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvExample.HideSelection = false;
            this.lsvExample.Location = new System.Drawing.Point(6, 16);
            this.lsvExample.MultiSelect = false;
            this.lsvExample.Name = "lsvExample";
            this.lsvExample.ShowGroups = false;
            this.lsvExample.ShowItemToolTips = true;
            this.lsvExample.Size = new System.Drawing.Size(195, 85);
            this.lsvExample.TabIndex = 2223346;
            this.lsvExample.TileSize = new System.Drawing.Size(128, 32);
            this.lsvExample.UseCompatibleStateImageBehavior = false;
            this.lsvExample.View = System.Windows.Forms.View.Details;
            this.lsvExample.SelectedIndexChanged += new System.EventHandler(this.lsvExample_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 172;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(8, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 12);
            this.label7.TabIndex = 2223348;
            this.label7.Text = "常用语句";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnCloseForm);
            this.panel1.Controls.Add(this.ButtonClose);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.aButton3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.aButton2);
            this.panel1.Controls.Add(this.aButton1);
            this.panel1.Controls.Add(this.groupBoxTtasklog);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Location = new System.Drawing.Point(8, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 532);
            this.panel1.TabIndex = 2223343;
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseForm.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseForm.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btnCloseForm.BackImg")));
            this.btnCloseForm.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnCloseForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCloseForm.Location = new System.Drawing.Point(559, 506);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(82, 23);
            this.btnCloseForm.TabIndex = 2223344;
            this.btnCloseForm.Text = "关闭窗口";
            this.btnCloseForm.UseVisualStyleBackColor = true;
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.ButtonClose.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("ButtonClose.BackImg")));
            this.ButtonClose.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ButtonClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonClose.Location = new System.Drawing.Point(246, 506);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(120, 23);
            this.ButtonClose.TabIndex = 2223343;
            this.ButtonClose.Text = "结束对话";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // FormChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 625);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsResize = true;
            this.KeyPreview = true;
            this.Name = "FormChat";
            this.Text = "在线咨询";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormChat_FormClosed);
            this.Load += new System.EventHandler(this.FormChat_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxTtasklog.ResumeLayout(false);
            this.groupBoxTtasklog.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GSSUI.DataGridViewUI dgvRoleList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxTtasklog;
        private GSSUI.AControl.ARichTextBox.ARichTextBox tboxMSGList;
        private GSSUI.AControl.AButton.AButton buttonOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private GSSUI.AControl.ARichTextBox.ARichTextBox tboxNote;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Label lblLoadingRole;
        private System.Windows.Forms.Label lblTaskID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLoadingMSGList;
        private System.Windows.Forms.Label lblRoleInfo;
        private GSSUI.AControl.AButton.AButton aButton1;
        private GSSUI.AControl.AButton.AButton aButton2;
        private GSSUI.AControl.AButton.AButton aButton3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewImageColumn ColumnTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSend;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lboxExample;
        private System.Windows.Forms.TextBox tboxRoleName;
        private GSSUI.AControl.AButton.AButton btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private GSSUI.AControl.AButton.AButton btnRlstClear;
        private GSSUI.AControl.AButton.AButton ButtonClose;
        private GSSUI.AControl.AButton.AButton btnCloseForm;
        private System.Windows.Forms.ListView lsvExample;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}