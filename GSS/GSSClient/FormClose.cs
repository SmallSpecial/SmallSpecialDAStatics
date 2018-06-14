using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSSUI;
using GSSUI.AForm;

namespace GSSClient
{
    public partial class FormClose : GSSUI.AForm.ABaseForm
    {
        #region 私有变量
        private DialogResult IsOK;
        private Point _local;
        private Size _size;
        #endregion
        public FormClose(Point local, Size size)
        {
            _local = local;
            _size = size;
            InitializeComponent();
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClose));
            //this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
        }

        private void FormClose_Load(object sender, EventArgs e)
        {
            this.Location = _local;
            this.Size = _size;
            this.FadeTime = 1.05;
            this.TargetOpacity = 0.65;
            timer1.Enabled = true;
        }

        private void FormClose_Shown(object sender, EventArgs e)
        {
            //IsOK = MsgBox.Show("确定要退出本系统吗?", LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            //Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_CLOSE, 0);

        }

        private void FormClose_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = IsOK;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity == this.TargetOpacity)
            {
                timer1.Enabled = false;
                IsOK = MsgBox.Show(LanguageResource.Language.Tip_SureExitApp, LanguageResource.Language.Tip_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_CLOSE, 0);
               // this.Close();

            }
        }
    }
}
