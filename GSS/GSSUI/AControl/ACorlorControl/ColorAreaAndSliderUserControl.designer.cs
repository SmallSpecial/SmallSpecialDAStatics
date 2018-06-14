namespace GSSUI.AControl.ACorlorControl
{
    partial class ColorAreaAndSliderUserControl
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
            this.colorSliderUserControl = new GSSUI.AControl.ACorlorControl.ColorSliderUserControl();
            this.colorAreaUserControl = new GSSUI.AControl.ACorlorControl.ColorAreaUserControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // colorSliderUserControl
            // 
            this.colorSliderUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorSliderUserControl.Location = new System.Drawing.Point(3, 72);
            this.colorSliderUserControl.Name = "colorSliderUserControl";
            this.colorSliderUserControl.Size = new System.Drawing.Size(221, 11);
            this.colorSliderUserControl.TabIndex = 1;
            this.toolTip1.SetToolTip(this.colorSliderUserControl, "调整明暗度");
            this.colorSliderUserControl.ValueChanged += new System.EventHandler(this.colorSliderUserControl_ValueChanged);
            this.colorSliderUserControl.ValueChangedByUser += new System.EventHandler(this.colorSliderUserControl_ValueChangedByUser);
            // 
            // colorAreaUserControl
            // 
            this.colorAreaUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorAreaUserControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorAreaUserControl.Location = new System.Drawing.Point(3, 3);
            this.colorAreaUserControl.Name = "colorAreaUserControl";
            this.colorAreaUserControl.Size = new System.Drawing.Size(220, 66);
            this.colorAreaUserControl.TabIndex = 0;
            this.toolTip1.SetToolTip(this.colorAreaUserControl, "调整颜色");
            this.colorAreaUserControl.HueSaturationChanged += new System.EventHandler(this.colorAreaUserControl1_HueSaturationChanged);
            this.colorAreaUserControl.ValueChangedByUser += new System.EventHandler(this.colorAreaUserControl1_ValueChangedByUser);
            // 
            // ColorAreaAndSliderUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorSliderUserControl);
            this.Controls.Add(this.colorAreaUserControl);
            this.Name = "ColorAreaAndSliderUserControl";
            this.Size = new System.Drawing.Size(226, 86);
            this.ResumeLayout(false);

        }

        #endregion

        private ColorAreaUserControl colorAreaUserControl;
        private ColorSliderUserControl colorSliderUserControl;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
