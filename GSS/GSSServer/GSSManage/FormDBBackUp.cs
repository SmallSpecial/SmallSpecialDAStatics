using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSSServer.GSSManage
{
    public partial class FormDBBackUp : Form
    {
        public FormDBBackUp()
        {
            InitializeComponent();
        }
        private void FormDBBackUp_Load(object sender, EventArgs e)
        {
            textBox2.Text = Application.StartupPath + @"\DbBack\";
            textBox1.Text = DateTime.Today.ToString("yyyy-M-d");
        }
        private void buttonSure_Click(object sender, EventArgs e)
        {
            //if (textBoxX1.Text == "")
            //{
            //    MessageBoxEx.Show("请输入要备份的数据库名称！");
            //    return;
            //}
            //try
            //{
            //    FH.SQLBACK(".",
            //        ConfigurationManager.ConnectionStrings["UserNmae"].ConnectionString,
            //        ConfigurationManager.ConnectionStrings["PassWord"].ConnectionString,
            //        "CarsManagerment",
            //        textBoxX2.Text + textBoxX1.Text + ".bak"
            //        );
            //    MessageBoxEx.Show("数据库备份成功！");
            //    this.Close();
            //}
            //catch
            //{
            //    MessageBoxEx.Show("数据库备份失败！");
            //}
        }

        private void buttonESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
