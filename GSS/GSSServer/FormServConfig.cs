using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using GSS.DBUtility;

namespace GSSServer
{
    public partial class FormServConfig : Form
    {

        public FormServConfig()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            formInit();
        }

        #region 窗口事件

        /// <summary>
        /// 服务端数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string gssip = cbGSSip.Text;
                string gssport = tPort.Text.Trim();
                if (!WinUtil.isIpaddres(gssip))
                {
                    MessageBox.Show("服务器IP地址格式不正确!");
                    return;
                }
                if (!WinUtil.IsNumber(gssport))
                {
                    MessageBox.Show("服务器端口号应该为数字!");
                    return;
                }

                string sql = "UPDATE GSSCONFIG SET GSSIP='" + gssip + "',GSSPORT='" + gssport + "'WHERE ID=1";
                int row = DbHelperSQLite.ExecuteSql(sql);
                if (row >= 1)
                {
                    MessageBox.Show("服务端参数保存成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("服务端参数保存失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                MessageBox.Show("服务端参数提交失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //日志记录
                ShareData.Log.Warn("服务端参数提交失败!" + ex.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 数据库参数保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string dbip = tDataIp.Text.Trim();
                string dbname = tDataName.Text.Trim();
                string uid = tDataUid.Text.Trim();
                string upsw = tDataPwd.Text.Trim();
                if (AppConfig.VerifyIp&& !WinUtil.isIpaddres(dbip))
                {
                    MessageBox.Show("数据库IP地址格式不正确!");
                    return;
                }
                if (dbname.Length == 0)
                {
                    MessageBox.Show("数据库名称能为空!");
                    return;
                }
                if (uid.Length == 0)
                {
                    MessageBox.Show("数据库用户名不能为空");
                    return;
                }
                if (upsw.Length == 0)
                {
                    MessageBox.Show("数据库密码不能为空");
                    return;
                }

                string sql = "UPDATE GSSCONFIG SET GSSDBIP='" + dbip + "',GSSDBNAME='" + dbname + "',GSSDBUID='" + uid + "',GSSDBPSW='" + upsw + "' WHERE ID=1";
                int row = DbHelperSQLite.ExecuteSql(sql);
                if (row >= 1)
                {
                    MessageBox.Show("GSS数据库服务器参数保存成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("GSS数据库服务器参数保存失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                MessageBox.Show("GSS数据库服务器参数提交失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //日志记录
                ShareData.Log.Warn("GSS数据库服务器参数提交失败!" + ex.Message);
            }
        }
        #endregion

        #region 私用方法
        /// <summary>
        /// 界面初始化
        /// </summary>
        private void formInit()
        {
            IPHostEntry iphost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipaddr in iphost.AddressList)
            {
                cbGSSip.Items.Add(ipaddr.ToString());
            }
            cbGSSip.SelectedIndex = 0;
            button1.Focus();

            //DbHelperSQLite.connectionString = "Data Source=" + Application.StartupPath + "\\GSSDATA\\GSSConfig.db;Version=3;Password=ssllyy";
            //DbHelperSQLite.connectionString = "Data Source=" + Application.StartupPath + "\\GSSDATA\\GSSConfig.db;Version=3;";
            try
            {
                string sqlstr = "SELECT * FROM GSSCONFIG WHERE ID=1";
                DataSet ds = DbHelperSQLite.Query(sqlstr);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    cbGSSip.Text = ds.Tables[0].Rows[0]["GSSIP"].ToString();
                    tPort.Text = ds.Tables[0].Rows[0]["GSSPORT"].ToString();
                    tDataIp.Text = ds.Tables[0].Rows[0]["GSSDBIP"].ToString();
                    tDataName.Text = ds.Tables[0].Rows[0]["GSSDBNAME"].ToString();
                    tDataUid.Text = ds.Tables[0].Rows[0]["GSSDBUID"].ToString();
                    tDataPwd.Text = ds.Tables[0].Rows[0]["GSSDBPSW"].ToString();
                }
            }
            catch (System.Exception ex)
            {
                ex.ToString().ErrorLogger();
                //日志记录
                ShareData.Log.Warn("GSS参数设置初始化失败!" + ex.Message);
            }

        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {

            string Server = tDataIp.Text;
            string Database = tDataName.Text;
            string Uid = tDataUid.Text;
            string Pwd = tDataPwd.Text;

            bool isok=DbHelperSQL.ConnectionTest(Server, Database, Uid, Pwd);
            if (isok)
            {
                MessageBox.Show("数据库连接测试成功", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("数据库连接测试失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
