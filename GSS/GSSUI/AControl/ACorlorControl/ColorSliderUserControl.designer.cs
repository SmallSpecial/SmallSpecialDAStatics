namespace GSSUI.AControl.ACorlorControl
{
    partial class ColorSliderUserControl
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
            this.arrowControl = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.arrowControl)).BeginInit();
            this.SuspendLayout();
            // 
            // arrowControl
            // 
            this.arrowControl.BackColor = System.Drawing.Color.Transparent;
            this.arrowControl.Image = global::GSSUI.Properties.Resources.colorslider_dragbackground;
            this.arrowControl.Location = new System.Drawing.Point(41, -1);
            this.arrowControl.Name = "arrowControl";
            this.arrowControl.Size = new System.Drawing.Size(12, 12);
            this.arrowControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.arrowControl.TabIndex = 0;
            this.arrowControl.TabStop = false;
            this.arrowControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.arrowControl_MouseMove);
            this.arrowControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.arrowControl_MouseDown);
            // 
            // ColorSliderUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.arrowControl);
            this.Name = "ColorSliderUserControl";
            this.Size = new System.Drawing.Size(196, 12);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorSliderUserControl_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorSliderUserControl_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.arrowControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox arrowControl;
    }
}
