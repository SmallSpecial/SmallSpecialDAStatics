using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSS.DBUtility;

namespace GSSTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 1)
            {
                try
                {
                    if (textBox2.Text.Trim().Length > 1)
                    {
                        DbHelperSQLite.connectionString = "Data Source=" + textBox1.Text.Trim() + ";Version=3;Password=" + textBox2.Text.Trim() + "";
                    }
                    else
                    {
                        DbHelperSQLite.connectionString = "Data Source=" + textBox1.Text.Trim() + ";Version=3;";
                    }
                    DbHelperSQLite.ChangePSW(textBox3.Text.Trim());
                    MessageBox.Show("修改密码成功!");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("修改密码失败!");
                }

            }
            else
            {
                MessageBox.Show("请选择SQLITE文件!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
                MessageBox.Show("请输入原文密码!");
            else
                textBox5.Text = DESEncrypt.Encrypt(textBox4.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
                MessageBox.Show("请输入加密密码!");
            else
                textBox4.Text = DESEncrypt.Decrypt(textBox5.Text);
        }

    }
}
