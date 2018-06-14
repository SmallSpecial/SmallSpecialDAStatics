namespace GSSUI.AForm
{
    partial class FormMainSkin
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
            this.picBoxSkin = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSkin)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxSkin
            // 
            this.picBoxSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxSkin.BackColor = System.Drawing.Color.Transparent;
            this.picBoxSkin.Location = new System.Drawing.Point(482, 8);
            this.picBoxSkin.Name = "picBoxSkin";
            this.picBoxSkin.Size = new System.Drawing.Size(20, 20);
            this.picBoxSkin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBoxSkin.TabIndex = 4;
            this.picBoxSkin.TabStop = false;
            this.picBoxSkin.MouseLeave += new System.EventHandler(this.picBoxSkin_MouseLeave);
            this.picBoxSkin.Click += new System.EventHandler(this.picBoxSkin_Click);
            this.picBoxSkin.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoxSkin_Paint);
            this.picBoxSkin.MouseEnter += new System.EventHandler(this.picBoxSkin_MouseEnter);
            // 
            // FormMainSkin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BacklightImg = global::GSSUI.Properties.Resources.aio_png_bkg1;
            this.BacklightLTRB = new System.Drawing.Rectangle(8, 60, 8, 80);
            this.ClientSize = new System.Drawing.Size(600, 446);
            this.Controls.Add(this.picBoxSkin);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormMainSkin";
            this.Text = "FormMain1";
            this.Load += new System.EventHandler(this.FormMain1_Load);
            this.Controls.SetChildIndex(this.picBoxSkin, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSkin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxSkin;
    }
}