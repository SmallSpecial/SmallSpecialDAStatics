using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSSClient
{
    public partial class FormTaskFlow : GSSUI.AForm.FormMain
    {
        public FormTaskFlow()
        {
            InitializeComponent();
            this.Text = LanguageResource.Language.LblWorkOrderFlow;
            this.Refresh();
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
    }
}
