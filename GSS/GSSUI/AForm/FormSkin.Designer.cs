namespace GSSUI.AForm
{
    partial class FormSkin
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
            this.colorAreaAndSliderUserControl1 = new GSSUI.AControl.ACorlorControl.ColorAreaAndSliderUserControl();
            this.aPictrueBoxColor = new GSSUI.AControl.APictrueBox.APictrueBox(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.PicB1 = new GSSUI.AControl.APictrueBox.APictrueBox(this.components);
            this.PicB4 = new GSSUI.AControl.APictrueBox.APictrueBox(this.components);
            this.PicB2 = new GSSUI.AControl.APictrueBox.APictrueBox(this.components);
            this.PicB3 = new GSSUI.AControl.APictrueBox.APictrueBox(this.components);
            this.PicB5 = new GSSUI.AControl.APictrueBox.APictrueBox(this.components);
            this.aPictrueBoxShade = new GSSUI.AControl.APictrueBox.APictrueBox(this.components);
            this.colorSliderUserControl1 = new GSSUI.AControl.ACorlorControl.ColorSliderUserControl();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.lblMostTop = new System.Windows.Forms.Label();
            this.lblDefaultSkin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.aPictrueBoxColor)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicB4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicB2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicB3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicB5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aPictrueBoxShade)).BeginInit();
            this.SuspendLayout();
            // 
            // colorAreaAndSliderUserControl1
            // 
            this.colorAreaAndSliderUserControl1.Location = new System.Drawing.Point(9, 43);
            this.colorAreaAndSliderUserControl1.Name = "colorAreaAndSliderUserControl1";
            this.colorAreaAndSliderUserControl1.Size = new System.Drawing.Size(237, 85);
            this.colorAreaAndSliderUserControl1.TabIndex = 10;
            this.colorAreaAndSliderUserControl1.Visible = false;
            this.colorAreaAndSliderUserControl1.ColorChanged += new System.EventHandler(this.colorAreaAndSliderUserControl1_ColorChanged);
            // 
            // aPictrueBoxColor
            // 
            this.aPictrueBoxColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aPictrueBoxColor.Location = new System.Drawing.Point(54, 8);
            this.aPictrueBoxColor.Name = "aPictrueBoxColor";
            this.aPictrueBoxColor.NormalImg = null;
            this.aPictrueBoxColor.OverImg = null;
            this.aPictrueBoxColor.Size = new System.Drawing.Size(31, 31);
            this.aPictrueBoxColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.aPictrueBoxColor.TabIndex = 8;
            this.aPictrueBoxColor.TabStop = false;
            this.toolTip2.SetToolTip(this.aPictrueBoxColor, "颜色");
            this.aPictrueBoxColor.MouseLeave += new System.EventHandler(this.aPictrueBoxShade_MouseLeave);
            this.aPictrueBoxColor.Click += new System.EventHandler(this.aPictrueBoxColor_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.PicB1);
            this.panel1.Controls.Add(this.PicB4);
            this.panel1.Controls.Add(this.PicB2);
            this.panel1.Controls.Add(this.PicB3);
            this.panel1.Controls.Add(this.PicB5);
            this.panel1.Location = new System.Drawing.Point(11, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 81);
            this.panel1.TabIndex = 7;
            // 
            // PicB1
            // 
            this.PicB1.Image = global::GSSUI.Properties.Resources.w7;
            this.PicB1.Location = new System.Drawing.Point(5, 12);
            this.PicB1.Name = "PicB1";
            this.PicB1.NormalImg = global::GSSUI.Properties.Resources.w7;
            this.PicB1.OverImg = null;
            this.PicB1.Size = new System.Drawing.Size(40, 53);
            this.PicB1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicB1.TabIndex = 16;
            this.PicB1.TabStop = false;
            this.PicB1.Click += new System.EventHandler(this.PicB1_Click);
            // 
            // PicB4
            // 
            this.PicB4.Image = global::GSSUI.Properties.Resources.w4;
            this.PicB4.Location = new System.Drawing.Point(143, 12);
            this.PicB4.Name = "PicB4";
            this.PicB4.NormalImg = global::GSSUI.Properties.Resources.w4;
            this.PicB4.OverImg = null;
            this.PicB4.Size = new System.Drawing.Size(40, 53);
            this.PicB4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicB4.TabIndex = 15;
            this.PicB4.TabStop = false;
            this.PicB4.Click += new System.EventHandler(this.PicB4_Click);
            // 
            // PicB2
            // 
            this.PicB2.Image = global::GSSUI.Properties.Resources.w2;
            this.PicB2.Location = new System.Drawing.Point(51, 12);
            this.PicB2.Name = "PicB2";
            this.PicB2.NormalImg = global::GSSUI.Properties.Resources.w2;
            this.PicB2.OverImg = null;
            this.PicB2.Size = new System.Drawing.Size(40, 53);
            this.PicB2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicB2.TabIndex = 14;
            this.PicB2.TabStop = false;
            this.PicB2.Click += new System.EventHandler(this.PicB2_Click);
            // 
            // PicB3
            // 
            this.PicB3.Image = global::GSSUI.Properties.Resources.w3;
            this.PicB3.Location = new System.Drawing.Point(97, 12);
            this.PicB3.Name = "PicB3";
            this.PicB3.NormalImg = global::GSSUI.Properties.Resources.w3;
            this.PicB3.OverImg = null;
            this.PicB3.Size = new System.Drawing.Size(40, 53);
            this.PicB3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicB3.TabIndex = 13;
            this.PicB3.TabStop = false;
            this.PicB3.Click += new System.EventHandler(this.PicB3_Click);
            // 
            // PicB5
            // 
            this.PicB5.Image = global::GSSUI.Properties.Resources.w1;
            this.PicB5.Location = new System.Drawing.Point(189, 12);
            this.PicB5.Name = "PicB5";
            this.PicB5.NormalImg = global::GSSUI.Properties.Resources.w1;
            this.PicB5.OverImg = null;
            this.PicB5.Size = new System.Drawing.Size(40, 53);
            this.PicB5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicB5.TabIndex = 12;
            this.PicB5.TabStop = false;
            this.PicB5.Click += new System.EventHandler(this.PicB5_Click);
            // 
            // aPictrueBoxShade
            // 
            this.aPictrueBoxShade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aPictrueBoxShade.Location = new System.Drawing.Point(12, 8);
            this.aPictrueBoxShade.Name = "aPictrueBoxShade";
            this.aPictrueBoxShade.NormalImg = null;
            this.aPictrueBoxShade.OverImg = null;
            this.aPictrueBoxShade.Size = new System.Drawing.Size(31, 31);
            this.aPictrueBoxShade.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.aPictrueBoxShade.TabIndex = 11;
            this.aPictrueBoxShade.TabStop = false;
            this.toolTip2.SetToolTip(this.aPictrueBoxShade, "皮肤");
            this.aPictrueBoxShade.MouseLeave += new System.EventHandler(this.aPictrueBoxShade_MouseLeave);
            this.aPictrueBoxShade.Click += new System.EventHandler(this.aPictrueBoxShade_Click);
            // 
            // colorSliderUserControl1
            // 
            this.colorSliderUserControl1.Location = new System.Drawing.Point(12, 129);
            this.colorSliderUserControl1.Name = "colorSliderUserControl1";
            this.colorSliderUserControl1.Size = new System.Drawing.Size(232, 11);
            this.colorSliderUserControl1.TabIndex = 12;
            this.toolTip2.SetToolTip(this.colorSliderUserControl1, "调整窗体透明度");
            this.colorSliderUserControl1.ValueChangedByUser += new System.EventHandler(this.colorSliderUserControl1_ValueChangedByUser);
            // 
            // lblMostTop
            // 
            this.lblMostTop.AutoSize = true;
            this.lblMostTop.Font = new System.Drawing.Font("宋体", 8.5F, System.Drawing.FontStyle.Underline);
            this.lblMostTop.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblMostTop.Location = new System.Drawing.Point(167, 23);
            this.lblMostTop.Name = "lblMostTop";
            this.lblMostTop.Size = new System.Drawing.Size(77, 12);
            this.lblMostTop.TabIndex = 13;
            this.lblMostTop.Text = "设置窗体最前";
            this.lblMostTop.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblDefaultSkin
            // 
            this.lblDefaultSkin.AutoSize = true;
            this.lblDefaultSkin.Font = new System.Drawing.Font("宋体", 8.5F, System.Drawing.FontStyle.Underline);
            this.lblDefaultSkin.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblDefaultSkin.Location = new System.Drawing.Point(106, 23);
            this.lblDefaultSkin.Name = "lblDefaultSkin";
            this.lblDefaultSkin.Size = new System.Drawing.Size(53, 12);
            this.lblDefaultSkin.TabIndex = 14;
            this.lblDefaultSkin.Text = "默认皮肤";
            this.lblDefaultSkin.Click += new System.EventHandler(this.lblDefaultSkin_Click);
            // 
            // FormSkin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(256, 150);
            this.Controls.Add(this.lblDefaultSkin);
            this.Controls.Add(this.lblMostTop);
            this.Controls.Add(this.colorSliderUserControl1);
            this.Controls.Add(this.aPictrueBoxShade);
            this.Controls.Add(this.aPictrueBoxColor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.colorAreaAndSliderUserControl1);
            this.FormSystemBtnSet = GSSUI.AForm.ABaseForm.FormSystemBtn.SystemNo;
            this.IsResize = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormSkin";
            this.Opacity = 0.93;
            this.ShowInTaskbar = false;
            this.Text = "";
            this.TopMost = true;
            this.UseIcon = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSkin_FormClosed);
            this.Controls.SetChildIndex(this.colorAreaAndSliderUserControl1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.aPictrueBoxColor, 0);
            this.Controls.SetChildIndex(this.aPictrueBoxShade, 0);
            this.Controls.SetChildIndex(this.colorSliderUserControl1, 0);
            this.Controls.SetChildIndex(this.lblMostTop, 0);
            this.Controls.SetChildIndex(this.lblDefaultSkin, 0);
            ((System.ComponentModel.ISupportInitialize)(this.aPictrueBoxColor)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicB4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicB2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicB3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicB5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aPictrueBoxShade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GSSUI.AControl.ACorlorControl.ColorAreaAndSliderUserControl colorAreaAndSliderUserControl1;
        private GSSUI.AControl.APictrueBox.APictrueBox aPictrueBoxColor;
        private System.Windows.Forms.Panel panel1;
        private GSSUI.AControl.APictrueBox.APictrueBox aPictrueBoxShade;
        private GSSUI.AControl.APictrueBox.APictrueBox PicB5;
        private GSSUI.AControl.ACorlorControl.ColorSliderUserControl colorSliderUserControl1;
        private GSSUI.AControl.APictrueBox.APictrueBox PicB4;
        private GSSUI.AControl.APictrueBox.APictrueBox PicB2;
        private GSSUI.AControl.APictrueBox.APictrueBox PicB3;
        private GSSUI.AControl.APictrueBox.APictrueBox PicB1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label lblMostTop;
        private System.Windows.Forms.Label lblDefaultSkin;
    }
}