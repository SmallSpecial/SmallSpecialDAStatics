namespace GSSClient
{
    partial class FormToolFDBI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToolFDBI));
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxZoneList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonOffLine = new System.Windows.Forms.RadioButton();
            this.radioButtonOnLine = new System.Windows.Forms.RadioButton();
            this.dataGridViewUIRequestList = new GSSUI.DataGridViewUI();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rboxOP_TIME = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonToExcel = new System.Windows.Forms.ToolStripButton();
            this.tboxOP_BAK = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tboxPARA_2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tboxPARA_1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tboxCID = new System.Windows.Forms.TextBox();
            this.tboxUID = new System.Windows.Forms.TextBox();
            this.comboBoxType1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxType0 = new System.Windows.Forms.ComboBox();
            this.ButtonReset = new GSSUI.AControl.AButton.AButton();
            this.ButtonRequest = new GSSUI.AControl.AButton.AButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxQuerySearch = new System.Windows.Forms.GroupBox();
            this.labelTool0 = new System.Windows.Forms.Label();
            this.GroupBoxQuerySql = new System.Windows.Forms.GroupBox();
            this.RichTextBoxSql = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.ButtonSqlQuery = new GSSUI.AControl.AButton.AButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.labelTool3 = new System.Windows.Forms.Label();
            this.comboBoxBigZoneList = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUIRequestList)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxQuerySearch.SuspendLayout();
            this.GroupBoxQuerySql.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(17, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "战区列表:";
            // 
            // comboBoxZoneList
            // 
            this.comboBoxZoneList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZoneList.FormattingEnabled = true;
            this.comboBoxZoneList.Items.AddRange(new object[] {
            "请选择.."});
            this.comboBoxZoneList.Location = new System.Drawing.Point(193, 53);
            this.comboBoxZoneList.Name = "comboBoxZoneList";
            this.comboBoxZoneList.Size = new System.Drawing.Size(126, 20);
            this.comboBoxZoneList.TabIndex = 4;
            this.comboBoxZoneList.Tag = global::GSSClient.Properties.Resources.d;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(325, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "查询方式:";
            // 
            // radioButtonOffLine
            // 
            this.radioButtonOffLine.AutoSize = true;
            this.radioButtonOffLine.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonOffLine.Location = new System.Drawing.Point(390, 54);
            this.radioButtonOffLine.Name = "radioButtonOffLine";
            this.radioButtonOffLine.Size = new System.Drawing.Size(71, 16);
            this.radioButtonOffLine.TabIndex = 7;
            this.radioButtonOffLine.Text = "离线查询";
            this.radioButtonOffLine.UseVisualStyleBackColor = false;
            // 
            // radioButtonOnLine
            // 
            this.radioButtonOnLine.AutoSize = true;
            this.radioButtonOnLine.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonOnLine.Checked = true;
            this.radioButtonOnLine.Location = new System.Drawing.Point(467, 54);
            this.radioButtonOnLine.Name = "radioButtonOnLine";
            this.radioButtonOnLine.Size = new System.Drawing.Size(71, 16);
            this.radioButtonOnLine.TabIndex = 7;
            this.radioButtonOnLine.TabStop = true;
            this.radioButtonOnLine.Text = "实时查询";
            this.radioButtonOnLine.UseVisualStyleBackColor = false;
            // 
            // dataGridViewUIRequestList
            // 
            this.dataGridViewUIRequestList.AllowUserToAddRows = false;
            this.dataGridViewUIRequestList.AllowUserToDeleteRows = false;
            this.dataGridViewUIRequestList.AllowUserToResizeRows = false;
            this.dataGridViewUIRequestList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewUIRequestList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUIRequestList.Location = new System.Drawing.Point(6, 20);
            this.dataGridViewUIRequestList.Name = "dataGridViewUIRequestList";
            this.dataGridViewUIRequestList.ReadOnly = true;
            this.dataGridViewUIRequestList.RowHeadersVisible = false;
            this.dataGridViewUIRequestList.RowTemplate.Height = 23;
            this.dataGridViewUIRequestList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUIRequestList.Size = new System.Drawing.Size(851, 339);
            this.dataGridViewUIRequestList.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.rboxOP_TIME);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.tboxOP_BAK);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.tboxPARA_2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tboxPARA_1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tboxCID);
            this.panel1.Controls.Add(this.tboxUID);
            this.panel1.Controls.Add(this.comboBoxType1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.comboBoxType0);
            this.panel1.Controls.Add(this.ButtonReset);
            this.panel1.Controls.Add(this.ButtonRequest);
            this.panel1.Location = new System.Drawing.Point(5, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 63);
            this.panel1.TabIndex = 9;
            // 
            // rboxOP_TIME
            // 
            this.rboxOP_TIME.CustomFormat = "yyyy-MM-dd";
            this.rboxOP_TIME.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.rboxOP_TIME.Location = new System.Drawing.Point(608, 7);
            this.rboxOP_TIME.Name = "rboxOP_TIME";
            this.rboxOP_TIME.Size = new System.Drawing.Size(134, 21);
            this.rboxOP_TIME.TabIndex = 9;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonToExcel});
            this.toolStrip1.Location = new System.Drawing.Point(752, 32);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(87, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonToExcel
            // 
            this.toolStripButtonToExcel.Image = global::GSSClient.Properties.Resources.OK;
            this.toolStripButtonToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonToExcel.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripButtonToExcel.Name = "toolStripButtonToExcel";
            this.toolStripButtonToExcel.Size = new System.Drawing.Size(79, 22);
            this.toolStripButtonToExcel.Text = "导出EXCEL";
            this.toolStripButtonToExcel.Click += new System.EventHandler(this.toolStripButtonToExcel_Click);
            // 
            // tboxOP_BAK
            // 
            this.tboxOP_BAK.Location = new System.Drawing.Point(386, 36);
            this.tboxOP_BAK.Name = "tboxOP_BAK";
            this.tboxOP_BAK.Size = new System.Drawing.Size(170, 21);
            this.tboxOP_BAK.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(309, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 7;
            this.label13.Text = "对象方描述:";
            // 
            // tboxPARA_2
            // 
            this.tboxPARA_2.Location = new System.Drawing.Point(203, 36);
            this.tboxPARA_2.Name = "tboxPARA_2";
            this.tboxPARA_2.Size = new System.Drawing.Size(100, 21);
            this.tboxPARA_2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(156, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "参数2:";
            // 
            // tboxPARA_1
            // 
            this.tboxPARA_1.Location = new System.Drawing.Point(66, 36);
            this.tboxPARA_1.Name = "tboxPARA_1";
            this.tboxPARA_1.Size = new System.Drawing.Size(84, 21);
            this.tboxPARA_1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(19, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "参数1:";
            // 
            // tboxCID
            // 
            this.tboxCID.Location = new System.Drawing.Point(481, 8);
            this.tboxCID.Name = "tboxCID";
            this.tboxCID.Size = new System.Drawing.Size(75, 21);
            this.tboxCID.TabIndex = 6;
            // 
            // tboxUID
            // 
            this.tboxUID.Location = new System.Drawing.Point(338, 8);
            this.tboxUID.Name = "tboxUID";
            this.tboxUID.Size = new System.Drawing.Size(78, 21);
            this.tboxUID.TabIndex = 6;
            // 
            // comboBoxType1
            // 
            this.comboBoxType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType1.FormattingEnabled = true;
            this.comboBoxType1.Location = new System.Drawing.Point(156, 9);
            this.comboBoxType1.Name = "comboBoxType1";
            this.comboBoxType1.Size = new System.Drawing.Size(117, 20);
            this.comboBoxType1.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(279, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "帐号UID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(567, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "日期:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(422, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "角色CID:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(5, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "查询类型:";
            // 
            // comboBoxType0
            // 
            this.comboBoxType0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType0.FormattingEnabled = true;
            this.comboBoxType0.Items.AddRange(new object[] {
            "道具操作"});
            this.comboBoxType0.Location = new System.Drawing.Point(66, 8);
            this.comboBoxType0.Name = "comboBoxType0";
            this.comboBoxType0.Size = new System.Drawing.Size(84, 20);
            this.comboBoxType0.TabIndex = 4;
            this.comboBoxType0.SelectedIndexChanged += new System.EventHandler(this.comboBoxType0_SelectedIndexChanged);
            // 
            // ButtonReset
            // 
            this.ButtonReset.BackColor = System.Drawing.Color.Transparent;
            this.ButtonReset.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("ButtonReset.BackImg")));
            this.ButtonReset.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ButtonReset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonReset.Location = new System.Drawing.Point(655, 34);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(87, 23);
            this.ButtonReset.TabIndex = 3;
            this.ButtonReset.Text = LanguageResource.Language.LblReset;
            this.ButtonReset.UseVisualStyleBackColor = true;
            this.ButtonReset.Click += new System.EventHandler(this.button2_Click);
            // 
            // ButtonRequest
            // 
            this.ButtonRequest.BackColor = System.Drawing.Color.Transparent;
            this.ButtonRequest.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("ButtonRequest.BackImg")));
            this.ButtonRequest.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ButtonRequest.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonRequest.Location = new System.Drawing.Point(562, 34);
            this.ButtonRequest.Name = "ButtonRequest";
            this.ButtonRequest.Size = new System.Drawing.Size(87, 23);
            this.ButtonRequest.TabIndex = 3;
            this.ButtonRequest.Text = "查询";
            this.ButtonRequest.UseVisualStyleBackColor = true;
            this.ButtonRequest.Click += new System.EventHandler(this.ButtonRequest_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Location = new System.Drawing.Point(574, -449);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 76);
            this.panel2.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(590, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(444, 9);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(75, 21);
            this.textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(307, 9);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(78, 21);
            this.textBox3.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(525, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "道具名称:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(160, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(88, 20);
            this.comboBox1.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(391, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "道具ID:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(254, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "角色ID:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(3, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = LanguageResource.Language.LblType;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(44, 9);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(110, 20);
            this.comboBox2.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dataGridViewUIRequestList);
            this.groupBox1.Location = new System.Drawing.Point(11, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(863, 365);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询结果";
            // 
            // groupBoxQuerySearch
            // 
            this.groupBoxQuerySearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxQuerySearch.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxQuerySearch.Controls.Add(this.panel1);
            this.groupBoxQuerySearch.Location = new System.Drawing.Point(11, 76);
            this.groupBoxQuerySearch.Name = "groupBoxQuerySearch";
            this.groupBoxQuerySearch.Size = new System.Drawing.Size(863, 88);
            this.groupBoxQuerySearch.TabIndex = 11;
            this.groupBoxQuerySearch.TabStop = false;
            this.groupBoxQuerySearch.Text = "FDBI日志查询";
            // 
            // labelTool0
            // 
            this.labelTool0.AutoSize = true;
            this.labelTool0.BackColor = System.Drawing.Color.Transparent;
            this.labelTool0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelTool0.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTool0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTool0.Location = new System.Drawing.Point(663, 53);
            this.labelTool0.Name = "labelTool0";
            this.labelTool0.Size = new System.Drawing.Size(110, 16);
            this.labelTool0.TabIndex = 12;
            this.labelTool0.Text = "→自定义查询";
            this.labelTool0.MouseLeave += new System.EventHandler(this.labelTool1_MouseLeave);
            this.labelTool0.Click += new System.EventHandler(this.labelTool0_Click);
            this.labelTool0.MouseEnter += new System.EventHandler(this.labelTool1_MouseEnter);
            // 
            // GroupBoxQuerySql
            // 
            this.GroupBoxQuerySql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxQuerySql.BackColor = System.Drawing.Color.Transparent;
            this.GroupBoxQuerySql.Controls.Add(this.RichTextBoxSql);
            this.GroupBoxQuerySql.Controls.Add(this.ButtonSqlQuery);
            this.GroupBoxQuerySql.Controls.Add(this.toolStrip2);
            this.GroupBoxQuerySql.Location = new System.Drawing.Point(12, -622);
            this.GroupBoxQuerySql.Name = "GroupBoxQuerySql";
            this.GroupBoxQuerySql.Size = new System.Drawing.Size(863, 88);
            this.GroupBoxQuerySql.TabIndex = 14;
            this.GroupBoxQuerySql.TabStop = false;
            this.GroupBoxQuerySql.Text = "自定义SQL语句";
            // 
            // RichTextBoxSql
            // 
            this.RichTextBoxSql.Location = new System.Drawing.Point(6, 20);
            this.RichTextBoxSql.Name = "RichTextBoxSql";
            this.RichTextBoxSql.Size = new System.Drawing.Size(693, 62);
            this.RichTextBoxSql.TabIndex = 5;
            this.RichTextBoxSql.Text = global::GSSClient.Properties.Resources.d;
            // 
            // ButtonSqlQuery
            // 
            this.ButtonSqlQuery.BackColor = System.Drawing.Color.Transparent;
            this.ButtonSqlQuery.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("ButtonSqlQuery.BackImg")));
            this.ButtonSqlQuery.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ButtonSqlQuery.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButtonSqlQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonSqlQuery.Location = new System.Drawing.Point(705, 47);
            this.ButtonSqlQuery.Name = "ButtonSqlQuery";
            this.ButtonSqlQuery.Size = new System.Drawing.Size(143, 29);
            this.ButtonSqlQuery.TabIndex = 3;
            this.ButtonSqlQuery.Text = "执  行";
            this.ButtonSqlQuery.UseVisualStyleBackColor = true;
            this.ButtonSqlQuery.Click += new System.EventHandler(this.ButtonDoSqlQuery_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(698, 19);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(159, 25);
            this.toolStrip2.TabIndex = 19;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::GSSClient.Properties.Resources.People;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton3.Text = "查询模板";
            this.toolStripButton3.Click += new System.EventHandler(this.labelTool1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::GSSClient.Properties.Resources.OK;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton2.Text = "导出EXCEL";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButtonToExcel_Click);
            // 
            // labelTool3
            // 
            this.labelTool3.AutoSize = true;
            this.labelTool3.BackColor = System.Drawing.Color.Transparent;
            this.labelTool3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTool3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelTool3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTool3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelTool3.Location = new System.Drawing.Point(560, 53);
            this.labelTool3.Name = "labelTool3";
            this.labelTool3.Size = new System.Drawing.Size(97, 18);
            this.labelTool3.TabIndex = 15;
            this.labelTool3.Text = "→FDBI查询";
            this.labelTool3.MouseLeave += new System.EventHandler(this.labelTool1_MouseLeave);
            this.labelTool3.Click += new System.EventHandler(this.labelTool3_Click);
            this.labelTool3.MouseEnter += new System.EventHandler(this.labelTool1_MouseEnter);
            // 
            // comboBoxBigZoneList
            // 
            this.comboBoxBigZoneList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBigZoneList.FormattingEnabled = true;
            this.comboBoxBigZoneList.Location = new System.Drawing.Point(82, 53);
            this.comboBoxBigZoneList.Name = "comboBoxBigZoneList";
            this.comboBoxBigZoneList.Size = new System.Drawing.Size(105, 20);
            this.comboBoxBigZoneList.TabIndex = 16;
            this.comboBoxBigZoneList.SelectedIndexChanged += new System.EventHandler(this.comboBoxBigZoneList_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormToolFDBI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 572);
            this.Controls.Add(this.comboBoxBigZoneList);
            this.Controls.Add(this.labelTool3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelTool0);
            this.Controls.Add(this.groupBoxQuerySearch);
            this.Controls.Add(this.radioButtonOnLine);
            this.Controls.Add(this.GroupBoxQuerySql);
            this.Controls.Add(this.radioButtonOffLine);
            this.Controls.Add(this.comboBoxZoneList);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsResize = true;
            this.Name = "FormToolFDBI";
            this.Text = "DBIS工具";
            this.Load += new System.EventHandler(this.FormToolGuserLock_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormToolGuserLock_FormClosing);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.comboBoxZoneList, 0);
            this.Controls.SetChildIndex(this.radioButtonOffLine, 0);
            this.Controls.SetChildIndex(this.GroupBoxQuerySql, 0);
            this.Controls.SetChildIndex(this.radioButtonOnLine, 0);
            this.Controls.SetChildIndex(this.groupBoxQuerySearch, 0);
            this.Controls.SetChildIndex(this.labelTool0, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.labelTool3, 0);
            this.Controls.SetChildIndex(this.comboBoxBigZoneList, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUIRequestList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBoxQuerySearch.ResumeLayout(false);
            this.GroupBoxQuerySql.ResumeLayout(false);
            this.GroupBoxQuerySql.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxZoneList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonOffLine;
        private System.Windows.Forms.RadioButton radioButtonOnLine;
        private GSSUI.DataGridViewUI dataGridViewUIRequestList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxType1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxType0;
        private System.Windows.Forms.TextBox tboxCID;
        private System.Windows.Forms.TextBox tboxUID;
        private System.Windows.Forms.Label label8;
        private GSSUI.AControl.AButton.AButton ButtonReset;
        private GSSUI.AControl.AButton.AButton ButtonRequest;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxQuerySearch;
        private System.Windows.Forms.Label labelTool0;
        private System.Windows.Forms.GroupBox GroupBoxQuerySql;
        private GSSUI.AControl.ARichTextBox.ARichTextBox RichTextBoxSql;
        private GSSUI.AControl.AButton.AButton ButtonSqlQuery;
        private System.Windows.Forms.Label labelTool3;
        private System.Windows.Forms.TextBox tboxOP_BAK;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tboxPARA_2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tboxPARA_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxBigZoneList;
        private System.Windows.Forms.DateTimePicker rboxOP_TIME;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonToExcel;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}