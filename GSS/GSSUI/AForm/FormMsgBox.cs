using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GSSUI.AClass;
namespace GSSUI.AForm
{
    public partial class FormMsgBox : ABaseForm
    {
        private string message = "";
        private int mode = 1;
        DialogResult diaResult = DialogResult.Cancel;
        public FormMsgBox(string message, string title, MessageBoxButtons mbb, MessageBoxIcon mbi)
        {
            this.message=message;
            InitializeComponent();
            switch (mbb)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    break;
                case MessageBoxButtons.OK:
                    aButtonOK.Left = this.Width - aButtonOK.Width - 20;
                    break;
                case MessageBoxButtons.OKCancel:
                    mode = 1;
                    aButtonOK.Text = LanguageResource.Language.BtnSure;
                    aButtonCancel.Text = LanguageResource.Language.BtnCancel;
                    aButtonCancel.Visible = true;
                    break;
                case MessageBoxButtons.RetryCancel:
                    aButtonCancel.Visible = true;
                    aButtonOK.Visible = false;
                   // Retry.Visible = true;
                    break;
                case MessageBoxButtons.YesNo:
                    mode = 2;
                    //aButtonOK.Left = aButtonOK.Left - 30;
                    //aButtonCancel.Left = aButtonCancel.Left - 30;
                    aButtonOK.Text = LanguageResource.Language.BtnYes;
                    aButtonCancel.Text = LanguageResource.Language.BtnNo;
                    aButtonCancel.Visible = true;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    mode = 3;
                    aButtonOK.Text = LanguageResource.Language.BtnYes;
                    aButtonCancel.Text = LanguageResource.Language.BtnCancel;
                    aButtonCancel.Visible = true;
                    break;
            }
            switch (mbi)
            {
                case MessageBoxIcon.Asterisk:
                    iconPic.Image = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.sysmessagebox_inforFile.png");
                    break;
                case MessageBoxIcon.Error:
                    iconPic.Image = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.sysmessagebox_errorFile.png");
                    break;
                case MessageBoxIcon.Exclamation:
                    iconPic.Image = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.sysmessagebox_warningFile.png");
                    break;
                case MessageBoxIcon.None:
                    break;
                case MessageBoxIcon.Question:
                    iconPic.Image = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.sysmessagebox_questionFile.png");
                   // iconPic.BackgroundImage = ImageObject.GetResBitmap("GSSUI.ASkinImg.FormImg.sysmessagebox_questionFile.png");
                    break;
            }
            //if (message.Split('\n').Length > 6)
            //{
            //    labelMore.Visible = true;
            //}
            this.Text = title;
            labelMSG.Text = message;
        }

        private void aButton1_Click(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                this.diaResult = DialogResult.OK;
            }
            if (mode == 2)
            {
                this.diaResult = DialogResult.Yes;
            }
            this.Close();
        }

        private void aButton2_Click(object sender, EventArgs e)
        {
            if (mode == 2)
            {
                this.diaResult = DialogResult.No;
            }
            this.Close();
        }

        private void FormMsgBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = diaResult;
        }

    }
}
