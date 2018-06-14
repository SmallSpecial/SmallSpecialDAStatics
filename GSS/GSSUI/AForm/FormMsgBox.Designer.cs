namespace GSSUI.AForm
{
    partial class FormMsgBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMsgBox));
            this.iconPic = new System.Windows.Forms.PictureBox();
            this.aButtonOK = new GSSUI.AControl.AButton.AButton();
            this.aButtonCancel = new GSSUI.AControl.AButton.AButton();
            this.labelMSG = new System.Windows.Forms.Label();
            this.labelMore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic)).BeginInit();
            this.SuspendLayout();
            // 
            // iconPic
            // 
            this.iconPic.BackColor = System.Drawing.Color.Transparent;
            this.iconPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.iconPic.Location = new System.Drawing.Point(26, 69);
            this.iconPic.Name = "iconPic";
            this.iconPic.Size = new System.Drawing.Size(62, 61);
            this.iconPic.TabIndex = 4;
            this.iconPic.TabStop = false;
            // 
            // aButtonOK
            // 
            this.aButtonOK.BackColor = System.Drawing.Color.Transparent;
            this.aButtonOK.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("aButtonOK.BackImg")));
            this.aButtonOK.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.aButtonOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aButtonOK.Location = new System.Drawing.Point(62, 148);
            this.aButtonOK.Name = "aButtonOK";
            this.aButtonOK.Size = new System.Drawing.Size(75, 23);
            this.aButtonOK.TabIndex = 0;
            this.aButtonOK.Text = LanguageResource.Language.BtnSure;
            this.aButtonOK.UseVisualStyleBackColor = true;
            this.aButtonOK.Click += new System.EventHandler(this.aButton1_Click);
            // 
            // aButtonCancel
            // 
            this.aButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.aButtonCancel.BackImg = ((System.Drawing.Bitmap)(resources.GetObject("aButtonCancel.BackImg")));
            this.aButtonCancel.BacklightLTRB = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.aButtonCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aButtonCancel.Location = new System.Drawing.Point(169, 148);
            this.aButtonCancel.Name = "aButtonCancel";
            this.aButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.aButtonCancel.TabIndex = 1;
            this.aButtonCancel.Text = "取消";
            this.aButtonCancel.UseVisualStyleBackColor = true;
            this.aButtonCancel.Visible = false;
            this.aButtonCancel.Click += new System.EventHandler(this.aButton2_Click);
            // 
            // labelMSG
            // 
            this.labelMSG.BackColor = System.Drawing.Color.Transparent;
            this.labelMSG.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMSG.Location = new System.Drawing.Point(94, 50);
            this.labelMSG.Name = "labelMSG";
            this.labelMSG.Size = new System.Drawing.Size(190, 82);
            this.labelMSG.TabIndex = 7;
            this.labelMSG.Text = "消息";
            this.labelMSG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelMore
            // 
            this.labelMore.AutoSize = true;
            this.labelMore.BackColor = System.Drawing.Color.Transparent;
            this.labelMore.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelMore.Location = new System.Drawing.Point(95, 132);
            this.labelMore.Name = "labelMore";
            this.labelMore.Size = new System.Drawing.Size(48, 17);
            this.labelMore.TabIndex = 8;
            this.labelMore.Text = "...... ......";
            this.labelMore.Visible = false;
            // 
            // FormMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BacklightImg = global::GSSUI.Properties.Resources.all_inside_bkg;
            this.BacklightLTRB = new System.Drawing.Rectangle(10, 60, 10, 60);
            this.ClientSize = new System.Drawing.Size(301, 181);
            this.Controls.Add(this.labelMore);
            this.Controls.Add(this.labelMSG);
            this.Controls.Add(this.aButtonCancel);
            this.Controls.Add(this.aButtonOK);
            this.Controls.Add(this.iconPic);
            this.FormSystemBtnSet = GSSUI.AForm.ABaseForm.FormSystemBtn.btn_miniAndbtn_close;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsResize = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormMsgBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "提示窗口";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMsgBox_FormClosing);
            this.Controls.SetChildIndex(this.iconPic, 0);
            this.Controls.SetChildIndex(this.aButtonOK, 0);
            this.Controls.SetChildIndex(this.aButtonCancel, 0);
            this.Controls.SetChildIndex(this.labelMSG, 0);
            this.Controls.SetChildIndex(this.labelMore, 0);
            ((System.ComponentModel.ISupportInitialize)(this.iconPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iconPic;
        private GSSUI.AControl.AButton.AButton aButtonOK;
        private GSSUI.AControl.AButton.AButton aButtonCancel;
        private System.Windows.Forms.Label labelMSG;
        private System.Windows.Forms.Label labelMore;
    }
}