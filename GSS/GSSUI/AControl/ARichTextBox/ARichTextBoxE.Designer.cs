namespace GSSUI.AControl.ARichTextBox
{
    partial class ARichTextBoxE
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.aRichTextBox1 = new GSSUI.AControl.ARichTextBox.ARichTextBox(this.components);
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 289);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // aRichTextBox1
            // 
            this.aRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aRichTextBox1.Location = new System.Drawing.Point(0, 0);
            this.aRichTextBox1.MaxLength = 3000;
            this.aRichTextBox1.Name = "aRichTextBox1";
            this.aRichTextBox1.Size = new System.Drawing.Size(439, 294);
            this.aRichTextBox1.TabIndex = 0;
            this.aRichTextBox1.Text = "";
            this.aRichTextBox1.VScroll += new System.EventHandler(this.aRichTextBox1_VScroll);
            this.aRichTextBox1.TextChanged += new System.EventHandler(this.aRichTextBox1_TextChanged);
            // 
            // ARichTextBoxE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.aRichTextBox1);
            this.Name = "ARichTextBoxE";
            this.Size = new System.Drawing.Size(439, 294);
            this.ResumeLayout(false);

        }

        #endregion

        private ARichTextBox aRichTextBox1;
        private System.Windows.Forms.Panel panel1;
    }
}
