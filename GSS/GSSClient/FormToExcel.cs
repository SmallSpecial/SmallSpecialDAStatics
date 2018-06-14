using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace GSSClient
{
    public partial class FormToExcel : GSSUI.AForm.ABaseForm
    {
        DataGridView dg;
        string v;
        Thread thdSub;
        public FormToExcel(DataGridView _dg, string _v)
        {
            dg = _dg;
            v = _v;
            InitializeComponent();
        }


        public void start()
        {
            Export(dg, v);
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }
        public void Export(DataGridView dg, string v)
        {
            ExceUtil.ExportToCsv((DataTable)dg.DataSource, v);
        }

        bool ToExport = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ToExport)
            { 
             thdSub = new Thread(new ThreadStart(start));

            thdSub.Start();

            ToExport = false;
            }
            if (progressBar1.Value==progressBar1.Maximum)
            {
                progressBar1.Value = progressBar1.Minimum;
            }
            progressBar1.PerformStep();
           
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
          this.Close();
        }

    }
}
