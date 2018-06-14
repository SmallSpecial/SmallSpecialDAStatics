namespace GSSUI.AForm
{
    partial class ABaseForm
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
                _BacklightImg.Dispose();
                btn_closeImg.Dispose();
                btn_maxImg.Dispose();
                btn_miniImg.Dispose();
                btn_restoreImg.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ABaseForm));
            this.btn_restore = new GSSUI.AControl.AButton.ControlButton();
            this.btn_mini = new GSSUI.AControl.AButton.ControlButton();
            this.btn_max = new GSSUI.AControl.AButton.ControlButton();
            this.btn_close = new GSSUI.AControl.AButton.ControlButton();
            this.SuspendLayout();
            // 
            // btn_restore
            // 
            this.btn_restore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_restore.BackColor = System.Drawing.Color.Transparent;
            this.btn_restore.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btn_restore.BackImg")));
            this.btn_restore.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_restore.IsTabFocus = false;
            this.btn_restore.Location = new System.Drawing.Point(349, 30);
            this.btn_restore.Name = "btn_restore";
            this.btn_restore.Size = new System.Drawing.Size(25, 18);
            this.btn_restore.TabIndex = 55555;
            this.btn_restore.UseVisualStyleBackColor = true;
            this.btn_restore.Click += new System.EventHandler(this.btn_restore_Click);
            // 
            // btn_mini
            // 
            this.btn_mini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_mini.BackColor = System.Drawing.Color.Transparent;
            this.btn_mini.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btn_mini.BackImg")));
            this.btn_mini.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_mini.IsTabFocus = false;
            this.btn_mini.Location = new System.Drawing.Point(320, 6);
            this.btn_mini.Name = "btn_mini";
            this.btn_mini.Size = new System.Drawing.Size(25, 18);
            this.btn_mini.TabIndex = 22222;
            this.btn_mini.UseVisualStyleBackColor = true;
            this.btn_mini.Click += new System.EventHandler(this.btn_mini_Click);
            // 
            // btn_max
            // 
            this.btn_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_max.BackColor = System.Drawing.Color.Transparent;
            this.btn_max.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btn_max.BackImg")));
            this.btn_max.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_max.IsTabFocus = false;
            this.btn_max.Location = new System.Drawing.Point(349, 6);
            this.btn_max.Name = "btn_max";
            this.btn_max.Size = new System.Drawing.Size(25, 18);
            this.btn_max.TabIndex = 2223333;
            this.btn_max.UseVisualStyleBackColor = true;
            this.btn_max.Click += new System.EventHandler(this.btn_max_Click);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("btn_close.BackImg")));
            this.btn_close.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_close.IsTabFocus = false;
            this.btn_close.Location = new System.Drawing.Point(380, 6);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(38, 18);
            this.btn_close.TabIndex = 44444;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // ABaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(422, 305);
            this.Controls.Add(this.btn_restore);
            this.Controls.Add(this.btn_mini);
            this.Controls.Add(this.btn_max);
            this.Controls.Add(this.btn_close);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ABaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlBaseForm";
            this.ResumeLayout(false);

        }

        #endregion

        private GSSUI.AControl.AButton.ControlButton btn_close;
        private GSSUI.AControl.AButton.ControlButton btn_max;
        private GSSUI.AControl.AButton.ControlButton btn_mini;
        private GSSUI.AControl.AButton.ControlButton btn_restore;
    }
}