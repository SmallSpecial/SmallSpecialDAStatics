﻿namespace GSSUI.AControl.ACorlorControl
{
    partial class ColorAreaUserControl
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
            this.SuspendLayout();
            // 
            // ColorAreaUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "ColorAreaUserControl";
            this.Size = new System.Drawing.Size(408, 172);
            this.MouseLeave += new System.EventHandler(this.ColorAreaUserControl_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorAreaUserControl_MouseMove);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorAreaUserControl_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorAreaUserControl_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
